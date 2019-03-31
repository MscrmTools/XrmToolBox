using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk.Metadata;

namespace XrmToolBox.CustomControls
{
    public partial class AttributeMetadataListViewControl : BoundListViewControl
    {

        private EntityMetadata _parentEntity;
        #region Public Properties
        /// <summary>
        /// Reference to the Parent Entity for the attributes
        /// </summary>
        [Category("XrmToolBox")]
        [Description("Reference to the Parent Entity from which the Attributes will be loaded")]
        [Browsable(false)]
        public EntityMetadata ParentEntity { get => _parentEntity; set => SetParentEntity(value); }

        /// <summary>
        /// The Entity Logical Name for the Parent Entity
        /// </summary>
        [Category("XrmToolBox")]
        [Description("Parent Entity LogicalName from which the Attributes will be loaded")]
        [Browsable(false)]
        public string ParentEntityLogicalName { get => _parentEntity?.LogicalName; set => SetParentEntity(value); }

        /// <summary>
        /// Reference to the currently selected Attribute
        /// </summary>
        [Category("XrmToolBox")]
        [Description("Reference to the Selected AttributeMetadata item in the ListView")]
        [Browsable(false)]
        public AttributeMetadata SelectedAttribute { get => SelectedItem as AttributeMetadata; }

        /// <summary>
        /// Reference to the currently selected Attribute
        /// </summary>
        [Category("XrmToolBox")]
        [Description("Reference to the Checked AttributeMetadata items in the ListView")]
        [Browsable(false)]
        public List<AttributeMetadata> CheckedAttributes { get => CheckedObjects?.Select(i => i as AttributeMetadata).ToList<AttributeMetadata>(); }

        /// <summary>
        /// Reference to the currently selected Attribute
        /// </summary>
        [Category("XrmToolBox")]
        [Description("Reference to all Attributes in the list")]
        [Browsable(false)]
        public List<AttributeMetadata> AllAttributes { get => 
                _allItems
                .Select(a => a as AttributeMetadata)
                .ToList();
        }

        /// <summary>
        /// Reference to all Attributes as a bindable list
        /// </summary>
        [Category("XrmToolBox")]
        [Description("Reference to all Attributes as a bindable list")]
        [Browsable(false)]
        public List<ListDisplayItem> AllAttributesBindable
        {
            get {
                var allAttr = new List<ListDisplayItem>();
                if (AllAttributes != null)
                {
                    allAttr = AllAttributes
                        .Select(attr => new ListDisplayItem(attr.SchemaName,
                                       CommonCRMActions.GetLocalizedLabel(attr.DisplayName, attr.SchemaName, LanguageCode),
                                       CommonCRMActions.GetLocalizedLabel(attr.Description, null, LanguageCode),
                                       attr))
                        .ToList();
                }
                return allAttr;
            }
        }

        /// <summary>
        /// Reference to all Attributes as a bindable list
        /// </summary>
        [Category("XrmToolBox")]
        [Description("Flag indicating whether to include Virtual Attributes in the list")]
        public bool IncludeVirtual { get; set; } = false;

        /// <summary>
        /// Set some default properties for AttributeMetadata 
        /// </summary>
        protected override void ResetPropertyList() {
            PropertyList = new List<string> { "LogicalName", "DisplayName", "AttributeTypeName", "IsCustomAttribute", "Description", "IsPrimaryId", "IsPrimaryName" };
        }
        #endregion

        public AttributeMetadataListViewControl()
        {
            // init the property list
            ResetPropertyList();

            InitializeComponent();
        }

        /// <summary>
        /// Clear out the parent entity and the related attributes
        /// </summary>
        public override void ClearData()
        {
            _parentEntity = null;
            base.ClearData();
        }

        /// <summary>
        /// Overloaded method to ensure this is not called from WorkAsync
        /// </summary>
        public override void LoadData()
        {
            // this is for developers!  If LoadData is called from within WorkAsync, the controls will be 
            // inaccessible because of the worker thread
            if (InvokeRequired)
                throw new InvalidOperationException("This method cannot be invoked from WorkAsync");

            LoadData(true);
        }

        #region Private methods 
        /// <summary>
        /// Set a reference to the parent entity for the attributes
        /// </summary>
        /// <param name="entity"></param>
        private void SetParentEntity(EntityMetadata entity)
        {
            // already cleared or never initialized, nothing to do 
            if (_parentEntity == null && entity == null)
            {
                return;
            }
            // if this is a reference to the same entity, then do not reload.
            else if (_parentEntity?.LogicalName.ToLower() == entity?.LogicalName.ToLower())
            {
                return;
            }

            // now update the parent entity reference
            _parentEntity = entity;

            // if the parent entity is null
            if (ParentEntity == null)
            {
                ClearData();
            }
            else
            {
                // see if the Attributes collection has been loaded on the entity
                if (ParentEntity.Attributes == null)
                {
                    // load the attributes for the entity
                    LoadData(false);
                }
            }
        }

        /// <summary>
        /// Do the actual load of the Attribute metadata 
        /// </summary>
        /// <param name="throwException"></param>
        protected override void LoadData(bool throwException)
        {
            if (Service == null || ParentEntityLogicalName == null)
            {
                var ex = new InvalidOperationException("The Service reference and Parent Entity must be set before loading the Entities list");

                // raise the error event and if set, throw error
                OnNotificationMessage(ex.Message, MessageLevel.Exception, ex);

                if (throwException)
                {
                    throw ex;
                }
                return;
            }

            try
            {
                OnProgressChanged(0, "Begin loading Entity Attributes from CRM");

                OnBeginLoadData();

                // load the entity metadata for the current entity logical name
                var worker = new BackgroundWorker();

                worker.DoWork += (w, e) =>
                {
                    var parentEntity = CommonCRMActions.RetrieveEntity(Service, ParentEntityLogicalName, true, new List<EntityFilters> { EntityFilters.Attributes });
                    e.Result = parentEntity;
                };

                worker.RunWorkerCompleted += (s, e) =>
                {
                    // set the parent entity reference with the loaded attributes
                    _parentEntity = e.Result as EntityMetadata;

                    OnProgressChanged(100, "Loading Entity Attributes from CRM complete!");
                    var attribs = ParentEntity.Attributes.ToList();

                    // call the base method for loading the items
                    if (IncludeVirtual)
                    {
                        LoadData(attribs);
                    }
                    else
                    {
                        LoadData(attribs.Where(a => a.AttributeType != AttributeTypeCode.Virtual).ToList());
                    }
                };

                // kick off the worker thread!
                worker.RunWorkerAsync();
            }
            catch (System.ServiceModel.FaultException ex)
            {
                OnNotificationMessage($"An error occured attetmpting to load the list of Entity Attributes", MessageLevel.Exception, ex);

                if (throwException)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Set the Logical Name of the Parent Entity so that we can load its attributes
        /// </summary>
        /// <param name="entityLogicalName"></param>
        private void SetParentEntity(string entityLogicalName)
        {
            // already cleared or never initialized, nothing to do 
            if ((_parentEntity == null) && (entityLogicalName == null))
            {
                return;
            }
            // setting existing to null, clear and disable
            else if ((_parentEntity != null) && (entityLogicalName == null))
            {
                ClearData();
            }
            else
            {
                // set up new.. 
                _parentEntity = new EntityMetadata()
                {
                    LogicalName = entityLogicalName
                };

                // set up the entity and then load
                LoadData(false);
            }
        }
        #endregion
    }
}
