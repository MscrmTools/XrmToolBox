using Microsoft.Xrm.Sdk;
using MsCrmTools.UserRolesManager.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MsCrmTools.UserRolesManager.UserControls
{
    public partial class RoleSelector : UserControl
    {
        private int currentColumnOrder;
        private IOrganizationService service;

        public RoleSelector()
        {
            InitializeComponent();
        }

        public List<Entity> AllRoles { get; set; }

        public List<Entity> SelectedRoles
        {
            get
            {
                return listView1.SelectedItems.Cast<ListViewItem>().Select(r => (Entity)r.Tag).ToList();
            }
        }

        public IOrganizationService Service
        {
            set { service = value; }
        }

        public void LoadRoles(Guid? specificBusinessUnitId = null)
        {
            listView1.Items.Clear();

            var rManager = new RoleManager(service);
            var rootBuId = rManager.GetRootBusinessUnitId();
            AllRoles = rManager.GetRoles();

            listView1.Items.AddRange(AllRoles
                .Where(role => role.GetAttributeValue<EntityReference>("parentrootroleid").Id == role.Id)
                .Select(role => new ListViewItem
            {
                Text = role.GetAttributeValue<string>("name"),
                SubItems =
                {
                    role.GetAttributeValue<EntityReference>("businessunitid").Name,
                    (rootBuId != role.GetAttributeValue<EntityReference>("businessunitid").Id).ToString()
                },
                ImageIndex = 0,
                StateImageIndex = 0,
                Tag = role
            }).ToArray());
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == currentColumnOrder)
            {
                listView1.Sorting = listView1.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;

                listView1.ListViewItemSorter = new ListViewItemComparer(e.Column, listView1.Sorting);
            }
            else
            {
                currentColumnOrder = e.Column;
                listView1.ListViewItemSorter = new ListViewItemComparer(e.Column, SortOrder.Ascending);
            }
        }
    }
}