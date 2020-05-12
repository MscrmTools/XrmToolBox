using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.AppCode;

namespace XrmToolBox.Forms
{
    public partial class UserSelectionDialog : Form
    {
        private readonly IOrganizationService _service;

        private int currentAttributesColumnOrder;

        private bool moreUsers;

        public UserSelectionDialog(IOrganizationService service)
        {
            InitializeComponent();

            lvUsers.ListViewItemSorter = new ListViewItemComparer();

            _service = service;
        }

        public Entity SelectedUser { get; private set; }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            SelectedUser = (Entity)lvUsers.SelectedItems[0].Tag;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnSearch_Click(object sender, System.EventArgs e)
        {
            SearchUsers();
        }

        private void lvUsers_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == currentAttributesColumnOrder)
            {
                lvUsers.Sorting = lvUsers.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
                lvUsers.ListViewItemSorter = new ListViewItemComparer(e.Column, lvUsers.Sorting);
            }
            else
            {
                currentAttributesColumnOrder = e.Column;
                lvUsers.ListViewItemSorter = new ListViewItemComparer(e.Column, SortOrder.Ascending);
            }
        }

        private void lvUsers_DoubleClick(object sender, System.EventArgs e)
        {
            btnOK_Click(null, null);
        }

        private void SearchUsers()
        {
            var bw = new BackgroundWorker();
            bw.DoWork += (sender, e) =>
            {
                var query = new QueryExpression("systemuser")
                {
                    PageInfo = new PagingInfo
                    {
                        PageNumber = 1,
                        Count = 100
                    },
                    NoLock = true,
                    ColumnSet = new ColumnSet("fullname", "businessunitid")
                };

                if (txtSearch.Text.Length > 0)
                {
                    query.Criteria = new FilterExpression(LogicalOperator.Or)
                    {
                        Conditions =
                        {
                            new ConditionExpression("fullname", ConditionOperator.Like, $"%{txtSearch.Text}%"),
                            new ConditionExpression("domainname", ConditionOperator.Like, $"%{txtSearch.Text}%"),
                            new ConditionExpression("internalemailaddress", ConditionOperator.Like,
                                $"%{txtSearch.Text}%"),
                        }
                    };
                }

                var usersResult = _service.RetrieveMultiple(query);

                moreUsers = usersResult.MoreRecords;

                e.Result = usersResult.Entities.Select(u =>
                    new ListViewItem
                    {
                        Text = u.GetAttributeValue<string>("fullname"),
                        SubItems =
                        {
                            u.GetAttributeValue<EntityReference>("businessunitid").Name
                        },
                        Tag = u
                    }).ToList();
            };
            bw.RunWorkerCompleted += (sender, e) =>
            {
                var list = (List<ListViewItem>)e.Result;
                lvUsers.Items.Clear();
                lvUsers.Items.AddRange(list.ToArray());

                pnlMoreUsers.Visible = moreUsers;
            };
            bw.RunWorkerAsync();
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