// PROJECT : MsCrmTools.RoleUpdater
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Xrm.Sdk;
using MsCrmTools.RoleUpdater.DelegatesHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MsCrmTools.RoleUpdater.Controls
{
    public partial class RoleSelectionControl : UserControl
    {
        #region Variables

        /// <summary>
        /// List of existing roles
        /// </summary>
        private readonly List<Entity> roles;

        private readonly UpdateSettings settings;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class RoleSelectionControl
        /// </summary>
        /// <param name="roles">List of existing roles</param>
        /// <param name="settings"> </param>
        public RoleSelectionControl(List<Entity> roles, UpdateSettings settings)
        {
            InitializeComponent();

            this.roles = roles;
            this.settings = settings;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets the list of selected roles
        /// </summary>
        public List<Entity> SelectedRoles
        {
            get
            {
                var selectedRoles = (from ListViewItem item in lvRoles.SelectedItems select (Entity)item.Tag).ToList();

                if (selectedRoles.Count == 0)
                {
                    MessageBox.Show(this, "Please select at least one role", "Warning", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return null;
                }

                return selectedRoles;
            }
        }

        #endregion Properties

        #region Methods

        private void RoleSelectionControlLoad(object sender, EventArgs e)
        {
            // Loads roles in the list view control
            foreach (var role in roles)
            {
                var item = new ListViewItem
                {
                    Text = role["name"].ToString(),
                    Tag = role
                };

                item.SubItems.Add(((EntityReference)role["businessunitid"]).Name);

                ListViewDelegates.AddItem(lvRoles, item);

                if (settings != null && settings.SelectedRoles.Count > 0)
                {
                    if (settings.SelectedRoles.Any(x => x["name"] == role["name"]))
                        item.Selected = true;
                }
            }

            lvRoles.Columns[lvRoles.Columns.Count - 1].Width = -2;
        }

        #endregion Methods
    }
}