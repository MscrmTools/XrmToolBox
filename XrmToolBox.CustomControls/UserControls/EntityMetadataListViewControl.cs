using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Xrm.Sdk.Metadata;

namespace XrmToolBox.CustomControls
{
    /// <summary>
    /// Bound ListView control for EntityMetadata
    /// </summary>
    public partial class EntityMetadataListViewControl : BoundListViewControl
    {
        #region Private items
        private ConfigurationInfo _config = null;
        private string _solutionFilter = null;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public EntityMetadataListViewControl()
        {
            InitializeComponent();

            // set up some default values and uI state
            _config = new ConfigurationInfo();

            // init the col defs items
            // ResetListViewColDefs();
        }

        #region Runtime Properties
        /// <summary>
        /// List of all checked EntityMetadata objects in the ListView
        /// </summary>
        [Category("XrmToolBox")]
        [DisplayName("Checked Entities List")]
        [Description("List of all Entities that are checked in the control.")]
        [Browsable(false)]
        public List<EntityMetadata> CheckedEntities { get => CheckedObjects?.Select(i => i as EntityMetadata).ToList<EntityMetadata>(); }

        /// <summary>
        /// The currently selected EntityMetadata object in the ListView
        /// </summary>
        [Category("XrmToolBox")]
        [DisplayName("Selected Entity")]
        [Description("The Entity that is currently selected the control.")]
        [Browsable(false)]
        public EntityMetadata SelectedEntity { get => SelectedItem as EntityMetadata; }

        /// <summary>
        /// List of all loaded EntityMetadata objects for the current connection
        /// </summary>
        [Category("XrmToolBox")]
        [DisplayName("All Entities List")]
        [Description("List of all Entities that have been loaded.")]
        [Browsable(false)]
        public List<EntityMetadata> AllEntities
        {
            get => AllItems?.Select(item => item as EntityMetadata).ToList();
        }


        /// <summary>
        /// Set some default properties for AttributeMetadata 
        /// </summary>
        protected override void ResetPropertyList()
        {
            PropertyList = new List<string> { "LogicalName", "DisplayName", "IsManaged", "Description", "IsCustomEntity" };
        }
        #endregion

        #region Public properties - Entities 
        /// <summary>
        /// Defines which Entity types should be loaded on retrieve.
        /// </summary>
        [Category("XrmToolBox")]
        [DisplayName("Solution Filter")]
        [Description("Specifies a Solution Unique Name filter to be used when retrieving Entities.")]
        public string SolutionFilter
        {
            get => _solutionFilter;
            set => _solutionFilter = value;
        }

        /// <summary>
        /// List of Entities to excluded upon retrieval.
        /// </summary>
        [Category("XrmToolBox")]
        [DisplayName("Entity Filters")]
        [Description("List of Entities to excluded upon retrieval.")]
        [Browsable(false)]
        public List<FilterInfo> EntityFilters
        {
            get { return _config.EntityFilters; }
            internal set { _config.EntityFilters = value; }
        }

        /// <summary>
        /// List of EntityFilters to be applied on the 
        /// </summary>
        [Category("XrmToolBox")]
        [DisplayName("Entity Request Filters")]
        [Description("List of EntityFilters applied to Entity retrieval. This is the EntityFilter structure passed with the RetrieveAllEntitiesRequest")]
        public List<EntityFilters> EntityRequestFilters
        {
            get { return _config.EntityRequestFilters; }
            internal set { _config.EntityRequestFilters = value; }
        }

        /// <summary>
        /// Flag indicating whether to retrieve the metadata that has not been published
        /// </summary>
        [Category("XrmToolBox")]
        [DisplayName("Retrieve As If Published")]
        [Description("Flag indicating whether to retrieve the metadata that has not been published")]
        public bool RetrieveAsIfPublished
        {
            get { return _config.RetrieveAsIfPublished; }
            set { _config.RetrieveAsIfPublished = value; }
        }
        /// <summary>
        /// Load the Attributes using the current IOrganizationService.
        /// The call is asynchronous and will leverage the WorkAsync object on the parent XrmToolBox control
        /// </summary>
        public override void LoadData()
        {
            // this is for developers!  If LoadData is called from within WorkAsync, the controls will be 
            // inaccessible because of the worker thread
            if (this.InvokeRequired)
                throw new InvalidOperationException("This method cannot be invoked from WorkAsync");

            LoadData(true);
        }
        #endregion

        #region Main methods 
        /// <summary>
        /// Private method that will rethrow an Exception if specified in the parameter.
        /// This is meant to allow for external programmatic calls to load vs those from the built in controls
        /// </summary>
        /// <param name="throwException">Flag indicating whether to rethrow a captured exception</param>
        protected override void LoadData(bool throwException)
        {
            base.LoadData(throwException);

            if (Service == null) {
                return;
            }

            try
            {
                OnProgressChanged(0, "Loading Entities from CRM");

                OnBeginLoadData();

                // first clear out all data currently loaded
                ClearData();

                var worker = new BackgroundWorker();

                worker.DoWork += (w, e) => {

                    var entities = new List<EntityMetadata>();

                    if (SolutionFilter != null)
                    {
                        entities = CommonCRMActions.RetrieveEntitiesForSolution(Service, SolutionFilter);
                    }
                    else
                    {
                        entities = CommonCRMActions.RetrieveAllEntities(Service, _config);
                    }
                    e.Result = entities;
                };

                worker.RunWorkerCompleted += (s, e) =>
                {
                    var entities = e.Result as List<EntityMetadata>;

                    // reset the list of all entities 
                    var allEntities = new List<EntityMetadata>();

                    double counter = 0;
                    double total = entities.Count;

                    OnProgressChanged(1, $"{entities.Count} Entities loaded");

                    foreach (var entity in entities)
                    {
                        counter++;
                        if (counter % 50 == 0)
                        {
                            OnProgressChanged((int)(100 * counter / total), "Loading Entities ...");
                        }
                        // filter based on configuration settings
                        if (_config.FilterEntity(entity.LogicalName))
                        {
                            continue;
                        }
                        // see if we are filtering by system and custom
                        else if (_config.EntityTypes != EnumEntityTypes.BothCustomAndSystem)
                        {
                            if ((_config.EntityTypes == EnumEntityTypes.Custom) && (!entity.IsCustomEntity.Value))
                            {
                                continue;
                            }
                            else if ((_config.EntityTypes == EnumEntityTypes.System) && (entity.IsCustomEntity.Value))
                            {
                                continue;
                            }
                        }

                        allEntities.Add(entity);
                    }

                    // now that the entities are loaded, populate the list view.
                    LoadData<EntityMetadata>(allEntities);

                    OnProgressChanged(100, "Loading Entities from CRM Complete!");
                };

                // kick off the worker thread!
                worker.RunWorkerAsync();
            }
            catch (System.ServiceModel.FaultException ex)
            {
                OnNotificationMessage($"An error occured attetmpting to load the list of Entities", MessageLevel.Exception, ex);

                if (throwException)
                {
                    throw ex;
                }
            }
        }
        #endregion
    }
}
