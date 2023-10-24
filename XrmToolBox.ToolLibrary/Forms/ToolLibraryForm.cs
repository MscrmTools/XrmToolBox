using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using XrmToolBox.ToolLibrary.AppCode;
using XrmToolBox.ToolLibrary.UserControls;

namespace XrmToolBox.ToolLibrary.Forms
{
    public partial class ToolLibraryForm : DockContent
    {
        public static readonly Version MinCompatibleVersion = new Version(1, 2015, 12, 20);
        private readonly ImageCache imageCache;
        private readonly ToolLibrary toolLibrary;
        private bool isUpdateOnly;
        private List<ListViewItem> items = new List<ListViewItem>();
        private FileInfo[] plugins;
        private Thread searchThread;
        private IToolLibrarySettings settings;
        private string toolsPackagesCachePath = Path.Combine(Paths.XrmToolBoxPath, "NugetPlugins");

        public ToolLibraryForm(ToolLibrary toolLibrary, IToolLibrarySettings settings)
        {
            InitializeComponent();

            this.toolLibrary = toolLibrary;

            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.DoubleBuffer,
                true);

            lblToolsSearch.SetAutoWidth();
            lblFilterCategory.SetAutoWidth();
            lblFilterRepository.SetAutoWidth();
            lblSeparator1.SetAutoWidth();
            lblSeparator2.SetAutoWidth();
            lblFilterUpdateInfo.SetAutoWidth();
            llRepoMoreInfo.SetAutoWidth();
            lblLoading.Location = new Point(lblLoading.Parent.Width / 2 - lblLoading.Width / 2, lblLoading.Parent.Height / 2 - lblLoading.Height / 2);

            tsmiOpenToolLibraryAlways.Checked = settings.DisplayPluginsStoreOnStartup && !settings.DisplayPluginsStoreOnlyIfUpdates;
            tsmiOpenToolLibraryOnlyIfUpdatesAreAvailable.Checked = settings.DisplayPluginsStoreOnStartup && settings.DisplayPluginsStoreOnlyIfUpdates;
            tsmiOpenToolLibraryNever.Checked = !settings.DisplayPluginsStoreOnStartup && !settings.DisplayPluginsStoreOnlyIfUpdates;

            foreach (var chk in pnlToolsTop.Controls.OfType<CheckBox>().Where(c => c.Image != null))
            {
                chk.Image = ResizeImage(chk.Image, 24, 24);
                chk.Width = 54;
            }

            pnlToolsTop.Height = 34;

            ToolTip tt = new ToolTip();
            tt.SetToolTip(chkFilterMvp, "Show tools from Microsoft MVPs only");
            tt.SetToolTip(chkFilterNew, "Show new tools only\n\nFirst release date not older than a month");
            tt.SetToolTip(chkFilterOpenSource, "Show open source tools only");
            tt.SetToolTip(chkFilterTopRating, $"Show top rated tools only\n\nAt least {settings.MostRatedMinNumberOfVotes} votes for an average rating greater or equal to {settings.MostRatedMinRatingAverage}\n\nThese parameters can be changed in your XrmToolBox settings");
            tt.SetToolTip(chkToInstall, "Show tools not yet installed");
            tt.SetToolTip(chkShowUpdates, "Show tools with updates");
            tt.SetToolTip(chkShowInstalled, "Show tools already installed");
            tt.SetToolTip(chkIncompatible, "Show tools not compatible with this version of XrmToolBox");

            imageCache = new ImageCache(Paths.XrmToolBoxPath, toolLibrary.HttpClient);
            imageCache.Load();
            InitImages();

            this.settings = settings;

            tssClearPackageCache.ToolTipText = $"Clean XrmToolBox Tool Library cache folder\n\nCurrent cache folder size: {CalculateCacheFolderSize()}MB";

            ShowRestartButton();
        }

        public event EventHandler PluginsClosingRequested;

        public event EventHandler<OpenPluginEventArgs> PluginsOpeningRequested;

        public long CalculateCacheFolderSize()
        {
            if (Directory.Exists(toolsPackagesCachePath))
            {
                var size = GetDirectorySize(toolsPackagesCachePath);
                return size / 1024 / 1024;
            }

            return 0;
        }

        public void SetFilters(PackageInstallAction filters)
        {
            chkShowInstalled.Checked = (filters & PackageInstallAction.None) == PackageInstallAction.None;
            chkShowUpdates.Checked = (filters & PackageInstallAction.Update) == PackageInstallAction.Update;
            chkToInstall.Checked = (filters & PackageInstallAction.Install) == PackageInstallAction.Install;
        }

        public void ShowUpdatesOnly()
        {
            isUpdateOnly = true;

            foreach (var cb in pnlToolsTop.Controls.OfType<CheckBox>())
            {
                cb.CheckedChanged -= chkFilter_CheckedChanged;
                cb.Checked = false;
                cb.CheckedChanged += chkFilter_CheckedChanged;
            }
            chkShowUpdates.Checked = true;
            pnlFilterUpdateInfo.Visible = true;
        }

        protected override void OnResizeBegin(EventArgs e)
        {
            SuspendLayout();
            base.OnResizeBegin(e);
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            ResumeLayout();
            base.OnResizeEnd(e);
        }

        private void Filter()
        {
            Thread.Sleep(500);

            var mi = new MethodInvoker(new Action(() =>
            {
                var filteredTools = items.Where(i =>
                       (txtSearch.Text.Length == 0 || (((XtbPlugin)i.Tag).Name.ToLower().IndexOf(txtSearch.Text, StringComparison.InvariantCultureIgnoreCase) >= 0
                       || ((XtbPlugin)i.Tag).Description.ToLower().IndexOf(txtSearch.Text, StringComparison.InvariantCultureIgnoreCase) >= 0
                       || ((XtbPlugin)i.Tag).Authors.ToLower().IndexOf(txtSearch.Text, StringComparison.InvariantCultureIgnoreCase) >= 0
                       ))
                       && (cbbCategories.SelectedIndex == 0 || (((XtbPlugin)i.Tag).Categories.Contains(cbbCategories.SelectedItem.ToString())))
                       && (cbbRepositories.SelectedItem.ToString() == "-- All --" || (((XtbPlugin)i.Tag).SourceRepositoryName == cbbRepositories.SelectedItem.ToString()))
                       && (!chkFilterOpenSource.Checked || (((XtbPlugin)i.Tag).IsOpenSource ?? false) && chkFilterOpenSource.Checked && !(((XtbPlugin)i.Tag).IsFromCustomRepo))
                       && (!chkFilterMvp.Checked || (((XtbPlugin)i.Tag).IsMvp ?? false) && chkFilterMvp.Checked && !(((XtbPlugin)i.Tag).IsFromCustomRepo))
                       && (!chkFilterTopRating.Checked || (((XtbPlugin)i.Tag).TotalFeedbackRating > settings.MostRatedMinNumberOfVotes && ((XtbPlugin)i.Tag).AverageFeedbackRating > settings.MostRatedMinRatingAverage && chkFilterTopRating.Checked) && !(((XtbPlugin)i.Tag).IsFromCustomRepo))
                       && (!chkFilterNew.Checked || (((XtbPlugin)i.Tag).FirstReleaseDate > DateTime.Now.AddMonths(-1)) && chkFilterNew.Checked)
                       && ((((XtbPlugin)i.Tag).Compatibilty != CompatibleState.Compatible) && chkIncompatible.Checked
                       || (((XtbPlugin)i.Tag).Action == PackageInstallAction.None) && chkShowInstalled.Checked
                       || (((XtbPlugin)i.Tag).Action == PackageInstallAction.Update) && chkShowUpdates.Checked
                       || (((XtbPlugin)i.Tag).Action == PackageInstallAction.Install) && chkToInstall.Checked))
                       .OrderBy(t => ((XtbPlugin)t.Tag).CleanedName)
                       .ToList();

                if (IsHandleCreated)
                {
                    lvTools.ItemChecked -= lvTools_ItemChecked;
                    lvTools.Resize -= lvTools_Resize;

                    lvTools.Items.Clear();
                    lvTools.Items.AddRange(filteredTools.ToArray());

                    lvTools.ItemChecked += lvTools_ItemChecked;
                    lvTools.Resize += lvTools_Resize;
                }
            }));

            if (InvokeRequired)
            {
                Invoke(mi);
            }
            else
            {
                mi();
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

        private void llApplyUserFilter_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            isUpdateOnly = false;

            chkFilterMvp.Checked = settings.LibraryFilterMvp;
            chkFilterOpenSource.Checked = settings.LibraryFilterOpenSource;
            chkFilterTopRating.Checked = settings.LibraryFilterRating;
            chkFilterNew.Checked = settings.LibraryFilterNew;
            chkShowInstalled.Checked = settings.LibraryShowInstalled;
            chkShowUpdates.Checked = settings.LibraryShowUpdates;
            chkToInstall.Checked = settings.LibraryShowNotInstalled;
            chkIncompatible.Checked = settings.LibraryShowIncompatible;

            SetCheckboxEventForSettings();

            pnlFilterUpdateInfo.Visible = false;
        }

        private void llRepoMoreInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.xrmtoolbox.com/documentation/for-it-administrators/set-your-own-tools-repositories-for-tool-library/");
        }

        private void MainLoad(bool isRefresh = false)
        {
            lvTools.Visible = false;
            pnlLoading.Visible = true;
            pnlLoading.Location = new Point((Width - pnlLoading.Width) / 2, (lvTools.Height - pnlLoading.Height) / 2);
            pnlLoading.BringToFront();

            var bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.DoWork += (s, evt) =>
            {
                ((BackgroundWorker)s).ReportProgress(0, "Loading tools...");

                if (toolLibrary.XrmToolBoxPlugins.Plugins.Count == 0 || isRefresh)
                {
                    toolLibrary.LoadTools().Wait();
                }

                if (imageCache.Cache.Count == 0)
                {
                    ((BackgroundWorker)s).ReportProgress(0, "Caching tools logo for first use. Please wait...");

                    Parallel.ForEach(toolLibrary.XrmToolBoxPlugins.Plugins,
                     tool =>
                     {
                         imageCache.AddImage(tool.LogoUrl);
                     });

                    imageCache.Save();
                }
            };
            bw.ProgressChanged += (s, evt) =>
            {
                lblLoading.Text = evt.UserState.ToString();
            };
            bw.RunWorkerCompleted += (s, evt) =>
            {
                lvTools.Visible = true;

                cbbRepositories.SelectedIndexChanged -= cbbCategories_SelectedIndexChanged;
                cbbRepositories.Items.Clear();
                if (toolLibrary.Repositories.Count > 1)
                {
                    cbbRepositories.Items.Add("-- All --");
                }
                cbbRepositories.Items.AddRange(toolLibrary.Repositories.Keys.ToArray());
                cbbRepositories.SelectedIndex = 0;
                cbbRepositories.SelectedIndexChanged += cbbCategories_SelectedIndexChanged;

                cbbCategories.SelectedIndexChanged -= cbbCategories_SelectedIndexChanged;
                cbbCategories.Items.Clear();
                cbbCategories.Items.Add("-- All --");
                cbbCategories.Items.AddRange(toolLibrary.Categories.ToArray());
                cbbCategories.SelectedIndex = 0;
                cbbCategories.SelectedIndexChanged += cbbCategories_SelectedIndexChanged;

                cbbCategories.SetAutoWidth();
                cbbRepositories.SetAutoWidth();
                pnlFilterCategory.Width = cbbCategories.Width + 20;
                pnlFilterRepository.Width = cbbRepositories.Width + 20;

                items = toolLibrary.XrmToolBoxPlugins.Plugins.Select(p => new ListViewItem(p.CleanedName) { Text = p.CleanedName, SubItems = { new ListViewItem.ListViewSubItem { Text = p.Name } }, Tag = p }).ToList();

                Filter();

                lvTools_Resize(lvTools, new EventArgs());
                pnlLoading.Visible = false;
            };
            bw.RunWorkerAsync();
        }

        private void SetState(bool isWorking)
        {
            lblLoading.Location = new Point(Width / 2 - lblLoading.Width / 2, lblLoading.Parent.Height / 2 - lblLoading.Height / 2);

            toolStrip1.Enabled = !isWorking;
            pnlToolsTop.Enabled = !isWorking;

            if (isWorking && pnlToolProperties.Controls.Count > 0)
            {
                var ctrl = pnlToolProperties.Controls[0];
                pnlToolProperties.Controls.Remove(ctrl);
                ctrl.Dispose();
            }
        }

        #region Events

        private void cbbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchThread?.Abort();
            searchThread = new Thread(Filter);
            searchThread.Start();
        }

        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            searchThread?.Abort();
            searchThread = new Thread(Filter);
            searchThread.Start();
        }

        private void Ctrl_OnToolOpenRequested(object sender, EventArgs e)
        {
            PluginsOpeningRequested?.Invoke(this, new OpenPluginEventArgs { Plugin = (XtbPlugin)lvTools.SelectedItems[0].Tag });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!isUpdateOnly)
            {
                chkFilterMvp.Checked = settings.LibraryFilterMvp;
                chkFilterOpenSource.Checked = settings.LibraryFilterOpenSource;
                chkFilterTopRating.Checked = settings.LibraryFilterRating;
                chkFilterNew.Checked = settings.LibraryFilterNew;
                chkShowInstalled.Checked = settings.LibraryShowInstalled;
                chkShowUpdates.Checked = settings.LibraryShowUpdates;
                chkToInstall.Checked = settings.LibraryShowNotInstalled;
                chkIncompatible.Checked = settings.LibraryShowIncompatible;

                SetCheckboxEventForSettings();
            }

            MainLoad();
        }

        private void lvTools_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 0)
            {
                bool value = false;
                try
                {
                    value = Convert.ToBoolean(lvTools.Columns[e.Column].Tag);
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch
                {
                }

                lvTools.ItemChecked -= lvTools_ItemChecked;

                lvTools.Columns[e.Column].Tag = !value;
                foreach (ListViewItem item in lvTools.Items)
                    item.Checked = !value;

                lvTools.ItemChecked += lvTools_ItemChecked;
                lvTools_ItemChecked(lvTools, null);
                lvTools.Invalidate();
            }
        }

        private void lvTools_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            tsbBulkInstall.Enabled = lvTools.CheckedItems.Cast<ListViewItem>().Any(i => ((XtbPlugin)i.Tag).Action == PackageInstallAction.Install || ((XtbPlugin)i.Tag).Action == PackageInstallAction.Update);
            tsbBulkDelete.Enabled = lvTools.CheckedItems.Cast<ListViewItem>().Any(i => ((XtbPlugin)i.Tag).Action == PackageInstallAction.None);
        }

        private void lvTools_Resize(object sender, EventArgs e)
        {
            chContent.Width = lvTools.Width - chCheckbox.Width - 26;

            if(lblLoading.Parent != null)
            lblLoading.Location = new Point(Width / 2 - lblLoading.Width / 2, lblLoading.Parent.Height / 2 - lblLoading.Height / 2);
        }

        private void lvTools_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pnlToolProperties.Controls.Count == 1)
            {
                pnlToolProperties.Controls[0].Dispose();
                pnlToolProperties.Controls.Clear();
            }

            if (lvTools.SelectedItems.Count == 0) return;

            var tool = (XtbPlugin)lvTools.SelectedItems[0].Tag;
            if (tool.Logo == null && tool.LogoUrl != null)
            {
                tool.Logo = imageCache.Cache[tool.LogoUrl];
            }

            pnlCustomRepoWarning.Visible = tool.IsFromCustomRepo;

            var ctrl = new ToolPackageCtrl(tool, MinCompatibleVersion, toolLibrary);
            ctrl.Dock = DockStyle.Fill;
            ctrl.OnToolOperationRequested += Ctrl_OnToolOperationRequested;
            ctrl.OnToolOpenRequested += Ctrl_OnToolOpenRequested;
            pnlToolProperties.Controls.Add(ctrl);
        }

        private void SetCheckboxEventForSettings()
        {
            foreach (var cb in pnlToolsTop.Controls.OfType<CheckBox>())
            {
                cb.CheckedChanged += (sender, e) =>
                {
                    settings.LibraryFilterMvp = chkFilterMvp.Checked;
                    settings.LibraryFilterOpenSource = chkFilterOpenSource.Checked;
                    settings.LibraryFilterRating = chkFilterTopRating.Checked;
                    settings.LibraryFilterNew = chkFilterNew.Checked;
                    settings.LibraryShowInstalled = chkShowInstalled.Checked;
                    settings.LibraryShowUpdates = chkShowUpdates.Checked;
                    settings.LibraryShowNotInstalled = chkToInstall.Checked;
                    settings.LibraryShowIncompatible = chkIncompatible.Checked;
                };
            }
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            if (pnlToolProperties.Controls.Count == 1)
            {
                var ctrl = pnlToolProperties.Controls[0];
                pnlToolProperties.Controls.Remove(ctrl);
                ctrl.Dispose();
            }

            MainLoad(true);
        }

        private void tsddActions_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == tssClearPackageCache)
            {
                if (DialogResult.No == MessageBox.Show(this, @"Are you sure you want to delete XrmToolBox Tool Library cache?", @"Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    return;
                }

                if (Directory.Exists(toolsPackagesCachePath))
                {
                    foreach (var item in new DirectoryInfo(toolsPackagesCachePath).GetFileSystemInfos())
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
                }

                tssClearPackageCache.ToolTipText = $@"Clean XrmToolBox Tool Library cache folder

Current cache folder size: {0}MB";
                MessageBox.Show(this, @"Cache folder has been cleaned", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (e.ClickedItem == tssReloadImageCache)
            {
                if (DialogResult.No == MessageBox.Show(this, @"Are you sure you want to refresh Tools logo cache?", @"Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    return;
                }

                SetState(true);
                lvTools.Visible = false;
                pnlLoading.Visible = true;
                lblLoading.Text = "Caching tools logo. Please wait...";
                pnlLoading.BringToFront();

                var bw = new BackgroundWorker();
                bw.DoWork += (s, evt) =>
                {
                    imageCache.Refresh(toolLibrary.XrmToolBoxPlugins.Plugins);
                };
                bw.RunWorkerCompleted += (s, evt) =>
                {
                    Filter();
                    lvTools.Visible = true;
                    pnlLoading.Visible = false;
                    SetState(false);
                };
                bw.RunWorkerAsync();
            }
        }

        private void TsmiStartup_DropDownItemClicked(object sender, System.Windows.Forms.ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == tsmiOpenToolLibraryAlways)
            {
                settings.DisplayPluginsStoreOnStartup = true;
                settings.DisplayPluginsStoreOnlyIfUpdates = false;

                tsmiOpenToolLibraryOnlyIfUpdatesAreAvailable.Checked = false;
                tsmiOpenToolLibraryNever.Checked = false;
            }
            else if (e.ClickedItem == tsmiOpenToolLibraryOnlyIfUpdatesAreAvailable)
            {
                settings.DisplayPluginsStoreOnStartup = true;
                settings.DisplayPluginsStoreOnlyIfUpdates = true;

                tsmiOpenToolLibraryAlways.Checked = false;
                tsmiOpenToolLibraryNever.Checked = false;
            }
            else
            {
                settings.DisplayPluginsStoreOnStartup = false;
                settings.DisplayPluginsStoreOnlyIfUpdates = false;

                tsmiOpenToolLibraryAlways.Checked = false;
                tsmiOpenToolLibraryOnlyIfUpdatesAreAvailable.Checked = false;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            searchThread?.Abort();
            searchThread = new Thread(Filter);
            searchThread.Start();
        }

        #endregion Events

        private void tsbRestart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}