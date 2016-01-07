using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MscrmTools.SyncFilterManager.AppCode;
using MscrmTools.SyncFilterManager.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace MscrmTools.SyncFilterManager.Controls
{
    public partial class CrmUserList : UserControl
    {
        private ConnectionDetail connectionDetail;
        private Panel loadingPanel;

        private IOrganizationService service;

        public CrmUserList()
        {
            InitializeComponent();
        }

        public CrmUserList(IOrganizationService service, bool selectMultipleUsers, ConnectionDetail connectionDetail)
        {
            this.service = service;
            this.connectionDetail = connectionDetail;

            InitializeComponent();

            lvUsers.MultiSelect = selectMultipleUsers;
        }

        /// <summary>
        /// EventHandler to request a connection to an organization
        /// </summary>
        public event EventHandler OnRequestConnection;

        public ConnectionDetail ConnectionDetail
        {
            set { connectionDetail = value; }
        }

        [Description("Select Multiple Users"), Category("List")]
        public bool SelectMultipleUsers
        {
            set { lvUsers.MultiSelect = value; }
            get { return lvUsers.MultiSelect; }
        }

        public IOrganizationService Service
        {
            set { service = value; }
        }

        public List<Entity> GetSelectedUsers()
        {
            return (from ListViewItem lvi in lvUsers.SelectedItems select (Entity)lvi.Tag).ToList();
        }

        internal void ReplaceUserFilters()
        {
            if (MessageBox.Show(ParentForm,
                "Are you sure you want to apply the selected user synchronization filters to other users?",
                "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            List<Entity> users = null;

            var usDialog = new UserSelectionDialog(service);
            if (usDialog.ShowDialog(this) == DialogResult.OK)
            {
                users = usDialog.SelectedUsers;
            }
            else
            {
                return;
            }

            loadingPanel = InformationPanel.GetInformationPanel(this, "Initiating...", 340, 120);

            var bwReplaceFilters = new BackgroundWorker { WorkerReportsProgress = true };
            bwReplaceFilters.DoWork += bwReplaceFilters_DoWork;
            bwReplaceFilters.ProgressChanged += bwReplaceFilters_ProgressChanged;
            bwReplaceFilters.RunWorkerCompleted += bwReplaceFilters_RunWorkerCompleted;
            bwReplaceFilters.RunWorkerAsync(new object[] { GetSelectedUsers()[0], users });
        }

        internal void Search()
        {
            LoadUsers();
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

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var searchTerm = e.Argument.ToString();

            var qe = new QueryExpression("systemuser");
            qe.ColumnSet = new ColumnSet(new[] { "systemuserid", "fullname", "businessunitid" });
            qe.PageInfo = new PagingInfo { Count = 250, PageNumber = 1, ReturnTotalRecordCount = true };
            qe.Criteria = new FilterExpression(LogicalOperator.And)
            {
                Filters =
                {
                    new FilterExpression(LogicalOperator.Or)
                    {
                        Conditions =
                        {
                            new ConditionExpression("fullname", ConditionOperator.BeginsWith,
                                searchTerm.Replace("*", "%")),
                            new ConditionExpression("domainname", ConditionOperator.BeginsWith,
                                searchTerm.Replace("*", "%")),
                            new ConditionExpression("firstname", ConditionOperator.BeginsWith,
                                searchTerm.Replace("*", "%")),
                            new ConditionExpression("lastname", ConditionOperator.BeginsWith,
                                searchTerm.Replace("*", "%"))
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

                InformationPanel.ChangeInformationPanelMessage(loadingPanel,
                    string.Format("Retrieving users ({0} %)...",
                        qe.PageInfo.PageNumber * qe.PageInfo.Count / result.TotalRecordCount * 100));

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
                MessageBox.Show(this, "Error while searching users: " + e.Error.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                var users = new List<ListViewItem>();

                foreach (var user in (List<Entity>)e.Result)
                {
                    var lvi = new ListViewItem(user.GetAttributeValue<string>("fullname")) { Tag = user };
                    lvi.SubItems.Add(user.GetAttributeValue<EntityReference>("businessunitid").Name);
                    users.Add(lvi);
                }

                lvUsers.Items.AddRange(users.ToArray());
            }
        }

        private void bwReplaceFilters_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = (BackgroundWorker)sender;
            var sourceUser = (Entity)((object[])e.Argument)[0];
            var targetUsers = (List<Entity>)((object[])e.Argument)[1];

            var rManager = new RuleManager("userquery", service, connectionDetail);
            rManager.AddRulesFromUser(sourceUser, targetUsers, worker);
        }

        private void bwReplaceFilters_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            InformationPanel.ChangeInformationPanelMessage(loadingPanel, e.UserState.ToString());
        }

        private void bwReplaceFilters_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Controls.Remove(loadingPanel);
            loadingPanel.Dispose();

            if (e.Error != null)
            {
                MessageBox.Show(this, "Error while applying filters: " + e.Error.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void chkSelectUnselectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvUsers.Items)
            {
                item.Selected = chkSelectUnselectAll.Checked;
            }
        }

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

        private void lvUsers_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvUsers.Sorting = lvUsers.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lvUsers.ListViewItemSorter = new ListViewItemComparer(e.Column, lvUsers.Sorting);
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                btnSearch_Click(null, null);
            }
        }
    }
}