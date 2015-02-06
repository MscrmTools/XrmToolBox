using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using McTools.Xrm.Connection;
using XrmToolBox;
using Microsoft.Xrm.Sdk;
using System.Xml;
using Microsoft.Xrm.Sdk.Metadata;
using Tanguy.WinForm.Utilities.DelegatesHelpers;
using MsCrmTools.ViewLayoutReplicator.Helpers;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk.Client;
using MsCrmTools.ViewLayoutReplicator.Forms;
using Microsoft.Crm.Sdk.Messages;
using XrmToolBox.Attributes;
using CrmExceptionHelper = XrmToolBox.CrmExceptionHelper;

[assembly: BackgroundColor("MediumBlue")]
[assembly: PrimaryFontColor("White")]
[assembly: SecondaryFontColor("LightGray")]
[assembly: SmallImageBase64("iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAACxIAAAsSAdLdfvwAAAAHdElNRQfdBRwPFgneQ4hIAAAAGnRFWHRTb2Z0d2FyZQBQYWludC5ORVQgdjMuNS4xMDD0cqEAAARBSURBVFhH7VZ7TFtVGC9lpVDaSwqXMiQttFCQh50WpKiw8YwPNGoQdVlkI2o0kqDBZGJ0icYxNrXhNZSaZUaMhpEsW7YosiywGqCrpS3t4NIic0s2mLO0c74ShPbz3EdbS9sJOPYXv+SXrz3nd37f1++cc29Zm9jESvSUVoj1hcVqomi7xualio7TKHppU5VopotKqEjO0+MlGgsam0DaCSr6STz8hPrG0V4xkyY0OvfsSTgl2moiGjHQpmFgi4hZGzkCMGzPBuKAGGzdcTQ/oaPudSGYKh8xLY0bE5h0gegouF96WpRkmv+YBx4HG4aV6yiAoR3H4UabHNzzMeBZiABAJD0Nh/hgrKg0uTRHpExaGupnnsVPJ4rMV1r41AKSZ5Wo8hDmXk4jEhE8Koaat3H48HNTLvLa4vP0LLBBvx8DY3mleXFkDKeSt21Tpp3Cceu19lha5KTFV0f5cC4fAztKstJ8Jl4EP36JwcxxHowfFMBYfGyQhuQ0Wvvr53lUYrILVCfQZ6MadWJHudXZ2Z3Gas9V7DaVZbrBEe0T0cIIGDnMC1nArDwDzXN9WoNaAFMhdCQv3pcKy5eFPk8yLs5HwYA8wUM8t6uepR0cjDySntUx99494Lke5ROS1PZyUAHBv+4iKgCctJak28mFQbEgSEcRHcw/Tsh8vn/PbYHva+PAkv/Ap/M/GCKpbTjZ1RXzlVw+4DpK7tnqC/AssCgdaa7bRW5XoM7L62/SvuBEZ2AvBvocxfBc3zEeldyL/TW1ScckUsOf32WuoQB/sWNvxYY9kJeqpEjDhskuPpxPlVsnm99JYdIG4v0dpbIzSrlj0SChFty6AP8ZIGlsxWFyhc7L2UIxXDrBh5F0scv6SsPdTLrQ+KhAVTxanOtcnhWtoQNs0DXzw3bAnpECw9Lkm5bqp8qYNLdGV4HqBWttNpz7LD58AegW/HsLdHXhzwDBwcDy2JMvMvarQ3euYt+ZvNRlW5hrCAv+W7Ds5MCQNPSDi4jC3BMl5S2M7erxwc6d0T3J4i8IdrAp3QF/AfZ+AVjCPAfMecq+8TeaYhjbtWGgr5/9dVLK2akVRdjxreBqSwQXem/M7YsFPR68TTY2D6xZitGrRj1919eL1rIKyUk8eSYowX/QmiL7iXitMZ2x+X9oeqg4fSBW6CCN6YNGtjscY+CCMNllebUhh1l+e3AoT1Gt5WK/67YlwlBRHCIWyAcx+FYlBKsg4S9T1aNPM8tuLz7Myqmbej53yX0FC7iC5EG8aePCUH68x1xa9RIj3xhoMjJbL+9VeDwLHF8BbgcHtNXoH09RSQcj2zi8vbue23OXpPeXdvSeR4mXrkWC7mUMXbf8flPzu9GMbGPRVP041p+Vof/teDaYWwSgl2eaiAMHhcz0nUFHTY3kG5mMGJfIZi40NKYxw3cWnaXl956vqy9kvm5iE+sAi/UPGYDwI+f5aW0AAAAASUVORK5CYII=")]
[assembly: BigImageBase64("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")]
namespace DamSim.ViewTransferTool
{
    public partial class ViewTransferTool : UserControl, IMsCrmToolsPluginUserControl
    {
        #region Variables

        /// <summary>
        /// Dynamics CRM 2011 organization service
        /// </summary>
        private IOrganizationService service;

        /// <summary>
        /// Dynamics CRM 2011 target organization service
        /// </summary>
        private IOrganizationService targetService;

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

        private EntityMetadata _savedQueryMetadata;

        #endregion

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

        public void UpdateConnection(Microsoft.Xrm.Sdk.IOrganizationService newService, ConnectionDetail connectionDetail, string actionName = "", object parameter = null)
        {
            if (actionName == "TargetOrganization")
            {
                targetService = newService;
                SetConnectionLabel(connectionDetail, "Target");
                ((OrganizationServiceProxy)((OrganizationService)targetService).InnerService).Timeout = new TimeSpan(
                    0, 1, 0, 0);
            }
            else
            {
                service = newService;
                SetConnectionLabel(connectionDetail, "Source");
                ((OrganizationServiceProxy)((OrganizationService)service).InnerService).Timeout = new TimeSpan(0, 1, 0, 0);
                LoadEntities();
            }
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

        #endregion

        private void btnSelectTarget_Click(object sender, EventArgs e)
        {
            if (OnRequestConnection != null)
            {
                var args = new RequestConnectionEventArgs { ActionName = "TargetOrganization", Control = this };
                OnRequestConnection(this, args);
            }
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

        #endregion

        #region FillViews

        private void lvEntities_SelectedIndexChanged(object sender, EventArgs e)
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
                            //item.SubItems.Add(view["name"].ToString());
                            display = false;
                        }
                        break;
                }

                #endregion

                if (display)
                {
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

        #endregion

        #region FillViewLayoutDetail

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

        private void BwDisplayViewRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lvSourceViews.SelectedIndexChanged += lvSourceViews_SelectedIndexChanged;
            lvSourceViews.Enabled = true;
            CommonDelegates.SetCursor(this, Cursors.Default);
        }

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
                            delegate(EntityMetadata emd)
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
                        header.Width = int.Parse(columnNode.Attributes["width"].Value);

                        ListViewDelegates.AddColumn(lvSourceViewLayoutPreview, header);

                        if (string.IsNullOrEmpty(item.Text))
                            item.Text = columnNode.Attributes["width"].Value + "px";
                        else
                            item.SubItems.Add(columnNode.Attributes["width"].Value + "px");
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

        #endregion

        #region Transfer views

        private void tsbTransferViews_Click(object sender, EventArgs e)
        {
            if(service==null || targetService == null)
            {
                MessageBox.Show("You must select both a source and a target environment.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(lvSourceViews.SelectedItems.Count == 0)
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

        #endregion
        
        #region Publish entity

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

        #endregion
        
        #region Publish all

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

        #endregion
    }
}
