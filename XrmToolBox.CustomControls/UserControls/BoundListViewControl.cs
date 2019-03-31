using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using System.Reflection;

namespace XrmToolBox.CustomControls
{
    /// <summary>
    /// Base class that will bind a generic object to the ListView
    /// </summary>
    public partial class BoundListViewControl : ListView, IXrmToolBoxControl
    {
        /// <summary>
        /// 
        /// </summary>
        public BoundListViewControl()
        {
            InitializeComponent();
        }

        #region Internal/private/protected properties for use by this and possibly derived classes
        /// <summary>
        /// Collection of ListViewColumn Defs
        /// </summary>
        internal protected List<ListViewItem> _listViewItemsColl = null;
        private string _filterString = null;

        private bool _performingBulkSelection = false; // let's keep the listview from flickering and crashing

        #region IXrmToolboxControl
        private IOrganizationService _service = null;
        #endregion

        /// <summary>
        /// internal list of all items 
        /// </summary>
        internal protected List<object> _allItems = new List<object>();

        /// <summary>
        /// .NET type for the bound list.  This will be used to retrieve property values 
        /// </summary>
        private Type _listItemType = null;
        private ColumnHeaderAutoResizeStyle _autoResizeStyle = ColumnHeaderAutoResizeStyle.None;
        
        #endregion

        #region Runtime properties

        /// <summary>
        /// Mode for automatically resizing Column in the ListView.  Autosize may cause some flickering for ListView many columns
        /// </summary>
        [Category("XrmToolBox")]
        [DisplayName("Auto Resize Column Style")]
        [Description("Mode for automatically resizing Column in the ListView.  Autosize may cause some flickering for ListView many columns")]
        public ColumnHeaderAutoResizeStyle AutosizeColumns
        {
            get { return _autoResizeStyle; }
            set {
                _autoResizeStyle = value;
                AutoResizeColumns(AutosizeColumns);
            }
        }
        /// <summary>
        /// Flag indicating whether to automatically load data when the Service connection is set or updated.
        /// </summary>
        [Category("XrmToolBox")]
        [DisplayName("Automatically Load Data")]
        [Description("Flag indicating whether to automatically load data when the Service connection is set or updated.")]
        public bool AutoLoadData { get; set; }
        
        /// <summary>
        /// Flag indicating whether to display Checkboxes in the ListView control
        /// </summary>
        [Category("XrmToolBox")]
        [DisplayName("Display Checkboxes")]
        [Description("Flag indicating whether to display Checkboxes in the ListView control")]
        public bool DisplayCheckBoxes
        {
            get { return CheckBoxes; }
            set {
                // uncheck everything if no checkboxes
                if (value == false)
                {
                    CheckAllNone(false);
                }
                _performingBulkSelection = true;
                SuspendLayout();

                CheckBoxes = value;

                ResumeLayout();
                _performingBulkSelection = false;
            }
        }

        /// <summary>
        /// Filter string for the toolbar 
        /// </summary>
        [Category("XrmToolBox")]
        [DisplayName("List Filter String")]
        [Description("Filter string applied to the main ListView control for the loaded Item List.")]
        public string ListFilterString
        {
            get {
                var filter = _filterString;
                return (filter != null) ? filter.Trim() : filter;
            }
            internal set {
                if (DesignMode) return;
                _filterString = value?.Trim();
            }
        }

        List<string> _propertyList = new List<string>();

        [Category("XrmToolBox")]
        [Description("List of the object properties to be loaded when creating the columns")]
        public List<string> PropertyList { get => _propertyList; set => _propertyList = value; }

        protected virtual void ResetPropertyList()
        {
            _propertyList = new List<string>();
        }
        protected virtual bool ShouldPropertyList()
        {
            return true;
        }

        #endregion

        #region Protected Internal properties/methods
        // These will be extended by derived classes
        /// <summary>
        /// .NET Type for the bound item. Used to set up columns and retrieve property values
        /// </summary>
        protected internal Type ListItemType
        {
            get { return _listItemType; }
            private set {
                _listItemType = value;
                // only set up columns col defs not defined
                SetUpListViewColumns();
            }
        }

        /// <summary>
        /// Helper method for setting the internal list of items bound to the ListView.
        /// Allows us to specify the object Type being bound
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        public virtual void LoadData<T>(List<T> items)
        {
            _allItems = items.ConvertAll(new Converter<T, object>((item) => { return item as object; }));

            ListItemType = typeof(T);

            // set the ListItemType from the list of items if not set
            SetUpListViewColumns();

            // load up the items into the list view.
            PopulateListView();

            OnLoadDataComplete();
        }

        /// <summary>
        /// Internal collection of items bound to the list view.
        /// </summary>
        protected internal List<object> AllItems
        {
            get { return _allItems; }
        }

        /// <summary>
        /// The currently selected object in the ListView
        /// </summary>
        protected internal object SelectedItem { get; private set; } = null;

        /// <summary>
        /// List of all checked items in the ListView
        /// </summary>
        protected internal List<object> CheckedObjects { get; private set; } = null;

        #endregion

        #region IXrmToolBoxControl Properties
        /// <summary>
        /// Reference to the IOrganizationService object
        /// </summary>
        [Category("XrmToolBox")]
        [DisplayName("Organization Service")]
        [Description("Reference to the IOrganizationService object.")]
        [Browsable(false)]
        public IOrganizationService Service { get => _service; set => UpdateConnection(value); }

        /// <summary>
        /// Language code to be used when displaying labels or other strings from CRM
        /// </summary>
        [Category("XrmToolBox")]
        [DisplayName("Language Code")]
        [Description("Language code for this control.")]
        public int LanguageCode { get; set; } = 1033;
        #endregion

        #region IXrmToolBoxControl Event Definitions

        #region Event Definitions
        /// <summary>
        /// Event fired when the progress changes for an async event 
        /// </summary>
        [Category("XrmToolBox")]
        [Description("Event fired when the progress changes for an async event")]
        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;

        /// <summary>
        /// Event that fires when the LoadData method begins
        /// </summary>
        [Category("XrmToolBox")]
        [Description("Event that fires when the LoadData method begins")]
        public virtual event EventHandler BeginLoadData;

        /// <summary>
        /// Event that fires when <see cref="LoadData"/>() completes
        /// </summary>
        [Category("XrmToolBox")]
        [Description("Event that fires when LoadData() completes")]
        public event EventHandler LoadDataComplete;

        /// <summary>
        /// Event that fires when ClearData() starts
        /// </summary>
        [Category("XrmToolBox")]
        [Description("Event that fires when ClearData() begins for the Dropdown")]
        public event EventHandler BeginClearData;

        /// <summary>
        /// Event that fires when ClearData() completes
        /// </summary>
        [Category("XrmToolBox")]
        [Description("Event that fires when ClearData() completes")]
        public event EventHandler ClearDataComplete;

        /// <summary>
        /// Event that fires when Close() begins
        /// </summary>
        [Category("XrmToolBox")]
        [Description("Event that fires when Close() begins")]
        public event EventHandler BeginClose;

        /// <summary>
        /// Event that fires when Close() completes
        /// </summary>
        [Category("XrmToolBox")]
        [Description("Event that fires when Close() completes")]
        public event EventHandler CloseComplete;

        /// <summary>
        /// A Notificaiton Has been raised
        /// </summary>
        [Category("XrmToolBox")]
        [Description("Event that fires when a Notification is raised")]
        public virtual event EventHandler<NotificationEventArgs> NotificationMessage;
        #endregion

        #region Methods
        /// <summary>
        /// Handle the updated connection 
        /// </summary>
        /// <param name="newService">Reference to the new IOrganizationService</param>
        public virtual void UpdateConnection(IOrganizationService newService)
        {
            _service = newService;

            // if the service had previously been set, then clear things out
            if (Service != null)
            {
                ClearData();
            }

            // if the auto load is set, now is the time to reload!
            if (AutoLoadData && (Service != null))
            {
                LoadData();
            }
        }

        /// <summary>
        /// Close has been called
        /// </summary>
        public virtual void Close()
        {
            OnBeginClose();

            _service = null;

            // use this. to ensure that any overrides are called
            ClearData();

            OnCloseComplete();
        }

        /// <summary>
        /// Load the data relevant to the control
        /// </summary>
        public virtual void LoadData()
        {
            OnLoadDataComplete();
        }
        /// <summary>
        /// Clear out the saved items list and update the ListView
        /// </summary>
        public virtual void ClearData()
        {
            // clear out list view list, collection of items, etc.
            _listViewItemsColl?.Clear();

            CheckedObjects = new List<object>();

            _allItems = new List<object>();

            Items.Clear();

            SelectedItemChanged?.Invoke(this, new EventArgs());
        }

        #endregion

        #region Event helpers
        /// <summary>
        /// Raises the ProgressChanged event to all listeners
        /// </summary>
        /// <param name="progressPercent">Progress percentage</param>
        /// <param name="message">Additional message with progress</param>
        protected void OnProgressChanged(int progressPercent, string message)
        {
            // if (this.InvokeRequired)  return;
            ProgressChanged?.Invoke(this, new ProgressChangedEventArgs(progressPercent, message));
        }

        /// <summary>
        /// Fires the NotificationMessgae event to all listeners
        /// </summary>
        /// <param name="message">Text message for client</param>
        /// <param name="level">MessageLevel for this event</param>
        /// <param name="ex">Optional Exception object</param>
        protected void OnNotificationMessage(string message, MessageLevel level, Exception ex = null)
        {
            // if (this.InvokeRequired) return;
            NotificationMessage?.Invoke(this, new NotificationEventArgs(message, level, ex));
        }

        /// <summary>
        /// Fires the BeginLoadData event to all listeners
        /// </summary>
        protected void OnBeginLoadData()
        {
            // if (this.InvokeRequired) return;
            BeginLoadData?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Fires the LoadDataComplete event for all listeners
        /// </summary>
        protected void OnLoadDataComplete()
        {
            // if (this.InvokeRequired) return;
            LoadDataComplete?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Fires the BeginClearData event to all listeners
        /// </summary>
        protected void OnBeginClearData()
        {
            // if (this.InvokeRequired) return;
            BeginClearData?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Fires the ClearDataComplete event to all listeners
        /// </summary>
        protected void OnClearDataComplete()
        {
            // if (this.InvokeRequired) return;
            ClearDataComplete?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Fires the BeginClose event to all listeners
        /// </summary>
        protected void OnBeginClose()
        {
            // if (this.InvokeRequired) return;
            BeginClose?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Fires the CloseComplete event to all listeners
        /// </summary>
        protected void OnCloseComplete()
        {
            // if (this.InvokeRequired) return;
            CloseComplete?.Invoke(this, new EventArgs());
        }
        #endregion

        #endregion

        #region ListView specific Event definition
        /// <summary>
        /// Event that fires when the Selected Item changes
        /// </summary>
        [Category("XrmToolBox")]
        [Description("Event that fires when the Selected Item changes")]
        public event EventHandler SelectedItemChanged;

        /// <summary>
        /// Event that fires when the list of Checked Items changes
        /// </summary>
        [Category("XrmToolBox")]
        [Description("Event that fires when the list of Checked Items changes")]
        public event EventHandler CheckedItemsChanged;

        /// <summary>
        /// Event that fires when FilterList() completes
        /// </summary>
        [Category("XrmToolBox")]
        [Description("Event that fires when the list of Checked Items changes")]
        public event EventHandler FilterListComplete;


        #endregion

        #region ListView Methods/Events
        /// <summary>
        /// Handle the ListView Item Checked event and set the selected list of items
        /// </summary>
        /// <param name="sender">event sender object</param>
        /// <param name="e">event args object</param>
        private void ListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (!_performingBulkSelection)
            {
                UpdateSelectedItemsList();
            }
        }

        /// <summary>
        /// Handle the item selection change for the list view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            // clear current selection 
            SelectedItem = null;

            if (!e.IsSelected)
                return;

            // grab the current selected object from the ListItem tag.
            SelectedItem = e.Item.Tag;

            SelectedItemChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Handle the column click for the list view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            SortList(e.Column);
        }

        /// <summary>
        /// Handle keyboard select all / none
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_KeyUp(object sender, KeyEventArgs e)
        {
            if (!e.Handled)
            {
                if (e.Control)
                {
                    if (e.KeyCode == Keys.A)
                    {
                        CheckAllNone(true);
                    }
                    else if (e.KeyCode == Keys.D)
                    {
                        CheckAllNone(false);
                    }
                }
            }
        }

        /// <summary>
        /// When the ListView is not active, the selected row is really hard to see. Set the highlight manually on enter/leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_HighLightSelected(object sender, EventArgs e)
        {
            SetSelectedHighlight(SystemColors.Highlight, SystemColors.HighlightText);
        }

        /// <summary>
        /// When the ListView is not active, the selected row is really hard to see. Set the highlight manually on enter/leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_ClearHighLight(object sender, EventArgs e)
        {
            SetSelectedHighlight(SystemColors.Window, SystemColors.WindowText);
        }
        /// <summary>
        /// Helper method for setting the fore and back color for the selected row
        /// </summary>
        /// <param name="backColor"></param>
        /// <param name="foreColor"></param>
        private void SetSelectedHighlight(Color backColor, Color foreColor)
        {
            SuspendLayout();

            if (SelectedItems.Count > 0)
            {
                var selItem = SelectedItems[0];
                selItem.ForeColor = foreColor;
                selItem.BackColor = backColor;
            }
            ResumeLayout();
        }
        /// <summary>
        /// Internal method that allows us to decide whether to throw an exception to the user, or to simply add notification
        /// </summary>
        /// <param name="throwException"></param>
        protected virtual void LoadData(bool throwException) {

            if (Service == null)
            {
                var ex = new InvalidOperationException("The Service reference must be set before loading the Entities list");

                // raise the error event and if set, throw error
                OnNotificationMessage(ex.Message, MessageLevel.Exception, ex);

                if (throwException)
                {
                    throw ex;
                }
                return;
            }
        }

        /// <summary>
        /// Sort the current list of items in the ListView
        /// </summary>
        /// <param name="sortColumn">ListView column index to be sorted</param>
        /// <param name="sortOrder">Sort order for the selected column</param>
        public void SortList(int sortColumn, SortOrder? sortOrder = null)
        {
            SuspendLayout();
            // toggle the sort order if not passed as a param
            if (sortOrder == null)
            {
                sortOrder = (Sorting == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;
            }

            _performingBulkSelection = true;

            // update the main list and save the values to properties
            Sorting = sortOrder.Value;

            // now apply the sorter helper 
            ListViewItemSorter = new ListViewItemComparer(sortColumn, Sorting);

            _performingBulkSelection = false;

            ResumeLayout();
        }

        /// <summary>
        /// Update the list of selected items based on the list of Checked items in the ListView
        /// Maintain this list dynamically because of how filtering has been implemented.  
        /// </summary>
        private void UpdateSelectedItemsList()
        {
            if (_performingBulkSelection)
            {
                return;
            }

            if (CheckedObjects == null)
            {
                CheckedObjects = new List<object>();
            }

            if (CheckedItems.Count == 0)
            {
                CheckedObjects.Clear();
            }
            else
            {
                foreach (ListViewItem listItem in Items)
                {
                    var item = listItem.Tag;
                    if (listItem.Checked)
                    {
                        // if not already added, add the checked item
                        if (!CheckedObjects.Contains(item))
                        {
                            CheckedObjects.Add(item);
                        }
                    }
                    else
                    {
                        // if already added, then remove it
                        if (CheckedObjects.Contains(item))
                        {
                            CheckedObjects.Remove(item);
                        }
                    }
                }
            }
            CheckedItemsChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Check all items in the ListView
        /// </summary>
        public void CheckAll()
        {
            CheckAllNone(true);
        }

        /// <summary>
        /// Uncheck all items in the ListView
        /// </summary>
        public void CheckNone()
        {
            CheckAllNone(false);
        }

        /// <summary>
        /// Toggle all or none checked in the main list view
        /// </summary>
        /// <param name="checkAll">flag indicating whether to check all items in the ListView</param>
        private void CheckAllNone(bool checkAll)
        {
            if (CheckedObjects == null)
                return;

            _performingBulkSelection = true;

            SuspendLayout();

            // if check all and we have checkboxes enabled, then do some work
            if (checkAll && CheckBoxes)
            {
                foreach (ListViewItem item in Items)
                {
                    item.Checked = true;
                }
            }
            else
            {
                foreach (ListViewItem item in CheckedObjects)
                {
                    item.Checked = false;
                }
            }
            ResumeLayout();

            _performingBulkSelection = false;

            // now that we have an updated list view, udpate the list of selected items
            UpdateSelectedItemsList();
        }

        /// <summary>
        /// Filter the list using the text in the text box.
        /// </summary>
        private void FilterList()
        {
            List<ListViewItem> newList = null;

            _performingBulkSelection = true;

            SuspendLayout();

            // filter the cols 
            if (_filterString.Length > 3)
            {
                // get the filter cols... this allows us to check the correct col for the value
                // if none found, default to first col
                // var filterCols = ListViewColDefs.Where(c => c.IsFilterColumn == true).ToList();
                //if (filterCols.Count == 0)
                //{
                //    filterCols = ListViewColDefs.OrderBy(c => c.Order).Take(1).ToList();
                //}

                newList = new List<ListViewItem>();
                foreach (ColumnHeader col in Columns)
                {
                    // filter the master list and bind it to the list view. Col 0 is the text while the rest are subitems/cols
                    var curr = _listViewItemsColl
                                .Where(i => (col.DisplayIndex == 0) ?
                                    i.Text.ToLower().Contains(_filterString) :
                                    i.SubItems[col.Name].Text.ToLower().Contains(_filterString)
                                );

                    // add to the current list
                    newList.AddRange(curr.Except(newList));
                }
            }
            else
            {
                if (Items.Count != _listViewItemsColl.Count)
                {
                    newList = _listViewItemsColl;
                }
            }

            // if we have a new list to be set, clear and reset groups
            if (newList != null)
            {
                Items.Clear();
                Items.AddRange(newList.ToArray<ListViewItem>());

                var props = ListItemType.GetProperties().ToDictionary(p => p.Name, p => p);

                // now that we have an updated list view, udpate the list of selected items
                UpdateSelectedItemsList();
            }
            _performingBulkSelection = false;

            ResumeLayout();

            FilterListComplete?.Invoke(this, new EventArgs());
        }
        #endregion

        #region Main Methods
        /// <summary>
        /// Load the Items into the list view, binding the columns based on the control properties
        /// </summary>
        internal virtual void PopulateListView()
        {
            SuspendLayout();
            Enabled = false;
            Items.Clear();
            Refresh();

            // persist the list of list view items for the filtering
            _listViewItemsColl = new List<ListViewItem>();

            // if ListItemType is null, we are clearing things out 
            if (_allItems != null && _allItems?.Count > 0)
            {
                var cols = Columns;
                // get the property definitions from the Type
                var props = ListItemType.GetProperties().ToDictionary(p => p.Name, p => p);

                foreach (var item in _allItems)
                {
                    var col = cols[0];

                    var prop = props
                        .Where(p => p.Key.ToLower() == col.Name.ToLower() || 
                        p.Key.ToLower() == col.Tag.ToString().ToLower())
                        .Select(p => p.Value)
                        .FirstOrDefault();
                    
                    var text = Utility.GetPropertyValue<string>(item, prop);

                    // new list view item 
                    var lvItem = new ListViewItem()
                    {
                        Name = cols[0].Name,
                        ImageIndex = 0,
                        StateImageIndex = 0,
                        Text = text,
                        Tag = item
                    };

                    for (var i = 1; i < cols.Count; i++)
                    {
                        col = cols[i];

                        prop = props
                            .Where(p => p.Key.ToLower() == col.Name.ToLower() ||
                            p.Key.ToLower() == col.Tag.ToString().ToLower())
                            .Select(p => p.Value)
                            .FirstOrDefault();

                        var colVal = Utility.GetPropertyValue<string>(item, prop);
                        var subitem = new ListViewItem.ListViewSubItem(lvItem, colVal)
                        {
                            Name = prop.Name
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
        }
        /// <summary>
        /// Set up the ListView Columns either using the Col Def list provided, 
        /// or generically with the object property definitions from object metadata 
        /// </summary>
        internal virtual void SetUpListViewColumns()
        {
            if (Columns.Count > 0)
                return;

            var cols = new List<ColumnHeader>();

            if (ListItemType != null)
            {
                // render all of the properties for the bound list object type
                PropertyInfo[] props = ListItemType.GetProperties();

                foreach (PropertyInfo p in props)
                {
                    if (PropertyList.Where(i => i.ToLower() == p.Name.ToLower()).Any())
                    {
                        cols.Add(new ColumnHeader()
                        {
                            Name = p.Name,
                            Text = p.Name,
                            Tag = p.Name,
                            Width = 100
                        });
                    }
                }
            }

            // if the two are the same, then no need to reset
            if (Columns.Count == cols.Count)
            {
                var listCols = Columns.Cast<ColumnHeader>().Select(c => c.Name);
                if (cols.Select(c => c.Name).SequenceEqual(listCols))
                {
                    return;
                }
            }

            SuspendLayout();
            Columns.Clear();

            Columns.AddRange(cols.ToArray());
            ResumeLayout();
        }
        #endregion

    }
}
