// PROJECT : MsCrmTools.SiteMapEditor
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Tanguy.WinForm.Utilities.DelegatesHelpers;

namespace MsCrmTools.SiteMapEditor.Forms
{
    public partial class EntityPicker : Form
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of class EntityPicker
        /// </summary>
        public EntityPicker()
        {
            InitializeComponent();
            FillEntities();
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or Sets the selected entity
        /// </summary>
        public string SelectedEntity { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Fills the list of entities
        /// </summary>
        private void FillEntities()
        {
            // Checks the application cache and load it if needed
            if (SiteMapEditor.entityCache == null || SiteMapEditor.entityCache.Count == 0)
            {
                SiteMapEditor.entityCache = new List<EntityMetadata>();

                var request = new RetrieveAllEntitiesRequest
                {
                    EntityFilters = EntityFilters.Entity
                };

                var response = (RetrieveAllEntitiesResponse)SiteMapEditor.service.Execute(request);

                foreach (var emd in response.EntityMetadata)
                {
                    SiteMapEditor.entityCache.Add(emd);
                }
            }

            // Displays entities
            foreach (var emd in SiteMapEditor.entityCache)
            {
                if (emd.IsCustomizable.Value && emd.DisplayName.UserLocalizedLabel != null)
                {
                    var item = new ListViewItem
                    {
                        Text = emd.DisplayName.UserLocalizedLabel.Label,
                        Tag = emd
                    };

                    ListViewDelegates.AddItem(lvSelectedEntities, item);
                }
            }

            // Enables controls
            ListViewDelegates.Sort(lvSelectedEntities);
            ListViewDelegates.SetEnableState(lvSelectedEntities, true);
            CommonDelegates.SetEnableState(btnCancel, true);
            CommonDelegates.SetEnableState(btnValidate, true);
        }

        private void BtnValidateClick(object sender, EventArgs e)
        {
            if (lvSelectedEntities.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Please select at least one entity!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SelectedEntity = ((EntityMetadata)lvSelectedEntities.SelectedItems[0].Tag).LogicalName;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            SelectedEntity = string.Empty;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion Methods
    }
}
