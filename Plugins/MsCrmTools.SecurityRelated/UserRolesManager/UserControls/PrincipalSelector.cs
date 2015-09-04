using Microsoft.Xrm.Sdk;
using MsCrmTools.UserRolesManager.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MsCrmTools.UserRolesManager.UserControls
{
    public partial class PrincipalSelector : UserControl
    {
        private int currentColumnOrder;
        private IOrganizationService service;

        public PrincipalSelector()
        {
            InitializeComponent();

            cbbType.SelectedIndexChanged -= cbbType_SelectedIndexChanged;
            cbbType.SelectedIndex = 0;
            cbbType.SelectedIndexChanged += cbbType_SelectedIndexChanged;
        }

        public List<Entity> SelectedItems
        {
            get { return lvUsersAndTeams.SelectedItems.Cast<ListViewItem>().Select(e => (Entity)e.Tag).ToList(); }
        }

        public IOrganizationService Service
        {
            set
            {
                service = value;
                cbbType_SelectedIndexChanged(null, null);
            }
        }

        public void LoadViews()
        {
            cbbType_SelectedIndexChanged(null, null);
        }

        private void cbbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (service == null)
            {
                throw new Exception("IOrganization service is not initialized for this control");
            }

            var vManager = new ViewManager(service);
            var items = new List<ViewItem>();
            lvUsersAndTeams.Columns.Clear();

            switch (cbbType.SelectedIndex)
            {
                case 0:
                    {
                        items = vManager.RetrieveViews("systemuser");

                        lvUsersAndTeams.Columns.AddRange(new[]
                    {
                        new ColumnHeader{Text = "Last name", Width = 150},
                        new ColumnHeader{Text = "First name", Width = 150},
                        new ColumnHeader{Text = "Business unit", Width = 150}
                    });
                    }
                    break;

                case 1:
                    {
                        items = vManager.RetrieveViews("team");

                        lvUsersAndTeams.Columns.AddRange(new[]
                    {
                        new ColumnHeader{Text = "Name", Width = 150},
                        new ColumnHeader{Text = "Business unit", Width = 150}
                    });
                    }
                    break;
            }

            if (items != null)
            {
                cbbViews.SelectedIndexChanged -= cbbViews_SelectedIndexChanged;
                cbbViews.Items.Clear();
                cbbViews.Items.AddRange(items.ToArray());
                cbbViews.SelectedIndexChanged += cbbViews_SelectedIndexChanged;
                cbbViews.SelectedIndex = 0;
            }
        }

        private void cbbViews_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvUsersAndTeams.Items.Clear();
            var viewItem = (ViewItem)cbbViews.SelectedItem;

            var entity = QueryHelper.GetItems(viewItem.FetchXml, service);

            if (entity.EntityName == "systemuser")
            {
                lvUsersAndTeams.Items.AddRange(entity.Entities.ToList().Select(record => new ListViewItem
                {
                    Text = record.GetAttributeValue<string>("lastname"),
                    ImageIndex = 0,
                    StateImageIndex = 0,
                    Tag = record,
                    SubItems =
                    {
                        record.GetAttributeValue<string>("firstname"),
                        record.GetAttributeValue<EntityReference>("businessunitid").Name
                    }
                }).ToArray());
            }
            else if (entity.EntityName == "team")
            {
                lvUsersAndTeams.Items.AddRange(entity.Entities.ToList().Select(record => new ListViewItem
                {
                    Text = record.GetAttributeValue<string>("name"),
                    ImageIndex = 1,
                    StateImageIndex = 1,
                    Tag = record,
                    SubItems =
                    {
                        record.GetAttributeValue<EntityReference>("businessunitid").Name
                    }
                }).ToArray());
            }
        }

        private void lvUsersAndTeams_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == currentColumnOrder)
            {
                lvUsersAndTeams.Sorting = lvUsersAndTeams.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;

                lvUsersAndTeams.ListViewItemSorter = new ListViewItemComparer(e.Column, lvUsersAndTeams.Sorting);
            }
            else
            {
                currentColumnOrder = e.Column;
                lvUsersAndTeams.ListViewItemSorter = new ListViewItemComparer(e.Column, SortOrder.Ascending);
            }
        }
    }
}