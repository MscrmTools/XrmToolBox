using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Javista.XrmToolBox.ImportNN.AppCode;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace Javista.XrmToolBox.ImportNN
{ 
    public partial class MainControl : PluginControlBase
    {
        private List<EntityMetadata> emds;

        public MainControl()
        {
            InitializeComponent();
        }

        private void tsbLoadMetadata_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadMetadata);
        }

        private void LoadMetadata()
        {
            WorkAsync("Loading metadata...",
                e =>
                {
                    var request = new RetrieveAllEntitiesRequest {EntityFilters = EntityFilters.All};
                    var response = (RetrieveAllEntitiesResponse) Service.Execute(request);
                    e.Result = response.EntityMetadata.ToList();
                },
                e =>
                {
                    if (e.Error == null)
                    {
                        emds = (List<EntityMetadata>) e.Result;

                        cbbFirstEntity.Items.Clear();

                        foreach (var emd in emds)
                        {
                            cbbFirstEntity.Items.Add(new EntityInfo {Metadata = emd});
                        }

                        if (cbbFirstEntity.Items.Count > 0)
                        {
                            cbbFirstEntity.SelectedIndex = 0;
                        }

                        cbbFirstEntity.DrawMode = DrawMode.OwnerDrawFixed;
                        cbbFirstEntity.DrawItem += cbbEntity_DrawItem;

                        tsbExport.Enabled = true;
                        tsbImportNN.Enabled = true;
                    }
                    else
                    {
                        tsbExport.Enabled = false;
                        tsbImportNN.Enabled = false;

                        MessageBox.Show(ParentForm, "An error occured: " + e.Error.Message, "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                });
        }

        private void cbbFirstEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbFirstEntity.SelectedItem == null)
                return;

            var emd = ((EntityInfo) cbbFirstEntity.SelectedItem).Metadata;
            var relationships = emd.ManyToManyRelationships;

            cbbFirstEntityAttribute.Items.Clear();

            foreach (var amd in emd.Attributes.Where(a => a.AttributeOf == null 
                && (a.AttributeType.Value == AttributeTypeCode.Integer
                || a.AttributeType.Value == AttributeTypeCode.Memo
                || a.AttributeType.Value == AttributeTypeCode.String)))
            {
                cbbFirstEntityAttribute.Items.Add(new AttributeInfo
                {
                    Metadata = amd
                });
            }

            cbbFirstEntityAttribute.DrawMode = DrawMode.OwnerDrawFixed;
            cbbFirstEntityAttribute.DrawItem +=cbbAttribute_DrawItem; 

            if (cbbFirstEntityAttribute.Items.Count > 0)
            {
                cbbFirstEntityAttribute.SelectedIndex = 0;
            }

            cbbRelationship.Items.Clear();

            foreach (var rel in relationships)
            {
                cbbRelationship.Items.Add(new RelationshipInfo {Metadata = rel});
            }

            if (cbbRelationship.Items.Count > 0)
            {
                cbbRelationship.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show(ParentForm, "No many to many relationships found for this entity!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            cbbRelationship.DrawMode = DrawMode.OwnerDrawFixed;
            cbbRelationship.DrawItem += cbbRelationship_DrawItem; 
        }

        private void cbbAttribute_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the default background
            e.DrawBackground();

            if (e.Index == -1) return;

            // The ComboBox is bound to a DataTable,
            // so the items are DataRowView objects.
            var attr = (AttributeInfo)((ComboBox)sender).Items[e.Index];

            // Retrieve the value of each column.
            string displayName = attr.Metadata.DisplayName.UserLocalizedLabel != null
                ? attr.Metadata.DisplayName.UserLocalizedLabel.Label
                : "N/A";
            string logicalName = attr.Metadata.LogicalName;

            // Get the bounds for the first column
            Rectangle r1 = e.Bounds;
            r1.Width /= 2;

            // Draw the text on the first column
            using (SolidBrush sb = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(displayName, e.Font, sb, r1);
            }

            // Get the bounds for the second column
            Rectangle r2 = e.Bounds;
            r2.X = e.Bounds.Width/2;
            r2.Width /= 2;

            // Draw the text on the second column
            using (SolidBrush sb = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(logicalName, e.Font, sb, r2);
            }
        }

        private void cbbEntity_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the default background
            e.DrawBackground();

            if (e.Index == -1) return;

            // The ComboBox is bound to a DataTable,
            // so the items are DataRowView objects.
            var attr = (EntityInfo)((ComboBox)sender).Items[e.Index];

            // Retrieve the value of each column.
            string displayName = attr.Metadata.DisplayName.UserLocalizedLabel != null
                ? attr.Metadata.DisplayName.UserLocalizedLabel.Label
                : "N/A";
            string logicalName = attr.Metadata.LogicalName;

            // Get the bounds for the first column
            Rectangle r1 = e.Bounds;
            r1.Width /= 2;

            // Draw the text on the first column
            using (SolidBrush sb = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(displayName, e.Font, sb, r1);
            }

            // Get the bounds for the second column
            Rectangle r2 = e.Bounds;
            r2.X = e.Bounds.Width / 2;
            r2.Width /= 2;

            // Draw the text on the second column
            using (SolidBrush sb = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(logicalName, e.Font, sb, r2);
            }
        }

        private void cbbRelationship_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the default background
            e.DrawBackground();

            if (e.Index == -1) return;

            // The ComboBox is bound to a DataTable,
            // so the items are DataRowView objects.
            var rel = (RelationshipInfo)((ComboBox)sender).Items[e.Index];

            // Retrieve the value of each column.
            string name = rel.Metadata.IntersectEntityName;
            string entity1 = rel.Metadata.Entity1LogicalName;
            string entity2 = rel.Metadata.Entity2LogicalName;

            // Get the bounds for the first column
            Rectangle r1 = e.Bounds;
            r1.Width /= 3;

            // Draw the text on the first column
            using (SolidBrush sb = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(name, e.Font, sb, r1);
            }

            // Get the bounds for the second column
            Rectangle r2 = e.Bounds;
            r2.X = e.Bounds.Width / 3;
            r2.Width /= 3;

            // Draw the text on the second column
            using (SolidBrush sb = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(entity1, e.Font, sb, r2);
            }

            // Get the bounds for the third column
            Rectangle r3 = e.Bounds;
            r3.X = e.Bounds.Width / 3 * 2;
            r3.Width /= 3;

            // Draw the text on the third column
            using (SolidBrush sb = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(entity2, e.Font, sb, r3);
            }
        }

        private void cbbRelationship_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbRelationship.SelectedItem == null)
                return;

            var rel = ((RelationshipInfo)cbbRelationship.SelectedItem).Metadata;
            cbbSecondEntity.Items.Clear();
            cbbSecondEntity.Items.Add(new EntityInfo
            {
                Metadata = emds.First(ent => (ent.LogicalName == rel.Entity1LogicalName && rel.Entity1LogicalName != ((EntityInfo)cbbFirstEntity.SelectedItem).Metadata.LogicalName)
                || (ent.LogicalName == rel.Entity2LogicalName && rel.Entity2LogicalName != ((EntityInfo)cbbFirstEntity.SelectedItem).Metadata.LogicalName)
                || (ent.LogicalName == rel.Entity2LogicalName && rel.Entity2LogicalName == ((EntityInfo)cbbFirstEntity.SelectedItem).Metadata.LogicalName))
            });

            if (cbbSecondEntity.Items.Count > 0)
            {
                cbbSecondEntity.SelectedIndex = 0;
            }

            cbbSecondEntity.DrawMode = DrawMode.OwnerDrawFixed;
            cbbSecondEntity.DrawItem += cbbEntity_DrawItem; 
        }

        private void cbbSecondEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbSecondEntity.SelectedItem == null)
                return;

            var emd = ((EntityInfo)cbbSecondEntity.SelectedItem).Metadata;

            cbbSecondEntityAttribute.Items.Clear();

            foreach (var amd in emd.Attributes.Where(a => a.AttributeOf == null
               && (a.AttributeType.Value == AttributeTypeCode.Integer
               || a.AttributeType.Value == AttributeTypeCode.Memo
               || a.AttributeType.Value == AttributeTypeCode.String)))
            {
                cbbSecondEntityAttribute.Items.Add(new AttributeInfo
                {
                    Metadata = amd
                });
            }

            if (cbbSecondEntityAttribute.Items.Count > 0)
            {
                cbbSecondEntityAttribute.SelectedIndex = 0;
            }

            cbbSecondEntityAttribute.DrawMode = DrawMode.OwnerDrawFixed;
            cbbSecondEntityAttribute.DrawItem += cbbAttribute_DrawItem; 
        }

        private void rdbFirstGuid_CheckedChanged(object sender, EventArgs e)
        {
            cbbFirstEntityAttribute.Enabled = rdbFirstAttribute.Checked;
        }

        private void rdbSecondGuid_CheckedChanged(object sender, EventArgs e)
        {
            cbbSecondEntityAttribute.Enabled = rdbSecondAttribute.Checked;

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Title = "Specify the file to process"
            };

            if (ofd.ShowDialog(ParentForm) == DialogResult.OK)
            {
                txtFilePath.Text = ofd.FileName;
            }
        }

        private void tsbImportNN_Click(object sender, EventArgs e)
        {
            listLog.Items.Clear();

            var settings = new ImportFileSettings
            {
                FirstEntity = ((EntityInfo)cbbFirstEntity.SelectedItem).Metadata.LogicalName,
                FirstAttributeIsGuid = rdbFirstGuid.Checked,
                FirstAttributeName = ((AttributeInfo)cbbFirstEntityAttribute.SelectedItem).Metadata.LogicalName,
                Relationship = ((RelationshipInfo)cbbRelationship.SelectedItem).Metadata.SchemaName,
                SecondEntity = ((EntityInfo)cbbSecondEntity.SelectedItem).Metadata.LogicalName,
                SecondAttributeIsGuid = rdbSecondGuid.Checked,
                SecondAttributeName = ((AttributeInfo)cbbSecondEntityAttribute.SelectedItem).Metadata.LogicalName,
            };

            WorkAsync("Importing many to many relationships...",
                evt =>
                {
                    var innerSettings = (ImportFileSettings)((object[])evt.Argument)[0];
                    var filePath = ((object[])evt.Argument)[1].ToString();
                    var ie = new ImportEngine(filePath, this.Service, innerSettings);
                    ie.RaiseError += ie_RaiseError;
                    ie.RaiseSuccess += ie_RaiseSuccess;
                    ie.Import();
                },
                evt =>{},
                new object[] { settings, txtFilePath.Text });
        }

        void ie_RaiseSuccess(object sender, ResultEventArgs e)
        {
            listLog.Items.Add(string.Format("Line '{0}' : Success!", e.LineNumber));
        }

        void ie_RaiseError(object sender, ResultEventArgs e)
        {
            listLog.Items.Add(string.Format("Line '{0}' : Error! {1}", e.LineNumber, e.Message));
        }

        private void tsbExport_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog {Title = "Select where to save the file", Filter="Csv file|*.csv"};
            if (sfd.ShowDialog(ParentForm) != DialogResult.OK)
            {
                return;
            }

            listLog.Items.Clear();

            var settings = new ImportFileSettings
            {
                FirstEntity = ((EntityInfo)cbbFirstEntity.SelectedItem).Metadata.LogicalName,
                FirstAttributeIsGuid = rdbFirstGuid.Checked,
                FirstAttributeName = ((AttributeInfo)cbbFirstEntityAttribute.SelectedItem).Metadata.LogicalName,
                Relationship = ((RelationshipInfo)cbbRelationship.SelectedItem).Metadata.IntersectEntityName,
                SecondEntity = ((EntityInfo)cbbSecondEntity.SelectedItem).Metadata.LogicalName,
                SecondAttributeIsGuid = rdbSecondGuid.Checked,
                SecondAttributeName = ((AttributeInfo)cbbSecondEntityAttribute.SelectedItem).Metadata.LogicalName,
            };

            WorkAsync("Exporting many to many relationship records...",
                evt =>
                {
                    var innerSettings = (ImportFileSettings)((object[])evt.Argument)[0];
                    var filePath = ((object[])evt.Argument)[1].ToString();
                    var ee = new ExportEngine(filePath, this.Service, innerSettings);
                    ee.RaiseError += ee_RaiseError;
                    ee.Export();
                },
                evt=>{},
                new object[] { settings, sfd.FileName });
        }

        void ee_RaiseError(object sender, ExportResultEventArgs e)
        {
            listLog.Items.Add(e.Message);
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }
    }
}
