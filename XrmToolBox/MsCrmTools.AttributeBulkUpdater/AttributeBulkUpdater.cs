// PROJECT : MsCrmTools.AttributeBulkUpdater
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.AttributeBulkUpdater.Helpers;
using Tanguy.WinForm.Utilities.DelegatesHelpers;
using XrmToolBox;

namespace MsCrmTools.AttributeBulkUpdater
{
    public partial class AttributeBulkUpdater : UserControl, IMsCrmToolsPluginUserControl
    {
        #region Variables

        /// <summary>
        /// Dynamics CRM 2011 organization service
        /// </summary>
        private IOrganizationService service;

        /// <summary>
        /// Original value for searchable property
        /// </summary>
        Dictionary<string, AttributeMetadata> attributesOriginalState;

        /// <summary>
        /// Current Attributes list order column index
        /// </summary>
        int currentAttributesColumnOrder;

        /// <summary>
        /// Information panel
        /// </summary>
        private Panel infoPanel;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class AttributeBulkUpdater
        /// </summary>
        public AttributeBulkUpdater()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region Handlers

        private void TsbCloseThisTabClick(object sender, EventArgs e)
        {
            const string message = "Are your sure you want to close this tab?";
            if (MessageBox.Show(message, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                OnCloseTool(this, null);
        }

        #endregion Handlers

        #region Methods

        #region Fill Entities

        private void tsbLoadEntities_Click(object sender, EventArgs e)
        {
            if (service == null)
            {
                if (OnRequestConnection != null)
                {
                    var args = new RequestConnectionEventArgs
                                   {
                                       ActionName = "LoadEntities",
                                       Control = this
                                   };
                    OnRequestConnection(this, args);
                }
                else
                {
                    MessageBox.Show(this, "OnRequestConnection event not registered!", "Error", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            else
            {
                LoadEntities();
            }
        }

        private void LoadEntities()
        {
            lvEntities.Items.Clear();
            lvAttributes.Items.Clear();
            btnCheck.Enabled = false;
            btnCheckAttrOnForms.Enabled = false;
            btnResetAttributes.Enabled = false;
            gbEntities.Enabled = false;
            tsbPublishEntity.Enabled = false;
            tsbSaveAttributes.Enabled = false;

            infoPanel = InformationPanel.GetInformationPanel(this, "Loading entities...", 340, 100);

            var bwFillEntities = new BackgroundWorker();
            bwFillEntities.DoWork += bwFillEntities_DoWork;
            bwFillEntities.RunWorkerCompleted += bwFillEntities_RunWorkerCompleted;
            bwFillEntities.RunWorkerAsync();
        }

        void bwFillEntities_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                string errorMessage = CrmExceptionHelper.GetErrorMessage(e.Error, true);
                CommonDelegates.DisplayMessageBox(ParentForm, errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            infoPanel.Dispose();
            Controls.Remove(infoPanel);
        }

        void bwFillEntities_DoWork(object sender, DoWorkEventArgs e)
        {
            CommonDelegates.SetCursor(this, Cursors.WaitCursor);
            //ccsb.SetMessage("Loading entities ...");

            // Caching entities
            List<EntityMetadata> entities = MetadataHelper.RetrieveEntities(service);

            //ccsb.SetMessage("Displaying entities...");

            // Filling entities list
            FillEntitiesList(entities);

            
            //ccsb.SetMessage(string.Empty);
            CommonDelegates.SetCursor(this, Cursors.Default);
        }

        /// <summary>
        /// Fills the entities listview
        /// </summary>
        public void FillEntitiesList(List<EntityMetadata> entities)
        {
            try
            {
                ListViewDelegates.ClearItems(lvEntities);

                foreach (EntityMetadata emd in entities)
                {
                    var item = new ListViewItem {Text = emd.DisplayName.UserLocalizedLabel.Label, Tag = emd};
                    item.SubItems.Add(emd.LogicalName);
                    ListViewDelegates.AddItem(lvEntities, item);
                }

                GroupBoxDelegates.SetEnableState(gbEntities, true);
            }
            catch (Exception error)
            {
                string errorMessage = CrmExceptionHelper.GetErrorMessage(error, true);
                CommonDelegates.DisplayMessageBox(ParentForm, errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Fill Attributes

        private void lvEntities_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvEntities.SelectedItems.Count > 0)
            {
                lvAttributes.Items.Clear();

                var emd = (EntityMetadata)lvEntities.SelectedItems[0].Tag;

                CommonDelegates.SetCursor(this, Cursors.WaitCursor);
                //ccsb.SetMessage("Loading attributes...");

                infoPanel = InformationPanel.GetInformationPanel(this, "Loading attributes...", 340, 100);

                var bwFillAttributes = new BackgroundWorker();
                bwFillAttributes.DoWork += bwFillAttributes_DoWork;
                bwFillAttributes.RunWorkerCompleted += bwFillAttributes_RunWorkerCompleted;
                bwFillAttributes.RunWorkerAsync(emd.LogicalName);
            }
        }

        void bwFillAttributes_DoWork(object sender, DoWorkEventArgs e)
        {
            EntityMetadata emd = MetadataHelper.RetrieveEntity(e.Argument.ToString(), service);

            // Retrieve forms for this entity
            XmlDocument allFormsDoc = MetadataHelper.RetrieveEntityForms(emd.LogicalName, service);

            attributesOriginalState = new Dictionary<string, AttributeMetadata>();

            foreach (AttributeMetadata amd in emd.Attributes)
            {
                if (amd.AttributeType.Value != AttributeTypeCode.Virtual
                    && string.IsNullOrEmpty(amd.AttributeOf))
                {
                    bool searchable = amd.IsValidForAdvancedFind.Value;
                    bool isAuditEnabled = amd.IsAuditEnabled.Value;

                    string label = amd.DisplayName.UserLocalizedLabel != null ? amd.DisplayName.UserLocalizedLabel.Label : "N/A";

                    ListViewItem item = new ListViewItem(label);
                    item.SubItems.Add(amd.LogicalName);
                    item.SubItems.Add(amd.IsValidForAdvancedFind.CanBeChanged.ToString());
                    item.SubItems.Add(amd.IsCustomizable.Value.ToString());
                    item.SubItems.Add((allFormsDoc.SelectSingleNode("//control[@datafieldname='" + amd.LogicalName + "']") != null).ToString());

                    item.Tag = amd;
                    item.Checked = searchable && chkValidForAdvancedFind.Checked || isAuditEnabled && chkValidForAudit.Checked;

                    if (!amd.IsValidForAdvancedFind.CanBeChanged || !amd.IsCustomizable.Value)
                    {
                        item.ForeColor = Color.Gray;
                    }

                    attributesOriginalState.Add(amd.LogicalName, amd);

                    ListViewDelegates.AddItem(lvAttributes, item);
                }
            }
        }

        void bwFillAttributes_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CommonDelegates.SetCursor(this, Cursors.Default);
            infoPanel.Dispose();
            Controls.Remove(infoPanel);

            if (e.Cancelled)
            {
                //ccsb.SetMessage("Process canceled");
                MessageBox.Show(this, "Process canceled", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (e.Error != null)
            {
                //ccsb.SetMessage(e.Error.Message);
                MessageBox.Show(this, e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //ccsb.SetMessage(string.Empty);

                lvAttributes.Enabled = true;
                tsbSaveAttributes.Enabled = true;
                tsbPublishEntity.Enabled = true;
                btnResetAttributes.Enabled = true;
                btnCheck.Enabled = true;
                btnCheckAttrOnForms.Enabled = true;
                gbAttributes.Enabled = true;
                gbPropertySelection.Enabled = true;
            }
        }

        #endregion

        #region Save Attributes

        private void tsbSaveAttributes_Click(object sender, EventArgs e)
        {
            if (!chkValidForAdvancedFind.Checked && !chkValidForAudit.Checked)
            {
                MessageBox.Show(this, "It is required to select at least one property to update:\r\n- Valid for advanced find\r\n- Is audit enabled",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            UpdateSettings us = new UpdateSettings();
            us.Items = new List<ListViewItem>();

            foreach (ListViewItem item in lvAttributes.Items)
            {
                us.Items.Add((ListViewItem)item.Clone());
            }

            us.UpdateValidForAdvancedFind = chkValidForAdvancedFind.Checked;
            us.UpdateAuditIsEnabled = chkValidForAudit.Checked;

            Forms.UpdateAttributesForm uaForm = new Forms.UpdateAttributesForm(service, us);
            uaForm.StartPosition = FormStartPosition.CenterParent;
            uaForm.ShowDialog();

            // reloads the attributes
            lvEntities_SelectedIndexChanged(null, null);
        }

        #endregion

        #region Publish Entity

        private void tsbPublishEntity_Click(object sender, EventArgs e)
        {
            if (lvEntities.SelectedItems.Count > 0)
            {
                tsbPublishEntity.Enabled = false;
                tsbSaveAttributes.Enabled = false;
                tsbLoadEntities.Enabled = false;

                CommonDelegates.SetCursor(this, Cursors.WaitCursor);
                //ccsb.SetMessage("Publishing entity...");

                infoPanel = InformationPanel.GetInformationPanel(this, "Publishing entities...", 340, 100);

                var bwPublish = new BackgroundWorker();
                bwPublish.DoWork += bwPublish_DoWork;
                bwPublish.RunWorkerCompleted += bwPublish_RunWorkerCompleted;
                bwPublish.RunWorkerAsync(lvEntities.SelectedItems[0].Tag);
            }
        }

        void bwPublish_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //ccsb.SetMessage(string.Empty);
            CommonDelegates.SetCursor(this, Cursors.Default);

            infoPanel.Dispose();
            Controls.Remove(infoPanel);

            if (e.Error != null)
            {
                string errorMessage = CrmExceptionHelper.GetErrorMessage(e.Error, false);
                CommonDelegates.DisplayMessageBox(ParentForm, errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            tsbPublishEntity.Enabled = true;
            tsbSaveAttributes.Enabled = true;
            tsbLoadEntities.Enabled = true;
        }

        void bwPublish_DoWork(object sender, DoWorkEventArgs e)
        {
                var currentEmd = (EntityMetadata)e.Argument;

                var pubRequest = new PublishXmlRequest();
                pubRequest.ParameterXml = string.Format(@"<importexportxml>
                                                           <entities>
                                                              <entity>{0}</entity>
                                                           </entities>
                                                           <nodes/><securityroles/><settings/><workflows/>
                                                        </importexportxml>", currentEmd.LogicalName);

                service.Execute(pubRequest);
        }

        #endregion

        private void btnResetAttributes_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvAttributes.Items)
            {
                AttributeMetadata amd = attributesOriginalState[item.SubItems[1].Text];

                if (chkValidForAdvancedFind.Checked && chkValidForAudit.Checked)
                {
                    item.Checked = amd.IsValidForAdvancedFind.Value && amd.IsAuditEnabled.Value;
                }
                else if (chkValidForAdvancedFind.Checked)
                {
                    item.Checked = amd.IsValidForAdvancedFind.Value;
                }
                else
                {
                    item.Checked = amd.IsAuditEnabled.Value;
                }
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvAttributes.Items)
            {
                item.Checked = ((Button)sender).Text == "Check All";
            }

            if (((Button)sender).Text == "Check All")
            {
                ((Button)sender).Text = "Clear All";
            }
            else
            {
                ((Button)sender).Text = "Check All";
            }
        }

        private void btnCheckAttrOnForms_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvAttributes.Items)
            {
                if (item.SubItems[4].Text.ToLower() == "true")
                {
                    item.Checked = true;
                }
                else
                {
                    item.Checked = false;
                }
            }
        }

        #endregion

        #region Column Sorting Handlers

        private void lvEntities_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvEntities.SelectedItems.Clear();
            lvAttributes.Items.Clear();
            gbAttributes.Enabled = false;
            gbPropertySelection.Enabled = false;
            tsbSaveAttributes.Enabled = false;
            tsbPublishEntity.Enabled = false;
            btnResetAttributes.Enabled = false;
            btnCheck.Enabled = false;
            btnCheckAttrOnForms.Enabled = false;

            if (lvEntities.Sorting == SortOrder.Ascending)
            {
                lvEntities.Sorting = SortOrder.Descending;
            }
            else
            {
                lvEntities.Sorting = SortOrder.Ascending;
            }

            lvEntities.ListViewItemSorter = new ListViewItemComparer(e.Column, lvEntities.Sorting);
        }

        private void lvAttributes_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == currentAttributesColumnOrder)
            {
                if (lvAttributes.Sorting == SortOrder.Ascending)
                {
                    lvAttributes.Sorting = SortOrder.Descending;
                }
                else
                {
                    lvAttributes.Sorting = SortOrder.Ascending;
                }

                lvAttributes.ListViewItemSorter = new ListViewItemComparer(e.Column, lvAttributes.Sorting);
            }
            else
            {
                currentAttributesColumnOrder = e.Column;
                lvAttributes.ListViewItemSorter = new ListViewItemComparer(e.Column, SortOrder.Ascending);
            }
        }

        #endregion

        private void chkValidForAdvancedFind_CheckedChanged(object sender, EventArgs e)
        {
            CheckItems();
        }

        private void chkValidForAudit_CheckedChanged(object sender, EventArgs e)
        {
            CheckItems();
        }

        private void CheckItems()
        {
            foreach (ListViewItem item in lvAttributes.Items)
            {
                AttributeMetadata amd = (AttributeMetadata)item.Tag;

                if (chkValidForAdvancedFind.Checked && chkValidForAudit.Checked)
                {
                    item.Checked = amd.IsValidForAdvancedFind.Value && amd.IsAuditEnabled.Value;
                }
                else if (chkValidForAdvancedFind.Checked)
                {
                    item.Checked = amd.IsValidForAdvancedFind.Value;
                }
                else if (chkValidForAudit.Checked)
                {
                    item.Checked = amd.IsAuditEnabled.Value;
                }
                else
                {
                    item.Checked = false;
                }
            }
        }

    

        #region IMsCrmToolsPluginUserControl Members

        public IOrganizationService Service
        {
            get { return service; }
        }

        public Image PluginLogo
        {
            get { return imageList1.Images[0]; }
        }

        public event EventHandler OnRequestConnection;
        public event EventHandler OnCloseTool;

        public void UpdateConnection(IOrganizationService newService, string actionName = "", object parameter = null)
        {
            service = newService;

            if (actionName == "LoadEntities")
            {
                LoadEntities();
            }
        }

        #endregion IMsCrmToolsPluginUserControl
      
    }
}
