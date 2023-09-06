using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace XrmToolBox.Controls
{
    public partial class HiddenToolsControl : UserControl
    {
        public HiddenToolsControl()
        {
            InitializeComponent();

            PopulateTools();
        }

        private ListViewItem GetListItem(string name)
        {
            var item = new ListViewItem(name);
            item.Checked = true;
            return item;
        }

        private void HiddenToolsControl_Paint(object sender, PaintEventArgs e)
        {
            PopulateTools();
        }

        private void lvAssemblies_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                Options.Instance.HiddenPlugins.Add(e.Item.Text);
            }
            else
            {
                Options.Instance.HiddenPlugins.Remove(e.Item.Text);
            }
        }

        private void PopulateTools()
        {
            lvAssemblies.ItemChecked -= lvAssemblies_ItemChecked;
            var items = Options.Instance.HiddenPlugins.Select(a => GetListItem(a)).ToArray();
            lvAssemblies.Items.Clear();
            lvAssemblies.Items.AddRange(items);
            lvAssemblies.ItemChecked += lvAssemblies_ItemChecked;
        }
    }
}