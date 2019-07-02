using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using XrmToolBox.PluginsStore.DTO;
using Message = System.Windows.Forms.Message;

namespace XrmToolBox.PluginsStore
{
    public partial class StoreFormFromPortal : Form, IStoreForm
    {
        private const string AiEndpoint = "https://dc.services.visualstudio.com/v2/track";
        private const string AiKey = "77a2080e-f82c-4b2f-bb77-eb407236b729";
        private readonly List<string> selectedPackagesId;
        private readonly StoreFromPortal store;
        private AppInsights ai = new AppInsights(new AiConfig(AiEndpoint, AiKey));
        private int newPlugin, updatePlugin, allPlugins;
        private Thread searchThread;
        private int sortedColumnIndex = -1;

        public StoreFormFromPortal()
        {
            InitializeComponent();

            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true);

            selectedPackagesId = new List<string>();

            store = new StoreFromPortal();
            store.PluginsUpdated += (sender, e) => { PluginsUpdated?.Invoke(sender, e); };
            var size = store.CalculateCacheFolderSize();
            tsbCleanCacheFolder.ToolTipText = $@"Clean XrmToolBox Plugins Store cache folder\r\n\r\nCurrent cache folder size: {size}MB";
        }

        public event EventHandler PluginsClosingRequested;

        public event EventHandler PluginsUpdated;

        public void AskForPluginsClosing()
        {
            PluginsClosingRequested?.Invoke(this, new EventArgs());
        }

        public void RefreshPluginsList(bool reload = true)
        {
            selectedPackagesId.Clear();

            tstSearch.TextChanged -= tstSearch_TextChanged;
            tstSearch.Text = @"Search by Title or Authors";
            tstSearch.ForeColor = SystemColors.InactiveCaption;
            tstSearch.TextChanged += tstSearch_TextChanged;
            tstSearch.Enabled = false;

            lvPlugins.Items.Clear();
            tssLabel.Text = @"Retrieving plugins from Nuget feed...";
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
                    store.LoadNugetPackages();
                }
                var xtbPackages = store.XrmToolBoxPlugins.Plugins.OrderBy(p => p.Name);

                var lvic = new List<ListViewItem>();
                foreach (var xtbPackage in xtbPackages)
                {
                    allPlugins++;

                    if (xtbPackage.Action == PackageInstallAction.Unavailable
                        && options.PluginsStoreShowIncompatible.HasValue
                        && options.PluginsStoreShowIncompatible.Value == false)
                    {
                        continue;
                    }

                    if (xtbPackage.Action == PackageInstallAction.Install)
                    {
                        newPlugin++;

                        if (options.PluginsStoreShowNew.HasValue
                            && options.PluginsStoreShowNew.Value == false)
                        {
                            continue;
                        }
                    }

                    if (xtbPackage.Action == PackageInstallAction.Update)
                    {
                        updatePlugin++;

                        if (options.PluginsStoreShowUpdates.HasValue
                            && options.PluginsStoreShowUpdates.Value == false)
                        {
                            continue;
                        }
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

                var items = (List<ListViewItem>)e.Result;

                tssPluginsCount.Text = $@"Plugins: {allPlugins} / New: {newPlugin} / Updates: {updatePlugin}";
                tssPluginsCount.Visible = true;

                lvPlugins.Items.AddRange(items.ToArray());
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

        private void BuildPropertiesPanel(XtbPlugin package)
        {
            scProperties.Panel1.Controls.Clear();

            var bitmap = new PictureBox
            {
                Size = new Size(48, 48),
                Dock = DockStyle.Left,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            try
            {
                bitmap.Load(package.LogoUrl ?? "https://raw.githubusercontent.com/wiki/MscrmTools/XrmToolBox/Images/unknown.png");
            }
            catch
            {
                byte[] bytes = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAMAAACdt4HsAAAAA3NCSVQICAjb4U/gAAAACXBIWXMAAAFzAAABcwHEdCJ9AAAAGXRFWHRTb2Z0d2FyZQB3d3cuaW5rc2NhcGUub3Jnm+48GgAAAVlQTFRF////AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAXF73CwAAAHJ0Uk5TAAEDBAUGBwsPEBEUFRYXGxwkJyorLjAxNzk+P0BBRkdKS01TVVlaW1xfYWJjZWhpbHBxcnZ3eHp8f4KGh4iJioyRkpOUnaOkpaaorrCxsrO2uLq7vsLDztLV2Nnc3d7f4+bn6Ort8fL09vf4+fr7/P3+Afu7TgAAAwJJREFUWMOtl+lbElEUxg+YImpkmZHaBm65oNJi4YZUSpFpOSgJpqYpiIL4+/8/9EHZvHNnxmc8n+ZyloeZ+77vOUdEY75wNJ5MZ/P5bDoZj4Z9civrmlgv0mTF9Ykup9mt09uVWmK5XHusbE23Okj3jOwBUDJikVAw4PEEgqFIzCgBsDfisct/kQE4SQ37m3/3D6dOADIvLNO7DYDckOlfbR3KARjd+vy+Q+BoqkXnb5k6Ag77dP6xMyjELC/MFyvA2Zipz7sM7PbYfaSeXWDZa5K/Bhgd9tfUYQBraoVlIOF1ghNvAlhW3h+YcQq1GeDGd+g7g4RzqCfgrOkuug/B8Dov4DXgsBEPBux23IZtHbtgNOAXCmb31z+/eXy8Od9vdpsFqKHak4GYGtO+UuXhSrvqjUGmyqwROFLxFzwAOD0FOAiqmDyCkWuS7MGUEtC2A+dzvS0tvXPnsNOmBEzB3hXppiGn8mcR9nuvHnv3YVFlVg6mRURkG4ZUYl9wOVA9DFxyoZJ4CLZFRLoqnKj8H4TV+mkVBlV9OKHSJSKTkFI/8gKM10/jsKDGpGBSRDZgWHX+hMf10xP4pcYMw4aIr0jJrzrfXG41nF7CVzXGX6Lok3ATJuv2oFF/P8I7kxgDwhI1ReEN0PxpwG0zGqMSh4hdgc/w24yrEYhLEkLW6Z0p4LmZJwRJSUPQSkM/fD8Gvpg6g5CWLAT0+U8BuHhr7g1AVvKULfrde6D045muj5bJ2xSYhW+d+kZcJm/zCrMwq/cGIGvzEa0LBCFtc43WBUKQtAGSdYEIxG2gPAqjem8MoloyXSvj0lKb3mtAWEdnJ3ZFZ42gOLErQdFImhO7ljRzUa2BTQ/Tqqiay3pVTXPZ1zpfVdY1jUVERO6fQ1HDhXpjMW9tVajBK3NfvbVpmquISOs/+HvPXCfrzVXX3kVEHn1KPNSisNbetQOGldQVmoTa7YjjfshyPea5HzTdj7ruh23X4777heMOVh73S5f7te8OFk/3q+8dLN+3W///A5bFM9Y/bySSAAAAAElFTkSuQmCC");

                Image errorImage;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    errorImage = Image.FromStream(ms);
                }
                bitmap.ErrorImage = errorImage;
            }

            var lblTitle = new Label
            {
                Dock = DockStyle.Top,
                Text = package.Name.Replace(" for XrmToolBox", ""),
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
                    Text = @"Description",
                    Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point),
                    Height = 16
                };

                scProperties.Panel1.Controls.AddRange(new Control[]
                {
                pnlDescription,
                lblDescriptionHeader,
                GetPropertiesPanelInformation("Project Url", package.ProjectUrl),
                GetPropertiesPanelInformation("Downloads count", package.TotalDownloadCount.ToString()),
                GetPropertiesPanelInformation("Latest release",package.LatestReleaseDate?.ToString("yyyy/MM/dd")??"N/A"),
                GetPropertiesPanelInformation("First release", package.FirstReleaseDate?.ToString("yyyy/MM/dd")??"N/A"),
                GetPropertiesPanelInformation("Authors", string.Join(", ", package.Authors)),
                GetPropertiesPanelInformation("Version", package.Version),
                pnlTitle
                });
            }
            else
            {
                pnlTitle.Controls.AddRange(new Control[] { lblDescription, lblTitle, bitmap });

                scProperties.Panel1.Controls.AddRange(new Control[]
                {
                GetPropertiesPanelInformation("Project Url", package.ProjectUrl),
                GetPropertiesPanelInformation("Downloads count", package.TotalDownloadCount.ToString()),
                GetPropertiesPanelInformation("Latest release", package.LatestReleaseDate?.ToString("yyyy/MM/dd")??"N/A"),
                GetPropertiesPanelInformation("First release", package.FirstReleaseDate?.ToString("yyyy/MM/dd")??"N/A"),
                GetPropertiesPanelInformation("Authors", package.Authors),
                GetPropertiesPanelInformation("Version", package.Version),
                pnlTitle
                });
            }
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
            var lvic = new List<ListViewItem>();
            foreach (var xtbPackage in store.XrmToolBoxPlugins.Plugins
                .Where(p => filter.Length > 0 &&
                            (p.Name.ToLower().Replace(" for xrmtoolbox", "").Contains(filter) ||
                             p.Authors.ToLower().IndexOf(filter, StringComparison.Ordinal) >= 0)
                            || p.Description.ToLower().Contains(filter)
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
                lvic.Last().Checked = selectedPackagesId.Contains(xtbPackage.Id);
            }

            Invoke(new Action(() =>
            {
                lvPlugins.Items.Clear();
                lvPlugins.Items.AddRange(lvic.ToArray());
            }));
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
            if (value is string stringValue)
            {
                rightControl = new Label
                {
                    Dock = DockStyle.Fill,
                    Text = stringValue,
                };
            }

            if (Uri.TryCreate(value?.ToString(), UriKind.Absolute, out var parsedUri))
            {
                rightControl = new LinkLabel
                {
                    Dock = DockStyle.Fill,
                    Text = parsedUri.AbsoluteUri,
                };
                rightControl.Click += (sender, e) =>
                {
                    Process.Start(((LinkLabel)sender).Text);
                };
            }
            else if (rightControl == null)
            {
                rightControl = new Label
                {
                    Dock = DockStyle.Fill,
                    Text = @"N/A"
                };
            }

            var pnl = new Panel
            {
                Height = 20,
                Dock = DockStyle.Top
            };

            pnl.Controls.AddRange(new[] { rightControl, lblLabel });

            return pnl;
        }

        private void llRatePlugin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var plugin = (XtbPlugin)lvPlugins.SelectedItems[0].Tag;
            Process.Start($"https://www.xrmtoolbox.com/plugins/plugininfo/rating/?pvid={plugin.LatestReleaseId}&id={plugin.Id}");
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

                var dValue = decimal.Parse(e.SubItem.Text);

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
            }
            else
            {
                selectedPackagesId.Remove(((XtbPlugin)e.Item.Tag).Id);
            }

            ManageUninstallButtonDisplay();
        }

        private void lvPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            ManageUninstallButtonDisplay();
            pnlReleaseNotesDetails.Controls.Clear();

            if (lvPlugins.SelectedItems.Count == 0)
            {
                return;
            }

            splitContainer1.Panel2Collapsed = false;

            var item = lvPlugins.SelectedItems[0];
            var packageItem = (XtbPlugin)item.Tag;
            var releaseNotes = packageItem.LatestReleaseNote;

            DisplayRatings(packageItem.AverageFeedbackRating, packageItem.TotalFeedbackRating);

            BuildPropertiesPanel(packageItem);

            if (!string.IsNullOrEmpty(releaseNotes))
            {
                if (Uri.TryCreate(releaseNotes, UriKind.Absolute, out _))
                {
                    var llbl = new LinkLabel { Text = releaseNotes };
                    llbl.Click += (s, evt) => { Process.Start(((LinkLabel)s).Text); };
                    llbl.AutoSize = false;
                    llbl.Dock = DockStyle.Fill;

                    pnlReleaseNotesDetails.Controls.Add(llbl);
                }
                else
                {
                    var lbl = new Label
                    {
                        Text = releaseNotes,
                        Dock = DockStyle.Fill,
                        AutoSize = false
                    };

                    pnlReleaseNotesDetails.Controls.Add(lbl);
                }
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
                        lblNotif.Text = $@"This plugin has not been developed specificaly to support latest breaking change version of XrmToolBox. Contact plugin author to make him support at least version {Store.MinCompatibleVersion}";
                        pbNotifIcon.Image = iiNotif.Images[2];
                        pnlNotif.Visible = true;
                    }
                    break;

                case CompatibleState.RequireNewVersionOfXtb:
                    {
                        lblNotif.Text = @"This plugin implements features from latest version of XrmToolBox. Please update your XrmToolBox to latest version to be able to install this plugin version";
                        pbNotifIcon.Image = iiNotif.Images[1];
                        pnlNotif.Visible = true;
                    }
                    break;

                case CompatibleState.Other:
                    {
                        lblNotif.Text = @"Something is wrong with this plugin package and we can't validate it is compatible with this version of XrmToolBox. Please contact the author to make him review his package";
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

        private void ManageUninstallButtonDisplay()
        {
            var packages =
                lvPlugins.CheckedItems.Cast<ListViewItem>()
                    .Where(l => l.Tag is XtbPlugin &&
                   (((XtbPlugin)l.Tag).Action == PackageInstallAction.None
                   || ((XtbPlugin)l.Tag).Action == PackageInstallAction.Update))
                    .Select(l => (XtbPlugin)l.Tag)
                    .ToList();
            if (packages.Count == 0)
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

            RefreshPluginsList();

            tstSearch.Focus();
        }

        private void tsbCleanCacheFolder_Click(object sender, EventArgs e)
        {
            if (DialogResult.No ==
                MessageBox.Show(this, @"Are you sure you want to delete XrmToolBox Plugins Store cache?", @"Question",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return;
            }

            var size = store.CleanCacheFolder();
            tsbCleanCacheFolder.ToolTipText = $@"Clean XrmToolBox Plugins Store cache folder\r\n\r\nCurrent cache folder size: {size}MB";
            MessageBox.Show(this, @"Cache folder has been cleaned");
        }

        private void tsbInstall_Click(object sender, EventArgs e)
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
                var licenseAcceptanceForm = new LicenseAcceptanceForm(licensedPlugins);
                if (licenseAcceptanceForm.ShowDialog(this) != DialogResult.Yes)
                {
                    return;
                }
            }

            tsMain.Enabled = false;
            lvPlugins.Enabled = false;

            foreach (var p in packages)
            {
                ai.WritePluginEvent(p.Name, p.Version, "Plugin-Install");
            }

            var bw = new BackgroundWorker { WorkerReportsProgress = true };
            bw.DoWork += (sbw, evt) =>
            {
                var updates = store.PrepareInstallationPackages((List<XtbPlugin>)evt.Argument, (BackgroundWorker)sbw);
                evt.Result = store.PerformInstallation(updates, this);
            };
            bw.ProgressChanged += (sbw, evt) =>
            {
                tssProgress.Style = ProgressBarStyle.Continuous;
                tssProgress.Visible = true;
                tssProgress.Value = evt.ProgressPercentage;
                tssLabel.Visible = true;
                tssLabel.Text = $@"Downloading {evt.UserState.ToString()}...";
                tssPluginsCount.Text = string.Empty;
            };
            bw.RunWorkerCompleted += (sbw, evt) =>
            {
                if (!Application.OpenForms.OfType<StoreFormFromPortal>().Any())
                    return;

                tsMain.Enabled = true;
                lvPlugins.Enabled = true;
                tssProgress.Visible = false;
                tssLabel.Text = string.Empty;

                if ((bool)evt.Result)
                {
                    // Refresh plugins list when installation is done
                    RefreshPluginsList();
                    MessageBox.Show(this, @"Installation done!", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                var size = store.CalculateCacheFolderSize();
                tsbCleanCacheFolder.ToolTipText = $@"Clean XrmToolBox Plugins Store cache folder\r\n\r\nCurrent cache folder size: {size}MB";
            };
            bw.RunWorkerAsync(packages);
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
                    @"Some plugins will be partially removed since they share files with other plugins.\n\nDo you want to continue anyway?",
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

            RefreshPluginsList(false);
        }

        private void tstSearch_Enter(object sender, EventArgs e)
        {
            tstSearch.Text = string.Empty;
            tstSearch.ForeColor = SystemColors.WindowText;
        }

        private void tstSearch_TextChanged(object sender, EventArgs e)
        {
            searchThread?.Abort();
            searchThread = new Thread(FilterPlugins);
            searchThread.Start(tstSearch.Text);
        }
    }
}