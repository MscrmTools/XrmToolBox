using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System.ComponentModel;

namespace xrmtb.XrmToolBox.Controls
{
    public partial class EntitiesCollectionListView : BoundListViewControl
    {
        /// <summary>
        /// 
        /// </summary>
        public EntitiesCollectionListView()
        {
            InitializeComponent();
        }
        /// <summary>
        /// List of all checked EntityMetadata objects in the ListView
        /// </summary>
        [Category("XrmToolBox")]
        [DisplayName("Checked List")]
        [Description("List of all Records that are checked in the control.")]
        [Browsable(false)]
        public List<Entity> CheckedEntities { get => CheckedObjects?.Select(i => i as Entity).ToList(); }

        /// <summary>
        /// The currently selected EntityMetadata object in the ListView
        /// </summary>
        [Category("XrmToolBox")]
        [DisplayName("Selected Entity")]
        [Description("The Entity that is currently selected the control.")]
        [Browsable(false)]
        public Entity SelectedEntity { get => SelectedItem as Entity; }

        /// <summary>
        /// List of all loaded EntityMetadata objects for the current connection
        /// </summary>
        [Category("XrmToolBox")]
        [DisplayName("All Entities List")]
        [Description("List of all Entities that have been loaded.")]
        [Browsable(false)]
        public List<Entity> AllEntities
        {
            get => AllItems?.ConvertAll<Entity>(new Converter<object, Entity>((item) => { return item as Entity; }));
        }

        /// <summary>
        /// Override the LoadData method so that we can convert the Entity and its Attributes to ListViewItems
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        public override void LoadData<T>(List<T> items)
        {
            // ensure that the entities list is of type entity 
            var records = items.Select(i => i as Entity).ToList();

            // covnert the Entities to objects that can be displayed
            SuspendLayout();
            Enabled = false;
            Items.Clear();
            Refresh();

            // persist the list of list view items for the filtering
            _listViewItemsColl = new List<ListViewItem>();

            // if ListItemType is null, we are clearing things out 
            if (records != null && records?.Count > 0)
            {
                // override the columns using the attributes 
                var cols = Columns;

                foreach (var record in records)
                {
                    var col = cols[0];
                    var colDef = col.Tag as ListViewColumnDef;

                    // new list view item 
                    var lvItem = new ListViewItem()
                    {
                        Name = cols[0].Name,
                        ImageIndex = 0,
                        StateImageIndex = 0,
                        Text = record[col.Name].ToString(),
                        Tag = record,  // stash the template here so we can view details later
                        Group = null
                    };

                    for (var i = 1; i < cols.Count; i++)
                    {
                        var colVal = (record.Attributes.ContainsKey(cols[i].Name)) ? record[cols[i].Name].ToString() :null;
                        var subitem = new ListViewItem.ListViewSubItem(lvItem, colVal)
                        {
                            Name = cols[i].Name
                        };
                        lvItem.SubItems.Add(subitem);
                    }

                    // add to the internal collection of ListView Items and the external list
                    _listViewItemsColl.Add(lvItem);
                }

                Items.AddRange(_listViewItemsColl.ToArray<ListViewItem>());

                // now auto size using the values specified
                AutoResizeColumns(AutosizeColumns);
            }

            ResumeLayout();
            Enabled = true;

            _allItems = records.Select(i => i as object).ToList(); ;
        }
    }
}
