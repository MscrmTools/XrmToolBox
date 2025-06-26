﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using XrmToolBox.PluginsStore.DTO;
using Message = System.Windows.Forms.Message;

namespace XrmToolBox.PluginsStore
{
    public enum PackageInstallAction
    {
        None,
        Install,
        Update,
        Unavailable
    }

    public partial class StoreFormFromPortal : Form
    {
        private const string AiEndpoint = "https://dc.services.visualstudio.com/v2/track";
        private const string AiKey = "77a2080e-f82c-4b2f-bb77-eb407236b729";
        private readonly List<string> selectedPackagesId;
        private readonly StoreFromPortal store;
        private AppInsights ai = new AppInsights(new AiConfig(AiEndpoint, AiKey));
        private List<ListViewItem> lvic;
        private int newPlugin, updatePlugin, allPlugins;
        private Thread searchThread;
        private int sortedColumnIndex = -1;
        private ToolTip tooltip;

        public StoreFormFromPortal(bool allowConnectionControlPrerelease)
        {
            InitializeComponent();

            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true);

            selectedPackagesId = new List<string>();

            store = new StoreFromPortal(allowConnectionControlPrerelease);
            store.OnDownloadingTool += Store_OnDownloadingTool;
            store.LoadNuget();
            store.PluginsUpdated += (sender, e) => { PluginsUpdated?.Invoke(sender, e); };
            var size = store.CalculateCacheFolderSize();
            tsbCleanCacheFolder.ToolTipText = $@"Clean XrmToolBox Tool Library cache folder

Current cache folder size: {size}MB";

            tooltip = new ToolTip();
            tooltip.ToolTipIcon = ToolTipIcon.Info;
            tooltip.ToolTipTitle = "Why a tool does not show as open source?";
            tooltip.SetToolTip(chkIsOpenSource, "The \"Is Open source\" property is handled manually. Please contact us if a tool does not show as Open source whereas it should");

            var leftColumnSize = TextRenderer.MeasureText(lblToolLabelDownloads.Text, lblToolLabelDownloads.Font);
            tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Absolute;
            tableLayoutPanel1.ColumnStyles[0].Width = leftColumnSize.Width + 20;
        }

        public event EventHandler PluginsClosingRequested;

        public event EventHandler PluginsUpdated;

        public void AskForPluginsClosing()
        {
            PluginsClosingRequested?.Invoke(this, new EventArgs());
        }

        public void RefreshPluginsList(bool reload = true, bool isFromLoad = false)
        {
            selectedPackagesId.Clear();

            tstSearch.Enabled = false;

            lvPlugins.Items.Clear();
            tssLabel.Text = @"Retrieving tools from XrmToolBox portal...";
            tssProgress.Style = ProgressBarStyle.Marquee;
            tssProgress.Visible = true;
            tssPluginsCount.Visible = false;
            splitContainer1.Panel2Collapsed = true;

            var bw = new BackgroundWorker();
            bw.DoWork += (sender, e) =>
            {
                allPlugins = 0;
                newPlugin = 0;
                updatePlugin = 0;

                var options = Options.Instance;

                if (reload)
                {
                    store.LoadTools().Wait();
                }
                var xtbPackages = store.XrmToolBoxPlugins.Plugins.OrderBy(p => p.Name);

                lvic = new List<ListViewItem>();
                foreach (var xtbPackage in xtbPackages)
                {
                    allPlugins++;
                    lvic.Add(xtbPackage.GetPluginsStoreItem());

                    if (xtbPackage.Action == PackageInstallAction.Install)
                    {
                        newPlugin++;
                    }
                    else if (xtbPackage.Action == PackageInstallAction.Update)
                    {
                        updatePlugin++;
                    }
                }
            };
            bw.RunWorkerCompleted += (sender, e) =>
            {
                tssProgress.Visible = false;
                tssLabel.Text = string.Empty;
                tstSearch.Enabled = true;

                if (e.Error != null)
                {
                    MessageBox.Show(this, $@"An error occured when refreshing list: {e.Error.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                tssPluginsCount.Text = $@"Tools: {allPlugins} / New: {newPlugin} / Updates: {updatePlugin}";
                tssPluginsCount.Visible = true;

                LoadCategories();
                FilterPlugins(tstSearch.Text == tstSearch.Tag?.ToString() ? string.Empty : tstSearch.Text);

                if (isFromLoad)
                {
                    lvPlugins.Sorting = Options.Instance.Order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
                    sortedColumnIndex = Options.Instance.OrderIndex;
                    lvPlugins_ColumnClick(lvPlugins, new ColumnClickEventArgs(Options.Instance.OrderIndex == 0 ? 1 : Options.Instance.OrderIndex));
                }
            };
            bw.RunWorkerAsync();
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ChkIsOpenSource_Click(object sender, EventArgs e)
        {
            searchThread?.Abort();
            searchThread = new Thread(FilterPlugins);
            searchThread.Start(tstSearch.Text == tstSearch.Tag?.ToString() ? string.Empty : tstSearch.Text);
        }

        private void chkTools_Click(object sender, EventArgs e)
        {
            var options = Options.Instance;

            if (sender == chkToolsInstalled)
            {
                options.PluginsStoreShowInstalled = chkToolsInstalled.Checked;
                tsmiShowInstalledPlugins.Checked = chkToolsInstalled.Checked;
            }
            else if (sender == chkToolsNotInstalled)
            {
                options.PluginsStoreShowNew = chkToolsNotInstalled.Checked;
                tsmiShowNewPlugins.Checked = chkToolsNotInstalled.Checked;
            }
            else if (sender == chkToolsNotCompatible)
            {
                options.PluginsStoreShowIncompatible = chkToolsNotCompatible.Checked;
                tsmiShowPluginsNotCompatible.Checked = chkToolsNotCompatible.Checked;
            }
            else if (sender == chkToolsWithUpdate)
            {
                options.PluginsStoreShowUpdates = chkToolsWithUpdate.Checked;
                tsmiShowPluginsUpdate.Checked = chkToolsWithUpdate.Checked;
            }

            options.Save();

            FilterPlugins(tstSearch.Text == tstSearch.Tag?.ToString() ? string.Empty : tstSearch.Text);
        }

        private void DisplayRatings(decimal rating, int numberOfRatings)
        {
            pbStar.Visible = false;
            if (rating > 0)
            {
                var sourceBmp = new Bitmap(ilImages24.Images[0]);
                Rectangle srcRect = new Rectangle(0, 0, Convert.ToInt32(Math.Truncate(rating / 5 * 120)), 24);
                Bitmap cropped = sourceBmp.Clone(srcRect, sourceBmp.PixelFormat);

                pbStar.Image = cropped;
                pbStar.Visible = true;
            }
            lblNoRating.Visible = numberOfRatings == 0;
        }

        private void FilterPlugins(object text)
        {
            var filter = text.ToString().ToLower();
            var options = Options.Instance;

            var categories = pnlCategories.Controls.OfType<CheckBox>()
                .Where(c => c.Checked)
                .Select(c => c.Text)
                .ToList();

            var currentList = new List<ListViewItem>();

            foreach (var item in lvic)
            {
                var tool = (XtbPlugin)item.Tag;

                var isValidForSelectedCategories = categories.Count == 0 || categories.All(c => tool.Categories.Contains(c));
                var fitsSearchTerm = filter.Length == 0 || tool.SearchedProperties.Any(sp => sp.ToLower().IndexOf(filter, StringComparison.InvariantCultureIgnoreCase) >= 0);
                var isValidForDisplayFilters = tool.Action == PackageInstallAction.Unavailable
                                               && (options.PluginsStoreShowIncompatible ?? false)
                                               ||
                                               tool.Action == PackageInstallAction.Install
                                               && (options.PluginsStoreShowNew ?? true)
                                               ||
                                               tool.Action == PackageInstallAction.Update
                                               && (options.PluginsStoreShowUpdates ?? true)
                                               ||
                                               tool.Action == PackageInstallAction.None
                                               && (options.PluginsStoreShowInstalled ?? true);
                var isOpenSource = (tool.IsOpenSource ?? false) && chkIsOpenSource.Checked || !chkIsOpenSource.Checked;

                if (isValidForSelectedCategories && fitsSearchTerm && isValidForDisplayFilters && isOpenSource)
                    currentList.Add(item);
            }

            if (IsHandleCreated)
            {
                Invoke(new Action(
                    () =>
                    {
                        lvPlugins.Items.Clear();
                        lvPlugins.Items.AddRange(currentList.ToArray());
                    }));
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Uri.IsWellFormedUriString(llToolProjectUrl.Text, UriKind.Absolute)) 
            {
                Process.Start(llToolProjectUrl.Text);
            }
            
        }

        private void llRatePlugin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var plugin = (XtbPlugin)lvPlugins.SelectedItems[0].Tag;
            Process.Start($"https://www.xrmtoolbox.com/plugins/plugininfo/rating/?pvid={plugin.LatestReleaseId}&id={plugin.Id}");
        }

        private void LoadCategories()
        {
            pnlCategories.Controls.Clear();

            foreach (var category in store.Categories)
            {
                var cb = new CheckBox
                {
                    Name = $"cb{category}",
                    Dock = DockStyle.Top,
                    Text = category
                };

                cb.Click += tstSearch_TextChanged;

                pnlCategories.Controls.Add(cb);
            }
        }

        private void lvPlugins_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 0)
            {
                bool value = false;
                try
                {
                    value = Convert.ToBoolean(lvPlugins.Columns[e.Column].Tag);
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch
                {
                }

                lvPlugins.Columns[e.Column].Tag = !value;
                foreach (ListViewItem item in lvPlugins.Items)
                    item.Checked = !value;

                lvPlugins.Invalidate();
            }
            else
            {
                if (sortedColumnIndex == -1 || e.Column != sortedColumnIndex)
                {
                    lvPlugins.Sorting = SortOrder.Ascending;
                }
                else
                {
                    lvPlugins.Sorting = lvPlugins.Sorting == SortOrder.Ascending
                        ? SortOrder.Descending
                        : SortOrder.Ascending;
                }

                lvPlugins.ListViewItemSorter = new ListViewItemComparer(e.Column, lvPlugins.Sorting);
                sortedColumnIndex = e.Column;
            }

            Options.Instance.OrderIndex = e.Column;
            Options.Instance.Order = lvPlugins.Sorting;
            Options.Instance.Save();
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
                // ReSharper disable once EmptyGeneralCatchClause
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

        private void lvPlugins_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            var plugin = (XtbPlugin)e.Item.Tag;

            if (e.ColumnIndex == 2 && e.SubItem.Text.Length > 0)
            {
                if (e.Item.Selected)
                {
                    e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
                }

                var dValue = plugin.AverageFeedbackRating;

                if (dValue > 0)
                {
                    var sourceBmp = new Bitmap(ilImages.Images[0]);
                    Rectangle srcRect = new Rectangle(0, 0, Convert.ToInt32(Math.Truncate(dValue / 5 * 80)), 16);
                    Bitmap cropped = sourceBmp.Clone(srcRect, sourceBmp.PixelFormat);

                    e.Graphics.DrawImage(cropped, new Point(e.Bounds.X, e.Bounds.Y));

                    e.Graphics.DrawString($"({plugin.TotalFeedbackRating})", e.Item.Font, new SolidBrush(e.Item.ForeColor), new PointF(e.Bounds.X + 80, e.Bounds.Y + 2));
                }
            }
            else if (e.ColumnIndex == 0)
            {
                e.DrawDefault = true;
            }
            else
            {
                // Draw the standard header background.
                e.DrawBackground();

                if (e.Item.Selected)
                {
                    e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
                }

                if (e.ColumnIndex == 1 && plugin.FirstReleaseDate > DateTime.Now.AddMonths(-1) && (plugin.Action == PackageInstallAction.Install || plugin.Action == PackageInstallAction.Unavailable))
                {
                    //add a 2 pixel buffer the match default behavior
                    Rectangle rec = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, e.Bounds.Width - 20,
                        e.Bounds.Height - 4);

                    TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.EndEllipsis |
                                            TextFormatFlags.SingleLine;

                    //If a different tabstop than the default is needed, will have to p/invoke DrawTextEx from win32.
                    TextRenderer.DrawText(e.Graphics, e.SubItem.Text, e.Item.ListView.Font, rec,
                        e.Item.Selected ? Color.White : e.Item.ForeColor, flags);

                    e.Graphics.DrawImage(iiNotif.Images[3], e.Bounds.X + e.Bounds.Width - 20, e.Bounds.Y);
                }
                else
                {
                    //add a 2 pixel buffer the match default behavior
                    Rectangle rec = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, e.Bounds.Width - 4,
                        e.Bounds.Height - 4);

                    TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.EndEllipsis |
                                            TextFormatFlags.SingleLine;

                    //If a different tabstop than the default is needed, will have to p/invoke DrawTextEx from win32.
                    TextRenderer.DrawText(e.Graphics, e.SubItem.Text, e.Item.ListView.Font, rec,
                        e.Item.Selected ? Color.White : e.Item.ForeColor, flags);
                }
            }
        }

        private void lvPlugins_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                selectedPackagesId.Add(((XtbPlugin)e.Item.Tag).Id);
                ManageUninstallButtonDisplay(true);
            }
            else
            {
                selectedPackagesId.Remove(((XtbPlugin)e.Item.Tag).Id);
            }
        }

        private void lvPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlReleaseNotesDetails.Controls.Clear();

            if (lvPlugins.SelectedItems.Count == 0)
            {
                tsbUninstall.Visible = false;
                return;
            }

            ManageUninstallButtonDisplay(false);

            splitContainer1.Panel2Collapsed = false;

            var item = lvPlugins.SelectedItems[0];
            var packageItem = (XtbPlugin)item.Tag;
            var releaseNotes = packageItem.LatestReleaseNote;

            DisplayRatings(packageItem.AverageFeedbackRating, packageItem.TotalFeedbackRating);

            try
            {
                pbToolImage.Load(packageItem.LogoUrl ?? "https://raw.githubusercontent.com/wiki/MscrmTools/XrmToolBox/Images/unknown.png");
            }
            catch
            {
                byte[] bytes = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAMAAACdt4HsAAAAA3NCSVQICAjb4U/gAAAACXBIWXMAAAFzAAABcwHEdCJ9AAAAGXRFWHRTb2Z0d2FyZQB3d3cuaW5rc2NhcGUub3Jnm+48GgAAAVlQTFRF////AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAXF73CwAAAHJ0Uk5TAAEDBAUGBwsPEBEUFRYXGxwkJyorLjAxNzk+P0BBRkdKS01TVVlaW1xfYWJjZWhpbHBxcnZ3eHp8f4KGh4iJioyRkpOUnaOkpaaorrCxsrO2uLq7vsLDztLV2Nnc3d7f4+bn6Ort8fL09vf4+fr7/P3+Afu7TgAAAwJJREFUWMOtl+lbElEUxg+YImpkmZHaBm65oNJi4YZUSpFpOSgJpqYpiIL4+/8/9EHZvHNnxmc8n+ZyloeZ+77vOUdEY75wNJ5MZ/P5bDoZj4Z9civrmlgv0mTF9Ykup9mt09uVWmK5XHusbE23Okj3jOwBUDJikVAw4PEEgqFIzCgBsDfisct/kQE4SQ37m3/3D6dOADIvLNO7DYDckOlfbR3KARjd+vy+Q+BoqkXnb5k6Ag77dP6xMyjELC/MFyvA2Zipz7sM7PbYfaSeXWDZa5K/Bhgd9tfUYQBraoVlIOF1ghNvAlhW3h+YcQq1GeDGd+g7g4RzqCfgrOkuug/B8Dov4DXgsBEPBux23IZtHbtgNOAXCmb31z+/eXy8Od9vdpsFqKHak4GYGtO+UuXhSrvqjUGmyqwROFLxFzwAOD0FOAiqmDyCkWuS7MGUEtC2A+dzvS0tvXPnsNOmBEzB3hXppiGn8mcR9nuvHnv3YVFlVg6mRURkG4ZUYl9wOVA9DFxyoZJ4CLZFRLoqnKj8H4TV+mkVBlV9OKHSJSKTkFI/8gKM10/jsKDGpGBSRDZgWHX+hMf10xP4pcYMw4aIr0jJrzrfXG41nF7CVzXGX6Lok3ATJuv2oFF/P8I7kxgDwhI1ReEN0PxpwG0zGqMSh4hdgc/w24yrEYhLEkLW6Z0p4LmZJwRJSUPQSkM/fD8Gvpg6g5CWLAT0+U8BuHhr7g1AVvKULfrde6D045muj5bJ2xSYhW+d+kZcJm/zCrMwq/cGIGvzEa0LBCFtc43WBUKQtAGSdYEIxG2gPAqjem8MoloyXSvj0lKb3mtAWEdnJ3ZFZ42gOLErQdFImhO7ljRzUa2BTQ/Tqqiay3pVTXPZ1zpfVdY1jUVERO6fQ1HDhXpjMW9tVajBK3NfvbVpmquISOs/+HvPXCfrzVXX3kVEHn1KPNSisNbetQOGldQVmoTa7YjjfshyPea5HzTdj7ruh23X4777heMOVh73S5f7te8OFk/3q+8dLN+3W///A5bFM9Y/bySSAAAAAElFTkSuQmCC");

                Image errorImage;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    errorImage = Image.FromStream(ms);
                }
                pbToolImage.ErrorImage = errorImage;
            }

            lblToolTitle.Text = packageItem.Name.Replace(" for XrmToolBox", "");
            lblToolDescription.Text = packageItem.Description;
            lblToolVersion.Text = packageItem.Version;
            lblToolAuthors.Text = packageItem.Authors;
            lblToolFirstRelease.Text = packageItem.FirstReleaseDate.Value.ToString(CultureInfo.CurrentCulture.DateTimeFormat);
            lblToolLatestRelease.Text = packageItem.LatestReleaseDate.Value.ToString(CultureInfo.CurrentCulture.DateTimeFormat);
            lblToolDownloads.Text = packageItem.TotalDownloadCount.ToString(CultureInfo.CurrentCulture.NumberFormat);
            llToolProjectUrl.Text = packageItem.ProjectUrl;

            if (!string.IsNullOrEmpty(releaseNotes))
            {
                var rtb = new RichTextBox
                {
                    Dock = DockStyle.Fill,
                    AutoSize = false,
                    ReadOnly = true,
                    BorderStyle = BorderStyle.None,
                    ShortcutsEnabled = true
                };
                rtb.LinkClicked += (richTextBox, evt) =>
                {
                    Process.Start(evt.LinkText);
                };
                rtb.Text = releaseNotes;

                pnlReleaseNotesDetails.Controls.Add(rtb);
            }
            else
            {
                var lbl = new Label
                {
                    Text = @"N/A",
                    Dock = DockStyle.Fill,
                    AutoSize = false
                };

                pnlReleaseNotesDetails.Controls.Add(lbl);
            }

            switch (packageItem.Compatibilty)
            {
                case CompatibleState.DoesntFitMinimumVersion:
                    {
                        lblNotif.Text = $@"This tool has not been developed specificaly to support latest breaking change version of XrmToolBox. Contact tool author to make him support at least version {StoreFromPortal.MinCompatibleVersion}";
                        pbNotifIcon.Image = iiNotif.Images[2];
                        pnlNotif.Visible = true;
                    }
                    break;

                case CompatibleState.RequireNewVersionOfXtb:
                    {
                        lblNotif.Text = @"This tool implements features from latest version of XrmToolBox. Please update your XrmToolBox to latest version to be able to install this tool version";
                        pbNotifIcon.Image = iiNotif.Images[1];
                        pnlNotif.Visible = true;
                    }
                    break;

                case CompatibleState.Other:
                    {
                        lblNotif.Text = @"Something is wrong with this tool package and we can't validate it is compatible with this version of XrmToolBox. Please contact the author to make him review his package";
                        pbNotifIcon.Image = iiNotif.Images[2];
                        pnlNotif.Visible = true;
                    }
                    break;

                default:
                    {
                        pnlNotif.Visible = false;
                    }
                    break;
            }
        }

        private void ManageUninstallButtonDisplay(bool isCheck)
        {
            List<XtbPlugin> packages;

            if (isCheck)
            {
                packages =
                    lvPlugins.CheckedItems.Cast<ListViewItem>()
                        .Where(l => l.Tag is XtbPlugin &&
                                    (((XtbPlugin)l.Tag).Action == PackageInstallAction.None
                                     || ((XtbPlugin)l.Tag).Action == PackageInstallAction.Update))
                        .Select(l => (XtbPlugin)l.Tag)
                        .ToList();
            }
            else
            {
                packages =
                    lvPlugins.SelectedItems.Cast<ListViewItem>()
                        .Where(l => l.Tag is XtbPlugin &&
                                    (((XtbPlugin)l.Tag).Action == PackageInstallAction.None
                                     || ((XtbPlugin)l.Tag).Action == PackageInstallAction.Update))
                        .Select(l => (XtbPlugin)l.Tag)
                        .ToList();
            }

            tsbUninstall.Visible = packages.Count != 0;
        }

        private void PluginsChecker_Load(object sender, EventArgs e)
        {
            var options = Options.Instance;

            tsbShowThisScreenOnStartup.Checked = options.DisplayPluginsStoreOnStartup.HasValue && options.DisplayPluginsStoreOnStartup.Value;
            tsmiShowInstalledPlugins.Checked = options.PluginsStoreShowInstalled ?? true;
            tsmiShowNewPlugins.Checked = options.PluginsStoreShowNew ?? true;
            tsmiShowPluginsNotCompatible.Checked = options.PluginsStoreShowIncompatible ?? true;
            tsmiShowPluginsUpdate.Checked = options.PluginsStoreShowUpdates ?? true;

            chkToolsInstalled.Checked = options.PluginsStoreShowInstalled ?? true;
            chkToolsNotInstalled.Checked = options.PluginsStoreShowNew ?? true;
            chkToolsNotCompatible.Checked = options.PluginsStoreShowIncompatible ?? true;
            chkToolsWithUpdate.Checked = options.PluginsStoreShowUpdates ?? true;

            try
            {
                RefreshPluginsList(true, true);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, $"Error while loading tools: {error.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            tstSearch.Focus();
        }

        private void Store_OnDownloadingTool(object sender, ToolInformationEventArgs e)
        {
            Invoke(new Action(() =>
            {
                tssProgress.Style = ProgressBarStyle.Continuous;
                tssProgress.Visible = true;
                tssProgress.Value = e.ProgressPercentage;
                tssLabel.Visible = true;
                tssLabel.Text = $@"Downloading {e.ToolName}...";
                tssPluginsCount.Text = string.Empty;
            }));
        }

        private void tsbCleanCacheFolder_Click(object sender, EventArgs e)
        {
            if (DialogResult.No ==
                MessageBox.Show(this, @"Are you sure you want to delete XrmToolBox Tool Library cache?", @"Question",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return;
            }

            var size = store.CleanCacheFolder();
            tsbCleanCacheFolder.ToolTipText = $@"Clean XrmToolBox Tool Library cache folder

Current cache folder size: {size}MB";
            MessageBox.Show(this, @"Cache folder has been cleaned");
        }

        private async void tsbInstall_Click(object sender, EventArgs e)
        {
            var packages =
                lvPlugins.CheckedItems.Cast<ListViewItem>()
                    .Where(l => l.Tag is XtbPlugin)
                    .Select(l => (XtbPlugin)l.Tag)
                    .ToList();
            if (packages.Count == 0)
            {
                packages =
                    lvPlugins.SelectedItems.Cast<ListViewItem>()
                        .Where(l => l.Tag is XtbPlugin)
                        .Select(l => (XtbPlugin)l.Tag)
                        .ToList();
            }

            if (packages.Count == 0)
            {
                return;
            }

            var licensedPlugins = packages.Where(p => p.RequireLicenseAcceptance ?? false).ToList();
            if (licensedPlugins.Any())
            {
                using (var licenseAcceptanceForm = new LicenseAcceptanceForm(licensedPlugins))
                {
                    if (licenseAcceptanceForm.ShowDialog(this) != DialogResult.Yes)
                    {
                        return;
                    }
                }
            }

            tsMain.Enabled = false;
            lvPlugins.Enabled = false;

            foreach (var p in packages)
            {
                ai.WritePluginEvent(p.Name, p.Version, "Plugin-Install");
            }

            try
            {
                var updates = await store.PrepareInstallationPackages(packages);
                bool result = store.PerformInstallation(updates, this);

                if (!Application.OpenForms.OfType<StoreFormFromPortal>().Any())
                    return;

                if (result)
                {
                    // Refresh plugins list when installation is done
                    RefreshPluginsList();
                    MessageBox.Show(this, @"Installation done!", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                var size = store.CalculateCacheFolderSize();
                tsbCleanCacheFolder.ToolTipText = $@"Clean XrmToolBox Tool Library cache folder

Current cache folder size: {size}MB";
            }
            catch (Exception error)
            {
                MessageBox.Show(this, $@"An error occured: {error.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                tsMain.Enabled = true;
                lvPlugins.Enabled = true;
                tssProgress.Visible = false;
                tssLabel.Text = string.Empty;
            }
        }

        private void tsbLoadPlugins_Click(object sender, EventArgs e)
        {
            RefreshPluginsList();
        }

        private void tsbProxySettings_Click(object sender, EventArgs e)
        {
            ProxySettingsHelper.registerSettings(this);
        }

        private void tsbShowThisScreenOnStartup_Click(object sender, EventArgs e)
        {
            ToolStripButton ctrl = (ToolStripButton)sender;

            Options.Instance.DisplayPluginsStoreOnStartup = ctrl.Checked;
            Options.Instance.Save();
        }

        private void tsbUninstall_Click(object sender, EventArgs e)
        {
            var packages =
               lvPlugins.CheckedItems.Cast<ListViewItem>()
                   .Where(l => l.Tag is XtbPlugin &&
                   (((XtbPlugin)l.Tag).Action == PackageInstallAction.None
                   || ((XtbPlugin)l.Tag).Action == PackageInstallAction.Update))
                   .Select(l => (XtbPlugin)l.Tag)
                   .ToList();

            packages.AddRange(lvPlugins.SelectedItems.Cast<ListViewItem>()
                .Where(l => l.Tag is XtbPlugin &&
                            (((XtbPlugin)l.Tag).Action == PackageInstallAction.None
                             || ((XtbPlugin)l.Tag).Action == PackageInstallAction.Update))
                .Select(l => (XtbPlugin)l.Tag)
                .ToList());

            if (packages.Count == 0)
            {
                return;
            }

            var pluginsToDelete = store.PrepareUninstallPlugins(packages);

            var conflicts = pluginsToDelete.Plugins.Where(p => p.Conflict).ToList();
            if (conflicts.Count > 0)
            {
                var result = MessageBox.Show(this,
                    @"Some tools will be partially removed since they share files with other tools.\n\nDo you want to continue anyway?",
                    @"Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    return;
                }

                store.PerformUninstallation(pluginsToDelete);
            }
            else
            {
                store.PerformUninstallation(pluginsToDelete);
            }

            foreach (var p in packages)
            {
                ai.WritePluginEvent(p.Name, p.Version, "Plugin-Uninstall");
            }
        }

        private void tsmiPluginDisplayOption_Click(object sender, EventArgs e)
        {
            var options = Options.Instance;
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            if (item == tsmiShowInstalledPlugins)
            {
                options.PluginsStoreShowInstalled = item.Checked;
                chkToolsInstalled.Checked = item.Checked;
            }
            else if (item == tsmiShowNewPlugins)
            {
                options.PluginsStoreShowNew = item.Checked;
                chkToolsNotInstalled.Checked = item.Checked;
            }
            else if (item == tsmiShowPluginsNotCompatible)
            {
                options.PluginsStoreShowIncompatible = item.Checked;
                chkToolsNotCompatible.Checked = item.Checked;
            }
            else if (item == tsmiShowPluginsUpdate)
            {
                options.PluginsStoreShowUpdates = item.Checked;
                chkToolsWithUpdate.Checked = item.Checked;
            }

            options.Save();

            FilterPlugins(tstSearch.Text == tstSearch.Tag?.ToString() ? string.Empty : tstSearch.Text);
        }

        private void tstSearch_Enter(object sender, EventArgs e)
        {
            var isSame = tstSearch.Text == @"Search by Title or Authors";
            tstSearch.TextChanged -= tstSearch_TextChanged;
            tstSearch.Text = isSame ? "" : tstSearch.Text;
            tstSearch.TextChanged += tstSearch_TextChanged;
            tstSearch.ForeColor = SystemColors.WindowText;
        }

        private void tstSearch_Leave(object sender, EventArgs e)
        {
            var isEmpty = tstSearch.Text.Length == 0;
            tstSearch.TextChanged -= tstSearch_TextChanged;
            tstSearch.Text = isEmpty ? @"Search by Title or Authors" : tstSearch.Text;
            tstSearch.TextChanged += tstSearch_TextChanged;

            var isSame = tstSearch.Text == @"Search by Title or Authors";
            if (isSame)
            {
                tstSearch.ForeColor = SystemColors.InactiveCaption;
            }
        }

        private void tstSearch_TextChanged(object sender, EventArgs e)
        {
            searchThread?.Abort();
            searchThread = new Thread(FilterPlugins);
            searchThread.Start(tstSearch.Text == tstSearch.Tag?.ToString() ? string.Empty : tstSearch.Text);
        }
    }
}
