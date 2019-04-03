using McTools.Xrm.Connection;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using XrmToolBox.AppCode;
using XrmToolBox.Extensibility;

namespace XrmToolBox.Forms
{
    public partial class OptionsDialog : Form
    {
        public OptionsDialog(Options option)
        {
            InitializeComponent();

            Option = (Options)option.Clone();

            lblChangePathDescription.Text = string.Format(lblChangePathDescription.Text, Paths.XrmToolBoxPath);
            propertyGrid1.SelectedObject = Option;
            chkOptinAI.Checked = Option.OptinForApplicationInsights;
        }

        public Options Option { get; private set; }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            Option = (Options)propertyGrid1.SelectedObject;
            Option.OptinForApplicationInsights = chkOptinAI.Checked;

            Option.Save();

            if (cbbProxyUsage.SelectedIndex == 2)
            {
                ConnectionManager.Instance.ConnectionsList.UseCustomProxy = true;
                ConnectionManager.Instance.ConnectionsList.UseInternetExplorerProxy = false;
                ConnectionManager.Instance.ConnectionsList.ProxyAddress = txtProxyAddress.Text;
                ConnectionManager.Instance.ConnectionsList.UserName = txtProxyUser.Text;
                ConnectionManager.Instance.ConnectionsList.Password = txtProxyPassword.Text;
                ConnectionManager.Instance.ConnectionsList.ByPassProxyOnLocal = chkByPassProxyOnLocal.Checked;
                ConnectionManager.Instance.ConnectionsList.UseDefaultCredentials = !rbCustomAuthYes.Checked;
            }
            else
            {
                ConnectionManager.Instance.ConnectionsList.UseInternetExplorerProxy = cbbProxyUsage.SelectedIndex == 1;
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

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, @"An error occured: " + error.Message, @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void cbbProxyUsage_SelectedIndexChanged(object sender, EventArgs e)
        {
            var useCustomProxy = cbbProxyUsage.SelectedIndex == 2;

            txtProxyAddress.Enabled = useCustomProxy;
            txtProxyPassword.Enabled = useCustomProxy && rbCustomAuthYes.Checked;
            txtProxyUser.Enabled = useCustomProxy && rbCustomAuthYes.Checked;
            chkByPassProxyOnLocal.Enabled = useCustomProxy;
            rbCustomAuthYes.Enabled = useCustomProxy;
            rbCustomAuthNo.Enabled = useCustomProxy;
        }

        private void llOpenRootFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName);
        }

        private void llOpenStorageFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Paths.XrmToolBoxPath);
        }

        private void rbCustomAuthYes_CheckedChanged(object sender, EventArgs e)
        {
            txtProxyPassword.Enabled = rbCustomAuthYes.Checked;
            txtProxyUser.Enabled = rbCustomAuthYes.Checked;
        }
    }
}