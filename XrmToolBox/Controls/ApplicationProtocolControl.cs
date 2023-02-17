using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using XrmToolBox.AppCode;
using XrmToolBox.Extensibility;

namespace XrmToolBox.Controls
{
    public partial class ApplicationProtocolControl : UserControl
    {
        public ApplicationProtocolControl()
        {
            InitializeComponent();

            CheckAppProtocolStatus();
        }

        private void btnAppProtocol_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(RegistryHelper.XtbProtocolPath()))
                {
                    RegistryHelper.RemoveXtbProtocol();
                }
                else
                {
                    RegistryHelper.AddXtbProtocol(Application.ExecutablePath, Paths.XrmToolBoxPath == "." ? new FileInfo(Application.ExecutablePath).DirectoryName : Paths.XrmToolBoxPath);
                }

                CheckAppProtocolStatus();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, $"An error occured when setting application protocol: {error.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckAppProtocolStatus()
        {
            var protocolPath = RegistryHelper.XtbProtocolPath();
            var isEnabled = !string.IsNullOrEmpty(protocolPath);

            lblAppProtocolStatus.Text = string.Format(lblAppProtocolStatus.Tag.ToString(), isEnabled ? "Enabled" : "Disabled");
            lblAppProtocolStatus.ForeColor = isEnabled ? Color.Green : Color.Red;

            btnAppProtocol.Text = isEnabled ? "Disable" : "Enable";

            lblAppProtocolPath.Text = protocolPath;
            lblAppProtocolPath.Visible = isEnabled;
        }

        private void llProtocolDoc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.xrmtoolbox.com/documentation/for-developers/implement-application-protocol/");
        }
    }
}