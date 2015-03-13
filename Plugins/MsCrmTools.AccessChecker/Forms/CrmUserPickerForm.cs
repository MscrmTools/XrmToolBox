using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;

namespace MsCrmTools.AccessChecker.Forms
{
    /// <summary>
    /// Class that display the control to select CRM user.
    /// </summary>
    public partial class CrmUserPickerForm : Form
    {
        #region Variables

        /// <summary>
        /// CRM access object
        /// </summary>
        readonly CrmAccess crmAccess;

        /// <summary>
        /// List of selected users
        /// </summary>
        public Dictionary<Guid, string> SelectedUsers;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class CrmUserPickerForm
        /// </summary>
        /// <param name="access"></param>
        public CrmUserPickerForm(CrmAccess access)
        {
            InitializeComponent();

            crmAccess = access;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Action when user clicks on the validation button
        /// </summary>
        private void ButtonValidateClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Action when user clicks on the cancel button
        /// </summary>
        private void ButtonCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Action when the user click on the button "Search"
        /// </summary>
        private void ButtonSearchClick(object sender, EventArgs e)
        {
            lvUsers.Items.Clear();

            List<Entity> users = crmAccess.GetUsers(txtSearchFilter.Text);

            foreach (var user in users)
            {
                var item = new ListViewItem(user.GetAttributeValue<string>("fullname")) { Tag = user.Id};
                item.SubItems.Add(user.GetAttributeValue<string>("domainname"));
                item.ImageIndex = 0;
                lvUsers.Items.Add(item);
            }
        }

        /// <summary>
        /// Action that occurs when the user select a user
        /// </summary>
        private void ListViewUsersSelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvUsers.SelectedItems.Count > 0)
            {
                SelectedUsers = new Dictionary<Guid, string>();
                SelectedUsers.Add((Guid)lvUsers.SelectedItems[0].Tag, lvUsers.SelectedItems[0].Text);
            }
        }

        /// <summary>
        /// Action when user press a keyboard key
        /// </summary>
        private void TxtSearchFilterKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                ButtonSearchClick(null, null);
            }
        }

        #endregion

        private void ListViewUsersMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (SelectedUsers.Count > 0)
            {
                ButtonValidateClick(null, null);
            }
        }

        private void LvUsersColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvUsers.Sorting = lvUsers.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lvUsers.ListViewItemSorter = new ListViewItemComparer(e.Column, lvUsers.Sorting);
        }
    }
}
