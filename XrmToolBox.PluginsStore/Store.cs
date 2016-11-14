using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NuGet;
using XrmToolBox.Extensibility;

namespace XrmToolBox.PluginsStore
{
    public class Store
    {
        private static readonly Version MinCompatibleVersion = new Version(1, 2015, 12, 20);

        private readonly string applicationPluginsFolder;
        private readonly PackageManager manager;
        private readonly string nugetPluginsFolder;
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

        public List<XtbNuGetPackage> Packages { get; private set; }

        public event EventHandler PluginsUpdated;

        private XtbNuGetPackage GetXtbPackage(IPackage package)
        {
            var xtbPackage = new XtbNuGetPackage(package, PackageInstallAction.None);

            var files = package.GetFiles();

            bool install = false, update = false, compatible = false, otherFilesFound = false;

            var xtbDependency = package.FindDependency("XrmToolBox", null);
            if (xtbDependency != null)
            {
                var xtbDependencyVersion = xtbDependency.VersionSpec.MinVersion.Version;
                compatible = IsPluginDependencyCompatible(xtbDependencyVersion);
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

                        var existingFileVersion = new Version(FileVersionInfo.GetVersionInfo(existingPluginFile.FullName).FileVersion);
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

            if (!compatible)
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

        private bool IsPluginDependencyCompatible(Version xtbDependencyVersion)
        {
            // Verify version plugin is built for with current XTB version and a compatibility list
            return xtbDependencyVersion >= MinCompatibleVersion;
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

        public void LoadNugetPackages()
        {
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
                Packages.Add(GetXtbPackage(package));
            }
        }

        public XtbNuGetPackage GetPackageByFileName(string fileName)
        {
            if (Packages == null || Packages.Count == 0)
            {
                LoadNugetPackages();
            }

            return Packages.FirstOrDefault(p => p.Package.GetFiles().Any(f => f.EffectivePath.ToLower().IndexOf(fileName) >= 0));
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

        public void PerformUninstallation(PluginDeletions deletions)
        {
            string filePath = Path.Combine(Paths.XrmToolBoxPath, "Deletion.xml");

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    var existingPds = (PluginDeletions) XmlSerializerHelper.Deserialize(reader.ReadToEnd(), typeof(PluginDeletions));
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

        public long CalculateCacheFolderSize()
        {
            if (Directory.Exists(nugetPluginsFolder))
            {
                var size = GetDirectorySize(nugetPluginsFolder);
                return size/1024/1024;

            }

            return 0;
        }
    }
}
