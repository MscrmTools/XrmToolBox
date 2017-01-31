using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using NuGet;
using XrmToolBox.Extensibility;

namespace XrmToolBox.PluginsStore
{
    public enum PackageInstallAction
    {
        None,
        Install,
        Update,
        Unavailable
    }

    public partial class StoreForm : Form
    {
        // This is the minimum version of XrmToolBox that does not include breaking changes
        private static Version MinCompatibleVersion = new Version(1, 2015, 12, 20);

        private readonly string applicationDataFolder;
        private readonly string applicationPluginsFolder;
        private readonly PackageManager manager;
        private readonly string nugetPluginsFolder;
        private FileInfo[] plugins;
        private List<XtbNuGetPackage> xtbPackages;
        private readonly List<string> selectedPackagesId;

        public StoreForm()
        {
            InitializeComponent();

            // Initializing folders variables
            var userApplicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            applicationDataFolder = Path.Combine(userApplicationDataPath, "XrmToolBox");
            nugetPluginsFolder = Path.Combine(applicationDataFolder, "NugetPlugins");
            applicationPluginsFolder = Paths.PluginsPath;

            // Reading existing plugins files
            plugins = new DirectoryInfo(applicationPluginsFolder).GetFiles();

            // Repository initialization
            var repository = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
            manager = new PackageManager(repository, nugetPluginsFolder);

            // Display cache folder size
            CalculateCacheFolderSize();

            selectedPackagesId = new List<string>();
        }

        public event EventHandler PluginsUpdated;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public List<XtbNuGetPackage> RetrieveNugetPackages()
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
            selectedPackagesId.Clear();

            tstSearch.TextChanged -= tstSearch_TextChanged;
            tstSearch.Text = "Search by Title or Authors";
            tstSearch.ForeColor = SystemColors.InactiveCaption;
            tstSearch.TextChanged += tstSearch_TextChanged;
            tstSearch.Enabled = false;

            plugins = new DirectoryInfo(applicationPluginsFolder).GetFiles();

            lvPlugins.Items.Clear();
            tssLabel.Text = "Retrieving plugins from Nuget feed...";
            tssProgress.Visible = true;

            var bw = new BackgroundWorker();
            bw.DoWork += (sender, e) =>
            {
                var options = Options.Instance;

                xtbPackages = RetrieveNugetPackages();

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
                tstSearch.Enabled = true;

                if (e.Error != null)
                {
                    MessageBox.Show(e.Error.Message);
                    return;
                }

                lvPlugins.Items.AddRange(((List<ListViewItem>) e.Result).ToArray());
            };
            bw.RunWorkerAsync();
        }

        private void CalculateCacheFolderSize()
        {
            if (Directory.Exists(nugetPluginsFolder))
            {
                var size = GetDirectorySize(nugetPluginsFolder);
                tsbCleanCacheFolder.ToolTipText = string.Format(
                    "Clean XrmToolBox Plugins Store cache folder\r\n\r\nCurrent cache folder size: {0}MB",
                    size/1024/1024);
            }
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

        private void lvPlugins_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 0)
            {
                bool value = false;
                try
                {
                    value = Convert.ToBoolean(this.lvPlugins.Columns[e.Column].Tag);
                }
                catch (Exception)
                {
                }
                this.lvPlugins.Columns[e.Column].Tag = !value;
                foreach (ListViewItem item in this.lvPlugins.Items)
                    item.Checked = !value;

                this.lvPlugins.Invalidate();
            }
            else
            {
                lvPlugins.Sorting = lvPlugins.Sorting == SortOrder.Ascending
                    ? SortOrder.Descending
                    : SortOrder.Ascending;
                lvPlugins.ListViewItemSorter = new ListViewItemComparer(e.Column, lvPlugins.Sorting);
            }
        }

        private void lvPlugins_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.DrawBackground();
                bool value = false;
                try
                {
                    value = Convert.ToBoolean(e.Header.Tag);
                }
                catch (Exception)
                {
                }
                CheckBoxRenderer.DrawCheckBox(e.Graphics,
                    new Point(e.Bounds.Left + 4, e.Bounds.Top + 4),
                    value ? System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal :
                    System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
            }
            else
            {
                e.DrawDefault = true;
            }
        }

        private void lvPlugins_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void lvPlugins_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void PluginsChecker_Load(object sender, EventArgs e)
        {
            var options = Options.Instance;

            tsbShowThisScreenOnStartup.Checked = options.DisplayPluginsStoreOnStartup.HasValue && options.DisplayPluginsStoreOnStartup.Value;
            tsmiShowInstalledPlugins.Checked = options.PluginsStoreShowInstalled.HasValue
                ? options.PluginsStoreShowInstalled.Value
                : true;
            tsmiShowNewPlugins.Checked = options.PluginsStoreShowNew.HasValue ? options.PluginsStoreShowNew.Value : true;
            tsmiShowPluginsNotCompatible.Checked = options.PluginsStoreShowIncompatible.HasValue
                ? options.PluginsStoreShowIncompatible.Value
                : true;
            tsmiShowPluginsUpdate.Checked = options.PluginsStoreShowUpdates.HasValue
                ? options.PluginsStoreShowUpdates.Value
                : true;

            RefreshPluginsList();

            tstSearch.Focus();
        }

        public PluginUpdates PrepareInstallationPackages(List<XtbNuGetPackage> xtbPackages)
        {
            var pus = new PluginUpdates {PreviousProcessId = Process.GetCurrentProcess().Id};

            foreach (var xtbPackage in xtbPackages)
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
                    var destinationFile = Path.Combine(applicationDataFolder, fi.EffectivePath);

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

        public void PerformInstallation(PluginUpdates updates)
        {
            if (updates.Plugins.Any(p => p.RequireRestart))
            {
                XmlSerializerHelper.SerializeToFile(updates, Path.Combine(applicationDataFolder, "Update.xml"));

                if (DialogResult.Yes == MessageBox.Show(
                    "This application needs to restart to install updated plugins (or new plugins that share some files with already installed plugins). Click Yes to restart this application now",
                    "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    Application.Restart();
                }
            }
            else
            {
                foreach (var pu in updates.Plugins)
                {
                    try
                    {
                        // Can install plugin directly
                        var destinationDirectory = Path.GetDirectoryName(pu.Destination);
                        if (!Directory.Exists(destinationDirectory))
                        {
                            Directory.CreateDirectory(destinationDirectory);
                        }
                        File.Copy(pu.Source, pu.Destination, true);
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show(this,
                            "An error occured while copying files: " + error.Message +
                            "\r\n\r\nCopy has been aborted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                PluginsUpdated?.Invoke(this, new EventArgs());

                // Refresh plugins list when installation is done
                RefreshPluginsList();
                CalculateCacheFolderSize();
                MessageBox.Show("Installation done!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tsbInstall_Click(object sender, EventArgs e)
        {
            var packages =
                lvPlugins.CheckedItems.Cast<ListViewItem>()
                    .Where(l => l.Tag is XtbNuGetPackage)
                    .Select(l => (XtbNuGetPackage) l.Tag)
                    .ToList();
            if (packages.Count == 0)
            {
                packages =
                    lvPlugins.SelectedItems.Cast<ListViewItem>()
                        .Where(l => l.Tag is XtbNuGetPackage)
                        .Select(l => (XtbNuGetPackage) l.Tag)
                        .ToList();
            }

            if (packages.Count == 0)
            {
                return;
            }

            var updates = PrepareInstallationPackages(packages);

            PerformInstallation(updates);
        }

        private void tsbLoadPlugins_Click(object sender, EventArgs e)
        {
            RefreshPluginsList();
        }

        private void tsbShowThisScreenOnStartup_Click(object sender, EventArgs e)
        {
            ToolStripButton ctrl = (ToolStripButton) sender;

            Options.Instance.DisplayPluginsStoreOnStartup = ctrl.Checked;
            Options.Instance.Save();
        }

        private void lvPlugins_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                selectedPackagesId.Add(((XtbNuGetPackage) e.Item.Tag).Package.Id);
            }
            else
            {
                selectedPackagesId.Remove(((XtbNuGetPackage)e.Item.Tag).Package.Id);
            }
        }

        private void lvPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlReleaseNotesDetails.Controls.Clear();

            if (lvPlugins.SelectedItems.Count == 0)
            {
                return;
            }

            var item = lvPlugins.SelectedItems[0];
            var releaseNotes = ((XtbNuGetPackage) item.Tag).Package.ReleaseNotes;

            BuildPropertiesPanel(((XtbNuGetPackage) item.Tag).Package);

            if (!string.IsNullOrEmpty(releaseNotes))
            {
                Uri releaseNotesUri;
                if (Uri.TryCreate(releaseNotes, UriKind.Absolute, out releaseNotesUri))
                {
                    var llbl = new LinkLabel {Text = releaseNotes};
                    llbl.Click += (s, evt) => { Process.Start(((LinkLabel) s).Text); };
                    llbl.AutoSize = false;
                    llbl.Dock = DockStyle.Fill;

                    pnlReleaseNotesDetails.Controls.Add(llbl);
                }
                else
                {
                    var lbl = new Label {Text = releaseNotes};
                    lbl.Dock = DockStyle.Fill;
                    lbl.AutoSize = false;

                    pnlReleaseNotesDetails.Controls.Add(lbl);
                }
            }
            else
            {
                var lbl = new Label {Text = "N/A"};
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

            if (lblDescription.Text.Contains("\n"))
            {
                pnlTitle.Controls.AddRange(new Control[] { lblTitle, bitmap });

                var pnlDescription = new Panel
                {
                    AutoScroll = true,
                    AutoScrollMinSize = new Size(0, 1000),
                    Dock = DockStyle.Fill
                };
                pnlDescription.Controls.Add(lblDescription);

                var lblDescriptionHeader = new Label
                {
                    Dock = DockStyle.Top,
                    Text = "Description",
                    Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point),
                    Height = 16
                };

                scProperties.Panel1.Controls.AddRange(new Control[]
                {
                pnlDescription,
                lblDescriptionHeader,
                GetPropertiesPanelInformation("Project Url", package.ProjectUrl),
                GetPropertiesPanelInformation("Downloads count", package.DownloadCount.ToString()),
                GetPropertiesPanelInformation("Authors", string.Join(", ", package.Authors)),
                GetPropertiesPanelInformation("Version", package.Version.ToString()),
                pnlTitle
                });
            }
            else
            {
                pnlTitle.Controls.AddRange(new Control[] { lblDescription, lblTitle, bitmap });

                scProperties.Panel1.Controls.AddRange(new Control[]
                {
                GetPropertiesPanelInformation("Project Url", package.ProjectUrl),
                GetPropertiesPanelInformation("Downloads count", package.DownloadCount.ToString()),
                GetPropertiesPanelInformation("Authors", string.Join(", ", package.Authors)),
                GetPropertiesPanelInformation("Version", package.Version.ToString()),
                pnlTitle
                });
            }
        }

        private Panel GetPropertiesPanelInformation(string label, object value)
        {
            var lblLabel = new Label
            {
                Dock = DockStyle.Left,
                Text = label,
                Width = 100,
                Height = 20
            };

            Control rightControl = null;
            var stringValue = value as string;
            if (stringValue != null)
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
                rightControl.Click += (sender, e) =>
                {
                    Process.Start(((LinkLabel) sender).Text);
                };
            }

            if (rightControl == null)
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

            pnl.Controls.AddRange(new [] {rightControl, lblLabel});

            return pnl;
        }

        private void tsmiPluginDisplayOption_Click(object sender, EventArgs e)
        {
            var options = Options.Instance;
            ToolStripMenuItem item = (ToolStripMenuItem) sender;

            if (item == tsmiShowInstalledPlugins)
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

        private void tsbProxySettings_Click(object sender, EventArgs e)
        {
            ProxySettingsHelper.registerSettings(this);
        }

        private void tsbCleanCacheFolder_Click(object sender, EventArgs e)
        {
            if (DialogResult.No ==
                MessageBox.Show(this, "Are you sure you want to delete XrmToolBox Plugins Store cache?", "Question",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return;
            }

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

                CalculateCacheFolderSize();
                MessageBox.Show(this, "Cache folder has been cleaned");
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

        private Thread searchThread;

        private void tstSearch_TextChanged(object sender, EventArgs e)
        {
            searchThread?.Abort();
            searchThread = new Thread(FilterPlugins);
            searchThread.Start(tstSearch.Text);
        }

        private void tstSearch_Enter(object sender, EventArgs e)
        {
            tstSearch.Text = string.Empty;
            tstSearch.ForeColor = SystemColors.WindowText;
        }

        private void FilterPlugins(object text)
        {
            var filter = text.ToString().ToLower();
            var options = Options.Instance;
            var lvic = new List<ListViewItem>();
            foreach (var xtbPackage in xtbPackages.Where(p => filter.Length > 0 &&
                                                              (p.Package.Title.ToLower().Replace(" for xrmtoolbox","").Contains(filter) ||
                                                               p.Package.Authors.Any(a => a.ToLower().Contains(filter)))
                                                               || filter.Length == 0)
                )
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

                // Check item if it was checked
                lvic.Last().Checked = selectedPackagesId.Contains(xtbPackage.Package.Id);
            }

            Invoke(new Action(() =>
            {
                lvPlugins.Items.Clear();
                lvPlugins.Items.AddRange(lvic.ToArray());
            }));
        }
    }

    public class XtbNuGetPackage
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
            var item = new ListViewItem(string.Empty);
            item.Tag = this;
            item.SubItems.Add(this.ToString());
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
                    return Package.Title.Replace(" for XrmToolBox","");
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