// PROJECT : MsCrmTools.AuditCenter
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using MsCrmTools.AuditCenter.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using CrmExceptionHelper = XrmToolBox.CrmExceptionHelper;

namespace MsCrmTools.AuditCenter
{
    public partial class MainControl : PluginControlBase
    {
        public enum ActionState
        {
            None,
            Added,
            Removed
        }

        #region Variables

        private List<AttributeInfo> attributeInfos;
        private List<EntityMetadata> emds;
        private List<EntityInfo> entityInfos;
        private List<SortingConfiguration> sortingConfigurations;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class <see cref="MainControl"/>
        /// </summary>
        public MainControl()
        {
            InitializeComponent();
            entityInfos = new List<EntityInfo>();
            attributeInfos = new List<AttributeInfo>();
            sortingConfigurations = new List<SortingConfiguration>();
        }

        #endregion Constructor

        #region Methods

        private void TsbCloseClick(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void TsbConnectClick(object sender, EventArgs e)
        {
            ExecuteMethod(LoadEntities);
        }

        #endregion Methods

        #region Load Entities

        private void AddEntityAttributesToList(EntityMetadata emd)
        {
            lvAttributes.Items.Clear();

            foreach (AttributeMetadata amd in emd.Attributes.Where(a => a.IsAuditEnabled != null
                                                                        && a.IsAuditEnabled.Value
                                                                        && a.AttributeOf == null))
            {
                var attributeInfo = attributeInfos.FirstOrDefault(a => a.Amd == amd);
                if (attributeInfo == null)
                {
                    attributeInfos.Add(new AttributeInfo { Action = ActionState.None, InitialState = true, Amd = amd });
                }
                else if (attributeInfo.Action == ActionState.Removed)
                {
                    continue;
                }

                string displayName = amd.DisplayName != null && amd.DisplayName.UserLocalizedLabel != null
                    ? amd.DisplayName.UserLocalizedLabel.Label
                    : "N/A";

                var itemAttr = new ListViewItem { Text = displayName, Tag = amd };
                itemAttr.SubItems.Add(amd.LogicalName);
                lvAttributes.Items.Add(itemAttr);
            }
        }

        private void LoadEntities()
        {
            entityInfos = new List<EntityInfo>();
            attributeInfos = new List<AttributeInfo>();
            lvEntities.Items.Clear();
            lvAttributes.Items.Clear();
            gbEntities.Enabled = false;
            gbAttributes.Enabled = false;
            tsbChangeSystemAuditStatus.Enabled = false;
            tsbChangeSystemAuditStatus.Image = statusImageList.Images[2];

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading entities...",
                AsyncArgument = null,
                Work = (bw, e) =>
                {
                    emds = MetadataHelper.RetrieveEntities(Service);

                    bw.ReportProgress(0, "Retrieving system audit status...");

                    var orgs = Service.RetrieveMultiple(new QueryExpression
                    {
                        EntityName = "organization",
                        ColumnSet = new ColumnSet(new[] { "isauditenabled" })
                    });

                    e.Result = orgs[0].GetAttributeValue<bool>("isauditenabled");
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        string errorMessage = CrmExceptionHelper.GetErrorMessage(e.Error, true);
                        MessageBox.Show(ParentForm, errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        var isAuditEnabled = (bool)e.Result;
                        lblStatusStatus.Text = isAuditEnabled ? "ON" : "OFF";
                        lblStatusStatus.ForeColor = isAuditEnabled ? Color.Green : Color.Red;

                        tsbChangeSystemAuditStatus.Image = isAuditEnabled ? statusImageList.Images[1] : statusImageList.Images[0];
                        tsbChangeSystemAuditStatus.Text = isAuditEnabled ? "Deactivate global audit" : "Activate global audit";

                        try
                        {
                            lvEntities.Items.Clear();

                            foreach (EntityMetadata emd in emds.Where(x => x.IsAuditEnabled.Value))
                            {
                                entityInfos.Add(new EntityInfo { Action = ActionState.None, Emd = emd, InitialState = true });

                                var item = new ListViewItem { Text = emd.DisplayName.UserLocalizedLabel.Label, Tag = emd };
                                item.SubItems.Add(emd.LogicalName);
                                lvEntities.Items.Add(item);
                            }

                            SortGroups(lvAttributes);
                        }
                        catch (Exception error)
                        {
                            string errorMessage = CrmExceptionHelper.GetErrorMessage(error, true);
                            MessageBox.Show(ParentForm, errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    gbEntities.Enabled = true;
                    gbAttributes.Enabled = true;
                    tsbChangeSystemAuditStatus.Enabled = true;
                },
                ProgressChanged = e =>
                {
                    SetWorkingMessage(e.UserState.ToString());
                }
            });
        }

        #endregion Load Entities

        #region Entity selection

        private void lvEntities_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            if (list.SelectedItems.Count == 1)
            {
                var emd = (EntityMetadata)list.SelectedItems[0].Tag;

                AddEntityAttributesToList(emd);
            }
        }

        #endregion Entity selection

        #region Add/Remove Entities/Attributes

        private void PbAddAttributeClick(object sender, EventArgs e)
        {
            if (lvEntities.SelectedItems.Count != 1)
            {
                MessageBox.Show(this, "Please select one entity to add attributes!", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var emd = (EntityMetadata)lvEntities.SelectedItems[0].Tag;

            var list = lvAttributes.Items.Cast<ListViewItem>().Select(i => i.SubItems[1].Text);

            var apForm = new AttributePicker(emd, list, Service);
            if (apForm.ShowDialog(this) == DialogResult.OK)
            {
                foreach (var amd in apForm.AttributesToAdd)
                {
                    string displayName = amd.DisplayName != null && amd.DisplayName.UserLocalizedLabel != null
                        ? amd.DisplayName.UserLocalizedLabel.Label
                        : "N/A";

                    UpdateAttributeDictionary(amd, ActionState.Added);

                    var item = new ListViewItem { Text = displayName, Tag = amd };
                    item.SubItems.Add(amd.LogicalName);
                    lvAttributes.Items.Add(item);
                }

                RefreshSorting(lvAttributes);
            }
        }

        private void PbAddEntityClick(object sender, EventArgs e)
        {
            var epForm = new EntityPicker(emds);
            if (epForm.ShowDialog(this) == DialogResult.OK)
            {
                foreach (var emd in epForm.EntitiesToAdd)
                {
                    bool doContinue = true;
                    foreach (ListViewItem existingItem in lvEntities.Items)
                    {
                        if (((EntityMetadata)existingItem.Tag).LogicalName == emd.LogicalName)
                            doContinue = false;
                    }

                    if (!doContinue)
                        continue;

                    UpdateEntityDictionary(emd, ActionState.Added);

                    var item = new ListViewItem { Text = emd.DisplayName.UserLocalizedLabel.Label, Tag = emd };
                    item.SubItems.Add(emd.LogicalName);
                    item.Selected = true;
                    lvEntities.Items.Add(item);

                    // AddEntityAttributesToList(emd);

                    SortGroups(lvAttributes);
                }

                RefreshSorting(lvEntities);
            }
        }

        private void PbRemoveAttributeClick(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvAttributes.SelectedItems)
            {
                var amd = (AttributeMetadata)item.Tag;
                UpdateAttributeDictionary(amd, ActionState.Removed);
                lvAttributes.Items.Remove(item);
            }

            RefreshSorting(lvAttributes);
        }

        private void PbRemoveEntityClick(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvEntities.SelectedItems)
            {
                var emd = (EntityMetadata)item.Tag;

                UpdateEntityDictionary(emd, ActionState.Removed);

                lvEntities.Items.Remove(item);

                foreach (
                    ListViewItem attrItem in
                        lvAttributes.Items.Cast<ListViewItem>()
                            .Where(i => ((AttributeMetadata)i.Tag).EntityLogicalName == emd.LogicalName))
                {
                    lvAttributes.Items.Remove(attrItem);
                }
            }

            RefreshSorting(lvEntities);
        }

        private void UpdateAttributeDictionary(AttributeMetadata amd, ActionState actionState)
        {
            var item = attributeInfos.FirstOrDefault(a => a.Amd.LogicalName == amd.LogicalName && a.Amd.EntityLogicalName == amd.EntityLogicalName);
            if (item != null)
            {
                if (item.Action == ActionState.Removed && actionState == ActionState.Added
                    || item.Action == ActionState.Added && actionState == ActionState.Removed)
                    item.Action = ActionState.None;
                else
                    item.Action = actionState;
            }
            else
            {
                item = new AttributeInfo
                {
                    Action = actionState,
                    Amd = amd,
                    InitialState = actionState != ActionState.Added
                };

                attributeInfos.Add(item);
            }

            tsbApplyChanges.Enabled = !((entityInfos.All(ei => ei.Action == ActionState.None) &&
                                       attributeInfos.All(ai => ai.Action == ActionState.None)));

            SortGroups(lvAttributes);
        }

        private void UpdateEntityDictionary(EntityMetadata emd, ActionState actionState)
        {
            var item = entityInfos.FirstOrDefault(e => e.Emd.LogicalName == emd.LogicalName);
            if (item != null)
            {
                if (item.Action == ActionState.Removed && actionState == ActionState.Added
                    || item.Action == ActionState.Added && actionState == ActionState.Removed)
                    item.Action = ActionState.None;
                else
                    item.Action = actionState;
            }
            else
            {
                item = new EntityInfo
                {
                    Action = actionState,
                    Emd = emd,
                    InitialState = actionState != ActionState.Added
                };

                entityInfos.Add(item);
            }

            tsbApplyChanges.Enabled = !((entityInfos.All(ei => ei.Action == ActionState.None) &&
                                         attributeInfos.All(ai => ai.Action == ActionState.None)));

            SortGroups(lvAttributes);
        }

        #endregion Add/Remove Entities/Attributes

        #region Global Audit settings

        private void TsbChangeSystemAuditStatusClick(object sender, EventArgs e)
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Updating audit status...",
                AsyncArgument = null,
                Work = (bw, evt) =>
                {
                    var orgs = Service.RetrieveMultiple(new QueryExpression
                    {
                        EntityName = "organization",
                        ColumnSet = new ColumnSet(new[] { "isauditenabled" })
                    });

                    var auditStatus = orgs[0].GetAttributeValue<bool>("isauditenabled");
                    orgs[0]["isauditenabled"] = !auditStatus;
                    Service.Update(orgs[0]);
                },
                PostWorkCallBack = evt =>
                {
                    if (evt.Error != null)
                    {
                        MessageBox.Show(this, "An error occured: " + evt.Error.Message, "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    else
                    {
                        lblStatusStatus.Text = lblStatusStatus.Text == "ON" ? "OFF" : "ON";
                        tsbChangeSystemAuditStatus.Image = lblStatusStatus.Text == "ON" ? statusImageList.Images[1] : statusImageList.Images[0];
                        tsbChangeSystemAuditStatus.Text = lblStatusStatus.Text == "ON" ? "Deactivate system audit" : "Activate system audit";
                        lblStatusStatus.ForeColor = lblStatusStatus.ForeColor == Color.Green ? Color.Red : Color.Green;
                    }
                }
            });
        }

        #endregion Global Audit settings

        #region Apply changes to entities and attributes

        private void TsbApplyChangesClick(object sender, EventArgs e)
        {
            if (entityInfos.All(ei => ei.Action == ActionState.None) &&
                attributeInfos.All(ai => ai.Action == ActionState.None))
                return;

            gbEntities.Enabled = false;
            gbAttributes.Enabled = false;
            toolStripMenu.Enabled = false;

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Updating entities...",
                AsyncArgument = null,
                Work = (bw, evt) =>
                {
                    foreach (EntityInfo ei in entityInfos.OrderBy(entity => entity.Emd.LogicalName))
                    {
                        if (ei.Action == ActionState.Added)
                        {
                            bw.ReportProgress(0, string.Format("Enabling entity '{0}' for audit...", ei.Emd.LogicalName));

                            ei.Emd.IsAuditEnabled.Value = true;
                        }
                        else if (ei.Action == ActionState.Removed)
                        {
                            bw.ReportProgress(0, string.Format("Disabling entity '{0}' for audit...", ei.Emd.LogicalName));

                            ei.Emd.IsAuditEnabled.Value = false;
                        }
                        else
                        {
                            continue;
                        }

                        var request = new UpdateEntityRequest { Entity = ei.Emd };
                        Service.Execute(request);

                        ei.Action = ActionState.None;
                    }

                    bw.ReportProgress(0, "Updating attributes...");

                    foreach (AttributeInfo ai in attributeInfos.OrderBy(a => a.Amd.EntityLogicalName).ThenBy(a => a.Amd.LogicalName))
                    {
                        if (ai.Action == ActionState.Added)
                        {
                            bw.ReportProgress(0, string.Format("Enabling attribute '{0}' ({1}) for audit...", ai.Amd.LogicalName, ai.Amd.EntityLogicalName));

                            ai.Amd.IsAuditEnabled.Value = true;
                        }
                        else if (ai.Action == ActionState.Removed)
                        {
                            bw.ReportProgress(0, string.Format("Disabling attribute '{0}' ({1}) for audit...", ai.Amd.LogicalName, ai.Amd.EntityLogicalName));

                            ai.Amd.IsAuditEnabled.Value = false;
                        }
                        else
                        {
                            continue;
                        }

                        var request = new UpdateAttributeRequest { Attribute = ai.Amd, EntityName = ai.Amd.EntityLogicalName };
                        Service.Execute(request);

                        ai.Action = ActionState.None;
                    }

                    bw.ReportProgress(0, "Publishing changes...");

                    var publishRequest = new PublishXmlRequest { ParameterXml = "<importexportxml><entities>" };

                    foreach (EntityInfo ei in entityInfos.OrderBy(entity => entity.Emd.LogicalName))
                    {
                        publishRequest.ParameterXml += string.Format("<entity>{0}</entity>", ei.Emd.LogicalName);
                    }

                    publishRequest.ParameterXml +=
                        "</entities><securityroles/><settings/><workflows/></importexportxml>";

                    Service.Execute(publishRequest);
                },
                PostWorkCallBack = evt =>
                {
                    if (evt.Error != null)
                    {
                        MessageBox.Show(this, "An error occured: " + evt.Error.Message, "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }

                    gbEntities.Enabled = true;
                    gbAttributes.Enabled = true;
                    toolStripMenu.Enabled = true;

                    tsbApplyChanges.Enabled = !((entityInfos.All(ei => ei.Action == ActionState.None) &&
                                          attributeInfos.All(ai => ai.Action == ActionState.None)));
                },
                ProgressChanged = evt =>
                {
                    SetWorkingMessage(evt.UserState.ToString());
                }
            });
        }

        #endregion Apply changes to entities and attributes

        private void ListViewColumnClick(object sender, ColumnClickEventArgs e)
        {
            var lv = (ListView)sender;

            lv.Sorting = lv.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;

            lv.ListViewItemSorter = new ListViewItemComparer(e.Column, lv.Sorting);

            var configuration = sortingConfigurations.FirstOrDefault(sc => sc.List == lv);
            if (configuration == null)
            {
                configuration = new SortingConfiguration
                {
                    ColumnIndex = e.Column,
                    List = lv,
                    Order = lv.Sorting
                };

                sortingConfigurations.Add(configuration);
            }
            else
            {
                configuration.ColumnIndex = e.Column;
                configuration.Order = lv.Sorting;
            }
        }

        private void RefreshSorting(ListView list)
        {
            var configuration = sortingConfigurations.FirstOrDefault(sc => sc.List == list);
            if (configuration != null)
            {
                list.ListViewItemSorter = new ListViewItemComparer(configuration.ColumnIndex, configuration.Order);
            }
        }

        private void SortGroups(ListView lv)
        {
            var groups = new ListViewGroup[lv.Groups.Count];

            lv.Groups.CopyTo(groups, 0);

            Array.Sort(groups, new GroupComparer());

            lv.BeginUpdate();
            lv.Groups.Clear();
            lv.Groups.AddRange(groups);
            lv.EndUpdate();
        }
    }
}