// PROJECT : MsCrmTools.SiteMapEditor
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MsCrmTools.SiteMapEditor.AppCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Tanguy.WinForm.Utilities.DelegatesHelpers;

namespace MsCrmTools.SiteMapEditor.Forms.WebRessources
{
    public partial class WebResourcePicker : Form
    {
        #region Variables

        /// <summary>
        /// Requested web resource type
        /// </summary>
        private readonly int requestedType;

        private readonly IOrganizationService service;

        private List<Entity> webResourcesHtmlCache;

        private List<Entity> webResourcesImageCache;

        /// <summary>
        /// Web resource type enumeration
        /// </summary>
        public enum WebResourceType
        {
            WebPage = 1,
            Css = 2,
            Script = 3,
            Data = 4,
            Image = 5,
            Silverlight = 8,
            Xsl = 9,
            Ico = 10
        }

        #endregion Variables

        #region Properties

        /// <summary>
        /// Gets or sets the selected web resource
        /// </summary>
        public string SelectedResource { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class WebResourcePicker
        /// </summary>
        /// <param name="type">Type of web resource to select</param>
        public WebResourcePicker(WebResourceType type, List<Entity> webResourcesImageCache, List<Entity> webResourcesHtmlCache, IOrganizationService service)
        {
            InitializeComponent();

            this.webResourcesImageCache = webResourcesImageCache;
            this.webResourcesHtmlCache = webResourcesHtmlCache;
            this.service = service;

            requestedType = (int)type;

            // Disables controls
            ListViewDelegates.SetEnableState(lstWebResources, false);
            CommonDelegates.SetEnableState(btnWebResourcePickerCancel, false);
            CommonDelegates.SetEnableState(btnWebResourcePickerValidate, false);
            CommonDelegates.SetEnableState(btnNewResource, false);

            // Run work
            var worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        #endregion Constructor

        #region Methods

        private void btnNewResource_Click(object sender, EventArgs e)
        {
            CreateWebResourceDialog cwrDialog = new CreateWebResourceDialog((CreateWebResourceDialog.WebResourceType)requestedType, service);
            cwrDialog.StartPosition = FormStartPosition.CenterParent;

            if (cwrDialog.ShowDialog() == DialogResult.OK)
            {
                ListViewItem item = new ListViewItem(cwrDialog.CreatedEntity["displayname"].ToString());
                item.SubItems.Add(cwrDialog.CreatedEntity["name"].ToString());
                item.Tag = cwrDialog.CreatedEntity;

                lstWebResources.Items.Add(item);
                lstWebResources.Sort();
            }
        }

        private void BtnRefreshClick(object sender, EventArgs e)
        {
            lstWebResources.Items.Clear();

            if (requestedType == 1)
                FillHtmlList();
            else
                FillImageList();
        }

        private void btnWebResourcePickerCancel_Click(object sender, EventArgs e)
        {
            SelectedResource = null;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnWebResourcePickerValidate_Click(object sender, EventArgs e)
        {
            if (lstWebResources.SelectedItems.Count > 0)
            {
                SelectedResource = lstWebResources.SelectedItems[0].SubItems[1].Text;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(this, "Please select a web resource!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FillHtmlList()
        {
            ListViewDelegates.ClearItems(lstWebResources);

            if (webResourcesHtmlCache == null || webResourcesHtmlCache.Count == 0)
            {
                webResourcesHtmlCache = new List<Entity>();

                QueryExpression qe = new QueryExpression("webresource");

                ConditionExpression ce = new ConditionExpression();
                ce.AttributeName = "webresourcetype";

                if (requestedType == (int)WebResourceType.Image)
                {
                    ce.Operator = ConditionOperator.In;
                    ce.Values.AddRange(5, 6, 7);
                }
                else
                {
                    ce.Operator = ConditionOperator.Equal;
                    ce.Values.Add(requestedType);
                }

                qe.Criteria.AddCondition(ce);
                qe.ColumnSet.AllColumns = true;

                EntityCollection ec = service.RetrieveMultiple(qe);

                foreach (Entity webresource in ec.Entities)
                {
                    webResourcesHtmlCache.Add(webresource);
                }
            }

            foreach (Entity webresource in webResourcesHtmlCache)
            {
                ListViewItem item = new ListViewItem(webresource.Contains("displayname") ? webresource["displayname"].ToString() : "N/A");
                item.SubItems.Add(webresource["name"].ToString());
                item.Tag = webresource;

                ListViewDelegates.AddItem(lstWebResources, item);
            }

            ListViewDelegates.Sort(lstWebResources);
            ListViewDelegates.SetEnableState(lstWebResources, true);
            CommonDelegates.SetEnableState(btnWebResourcePickerCancel, true);
            CommonDelegates.SetEnableState(btnWebResourcePickerValidate, true);
            CommonDelegates.SetEnableState(btnNewResource, true);
            CommonDelegates.SetEnableState(btnRefresh, true);
        }

        private void FillImageList()
        {
            ListViewDelegates.ClearItems(lstWebResources);

            if (webResourcesImageCache == null || webResourcesImageCache.Count == 0)
            {
                webResourcesImageCache = new List<Entity>();

                QueryExpression qe = new QueryExpression("webresource");

                ConditionExpression ce = new ConditionExpression();
                ce.AttributeName = "webresourcetype";

                if (requestedType == (int)WebResourceType.Image)
                {
                    ce.Operator = ConditionOperator.In;
                    ce.Values.AddRange(5, 6, 7);
                }
                else
                {
                    ce.Operator = ConditionOperator.Equal;
                    ce.Values.Add(requestedType);
                }

                qe.Criteria.AddCondition(ce);
                qe.ColumnSet.AllColumns = true;

                EntityCollection ec = service.RetrieveMultiple(qe);

                foreach (Entity webresource in ec.Entities)
                {
                    webResourcesImageCache.Add(webresource);
                }
            }

            foreach (Entity webresource in webResourcesImageCache)
            {
                ListViewItem item = new ListViewItem(webresource.Contains("displayname") ? webresource["displayname"].ToString() : "N/A");
                item.SubItems.Add(webresource["name"].ToString());
                item.Tag = webresource;

                ListViewDelegates.AddItem(lstWebResources, item);
            }

            ListViewDelegates.Sort(lstWebResources);
            ListViewDelegates.SetEnableState(lstWebResources, true);
            CommonDelegates.SetEnableState(btnWebResourcePickerCancel, true);
            CommonDelegates.SetEnableState(btnWebResourcePickerValidate, true);
            CommonDelegates.SetEnableState(btnNewResource, true);
            CommonDelegates.SetEnableState(btnRefresh, true);
        }

        private void lstWebResources_DoubleClick(object sender, EventArgs e)
        {
            if (lstWebResources.SelectedItems.Count > 0)
            {
                Entity webresource = (Entity)lstWebResources.SelectedItems[0].Tag;
                SelectedResource = webresource["name"].ToString(); ;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (requestedType == 1)
                FillHtmlList();
            else
                FillImageList();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(this, CrmExceptionHelper.GetErrorMessage(e.Error, false), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        #endregion Methods

        private void lstWebResources_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lstWebResources.Sorting = lstWebResources.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lstWebResources.ListViewItemSorter = new ListViewItemComparer(e.Column, lstWebResources.Sorting);
        }
    }
}