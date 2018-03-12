using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.PluginsStore.DTO;

namespace XrmToolBox.PluginsStore
{
    public partial class LicenseAcceptanceForm : Form
    {
        public LicenseAcceptanceForm(List<XtbPlugin> plugins)
        {
            InitializeComponent();

            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true);

            foreach (var plugin in plugins.OrderByDescending(p => p.Name))
            {
                pnlLicenses.Controls.Add(new PluginLicense(plugin) { Dock = DockStyle.Top });
            }
        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}