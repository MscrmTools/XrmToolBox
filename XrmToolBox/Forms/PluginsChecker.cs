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
        private FileInfo[] plugins;

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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        internal List<XtbNuGetPackage> RetrieveNugetPackages()
        {
            var packages = manager.SourceRepository.GetPackages()
                      .Where(p => p.Tags.ToLower().StartsWith("xrmtoolbox")
                                  && p.Tags.ToLower() != "xrmtoolbox"
                                  && p.IsLatestVersion)
                      .ToList();

            var list = new List<XtbNuGetPackage>();
            foreach (var package in packages)
            {
                list.Add(GetXtbPackage(package));
            }

            return list;
        }

        public void RefreshPluginsList()
        {
            plugins = new DirectoryInfo(applicationPluginsFolder).GetFiles();

            lvPlugins.Items.Clear();
            tssLabel.Text = "Retrieving plugins from Nuget feed...";
            tssProgress.Visible = true;

            var bw = new BackgroundWorker();
            bw.DoWork += (sender, e) =>
            {
                var options = ((MainForm)Owner).Options;

                var xtbPackages = RetrieveNugetPackages();

                var lvic = new List<ListViewItem>();
                foreach (var xtbPackage in xtbPackages)
                {
                    if (xtbPackage.Action == PackageInstallAction.Unavailable
                        && options.PluginsStoreShowIncompatible.HasValue
                        && options.PluginsStoreShowIncompatible.Value == false)
                    {
                        continue;
                    }

                    if (xtbPackage.Action == PackageInstallAction.Install
                       && options.PluginsStoreShowNew.HasValue
                       && options.PluginsStoreShowNew.Value == false)
                    {
                        continue;
                    }

                    if (xtbPackage.Action == PackageInstallAction.Update
                       && options.PluginsStoreShowUpdates.HasValue
                       && options.PluginsStoreShowUpdates.Value == false)
                    {
                        continue;
                    }

                    if (xtbPackage.Action == PackageInstallAction.None
                      && options.PluginsStoreShowInstalled.HasValue
                      && options.PluginsStoreShowInstalled.Value == false)
                    {
                        continue;
                    }

                    lvic.Add(xtbPackage.GetPluginsStoreItem());
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

            // TODO: Don't compare with all files, plugin packages may contain other dll's and exe's that have other versioning
            // How to determine actual version of existing plugin?
            foreach (var file in files)
            {
                if (Path.GetDirectoryName(file.EffectivePath).ToLower() == "plugins")
                {
                    // Only check version of files in the Plugins folder
                    var existingPluginFile = plugins.FirstOrDefault(p => file.EffectivePath.EndsWith(p.Name));
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

                        var existingFileVersion = existingPluginFile.GetAssemblyVersion();
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

        private void lvPlugins_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvPlugins.Sorting = lvPlugins.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lvPlugins.ListViewItemSorter = new ListViewItemComparer(e.Column, lvPlugins.Sorting);
        }

        private void PluginsChecker_Load(object sender, EventArgs e)
        {
            var options = ((MainForm)Owner).Options;

            tsbShowThisScreenOnStartup.Checked = options.DisplayPluginsStoreOnStartup;
            tsmiShowInstalledPlugins.Checked = options.PluginsStoreShowInstalled.HasValue ? options.PluginsStoreShowInstalled.Value : true;
            tsmiShowNewPlugins.Checked = options.PluginsStoreShowNew.HasValue ? options.PluginsStoreShowNew.Value : true;
            tsmiShowPluginsNotCompatible.Checked = options.PluginsStoreShowIncompatible.HasValue ? options.PluginsStoreShowIncompatible.Value : true;
            tsmiShowPluginsUpdate.Checked = options.PluginsStoreShowUpdates.HasValue ? options.PluginsStoreShowUpdates.Value : true;

            RefreshPluginsList();
        }

        private void tsbInstall_Click(object sender, EventArgs e)
        {
            if (lvPlugins.CheckedItems.Count == 0)
                return;

            ((MainForm)Owner).EnableNewPluginsWatching(false);

            var pus = new PluginUpdates { PreviousProcessId = Process.GetCurrentProcess().Id };

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

                    // XrmToolBox restart is required when a plugin has to be 
                    // updated or when a new plugin shares files with other 
                    // plugin(s) already installed
                    if (xtbPackage.RequiresXtbRestart)
                    {
                        pus.Plugins.Add(new PluginUpdate
                        {
                            Source = Path.Combine(packageFolder, fi.Path),
                            Destination = destinationFile
                        });
                    }
                    else if (xtbPackage.Action == PackageInstallAction.Install)
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
                }
            }

            if (pus.Plugins.Count > 0)
            {
                XmlSerializerHelper.SerializeToFile(pus, Path.Combine(applicationFolder, "Update.xml"));

                if (DialogResult.Yes == MessageBox.Show(
                    "This application needs to restart to install updated plugins (or new plugins that share some files with already installed plugins). Click Yes to restart this application now",
                    "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    Application.Restart();
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
            ToolStripButton ctrl = (ToolStripButton)sender;

            ((MainForm)Owner).Options.DisplayPluginsStoreOnStartup = ctrl.Checked;
            ((MainForm)Owner).Options.Save();
        }

        private void lvPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlReleaseNotesDetails.Controls.Clear();

            if (lvPlugins.SelectedItems.Count == 0)
            {
                return;
            }

            var item = lvPlugins.SelectedItems[0];
            var releaseNotes = ((XtbNuGetPackage)item.Tag).Package.ReleaseNotes;

            BuildPropertiesPanel(((XtbNuGetPackage)item.Tag).Package);

            if (!string.IsNullOrEmpty(releaseNotes))
            {
                Uri releaseNotesUri;
                if(Uri.TryCreate(releaseNotes, UriKind.Absolute, out releaseNotesUri))
                {
                    var llbl = new LinkLabel { Text = releaseNotes };
                    llbl.Click += (s, evt) => { Process.Start(((LinkLabel)s).Text); };
                    llbl.AutoSize = false;
                    llbl.Dock = DockStyle.Fill;

                    pnlReleaseNotesDetails.Controls.Add(llbl);
                }
                else
                {
                    var lbl = new Label { Text = releaseNotes };
                    lbl.Dock = DockStyle.Fill;
                    lbl.AutoSize = false;

                    pnlReleaseNotesDetails.Controls.Add(lbl);
                }
            }
            else
            {
                var lbl = new Label { Text = "N/A" };
                lbl.Dock = DockStyle.Fill;
                lbl.AutoSize = false;

                pnlReleaseNotesDetails.Controls.Add(lbl);
            }

        }

        private void BuildPropertiesPanel(IPackage package)
        {
            scProperties.Panel1.Controls.Clear();

            var bitmap = new PictureBox
            {
                Size = new Size(48, 48),
                Dock = DockStyle.Left,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            if (package.IconUrl != null)
                bitmap.Load(package.IconUrl.AbsoluteUri);
            else
                bitmap.Load("https://raw.githubusercontent.com/wiki/MscrmTools/XrmToolBox/Images/unknown.png");
           
            var lblTitle = new Label
            {
                Dock = DockStyle.Top,
                Text = package.Title.Replace(" for XrmToolBox", ""),
                Font = new Font("Microsoft Sans Serif", 20F),
                Height = 32
            };

            var lblDescription = new Label
            {
                Dock = DockStyle.Fill,
                Text = package.Description,
                Height = 16
            };

            var pnlTitle = new Panel
            {
                Height = 48,
                Dock = DockStyle.Top
            };

            pnlTitle.Controls.AddRange(new Control[] { lblDescription, lblTitle, bitmap });
            
            scProperties.Panel1.Controls.AddRange(new Control[] {
                GetPropertiesPanelInformation("Project Url", package.ProjectUrl),
                GetPropertiesPanelInformation("Downloads count", package.DownloadCount.ToString()),
                GetPropertiesPanelInformation("Authors", string.Join(", ", package.Authors)),
                GetPropertiesPanelInformation("Version", package.Version.ToString()),
                pnlTitle });
        }
        private Panel GetPropertiesPanelInformation(string label, object value)
        {
            var lblLabel = new Label
            {
                Dock = DockStyle.Left,
                Text = label.ToString(),
                Width = 100,
                Height = 20
            };

            Control rightControl = null;
            var stringValue = value as string;
            if(stringValue != null)
            {
                rightControl = new Label
                {
                    Dock = DockStyle.Fill,
                    Text = stringValue,
                };
            }

            var uriValue = value as Uri;
            if (uriValue != null)
            {
                rightControl = new LinkLabel
                {
                    Dock = DockStyle.Fill,
                    Text = uriValue.AbsoluteUri,
                };
                rightControl.Click += (sender, e) => {
                    Process.Start(((LinkLabel)sender).Text);
                };
            }

            if(rightControl == null)
            {
                rightControl = new Label
                {
                    Dock = DockStyle.Fill,
                    Text = "N/A",
                };
            }

            var pnl = new Panel
            {
                Height = 20,
                Dock = DockStyle.Top
            };

            pnl.Controls.AddRange(new Control[] { rightControl, lblLabel });

            return pnl;
        }

        private void tsmiPluginDisplayOption_Click(object sender, EventArgs e)
        {
            var options = ((MainForm)Owner).Options;
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            if(item == tsmiShowInstalledPlugins)
            {
                options.PluginsStoreShowInstalled = item.Checked;
            }
            else if (item == tsmiShowNewPlugins)
            {
                options.PluginsStoreShowNew = item.Checked;
            }
            else if (item == tsmiShowPluginsNotCompatible)
            {
                options.PluginsStoreShowIncompatible = item.Checked;
            }
            else if (item == tsmiShowPluginsUpdate)
            {
                options.PluginsStoreShowUpdates = item.Checked;
            }

            options.Save();

            RefreshPluginsList();
        }
    }

    internal class XtbNuGetPackage
    {
        public PackageInstallAction Action;
        public bool RequiresXtbRestart { get; set; }
        public IPackage Package;
        public Version CurrentVersion { get; set; }

        public XtbNuGetPackage(IPackage package, PackageInstallAction action)
        {
            Action = action;
            Package = package;
        }

        public ListViewItem GetPluginsStoreItem()
        {
            var item = new ListViewItem(this.ToString());
            item.Tag = this;
            item.SubItems.Add(Package.Version.ToString());
            item.SubItems.Add(CurrentVersion?.ToString()); 
            item.SubItems.Add(Package.Description);
            item.SubItems.Add(string.Join(", ", Package.Authors));
            var actionItem = item.SubItems.Add("None");
            item.SubItems.Add(Package.DownloadCount.ToString());

            switch (Action)
            {
                case PackageInstallAction.Unavailable:
                    actionItem.Text = "Incompatible";
                    item.ForeColor = Color.Red;
                    break;
                case PackageInstallAction.Update:
                    actionItem.Text = "Update";
                    item.ForeColor = Color.Blue;
                    break;
                case PackageInstallAction.Install:
                    actionItem.Text = "Install";
                    item.ForeColor = Color.Black;
                    break;
                case PackageInstallAction.None:
                default:
                    actionItem.Text = "N/A";
                    item.ForeColor = Color.Gray;
                    break;
            }

            return item;
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