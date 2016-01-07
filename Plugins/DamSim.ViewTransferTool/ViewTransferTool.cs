using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.ViewLayoutReplicator.Forms;
using MsCrmTools.ViewLayoutReplicator.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using Tanguy.WinForm.Utilities.DelegatesHelpers;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using CrmExceptionHelper = XrmToolBox.CrmExceptionHelper;

namespace DamSim.ViewTransferTool
{
    public partial class ViewTransferTool : UserControl, IXrmToolBoxPluginControl
    {
        #region Variables

        private EntityMetadata _savedQueryMetadata;

        /// <summary>
        /// List of entities
        /// </summary>
        private List<EntityMetadata> entitiesCache;

        /// <summary>
        /// Information panel
        /// </summary>
        private Panel informationPanel;

        /// <summary>
        /// Dynamics CRM 2011 organization service
        /// </summary>
        private IOrganizationService service;

        /// <summary>
        /// Dynamics CRM 2011 target organization service
        /// </summary>
        private IOrganizationService targetService;

        #endregion Variables

        public ViewTransferTool()
        {
            InitializeComponent();
        }

        #region XrmToolbox

        public event EventHandler OnCloseTool;

        public event EventHandler OnRequestConnection;

        public Image PluginLogo
        {
            get { return imageList.Images[0]; }
        }

        public Microsoft.Xrm.Sdk.IOrganizationService Service
        {
            get { throw new NotImplementedException(); }
        }

        public void ClosingPlugin(PluginCloseInfo info)
        {
            if (info.FormReason != CloseReason.None ||
                info.ToolBoxReason == ToolBoxCloseReason.CloseAll ||
                info.ToolBoxReason == ToolBoxCloseReason.CloseAllExceptActive)
            {
                return;
            }

            info.Cancel = MessageBox.Show(@"Are you sure you want to close this tab?", @"Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes;
        }

        public void UpdateConnection(Microsoft.Xrm.Sdk.IOrganizationService newService, ConnectionDetail connectionDetail, string actionName = "", object parameter = null)
        {
            if (actionName == "TargetOrganization")
            {
                targetService = newService;
                SetConnectionLabel(connectionDetail, "Target");
            }
            else
            {
                service = newService;
                SetConnectionLabel(connectionDetail, "Source");
            }
        }

        #endregion XrmToolbox

        public string GetCompany()
        {
            return GetType().GetCompany();
        }

        public string GetMyType()
        {
            return GetType().FullName;
        }

        public string GetVersion()
        {
            return GetType().Assembly.GetName().Version.ToString();
        }

        private void btnSelectTarget_Click(object sender, EventArgs e)
        {
            if (OnRequestConnection != null)
            {
                var args = new RequestConnectionEventArgs { ActionName = "TargetOrganization", Control = this };
                OnRequestConnection(this, args);
            }
        }

        private void chkShowActiveViews_CheckedChanged(object sender, EventArgs e)
        {
            PopulateSourceViews();
        }

        private void ResetFilterControls()
        {
            chkShowActiveViews.CheckedChanged -= chkShowActiveViews_CheckedChanged;
            chkShowActiveViews.Checked = false;
            chkShowActiveViews.CheckedChanged += chkShowActiveViews_CheckedChanged;
        }

        private void SetConnectionLabel(ConnectionDetail detail, string serviceType)
        {
            switch (serviceType)
            {
                case "Source":
                    lbSourceValue.Text = detail.ConnectionName;
                    lbSourceValue.ForeColor = Color.Green;
                    break;

                case "Target":
                    lbTargetValue.Text = detail.ConnectionName;
                    lbTargetValue.ForeColor = Color.Green;
                    break;
            }
        }

        #region FillEntities

        /// <summary>
        /// Fills the entities listview
        /// </summary>
        public void FillEntitiesList()
        {
            try
            {
                ListViewDelegates.ClearItems(lvEntities);

                foreach (EntityMetadata emd in entitiesCache)
                {
                    var item = new ListViewItem { Text = emd.DisplayName.UserLocalizedLabel.Label, Tag = emd.LogicalName };
                    item.SubItems.Add(emd.LogicalName);
                    ListViewDelegates.AddItem(lvEntities, item);
                }
            }
            catch (Exception error)
            {
                string errorMessage = CrmExceptionHelper.GetErrorMessage(error, true);
                CommonDelegates.DisplayMessageBox(ParentForm, errorMessage, "Error", MessageBoxButtons.OK,
                                                  MessageBoxIcon.Error);
            }
        }

        private void BwFillEntitiesDoWork(object sender, DoWorkEventArgs e)
        {
            // Getting saved query entity metadata
            _savedQueryMetadata = MetadataHelper.RetrieveEntity("savedquery", service);

            // Caching entities
            entitiesCache = MetadataHelper.RetrieveEntities(service);

            // Filling entities list
            FillEntitiesList();
        }

        private void BwFillEntitiesRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                string errorMessage = CrmExceptionHelper.GetErrorMessage(e.Error, true);
                CommonDelegates.DisplayMessageBox(ParentForm, errorMessage, "Error", MessageBoxButtons.OK,
                                                  MessageBoxIcon.Error);
            }
            else
            {
                gbEntities.Enabled = true;
                tsbPublishEntity.Enabled = true;
                tsbPublishAll.Enabled = true;
            }

            Controls.Remove(informationPanel);
            CommonDelegates.SetCursor(this, Cursors.Default);
        }

        private void LoadEntities()
        {
            lvEntities.Items.Clear();
            gbEntities.Enabled = false;
            tsbPublishEntity.Enabled = false;
            tsbPublishAll.Enabled = false;

            lvSourceViews.Items.Clear();
            lvSourceViewLayoutPreview.Columns.Clear();

            CommonDelegates.SetCursor(this, Cursors.WaitCursor);

            informationPanel = InformationPanel.GetInformationPanel(this, "Loading entities...", 340, 120);

            var bwFillEntities = new BackgroundWorker();
            bwFillEntities.DoWork += BwFillEntitiesDoWork;
            bwFillEntities.RunWorkerCompleted += BwFillEntitiesRunWorkerCompleted;
            bwFillEntities.RunWorkerAsync();
        }

        private void tsbCloseThisTab_Click(object sender, EventArgs e)
        {
            if (OnCloseTool != null)
            {
                const string message = "Are you sure to exit?";
                if (MessageBox.Show(message, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.Yes)
                    OnCloseTool(this, null);
            }
        }

        private void tsbLoadEntities_Click(object sender, EventArgs e)
        {
            if (service == null)
            {
                if (OnRequestConnection != null)
                {
                    var args = new RequestConnectionEventArgs
                    {
                        ActionName = "Load",
                        Control = this
                    };
                    OnRequestConnection(this, args);
                }
                else
                {
                    MessageBox.Show(this, "OnRequestConnection event not registered!", "Error", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            else
            {
                LoadEntities();
            }
        }

        #endregion FillEntities

        #region FillViews

        private void BwFillViewsDoWork(object sender, DoWorkEventArgs e)
        {
            string entityLogicalName = e.Argument.ToString();

            List<Entity> viewsList = ViewHelper.RetrieveViews(entityLogicalName, entitiesCache, service);
            viewsList.AddRange(ViewHelper.RetrieveUserViews(entityLogicalName, entitiesCache, service));

            foreach (Entity view in viewsList)
            {
                bool display = true;

                var item = new ListViewItem(view["name"].ToString());
                item.Tag = view;

                display = ShouldDisplayItem(item);

                if (display)
                {
                    if (view.Contains("statecode"))
                    {
                        int statecodeValue = ((OptionSetValue)view["statecode"]).Value;
                        switch (statecodeValue)
                        {
                            case ViewHelper.VIEW_STATECODE_ACTIVE:
                                item.SubItems.Add("Active");
                                break;

                            case ViewHelper.VIEW_STATECODE_INACTIVE:
                                item.SubItems.Add("Inactive");
                                break;
                        }
                    }
                    // Add view to each list of views (source and target)
                    ListViewItem clonedItem = (ListViewItem)item.Clone();
                    ListViewDelegates.AddItem(lvSourceViews, item);

                    if (view.Contains("iscustomizable") &&
                        ((BooleanManagedProperty)view["iscustomizable"]).Value == false)
                    {
                        clonedItem.ForeColor = Color.Gray;
                        clonedItem.ToolTipText = "This view has not been defined as customizable";
                    }

                    //ListViewDelegates.AddItem(lvTargetViews, clonedItem);
                }
            }
        }

        private void BwFillViewsRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Cursor = Cursors.Default;
            gbSourceViews.Enabled = true;

            if (e.Error != null)
            {
                MessageBox.Show(this, "An error occured: " + e.Error.Message, "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

            if (lvSourceViews.Items.Count == 0)
            {
                MessageBox.Show(this, "This entity does not contain any view", "Warning", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void lvEntities_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetFilterControls();
            PopulateSourceViews();
        }

        private void PopulateSourceViews()
        {
            if (lvEntities.SelectedItems.Count > 0)
            {
                string entityLogicalName = lvEntities.SelectedItems[0].Tag.ToString();

                // Reinit other controls
                lvSourceViews.Items.Clear();
                lvSourceViewLayoutPreview.Columns.Clear();

                Cursor = Cursors.WaitCursor;

                // Launch treatment
                var bwFillViews = new BackgroundWorker();
                bwFillViews.DoWork += BwFillViewsDoWork;
                bwFillViews.RunWorkerAsync(entityLogicalName);
                bwFillViews.RunWorkerCompleted += BwFillViewsRunWorkerCompleted;
            }
        }

        #endregion FillViews

        #region FillViewLayoutDetail

        private void BwDisplayViewDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (ListViewDelegates.GetSelectedItems(lvSourceViews).Count() > 1)
                {
                    ColumnHeader header = new ColumnHeader();
                    header.Width = 380;
                    header.Text = "Layout preview cannot be displayed when multiple views are selected.";
                    ListViewDelegates.AddColumn(lvSourceViewLayoutPreview, header);
                }
                else
                {
                    // Gets current view data
                    Entity currentSelectedView = (Entity)ListViewDelegates.GetSelectedItems(lvSourceViews)[0].Tag;
                    string layoutXml = currentSelectedView["layoutxml"].ToString();
                    string fetchXml = currentSelectedView.Contains("fetchxml")
                                          ? currentSelectedView["fetchxml"].ToString()
                                          : string.Empty;
                    string currentEntityDisplayName = ListViewDelegates.GetSelectedItems(lvEntities)[0].Text;

                    EntityMetadata currentEmd =
                        entitiesCache.Find(
                            delegate (EntityMetadata emd)
                            { return emd.DisplayName.UserLocalizedLabel.Label == currentEntityDisplayName; });

                    XmlDocument layoutDoc = new XmlDocument();
                    layoutDoc.LoadXml(layoutXml);

                    EntityMetadata emdWithItems = MetadataHelper.RetrieveEntity(currentEmd.LogicalName, service);

                    ListViewItem item = new ListViewItem();

                    foreach (XmlNode columnNode in layoutDoc.SelectNodes("grid/row/cell"))
                    {
                        ColumnHeader header = new ColumnHeader();

                        header.Text = MetadataHelper.RetrieveAttributeDisplayName(emdWithItems,
                                                                                  columnNode.Attributes["name"].Value,
                                                                                  fetchXml, service);

                        int columnWidth = columnNode.Attributes["width"] == null ? 0 : int.Parse(columnNode.Attributes["width"].Value);

                        header.Width = columnWidth;

                        ListViewDelegates.AddColumn(lvSourceViewLayoutPreview, header);

                        if (string.IsNullOrEmpty(item.Text))
                            item.Text = columnWidth == 0 ? "(undefined)" : (columnWidth + "px");
                        else
                            item.SubItems.Add(columnWidth == 0 ? "(undefined)" : (columnWidth + "px"));
                    }

                    ListViewDelegates.AddItem(lvSourceViewLayoutPreview, item);

                    GroupBoxDelegates.SetEnableState(gbSourceViewLayout, true);
                }
            }
            catch (Exception error)
            {
                CommonDelegates.DisplayMessageBox(ParentForm, "Error while displaying view: " + error.Message, "Error",
                                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BwDisplayViewRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lvSourceViews.SelectedIndexChanged += lvSourceViews_SelectedIndexChanged;
            lvSourceViews.Enabled = true;
            CommonDelegates.SetCursor(this, Cursors.Default);
        }

        private void lvSourceViews_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvSourceViewLayoutPreview.Columns.Clear();

            if (lvSourceViews.SelectedItems.Count > 0)
            {
                lvSourceViews.SelectedIndexChanged -= lvSourceViews_SelectedIndexChanged;
                lvSourceViewLayoutPreview.Items.Clear();
                lvSourceViews.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var bwDisplayView = new BackgroundWorker();
                bwDisplayView.DoWork += BwDisplayViewDoWork;
                bwDisplayView.RunWorkerCompleted += BwDisplayViewRunWorkerCompleted;
                bwDisplayView.RunWorkerAsync(lvSourceViews.SelectedItems[0].Tag);
            }
        }

        #endregion FillViewLayoutDetail

        #region Transfer views

        private void BwTransferViewsDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                List<Entity> checkedViews = new List<Entity>();

                foreach (ListViewItem item in ListViewDelegates.GetSelectedItems(lvSourceViews))
                {
                    checkedViews.Add((Entity)item.Tag);
                }

                e.Result = ViewHelper.TransferViews(checkedViews, service, targetService, _savedQueryMetadata);
            }
            catch (Exception error)
            {
                CommonDelegates.DisplayMessageBox(ParentForm, error.Message, "Error", MessageBoxButtons.OK,
                                                  MessageBoxIcon.Error);
            }
        }

        private void BwTransferViewsWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CommonDelegates.SetCursor(this, Cursors.Default);

            Controls.Remove(informationPanel);

            if (e.Result == null) return;

            if (((List<Tuple<string, string>>)e.Result).Count > 0)
            {
                var errorDialog = new ErrorList((List<Tuple<string, string>>)e.Result);
                errorDialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selected views have been successfully transfered!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tsbTransferViews_Click(object sender, EventArgs e)
        {
            if (service == null || targetService == null)
            {
                MessageBox.Show("You must select both a source and a target environment.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lvSourceViews.SelectedItems.Count == 0)
            {
                MessageBox.Show("You must select at least one view to be transfered in the right list.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CommonDelegates.SetCursor(this, Cursors.WaitCursor);
            var bwTransferViews = new BackgroundWorker();
            bwTransferViews.DoWork += BwTransferViewsDoWork;
            bwTransferViews.RunWorkerCompleted += BwTransferViewsWorkerCompleted;
            bwTransferViews.RunWorkerAsync();
        }

        #endregion Transfer views

        #region Publish entity

        private void BwPublishDoWork(object sender, DoWorkEventArgs e)
        {
            EntityMetadata currentEmd =
                entitiesCache.Find(
                    emd => emd.DisplayName.UserLocalizedLabel.Label == e.Argument.ToString());

            var pubRequest = new PublishXmlRequest();
            pubRequest.ParameterXml = string.Format(@"<importexportxml>
                                                           <entities>
                                                              <entity>{0}</entity>
                                                           </entities>
                                                           <nodes/><securityroles/><settings/><workflows/>
                                                        </importexportxml>",
                                                    currentEmd.LogicalName);

            targetService.Execute(pubRequest);
        }

        private void BwPublishRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CommonDelegates.SetCursor(this, Cursors.Default);
            //Cursor = Cursors.Default;

            if (e.Error != null)
            {
                string errorMessage = CrmExceptionHelper.GetErrorMessage(e.Error, false);
                MessageBox.Show(this, errorMessage, "Error", MessageBoxButtons.OK,
                                                  MessageBoxIcon.Error);
            }

            Controls.Remove(informationPanel);

            tsbPublishEntity.Enabled = true;
            tsbPublishAll.Enabled = true;
            tsbLoadEntities.Enabled = true;
        }

        private void tsbPublishEntity_Click(object sender, EventArgs e)
        {
            if (lvEntities.SelectedItems.Count > 0)
            {
                tsbPublishEntity.Enabled = false;
                tsbPublishAll.Enabled = false;
                tsbLoadEntities.Enabled = false;

                CommonDelegates.SetCursor(this, Cursors.WaitCursor);

                informationPanel = InformationPanel.GetInformationPanel(this, "Publishing entity...", 340, 120);

                var bwPublish = new BackgroundWorker();
                bwPublish.DoWork += BwPublishDoWork;
                bwPublish.RunWorkerCompleted += BwPublishRunWorkerCompleted;
                bwPublish.RunWorkerAsync(lvEntities.SelectedItems[0].Text);
            }
        }

        #endregion Publish entity

        #region Publish all

        private void BwPublishAllDoWork(object sender, DoWorkEventArgs e)
        {
            var pubRequest = new PublishAllXmlRequest();
            targetService.Execute(pubRequest);
        }

        private void BwPublishAllRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Cursor = Cursors.Default;

            if (e.Error != null)
            {
                string errorMessage = CrmExceptionHelper.GetErrorMessage(e.Error, false);
                MessageBox.Show(this, errorMessage, "Error", MessageBoxButtons.OK,
                                                  MessageBoxIcon.Error);
            }

            Controls.Remove(informationPanel);

            tsbPublishEntity.Enabled = true;
            tsbPublishAll.Enabled = true;
            tsbLoadEntities.Enabled = true;
        }

        private void tsbPublishAll_Click(object sender, EventArgs e)
        {
            tsbPublishEntity.Enabled = false;
            tsbPublishAll.Enabled = false;
            tsbLoadEntities.Enabled = false;

            Cursor = Cursors.WaitCursor;

            informationPanel = InformationPanel.GetInformationPanel(this, "Publishing all customizations...", 340, 120);

            var bwPublishAll = new BackgroundWorker();
            bwPublishAll.DoWork += BwPublishAllDoWork;
            bwPublishAll.RunWorkerCompleted += BwPublishAllRunWorkerCompleted;
            bwPublishAll.RunWorkerAsync();
        }

        #endregion Publish all

        private bool ShouldDisplayItem(ListViewItem item)
        {
            bool display = true;
            var view = (Entity)item.Tag;

            #region Gestion de l'image associée à la vue

            switch ((int)view["querytype"])
            {
                case ViewHelper.VIEW_BASIC:
                    {
                        if (view.LogicalName == "savedquery")
                        {
                            if ((bool)view["isdefault"])
                            {
                                item.SubItems.Add("Default public view");
                                item.ImageIndex = 3;
                            }
                            else
                            {
                                item.SubItems.Add("Public view");
                                item.ImageIndex = 0;
                            }
                        }
                        else
                        {
                            item.SubItems.Add("User view");
                            item.ImageIndex = 6;
                        }
                    }
                    break;

                case ViewHelper.VIEW_ADVANCEDFIND:
                    {
                        item.SubItems.Add("Advanced find view");
                        item.ImageIndex = 1;
                    }
                    break;

                case ViewHelper.VIEW_ASSOCIATED:
                    {
                        item.SubItems.Add("Associated view");
                        item.ImageIndex = 2;
                    }
                    break;

                case ViewHelper.VIEW_QUICKFIND:
                    {
                        item.SubItems.Add("QuickFind view");
                        item.ImageIndex = 5;
                    }
                    break;

                case ViewHelper.VIEW_SEARCH:
                    {
                        item.SubItems.Add("Lookup view");
                        item.ImageIndex = 4;
                    }
                    break;

                default:
                    {
                        return false;
                    }
            }

            #endregion Gestion de l'image associée à la vue

            #region Filters

            if (chkShowActiveViews.Checked)
            {
                var viewStateCode = view.GetAttributeValue<OptionSetValue>("statecode").Value;
                if (viewStateCode == ViewHelper.VIEW_STATECODE_INACTIVE)
                {
                    return false;
                }
            }

            #endregion Filters

            return display;
        }
    }
}