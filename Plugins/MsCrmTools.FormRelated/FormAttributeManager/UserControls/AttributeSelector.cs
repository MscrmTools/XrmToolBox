using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using MsCrmTools.FormAttributeManager.AppCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace MsCrmTools.FormAttributeManager.UserControls
{
    public partial class AttributeSelector : UserControl
    {
        private Panel infoPanel;
        private int localeId;
        private IOrganizationService service;

        public AttributeSelector()
        {
            InitializeComponent();
        }

        public event EventHandler<AttributeSelectedEventArgs> OnAttributeSelected;

        public event EventHandler<EntitySelectedEventArgs> OnEntitySelected;

        public List<AttributeMetadata> Attributes
        {
            get
            {
                if (lvAttributes.Items.Count == 0)
                    return null;

                return lvAttributes.Items.Cast<ListViewItem>().Select(i => (AttributeMetadata)i.Tag).ToList();
            }
        }

        public List<FormInfo> EntityForms { set; private get; }
        public int LocaleId { get { return localeId; } }

        public List<AttributeMetadata> SelectedAttributes
        {
            get
            {
                if (lvAttributes.SelectedItems.Count == 0)
                    return new List<AttributeMetadata>();

                return lvAttributes.SelectedItems.Cast<ListViewItem>().Select(i => (AttributeMetadata)i.Tag).ToList();
            }
        }

        public EntityMetadata SelectedEntity
        {
            get
            {
                if (cbbEntities.Items.Count == 0)
                    return null;

                return ((EntityInfo)cbbEntities.SelectedItem).Metadata;
            }
        }

        public IOrganizationService Service { set { service = value; } }

        public void LoadEntities()
        {
            infoPanel = InformationPanel.GetInformationPanel(ParentForm, "Loading entities...", 340, 150);

            var worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        protected virtual void RaiseAttributeSelected(AttributeSelectedEventArgs e)
        {
            EventHandler<AttributeSelectedEventArgs> handler = OnAttributeSelected;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void RaiseEntitySelected(EntitySelectedEventArgs e)
        {
            EventHandler<EntitySelectedEventArgs> handler = OnEntitySelected;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void attrWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var request = new RetrieveEntityRequest { EntityFilters = EntityFilters.Attributes, LogicalName = ((EntityInfo)e.Argument).Metadata.LogicalName };
            var response = (RetrieveEntityResponse)service.Execute(request);

            e.Result = response.EntityMetadata;
        }

        private void attrWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var itemList = new List<ListViewItem>();

            foreach (var amd in ((EntityMetadata)e.Result).Attributes.Where(a => a.AttributeOf == null))
            {
                var item =
                    new ListViewItem(amd.DisplayName != null && amd.DisplayName.UserLocalizedLabel != null
                        ? amd.DisplayName.UserLocalizedLabel.Label
                        : "N/A");
                item.SubItems.Add(amd.LogicalName);
                item.Tag = amd;

                itemList.Add(item);
            }

            lvAttributes.Items.AddRange(itemList.ToArray());

            if (ParentForm != null)
            {
                ParentForm.Controls.Remove(infoPanel);
            }
            infoPanel.Dispose();

            RaiseEntitySelected(new EntitySelectedEventArgs { Metadata = (EntityMetadata)e.Result });
        }

        private void cbbEntities_SelectedIndexChanged(object sender, EventArgs e)
        {
            var entityInfo = cbbEntities.SelectedItem;
            if (entityInfo != null)
            {
                lvAttributes.Items.Clear();

                infoPanel = InformationPanel.GetInformationPanel(ParentForm, "Loading attributes...", 340, 150);

                var attrWorker = new BackgroundWorker();
                attrWorker.DoWork += attrWorker_DoWork;
                attrWorker.RunWorkerCompleted += attrWorker_RunWorkerCompleted;
                attrWorker.RunWorkerAsync(entityInfo);

                lvAttributes.Focus();
            }
        }

        private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            var list = (ListView)sender;
            list.Sorting = list.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            list.ListViewItemSorter = new ListViewItemComparer(e.Column, list.Sorting);
        }

        private void llSelectAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem item in lvAttributes.Items)
            {
                item.Selected = true;
            }
            lvAttributes.Focus();
        }

        private void llSelectNone_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem item in lvAttributes.Items)
            {
                item.Selected = false;
            }
            lvAttributes.Focus();
        }

        private void llSelectNotOnForm_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem item in lvAttributes.Items)
            {
                var amd = (AttributeMetadata)item.Tag;
                item.Selected = EntityForms.All(f => !f.HasAttribute(amd.LogicalName));
            }
            lvAttributes.Focus();
        }

        private void llSelectOnForm_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem item in lvAttributes.Items)
            {
                var amd = (AttributeMetadata)item.Tag;
                item.Selected = EntityForms.All(f => f.HasAttribute(amd.LogicalName));
            }
            lvAttributes.Focus();
        }

        private void lvAttributes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvAttributes.SelectedItems.Count > 0)
            {
                RaiseAttributeSelected(new AttributeSelectedEventArgs { Metadata = (AttributeMetadata)lvAttributes.SelectedItems[0].Tag });
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var request = new RetrieveAllEntitiesRequest { EntityFilters = EntityFilters.Entity };
            var response = (RetrieveAllEntitiesResponse)service.Execute(request);

            var settings =
                service.RetrieveMultiple(new QueryExpression("usersettings") { ColumnSet = new ColumnSet("localeid") });
            localeId = settings.Entities.First().GetAttributeValue<int>("localeid");

            e.Result = response.EntityMetadata.Select(emd => new EntityInfo { Metadata = emd });
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (var ei in (IEnumerable<EntityInfo>)e.Result)
            {
                cbbEntities.Items.Add(ei);
            }

            if (ParentForm != null)
            { ParentForm.Controls.Remove(infoPanel); }
            infoPanel.Dispose();

            cbbEntities.DroppedDown = true;
        }
    }
}