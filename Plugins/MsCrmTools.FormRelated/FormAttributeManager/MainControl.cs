using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.FormAttributeManager.AppCode;
using MsCrmTools.FormAttributeManager.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace MsCrmTools.FormAttributeManager
{
    public partial class MainControl : PluginControlBase
    {
        public MainControl()
        {
            InitializeComponent();
        }

        private void AddLogItem(string entity, string form, string message, bool isError)
        {
            lvLogs.Items.Add(new ListViewItem(entity)
            {
                SubItems = { form, message },
                ForeColor = isError ? Color.Red : Color.Black
            });
        }

        private void addToFormsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Define which attribute is the reference attribute
            var possibleAttributes = attributeSelector1.Attributes;

            foreach (var fi in listView1.CheckedItems.Cast<ListViewItem>().Select(i => (FormInfo)i.Tag))
            {
                for (int i = possibleAttributes.Count - 1; i >= 0; i--)
                {
                    if (!fi.HasAttribute(possibleAttributes[i].LogicalName))
                    {
                        possibleAttributes.Remove(possibleAttributes[i]);
                    }
                }
            }

            var selector = new ReferenceAttributeSelector(possibleAttributes);
            if (selector.ShowDialog(this) == DialogResult.OK)
            {
                foreach (ListViewItem item in listView1.CheckedItems)
                {
                    var fi = (FormInfo)item.Tag;
                    var source = selector.SelectedAttribute.LogicalName;

                    foreach (var amd in attributeSelector1.SelectedAttributes)
                    {
                        try
                        {
                            fi.AddAttribute(amd.LogicalName, AttributeClass.GetClassId(amd).ToString("B"), amd.DisplayName, source);

                            source = amd.LogicalName;

                            AddLogItem(attributeSelector1.SelectedEntity.LogicalName, fi.ToString(),
                                "Attribute " + amd.LogicalName + " added to form",
                                false);

                            item.SubItems[item.SubItems.Count - 1].Text = true.ToString();

                            tslInfo.Visible = true;
                        }
                        catch (Exception error)
                        {
                            AddLogItem(attributeSelector1.SelectedEntity.LogicalName, fi.ToString(), error.Message, true);
                        }
                    }
                }
            }
        }

        private void attributeSelector1_OnAttributeSelected(object sender, AttributeSelectedEventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                var fi = (FormInfo)item.Tag;
                if (item.SubItems.Count == 1)
                {
                    item.SubItems.Add(string.Empty);
                }
                item.SubItems[item.SubItems.Count - 1].Text = fi.HasAttribute(e.Metadata.LogicalName).ToString();
            }
        }

        private void LoadEntities()
        {
            attributeSelector1.Service = Service;
            attributeSelector1.LoadEntities();
        }

        private void removeFromFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.CheckedItems)
            {
                var fi = (FormInfo)item.Tag;

                foreach (var amd in attributeSelector1.SelectedAttributes)
                {
                    try
                    {
                        fi.RemoveAttribute(amd.LogicalName);

                        AddLogItem(attributeSelector1.SelectedEntity.LogicalName, fi.ToString(),
                            "Attribute " + amd.LogicalName + " removed from form",
                            false);

                        item.SubItems[item.SubItems.Count - 1].Text = false.ToString();

                        tslInfo.Visible = true;
                    }
                    catch (Exception error)
                    {
                        AddLogItem(attributeSelector1.SelectedEntity.LogicalName, fi.ToString(), error.Message, true);
                    }
                }
            }
        }

        private void tsbClearLog_Click(object sender, EventArgs e)
        {
            lvLogs.Items.Clear();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void tsbLoadEntities_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadEntities);
        }

        private void tsbPublish_Click(object sender, EventArgs e)
        {
            if (attributeSelector1.SelectedEntity == null)
                return;

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Publishing entity...",
                AsyncArgument = attributeSelector1.SelectedEntity.LogicalName,
                Work = (bw, evt) =>
                {
                    var fm = new FormManager(Service);
                    fm.PublishForm(evt.Argument.ToString());
                },
                PostWorkCallBack = evt =>
                {
                    if (evt.Error != null)
                    {
                        MessageBox.Show(evt.Error.ToString());
                    }
                }
            });
        }

        private void tsbSaveForms_Click(object sender, EventArgs e)
        {
            if (attributeSelector1.SelectedEntity == null)
                return;

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Updating forms...",
                AsyncArgument = listView1.Items.Cast<ListViewItem>().Select(i => (FormInfo)i.Tag).ToList(),
                Work = (bw, evt) =>
                {
                    var fis = (List<FormInfo>)evt.Argument;

                    foreach (var fi in fis)
                    {
                        fi.Update(Service);
                    }
                },
                PostWorkCallBack = evt =>
                {
                    if (evt.Error != null)
                    {
                        MessageBox.Show(evt.Error.ToString());
                    }
                    else
                    {
                        tslInfo.Visible = false;
                    }
                }
            });
        }

        private void tsmiDisableFieldDisplay_Click(object sender, EventArgs e)
        {
            foreach (var fi in listView1.CheckedItems.Cast<ListViewItem>().Select(i => (FormInfo)i.Tag))
            {
                foreach (var amd in attributeSelector1.SelectedAttributes)
                {
                    try
                    {
                        fi.SetAttributeLabelDisplayMode(amd.LogicalName, false);

                        AddLogItem(attributeSelector1.SelectedEntity.LogicalName, fi.ToString(),
                            "Attribute " + amd.LogicalName + " label hidden", false);
                        tslInfo.Visible = true;
                    }
                    catch (Exception error)
                    {
                        AddLogItem(attributeSelector1.SelectedEntity.LogicalName, fi.ToString(), error.Message, true);
                    }
                }
            }
        }

        private void tsmiEnableFieldDisplay_Click(object sender, EventArgs e)
        {
            foreach (var fi in listView1.CheckedItems.Cast<ListViewItem>().Select(i => (FormInfo)i.Tag))
            {
                foreach (var amd in attributeSelector1.SelectedAttributes)
                {
                    try
                    {
                        fi.SetAttributeLabelDisplayMode(amd.LogicalName, true);

                        AddLogItem(attributeSelector1.SelectedEntity.LogicalName, fi.ToString(),
                            "Attribute " + amd.LogicalName + " label displayed", false);
                        tslInfo.Visible = true;
                    }
                    catch (Exception error)
                    {
                        AddLogItem(attributeSelector1.SelectedEntity.LogicalName, fi.ToString(), error.Message, true);
                    }
                }
            }
        }

        private void tsmiLockField_Click(object sender, EventArgs e)
        {
            foreach (var fi in listView1.CheckedItems.Cast<ListViewItem>().Select(i => (FormInfo)i.Tag))
            {
                foreach (var amd in attributeSelector1.SelectedAttributes)
                {
                    try
                    {
                        fi.SetAttributeLockMode(amd.LogicalName, true);

                        AddLogItem(attributeSelector1.SelectedEntity.LogicalName, fi.ToString(),
                            "Attribute " + amd.LogicalName + " locked", false);
                        tslInfo.Visible = true;
                    }
                    catch (Exception error)
                    {
                        AddLogItem(attributeSelector1.SelectedEntity.LogicalName, fi.ToString(), error.Message, true);
                    }
                }
            }
        }

        private void tsmiMarkAsEditable_Click(object sender, EventArgs e)
        {
            foreach (var fi in listView1.CheckedItems.Cast<ListViewItem>().Select(i => (FormInfo)i.Tag))
            {
                foreach (var amd in attributeSelector1.SelectedAttributes)
                {
                    try
                    {
                        fi.SetAttributeReadOnlyMode(amd.LogicalName, false);

                        AddLogItem(attributeSelector1.SelectedEntity.LogicalName, fi.ToString(),
                            "Attribute " + amd.LogicalName + " enabled", false);
                        tslInfo.Visible = true;
                    }
                    catch (Exception error)
                    {
                        AddLogItem(attributeSelector1.SelectedEntity.LogicalName, fi.ToString(), error.Message, true);
                    }
                }
            }
        }

        private void tsmiMarkAsNotVisible_Click(object sender, EventArgs e)
        {
            foreach (var fi in listView1.CheckedItems.Cast<ListViewItem>().Select(i => (FormInfo)i.Tag))
            {
                foreach (var amd in attributeSelector1.SelectedAttributes)
                {
                    try
                    {
                        fi.SetAttributeVisibilityMode(amd.LogicalName, false);

                        AddLogItem(attributeSelector1.SelectedEntity.LogicalName, fi.ToString(),
                            "Attribute " + amd.LogicalName + " hidden", false);
                        tslInfo.Visible = true;
                    }
                    catch (Exception error)
                    {
                        AddLogItem(attributeSelector1.SelectedEntity.LogicalName, fi.ToString(), error.Message, true);
                    }
                }
            }
        }

        private void tsmiMarkAsReadOnly_Click(object sender, EventArgs e)
        {
            foreach (var fi in listView1.CheckedItems.Cast<ListViewItem>().Select(i => (FormInfo)i.Tag))
            {
                foreach (var amd in attributeSelector1.SelectedAttributes)
                {
                    try
                    {
                        fi.SetAttributeReadOnlyMode(amd.LogicalName, true);

                        AddLogItem(attributeSelector1.SelectedEntity.LogicalName, fi.ToString(),
                            "Attribute " + amd.LogicalName + " disabled", false);
                        tslInfo.Visible = true;
                    }
                    catch (Exception error)
                    {
                        AddLogItem(attributeSelector1.SelectedEntity.LogicalName, fi.ToString(), error.Message, true);
                    }
                }
            }
        }

        private void tsmiMarkAsVisible_Click(object sender, EventArgs e)
        {
            foreach (var fi in listView1.CheckedItems.Cast<ListViewItem>().Select(i => (FormInfo)i.Tag))
            {
                foreach (var amd in attributeSelector1.SelectedAttributes)
                {
                    try
                    {
                        fi.SetAttributeVisibilityMode(amd.LogicalName, true);

                        AddLogItem(attributeSelector1.SelectedEntity.LogicalName, fi.ToString(),
                            "Attribute " + amd.LogicalName + " visible", false);
                        tslInfo.Visible = true;
                    }
                    catch (Exception error)
                    {
                        AddLogItem(attributeSelector1.SelectedEntity.LogicalName, fi.ToString(), error.Message, true);
                    }
                }
            }
        }

        private void tsmiUnlockField_Click(object sender, EventArgs e)
        {
            foreach (var fi in listView1.CheckedItems.Cast<ListViewItem>().Select(i => (FormInfo)i.Tag))
            {
                foreach (var amd in attributeSelector1.SelectedAttributes)
                {
                    try
                    {
                        fi.SetAttributeLockMode(amd.LogicalName, false);

                        AddLogItem(attributeSelector1.SelectedEntity.LogicalName, fi.ToString(),
                            "Attribute " + amd.LogicalName + " unlocked", false);
                        tslInfo.Visible = true;
                    }
                    catch (Exception error)
                    {
                        AddLogItem(attributeSelector1.SelectedEntity.LogicalName, fi.ToString(), error.Message, true);
                    }
                }
            }
        }

        #region Loading Entity Forms

        private void attributeSelector1_OnEntitySelected(object sender, EntitySelectedEventArgs e)
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading forms...",
                AsyncArgument = e.Metadata,
                Work = (bw, evt) =>
                {
                    var formManager = new FormManager(Service);
                    var forms = formManager.GetAllFormsByTypeCode(((EntityMetadata)evt.Argument).ObjectTypeCode.Value, ConnectionDetail);
                    var items = forms.Select(form => new FormInfo(form)).Select(fi => new ListViewItem(fi.ToString()) { Tag = fi }).ToList();
                    evt.Result = items;
                },
                PostWorkCallBack = evt =>
                {
                    if (evt.Error != null)
                    {
                        MessageBox.Show(evt.Error.ToString());
                    }
                    else
                    {
                        listView1.Items.Clear();
                        listView1.Items.AddRange(((List<ListViewItem>)evt.Result).ToArray());

                        // Adds forms list to attribute selector
                        attributeSelector1.EntityForms = ((List<ListViewItem>)evt.Result).Select(i => (FormInfo)i.Tag).ToList();
                    }
                }
            });
        }

        #endregion Loading Entity Forms

        private void updateLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (attributeSelector1.SelectedAttributes.Count != 1)
            {
                MessageBox.Show(this, "This feature requires that only one attribute is selected");
                return;
            }

            var labelDialog = new UpdateLabelDialog(attributeSelector1.LocaleId);
            if (labelDialog.ShowDialog(this) == DialogResult.OK)
            {
                foreach (var fi in listView1.CheckedItems.Cast<ListViewItem>().Select(i => (FormInfo)i.Tag))
                {
                    foreach (var amd in attributeSelector1.SelectedAttributes)
                    {
                        try
                        {
                            fi.ChangeLabel(amd.LogicalName, attributeSelector1.LocaleId, labelDialog.Label);

                            AddLogItem(attributeSelector1.SelectedEntity.LogicalName, fi.ToString(),
                                "Attribute " + amd.LogicalName + " label updated", false);
                            tslInfo.Visible = true;
                        }
                        catch (Exception error)
                        {
                            AddLogItem(attributeSelector1.SelectedEntity.LogicalName, fi.ToString(), error.Message, true);
                        }
                    }
                }
            }
        }
    }
}