﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using MsCrmTools.FormAttributeManager.AppCode;
using XrmToolBox;

namespace MsCrmTools.FormAttributeManager.UserControls
{
    public partial class AttributeSelector : UserControl
    {
        private IOrganizationService service;

        private Panel infoPanel;

        private int localeId;
        public AttributeSelector()
        {
            InitializeComponent();
        }

        public IOrganizationService Service { set { service = value; } }
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

        public List<AttributeMetadata> Attributes
        {
            get
            {
                if (lvAttributes.Items.Count == 0)
                    return null;
                
                return lvAttributes.Items.Cast<ListViewItem>().Select(i => (AttributeMetadata) i.Tag).ToList();
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

        public event EventHandler<AttributeSelectedEventArgs> OnAttributeSelected;
        public event EventHandler<EntitySelectedEventArgs> OnEntitySelected;

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
        
                                                       
        public void LoadEntities()
        {
            infoPanel = InformationPanel.GetInformationPanel(ParentForm, "Loading entities...", 340, 150);

            var worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var request = new RetrieveAllEntitiesRequest{EntityFilters = EntityFilters.Entity};
            var response = (RetrieveAllEntitiesResponse) service.Execute(request);

            var settings =
                service.RetrieveMultiple(new QueryExpression("usersettings") {ColumnSet = new ColumnSet("localeid")});
            localeId = settings.Entities.First().GetAttributeValue<int>("localeid");
            
            e.Result = response.EntityMetadata.Select(emd => new EntityInfo { Metadata = emd });
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (var ei in (IEnumerable<EntityInfo>) e.Result)
            {
                cbbEntities.Items.Add(ei);
            }

            if(ParentForm != null)
            {ParentForm.Controls.Remove(infoPanel);}
            infoPanel.Dispose();

            cbbEntities.DroppedDown = true;
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

        void attrWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var request = new RetrieveEntityRequest {EntityFilters = EntityFilters.Attributes, LogicalName = ((EntityInfo)e.Argument).Metadata.LogicalName};
            var response = (RetrieveEntityResponse) service.Execute(request);

            e.Result = response.EntityMetadata;
        }

        void attrWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var itemList = new List<ListViewItem>();

            foreach (var amd in ((EntityMetadata) e.Result).Attributes.Where(a=>a.AttributeOf == null))
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

        private void lvAttributes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvAttributes.SelectedItems.Count > 0)
            {
                RaiseAttributeSelected(new AttributeSelectedEventArgs{Metadata = (AttributeMetadata)lvAttributes.SelectedItems[0].Tag});
            }
        }
    }
}
