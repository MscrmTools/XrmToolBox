using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MscrmTools.SyncFilterManager.Forms
{
    public partial class UserSelectionDialog : Form
    {
        public UserSelectionDialog(IOrganizationService service)
        {
            InitializeComponent();

            crmUserList1.Service = service;
        }

        public List<Entity> SelectedUsers { private set; get; }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedUsers = crmUserList1.GetSelectedUsers();

            if (SelectedUsers.Count == 0)
            {
                MessageBox.Show(this, "Please select at least one user", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}