// PROJECT : MsCrmTools.ViewLayoutReplicator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.ViewLayoutReplicator.Helpers;
using Tanguy.WinForm.Utilities.DelegatesHelpers;
using XrmToolBox;

namespace MsCrmTools.ViewLayoutReplicator
{
    public partial class ViewLayoutReplicatorControl : UserControl, IMsCrmToolsPluginUserControl
    {
        #region Variables

        /// <summary>
        /// Dynamics CRM 2011 organization service
        /// </summary>
        private IOrganizationService service;

        /// <summary>
        /// XML Document that represents customization
        /// </summary>
        private XmlDocument custoDoc;

        /// <summary>
        /// List of entities
        /// </summary>
        private List<EntityMetadata> entitiesCache;

        /// <summary>
        /// List of views
        /// </summary>
        private Dictionary<Guid, Entity> viewsList;

        /// <summary>
        /// Information panel
        /// </summary>
        private Panel informationPanel;

        #endregion

        #region Constructor

        public ViewLayoutReplicatorControl()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region Main ToolStrip Handlers

        #region Fill Entities

        private void TsbLoadEntitiesClick(object sender, EventArgs e)
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

        private void LoadEntities()
        {
            lvEntities.Items.Clear();
            gbEntities.Enabled = false;
            tsbPublishEntity.Enabled = false;
            tsbSaveViews.Enabled = false;

            lvSourceViews.Items.Clear();
            lvTargetViews.Items.Clear();
            lvSourceViewLayoutPreview.Columns.Clear();

            CommonDelegates.SetCursor(this, Cursors.WaitCursor);

            informationPanel = InformationPanel.GetInformationPanel(this, "Loading entities...", 340, 100);

            var bwFillEntities = new BackgroundWorker();
            bwFillEntities.DoWork += BwFillEntitiesDoWork;
            bwFillEntities.RunWorkerCompleted += BwFillEntitiesRunWorkerCompleted;
            bwFillEntities.RunWorkerAsync();
        }

        private void BwFillEntitiesDoWork(object sender, DoWorkEventArgs e)
        {
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
            }

            Controls.Remove(informationPanel);
            CommonDelegates.SetCursor(this, Cursors.Default);
        }

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
                    var item = new ListViewItem {Text = emd.DisplayName.UserLocalizedLabel.Label, Tag = emd.LogicalName};
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

        #endregion

        #region Save Views

        private void TsbSaveViewsClick(object sender, EventArgs e)
        {
            tsbPublishEntity.Enabled = false;
            tsbSaveViews.Enabled = false;
            tsbLoadEntities.Enabled = false;

            //this.Cursor = Cursors.WaitCursor;
            CommonDelegates.SetCursor(this, Cursors.WaitCursor);

            informationPanel = InformationPanel.GetInformationPanel(this, "Saving views...", 340, 100);

            var bwSaveViews = new BackgroundWorker();
            bwSaveViews.DoWork += BwSaveViewsDoWork;
            bwSaveViews.RunWorkerCompleted += BwSaveViewsRunWorkerCompleted;
            bwSaveViews.RunWorkerAsync();
        }

        private void BwSaveViewsDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Entity sourceView = (Entity)ListViewDelegates.GetSelectedItems(lvSourceViews)[0].Tag;

                List<Entity> targetViews = new List<Entity>();

                foreach (ListViewItem item in ListViewDelegates.GetCheckedItems(lvTargetViews))
                {
                    targetViews.Add((Entity)item.Tag);
                }

                e.Result = ViewHelper.PropagateLayout(sourceView, targetViews, service);
            }
            catch (Exception error)
            {
                CommonDelegates.DisplayMessageBox(ParentForm, error.Message, "Error", MessageBoxButtons.OK,
                                                  MessageBoxIcon.Error);
            }
        }

        private void BwSaveViewsRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CommonDelegates.SetCursor(this, Cursors.Default);
            //Cursor = Cursors.Default;

            Controls.Remove(informationPanel);

            if ((bool) e.Result == false)
            {
                MessageBox.Show(this, "Checked target views updated!\n\nThe associated view has not been updated because of related attributes",
                                                  "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            tsbPublishEntity.Enabled = true;
            tsbSaveViews.Enabled = true;
            tsbLoadEntities.Enabled = true;
        }
        
        #endregion

        #region Publish Entity

        private void TsbPublishEntityClick(object sender, EventArgs e)
        {
            if (lvEntities.SelectedItems.Count > 0)
            {
                tsbPublishEntity.Enabled = false;
                tsbSaveViews.Enabled = false;
                tsbLoadEntities.Enabled = false;

                CommonDelegates.SetCursor(this, Cursors.WaitCursor);

                informationPanel = InformationPanel.GetInformationPanel(this, "Publishing entity...", 340, 100);

                var bwPublish = new BackgroundWorker();
                bwPublish.DoWork += BwPublishDoWork;
                bwPublish.RunWorkerCompleted += BwPublishRunWorkerCompleted;
                bwPublish.RunWorkerAsync(lvEntities.SelectedItems[0].Text);
            }
        }

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

                service.Execute(pubRequest);
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
            tsbSaveViews.Enabled = true;
            tsbLoadEntities.Enabled = true;
        }

        #endregion

        #endregion

        #region ListViews Handlers

        #region Fill Views

        private void lvEntities_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvEntities.SelectedItems.Count > 0)
            {
                string entityLogicalName = lvEntities.SelectedItems[0].Tag.ToString();

                // Reinit other controls
                lvSourceViews.Items.Clear();
                lvSourceViewLayoutPreview.Columns.Clear();
                lvTargetViews.Items.Clear();

                Cursor = Cursors.WaitCursor;

                // Launch treatment
                var bwFillViews = new BackgroundWorker();
                bwFillViews.DoWork += BwFillViewsDoWork;
                bwFillViews.RunWorkerAsync(entityLogicalName);
                bwFillViews.RunWorkerCompleted += BwFillViewsRunWorkerCompleted;
            }
        }

        private void BwFillViewsDoWork(object sender, DoWorkEventArgs e)
        {
            string entityLogicalName = e.Argument.ToString();

            List<Entity> viewsList = ViewHelper.RetrieveViews(entityLogicalName, entitiesCache, service);

            foreach (Entity view in viewsList)
            {
                bool display = true;

                var item = new ListViewItem(view["name"].ToString());
                item.Tag = view;

                #region Gestion de l'image associée à la vue

                switch ((int) view["querytype"])
                {
                    case ViewHelper.VIEW_BASIC:
                        {
                            if ((bool) view["isdefault"])
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
                            display = false;
                        }
                        break;
                }

                #endregion

                if (display)
                {
                    // Add view to each list of views (source and target)
                    ListViewItem clonedItem = (ListViewItem) item.Clone();
                    ListViewDelegates.AddItem(lvSourceViews, item);

                    if (view.Contains("iscustomizable") &&
                        ((BooleanManagedProperty) view["iscustomizable"]).Value == false)
                    {
                        clonedItem.ForeColor = Color.Gray;
                        clonedItem.ToolTipText = "This view has not been defined as customizable";
                    }

                    ListViewDelegates.AddItem(lvTargetViews, clonedItem);
                }
            }
        }

        private void BwFillViewsRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null && !e.Cancelled)
            {
                Cursor = Cursors.Default;
                gbSourceViews.Enabled = true;
                gbTargetViews.Enabled = true;
            }

            if (lvSourceViews.Items.Count == 0)
            {
                MessageBox.Show(this, "This entity does not contain any view", "Warning", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region Display View

        private void LvSourceViewsSelectedIndexChanged(object sender, System.EventArgs e)
        {
            lvSourceViewLayoutPreview.Columns.Clear();

            if (lvSourceViews.SelectedItems.Count > 0)
            {
                lvSourceViews.SelectedIndexChanged -= LvSourceViewsSelectedIndexChanged;

                lvSourceViews.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var bwDisplayView = new BackgroundWorker();
                bwDisplayView.DoWork += BwDisplayViewDoWork;
                bwDisplayView.RunWorkerCompleted += BwDisplayViewRunWorkerCompleted;
                bwDisplayView.RunWorkerAsync(lvSourceViews.SelectedItems[0].Tag);
            }
        }

        private void BwDisplayViewRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lvSourceViews.SelectedIndexChanged += LvSourceViewsSelectedIndexChanged;
            lvSourceViews.Enabled = true;
            Cursor = Cursors.Default;
        }

        private void BwDisplayViewDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // Gets current view data
                Entity currentSelectedView = (Entity) ListViewDelegates.GetSelectedItems(lvSourceViews)[0].Tag;
                string layoutXml = currentSelectedView["layoutxml"].ToString();
                string fetchXml = currentSelectedView.Contains("fetchxml")
                                      ? currentSelectedView["fetchxml"].ToString()
                                      : string.Empty;
                string currentEntityDisplayName = ListViewDelegates.GetSelectedItems(lvEntities)[0].Text;

                EntityMetadata currentEmd =
                    entitiesCache.Find(
                        delegate(EntityMetadata emd)
                            { return emd.DisplayName.UserLocalizedLabel.Label == currentEntityDisplayName; });

                XmlDocument layoutDoc = new XmlDocument();
                layoutDoc.LoadXml(layoutXml);

                EntityMetadata emdWithItems = MetadataHelper.RetrieveEntity(currentEmd.LogicalName, service);

                ListViewItem item = new ListViewItem();
                item.Text = "preview";

                foreach (XmlNode columnNode in layoutDoc.SelectNodes("grid/row/cell"))
                {
                    ColumnHeader header = new ColumnHeader();

                    header.Text = MetadataHelper.RetrieveAttributeDisplayName(emdWithItems,
                                                                              columnNode.Attributes["name"].Value,
                                                                              fetchXml, service);
                    header.Width = int.Parse(columnNode.Attributes["width"].Value);

                    ListViewDelegates.AddColumn(lvSourceViewLayoutPreview, header);

                    item.SubItems.Add("preview");
                }

                ListViewDelegates.AddItem(lvSourceViewLayoutPreview, item);

                GroupBoxDelegates.SetEnableState(gbSourceViewLayout, true);
            }
            catch (Exception error)
            {
                CommonDelegates.DisplayMessageBox(ParentForm, "Error while displaying view: " + error.Message, "Error",
                                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LvTargetViewsItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked && e.Item.ForeColor == Color.Gray)
            {
                MessageBox.Show(this, "This view has not been defined as customizable. It can't be customized!",
                                "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Item.Checked = false;
            }

            if (ListViewDelegates.GetCheckedItems(lvTargetViews).Length > 0)
            {
                tsbSaveViews.Enabled = true;
                tsbPublishEntity.Enabled = true;
            }
            else
            {
                tsbSaveViews.Enabled = false;
                tsbPublishEntity.Enabled = false;
            }
        }

        #endregion

        #endregion

        public IOrganizationService Service
        {
            get { return service; }
        }

        public Image PluginLogo
        {
            get { return imageList2.Images[0]; }
        }

        public event EventHandler OnRequestConnection;
        public event EventHandler OnCloseTool;

        public void UpdateConnection(IOrganizationService newService, string actionName = "", object parameter = null)
        {
            service = newService;

            if (actionName == "Load")
            {
                LoadEntities();
            }
        }

        private void TsbCloseThisTabClick(object sender, EventArgs e)
        {
            const string message = "Are your sure you want to close this tab?";
            if (MessageBox.Show(message, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.Yes)
                OnCloseTool(this, null);
        }

        private void LvEntitiesColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvEntities.Sorting = lvEntities.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lvEntities.ListViewItemSorter = new ListViewItemComparer(e.Column, lvEntities.Sorting);
        }
    }
}
