using Newtonsoft.Json;
using NuGet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace XrmToolBox.PluginsStore
{
    public class Store : IStore
    {
        public static readonly Version MinCompatibleVersion = new Version(1, 2015, 12, 20);

        private readonly string applicationPluginsFolder;
        private readonly PackageManager manager;
        private readonly string nugetPluginsFolder;
        private Dictionary<string, int> currentVersionDownloadsCount;
        private FileInfo[] plugins;

        public Store()
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

        public bool HasUpdates => Packages?.Any(p => p.Action == PackageInstallAction.Update) ?? false;
        public List<XtbNuGetPackage> Packages { get; private set; }
        public int PluginsCount => Packages?.Count ?? 0;

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

        public XtbNuGetPackage GetPackageByFileName(string fileName)
        {
            if (Packages == null || Packages.Count == 0)
            {
                LoadNugetPackages();
            }

            return Packages?.FirstOrDefault(p => p.Package.GetFiles().Any(f => f.EffectivePath.ToLower().IndexOf(fileName) >= 0));
        }

        public string GetPluginProjectUrlByFileName(string fileName)
        {
            XtbNuGetPackage package = GetPackageByFileName(fileName);
            return package?.Package?.ProjectUrl?.ToString();
        }

        public void LoadNugetPackages()
        {
            currentVersionDownloadsCount = new Dictionary<string, int>();

            // Retrieving last version download count for each plugin
            var request = WebRequest.CreateHttp("https://api-v2v3search-0.nuget.org/query?q=tags:XrmToolBox");
            dynamic nugetPackages;

            do
            {
                var response = request.GetResponse();
                string responseFromServer;
                using (Stream dataStream = response.GetResponseStream())
                {
                    if (dataStream == null) break;

                    using (StreamReader reader = new StreamReader(dataStream))
                    {
                        responseFromServer = reader.ReadToEnd();
                    }
                }
                nugetPackages = JsonConvert.DeserializeObject(responseFromServer);

                foreach (var plugin in nugetPackages["data"])
                {
                    currentVersionDownloadsCount.Add(plugin["id"].Value.ToString().ToLowerInvariant(), (int)plugin["versions"].Last["downloads"].Value);
                }

                request = WebRequest.CreateHttp("https://api-v2v3search-0.nuget.org/query?q=tags:XrmToolBox&skip=" + currentVersionDownloadsCount.Count);
            } while (nugetPackages["data"].Count == 20);

            // Reading existing plugins files
            plugins = new DirectoryInfo(applicationPluginsFolder).GetFiles();

            var packages = manager.SourceRepository.GetPackages()
                .Where(p => p.Tags.ToLower().StartsWith("xrmtoolbox")
                            && p.Tags.ToLower() != "xrmtoolbox"
                            && p.IsLatestVersion)
                .ToList();

            Packages = new List<XtbNuGetPackage>();
            foreach (var package in packages)
            {
                var xtbPackage = GetXtbPackage(package);
                Packages.Add(xtbPackage);
            }

            Action action = UpdateReleaseDates;
            action.BeginInvoke(ar => action.EndInvoke(ar), null);
        }

        public bool PerformInstallation(PluginUpdates updates)
        {
            if (updates.Plugins.Any(p => p.RequireRestart))
            {
                XmlSerializerHelper.SerializeToFile(updates, Path.Combine(Paths.XrmToolBoxPath, "Update.xml"));

                if (DialogResult.Yes == MessageBox.Show(
                    "This application needs to restart to install updated plugins (or new plugins that share some files with already installed plugins). Click Yes to restart this application now",
                    "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    Application.Restart();
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

        public PluginUpdates PrepareInstallationPackages(List<XtbNuGetPackage> packages)
        {
            var pus = new PluginUpdates { PreviousProcessId = Process.GetCurrentProcess().Id };

            foreach (var xtbPackage in packages)
            {
                if (xtbPackage.Action == PackageInstallAction.Unavailable)
                {
                    if (xtbPackage.Package.ProjectUrl != null &&
                        !string.IsNullOrEmpty(xtbPackage.Package.ProjectUrl.ToString()))
                    {
                        if (DialogResult.Yes ==
                            MessageBox.Show(
                                $"{xtbPackage.Package.Title}\nis incompatible with this version of XrmToolBox.\nOpen project URL?",
                                "Incompatible plugin", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                        {
                            Process.Start(xtbPackage.Package.ProjectUrl.ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show(
                            $"{xtbPackage.Package.Title}\nis incompatible with this version of XrmToolBox.",
                            "Incompatible plugin", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    continue;
                }
                manager.InstallPackage(xtbPackage.Package, true, false);

                var packageFolder = Path.Combine(nugetPluginsFolder,
                    xtbPackage.Package.Id + "." + xtbPackage.Package.Version);

                foreach (var fi in xtbPackage.Package.GetFiles())
                {
                    var destinationFile = Path.Combine(Paths.XrmToolBoxPath, fi.EffectivePath);

                    // XrmToolBox restart is required when a plugin has to be
                    // updated or when a new plugin shares files with other
                    // plugin(s) already installed
                    if (xtbPackage.RequiresXtbRestart)
                    {
                        pus.Plugins.Add(new PluginUpdate
                        {
                            Source = Path.Combine(packageFolder, fi.Path),
                            Destination = destinationFile,
                            RequireRestart = true
                        });
                    }
                    else if (xtbPackage.Action == PackageInstallAction.Install)
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

        public PluginDeletions PrepareUninstallPlugins(List<XtbNuGetPackage> packages)
        {
            var pds = new PluginDeletions { PreviousProcessId = Process.GetCurrentProcess().Id };

            // Get list of files to delete
            var packagesToDelete = new Dictionary<XtbNuGetPackage, List<string>>();

            foreach (var p in packages)
            {
                var files = p.Package.GetFiles();
                packagesToDelete.Add(p, files.Select(f => f.EffectivePath).ToList());
            }

            foreach (var package in packagesToDelete)
            {
                var conflicts = Packages.Where(p =>
                    !packagesToDelete.ContainsKey(p)
                    && (p.Action == PackageInstallAction.None || p.Action == PackageInstallAction.Update)
                    && p.Package.GetFiles().Any(ff => package.Value.Contains(ff.EffectivePath))).ToList();

                if (conflicts.Any())
                {
                    var conflictedFiles = conflicts.SelectMany(c => c.Package.GetFiles()).Select(f => f.EffectivePath);

                    pds.Plugins.Add(new PluginDeletion
                    {
                        Conflict = true,
                        Files = package.Value.Where(f => !conflictedFiles.Contains(f)).ToList()
                    });
                }
                else
                {
                    pds.Plugins.Add(new PluginDeletion
                    {
                        Files = package.Value,
                    });
                }
            }

            return pds;
        }

        public void UninstallByFileName(string fileName)
        {
            var package = GetPackageByFileName(fileName.ToLower());

            if (package != null)
            {
                var pds = PrepareUninstallPlugins(new List<XtbNuGetPackage> { package });
                PerformUninstallation(pds);
            }
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

        private XtbNuGetPackage GetXtbPackage(IPackage package)
        {
            var xtbPackage = new XtbNuGetPackage(package, PackageInstallAction.None, currentVersionDownloadsCount);

            var files = package.GetFiles();

            bool install = false, update = false, otherFilesFound = false;

            var xtbDependency = package.FindDependency("XrmToolBox", null);
            if (xtbDependency != null)
            {
                var xtbDependencyVersion = xtbDependency.VersionSpec.MinVersion.Version;
                xtbPackage.Compatibilty = IsPluginDependencyCompatible(xtbDependencyVersion);
            }
            else
            {
                xtbDependency = package.FindDependency("XrmToolBoxPackage", null);
                if (xtbDependency != null)
                {
                    var xtbDependencyVersion = xtbDependency.VersionSpec.MinVersion.Version;
                    xtbPackage.Compatibilty = IsPluginDependencyCompatible(xtbDependencyVersion);
                }
                else
                {
                    xtbPackage.Compatibilty = CompatibleState.Other;
                }
            }

            var currentVersion = new Version(int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue);
            var currentVersionFound = false;

            foreach (var file in files)
            {
                var directoryName = Path.GetDirectoryName(file.EffectivePath);
                if (directoryName == null)
                {
                    continue;
                }

                if (directoryName.ToLower() == "plugins")
                {
                    // Only check version of files in the Plugins folder
                    var existingPluginFile =
                        plugins.FirstOrDefault(p => file.EffectivePath.ToLower().EndsWith(p.Name.ToLower()));
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
                        if (existingFileVersion < package.Version.Version)
                        {
                            update = true;
                        }
                    }
                }
            }

            if (currentVersionFound)
            {
                xtbPackage.CurrentVersion = currentVersion;
            }

            if (otherFilesFound || update)
            {
                xtbPackage.RequiresXtbRestart = true;
            }

            if (xtbPackage.Compatibilty != CompatibleState.Compatible)
            {
                xtbPackage.Action = PackageInstallAction.Unavailable;
            }
            else if (update)
            {
                xtbPackage.Action = PackageInstallAction.Update;
            }
            else if (install)
            {
                xtbPackage.Action = PackageInstallAction.Install;
            }
            else
            {
                xtbPackage.Action = PackageInstallAction.None;
            }

            return xtbPackage;
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

        private void UpdateReleaseDates()
        {
            foreach (var package in Packages)
            {
                var allPackages = manager.SourceRepository.GetPackages()
                    .Where(p => p.Id == package.Package.Id).ToList();

                var first = allPackages.Select(p => (DataServicePackage)p).OrderBy(p => p.LastUpdated).FirstOrDefault();

                package.FirstReleaseDate = first?.LastUpdated.Date ?? new DateTime();
                package.LatestReleaseDate = ((DataServicePackage)package.Package).LastUpdated.Date;
            }
        }
    }
}