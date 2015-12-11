// PROJECT : MsCrmTools.ViewLayoutReplicator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

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

namespace MsCrmTools.ViewLayoutReplicator
{
    public partial class ViewLayoutReplicator : PluginControlBase
    {
        private List<EntityMetadata> entitiesCache;
        private ListViewItem[] listViewItemsCache;

        #region Constructor

        public ViewLayoutReplicator()
        {
            InitializeComponent();

            var tt = new ToolTip();
            tt.SetToolTip(lvSourceViews, "Double click on a selected row to display its layout XML");
        }

        #endregion Constructor

        #region Main ToolStrip Handlers

        #region Fill Entities

        private void LoadEntities()
        {
            txtSearchEntity.Text = string.Empty;
            lvEntities.Items.Clear();
            gbEntities.Enabled = false;
            tsbPublishEntity.Enabled = false;
            tsbPublishAll.Enabled = false;
            tsbSaveViews.Enabled = false;

            lvSourceViews.Items.Clear();
            lvTargetViews.Items.Clear();
            lvSourceViewLayoutPreview.Columns.Clear();

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading entities...",
                Work = (bw, e) =>
                {
                    e.Result = MetadataHelper.RetrieveEntities(Service);
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        string errorMessage = CrmExceptionHelper.GetErrorMessage(e.Error, true);
                        CommonDelegates.DisplayMessageBox(ParentForm, errorMessage, "Error", MessageBoxButtons.OK,
                                                          MessageBoxIcon.Error);
                    }
                    else
                    {
                        entitiesCache = (List<EntityMetadata>)e.Result;
                        lvEntities.Items.Clear();
                        var list = new List<ListViewItem>();
                        foreach (EntityMetadata emd in (List<EntityMetadata>)e.Result)
                        {
                            var item = new ListViewItem { Text = emd.DisplayName.UserLocalizedLabel.Label, Tag = emd.LogicalName };
                            item.SubItems.Add(emd.LogicalName);
                            list.Add(item);
                        }

                        this.listViewItemsCache = list.ToArray();
                        lvEntities.Items.AddRange(listViewItemsCache);

                        gbEntities.Enabled = true;
                        tsbPublishEntity.Enabled = true;
                        tsbPublishAll.Enabled = true;
                        tsbSaveViews.Enabled = true;
                    }
                }
            });
        }

        private void TsbLoadEntitiesClick(object sender, EventArgs e)
        {
            ExecuteMethod(LoadEntities);
        }

        #endregion Fill Entities

        #region Save Views

        private void TsbSaveViewsClick(object sender, EventArgs e)
        {
            tsbPublishEntity.Enabled = false;
            tsbPublishAll.Enabled = false;
            tsbSaveViews.Enabled = false;
            tsbLoadEntities.Enabled = false;

            var targetViews = lvTargetViews.CheckedItems.Cast<ListViewItem>().Select(i => (Entity)i.Tag).ToList();
            var sourceView = (Entity)lvSourceViews.SelectedItems.Cast<ListViewItem>().First().Tag;

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Saving views...",
                AsyncArgument = new object[] { sourceView, targetViews },
                Work = (bw, evt) =>
                {
                    var args = (object[])evt.Argument;
                    evt.Result = ViewHelper.PropagateLayout((Entity)args[0], (List<Entity>)args[1], Service);
                },
                PostWorkCallBack = evt =>
                {
                    if (((List<Tuple<string, string>>)evt.Result).Count > 0)
                    {
                        var errorDialog = new ErrorList((List<Tuple<string, string>>)evt.Result);
                        errorDialog.ShowDialog();
                    }

                    tsbPublishEntity.Enabled = true;
                    tsbPublishAll.Enabled = true;
                    tsbSaveViews.Enabled = true;
                    tsbLoadEntities.Enabled = true;
                }
            });
        }

        #endregion Save Views

        #region Publish Entity

        private void TsbPublishEntityClick(object sender, EventArgs e)
        {
            if (lvEntities.SelectedItems.Count > 0)
            {
                tsbPublishEntity.Enabled = false;
                tsbPublishAll.Enabled = false;
                tsbSaveViews.Enabled = false;
                tsbLoadEntities.Enabled = false;

                WorkAsync(new WorkAsyncInfo
                {
                    Message = "Publishing entity...",
                    AsyncArgument = lvEntities.SelectedItems[0].Tag,
                    Work = (bw, evt) =>
                    {
                        var pubRequest = new PublishXmlRequest();
                        pubRequest.ParameterXml = string.Format(@"<importexportxml>
                                                           <entities>
                                                              <entity>{0}</entity>
                                                           </entities>
                                                           <nodes/><securityroles/><settings/><workflows/>
                                                        </importexportxml>",
                                                                evt.Argument);

                        Service.Execute(pubRequest);
                    },
                    PostWorkCallBack = evt =>
                    {
                        if (evt.Error != null)
                        {
                            string errorMessage = CrmExceptionHelper.GetErrorMessage(evt.Error, false);
                            MessageBox.Show(this, errorMessage, "Error", MessageBoxButtons.OK,
                                                              MessageBoxIcon.Error);
                        }

                        tsbPublishEntity.Enabled = true;
                        tsbPublishAll.Enabled = true;
                        tsbSaveViews.Enabled = true;
                        tsbLoadEntities.Enabled = true;
                    }
                });
            }
        }

        #endregion Publish Entity

        #endregion Main ToolStrip Handlers

        #region ListViews Handlers

        #region Fill Views

        private void BwFillViewsDoWork(object sender, DoWorkEventArgs e)
        {
            string entityLogicalName = e.Argument.ToString();

            List<Entity> viewsList = ViewHelper.RetrieveViews(entityLogicalName, entitiesCache, Service);
            viewsList.AddRange(ViewHelper.RetrieveUserViews(entityLogicalName, entitiesCache, Service));

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

                #endregion Gestion de l'image associée à la vue

                if (display)
                {
                    // Add view to each list of views (source and target)
                    ListViewItem clonedItem = (ListViewItem)item.Clone();
                    ListViewDelegates.AddItem(lvSourceViews, item);

                    if (view.Contains("iscustomizable") && ((BooleanManagedProperty)view["iscustomizable"]).Value == false
                        && view.Contains("ismanaged") && (bool)view["ismanaged"])
                    {
                        clonedItem.ForeColor = Color.Gray;
                        clonedItem.ToolTipText = "This managed view has not been defined as customizable";
                    }

                    ListViewDelegates.AddItem(lvTargetViews, clonedItem);
                }
            }
        }

        private void BwFillViewsRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Cursor = Cursors.Default;
            gbSourceViews.Enabled = true;
            gbTargetViews.Enabled = true;

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

        #endregion Fill Views

        #region Display View

        private void LvSourceViewsSelectedIndexChanged(object sender, EventArgs e)
        {
            lvSourceViewLayoutPreview.Columns.Clear();

            if (lvSourceViews.SelectedItems.Count > 0)
            {
                lvSourceViews.SelectedIndexChanged -= LvSourceViewsSelectedIndexChanged;
                lvSourceViewLayoutPreview.Items.Clear();
                lvSourceViews.Enabled = false;

                WorkAsync(new WorkAsyncInfo
                {
                    Message = "Loading view layout...",
                    AsyncArgument = lvSourceViews.SelectedItems[0].Tag,
                    Work = (bw, evt) =>
                    {
                        Entity currentSelectedView = (Entity)evt.Argument;
                        string layoutXml = currentSelectedView["layoutxml"].ToString();
                        string fetchXml = currentSelectedView.Contains("fetchxml")
                                              ? currentSelectedView["fetchxml"].ToString()
                                              : string.Empty;
                        string currentEntityDisplayName = ListViewDelegates.GetSelectedItems(lvEntities)[0].Text;

                        EntityMetadata currentEmd = entitiesCache.Find(
                            emd => emd.DisplayName.UserLocalizedLabel.Label == currentEntityDisplayName);

                        XmlDocument layoutDoc = new XmlDocument();
                        layoutDoc.LoadXml(layoutXml);

                        EntityMetadata emdWithItems = MetadataHelper.RetrieveEntity(currentEmd.LogicalName, Service);

                        var headers = new List<ColumnHeader>();

                        var item = new ListViewItem();

                        foreach (XmlNode columnNode in layoutDoc.SelectNodes("grid/row/cell"))
                        {
                            ColumnHeader header = new ColumnHeader();

                            header.Width = int.Parse(columnNode.Attributes["width"].Value);
                            header.Text = MetadataHelper.RetrieveAttributeDisplayName(emdWithItems,
                                                                                      columnNode.Attributes["name"].Value,
                                                                                      fetchXml, Service);
                            headers.Add(header);

                            if (string.IsNullOrEmpty(item.Text))//item.SubItems.Add("preview");
                                item.Text = columnNode.Attributes["width"].Value + "px";
                            else
                                item.SubItems.Add(columnNode.Attributes["width"].Value + "px");
                        }

                        evt.Result = new object[] { headers, item };
                    },
                    PostWorkCallBack = evt =>
                    {
                        if (evt.Error != null)
                        {
                            MessageBox.Show(ParentForm, "Error while displaying view: " + evt.Error.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            var args = (object[])evt.Result;

                            lvSourceViewLayoutPreview.Columns.AddRange(((List<ColumnHeader>)args[0]).ToArray());
                            lvSourceViewLayoutPreview.Items.Add((ListViewItem)args[1]);
                            lvSourceViewLayoutPreview.Enabled = true;
                        }

                        lvSourceViews.SelectedIndexChanged += LvSourceViewsSelectedIndexChanged;
                        lvSourceViews.Enabled = true;
                    }
                });
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

        #endregion Display View

        #endregion ListViews Handlers

        private void LvEntitiesColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvEntities.Sorting = lvEntities.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lvEntities.ListViewItemSorter = new ListViewItemComparer(e.Column, lvEntities.Sorting);
        }

        private void LvSourceViewsDoubleClick(object sender, EventArgs e)
        {
            if (lvSourceViews.SelectedItems.Count == 0)
                return;

            ListViewItem item = lvSourceViews.SelectedItems[0];
            var view = (Entity)item.Tag;

            var dialog = new XmlContentDisplayDialog(view["layoutxml"].ToString());
            dialog.ShowDialog(this);
        }

        private void OnSearchKeyUp(object sender, KeyEventArgs e)
        {
            var entityName = txtSearchEntity.Text;
            if (string.IsNullOrWhiteSpace(entityName))
            {
                lvEntities.BeginUpdate();
                lvEntities.Items.Clear();
                lvEntities.Items.AddRange(listViewItemsCache);
                lvEntities.EndUpdate();
            }
            else
            {
                lvEntities.BeginUpdate();
                lvEntities.Items.Clear();
                var filteredItems = listViewItemsCache
                    .Where(item => item.Text.StartsWith(entityName, StringComparison.OrdinalIgnoreCase))
                    .ToArray();
                lvEntities.Items.AddRange(filteredItems);
                lvEntities.EndUpdate();
            }
        }

        private void TsbCloseThisTabClick(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void TsbPublishAllClick(object sender, EventArgs e)
        {
            tsbPublishEntity.Enabled = false;
            tsbPublishAll.Enabled = false;
            tsbSaveViews.Enabled = false;
            tsbLoadEntities.Enabled = false;

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Publishing all customizations...",
                AsyncArgument = null,
                Work = (bw, evt) =>
                {
                    var pubRequest = new PublishAllXmlRequest();
                    Service.Execute(pubRequest);
                },
                PostWorkCallBack = evt =>
                {
                    if (evt.Error != null)
                    {
                        string errorMessage = CrmExceptionHelper.GetErrorMessage(evt.Error, false);
                        MessageBox.Show(this, errorMessage, "Error", MessageBoxButtons.OK,
                                                          MessageBoxIcon.Error);
                    }

                    tsbPublishEntity.Enabled = true;
                    tsbPublishAll.Enabled = true;
                    tsbSaveViews.Enabled = true;
                    tsbLoadEntities.Enabled = true;
                }
            });
        }
    }
}