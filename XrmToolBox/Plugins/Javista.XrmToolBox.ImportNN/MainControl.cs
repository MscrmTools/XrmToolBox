using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Javista.XrmToolBox.ImportNN.AppCode;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using XrmToolBox;
using XrmToolBox.Attributes;

[assembly: BackgroundColor(30,30,30)]
[assembly: PrimaryFontColor(255,255,255)]
[assembly: SecondaryFontColor("Gray")]
[assembly: SmallImageBase64("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")]
[assembly: BigImageBase64("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")]
namespace Javista.XrmToolBox.ImportNN
{
    public partial class MainControl : UserControl, IMsCrmToolsPluginUserControl
    {
        private List<EntityMetadata> emds;

        private Panel infoPanel;

        public MainControl()
        {
            InitializeComponent();
        }

        public event EventHandler OnCloseTool;

        public event EventHandler OnRequestConnection;

        public Image PluginLogo
        {
            get { return null; }
        }

        public IOrganizationService Service
        {
            get { return service; }
        }

        public void UpdateConnection(IOrganizationService newService, ConnectionDetail connectionDetail, string actionName = "", object parameter = null)
        {
            service = newService;

            switch (actionName)
            {
                case "LoadMetadata":
                {
                    LoadMetadata();
                }
                    break;
            }
        }

        public void ClosingPlugin(PluginCloseInfo info)
        {

            if (info.FormReason != CloseReason.None ||
                info.ToolBoxReason == ToolBoxCloseReason.CloseAll ||
                info.ToolBoxReason == ToolBoxCloseReason.CloseAllExceptActive)
            {
                return;
            }

            info.Cancel = MessageBox.Show(@"Are you sure you want to close this tab?", @"Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes;
        }

        private void tsbLoadMetadata_Click(object sender, EventArgs e)
        {
            if (Service == null)
            {
                OnRequestConnection(this, new RequestConnectionEventArgs
                {
                    ActionName = "LoadMetadata",
                    Control = this,
                    Parameter = null
                });
            }
            else
            {
                LoadMetadata();
            }
        }

        private void LoadMetadata()
        {
            infoPanel = InformationPanel.GetInformationPanel(this, "Loading metadata...", 340, 150);

            var worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var request = new RetrieveAllEntitiesRequest{EntityFilters = EntityFilters.All};
            var response = (RetrieveAllEntitiesResponse) service.Execute(request);
            e.Result = response.EntityMetadata.ToList();
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Controls.Remove(infoPanel);
            infoPanel.Dispose();

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

                MessageBox.Show(ParentForm, "An error occured: " + e.Error.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
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
            infoPanel = InformationPanel.GetInformationPanel(this, "Importing many to many relationships...", 340, 150);

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

            var importWorker = new BackgroundWorker();
            importWorker.DoWork += importWorker_DoWork;
            importWorker.RunWorkerCompleted += importWorker_RunWorkerCompleted;
            importWorker.RunWorkerAsync(new object[]{settings, txtFilePath.Text});
        }

        void importWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var settings = (ImportFileSettings) ((object[])e.Argument)[0];
            var filePath = ((object[])e.Argument)[1].ToString();
            var ie = new ImportEngine(filePath, service, settings);
            ie.RaiseError += ie_RaiseError;
            ie.RaiseSuccess += ie_RaiseSuccess;
            ie.Import();
        }

        void ie_RaiseSuccess(object sender, ResultEventArgs e)
        {
            listLog.Items.Add(string.Format("Line '{0}' : Success!", e.LineNumber));
        }

        void ie_RaiseError(object sender, ResultEventArgs e)
        {
            listLog.Items.Add(string.Format("Line '{0}' : Error! {1}", e.LineNumber, e.Message));
        }

        void importWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Controls.Remove(infoPanel);
            infoPanel.Dispose();
        }

        private void tsbExport_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog {Title = "Select where to save the file", Filter="Csv file|*.csv"};
            if (sfd.ShowDialog(ParentForm) != DialogResult.OK)
            {
                return;
            }

            infoPanel = InformationPanel.GetInformationPanel(this, "Exporting many to many relationship records...", 340, 150);

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

            var exportWorker = new BackgroundWorker();
            exportWorker.DoWork += exportWorker_DoWork;
            exportWorker.RunWorkerCompleted += exportWorker_RunWorkerCompleted;
            exportWorker.RunWorkerAsync(new object[] { settings, sfd.FileName });
        }

        void exportWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var settings = (ImportFileSettings)((object[])e.Argument)[0];
            var filePath = ((object[])e.Argument)[1].ToString();
            var ee = new ExportEngine(filePath, service, settings);
            ee.RaiseError += ee_RaiseError;
            ee.Export();
        }

        void ee_RaiseError(object sender, ExportResultEventArgs e)
        {
            listLog.Items.Add(e.Message);
        }

        void exportWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Controls.Remove(infoPanel);
            infoPanel.Dispose();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            const string message = "Are your sure you want to close this tab?";
            if (MessageBox.Show(message, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                OnCloseTool(this, null);
        }
    }
}
