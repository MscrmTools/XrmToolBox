using McTools.Xrm.Connection;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using XrmToolBox.AppCode;
using XrmToolBox.Forms;

namespace XrmToolBox.Controls
{
    public enum ProxySelection
    {
        [Description("No proxy")]
        NoProxy,

        [Description("Use custom proxy")]
        UseCustomProxy,

        [Description("Use default browser proxy")]
        UserDefault
    }

    public partial class ProxyControl : UserControl
    {
        public ProxyControl()
        {
            InitializeComponent();

            ddscSelection.EnumType = typeof(ProxySelection);
            ddscSelection.Value = ConnectionManager.Instance.ConnectionsList.UseCustomProxy ? ProxySelection.UseCustomProxy :
                ConnectionManager.Instance.ConnectionsList.UseInternetExplorerProxy ? ProxySelection.UserDefault : ProxySelection.NoProxy;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (ddscSelection.Value == (Enum)ProxySelection.UseCustomProxy)
            {
                ConnectionManager.Instance.ConnectionsList.UseCustomProxy = true;
                ConnectionManager.Instance.ConnectionsList.UseInternetExplorerProxy = false;
                ConnectionManager.Instance.ConnectionsList.ProxyAddress = txtscCustomProxyAddress.Text;
                ConnectionManager.Instance.ConnectionsList.UserName = txtscUserName.Text;
                ConnectionManager.Instance.ConnectionsList.Password = txtscPassword.Text;
                ConnectionManager.Instance.ConnectionsList.ByPassProxyOnLocal = sscBypassLocal.Checked;
                ConnectionManager.Instance.ConnectionsList.UseDefaultCredentials = !sscCustomAuth.Checked;
            }
            else
            {
                ConnectionManager.Instance.ConnectionsList.UseInternetExplorerProxy = ddscSelection.Value == (Enum)ProxySelection.UserDefault;
                ConnectionManager.Instance.ConnectionsList.UseCustomProxy = false;
                ConnectionManager.Instance.ConnectionsList.ProxyAddress = null;
                ConnectionManager.Instance.ConnectionsList.UserName = null;
                ConnectionManager.Instance.ConnectionsList.Password = null;
                ConnectionManager.Instance.ConnectionsList.ByPassProxyOnLocal = false;
                ConnectionManager.Instance.ConnectionsList.UseDefaultCredentials = false;
            }

            try
            {
                WebProxyHelper.ApplyProxy();

                ConnectionManager.Instance.SaveConnectionsFile();

                var sd = new SuccessDialog();
                sd.Location = new Point(TopLevelControl.Location.X + Parent.Parent.Parent.Controls[1].ClientRectangle.Width + 20, TopLevelControl.Location.Y + 90);
                sd.Show();

                var timer = new Timer();
                timer.Interval = 200;
                timer.Tick += (s, evt) =>
                {
                    sd.Opacity -= 0.05;
                    if (sd.Opacity == 0)
                    {
                        sd.Close();
                        sd.Dispose();
                        timer.Stop();
                    }
                };
                timer.Start();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, @"An error occured: " + error.Message, @"Error", MessageBoxButtons.OK,
                  MessageBoxIcon.Error);
            }
        }

        private void ddscSelection_OnSettingsPropertyChanged(object sender, SettingsPropertyEventArgs e)
        {
            txtscCustomProxyAddress.Enabled = ddscSelection.Value.ToString() == ProxySelection.UseCustomProxy.ToString();
            sscCustomAuth.Enabled = ddscSelection.Value.ToString() == ProxySelection.UseCustomProxy.ToString();
            sscBypassLocal.Enabled = ddscSelection.Value.ToString() == ProxySelection.UseCustomProxy.ToString();
        }

        private void sscCustomAuth_OnSettingsPropertyChanged(object sender, SettingsPropertyEventArgs e)
        {
            txtscUserName.Enabled = sscCustomAuth.Checked;
            txtscPassword.Enabled = sscCustomAuth.Checked;
        }
    }
}