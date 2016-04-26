using NuGet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using XrmToolBox.AppCode;

namespace XrmToolBox.Forms
{
    internal enum PackageInstallAction
    {
        None,
        Install,
        Update,
        Unavailable
    }

    public partial class PluginsChecker : Form
    {
        // This is the minimum version of XrmToolBox that does not include breaking changes
        private static Version MinCompatibleVersion = new Version(1, 2015, 12, 20);

        private readonly string applicationDataFolder;
        private readonly string applicationFolder;
        private readonly string applicationPluginsFolder;
        private readonly PackageManager manager;
        private readonly string nugetPluginsFolder;
        private readonly FileInfo[] plugins;

        public PluginsChecker()
        {
            InitializeComponent();

            // Initializing folders variables
            applicationDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
             "XrmToolBox");
            nugetPluginsFolder = Path.Combine(applicationDataFolder, "Plugins");
            applicationFolder = new FileInfo(Assembly.GetExecutingAssembly().FullName).DirectoryName;
            applicationPluginsFolder = Path.Combine(applicationFolder, "Plugins");

            // Reading existing plugins files
            plugins = new DirectoryInfo(applicationPluginsFolder).GetFiles();

            // Repository initialization
            var repository = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
            manager = new PackageManager(repository, nugetPluginsFolder);
        }

        public void RefreshPluginsList()
        {
            lvPlugins.Items.Clear();
            tssLabel.Text = "Retrieving plugins from Nuget feed...";
            tssProgress.Visible = true;

            var bw = new BackgroundWorker();
            bw.DoWork += (sender, e) =>
            {
                var packages = manager.SourceRepository.GetPackages()
                    .Where(p => p.Tags.ToLower().StartsWith("xrmtoolbox")
                                && p.Tags.ToLower() != "xrmtoolbox"
                                && p.IsLatestVersion)
                    .ToList();

                var lvic = new List<ListViewItem>();
                foreach (var package in packages)
                {
                    var item = AddPackage(package);
                    lvic.Add(item);
                }
                e.Result = lvic;
            };
            bw.RunWorkerCompleted += (sender, e) =>
            {
                tssProgress.Visible = false;
                tssLabel.Text = string.Empty;

                if (e.Error != null)
                {
                    MessageBox.Show(e.Error.Message);
                    return;
                }

                lvPlugins.Items.AddRange(((List<ListViewItem>)e.Result).ToArray());
            };
            bw.RunWorkerAsync();
        }

        private ListViewItem AddPackage(IPackage package)
        {
            var xtbPackage = new XtbNuGetPackage(package, PackageInstallAction.None);
            var item = new ListViewItem(xtbPackage.ToString());
            item.Tag = xtbPackage;
            item.SubItems.Add(package.Version.ToString());
            var currentVerItem = item.SubItems.Add("");  //Current version
            item.SubItems.Add(package.Description);
            item.SubItems.Add(string.Join(", ", package.Authors));
            var actionItem = item.SubItems.Add("None");

            var files = package.GetFiles();

            bool install = false, update = false, compatible = false;

            var xtbDependency = package.FindDependency("XrmToolBox", null);
            if (xtbDependency != null)
            {
                var xtbDependencyVersion = xtbDependency.VersionSpec.MinVersion.Version;
                compatible = IsPluginDependencyCompatible(xtbDependencyVersion);
            }

            var currentVersion = new Version(int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue);
            var currentVersionFound = false;

            // TODO: Don't compare with all files, plugin packages may contain other dll's and exe's that have other versioning
            // How to determine actual version of existing plugin?
            foreach (var file in files)
            {
                if (Path.GetDirectoryName(file.EffectivePath).ToLower() == "plugins")
                {   // Only check version of files in the Plugins folder
                    var existingPluginFile = plugins.FirstOrDefault(p => file.EffectivePath.EndsWith(p.Name));
                    if (existingPluginFile == null)
                    {
                        install = true;
                    }
                    else
                    {
                        var existingFileVersion = FileVersionInfo.GetVersionInfo(existingPluginFile.FullName);
                        var fileVersion = Version.Parse(existingFileVersion.FileVersion);
                        if (fileVersion < currentVersion)
                        {
                            currentVersion = fileVersion;
                            currentVersionFound = true;
                        }
                        if (fileVersion < package.Version.Version)
                        {
                            update = true;
                        }
                    }
                }
            }
            if (currentVersionFound)
            {
                currentVerItem.Text = currentVersion.ToString();
            }
            if (!compatible)
            {
                actionItem.Text = "Incompatible";
                item.ForeColor = Color.Red;
                xtbPackage.Action = PackageInstallAction.Unavailable;
            }
            else if (update)
            {
                actionItem.Text = "Update";
                item.ForeColor = Color.Blue;
                xtbPackage.Action = PackageInstallAction.Update;
            }
            else if (install)
            {
                actionItem.Text = "Install";
                item.ForeColor = Color.Black;
                xtbPackage.Action = PackageInstallAction.Install;
            }
            else
            {
                actionItem.Text = "N/A";
                item.ForeColor = Color.Gray;
                xtbPackage.Action = PackageInstallAction.None;
            }

            return item;
        }

        private bool IsPluginDependencyCompatible(Version xtbDependencyVersion)
        {
            // Verify version plugin is built for with current XTB version and a compatibility list
            return xtbDependencyVersion >= MinCompatibleVersion;
        }

        private void PluginsChecker_Load(object sender, EventArgs e)
        {
            tsbShowThisScreenOnStartup.Checked = ((MainForm)Owner).Options.DisplayPluginsStoreOnStartup;

            RefreshPluginsList();
        }

        private void tsbInstall_Click(object sender, EventArgs e)
        {
            if (lvPlugins.CheckedItems.Count == 0)
                return;

            ((MainForm)Owner).EnableNewPluginsWatching(false);

            var pus = new PluginUpdates();

            foreach (ListViewItem item in lvPlugins.CheckedItems.Cast<ListViewItem>().Where(l => l.Tag is XtbNuGetPackage))
            {
                var xtbPackage = (XtbNuGetPackage)item.Tag;

                if (xtbPackage.Action == PackageInstallAction.Unavailable)
                {
                    if (xtbPackage.Package.ProjectUrl != null && !string.IsNullOrEmpty(xtbPackage.Package.ProjectUrl.ToString()))
                    {
                        if (DialogResult.Yes == MessageBox.Show($"{xtbPackage.Package.Title}\nis incompatible with this version of XrmToolBox.\nOpen project URL?", "Incompatible plugin", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                        {
                            Process.Start(xtbPackage.Package.ProjectUrl.ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show($"{xtbPackage.Package.Title}\nis incompatible with this version of XrmToolBox.", "Incompatible plugin", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    continue;
                }
                manager.InstallPackage(xtbPackage.Package, true, false);

                var packageFolder = Path.Combine(nugetPluginsFolder, xtbPackage.Package.Id + "." + xtbPackage.Package.Version);

                foreach (var fi in xtbPackage.Package.GetFiles())
                {
                    var destinationFile = Path.Combine(applicationFolder, fi.EffectivePath);
                    if (xtbPackage.Action == PackageInstallAction.Install)
                    {
                        try
                        {
                            // Can install plugin directly
                            var destinationDirectory = Path.GetDirectoryName(destinationFile);
                            if (!Directory.Exists(destinationDirectory))
                            {
                                Directory.CreateDirectory(destinationDirectory);
                            }
                            File.Copy(Path.Combine(packageFolder, fi.Path), destinationFile, true);
                        }
                        catch (Exception error)
                        {
                            MessageBox.Show(this,
                                "An error occured while copying files: " + error.Message +
                                "\r\n\r\nCopy has been aborted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else if (xtbPackage.Action == PackageInstallAction.Update)
                    {
                        pus.Plugins.Add(new PluginUpdate
                        {
                            Source = Path.Combine(packageFolder, fi.Path),
                            Destination = destinationFile
                        });
                    }
                }
            }

            if (pus.Plugins.Count > 0)
            {
                XmlSerializerHelper.SerializeToFile(pus, Path.Combine(applicationFolder, "Update.xml"));

                if (DialogResult.Yes == MessageBox.Show(
                    "This application needs to restart to install updated plugins. Click Yes to restart this application now",
                    "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    Application.Restart();
                    Environment.Exit(0);
                }
            }
            else
            {
                // Refresh plugins list when installation is done
                ((MainForm)Owner).ReloadPluginsList();
                RefreshPluginsList();
                MessageBox.Show("Installation done!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

             ((MainForm)Owner).EnableNewPluginsWatching(true);
        }

        private void tsbLoadPlugins_Click(object sender, EventArgs e)
        {
            RefreshPluginsList();
        }

        private void tsbShowThisScreenOnStartup_Click(object sender, EventArgs e)
        {
            ((ToolStripButton)sender).Checked = !((ToolStripButton)sender).Checked;
            ((MainForm)Owner).Options.DisplayPluginsStoreOnStartup = ((ToolStripButton)sender).Checked;
            ((MainForm)Owner).Options.Save();
        }
    }

    internal class XtbNuGetPackage
    {
        public PackageInstallAction Action;
        public IPackage Package;

        public XtbNuGetPackage(IPackage package, PackageInstallAction action)
        {
            Action = action;
            Package = package;
        }

        public override string ToString()
        {
            if (Package != null)
            {
                if (!string.IsNullOrWhiteSpace(Package.Title))
                {
                    return Package.Title;
                }
                else
                {
                    return Package.Id;
                }
            }
            return "?";
        }
    }
}