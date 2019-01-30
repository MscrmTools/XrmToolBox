using NuGet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.PluginsStore.DTO;

namespace XrmToolBox.PluginsStore
{
    public class StoreFromPortal : IStore

    {
        public static readonly Version MinCompatibleVersion = new Version(1, 2015, 12, 20);

        private readonly string applicationPluginsFolder;
        private readonly string nugetPluginsFolder;
        private Dictionary<string, int> currentVersionDownloadsCount;
        private PackageManager manager;
        private FileInfo[] plugins;

        public StoreFromPortal()
        {
            // Initializing folders variables
            nugetPluginsFolder = Path.Combine(Paths.XrmToolBoxPath, "NugetPlugins");
            applicationPluginsFolder = Paths.PluginsPath;

            // Reading existing plugins files
            plugins = new DirectoryInfo(applicationPluginsFolder).GetFiles();

            // Repository initialization
            var repository = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
            manager = new PackageManager(repository, nugetPluginsFolder);
        }

        public event EventHandler PluginsUpdated;

        public bool HasUpdates => XrmToolBoxPlugins?.Plugins.Any(p => p.Action == PackageInstallAction.Update) ?? false;
        public int PluginsCount => XrmToolBoxPlugins?.Plugins.Count ?? 0;
        public XtbPlugins XrmToolBoxPlugins { get; set; }

        public long CalculateCacheFolderSize()
        {
            if (Directory.Exists(nugetPluginsFolder))
            {
                var size = GetDirectorySize(nugetPluginsFolder);
                return size / 1024 / 1024;
            }

            return 0;
        }

        public long CleanCacheFolder()
        {
            if (Directory.Exists(nugetPluginsFolder))
            {
                foreach (var item in new DirectoryInfo(nugetPluginsFolder).GetFileSystemInfos())
                {
                    if ((item.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        Directory.Delete(item.FullName, true);
                    }
                    else
                    {
                        File.Delete(item.FullName);
                    }
                }

                return CalculateCacheFolderSize();
            }

            return 0;
        }

        public XtbPlugin GetPluginByFileName(string filename)
        {
            if (XrmToolBoxPlugins == null)
            {
                LoadNugetPackages();
            }

            return XrmToolBoxPlugins.Plugins.FirstOrDefault(p => p.Files.Any(f => f.ToLower().IndexOf(filename.ToLower(), StringComparison.Ordinal) >= 0));
        }

        public string GetPluginProjectUrlByFileName(string fileName)
        {
            XtbPlugin plugin = GetPluginByFileName(fileName);
            return plugin?.ProjectUrl;
        }

        public XtbPlugin GetPluginUpdateByFile(string filepath)
        {
            var fi = new FileInfo(filepath);
            var plugin = XrmToolBoxPlugins.Plugins.FirstOrDefault(p => p.Files.Any(f => f.ToLower().Contains(fi.Name.ToLower())));
            if (plugin != null && plugin.Action == PackageInstallAction.Update)
            {
                return plugin;
            }

            return null;
        }

        public void LoadNugetPackages()
        {
            plugins = new DirectoryInfo(applicationPluginsFolder).GetFiles();

            XrmToolBoxPlugins = GetContent<XtbPlugins>("https://www.xrmtoolbox.com/_odata/plugins");

            foreach (var plugin in XrmToolBoxPlugins.Plugins)
            {
                AnalyzePackage(plugin);
                plugin.Compatibilty = IsPluginDependencyCompatible(new Version(plugin.MinimalXrmToolBoxVersion));
            }
        }

        public bool PerformInstallation(PluginUpdates updates, Form form)
        {
            if (updates.Plugins.Any(p => p.RequireRestart))
            {
                XmlSerializerHelper.SerializeToFile(updates, Path.Combine(Paths.XrmToolBoxPath, "Update.xml"));

                if (form is StoreFormFromPortal storeForm)
                {
                    form.Invoke(new Action(() =>
                    {
                        if (DialogResult.Yes == MessageBox.Show(form,
                                @"This application needs to restart to install updated plugins (or new plugins that share some files with already installed plugins). Click Yes to restart this application now",
                                @"Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                        {
                            storeForm.AskForPluginsClosing();

                            Application.Restart();
                        }
                    }));
                }

                return false;
            }

            foreach (var pu in updates.Plugins)
            {
                try
                {
                    // Can install plugin directly
                    var destinationDirectory = Path.GetDirectoryName(pu.Destination);
                    if (destinationDirectory == null)
                    {
                        continue;
                    }

                    if (!Directory.Exists(destinationDirectory))
                    {
                        Directory.CreateDirectory(destinationDirectory);
                    }
                    File.Copy(pu.Source, pu.Destination, true);
                }
                catch (Exception error)
                {
                    MessageBox.Show("An error occured while copying files: " + error.Message +
                                    "\r\n\r\nCopy has been aborted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            PluginsUpdated?.Invoke(this, new EventArgs());
            return true;
        }

        public void PerformUninstallation(PluginDeletions deletions)
        {
            string filePath = Path.Combine(Paths.XrmToolBoxPath, "Deletion.xml");

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    var existingPds = (PluginDeletions)XmlSerializerHelper.Deserialize(reader.ReadToEnd(), typeof(PluginDeletions));
                    deletions.Plugins.AddRange(existingPds.Plugins);
                }
            }

            XmlSerializerHelper.SerializeToFile(deletions, filePath);

            if (DialogResult.Yes == MessageBox.Show(
                "This application needs to restart to remove plugins. Click Yes to restart this application now",
                "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                Application.Restart();
            }
        }

        public PluginUpdates PrepareInstallationPackages(List<XtbPlugin> pluginsToInstall, BackgroundWorker worker = null)
        {
            var pus = new PluginUpdates { PreviousProcessId = Process.GetCurrentProcess().Id };
            int i = 0;
            foreach (var plugin in pluginsToInstall)
            {
                i++;
                worker?.ReportProgress(i * 100 / pluginsToInstall.Count, plugin.Name);

                var nugetPlugin =
                    manager.SourceRepository.FindPackage(plugin.NugetId, new SemanticVersion(plugin.Version), false, false);

                if (nugetPlugin == null)
                {
                    continue;
                }

                if (plugin.Action == PackageInstallAction.Unavailable)
                {
                    if (!string.IsNullOrEmpty(plugin.ProjectUrl))
                    {
                        var message =
                            $"{plugin.Name} is incompatible with this version of XrmToolBox.\nOpen project URL?";
                        if (DialogResult.Yes == MessageBox.Show(message, "Incompatible plugin", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                        {
                            Process.Start(plugin.ProjectUrl);
                        }
                    }
                    else
                    {
                        MessageBox.Show(
                            $"{plugin.Name} is incompatible with this version of XrmToolBox.",
                            "Incompatible plugin", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    continue;
                }

                manager.InstallPackage(nugetPlugin, true, false);

                var packageFolder = Path.Combine(nugetPluginsFolder, $"{nugetPlugin.Id}.{nugetPlugin.Version}");

                foreach (var fi in nugetPlugin.GetFiles())
                {
                    var destinationFile = Path.Combine(Paths.XrmToolBoxPath, fi.EffectivePath);

                    // XrmToolBox restart is required when a plugin has to be
                    // updated or when a new plugin shares files with other
                    // plugin(s) already installed
                    if (plugin.RequiresXtbRestart)
                    {
                        pus.Plugins.Add(new PluginUpdate
                        {
                            Source = Path.Combine(packageFolder, fi.Path),
                            Destination = destinationFile,
                            RequireRestart = true
                        });
                    }
                    else if (plugin.Action == PackageInstallAction.Install)
                    {
                        pus.Plugins.Add(new PluginUpdate
                        {
                            Source = Path.Combine(packageFolder, fi.Path),
                            Destination = destinationFile,
                            RequireRestart = false
                        });
                    }
                }
            }

            return pus;
        }

        public PluginDeletions PrepareUninstallPlugins(List<XtbPlugin> pluginsTodelete)
        {
            var pds = new PluginDeletions { PreviousProcessId = Process.GetCurrentProcess().Id };

            // Get list of files to delete
            foreach (var plugin in pluginsTodelete)
            {
                var conflicts = XrmToolBoxPlugins.Plugins.Where(p =>
                    p.Id != plugin.Id
                    && (p.Action == PackageInstallAction.None || p.Action == PackageInstallAction.Update)
                    && plugin.Files.Any(ff => p.Files.Contains(ff))).ToList();

                if (conflicts.Any())
                {
                    var conflictedFiles = conflicts.SelectMany(c => c.Files).Select(f => f);

                    pds.Plugins.Add(new PluginDeletion
                    {
                        Conflict = true,
                        Files = plugin.Files.Where(f => !conflictedFiles.Contains(f)).Select(f => Path.GetFileName(f)).ToList()
                    });
                }
                else
                {
                    pds.Plugins.Add(new PluginDeletion
                    {
                        Files = plugin.Files.Select(f => Path.GetFileName(f)).ToList()
                    });
                }
            }

            return pds;
        }

        public void UninstallByFileName(string fileName)
        {
            var plugin = GetPluginByFileName(fileName.ToLower());

            if (plugin != null)
            {
                var pds = PrepareUninstallPlugins(new List<XtbPlugin> { plugin });
                PerformUninstallation(pds);
            }
        }

        private void AnalyzePackage(XtbPlugin plugin)
        {
            var files = plugin.Files;

            bool install = false, update = false, otherFilesFound = false;

            if (string.IsNullOrEmpty(plugin.MinimalXrmToolBoxVersion))
            {
                plugin.Compatibilty = CompatibleState.Other;
            }
            else
            {
                plugin.Compatibilty = IsPluginDependencyCompatible(new Version(plugin.MinimalXrmToolBoxVersion));
            }

            var currentVersion = new Version(int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue);
            var currentVersionFound = false;

            foreach (var file in files)
            {
                var directoryName = Path.GetDirectoryName(file);
                if (directoryName == null)
                {
                    continue;
                }

                if (directoryName.ToLower().EndsWith("plugins"))
                {
                    // Only check version of files in the Plugins folder
                    var existingPluginFile =
                        plugins.FirstOrDefault(p => file.ToLower().EndsWith(p.Name.ToLower()));
                    if (existingPluginFile == null)
                    {
                        install = true;
                    }
                    else
                    {
                        // If a file is found, we check version only if the file
                        // contains classes that implement IXrmToolBoxPlugin
                        if (!existingPluginFile.ImplementsXrmToolBoxPlugin())
                        {
                            otherFilesFound = true;
                            continue;
                        }

                        var fileVersionInfo = FileVersionInfo.GetVersionInfo(existingPluginFile.FullName);
                        var fileVersion = new Version(fileVersionInfo.FileMajorPart, fileVersionInfo.FileMinorPart, fileVersionInfo.FileBuildPart, fileVersionInfo.FilePrivatePart);

                        var existingFileVersion = fileVersion;
                        if (existingFileVersion < currentVersion)
                        {
                            currentVersion = existingFileVersion;
                            currentVersionFound = true;
                        }
                        if (existingFileVersion < new Version(plugin.Version))
                        {
                            update = true;
                        }
                    }
                }
            }

            if (currentVersionFound)
            {
                plugin.CurrentVersion = currentVersion;
            }

            if (otherFilesFound || update)
            {
                plugin.RequiresXtbRestart = true;
            }

            if (plugin.Compatibilty != CompatibleState.Compatible)
            {
                plugin.Action = PackageInstallAction.Unavailable;
            }
            else if (update)
            {
                plugin.Action = PackageInstallAction.Update;
            }
            else if (install)
            {
                plugin.Action = PackageInstallAction.Install;
            }
            else
            {
                plugin.Action = PackageInstallAction.None;
            }
        }

        private T GetContent<T>(string url) where T : new()
        {
            try
            {
                var request = WebRequest.CreateHttp(url);
                var response = request.GetResponse();
                using (Stream dataStream = response.GetResponseStream())
                {
                    if (dataStream != null)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(T),
                            new DataContractJsonSerializerSettings
                            {
                                UseSimpleDictionaryFormat = true,
                                DateTimeFormat = new DateTimeFormat("yyyy-MM-dd'T'HH:mm:ss", new DateTimeFormatInfo { FullDateTimePattern = "yyyy-MM-dd'T'HH:mm:ss" })
                            });

                        return (T)serializer.ReadObject(dataStream);
                    }
                }
            }
            catch
            {
            }
            return new T();
        }

        private long GetDirectorySize(string path)
        {
            string[] files = Directory.GetFiles(path);
            string[] subdirectories = Directory.GetDirectories(path);

            long size = files.Sum(x => new FileInfo(x).Length);
            foreach (string s in subdirectories)
                size += GetDirectorySize(s);

            return size;
        }

        /// <summary>
        /// Verify version plugin is built for with current XTB version and a compatibility list
        /// </summary>
        /// <param name="xtbDependencyVersion"></param>
        /// <returns></returns>
        private CompatibleState IsPluginDependencyCompatible(Version xtbDependencyVersion)
        {
            if (xtbDependencyVersion >= MinCompatibleVersion
                && xtbDependencyVersion <= Assembly.GetEntryAssembly().GetName().Version)
            {
                return CompatibleState.Compatible;
            }

            if (xtbDependencyVersion < MinCompatibleVersion)
            {
                return CompatibleState.DoesntFitMinimumVersion;
            }

            if (xtbDependencyVersion > Assembly.GetEntryAssembly().GetName().Version)
            {
                return CompatibleState.RequireNewVersionOfXtb;
            }

            return CompatibleState.Other;
        }
    }
}