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
    public partial class PluginsChecker : Form
    {
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
            tssProgress.Visible = true;
            tssLabel.Text = "Retrieving plugins from Nuget feed...";

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
                    var item = new ListViewItem(package.Title);
                    item.SubItems.Add(package.Version.ToString());
                    item.SubItems.Add(string.Join(", ", package.Authors));
                    item.SubItems.Add(package.Description);
                    item.Tag = package;

                    var files = package.GetFiles();

                    bool install = false, update = false;

                    foreach (var file in files)
                    {
                        var existingPluginFile = plugins.FirstOrDefault(p => file.EffectivePath.EndsWith(p.Name));
                        if (existingPluginFile == null)
                        {
                            install = true;
                        }
                        else
                        {
                            var existingFileVersion = FileVersionInfo.GetVersionInfo(existingPluginFile.FullName);

                            if (new Version(existingFileVersion.FileVersion) < package.Version.Version)
                            {
                                update = true;
                            }
                        }
                    }

                    if (update)
                    {
                        item.SubItems.Add("Update");
                        item.ForeColor = Color.Blue;
                    }
                    else if (install)
                    {
                        item.SubItems.Add("Install");
                    }
                    else
                    {
                        item.SubItems.Add("None");
                        item.ForeColor = Color.Gray;
                    }

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

        private void PluginsChecker_Load(object sender, EventArgs e)
        {
            RefreshPluginsList();
        }

        private void tsbInstall_Click(object sender, EventArgs e)
        {
            if (lvPlugins.CheckedItems.Count == 0)
                return;

            var pus = new PluginUpdates();

            foreach (ListViewItem item in lvPlugins.CheckedItems.Cast<ListViewItem>().Where(l => l.ForeColor != Color.Gray))
            {
                var package = (IPackage)item.Tag;
                manager.InstallPackage(package, false, false);

                var packageFolder = Path.Combine(nugetPluginsFolder, package.Id + "." + package.Version);

                foreach (var fi in package.AssemblyReferences)
                {
                    if (item.ForeColor == DefaultForeColor)
                    {
                        try
                        {
                            // Can install plugin directly
                            File.Copy(Path.Combine(packageFolder, fi.Path), Path.Combine(applicationPluginsFolder, fi.Name));
                        }
                        catch (Exception error)
                        {
                            MessageBox.Show(this,
                                "An error occured while copying files: " + error.Message +
                                "\r\n\r\nCopy has been aborted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        pus.Plugins.Add(new PluginUpdate
                        {
                            Source = Path.Combine(packageFolder, fi.Path),
                            Destination = Path.Combine(applicationPluginsFolder, fi.Name)
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
        }

        private void tsbLoadPlugins_Click(object sender, EventArgs e)
        {
            RefreshPluginsList();
        }
    }
}