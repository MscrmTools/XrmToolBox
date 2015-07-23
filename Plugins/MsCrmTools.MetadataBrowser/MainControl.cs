using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.MetadataBrowser.AppCode;
using MsCrmTools.MetadataBrowser.AppCode.LabelMd;
using MsCrmTools.MetadataBrowser.Forms;
using MsCrmTools.MetadataBrowser.Helpers;
using MsCrmTools.MetadataBrowser.UserControls;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace MsCrmTools.MetadataBrowser
{
    public partial class MainControl : PluginControlBase
    {
        private bool initialized;
        private ListViewColumnsSettings lvcSettings;        
        private EntityMetadata[] currentAllMetadata;
        private EntityMetadata currentMetadata;
   
        public MainControl()
        {
            InitializeComponent();
            lvcSettings = ListViewColumnsSettings.LoadSettings();

            this.Enter += MainControl_Enter;
        }

        void MainControl_Enter(object sender, EventArgs e)
        {
            if (sender != null)
            {
                if (sender is MainControl)
                {
                    if (((MainControl)sender).Service != null && !initialized)
                    {
                        ExecuteMethod(LoadEntities);
                        initialized = true;
                    }
                }
            }
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void tsbLoadEntities_Click(object sender, EventArgs e)
        {
             ExecuteMethod(LoadEntities);
        }

        public void LoadEntities()
        {
            // Loads listview header column for entities
            ListViewColumnHelper.AddColumnsHeader(entityListView, typeof(EntityMetadataInfo), ListViewColumnsSettings.EntityFirstColumns, lvcSettings.EntitySelectedAttributes, ListViewColumnsSettings.EntityAttributesToIgnore);
          
            WorkAsync("Loading Entities...",
                e =>
                {
                    // Search for all entities metadata
                    var request = new RetrieveAllEntitiesRequest{EntityFilters = EntityFilters.Entity};
                    var response = (RetrieveAllEntitiesResponse)Service.Execute(request);

                    currentAllMetadata = response.EntityMetadata;
                    
                    // return listview items
                    e.Result = BuildEntityItems(currentAllMetadata.ToList());
                },
                e =>
                {
                    entityListView.Items.Clear();
                    // Add listview items to listview
                    entityListView.Items.AddRange(((List<ListViewItem>)e.Result).ToArray());
                });
        }

        private List<ListViewItem> BuildEntityItems(IEnumerable<EntityMetadata> emds)
        {
            if (emds == null) return new List<ListViewItem>();

            var items = new List<ListViewItem>();

            // Stores each property in a listviewitem
            foreach (var metadata in emds.OrderBy(m => m.LogicalName))
            {
                var emd = new EntityMetadataInfo(metadata);

                var item = new ListViewItem(emd.LogicalName) { Tag = metadata };
                item.SubItems.Add(emd.SchemaName);
                item.SubItems.Add(emd.ObjectTypeCode.ToString(CultureInfo.InvariantCulture));
                AddSecondarySubItems(typeof(EntityMetadataInfo), ListViewColumnsSettings.EntityFirstColumns, lvcSettings.EntitySelectedAttributes, emd, item);

                items.Add(item);
            }

            return items;
        }

        private void entityListView_DoubleClick(object sender, EventArgs e)
        {
            ExecuteMethod(LoadEntity);
        }

        private void LoadEntity()
        {
            if (entityListView.SelectedItems.Count == 0)
                return;

            var emd = new EntityMetadataInfo((EntityMetadata)entityListView.SelectedItems[0].Tag);

            WorkAsync("Loading Entity...",
                dwe =>
                {
                    var request = new RetrieveEntityRequest
                    {
                        EntityFilters = EntityFilters.All,
                        LogicalName = ((EntityMetadataInfo)dwe.Argument).LogicalName
                    };
                    var response = (RetrieveEntityResponse)Service.Execute(request);
                    dwe.Result = response.EntityMetadata;
                },
                wce =>
                {
                    var emdFull = (EntityMetadata) wce.Result;

                    TabPage tab;
                    if (mainTabControl.TabPages.ContainsKey(emd.SchemaName))
                    {
                        tab = mainTabControl.TabPages[emd.SchemaName];
                        ((EntityPropertiesControl)tab.Controls[0]).RefreshContent(emdFull);
                    }
                    else
                    {
                        mainTabControl.TabPages.Add(emd.SchemaName,emd.SchemaName);
                        tab = mainTabControl.TabPages[emd.SchemaName];

                        var epc = new EntityPropertiesControl(emdFull, lvcSettings)
                        {
                            Dock = DockStyle.Fill,
                            Name = emdFull.SchemaName
                        };
                        epc.OnColumnSettingsUpdated += epc_OnColumnSettingsUpdated;
                        tab.Controls.Add(epc);
                        mainTabControl.SelectTab(tab);
                    }
                },
                emd);
        }

        void epc_OnColumnSettingsUpdated(object sender, ColumnSettingsUpdatedEventArgs e)
        {
            lvcSettings = (ListViewColumnsSettings)e.Settings.Clone();
            lvcSettings.SaveSettings();

            foreach (TabPage page in mainTabControl.TabPages)
            {
                if (page.TabIndex == 0) continue;
                var ctrl = ((EntityPropertiesControl)page.Controls[0]);
                if (ctrl.Name != e.Control.Name)
                {
                    ctrl.RefreshColumns(lvcSettings);
                }
            }
        }

        private void AddSecondarySubItems(Type type, string[] firstColumns, string[] selectedAttributes, object o, ListViewItem item)
        {
            if (selectedAttributes == null)
            {
                foreach (var prop in type.GetProperties().OrderBy(p => p.Name))
                {
                    if (firstColumns.Contains(prop.Name))
                        continue;

                    if(ListViewColumnsSettings.EntityAttributesToIgnore.Contains(prop.Name))
                        continue;

                    object value = null;

                    try
                    {
                        value = prop.GetValue(o, null);
                    }
                    catch (Exception error)
                    {
                        //MessageBox.Show(error.ToString());
                    }

                    var labelInfoValue = value as LabelInfo;
                    var managedPropertyInfoValue = value as BooleanManagedPropertyInfo;
                    var cascadeConfigurationInfoValue = value as CascadeConfigurationInfo;
                    var associatedMenuBehaviorInfoValue = value as AssociatedMenuConfigurationInfo;
                    var requiredLevelInfoValue = value as AttributeRequiredLevelManagedPropertyInfo;
                    if (labelInfoValue != null)
                    {
                        item.SubItems.Add(labelInfoValue.UserLocalizedLabel != null
                            ? labelInfoValue.UserLocalizedLabel.Label
                            : "N/A");
                    }
                    else if (managedPropertyInfoValue != null)
                    {
                        item.SubItems.Add(managedPropertyInfoValue.Value.ToString());
                    }
                    else if (requiredLevelInfoValue != null)
                    {
                        item.SubItems.Add(requiredLevelInfoValue.Value.ToString());
                    }
                    else if (cascadeConfigurationInfoValue != null || associatedMenuBehaviorInfoValue != null)
                    {
                        item.SubItems.Add("(Open row to see details)");
                    }
                    else
                    {
                        item.SubItems.Add(value == null ? "" : value.ToString());
                    }

                }
            }
            else
            {
                var properties = type.GetProperties();

                foreach (var attr in selectedAttributes)
                {
                    if (firstColumns.Contains(attr))
                        continue;

                    var prop = properties.First(p => p.Name == attr);

                    try
                    {
                        var value = prop.GetValue(o, null);
                        var labelInfoValue = value as LabelInfo;
                        if (labelInfoValue != null)
                        {
                            item.SubItems.Add(labelInfoValue.UserLocalizedLabel != null
                                ? labelInfoValue.UserLocalizedLabel.Label
                                : "N/A");
                        }
                        else
                        {
                            item.SubItems.Add(value == null ? "" : value.ToString());
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            var list = (ListView) sender;
            list.Sorting = list.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            list.ListViewItemSorter = new ListViewItemComparer(e.Column, list.Sorting);
        } 

        private void tsbColumns_Click(object sender, EventArgs e)
        {
            switch (((ToolStripButton) sender).Name)
            {
                case "tsbEntityColumns":
                {
                    var dialog = new ColumnSelector(typeof (EntityMetadataInfo),
                        ListViewColumnsSettings.EntityFirstColumns,
                        ListViewColumnsSettings.EntityAttributesToIgnore,
                        lvcSettings.EntitySelectedAttributes);

                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        lvcSettings.EntitySelectedAttributes = dialog.UpdatedCurrentAttributes;
                        entityListView.Columns.Clear();
                        entityListView.Items.Clear();

                        ListViewColumnHelper.AddColumnsHeader(entityListView,
                            typeof (EntityMetadataInfo),
                            ListViewColumnsSettings.EntityFirstColumns,
                            lvcSettings.EntitySelectedAttributes,
                            ListViewColumnsSettings.EntityAttributesToIgnore);

                        entityListView.Items.AddRange(BuildEntityItems(currentAllMetadata).ToArray());
                    }
                }
                    break;
                default:
                {
                    MessageBox.Show(this, "Unexpected source for hiding panels");
                }
                    break;
            }

            try
            {
                lvcSettings.SaveSettings();
                foreach (TabPage page in mainTabControl.TabPages)
                {
                    if (page.TabIndex == 0) continue;

                    ((EntityPropertiesControl)page.Controls[0]).RefreshColumns(lvcSettings);
                }
            }
            catch (UnauthorizedAccessException error)
            {
                MessageBox.Show(this, "An error occured while trying to save your settings: " + error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
