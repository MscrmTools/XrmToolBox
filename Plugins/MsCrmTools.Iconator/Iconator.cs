// PROJECT : MsCrmTools.Iconator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.Iconator.AppCode;
using MsCrmTools.Iconator.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace MsCrmTools.Iconator
{
    public partial class Iconator : PluginControlBase
    {
        #region Variables

        private readonly List<Entity> webResourceRetrivedList;

        #endregion Variables

        #region Constructor

        public Iconator()
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

        private void LvWebRessourcesOtherSelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewWebRessourcesOther.SelectedItems.Count > 0)
            {
                labelSizeWr.Text = "Image size: " +
                                   ((WebResourcesManager.WebResourceAndImage)listViewWebRessourcesOther.FocusedItem.Tag)
                                       .Image.Size.ToString();
            }
        }

        private void LvWebRessourcesSelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewWebRessources32.SelectedItems.Count > 0)
            {
                var webRessource = (Entity)listViewWebRessources32.SelectedItems[0].Tag;
            }
        }

        #endregion ListViewItems selection

        #region Main menu actions

        private void DoAction()
        {
            listViewEntities.Items.Clear();
            listViewWebRessources16.Items.Clear();
            listViewWebRessources32.Items.Clear();
            listViewWebRessourcesOther.Items.Clear();

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading Entities...",
                AsyncArgument = null,
                Work = (bw, e) =>
                {
                    var cc = new CrmComponents();

                    // Display retrieved entities
                    var queryEntities = from entityList in MetadataManager.GetEntitiesList(Service)
                                        orderby entityList.DisplayName.UserLocalizedLabel.Label
                                        select entityList;

                    foreach (var entity in queryEntities)
                    {
                        var lvi = new ListViewItem(entity.DisplayName.UserLocalizedLabel.Label) { Tag = entity };
                        lvi.SubItems.Add(entity.LogicalName);
                        cc.Entities.Add(lvi);
                    }

                    bw.ReportProgress(0, "Loading Web resources...");

                    var queryWebResources =
                        from webResourceList in WebResourcesManager.GetWebResourcesOnSolution(Service).Entities
                        orderby webResourceList.GetAttributeValue<string>("name")
                        select webResourceList;

                    foreach (var webResource in queryWebResources)
                    {
                        try
                        {
                            var imageConverted =
                                ImageHelper.ConvertWebResContent(webResource.GetAttributeValue<string>("content"));

                            if (imageConverted == null)
                                continue;

                            if (imageConverted.Size.Height == 32 && imageConverted.Size.Width == 32)
                            {
                                var lvi = new ListViewItem(webResource.GetAttributeValue<string>("name"))
                                {
                                    Tag = webResource,
                                    ImageIndex = cc.Images32.Count
                                };
                                cc.Icons32.Add(lvi);
                                cc.Images32.Add(imageConverted);
                            }
                            else if (imageConverted.Size.Height == 16 && imageConverted.Size.Width == 16)
                            {
                                var lvi = new ListViewItem(webResource.GetAttributeValue<string>("name"))
                                {
                                    Tag = webResource,
                                    ImageIndex = cc.Images16.Count
                                };
                                cc.Icons16.Add(lvi);
                                cc.Images16.Add(imageConverted);
                            }
                            else
                            {
                                var listWrImage = new WebResourcesManager.WebResourceAndImage
                                {
                                    Image = imageConverted,
                                    Webresource = webResource
                                };
                                var lvi = new ListViewItem(webResource.GetAttributeValue<string>("name"))
                                {
                                    Tag = listWrImage,
                                    ImageIndex = cc.ImagesOthers.Count,
                                };
                                cc.IconsOthers.Add(lvi);
                                cc.ImagesOthers.Add(imageConverted);
                            }

                            webResourceRetrivedList.Add(webResource);
                        }
                        catch
                        {
                            // ignored
                        }
                    }

                    e.Result = cc;
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        MessageBox.Show(this, "Error while loading Crm components: " + e.Error.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        var cc = (CrmComponents)e.Result;

                        var imageList16 = new ImageList
                        {
                            ImageSize = new Size(16, 16),
                            ColorDepth = ColorDepth.Depth32Bit
                        };
                        var imageList32 = new ImageList
                        {
                            ImageSize = new Size(32, 32),
                            ColorDepth = ColorDepth.Depth32Bit
                        };
                        var imageListOther = new ImageList { ColorDepth = ColorDepth.Depth32Bit };

                        imageList16.Images.AddRange(cc.Images16.ToArray());
                        imageList32.Images.AddRange(cc.Images32.ToArray());
                        imageListOther.Images.AddRange(cc.ImagesOthers.ToArray());

                        listViewWebRessources16.LargeImageList = imageList16;
                        listViewWebRessources32.LargeImageList = imageList32;
                        listViewWebRessourcesOther.LargeImageList = imageListOther;

                        listViewEntities.Items.AddRange(cc.Entities.ToArray());
                        listViewWebRessources16.Items.AddRange(cc.Icons16.ToArray());
                        listViewWebRessources32.Items.AddRange(cc.Icons32.ToArray());
                        listViewWebRessourcesOther.Items.AddRange(cc.IconsOthers.ToArray());
                    }
                    tsbAddIcon.Enabled = true;
                    tsbApply.Enabled = true;

                    SetEnableState(true);
                },
                ProgressChanged = e => { SetWorkingMessage(e.UserState.ToString()); }
            });
        }

        private void TsbAddIconClick(object sender, EventArgs e)
        {
            var icForm = new ImageCreationForm(Service);
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

        private void TsbConnectClick(object sender, EventArgs e)
        {
            ExecuteMethod(DoAction);
        }

        #region Apply Images to entities

        private void TsbApplyClick(object sender, EventArgs e)
        {
            if (lvMappings.Items.Count <= 0) return;

            var mappingList = (from ListViewItem item in lvMappings.Items select (EntityImageMap)item.Tag).ToList();
            SetEnableState(false);

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Applying images to entities. Please wait...",
                AsyncArgument = mappingList,
                Work = (bw, evt) =>
                {
                    MetadataManager.ApplyImagesToEntities((List<EntityImageMap>)evt.Argument, Service);
                },
                PostWorkCallBack = evt =>
                {
                    SetEnableState(true);

                    if (evt.Error != null)
                    {
                        MessageBox.Show(this, "Error while applying images to entities: " + evt.Error.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        lvMappings.Items.Clear();
                        LvEntitiesSelectedIndexChanged(null, null);
                    }
                }
            });
        }

        #endregion Apply Images to entities

        #endregion Main menu actions

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
                        ((WebResourcesManager.WebResourceAndImage)listViewWebRessourcesOther.SelectedItems[0].Tag)
                            .Webresource["name"].ToString();

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
                    ((EntityMetadata)listViewEntities.SelectedItems[0].Tag).DisplayName.UserLocalizedLabel.Label)
                {
                    Tag = mapping
                };
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
                MessageBox.Show(this, "Please select at least one entity and one image", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
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

        #endregion Map/UnMap

        #region Reset Icons

        private void BtnResetIconClick(object sender, EventArgs e)
        {
            if (listViewEntities.SelectedItems.Count > 0)
            {
                if (DialogResult.Yes ==
                    MessageBox.Show(this, "Are you sure you want to reset icons for this entity?", "Question",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    SetEnableState(false);

                    WorkAsync(new WorkAsyncInfo
                    {
                        Message = "Reseting icons for entity. Please wait...",
                        AsyncArgument = listViewEntities.SelectedItems[0].Tag,
                        Work = (bw, evt) =>
                        {
                            MetadataManager.ResetIcons((EntityMetadata)evt.Argument, Service);
                        },
                        PostWorkCallBack = evt =>
                        {
                            SetEnableState(true);

                            if (evt.Error != null)
                            {
                                MessageBox.Show(this, "Error while reseting icons for entity: " + evt.Error.Message,
                                    "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                LvEntitiesSelectedIndexChanged(null, null);
                            }
                        },
                    });
                }
            }
            else
            {
                MessageBox.Show(this, "No entity selected", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void TsbCloseThisTabClick(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void tsbToggleBackground_Click(object sender, EventArgs e)
        {
            listViewWebRessources16.BackColor = listViewWebRessources16.BackColor == Color.FromName("Window")
                ? Color.Black
                : Color.FromName("Window");

            listViewWebRessources16.ForeColor = listViewWebRessources16.ForeColor == Color.FromName("WindowText")
                ? Color.White
                : Color.FromName("WindowText");

            listViewWebRessources32.BackColor = listViewWebRessources32.BackColor == Color.FromName("Window")
                ? Color.Black
                : Color.FromName("Window");

            listViewWebRessources32.ForeColor = listViewWebRessources32.ForeColor == Color.FromName("WindowText")
                ? Color.White
                : Color.FromName("WindowText");

            listViewWebRessourcesOther.BackColor = listViewWebRessourcesOther.BackColor == Color.FromName("Window")
                ? Color.Black
                : Color.FromName("Window");

            listViewWebRessourcesOther.ForeColor = listViewWebRessourcesOther.ForeColor == Color.FromName("WindowText")
               ? Color.White
               : Color.FromName("WindowText");
        }
    }
}