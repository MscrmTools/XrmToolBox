using McTools.Xrm.Connection;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Net.Http;
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
            ddscSelection.Value = Options.Instance.UseCustomProxy ? ProxySelection.UseCustomProxy :
                Options.Instance.UseInternetExplorerProxy ? ProxySelection.UserDefault : ProxySelection.NoProxy;

            txtscCustomProxyAddress.Text = Options.Instance.ProxyAddress;
            txtscUserName.Text = Options.Instance.UserName;
            txtscPassword.Text = Options.Instance.Password;
        }

        public event EventHandler OnProxySettingsChanged;

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (ddscSelection.Value.ToString() == ((Enum)ProxySelection.UseCustomProxy).ToString())
            {
                Options.Instance.UseCustomProxy = true;
                Options.Instance.UseInternetExplorerProxy = false;
                Options.Instance.ProxyAddress = txtscCustomProxyAddress.Text;
                Options.Instance.UserName = txtscUserName.Text;
                Options.Instance.Password = txtscPassword.Text;
                Options.Instance.ByPassProxyOnLocal = sscBypassLocal.Checked;
                Options.Instance.UseDefaultCredentials = !sscCustomAuth.Checked;
            }
            else
            {
                Options.Instance.UseInternetExplorerProxy = ddscSelection.Value.ToString() == ((Enum)ProxySelection.UserDefault).ToString();
                Options.Instance.UseCustomProxy = false;
                Options.Instance.ProxyAddress = null;
                Options.Instance.UserName = null;
                Options.Instance.Password = null;
                Options.Instance.ByPassProxyOnLocal = false;
                Options.Instance.UseDefaultCredentials = false;
            }

            try
            {
                WebProxyHelper.ApplyProxy();

                try
                {
                    LblConnectivityTest.Text = "Testing access to XrmToolBox portal...";
                    LblConnectivityTest.ForeColor = SystemColors.Control;

                    var testClient = new HttpClient();
                    testClient.GetAsync("https://www.xrmtoolbox.com").GetAwaiter().GetResult();

                    LblConnectivityTest.Text = "Connection test succeeded!";
                    LblConnectivityTest.ForeColor = Color.Green;

                    Options.Instance.Save();

                    OnProxySettingsChanged?.Invoke(this, new EventArgs());

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
                    LblConnectivityTest.Text = "Connection test failed for the provided proxy information! " + (error.InnerException?.Message ?? error.Message);
                    LblConnectivityTest.ForeColor = Color.Red;
                }
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