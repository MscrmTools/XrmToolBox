using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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

    public partial class StoreForm : Form, IStoreForm
    {
        private int sortedColumnIndex = -1;
        private int newPlugin, updatePlugin, allPlugins;

        private readonly List<string> selectedPackagesId;

        private readonly Store store;

        public StoreForm()
        {
            InitializeComponent();

            selectedPackagesId = new List<string>();

            store = new Store();
            store.PluginsUpdated += (sender, e) => { PluginsUpdated?.Invoke(sender, e); };
            var size = store.CalculateCacheFolderSize();
            tsbCleanCacheFolder.ToolTipText = string.Format("Clean XrmToolBox Plugins Store cache folder\r\n\r\nCurrent cache folder size: {0}MB", size);
        }

        public event EventHandler PluginsUpdated;

        private void PluginsChecker_Load(object sender, EventArgs e)
        {
            var options = Options.Instance;

            tsbShowThisScreenOnStartup.Checked = options.DisplayPluginsStoreOnStartup.HasValue && options.DisplayPluginsStoreOnStartup.Value;
            tsmiShowInstalledPlugins.Checked = options.PluginsStoreShowInstalled ?? true;
            tsmiShowNewPlugins.Checked = options.PluginsStoreShowNew ?? true;
            tsmiShowPluginsNotCompatible.Checked = options.PluginsStoreShowIncompatible ?? true;
            tsmiShowPluginsUpdate.Checked = options.PluginsStoreShowUpdates ?? true;
            tsmiUseLegacyPluginsStore.Checked = options.UseLegacy ?? true;

            RefreshPluginsList();

            tstSearch.Focus();
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

        public void RefreshPluginsList(bool reload = true)
        {
            selectedPackagesId.Clear();

            tstSearch.TextChanged -= tstSearch_TextChanged;
            tstSearch.Text = "Search by Title or Authors";
            tstSearch.ForeColor = SystemColors.InactiveCaption;
            tstSearch.TextChanged += tstSearch_TextChanged;
            tstSearch.Enabled = false;

            lvPlugins.Items.Clear();
            tssLabel.Text = "Retrieving plugins from Nuget feed...";
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
                var xtbPackages = store.Packages.OrderBy(p => p.Package.Title);

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

                tssPluginsCount.Text = string.Format("Plugins: {0} / New: {1} / Updates: {2}",
                    allPlugins,
                    newPlugin,
                    updatePlugin);
                tssPluginsCount.Visible = true;

                lvPlugins.Items.AddRange(items.ToArray());
            };
            bw.RunWorkerAsync();
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

        private void tsbInstall_Click(object sender, EventArgs e)
        {
            var packages =
                lvPlugins.CheckedItems.Cast<ListViewItem>()
                    .Where(l => l.Tag is XtbNuGetPackage)
                    .Select(l => (XtbNuGetPackage)l.Tag)
                    .ToList();
            if (packages.Count == 0)
            {
                packages =
                    lvPlugins.SelectedItems.Cast<ListViewItem>()
                        .Where(l => l.Tag is XtbNuGetPackage)
                        .Select(l => (XtbNuGetPackage)l.Tag)
                        .ToList();
            }

            if (packages.Count == 0)
            {
                return;
            }

            var updates = store.PrepareInstallationPackages(packages);
            if (store.PerformInstallation(updates))
            {
                // Refresh plugins list when installation is done
                RefreshPluginsList();
                MessageBox.Show("Installation done!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            var size = store.CalculateCacheFolderSize();
            tsbCleanCacheFolder.ToolTipText = string.Format("Clean XrmToolBox Plugins Store cache folder\r\n\r\nCurrent cache folder size: {0}MB", size);
        }

        private void tsbLoadPlugins_Click(object sender, EventArgs e)
        {
            RefreshPluginsList();
        }

        private void tsbShowThisScreenOnStartup_Click(object sender, EventArgs e)
        {
            ToolStripButton ctrl = (ToolStripButton)sender;

            Options.Instance.DisplayPluginsStoreOnStartup = ctrl.Checked;
            Options.Instance.Save();
        }

        private void lvPlugins_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                selectedPackagesId.Add(((XtbNuGetPackage)e.Item.Tag).Package.Id);
            }
            else
            {
                selectedPackagesId.Remove(((XtbNuGetPackage)e.Item.Tag).Package.Id);
            }

            ManageUninstallButtonDisplay();
        }

        private void lvPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            ManageUninstallButtonDisplay();
            pnlReleaseNotesDetails.Controls.Clear();

            if (lvPlugins.SelectedItems.Count == 0)
            {
                splitContainer1.Panel2Collapsed = true;
                return;
            }
            splitContainer1.Panel2Collapsed = false;

            var item = lvPlugins.SelectedItems[0];
            var packageItem = (XtbNuGetPackage)item.Tag;
            var releaseNotes = packageItem.Package.ReleaseNotes;

            BuildPropertiesPanel(packageItem);

            if (!string.IsNullOrEmpty(releaseNotes))
            {
                Uri releaseNotesUri;
                if (Uri.TryCreate(releaseNotes, UriKind.Absolute, out releaseNotesUri))
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

            switch (packageItem.Compatibilty)
            {
                case CompatibleState.DoesntFitMinimumVersion:
                    {
                        lblNotif.Text = $"This plugin has not been developed specificaly to support latest breaking change version of XrmToolBox. Contact plugin author to make him support at least version {Store.MinCompatibleVersion}";
                        pbNotifIcon.Image = iiNotif.Images[2];
                        pnlNotif.Visible = true;
                    }
                    break;

                case CompatibleState.RequireNewVersionOfXtb:
                    {
                        lblNotif.Text = "This plugin implements features from latest version of XrmToolBox. Please update your XrmToolBox to latest version to be able to install this plugin version";
                        pbNotifIcon.Image = iiNotif.Images[1];
                        pnlNotif.Visible = true;
                    }
                    break;

                case CompatibleState.Other:
                    {
                        lblNotif.Text = "Something is wrong with this plugin package and we can't validate it is compatible with this version of XrmToolBox. Please contact the author to make him review his package";
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
                    .Where(l => l.Tag is XtbNuGetPackage &&
                   (((XtbNuGetPackage)l.Tag).Action == PackageInstallAction.None
                   || ((XtbNuGetPackage)l.Tag).Action == PackageInstallAction.Update))
                    .Select(l => (XtbNuGetPackage)l.Tag)
                    .ToList();
            if (packages.Count == 0)
            {
                packages =
                    lvPlugins.SelectedItems.Cast<ListViewItem>()
                        .Where(l => l.Tag is XtbNuGetPackage &&
                   (((XtbNuGetPackage)l.Tag).Action == PackageInstallAction.None
                   || ((XtbNuGetPackage)l.Tag).Action == PackageInstallAction.Update))
                        .Select(l => (XtbNuGetPackage)l.Tag)
                        .ToList();
            }

            tsbUninstall.Visible = packages.Count != 0;
        }

        private void BuildPropertiesPanel(XtbNuGetPackage package)
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
                bitmap.Load(package.Package.IconUrl?.AbsoluteUri ?? "https://raw.githubusercontent.com/wiki/MscrmTools/XrmToolBox/Images/unknown.png");
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
                Text = package.Package.Title.Replace(" for XrmToolBox", ""),
                Font = new Font("Microsoft Sans Serif", 20F),
                Height = 32
            };

            var lblDescription = new Label
            {
                Dock = DockStyle.Fill,
                Text = package.Package.Description,
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
                GetPropertiesPanelInformation("Project Url", package.Package.ProjectUrl),
                GetPropertiesPanelInformation("Downloads count", package.Package.DownloadCount.ToString()),
                GetPropertiesPanelInformation("Latest release", package.LatestReleaseDate.ToString("yyyy/MM/dd")),
                GetPropertiesPanelInformation("First release", package.FirstReleaseDate.ToString("yyyy/MM/dd")),
                GetPropertiesPanelInformation("Authors", string.Join(", ", package.Package.Authors)),
                GetPropertiesPanelInformation("Version", package.Package.Version.ToString()),
                pnlTitle
                });
            }
            else
            {
                pnlTitle.Controls.AddRange(new Control[] { lblDescription, lblTitle, bitmap });

                scProperties.Panel1.Controls.AddRange(new Control[]
                {
                GetPropertiesPanelInformation("Project Url", package.Package.ProjectUrl),
                GetPropertiesPanelInformation("Downloads count", package.Package.DownloadCount.ToString()),
                GetPropertiesPanelInformation("Latest release", package.LatestReleaseDate.ToString("yyyy/MM/dd")),
                GetPropertiesPanelInformation("First release", package.FirstReleaseDate.ToString("yyyy/MM/dd")),
                GetPropertiesPanelInformation("Authors", string.Join(", ", package.Package.Authors)),
                GetPropertiesPanelInformation("Version", package.Package.Version.ToString()),
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
                    Process.Start(((LinkLabel)sender).Text);
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

            pnl.Controls.AddRange(new[] { rightControl, lblLabel });

            return pnl;
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

        private void tsmiUseLegacyPluginsStore_Click(object sender, EventArgs e)
        {
            Options.Instance.UseLegacy = ((ToolStripMenuItem)sender).Checked;
            Options.Instance.Save();
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

            var size = store.CleanCacheFolder();
            tsbCleanCacheFolder.ToolTipText = string.Format("Clean XrmToolBox Plugins Store cache folder\r\n\r\nCurrent cache folder size: {0}MB", size);
            MessageBox.Show(this, "Cache folder has been cleaned");
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
            foreach (var xtbPackage in store.Packages.Where(p => filter.Length > 0 &&
                                                              (p.Package.Title.ToLower().Replace(" for xrmtoolbox", "").Contains(filter) ||
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

        private void tsbUninstall_Click(object sender, EventArgs e)
        {
            var packages =
               lvPlugins.CheckedItems.Cast<ListViewItem>()
                   .Where(l => l.Tag is XtbNuGetPackage &&
                   (((XtbNuGetPackage)l.Tag).Action == PackageInstallAction.None
                   || ((XtbNuGetPackage)l.Tag).Action == PackageInstallAction.Update))
                   .Select(l => (XtbNuGetPackage)l.Tag)
                   .ToList();

            packages.AddRange(lvPlugins.SelectedItems.Cast<ListViewItem>()
                .Where(l => l.Tag is XtbNuGetPackage &&
                            (((XtbNuGetPackage)l.Tag).Action == PackageInstallAction.None
                             || ((XtbNuGetPackage)l.Tag).Action == PackageInstallAction.Update))
                .Select(l => (XtbNuGetPackage)l.Tag)
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
                    "Some plugins will be partially removed since they share files with other plugins.\n\nDo you want to continue anyway?",
                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
        }
    }
}