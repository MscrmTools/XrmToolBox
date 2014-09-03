using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xrm.Client.Metadata;
using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.FormAttributeManager.AppCode;

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
            get { return ((AttributeMetadataInfo) cbbAttributes.SelectedItem).Amd; }
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
