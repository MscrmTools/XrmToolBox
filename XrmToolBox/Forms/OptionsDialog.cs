using McTools.Xrm.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using XrmToolBox.AppCode;

namespace XrmToolBox.Forms
{
    public partial class OptionsDialog : Form
    {
        private readonly Options option;

        private readonly PluginManagerExtended pManager;

        public OptionsDialog(Options option, PluginManagerExtended pManager)
        {
            InitializeComponent();

            this.option = (Options)option.Clone();
            this.pManager = pManager;

            rdbToolsListLarge.Checked = option.DisplayLargeIcons;
            rdbToolsListSmall.Checked = !option.DisplayLargeIcons;
            chkDisplayMuFirst.Checked = option.DisplayMostUsedFirst;
            chkAllowUsageStatistics.Checked = option.AllowLogUsage.HasValue && option.AllowLogUsage.Value;
            chkClosePluginsSilently.Checked = option.CloseOpenedPluginsSilently;
            chkDisplayPluginsStoreOnStartup.Checked = option.DisplayPluginsStoreOnStartup;
        }

        public Options Option { get { return option; } }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            option.AllowLogUsage = chkAllowUsageStatistics.Checked;
            option.DisplayLargeIcons = rdbToolsListLarge.Checked;
            option.DisplayMostUsedFirst = chkDisplayMuFirst.Checked;
            option.CloseOpenedPluginsSilently = chkClosePluginsSilently.Checked;
            option.DisplayPluginsStoreOnStartup = chkDisplayPluginsStoreOnStartup.Checked;

            option.HiddenPlugins =
                lvPlugins.Items.Cast<ListViewItem>().Where(i => i.Checked == false).Select(i => i.Text).ToList();

            option.Save();

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
                MessageBox.Show(this, "An error occured: " + error.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnResetMuListClick(object sender, EventArgs e)
        {
            option.MostUsedList = new List<PluginUseCount>();
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

        private object GetAssemblyAttribute(Assembly assembly, Type attributeType)
        {
            return assembly.GetCustomAttributes(attributeType, true)[0];
        }

        private void OptionsDialog_Load(object sender, EventArgs e)
        {
            foreach (var plugin in pManager.Plugins)
            {
                var title = plugin.Metadata.Name;
                var author = plugin.Value.GetCompany();
                var version = plugin.Value.GetVersion();

                var item = new ListViewItem(title);
                item.SubItems.Add(author);
                item.SubItems.Add(version);
                item.Checked = option.HiddenPlugins == null || !option.HiddenPlugins.Contains(title);

                lvPlugins.Items.Add(item);
            }

            // Load proxy options
            cbbProxyUsage.SelectedIndex = ConnectionManager.Instance.ConnectionsList.UseCustomProxy ? 2 : ConnectionManager.Instance.ConnectionsList.UseInternetExplorerProxy ? 1 : 0;
            rbCustomAuthYes.Checked = !ConnectionManager.Instance.ConnectionsList.UseDefaultCredentials;
            chkByPassProxyOnLocal.Checked = ConnectionManager.Instance.ConnectionsList.ByPassProxyOnLocal;
            txtProxyAddress.Text = ConnectionManager.Instance.ConnectionsList.ProxyAddress;
            txtProxyUser.Text = ConnectionManager.Instance.ConnectionsList.UserName;
            txtProxyPassword.Text = ConnectionManager.Instance.ConnectionsList.Password;
            cbbProxyUsage_SelectedIndexChanged(null, null);
        }

        private void rbCustomAuthYes_CheckedChanged(object sender, EventArgs e)
        {
            txtProxyPassword.Enabled = rbCustomAuthYes.Checked;
            txtProxyUser.Enabled = rbCustomAuthYes.Checked;
        }
    }
}