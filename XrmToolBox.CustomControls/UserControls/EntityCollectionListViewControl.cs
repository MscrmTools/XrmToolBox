using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System.ComponentModel;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;

namespace XrmToolBox.CustomControls
{
    public partial class EntityCollectionListViewControl : BoundListViewControl
    {
        #region Private Properties
        private bool _showFriendlyNames = true;
        private bool _showLocalTimes = true;
        private bool _autoRefresh = false;
        #endregion
        /// <summary>
        /// 
        /// </summary>
        public EntityCollectionListViewControl()
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

        [Category("XrmToolBox")]
        [DefaultValue(false)]
        [Description("True to show timestamps in local time, false to show UTC. Only valid when ShowFriendlyNames is true.")]
        public bool ShowLocalTimes
        {
            get { return _showLocalTimes; }
            set {
                if (value != _showLocalTimes)
                {
                    _showLocalTimes = value && ShowFriendlyNames;
                    if (AutoRefresh)
                    {
                        Refresh();
                    }
                }
            }
        }

        [Category("XrmToolBox")]
        [DefaultValue(false)]
        [Description("True to show friendly names, False to show logical names and guid etc.")]
        public bool ShowFriendlyNames
        {
            get { return _showFriendlyNames; }
            set {
                if (value != _showFriendlyNames)
                {
                    _showFriendlyNames = value;
                    if (!_showFriendlyNames)
                    {
                        _showLocalTimes = false;
                    }
                    if (AutoRefresh)
                    {
                        Refresh();
                    }
                }
            }
        }

        [Category("XrmToolBox")]
        [DefaultValue(true)]
        [Description("Specify if content shall be automatically refreshed when entitycollection, service, flags etc are changed.")]
        public bool AutoRefresh
        {
            get { return _autoRefresh; }
            set {
                if (value != _autoRefresh)
                {
                    _autoRefresh = value;
                    if (_autoRefresh)
                    {
                        Refresh();
                    }
                }
            }
        }

        /// <summary>
        /// Load an existing list of Entities from an EntityCollection
        /// </summary>
        /// <param name="entityColl"></param>
        public void LoadData(EntityCollection entityColl)
        {
            LoadData(entityColl.Entities.ToList());
        }

        /// <summary>
        /// Load a list of Entities from a simple FetchXml query
        /// </summary>
        /// <param name="fetchXml"></param>
        public void LoadData(string fetchXml)
        {
            RetrieveEntities(new FetchExpression(fetchXml));
        }

        /// <summary>
        /// Load entities for a given entity name and a list of attributes
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="attributes"></param>
        /// <param name="top"></param>
        public void LoadData(string entityName, List<string> attributes = null, int? top = null)
        {
            // set up new query expression
            var query = new QueryExpression(entityName)
            {
                TopCount = (top.HasValue) ? top : 5000,
                ColumnSet = (attributes != null) ? new ColumnSet(attributes.ToArray()) : new ColumnSet(true)
            };

            RetrieveEntities(query);
        }

        /// <summary>
        /// Helper method for calling RetrieveMuliple on a BackgroundWorker
        /// </summary>
        /// <param name="query"></param>
        private void RetrieveEntities(QueryBase query) {

            if (Service == null)
            {
                var ex = new InvalidOperationException("The Service reference must be set before calling LoadData.");

                // raise the error event and if set, throw error
                OnNotificationMessage(ex.Message, MessageLevel.Exception, ex);
                return;
            }

            // load the entity collection and build columns from attributes
            OnBeginLoadData();

            try
            {
                ClearData();

                OnProgressChanged(0, "Begin loading Records from CRM");

                var worker = new BackgroundWorker();

                worker.DoWork += (w, e) => {
                    var queryExp = e.Argument as QueryBase;

                    var fetchReq = new RetrieveMultipleRequest {
                        Query = queryExp
                    };

                    var records = Service.Execute(fetchReq) as RetrieveMultipleResponse;
                    List<AttributeMetadata> attribs = CommonCRMActions.RetrieveEntityAttributes(Service, records.EntityCollection.EntityName, true);

                    e.Result = new Dictionary<string, object>()
                    {
                        {  "records",  records.EntityCollection.Entities.ToList() },
                        {  "attribMeta", attribs }
                    };
                };

                worker.RunWorkerCompleted += (s, e) =>
                {
                    var result = e.Result as Dictionary<string, object>;

                    var records = result["records"] as List<Entity>;
                    var attribMeta = result["attribMeta"] as List<AttributeMetadata>;

                    LoadData(records, attribMeta);

                    OnProgressChanged(100, "Loading records from CRM Complete!");

                    OnLoadDataComplete();
                };

                // kick off the worker thread!
                worker.RunWorkerAsync(query);
            }
            catch (System.ServiceModel.FaultException ex)
            {
                OnNotificationMessage($"An error occured attetmpting to load the list of Entities", MessageLevel.Exception, ex);
            }
        }

        /// <summary>
        /// Override the LoadData method so that we can convert the Entity and its Attributes to ListViewItems
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        public override void LoadData<T>(List<T> items) {

            // no records, no setup
            if (items.Count == 0)
            {
                ClearData();
                return;
            }
            // retrieve the entity metadata for the entity
            var entities = items.Select(i => i as Entity).ToList();

            var attribMetadata = CommonCRMActions.RetrieveEntityAttributes(Service, entities[0].LogicalName, true);

            LoadData(entities, attribMetadata);
        }


        /// <summary>
        /// Private method that can load a list of entities and build the cols correctly
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="attribMetadata"></param>
        private void LoadData(List<Entity> entities, List<AttributeMetadata> attribMetadata)
        {
            // use the attribute metadata to set up the cols
            SetUpListViewColumns(entities, attribMetadata);

            // covnert the Entities to objects that can be displayed
            SuspendLayout();
            Enabled = false;

            ClearData();

            Refresh();

            // persist the list of list view items for the filtering
            _listViewItemsColl = new List<ListViewItem>();

            // if ListItemType is null, we are clearing things out 
            if (entities != null && entities?.Count > 0)
            {
                foreach (var record in entities)
                {
                    var col = Columns[0];
                    var attName = Columns[0].Tag.ToString();

                    // new list view item 
                    var lvItem = new ListViewItem()
                    {
                        Name = col.Tag.ToString(),
                        ImageIndex = 0,
                        StateImageIndex = 0,
                        Text = GetEntityValue(record, attName, attribMetadata.Where(a => a.LogicalName == attName).FirstOrDefault()),
                        Tag = record,  // stash the template here so we can view details later
                        Group = null
                    };

                    for (var i = 1; i < Columns.Count; i++)
                    {
                        attName = Columns[i].Tag.ToString();

                        var colVal = GetEntityValue(record, attName, attribMetadata.Where(a => a.LogicalName == attName).FirstOrDefault());
                        var subitem = new ListViewItem.ListViewSubItem(lvItem, colVal)
                        {
                            Name = attName
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

            _allItems = entities.Select(i => i as object).ToList(); ;
        }

        /// <summary>
        /// Convert the column value to a string for display
        /// Stole from Jonas CRMGridView
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="attribName"></param>
        /// <param name="attrib"></param>
        /// <returns></returns>
        private string GetEntityValue(Entity entity, string attribName, AttributeMetadata attrib)
        {
            object value = null;
            try
            {
                if (attribName == "#no")
                {   // Sequence column
                    return null;
                }
                else if (attribName == "#id")
                {
                    value = entity.Id;
                }
                else if (attribName == "#entity")
                {
                    value = entity;
                }
                else if (entity.Contains(attribName) && entity[attribName] != null)
                {
                    value = entity[attribName];

                    if (ShowFriendlyNames)
                    {
                        if (value is DateTime && ShowLocalTimes && ((DateTime)value).Kind == DateTimeKind.Utc)
                        {
                            value = ((DateTime)value).ToLocalTime();
                        }
                        if (!ValueTypeIsFriendly(value) && (attrib != null))
                        {
                            value = EntitySerializer.AttributeToString(value, attrib);
                        }
                        else
                        {
                            value = EntitySerializer.AttributeToBaseType(value).ToString();
                        }
                    }
                    else
                    {
                        value = EntitySerializer.AttributeToBaseType(value);
                    }
                }
            }
            catch
            {
                // MessageBox.Show("Attribute " + col + " failed, value: " + entity[col].ToString());
            }
            return value?.ToString();
        }

        /// <summary>
        /// I stole this from Jonas
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool ValueTypeIsFriendly(object value)
        {
            return value is Int32 || value is decimal || value is double || value is string || value is Money;
        }

        /// <summary>
        /// Helper method for setting up the list view columns for the entity and the list of Attributes
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="attributes"></param>
        internal void SetUpListViewColumns(List<Entity> entities, List<AttributeMetadata> attributes = null)
        {
            // only set them up if we have no columns
            if (Columns.Count > 0) {
                return;
            }

            if (attributes == null) {
                attributes = CommonCRMActions.RetrieveEntityAttributes(Service, entities[0].LogicalName, true);
            }

            var columns = new List<ColumnHeader>();

            // Thanks Jonas - stole from the CRMGridView, PopulateColumnsFromEntities

            // get a unique list of the Attributes returned for all records
            var attribKeys = entities
                .SelectMany(e => e.Attributes)
                .Select(a => a.Key)
                .Distinct().ToList();

            var addedColumns = new List<string>();

            foreach (var entity in entities)
            {
                foreach (var attribute in attribKeys)
                {
                    if (!entity.Attributes.ContainsKey(attribute))
                    {
                        continue;
                    }
                    if (entity[attribute] is Guid && (Guid)entity[attribute] == entity.Id)
                    {
                        continue;
                    }
                    if (addedColumns.Contains(attribute))
                    {
                        continue;
                    }
                    var value = entity[attribute];
                    if (value == null)
                    {
                        continue;
                    }
                    var type = Utility.GetValueType(value, ShowFriendlyNames);

                    var meta = attributes.Where(a => a.LogicalName.ToLower() == attribute.ToLower()).FirstOrDefault();

                    var col = new ColumnHeader()
                    {
                        Name = attribute,
                        Text = (ShowFriendlyNames && meta != null &&
                            meta.DisplayName != null &&
                            meta.DisplayName.UserLocalizedLabel != null) ? meta.DisplayName.UserLocalizedLabel.Label : attribute,
                        DisplayIndex = columns.Count,
                        Width = 150,
                        Tag = attribute
                    };

                    columns.Add(col);

                    addedColumns.Add(attribute);
                }

                // if we have added all of the distinct cols, exit the loop;
                if (addedColumns.Count == attribKeys.Count)
                {
                    break;
                }
            }
            // now reset the current list 
            Columns.Clear();
            Columns.AddRange(columns.ToArray());
        }
    }
}
