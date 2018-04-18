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

            // LoadMru();
        }

        public event EventHandler<OpenMruPluginEventArgs> OpenMruPluginRequested;

        public event EventHandler OpenPluginsStoreRequested;

        public event EventHandler OpenConnectionsManagementRequested;

        public void LoadMru()
        {
            pnlMru.Controls.Clear();

            var list = new List<Control>();

            foreach (var mru in MostRecentlyUsedItems.Instance.Items.OrderBy(i => i.Date))
            {
                var plugin = pManager.Plugins.FirstOrDefault(p => p.Metadata.Name == mru.PluginName);
                if (plugin != null)
                {
                    var ctrl = new MostRecentlyUsedItemControl(plugin.Metadata.BigImageBase64, mru);
                    ctrl.OpenMruPluginRequested += Ctrl_OpenMruPluginRequested;
                    ctrl.Dock = DockStyle.Top;

                    list.Add(ctrl);
                }
            }

            pnlMru.Controls.AddRange(list.ToArray());
        }

        private void Ctrl_OpenMruPluginRequested(object sender, OpenMruPluginEventArgs e)
        {
            OpenMruPluginRequested?.Invoke(sender, e);
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

        private void StartPage_Resize(object sender, System.EventArgs e)
        {
            if (Width < 1200)
            {
                pnlMain.Controls.Remove(pnlMruParent);
                pnlMain.Controls.Remove(pnlRightActions);
                pnlMain.Controls.Remove(pnlWelcome);

                pnlWelcome.Dock = DockStyle.Top;
                pnlMruParent.Dock = DockStyle.Fill;
                pnlRightActions.Dock = DockStyle.Right;

                List<Control> controls = new List<Control>();
                controls.Add(pnlWelcome);
                controls.Add(pnlRightActions);
                controls.Add(pnlMruParent);
                controls.Reverse();

                pnlMain.Controls.AddRange(controls.ToArray());

                pnlMruParent.BringToFront();
            }
            else
            {
                pnlMain.Controls.Remove(pnlMruParent);
                pnlMain.Controls.Remove(pnlRightActions);
                pnlMain.Controls.Remove(pnlWelcome);

                pnlWelcome.Dock = DockStyle.Fill;
                pnlMruParent.Dock = DockStyle.Left;
                pnlRightActions.Dock = DockStyle.Right;

                List<Control> controls = new List<Control>();
                controls.Add(pnlMruParent);
                controls.Add(pnlRightActions);
                controls.Add(pnlWelcome);
                controls.Reverse();

                pnlMain.Controls.AddRange(controls.ToArray());
            }
        }

        private void lblWelcomeDescription_Resize(object sender, System.EventArgs e)
        {
            Size sz = new Size(lblWelcomeDescription.Width, Int32.MaxValue);
            sz = TextRenderer.MeasureText(lblWelcomeDescription.Text, lblWelcomeDescription.Font, sz, TextFormatFlags.WordBreak);
            lblWelcomeDescription.Height = sz.Height + Padding.Bottom + Padding.Top + 20;
        }

        private void chkDoNotShowAtStartup_CheckedChanged(object sender, System.EventArgs e)
        {
            Options.Instance.DoNotShowStartPage = chkDoNotShowAtStartup.Checked;
            Options.Instance.Save();
        }
    }
}