// PROJECT : MsCrmTools.Iconator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.Iconator.AppCode;
using MsCrmTools.Iconator.Properties;
using XrmToolBox;

namespace MsCrmTools.Iconator
{
    public partial class IconatorControl : UserControl, IMsCrmToolsPluginUserControl
    {
        #region Variables

        /// <summary>
        /// Crm Organization Service
        /// </summary>
        private IOrganizationService service;

        private readonly List<Entity> webResourceRetrivedList;

        #endregion Variables

        #region Constructor

        public IconatorControl()
        {
            InitializeComponent();
            webResourceRetrivedList = new List<Entity>();
        }

        #endregion Constructor

        #region ListViewItems selection

        private void LvEntitiesSelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewEntities.SelectedItems.Count > 0)
            {
                var entity = (EntityMetadata)listViewEntities.SelectedItems[0].Tag;

                if (!string.IsNullOrEmpty(entity.IconSmallName))
                {
                    var queryWrSmall = from wrList in webResourceRetrivedList
                                       where (string)wrList["name"] == entity.IconSmallName
                                       select wrList;

                    foreach (var entityWrS in queryWrSmall)
                    {
                        Image i = ImageHelper.ConvertWebResContent(entityWrS.Attributes["content"].ToString());
                        pictureBox16.Width = i.Width;
                        pictureBox16.Height = i.Height;
                        pictureBox16.Image = i;
                    }
                }
                else
                {
                    pictureBox16.Image = Resources.entity16_custom;
                }

                if (!string.IsNullOrEmpty(entity.IconMediumName))
                {
                    var queryMedium = from wrList in webResourceRetrivedList
                                      where (string)wrList["name"] == entity.IconMediumName
                                      select wrList;

                    foreach (var entityM in queryMedium)
                    {
                        var i = ImageHelper.ConvertWebResContent(entityM.Attributes["content"].ToString());
                        var iResize = ImageHelper.Resize(i, 32, 32);
                        pictureBox32.Width = iResize.Width;
                        pictureBox32.Height = iResize.Height;
                        pictureBox32.Image = iResize;
                    }
                }
                else
                {
                    pictureBox32.Image = Resources.ico_fhe_customentity;
                }
            }
        }

        private void LvWebRessourcesSelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewWebRessources32.SelectedItems.Count > 0)
            {
                var webRessource = (Entity)listViewWebRessources32.SelectedItems[0].Tag;
            }
        }

        private void LvWebRessourcesOtherSelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewWebRessourcesOther.SelectedItems.Count > 0)
            {
                labelSizeWr.Text = "Image size: " +
                    ((WebResourcesManager.WebResourceAndImage)listViewWebRessourcesOther.FocusedItem.Tag).Image.Size.ToString();
            }
        }

        #endregion ListViewItems selection

        #region Main menu actions

        private void TsbConnectClick(object sender, EventArgs e)
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
                DoAction();
            }
        }

        private void DoAction()
        {
            try
            {
                listViewEntities.Items.Clear();

                // Display retrieved entities
                var queryEntities = from entityList in MetadataManager.GetEntitiesList(service)
                                    orderby entityList.DisplayName.UserLocalizedLabel.Label
                                    select entityList;

                foreach (var entity in queryEntities)
                {
                    var lvi = new ListViewItem(entity.DisplayName.UserLocalizedLabel.Label) { Tag = entity };
                    lvi.SubItems.Add(entity.LogicalName);
                    listViewEntities.Items.Add(lvi);
                }

                // Display retrieved web resources
                listViewWebRessources16.Items.Clear();
                listViewWebRessources32.Items.Clear();
                listViewWebRessourcesOther.Items.Clear();

                var queryWebResources = from webResourceList in WebResourcesManager.GetWebResourcesOnSolution(service).Entities
                                        orderby webResourceList.Attributes["name"]
                                        select webResourceList;


                var imageList16 = new ImageList { ImageSize = new Size(16, 16), ColorDepth = ColorDepth.Depth32Bit };
                var imageList32 = new ImageList { ImageSize = new Size(32, 32), ColorDepth = ColorDepth.Depth32Bit };
                var imageListOther = new ImageList { ColorDepth = ColorDepth.Depth32Bit };

                foreach (var webResource in queryWebResources)
                {
                    var imageConverted = ImageHelper.ConvertWebResContent(webResource.Attributes["content"].ToString());

                    if (imageConverted.Size.Height == 32 && imageConverted.Size.Width == 32)
                    {
                        var lvi = new ListViewItem(webResource.Attributes["name"].ToString())
                        {
                            Tag = webResource,
                            ImageIndex = imageList32.Images.Count
                        };
                        listViewWebRessources32.Items.Add(lvi);
                        imageList32.Images.Add(imageConverted);
                    }
                    else if (imageConverted.Size.Height == 16 && imageConverted.Size.Width == 16)
                    {
                        var lvi = new ListViewItem(webResource.Attributes["name"].ToString())
                        {
                            Tag = webResource,
                            ImageIndex = imageList16.Images.Count
                        };
                        listViewWebRessources16.Items.Add(lvi);
                        imageList16.Images.Add(imageConverted);
                    }
                    else
                    {
                        var listWrImage = new WebResourcesManager.WebResourceAndImage
                                              {
                            Image = imageConverted,
                            Webresource = webResource
                        };
                        var lvi = new ListViewItem(webResource.Attributes["name"].ToString())
                        {
                            Tag = listWrImage,
                            ImageIndex = imageListOther.Images.Count,
                        };
                        listViewWebRessourcesOther.Items.Add(lvi);
                        imageListOther.Images.Add(imageConverted);
                    }
                    webResourceRetrivedList.Add(webResource);
                }

                listViewWebRessources32.LargeImageList = imageList32;
                listViewWebRessources16.LargeImageList = imageList16;
                listViewWebRessourcesOther.LargeImageList = imageListOther;

                tsbAddIcon.Enabled = true;
                tsbApply.Enabled = true;

                SetEnableState(true);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error on DoAction method : {0}", ex.InnerException.Message));
            }
        }

        private void TsbAddIconClick(object sender, EventArgs e)
        {
            var icForm = new ImageCreationForm(service);
            icForm.StartPosition = FormStartPosition.CenterParent;
            icForm.ShowDialog();

            if (icForm.WebResourcesCreated.Count > 0)
            {
                var imageList16 = listViewWebRessources16.LargeImageList;
                var imageList32 = listViewWebRessources32.LargeImageList;
                var imageListOther = listViewWebRessourcesOther.LargeImageList;

                foreach (var webResource in icForm.WebResourcesCreated)
                {
                    var imageConverted = ImageHelper.ConvertWebResContent(webResource.Attributes["content"].ToString());

                    if (imageConverted.Size.Height == 32 && imageConverted.Size.Width == 32)
                    {
                        var lvi = new ListViewItem(webResource.Attributes["name"].ToString())
                        {
                            Tag = webResource,
                            ImageIndex = imageList32.Images.Count
                        };
                        listViewWebRessources32.Items.Add(lvi);
                        imageList32.Images.Add(imageConverted);
                    }
                    else if (imageConverted.Size.Height == 16 && imageConverted.Size.Width == 16)
                    {
                        var lvi = new ListViewItem(webResource.Attributes["name"].ToString())
                        {
                            Tag = webResource,
                            ImageIndex = imageList16.Images.Count
                        };
                        listViewWebRessources16.Items.Add(lvi);
                        imageList16.Images.Add(imageConverted);
                    }
                    else
                    {
                        var listWrImage = new WebResourcesManager.WebResourceAndImage
                                              {
                            Image = imageConverted,
                            Webresource = webResource
                        };
                        var lvi = new ListViewItem(webResource.Attributes["name"].ToString())
                        {
                            Tag = listWrImage,
                            ImageIndex = imageListOther.Images.Count,
                        };
                        listViewWebRessourcesOther.Items.Add(lvi);
                        imageListOther.Images.Add(imageConverted);
                    }
                    webResourceRetrivedList.Add(webResource);
                }

                listViewWebRessources32.LargeImageList = imageList32;
                listViewWebRessources16.LargeImageList = imageList16;
                listViewWebRessourcesOther.LargeImageList = imageListOther;
            }
        }

        #region Apply Images to entities

        private void TsbApplyClick(object sender, EventArgs e)
        {
            if (lvMappings.Items.Count <= 0) return;

            var mappingList = (from ListViewItem item in lvMappings.Items select (EntityImageMap)item.Tag).ToList();

            var bWorker = new BackgroundWorker();
            bWorker.DoWork += BWorkerDoWork;
            bWorker.RunWorkerCompleted += BWorkerRunWorkerCompleted;
            bWorker.RunWorkerAsync(mappingList);

            lblWaiting.Text = "Applying images to entities. Please wait...";
            panelWaiting.Visible = true;
            Cursor = Cursors.WaitCursor;
            SetEnableState(false);
        }

        private void BWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            MetadataManager.ApplyImagesToEntities((List<EntityImageMap>)e.Argument, service);
        }

        private void BWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            panelWaiting.Visible = false;
            Cursor = Cursors.Default;
            SetEnableState(true);

            if (e.Error != null)
            {
                MessageBox.Show(this, "Error while applying images to entities: " + e.Error.Message, "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                lvMappings.Items.Clear();
            }
        }

        #endregion Apply Images to entities

      #endregion

        #region Map/UnMap

        private void BtnMapClick(object sender, EventArgs e)
        {
            if (listViewEntities.SelectedItems.Count > 0 &&
                (listViewWebRessources16.SelectedItems.Count > 0 || listViewWebRessources32.SelectedItems.Count > 0 ||
                 listViewWebRessourcesOther.SelectedItems.Count > 0))
            {
                var selectedEntity = (EntityMetadata)listViewEntities.SelectedItems[0].Tag;

                var mapping = new EntityImageMap { Entity = selectedEntity };

                if (listViewWebRessources16.SelectedItems.Count > 0)
                {
                    mapping.WebResourceName = ((Entity)listViewWebRessources16.SelectedItems[0].Tag)["name"].ToString();
                    mapping.ImageSize = 16;
                }
                else if (listViewWebRessources32.SelectedItems.Count > 0)
                {
                    mapping.WebResourceName = ((Entity)listViewWebRessources32.SelectedItems[0].Tag)["name"].ToString();
                    mapping.ImageSize = 32;
                }
                else
                {
                    mapping.WebResourceName =
                        ((WebResourcesManager.WebResourceAndImage)listViewWebRessourcesOther.SelectedItems[0].Tag).Webresource["name"].ToString();

                    var issDialog = new ImageSizeSelectionDialog { StartPosition = FormStartPosition.CenterParent };
                    if (issDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        mapping.ImageSize = issDialog.ImageSizeSelected;
                    }
                    else
                    {
                        return;
                    }
                }

                var item = new ListViewItem(
                    ((EntityMetadata)listViewEntities.SelectedItems[0].Tag).DisplayName.UserLocalizedLabel.Label) { Tag = mapping };
                item.SubItems.Add(mapping.ImageSize + "x" + mapping.ImageSize);
                item.SubItems.Add(mapping.WebResourceName);

                foreach (ListViewItem existingItem in lvMappings.Items)
                {
                    if (existingItem.Text == item.Text
                        && existingItem.SubItems[1].Text == item.SubItems[1].Text)
                    {
                        MessageBox.Show(this, "There is already a mapping for this entity and this size", "Warning",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                }


                lvMappings.Items.Add(item);
            }
            else
            {
                MessageBox.Show(this, "Please select at least one entity and one image", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnUnmapClick(object sender, EventArgs e)
        {
            if (lvMappings.SelectedItems.Count > 0)
            {
                lvMappings.Items.Remove(lvMappings.SelectedItems[0]);
            }
        }

        private void LvWebRessourcesMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listViewWebRessources16.SelectedItems.Count > 0
            || listViewWebRessources32.SelectedItems.Count > 0
                || listViewWebRessourcesOther.SelectedItems.Count > 0)
            {
                BtnMapClick(null, null);
            }
        }

        #endregion

        #region Reset Icons

        private void BtnResetIconClick(object sender, EventArgs e)
        {
            if (listViewEntities.SelectedItems.Count > 0)
            {
                if (DialogResult.Yes == MessageBox.Show(this, "Are you sure you want to reset icons for this entity?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    var bResetWorker = new BackgroundWorker();
                    bResetWorker.DoWork += BResetWorkerDoWork;
                    bResetWorker.RunWorkerCompleted += BResetWorkerRunWorkerCompleted;
                    bResetWorker.RunWorkerAsync(listViewEntities.SelectedItems[0].Tag);

                    lblWaiting.Text = "Reseting icons for entity. Please wait...";
                    panelWaiting.Visible = true;
                    Cursor = Cursors.WaitCursor;
                    SetEnableState(false);
                }
            }
            else
            {
                MessageBox.Show(this, "No entity selected", "Warning",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BResetWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            MetadataManager.ResetIcons((EntityMetadata)e.Argument, service);
        }

        private void BResetWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            panelWaiting.Visible = false;
            Cursor = Cursors.Default;
            SetEnableState(true);

            if (e.Error != null)
            {
                MessageBox.Show(this, "Error while reseting icons for entity: " + e.Error.Message, "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                LvEntitiesSelectedIndexChanged(null, null);
            }
        }

        #endregion Reset Icons

        #region Others

        private void BtnPreviewClick(object sender, EventArgs e)
        {
            if (listViewWebRessourcesOther.FocusedItem != null)
            {
                var img =
                    ((WebResourcesManager.WebResourceAndImage)listViewWebRessourcesOther.FocusedItem.Tag).Image;

                var preview = new ImagePreview(img) { StartPosition = FormStartPosition.CenterParent };
                preview.ShowDialog();
            }
        }

        private void SetEnableState(bool enabled)
        {
            mainMenu.Enabled = enabled;
            groupBoxCurrentIcon.Enabled = enabled;
            gbEntities.Enabled = enabled;
            splitContainer1.Enabled = enabled;
        }

        private void TabControlWebResourceSelectedIndexChanged(object sender, EventArgs e)
        {
            listViewWebRessources16.SelectedItems.Clear();
            listViewWebRessources32.SelectedItems.Clear();
            listViewWebRessourcesOther.SelectedItems.Clear();
        }


        #endregion Others

        private void MainFormResize(object sender, EventArgs e)
        {
            panelWaiting.Left = Width / 2 - panelWaiting.Width / 2;
            panelWaiting.Top = Height / 2 - panelWaiting.Height / 2;
        }

        public IOrganizationService Service
        {
            get { return service; }
        }

        public Image PluginLogo
        {
            get { return imageList1.Images[0]; }
        }

        public event EventHandler OnRequestConnection;
        public event EventHandler OnCloseTool;

        public void UpdateConnection(IOrganizationService newService, string actionName = "", object parameter = null)
        {
            service = newService;

            if (actionName == "Load")
            {
                DoAction();
            }
        }

        private void TsbCloseThisTabClick(object sender, EventArgs e)
        {
            const string message = "Are your sure you want to close this tab?";
            if (MessageBox.Show(message, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                OnCloseTool(this, null);
        }
    }
}
