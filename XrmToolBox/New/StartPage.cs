using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.AppCode;
using XrmToolBox.New.EventArgs;

namespace XrmToolBox.New
{
    public partial class StartPage : DockContent
    {
        private readonly PluginManagerExtended pManager;

        public StartPage(PluginManagerExtended pManager)
        {
            InitializeComponent();

            // Set drawing optimizations
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            this.pManager = pManager;
        }

        public event EventHandler OpenConnectionsManagementRequested;

        public event EventHandler<OpenMruPluginEventArgs> OpenMruPluginRequested;

        public event EventHandler<OpenFavoritePluginEventArgs> OpenPluginRequested;

        public event EventHandler OpenPluginsStoreRequested;

        public void LoadMru()
        {
            if (MostRecentlyUsedItems.Instance.Items.Any())
            {
                var mruList = new List<Control>();

                foreach (var mru in MostRecentlyUsedItems.Instance.Items.OrderBy(i => i.Date))
                {
                    var plugin = pManager.Plugins.FirstOrDefault(p => p.Metadata.Name == mru.PluginName);
                    if (plugin != null)
                    {
                        var ctrl = new MostRecentlyUsedItemControl(plugin.Metadata.BigImageBase64, mru);
                        ctrl.OpenMruPluginRequested += Ctrl_OpenMruPluginRequested;
                        ctrl.RemoveMruRequested += Ctrl_RemoveRequested;
                        ctrl.ClearMruRequested += Ctrl_ClearMruRequested;
                        ctrl.Dock = DockStyle.Top;

                        mruList.Add(ctrl);
                    }
                }

                pnlRecent.Controls.Clear();
                pnlRecent.Controls.AddRange(mruList.ToArray());
            }

            if (Favorites.Instance.Items.Any())
            {
                var width = Width < 1200 ? pnlFavorites.Width / 2 : pnlFavorites.Width;
                var index = 0;

                var favList = new List<Control>();

                foreach (var fav in Favorites.Instance.Items.OrderBy(i => i.PluginName))
                {
                    index++;
                    var left = index % 2 == 0 ? width : 0;

                    var plugin = pManager.Plugins.FirstOrDefault(p => p.Metadata.Name == fav.PluginName);
                    if (plugin != null)
                    {
                        var ctrl = new FavoriteControl(plugin.Metadata.BigImageBase64, fav);
                        ctrl.OpenFavoritePluginRequested += Ctrl_OpenFavoritePluginRequested;
                        ctrl.RemoveFavoriteRequested += Ctrl_RemoveFavoriteRequested;
                        if (Width < 1200)
                        {
                            ctrl.Width = width;
                            ctrl.Top = (index / 2) * ctrl.Height;
                            ctrl.Left = left;
                        }
                        else
                        {
                            ctrl.Dock = DockStyle.Top;
                        }

                        favList.Add(ctrl);
                    }
                }

                pnlFavorites.Controls.Clear();
                pnlFavorites.Controls.AddRange(favList.ToArray());
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.Tab:

                    if (TabIndex == DockPanel.Documents.OfType<DockContent>().Count() - 1)
                    {
                        DockPanel.Documents.OfType<DockContent>().FirstOrDefault(d => d.TabIndex == 0)?.Activate();
                        return true;
                    }

                    foreach (var document in DockPanel.Documents.OfType<DockContent>())
                    {
                        if (document.TabIndex == TabIndex + 1)
                        {
                            document.Activate();
                            return true;
                        }
                    }
                    break;

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }

            return true;
        }

        private void chkDoNotShowAtStartup_CheckedChanged(object sender, System.EventArgs e)
        {
            Options.Instance.DoNotShowStartPage = chkDoNotShowAtStartup.Checked;
            Options.Instance.Save();
        }

        private void Ctrl_ClearMruRequested(object sender, System.EventArgs e)
        {
            if (MessageBox.Show(this, @"Are you sure you want to clear the Most Recently Used plugins list?", @"Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MostRecentlyUsedItems.Instance.Items.Clear();
                MostRecentlyUsedItems.Instance.Save();
                LoadMru();
            }
        }

        private void Ctrl_OpenFavoritePluginRequested(object sender, OpenFavoritePluginEventArgs e)
        {
            OpenPluginRequested?.Invoke(this, new OpenFavoritePluginEventArgs(e.Item));
        }

        private void Ctrl_OpenMruPluginRequested(object sender, OpenMruPluginEventArgs e)
        {
            OpenMruPluginRequested?.Invoke(sender, e);
        }

        private void Ctrl_RemoveFavoriteRequested(object sender, System.EventArgs e)
        {
            var favControl = (FavoriteControl)sender;
            pnlFavorites.Controls.Remove(favControl);

            Favorites.Instance.Items.Remove(favControl.Item);
            Favorites.Instance.Save();
        }

        private void Ctrl_RemoveRequested(object sender, System.EventArgs e)
        {
            var mruControl = (MostRecentlyUsedItemControl)sender;
            pnlFavorites.Controls.Remove(mruControl);

            MostRecentlyUsedItems.Instance.Items.Remove(mruControl.Item);
            MostRecentlyUsedItems.Instance.Save();
        }

        private void LabelClick(object sender, System.EventArgs e)
        {
            if (sender == lblOpenPluginsStore)
            {
                OpenPluginsStoreRequested?.Invoke(this, new System.EventArgs());
            }
            else if (sender == lblManageConnections)
            {
                OpenConnectionsManagementRequested?.Invoke(this, new System.EventArgs());
            }
            else if (sender == lblVisitXrmToolBoxPortal)
            {
                Process.Start("https://www.xrmtoolbox.com");
            }
            else if (sender == lblDocsForUsers)
            {
                Process.Start("https://www.xrmtoolbox.com/documentation/for-users/");
            }
            else if (sender == lblDocsForDev)
            {
                Process.Start("https://www.xrmtoolbox.com/documentation/for-developers/");
            }
            else if (sender == lblFollowUsTwitter)
            {
                Process.Start("https://twitter.com/XrmToolBox");
            }
            else if (sender == lblChatGitter)
            {
                Process.Start("https://gitter.im/MscrmTools/XrmToolBox");
            }
            else if (sender == lblDonatePayPal)
            {
                Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=donations&business=tanguy92@hotmail.com&lc=EN&item_name=Donation%20for%20MSCRM%20Tools%20-%20XrmToolBox&currency_code=USD&bn=PP%2dDonationsBF");
            }
        }

        private void LabelMouseEnter(object sender, System.EventArgs e)
        {
            var label = (Label)sender;
            label.Cursor = Cursors.Hand;
        }

        private void LabelMouseLeave(object sender, System.EventArgs e)
        {
            var label = (Label)sender;
            label.Cursor = Cursors.Default;
        }

        private void lblWelcomeDescription_Resize(object sender, System.EventArgs e)
        {
            Size sz = new Size(lblWelcomeDescription.Width, Int32.MaxValue);
            sz = TextRenderer.MeasureText(lblWelcomeDescription.Text, lblWelcomeDescription.Font, sz, TextFormatFlags.WordBreak);
            lblWelcomeDescription.Height = sz.Height + Padding.Bottom + Padding.Top + 20;
        }

        private void StartPage_Resize(object sender, System.EventArgs e)
        {
            if (Width < 1200)
            {
                pnlMain.Controls.Remove(pnlFavoritesParent);
                pnlMain.Controls.Remove(pnlRecentParent);
                pnlMain.Controls.Remove(pnlRightActions);
                pnlMain.Controls.Remove(pnlWelcome);

                pnlWelcome.Dock = DockStyle.Top;
                pnlRecentParent.Dock = DockStyle.Fill;
                pnlFavoritesParent.Dock = DockStyle.Top;
                pnlRightActions.Dock = DockStyle.Right;

                pnlFavorites.Height = 100 + (Favorites.Instance.Items.Count + 1) / 2 * 130;

                List<Control> controls = new List<Control>();
                controls.Add(pnlWelcome);
                controls.Add(pnlFavoritesParent);
                controls.Add(pnlRightActions);
                controls.Add(pnlRecentParent);
                controls.Reverse();

                pnlMain.Controls.AddRange(controls.ToArray());

                // pnlFavoritesParent.BringToFront();
            }
            else
            {
                pnlMain.Controls.Remove(pnlFavoritesParent);
                pnlMain.Controls.Remove(pnlRecentParent);
                pnlMain.Controls.Remove(pnlRightActions);
                pnlMain.Controls.Remove(pnlWelcome);

                pnlWelcome.Dock = DockStyle.Top;
                pnlRecentParent.Dock = DockStyle.Fill;
                pnlFavoritesParent.Dock = DockStyle.Left;
                pnlRightActions.Dock = DockStyle.Right;

                List<Control> controls = new List<Control>();
                controls.Add(pnlFavoritesParent);
                controls.Add(pnlRightActions);
                controls.Add(pnlWelcome);
                controls.Add(pnlRecentParent);
                controls.Reverse();

                pnlMain.Controls.AddRange(controls.ToArray());
            }
        }
    }
}