using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Crm.Sdk.Messages;

namespace MsCrmTools.MetadataBrowser.Forms
{
    public partial class ColumnSelector : Form
    {
        private readonly Type type;
        private readonly string[] firstColumns;
        private readonly string[] attributeToIgnore;
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

        private void ColumnSelector_Load(object sender, EventArgs e)
        {
            List<ListViewItem> list;

            if (currentAttributes != null)
            {
                var dico = new Dictionary<int, ListViewItem>();

                foreach (var prop in type.GetProperties())
                {
                    if (firstColumns.Contains(prop.Name) || attributeToIgnore.Contains(prop.Name))
                    {
                        continue;
                    }

                    var item = new ListViewItem(prop.Name);
                    var index = Array.IndexOf(currentAttributes, prop.Name);

                    if (index >= 0)
                    {
                        if (index <= firstColumns.Length - 1)
                        {
                            item.ForeColor = Color.Gray;
                        }

                        item.Checked = true;
                        dico.Add(index, item);
                    }
                    else
                    {
                        dico.Add(dico.Count + 1000, item);
                    }
                }

                list = dico.OrderBy(x => x.Key).Select(x => x.Value).ToList();
            }
            else
            {
                list = firstColumns.Select(fc => new ListViewItem(fc) {Checked = true, ForeColor = Color.Gray}).ToList();

                foreach (var prop in type.GetProperties())
                {
                    if (firstColumns.Contains(prop.Name) || attributeToIgnore.Contains(prop.Name))
                    {
                        continue;
                    }

                    var item = new ListViewItem(prop.Name){Checked = true};
                    list.Add(item);
                }
            }

            attributeListView.Items.AddRange(list.ToArray());
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            currentAttributes = attributeListView.CheckedItems.Cast<ListViewItem>().Select(i => i.Text).ToArray();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
