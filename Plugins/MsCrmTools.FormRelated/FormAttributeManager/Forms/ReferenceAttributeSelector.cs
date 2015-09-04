using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.FormAttributeManager.AppCode;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MsCrmTools.FormAttributeManager.Forms
{
    public partial class ReferenceAttributeSelector : Form
    {
        public ReferenceAttributeSelector(IEnumerable<AttributeMetadata> amds)
        {
            InitializeComponent();

            foreach (var amd in amds)
            {
                cbbAttributes.Items.Add(new AttributeMetadataInfo { Amd = amd });
            }

            cbbAttributes.SelectedIndex = 0;
        }

        public AttributeMetadata SelectedAttribute
        {
            get { return ((AttributeMetadataInfo)cbbAttributes.SelectedItem).Amd; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}