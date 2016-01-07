using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace MsCrmTools.AuditCenter.Forms
{
    public partial class AttributePicker : Form
    {
        public List<AttributeMetadata> AttributesToAdd;
        private readonly IEnumerable<string> alreadySelectedAttributes;
        private readonly EntityMetadata emd;
        private readonly IOrganizationService service;

        public AttributePicker(EntityMetadata emd, IEnumerable<string> alreadySelectedAttributes, IOrganizationService service)
        {
            this.emd = emd;
            this.alreadySelectedAttributes = alreadySelectedAttributes;
            this.service = service;
            InitializeComponent();
        }

        private void AttributePickerLoad(object sender, EventArgs e)
        {
            XmlDocument allFormsDoc = MetadataHelper.RetrieveEntityForms(emd.LogicalName, service);

            foreach (AttributeMetadata amd in emd.Attributes.Where(a => !alreadySelectedAttributes.Contains(a.LogicalName) && a.AttributeOf == null))//.Where(a => !a.IsAuditEnabled.Value))
            {
                string displayName = amd.DisplayName != null && amd.DisplayName.UserLocalizedLabel != null
                          ? amd.DisplayName.UserLocalizedLabel.Label
                          : "N/A";

                var item = new ListViewItem { Text = displayName, Tag = amd };
                item.SubItems.Add(amd.LogicalName);
                item.SubItems.Add((allFormsDoc.SelectSingleNode("//control[@datafieldname='" + amd.LogicalName + "']") != null).ToString());
                lvAttributes.Items.Add(item);
            }
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnCheckAttrOnFormsClick(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvAttributes.Items)
            {
                item.Checked = item.SubItems[2].Text.ToLower() == "true";
            }
        }

        private void BtnCheckClick(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvAttributes.Items)
            {
                item.Checked = ((Button)sender).Text == "Check All";
            }

            ((Button)sender).Text = ((Button)sender).Text == "Check All" ? "Clear All" : "Check All";
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            AttributesToAdd = new List<AttributeMetadata>();

            foreach (ListViewItem item in lvAttributes.CheckedItems)
            {
                AttributesToAdd.Add((AttributeMetadata)item.Tag);
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