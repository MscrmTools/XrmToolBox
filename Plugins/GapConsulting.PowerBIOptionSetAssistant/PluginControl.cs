using GapConsulting.PowerBIOptionSetAssistant.AppCode;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace GapConsulting.PowerBIOptionSetAssistant
{
    public partial class PluginControl : PluginControlBase
    {
        private EntityMetadataCollection emc;

        public PluginControl()
        {
            InitializeComponent();
        }

        public void DeleteEntity()
        {
            string message = "Deleting this entity will also delete records of this entity! Are you sure you want to proceed?";
            if (MessageBox.Show(this, message, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            tsbCreateRecords.Enabled = false;
            tsbDeleteEntity.Enabled = false;
            tsbLoadEntities.Enabled = false;

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Deleting entity...",
                Work = (bw, evt) =>
                {
                    var mm = new MetadataManager(Service);
                    var entityExists = mm.EntityExists("gap_powerbioptionsetref");
                    if (!entityExists)
                    {
                        throw new Exception(
                            "There is no 'Power BI Option-Set Xref' entity on the connected organization");
                    }

                    mm.DeleteEntity("gap_powerbioptionsetref");
                },
                PostWorkCallBack = evt =>
                {
                    if (evt.Error != null)
                    {
                        MessageBox.Show(this, evt.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    tsbCreateRecords.Enabled = true;
                    tsbDeleteEntity.Enabled = true;
                    tsbLoadEntities.Enabled = true;
                }
            });
        }

        public void LoadEntities()
        {
            lvEntities.Items.Clear();

            tsbCreateRecords.Enabled = false;
            tsbDeleteEntity.Enabled = false;
            tsbLoadEntities.Enabled = false;

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading Entities...",
                Work = (bw, evt) =>
                {
                    var mm = new MetadataManager(Service);
                    evt.Result = mm.GetEntitiesMetadata();
                },
                PostWorkCallBack = evt =>
                {
                    emc = (EntityMetadataCollection)evt.Result;

                    var list = new List<ListViewItem>();

                    foreach (var em in emc.Where(e => e.Attributes.Any(
                        a => a.AttributeType.Value == AttributeTypeCode.Picklist
                             || a.AttributeType.Value == AttributeTypeCode.State
                             || a.AttributeType.Value == AttributeTypeCode.Status)))
                    {
                        var item =
                            new ListViewItem(em.DisplayName == null || em.DisplayName.UserLocalizedLabel == null
                                ? "N/A"
                                : em.DisplayName.UserLocalizedLabel.Label);
                        item.SubItems.Add(em.LogicalName);
                        item.Tag = em;

                        list.Add(item);
                    }

                    lvEntities.Items.AddRange(list.ToArray());

                    tsbCreateRecords.Enabled = true;
                    tsbDeleteEntity.Enabled = true;
                    tsbLoadEntities.Enabled = true;
                }
            });
        }

        private void CreateRecords(Settings settings)
        {
            foreach (var optionSet in settings.OptionSets)
            {
                OptionMetadataCollection omc = null;

                if (optionSet is PicklistAttributeMetadata)
                {
                    omc = ((PicklistAttributeMetadata)optionSet).OptionSet.Options;
                }
                else if (optionSet is StateAttributeMetadata)
                {
                    omc = ((StateAttributeMetadata)optionSet).OptionSet.Options;
                }
                else
                {
                    omc = ((StatusAttributeMetadata)optionSet).OptionSet.Options;
                }

                foreach (OptionMetadata option in omc)
                {
                    foreach (LocalizedLabel label in option.Label.LocalizedLabels)
                    {
                        bool exists = true;
                        var record = GetRecord(settings.AllMetadata.First(e => e.LogicalName == optionSet.EntityLogicalName).SchemaName, optionSet.SchemaName, option.Value.Value, label.LanguageCode);
                        if (record == null)
                        {
                            record = new Entity("gap_powerbioptionsetref");
                            exists = false;
                        }
                        else
                        {
                            // The label did not change, no need to update
                            if (record.GetAttributeValue<string>("gap_label") == label.Label)
                            {
                                continue;
                            }
                        }

                        record["gap_entityname"] = settings.AllMetadata.First(e => e.LogicalName == optionSet.EntityLogicalName).DisplayName.UserLocalizedLabel.Label;
                        record["gap_entityschemaname"] = settings.AllMetadata.First(e => e.LogicalName == optionSet.EntityLogicalName).SchemaName;
                        record["gap_optionsetschemaname"] = optionSet.SchemaName;
                        record["gap_value"] = option.Value;
                        record["gap_language"] = label.LanguageCode;
                        record["gap_label"] = label.Label;

                        if (exists)
                        {
                            Service.Update(record);
                        }
                        else
                        {
                            Service.Create(record);
                        }
                    }
                }
            }
        }

        private Entity GetRecord(string entitySchemaName, string schemaName, int value, int languageCode)
        {
            var records = Service.RetrieveMultiple(new QueryExpression("gap_powerbioptionsetref")
            {
                ColumnSet = new ColumnSet(true),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("gap_entityschemaname", ConditionOperator.Equal, entitySchemaName),
                        new ConditionExpression("gap_optionsetschemaname", ConditionOperator.Equal, schemaName),
                        new ConditionExpression("gap_value", ConditionOperator.Equal, value),
                        new ConditionExpression("gap_language", ConditionOperator.Equal, languageCode),
                    }
                }
            }).Entities;

            if (records.Count > 1)
            {
                throw new Exception(string.Format("Multiple records found for unique OptionSet/Language/Value ({0},{1},{2})",
                    schemaName, languageCode, value));
            }

            return records.FirstOrDefault();
        }

        private void Listview_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            var lv = (ListView)sender;
            lv.Sorting = lvEntities.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lv.ListViewItemSorter = new ListViewItemComparer(e.Column, lv.Sorting);
        }

        private void llSelectAllOptionSet_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem item in lvOptionSets.Items)
            {
                item.Checked = true;
            }
        }

        private void llSelectNoOptionSet_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem item in lvOptionSets.Items)
            {
                item.Checked = false;
            }
        }

        private void lvEntities_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var emd = (EntityMetadata)e.Item.Tag;

            if (e.Item.Checked)
            {
                foreach (var attr in emd.Attributes)
                {
                    var item = new ListViewItem(attr.DisplayName.UserLocalizedLabel.Label);
                    item.SubItems.Add(attr.LogicalName);
                    item.SubItems.Add(emd.DisplayName.UserLocalizedLabel.Label);
                    item.Name = emd.LogicalName + attr.LogicalName;
                    item.Tag = attr;
                    lvOptionSets.Items.Add(item);
                }
            }
            else
            {
                foreach (var attr in emd.Attributes)
                {
                    lvOptionSets.Items.RemoveByKey(emd.LogicalName + attr.LogicalName);
                }
            }
        }

        private void tsbCloseThisTab_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void tsbCreateRecords_Click(object sender, EventArgs e)
        {
            if (lvOptionSets.CheckedItems.Count == 0)
                return;

            var mm = new MetadataManager(Service);
            var entityExists = mm.EntityExists("gap_powerbioptionsetref");
            if (!entityExists)
            {
                var message = "'Power BI Option-Set Xref' entity does not exist on your organization. Would you like to create it? If you answer 'no', the process will be aborted";
                if (MessageBox.Show(this, message, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
            }

            var settings = new Settings
            {
                CreateEntity = !entityExists,
                OptionSets = lvOptionSets.CheckedItems.Cast<ListViewItem>().Select(o => (AttributeMetadata)o.Tag).ToList(),
                AllMetadata = emc
            };

            tsbCreateRecords.Enabled = false;
            tsbDeleteEntity.Enabled = false;
            tsbLoadEntities.Enabled = false;

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Initializing...",
                AsyncArgument = settings,
                Work = (bw, evt) =>
                {
                    if (((Settings)evt.Argument).CreateEntity)
                    {
                        var organization = Service.RetrieveMultiple(new QueryExpression("organization") { ColumnSet = new ColumnSet("languagecode") }).Entities.First();

                        bw.ReportProgress(0, "Creating entity...");
                        mm.CreatePowerBiOptionSetRefEntity(organization.GetAttributeValue<int>("languagecode"), bw);
                    }

                    bw.ReportProgress(0, "Processing Option Sets ...");

                    CreateRecords(settings);
                },
                ProgressChanged = evt =>
                {
                    SetWorkingMessage(evt.UserState.ToString());
                },
                PostWorkCallBack = evt =>
                {
                    tsbCreateRecords.Enabled = true;
                    tsbDeleteEntity.Enabled = true;
                    tsbLoadEntities.Enabled = true;
                }
            });
        }

        private void tsbDeleteEntity_Click(object sender, EventArgs e)
        {
            ExecuteMethod(DeleteEntity);
        }

        private void tsbLoadEntities_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadEntities);
        }
    }
}