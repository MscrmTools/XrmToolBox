using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using MsCrmTools.FormLibrariesManager.AppCode;

namespace MsCrmTools.FormLibrariesManager.UserControls
{
    public partial class CrmForms : UserControl
    {
        public CrmForms()
        {
            InitializeComponent();
        }

        public IOrganizationService Service { get; set; }

        public void LoadForms(List<Entity> forms)
        {
            lvForms.Items.Clear();

            foreach (var form in forms)
            {
                var item = new ListViewItem(form.GetAttributeValue<string>("objecttypecode"));
                item.SubItems.Add(form.GetAttributeValue<string>("name"));
                item.Tag = form;

                lvForms.Items.Add(item);
            }
        }

        public List<Entity> GetSelectedForms()
        {
            return lvForms.CheckedItems.Cast<ListViewItem>().Select(i => (Entity)i.Tag).ToList();
        }

        private void lvForms_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvForms.Sorting = lvForms.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lvForms.ListViewItemSorter = new ListViewItemComparer(e.Column, lvForms.Sorting);
        }
    }
}
