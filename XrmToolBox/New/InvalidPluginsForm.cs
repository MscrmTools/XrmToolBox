using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace XrmToolBox.New
{
    public partial class InvalidPluginsForm : Form
    {
        private readonly Dictionary<string, string> validationErrors;

        public InvalidPluginsForm(Dictionary<string, string> validationErrors)
        {
            InitializeComponent();

            this.validationErrors = validationErrors;
        }

        private void InvalidPluginsForm_Load(object sender, System.EventArgs e)
        {
            lvPlugins.Items.AddRange(validationErrors.Select(ve => new ListViewItem(ve.Key) { Tag = ve.Value }).ToArray());
        }

        private void llShortcut_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(this, @"Do not forget to close XrmToolBox before deleting tools", @"Information",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            Process.Start(Paths.PluginsPath);
            Close();
        }

        private void lvPlugins_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (lvPlugins.SelectedItems.Count > 0)
            {
                txtError.Text = lvPlugins.SelectedItems[0].Tag.ToString();
            }
        }
    }
}