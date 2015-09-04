using System;
using System.Windows.Forms;

namespace McTools.Xrm.Connection.WinForms
{
    public partial class ProxySettingsForm : Form
    {
        public ProxySettingsForm()
        {
            InitializeComponent();
        }

        public string Password { get; set; }
        public string proxyAddress { get; set; }
        public string proxyPort { get; set; }
        public bool UseCustomProxy { get; set; }
        public string UserName { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            this.chkCustomProxy.Checked = this.UseCustomProxy;
            this.txtProxyAddress.Text = this.proxyAddress ?? string.Empty;
            this.txtProxyPort.Text = this.proxyPort ?? string.Empty;
            this.txtUserLogin.Text = this.UserName ?? string.Empty;
            this.txtUserPassword.Text = this.Password ?? string.Empty;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.UseCustomProxy = this.chkCustomProxy.Checked;

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.proxyAddress = this.txtProxyAddress.Text;
            this.proxyPort = this.txtProxyPort.Text;
            this.UserName = this.txtUserLogin.Text;
            this.Password = this.txtUserPassword.Text;
            this.UseCustomProxy = this.chkCustomProxy.Checked;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void chkCustomProxy_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlCustomSettings.Enabled = this.chkCustomProxy.Checked;
        }

        private void txtProxyPort_TextChanged(object sender, EventArgs e)
        {
            int port;

            if (!int.TryParse(this.txtProxyPort.Text, out port))
            {
                MessageBox.Show(this, "Only numeric characters are allowed!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtProxyPort.Text = this.txtProxyPort.Text.Substring(0, this.txtProxyPort.Text.Length - 1);
                this.txtProxyPort.Select(this.txtProxyPort.Text.Length, 0);
            }
        }
    }
}