// PROJECT : MsCrmTools.SiteMapEditor
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Tanguy.WinForm.Utilities.DelegatesHelpers;

namespace MsCrmTools.SiteMapEditor.Forms
{
    public partial class EntityPicker : Form
    {
        private List<EntityMetadata> entityCache;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class EntityPicker
        /// </summary>
        public EntityPicker(List<EntityMetadata> entityCache)
        {
            InitializeComponent();
            this.entityCache = entityCache;
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

        private void BtnCancelClick(object sender, EventArgs e)
        {
            SelectedEntity = string.Empty;
            DialogResult = DialogResult.Cancel;
            Close();
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

        /// <summary>
        /// Fills the list of entities
        /// </summary>
        private void FillEntities()
        {
            // Displays entities
            if (entityCache == null)
            {
                MessageBox.Show(this,
                    "You are not connected to an organization, so it is not possible to display a list of entities\n\nPlease use menu \"More actions\" to load entities and web resources",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }

            foreach (var emd in entityCache)
            {
                if ((emd.IsCustomizable.Value || emd.IsManaged.Value == false) && emd.DisplayName.UserLocalizedLabel != null)
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

        #endregion Methods
    }
}