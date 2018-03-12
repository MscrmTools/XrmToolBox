using System;
using System.Diagnostics;
using System.Windows.Forms;
using XrmToolBox.PluginsStore.DTO;

namespace XrmToolBox.PluginsStore
{
    public partial class PluginLicense : UserControl
    {
        private readonly XtbPlugin plugin;

        public PluginLicense(XtbPlugin plugin)
        {
            InitializeComponent();

            lblPluginName.Text = plugin.Name;
            lblAuthors.Text = string.Format(lblAuthors.Tag.ToString(), plugin.Authors);

            this.plugin = plugin;
        }

        private void llLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (string.IsNullOrEmpty(plugin.LicenseUrl))
            {
                MessageBox.Show(this, @"License url is empty!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Uri.TryCreate(plugin.LicenseUrl, UriKind.Absolute, out Uri _))
            {
                MessageBox.Show(this, @"License url is not a valid URI!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Process.Start(plugin.LicenseUrl);
        }
    }
}