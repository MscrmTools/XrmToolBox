using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk.Metadata;

namespace MsCrmTools.AuditCenter.Forms
{
    public partial class EntityPicker : Form
    {
        private readonly List<EntityMetadata> emds;
        public List<EntityMetadata> EntitiesToAdd;

        public EntityPicker(List<EntityMetadata> emds)
        {
            this.emds = emds;
            InitializeComponent();
        }

        private void EntityPickerLoad(object sender, EventArgs e)
        {
            foreach (EntityMetadata emd in emds.Where(emd => !emd.IsAuditEnabled.Value))
            {
                var item = new ListViewItem { Text = emd.DisplayName.UserLocalizedLabel.Label, Tag = emd };
                item.SubItems.Add(emd.LogicalName);
                lvEntities.Items.Add(item);
            }
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            EntitiesToAdd = new List<EntityMetadata>();

            foreach (ListViewItem item in lvEntities.SelectedItems)
            {
                EntitiesToAdd.Add((EntityMetadata)item.Tag);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void ListViewColumnClick(object sender, ColumnClickEventArgs e)
        {
            var lv = (ListView)sender;

            lv.Sorting = lv.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;

            lv.ListViewItemSorter = new ListViewItemComparer(e.Column, lv.Sorting);
        }
    }
}
