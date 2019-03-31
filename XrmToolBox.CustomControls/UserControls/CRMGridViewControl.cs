using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System.Drawing;
using System.Linq;

namespace XrmToolBox.CustomControls
{
    public partial class CRMGridViewControl : DataGridView
    {
        #region Private properties
        private IOrganizationService organizationService;
        private EntityCollection entityCollection;
        private bool autoRefresh = true;
        private bool showFriendlyNames = false;
        private bool showIdColumn = true;
        private bool showIndexColumn = true;
        private bool showLocalTimes = false;
        private bool entityReferenceClickable = false;
        private bool designedColumnsDetermined = false;
        private bool designedColumnsUsed = false;
        private List<string> filterColumns = null;
        private string filterText = null;
        private DataGridViewColumn[] designedColumns;
        #endregion

        #region Constructor
        public CRMGridViewControl()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            InitializeComponent();
            ReadOnly = true;
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToOrderColumns = true;
            AllowUserToResizeRows = false;
            CellClick += HandleClick;
            CellDoubleClick += HandleDoubleClick;
            CellEnter += HandleCellEnter;
            CellLeave += HandleCellLeave;
            CellMouseEnter += HandleCellMouseEnter;
            CellMouseLeave += HandleCellMouseLeave;
        }
        #endregion

        #region Published properties
        [Category("Data")]
        [DefaultValue(null)]
        public IOrganizationService OrganizationService
        {
            get { return organizationService; }
            set
            {
                organizationService = value;
                if (autoRefresh)
                {
                    Refresh();
                }
            }
        }

        [Category("Data")]
        [Description("Indicates the source of data (EntityCollection) for the CRMGridView control.")]
        public new object DataSource
        {
            get
            {
                if (entityCollection != null)
                {
                    return entityCollection;
                }
                return base.DataSource;
            }
            set
            {
                designedColumns = new DataGridViewColumn[Columns.Count];
                Columns.CopyTo(designedColumns, 0);
                base.DataSource = value;
                entityCollection = value as EntityCollection;
                if (designedColumnsDetermined && designedColumnsUsed && designedColumns != null)
                {
                    foreach (DataGridViewColumn col in designedColumns)
                    {
                        if (!Columns.Contains(col.Name))
                        {
                            col.DisplayIndex = Columns.Count;
                            Columns.Add(col);
                        }
                    }
                }
                if (entityCollection != null && autoRefresh)
                {
                    Refresh();
                }
            }
        }

        [Category("Data")]
        [DefaultValue(true)]
        [Description("Specify if content shall be automatically refreshed when entitycollection, service, flags etc are changed.")]
        public bool AutoRefresh
        {
            get { return autoRefresh; }
            set
            {
                if (value != autoRefresh)
                {
                    autoRefresh = value;
                    if (autoRefresh)
                    {
                        Refresh();
                    }
                }
            }
        }

        [Category("Data")]
        [DefaultValue(null)]
        [Description("Columns to investigate when filtering rows with FilterText. Enter datacolumn name separated by comma.")]
        public string FilterColumns
        {
            get => filterColumns == null ? string.Empty : string.Join(", ", filterColumns);
            set
            {
                filterColumns = string.IsNullOrWhiteSpace(value) ? null :
                    value.ToLowerInvariant().Split(',').Select(c => c.Trim()).Where(c => !string.IsNullOrEmpty(c)).ToList();
                if (autoRefresh)
                {
                    Refresh();
                }
            }
        }

        [Category("Data")]
        [DefaultValue(null)]
        [Description("Text to search for in FilterColumns to filter visible rows in the grid.")]
        public string FilterText
        {
            get => filterText;
            set
            {
                filterText = value.ToLowerInvariant();
                if (autoRefresh)
                {
                    Refresh();
                }
            }
        }

        [Category("CRM")]
        [DefaultValue(false)]
        [Description("True to show timestamps in local time, false to show UTC. Only valid when ShowFriendlyNames is true.")]
        public bool ShowLocalTimes
        {
            get { return showLocalTimes; }
            set
            {
                if (value != showLocalTimes)
                {
                    showLocalTimes = value && showFriendlyNames;
                    if (autoRefresh)
                    {
                        Refresh();
                    }
                }
            }
        }

        [Category("CRM")]
        [DefaultValue(false)]
        [Description("True to show friendly names, False to show logical names and guid etc.")]
        public bool ShowFriendlyNames
        {
            get { return showFriendlyNames; }
            set
            {
                if (value != showFriendlyNames)
                {
                    showFriendlyNames = value;
                    if (!showFriendlyNames)
                    {
                        showLocalTimes = false;
                    }
                    if (autoRefresh)
                    {
                        Refresh();
                    }
                }
            }
        }

        [Category("CRM")]
        [DefaultValue(true)]
        [Description("Set this to show the id of each record first in the grid.")]
        public bool ShowIdColumn
        {
            get { return showIdColumn; }
            set
            {
                if (value != ShowIdColumn)
                {
                    showIdColumn = value;
                    if (autoRefresh)
                    {
                        Refresh();
                    }
                }
            }
        }

        [Category("CRM")]
        [DefaultValue(true)]
        [Description("Set this to display a counter column first in the grid.")]
        public bool ShowIndexColumn
        {
            get { return showIndexColumn; }
            set
            {
                if (value != showIndexColumn)
                {
                    showIndexColumn = value;
                    if (autoRefresh)
                    {
                        Refresh();
                    }
                }
            }
        }

        [Category("CRM")]
        [DefaultValue(false)]
        [Description("Set this to give EntityReference cells a clickable appearance.")]
        public bool EntityReferenceClickable
        {
            get { return entityReferenceClickable; }
            set { entityReferenceClickable = value; }
        }
        #endregion

        #region Published events
        [Category("CRM")]
        public event CRMRecordEventHandler RecordClick;

        [Category("CRM")]
        public event CRMRecordEventHandler RecordDoubleClick;

        [Category("CRM")]
        public event CRMRecordEventHandler RecordEnter;

        [Category("CRM")]
        public event CRMRecordEventHandler RecordLeave;

        [Category("CRM")]
        public event CRMRecordEventHandler RecordMouseEnter;

        [Category("CRM")]
        public event CRMRecordEventHandler RecordMouseLeave;
        #endregion

        #region Public properties
        /// <summary>
        /// EntityCollection representing currently selected rows
        /// </summary>
        public EntityCollection SelectedRowRecords
        {
            get
            {
                if (entityCollection == null)
                {
                    return null;
                }
                var result = new EntityCollection();
                result.EntityName = entityCollection.EntityName;
                foreach (DataGridViewRow row in SelectedRows)
                {
                    var entity = row.Cells["#entity"].Value as Entity;
                    if (entity != null)
                    {
                        result.Entities.Add(entity);
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// EntityCollection representing all currently selected cells
        /// </summary>
        public EntityCollection SelectedCellRecords
        {
            get
            {
                if (entityCollection == null)
                {
                    return null;
                }
                var result = new EntityCollection();
                result.EntityName = entityCollection.EntityName;
                foreach (DataGridViewCell cell in SelectedCells)
                {
                    if (cell.RowIndex >= 0 && cell.RowIndex < Rows.Count)
                    {
                        var row = Rows[cell.RowIndex];
                        var entity = row.Cells["#entity"].Value as Entity;
                        if (entity != null && !result.Entities.Contains(entity))
                        {
                            result.Entities.Add(entity);
                        }
                    }
                }
                return result;
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Gets the DataSource object as requested type.
        /// For the CRMGridView the primary expected types T are EntityCollection or DataTable.
        /// </summary>
        /// <typeparam name="T">Type of the DataSource to return.</typeparam>
        /// <returns>DataSource of type T if available, otherwise null.</returns>
        public T GetDataSource<T>()
        {
            if (typeof(T) == typeof(EntityCollection))
            {
                return (T)(object)entityCollection;
            }
            return (T)base.DataSource;
        }

        /// <summary>
        /// Refresh the contents of the gridview based on associated Entities and flags
        /// </summary>
        public override void Refresh()
        {
            if (entityCollection != null)
            {
                var cols = GetTableColumns(entityCollection);
                var data = GetDataTable(entityCollection, cols);
                BindData(data);
            }
            base.Refresh();
        }
        #endregion

        #region Private event handler methods
        private void HandleClick(object sender, DataGridViewCellEventArgs e)
        {
            OnRecordEvent(GetCRMRecordEventArgs(e), RecordClick);
        }

        private void HandleDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                OnRecordEvent(GetCRMRecordEventArgs(e), RecordDoubleClick);
            }
        }

        private void HandleCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            OnRecordEvent(GetCRMRecordEventArgs(e), RecordEnter);
        }

        private void HandleCellLeave(object sender, DataGridViewCellEventArgs e)
        {
            OnRecordEvent(GetCRMRecordEventArgs(e), RecordLeave);
        }

        private void HandleCellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (entityReferenceClickable)
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                {
                    return;
                }
                var entity = GetRecordFromCellEvent(e);
                var row = Rows[e.RowIndex];
                var col = Columns[e.ColumnIndex];
                if (!entity.Contains(col.Name))
                {
                    return;
                }
                var value = entity[col.Name];
                if (value is EntityReference)
                {
                    var font = new Font(Font, FontStyle.Underline);
                    var cell = row.Cells[e.ColumnIndex];
                    cell.Style.Font = font;
                    Cursor = Cursors.Hand;
                }
            }
            OnRecordEvent(GetCRMRecordEventArgs(e), RecordMouseEnter);
        }

        private void HandleCellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (entityReferenceClickable)
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                {
                    return;
                }
                var entity = GetRecordFromCellEvent(e);
                var row = Rows[e.RowIndex];
                var col = Columns[e.ColumnIndex];
                if (!entity.Contains(col.Name))
                {
                    return;
                }
                var value = entity[col.Name];
                if (value is EntityReference)
                {
                    var font = new Font(Font, FontStyle.Regular);
                    var cell = row.Cells[e.ColumnIndex];
                    cell.Style.Font = font;
                    Cursor = Cursors.Default;
                }
            }
            OnRecordEvent(GetCRMRecordEventArgs(e), RecordMouseLeave);
        }

        private void OnRecordEvent(CRMRecordEventArgs e, CRMRecordEventHandler RecordEventHandler)
        {
            var handler = RecordEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion

        #region Private methods
        private CRMRecordEventArgs GetCRMRecordEventArgs(DataGridViewCellEventArgs e)
        {
            Entity entity = GetRecordFromCellEvent(e);
            var attribute = e.ColumnIndex >= 0 ? Columns[e.ColumnIndex].Name : string.Empty;
            var args = new CRMRecordEventArgs(e.ColumnIndex, e.RowIndex, entity, attribute);
            return args;
        }

        private Entity GetRecordFromCellEvent(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return null;
            }
            var rowno = e.RowIndex;
            var row = Rows[rowno];
            var entity = row.Cells["#entity"].Value as Entity;
            return entity;
        }

        private List<DataColumn> GetTableColumns(EntityCollection entities)
        {
            var columns = new List<DataColumn>();
            if (!designedColumnsDetermined)
            {
                designedColumnsUsed = Columns.Count > 0;
                designedColumnsDetermined = true;
                if (designedColumnsUsed)
                {
                    designedColumns = new DataGridViewColumn[Columns.Count];
                    Columns.CopyTo(designedColumns, 0);
                }
            }
            if (designedColumnsUsed)
            {
                PopulateColumnsFromDesign(entities, columns);
            }
            else
            {
                Columns.Clear();
                columns.Add(new DataColumn("#no", typeof(int)) { Caption = "#", AutoIncrement = true, AutoIncrementSeed = 1 });
                columns.Add(new DataColumn("#id", typeof(Guid)) { Caption = "Id" });
                PopulateColumnsFromEntities(entities, columns);
            }
            columns.Add(new DataColumn("#entity", typeof(Entity)));
            return columns;
        }

        private void PopulateColumnsFromDesign(EntityCollection entities, List<DataColumn> columns)
        {
            foreach (DataGridViewColumn viewcol in Columns)
            {
                if (viewcol.Name == "#entity")
                {
                    continue;
                }
                if (string.IsNullOrEmpty(viewcol.DataPropertyName))
                {
                    viewcol.DataPropertyName = viewcol.Name;
                }
                var attribute = viewcol.DataPropertyName;
                var value = GetFirstValueForAttribute(entities, attribute);
                var type = GetValueType(value);
                var dataColumn = new DataColumn(attribute, type);
                dataColumn.Caption = viewcol.HeaderText;
                var meta = MetadataHelper.GetAttribute(organizationService, entities.EntityName, attribute, value);
                dataColumn.ExtendedProperties.Add("Metadata", meta);
                dataColumn.ExtendedProperties.Add("OriginalType", value != null ? value.GetType() : null);
                columns.Add(dataColumn);
            }
        }

        private Type GetValueType(object value)
        {
            if (value == null)
            {
                return typeof(string);
            }
            if (showFriendlyNames && !ValueTypeIsFriendly(value))
            {
                return typeof(string);
            }
            var basevalue = EntitySerializer.AttributeToBaseType(value);
            if (basevalue == null)
            {
                return typeof(string);
            }
            return basevalue.GetType();
        }

        private bool ValueTypeIsFriendly(object value)
        {
            return value is Int32 || value is decimal || value is double || value is string || value is Money;
        }

        private void PopulateColumnsFromEntities(EntityCollection entities, List<DataColumn> columns)
        {
            //Stopwatch stopWatch = new Stopwatch();
            //stopWatch.Start();

            // get a unique list of the Attributes returned for all records
            var attribKeys = entities.Entities
                .SelectMany(e => e.Attributes)
                .Select(a => a.Key)
                .Distinct().ToList();

            var addedColumns = new List<string>();
            foreach (var entity in entities.Entities)
            {
                //foreach (var attribute in entity.Attributes.Keys)
                foreach (var attribute in attribKeys)
                {
                    //if (entity[attribute] == null)
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

                    var type = GetValueType(value);
                    var dataColumn = new DataColumn(attribute, type);
                    var meta = MetadataHelper.GetAttribute(organizationService, entities.EntityName, attribute, entity[attribute]);
                    dataColumn.Caption =
                        showFriendlyNames &&
                        meta != null &&
                        meta.DisplayName != null &&
                        meta.DisplayName.UserLocalizedLabel != null ? meta.DisplayName.UserLocalizedLabel.Label : attribute;
                    dataColumn.ExtendedProperties.Add("Metadata", meta);
                    dataColumn.ExtendedProperties.Add("OriginalType", value.GetType());
                    columns.Add(dataColumn);
                    addedColumns.Add(attribute);
                }

                // if we have added all of the distinct cols, exit the loop;
                if (addedColumns.Count == attribKeys.Count)
                {
                    break;
                }
            }

            //stopWatch.Stop();
            //// Get the elapsed time as a TimeSpan value.
            //TimeSpan ts = stopWatch.Elapsed;

            //// Format and display the TimeSpan value.
            //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            //    ts.Hours, ts.Minutes, ts.Seconds,
            //    ts.Milliseconds / 10);
            // MessageBox.Show(elapsedTime);
        }

        private object GetFirstValueForAttribute(EntityCollection entities, string attribute)
        {
            foreach (var entity in entities.Entities)
            {
                if (entity.Contains(attribute) && entity[attribute] != null)
                {
                    return entity[attribute];
                }
            }
            return null;
        }

        private DataTable GetDataTable(EntityCollection entities, List<DataColumn> columns)
        {
            var dTable = new DataTable();
            dTable.Columns.AddRange(columns.ToArray());
            var filteredcols = columns.Cast<DataColumn>().Where(c => filterColumns == null || filterColumns.Contains(c.ColumnName.ToLowerInvariant())).ToList();

            foreach (var entity in entities.Entities)
            {
                var dRow = dTable.NewRow();
                foreach (DataColumn column in dTable.Columns)
                {
                    var col = column.ColumnName;
                    try
                    {
                        object value = null;
                        if (col == "#no")
                        {   // Sequence column
                            continue;
                        }
                        else if (col == "#id")
                        {
                            value = entity.Id;
                        }
                        else if (col == "#entity")
                        {
                            value = entity;
                        }
                        else if (entity.Contains(col) && entity[col] != null)
                        {
                            value = entity[col];
                            if (showFriendlyNames)
                            {
                                if (value is DateTime && showLocalTimes && ((DateTime)value).Kind == DateTimeKind.Utc)
                                {
                                    value = ((DateTime)value).ToLocalTime();
                                }
                                if (!ValueTypeIsFriendly(value) && column.ExtendedProperties.ContainsKey("Metadata"))
                                {
                                    value = EntitySerializer.AttributeToString(value, column.ExtendedProperties["Metadata"] as AttributeMetadata);
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
                        if (value == null)
                        {
                            value = DBNull.Value;
                        }
                        dRow[column] = value;
                    }
                    catch
                    {
                        MessageBox.Show("Attribute " + col + " failed, value: " + entity[col].ToString());
                    }
                }
                if (FilterIncludeRow(dRow, filteredcols))
                {
                    dTable.Rows.Add(dRow);
                }
            }
            return dTable;
        }

        private bool FilterIncludeRow(DataRow dRow, List<DataColumn> filtercolumns)
        {
            if (string.IsNullOrEmpty(filterText))
            {
                return true;
            }
            foreach (var column in filtercolumns)
            {
                var cellvalue = dRow[column];
                if (cellvalue.ToString().ToLowerInvariant().Contains(filterText))
                {
                    return true;
                }
            }
            return false;
        }

        private void BindData(DataTable dTable)
        {
            SuspendLayout();
            base.DataSource = dTable;
            foreach (DataGridViewColumn col in Columns)
            {
                var datacolumn = dTable.Columns[col.DataPropertyName];
                col.HeaderText = datacolumn.Caption;
                var type = datacolumn.DataType;
                if (datacolumn.ExtendedProperties.ContainsKey("OriginalType"))
                {
                    type = datacolumn.ExtendedProperties["OriginalType"] as Type;
                }
                if (type == typeof(int) || type == typeof(decimal) || type == typeof(double) || type == typeof(Money))
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (datacolumn.ColumnName == "#no")
                {
                    col.Visible = showIndexColumn;
                }
                if (datacolumn.ColumnName == "#id")
                {
                    col.Visible = showIdColumn;
                }
                if (datacolumn.ColumnName == "#entity")
                {
                    col.Visible = false;
                }
            }
            if (AutoSizeColumnsMode != DataGridViewAutoSizeColumnsMode.None && AutoSizeColumnsMode != DataGridViewAutoSizeColumnsMode.Fill)
            {
                AutoResizeColumns(AutoSizeColumnsMode);
            }
            ResumeLayout();
        }
        #endregion
    }

    public delegate void CRMRecordEventHandler(object sender, CRMRecordEventArgs e);
}
