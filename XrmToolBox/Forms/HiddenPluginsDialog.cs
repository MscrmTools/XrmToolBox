using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace XrmToolBox.Forms
{
    public partial class HiddenPluginsDialog : Form
    {
        public HiddenPluginsDialog(List<string> hiddenPlugins)
        {
            InitializeComponent();

            foreach (var plugin in PluginManagerExtended.Instance.Plugins)
            {
                try
                {
                    var title = plugin.Metadata.Name;
                    var author = plugin.Value.GetCompany();
                    var version = plugin.Value.GetVersion();

                    var item = new ListViewItem(title);
                    item.SubItems.Add(author);
                    item.SubItems.Add(version);
                    item.Checked = hiddenPlugins?.Contains(title) ?? false;

                    lvPlugins.Items.Add(item);
                }
                catch { }
            }
        }

        public List<string> HiddenPlugins
        {
            get
            {
                return lvPlugins.Items
                    .Cast<ListViewItem>()
                    .Where(i => i.Checked)
                    .Select(i => i.Text)
                    .ToList();
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}