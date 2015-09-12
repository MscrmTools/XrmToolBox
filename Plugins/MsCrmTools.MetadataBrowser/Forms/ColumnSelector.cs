using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MsCrmTools.MetadataBrowser.Forms
{
    public partial class ColumnSelector : Form
    {
        private readonly string[] attributeToIgnore;
        private readonly string[] firstColumns;
        private readonly Type type;
        private string[] currentAttributes;

        public ColumnSelector(Type type, string[] firstColumns, string[] attributeToIgnore, string[] currentAttributes)
        {
            InitializeComponent();

            this.type = type;
            this.firstColumns = firstColumns;
            this.attributeToIgnore = attributeToIgnore;
            this.currentAttributes = currentAttributes;
        }

        public string[] UpdatedCurrentAttributes { get { return currentAttributes; } }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            currentAttributes = attributeListView.CheckedItems.Cast<ListViewItem>().Select(i => i.Text).ToArray();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnResetToDefault_Click(object sender, EventArgs e)
        {
            currentAttributes = null;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void ColumnSelector_Load(object sender, EventArgs e)
        {
            List<ListViewItem> list;

            if (currentAttributes != null)
            {
                var dico = new Dictionary<int, ListViewItem>();

                foreach (var attrName in firstColumns)
                {
                    var item = new ListViewItem(attrName) { ForeColor = Color.Gray, Checked = true };
                    var index = Array.IndexOf(firstColumns, attrName);
                    dico.Add(index, item);
                }

                foreach (var prop in type.GetProperties())
                {
                    if (firstColumns.Contains(prop.Name) || attributeToIgnore.Contains(prop.Name))
                    {
                        continue;
                    }

                    var item = new ListViewItem(prop.Name) { Checked = currentAttributes.Contains(prop.Name) };
                    dico.Add(dico.Count + (currentAttributes.Contains(prop.Name) ? 1000 : 2000), item);
                }

                list = dico.OrderBy(x => x.Key).Select(x => x.Value).ToList();
            }
            else
            {
                list = firstColumns.Select(fc => new ListViewItem(fc) { Checked = true, ForeColor = Color.Gray }).ToList();

                foreach (var prop in type.GetProperties())
                {
                    if (firstColumns.Contains(prop.Name) || attributeToIgnore.Contains(prop.Name))
                    {
                        continue;
                    }

                    var item = new ListViewItem(prop.Name) { Checked = true };
                    list.Add(item);
                }
            }

            attributeListView.Items.AddRange(list.ToArray());
        }

        private void tsbDown_Click(object sender, EventArgs e)
        {
            var currentItem = attributeListView.SelectedItems[0];
            var currentIndex = currentItem.Index;
            if (currentIndex == attributeListView.Items.Count - 1)
                return;
            if (firstColumns.Contains(attributeListView.Items[currentItem.Index].Text))
                return;

            var nextItem = attributeListView.Items[currentItem.Index + 1];
            var nextIndex = nextItem.Index;
            attributeListView.Items.Remove(nextItem);
            attributeListView.Items.Remove(currentItem);

            attributeListView.Items.Insert(currentIndex, nextItem);
            attributeListView.Items.Insert(nextIndex, currentItem);
        }

        private void tsbUp_Click(object sender, EventArgs e)
        {
            var currentItem = attributeListView.SelectedItems[0];
            var currentIndex = currentItem.Index;
            if (currentIndex == 0)
                return;
            if (firstColumns.Contains(attributeListView.Items[currentItem.Index - 1].Text))
                return;

            var previousItem = attributeListView.Items[currentItem.Index - 1];
            var previousIndex = previousItem.Index;
            attributeListView.Items.Remove(previousItem);
            attributeListView.Items.Remove(currentItem);

            attributeListView.Items.Insert(previousIndex, currentItem);
            attributeListView.Items.Insert(currentIndex, previousItem);
        }
    }
}