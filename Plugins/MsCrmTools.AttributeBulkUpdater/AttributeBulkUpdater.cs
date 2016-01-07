// PROJECT : MsCrmTools.AttributeBulkUpdater
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.AttributeBulkUpdater.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using XrmToolBox.Extensibility;
using CrmExceptionHelper = XrmToolBox.CrmExceptionHelper;

namespace MsCrmTools.AttributeBulkUpdater
{
    public partial class AttributeBulkUpdater : PluginControlBase
    {
        #region Variables

        /// <summary>
        /// Original value for searchable property
        /// </summary>
        private Dictionary<string, AttributeMetadata> attributesOriginalState;

        /// <summary>
        /// Current Attributes list order column index
        /// </summary>
        private int currentAttributesColumnOrder;

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

        #region Methods

        private void btnCheck_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvAttributes.Items)
            {
                item.Checked = ((Button)sender).Text == "Check All";
            }

            ((Button)sender).Text = ((Button)sender).Text == "Check All" ? "Clear All" : "Check All";
        }

        private void btnCheckAttrOnForms_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvAttributes.Items)
            {
                item.Checked = item.SubItems[4].Text.ToLower() == "true";
            }
        }

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

        private void TsbCloseThisTabClick(object sender, EventArgs e)
        {
            CloseTool();
        }

        #region Fill Entities

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

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading entities...",
                Work = (bw, e) => { e.Result = MetadataHelper.RetrieveEntities(Service); },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        string errorMessage = CrmExceptionHelper.GetErrorMessage(e.Error, true);
                        MessageBox.Show(ParentForm, errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        var items = new List<ListViewItem>();
                        foreach (EntityMetadata emd in (List<EntityMetadata>)e.Result)
                        {
                            var item = new ListViewItem { Text = emd.DisplayName.UserLocalizedLabel.Label, Tag = emd };
                            item.SubItems.Add(emd.LogicalName);
                            items.Add(item);
                        }
                        lvEntities.Items.AddRange(items.ToArray());

                        gbEntities.Enabled = true;
                    }
                }
            });
        }

        private void tsbLoadEntities_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadEntities);
        }

        #endregion Fill Entities

        #region Fill Attributes

        private void lvEntities_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvEntities.SelectedItems.Count > 0)
            {
                lvAttributes.Items.Clear();

                var emd = (EntityMetadata)lvEntities.SelectedItems[0].Tag;

                WorkAsync(new WorkAsyncInfo
                {
                    Message = "Loading attributes...",
                    AsyncArgument = emd.LogicalName,
                    Work = (bw, evt) =>
                    {
                        attributesOriginalState = new Dictionary<string, AttributeMetadata>();
                        EntityMetadata metadata = MetadataHelper.RetrieveEntity(evt.Argument.ToString(), Service);
                        XmlDocument allFormsDoc = MetadataHelper.RetrieveEntityForms(metadata.LogicalName, Service, ConnectionDetail);
                        var items = new List<ListViewItem>();

                        foreach (AttributeMetadata amd in metadata.Attributes)
                        {
                            if (amd.AttributeType.HasValue
                                && amd.AttributeType.Value != AttributeTypeCode.Virtual
                                && string.IsNullOrEmpty(amd.AttributeOf))
                            {
                                bool searchable = amd.IsValidForAdvancedFind.Value;
                                bool isAuditEnabled = amd.IsAuditEnabled.Value;

                                string label = amd.DisplayName.UserLocalizedLabel != null ? amd.DisplayName.UserLocalizedLabel.Label : "N/A";

                                var item = new ListViewItem(label);
                                item.SubItems.Add(amd.LogicalName);
                                item.SubItems.Add(amd.IsValidForAdvancedFind.CanBeChanged.ToString());
                                item.SubItems.Add((amd.IsCustomizable.Value || amd.IsManaged.HasValue && amd.IsManaged.Value == false).ToString());
                                item.SubItems.Add((allFormsDoc.SelectSingleNode("//control[@datafieldname='" + amd.LogicalName + "']") != null).ToString());
                                item.SubItems.Add(amd.RequiredLevel.Value.ToString());

                                item.Tag = amd;
                                item.Checked = searchable && chkValidForAdvancedFind.Checked || isAuditEnabled && chkValidForAudit.Checked;

                                if (!amd.IsValidForAdvancedFind.CanBeChanged || (!amd.IsCustomizable.Value && amd.IsManaged.HasValue && amd.IsManaged.Value))
                                {
                                    item.ForeColor = Color.Gray;
                                }

                                attributesOriginalState.Add(amd.LogicalName, amd);

                                items.Add(item);
                            }
                        }

                        evt.Result = items;
                    },
                    PostWorkCallBack = evt =>
                    {
                        if (evt.Error != null)
                        {
                            MessageBox.Show(this, evt.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            lvAttributes.Items.AddRange(((List<ListViewItem>)evt.Result).ToArray());

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
                });
            }
        }

        #endregion Fill Attributes

        #region Save Attributes

        private void tsbSaveAttributes_Click(object sender, EventArgs e)
        {
            if (!chkValidForAdvancedFind.Checked && !chkValidForAudit.Checked && !chkRequirementLevel.Checked)
            {
                MessageBox.Show(this, "It is required to select at least one property to update:\r\n- Valid for advanced find\r\n- Is audit enabled\r\n - Requirement level",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var us = new UpdateSettings
            {
                Items = lvAttributes.Items.Cast<ListViewItem>().Select(i => (ListViewItem)i.Clone()).ToList(),
                UpdateValidForAdvancedFind = chkValidForAdvancedFind.Checked,
                UpdateAuditIsEnabled = chkValidForAudit.Checked,
                UpdateRequirementLevel = chkRequirementLevel.Checked,
                RequirementLevelValue = chkRequirementLevel.Checked ? MapSdkValue(cboRequirementLevel.SelectedIndex) : null
            };

            var uaForm = new Forms.UpdateAttributesForm(Service, us);
            uaForm.ShowDialog();

            // reloads the attributes
            lvEntities_SelectedIndexChanged(null, null);
        }

        #endregion Save Attributes

        #region Publish Entity

        private void tsbPublishEntity_Click(object sender, EventArgs e)
        {
            if (lvEntities.SelectedItems.Count > 0)
            {
                tsbPublishEntity.Enabled = false;
                tsbSaveAttributes.Enabled = false;
                tsbLoadEntities.Enabled = false;

                WorkAsync(new WorkAsyncInfo
                {
                    Message = "Publishing entities...",
                    AsyncArgument = lvEntities.SelectedItems[0].Tag,
                    Work = (bw, evt) =>
                    {
                        var currentEmd = (EntityMetadata)evt.Argument;

                        var pubRequest = new PublishXmlRequest();
                        pubRequest.ParameterXml = string.Format(@"<importexportxml>
                                                           <entities>
                                                              <entity>{0}</entity>
                                                           </entities>
                                                           <nodes/><securityroles/><settings/><workflows/>
                                                        </importexportxml>", currentEmd.LogicalName);

                        Service.Execute(pubRequest);
                    },
                    PostWorkCallBack = evt =>
                    {
                        if (evt.Error != null)
                        {
                            string errorMessage = CrmExceptionHelper.GetErrorMessage(evt.Error, false);
                            MessageBox.Show(ParentForm, errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        tsbPublishEntity.Enabled = true;
                        tsbSaveAttributes.Enabled = true;
                        tsbLoadEntities.Enabled = true;
                    }
                });
            }
        }

        #endregion Publish Entity

        #endregion Methods

        #region Column Sorting Handlers

        private void lvAttributes_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == currentAttributesColumnOrder)
            {
                lvAttributes.Sorting = lvAttributes.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
                lvAttributes.ListViewItemSorter = new ListViewItemComparer(e.Column, lvAttributes.Sorting);
            }
            else
            {
                currentAttributesColumnOrder = e.Column;
                lvAttributes.ListViewItemSorter = new ListViewItemComparer(e.Column, SortOrder.Ascending);
            }
        }

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

            lvEntities.Sorting = lvEntities.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lvEntities.ListViewItemSorter = new ListViewItemComparer(e.Column, lvEntities.Sorting);
        }

        #endregion Column Sorting Handlers

        private void CheckItems()
        {
            foreach (ListViewItem item in lvAttributes.Items)
            {
                var amd = (AttributeMetadata)item.Tag;

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

        private void chkRequirementLevel_CheckedChanged(object sender, EventArgs e)
        {
            cboRequirementLevel.Enabled = chkRequirementLevel.Checked;
        }

        private void chkValidForAdvancedFind_CheckedChanged(object sender, EventArgs e)
        {
            CheckItems();
        }

        private void chkValidForAudit_CheckedChanged(object sender, EventArgs e)
        {
            CheckItems();
        }

        private AttributeRequiredLevel? MapSdkValue(int selectedValue)
        {
            switch (selectedValue)
            {
                case 0: return AttributeRequiredLevel.ApplicationRequired;
                case 1: return AttributeRequiredLevel.Recommended;
                case 2: return AttributeRequiredLevel.None;
                default: return null;
            }
        }
    }
}