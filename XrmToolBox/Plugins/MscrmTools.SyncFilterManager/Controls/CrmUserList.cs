using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MscrmTools.SyncFilterManager.AppCode;
using XrmToolBox;

namespace MscrmTools.SyncFilterManager.Controls
{
    public partial class CrmUserList : UserControl
    {
        private Panel loadingPanel;

        private IOrganizationService service;

        public CrmUserList()
        {
            InitializeComponent();
        }

        public CrmUserList(IOrganizationService service, bool selectMultipleUsers)
        {
            this.service = service;

            InitializeComponent();

            lvUsers.MultiSelect = selectMultipleUsers;
        }

        public IOrganizationService Service { set { service = value; } }

        [Description("Select Multiple Users"), Category("List")]
        public bool SelectMultipleUsers { set { lvUsers.MultiSelect = value; } get { return lvUsers.MultiSelect; } }

        /// <summary>
        /// EventHandler to request a connection to an organization
        /// </summary>
        public event EventHandler OnRequestConnection;

        

        private void LoadUsers()
        {
            btnSearch.Enabled = false;
            lvUsers.Items.Clear();

            loadingPanel = InformationPanel.GetInformationPanel(this, "Retrieving users...", 340, 120);

            var bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            bw.RunWorkerAsync(txtSearch.Text);
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var searchTerm = e.Argument.ToString();

            var qe = new QueryExpression("systemuser");
            qe.ColumnSet = new ColumnSet(new[] {"systemuserid", "fullname", "businessunitid"});
            qe.PageInfo = new PagingInfo{Count = 250, PageNumber = 1, ReturnTotalRecordCount = true};
            qe.Criteria = new FilterExpression(LogicalOperator.And)
            {
                Filters =
                {
                    new FilterExpression(LogicalOperator.Or)
                    {
                        Conditions =
                        {
                            new ConditionExpression("fullname", ConditionOperator.BeginsWith, searchTerm.Replace("*", "%")),
                            new ConditionExpression("domainname", ConditionOperator.BeginsWith, searchTerm.Replace("*", "%")),
                            new ConditionExpression("firstname", ConditionOperator.BeginsWith, searchTerm.Replace("*", "%")),
                            new ConditionExpression("lastname", ConditionOperator.BeginsWith, searchTerm.Replace("*", "%"))
                        }
                    },
                    new FilterExpression
                    {
                        Conditions = {new ConditionExpression("isdisabled", ConditionOperator.Equal, false)}
                    }
                }
            };

            EntityCollection result;
            var results = new List<Entity>();
            do
            {
                result = service.RetrieveMultiple(qe);
                results.AddRange(result.Entities);

                InformationPanel.ChangeInformationPanelMessage(loadingPanel, string.Format("Retrieving users ({0} %)...", qe.PageInfo.PageNumber * qe.PageInfo.Count / result.TotalRecordCount * 100));

                qe.PageInfo.PageNumber++;

            } while (result.MoreRecords);

            e.Result = results;
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnSearch.Enabled = true;

            Controls.Remove(loadingPanel);
            loadingPanel.Dispose();

            if (e.Error != null)
            {
                MessageBox.Show(this, "Error while searching users: " + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var users = new List<ListViewItem>();

                foreach (var user in (List<Entity>)e.Result)
                {
                    var lvi = new ListViewItem(user.GetAttributeValue<string>("fullname")) {Tag = user};
                    lvi.SubItems.Add(user.GetAttributeValue<EntityReference>("businessunitid").Name);
                    users.Add(lvi);
                }

                lvUsers.Items.AddRange(users.ToArray());
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (service == null)
            {
                OnRequestConnection(this, null);
            }
            else
            {
                LoadUsers();
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Return)
            {
                btnSearch_Click(null, null);
            }
        }

        public List<Entity> GetSelectedUsers()
        {
            return (from ListViewItem lvi in lvUsers.SelectedItems select (Entity) lvi.Tag).ToList();
        }

        private void lvUsers_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvUsers.Sorting = lvUsers.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lvUsers.ListViewItemSorter = new ListViewItemComparer(e.Column, lvUsers.Sorting);
        }

        private void chkSelectUnselectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvUsers.Items)
            {
                item.Selected = chkSelectUnselectAll.Checked;
            }
        }

        internal void Search()
        {
            LoadUsers();
        }
    }
}
