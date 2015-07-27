// PROJECT : MsCrmTools.WebResourcesManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using MsCrmTools.WebResourcesManager.AppCode;
using MsCrmTools.WebResourcesManager.DelegatesHelpers;
using MsCrmTools.WebResourcesManager.Forms;
using MsCrmTools.WebResourcesManager.Forms.Solutions;
using MsCrmTools.WebResourcesManager.UserControls;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using CrmExceptionHelper = XrmToolBox.CrmExceptionHelper;

namespace MsCrmTools.WebResourcesManager
{
    public partial class WebResourcesManager : PluginControlBase
    {
        #region Variables

        /// <summary>
        /// Scripts Manager
        /// </summary>
        WebResourceManager wrManager;

        /// <summary>
        /// List of invalid filenames when creating or importing web resources
        /// </summary>
        private List<string> invalidFilenames;

        const string OPENFILE_TITLE_MASK = "Select the {0} to replace the existing web resource";

        private string currentFolderForFiles;

        #endregion Variables

        #region Constructor

        public WebResourcesManager()
        {
            InitializeComponent();

            toolStripScriptContent.Visible = false;
        }

        #endregion Constructor

        #region IMsCrmToolsPluginUserControl Members

        //    wrManager = new WebResourceManager(newService);

        //void IMsCrmToolsPluginUserControl.UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter = null)
        //{
        //    service = newService;
                        
        //    var nodesList = new List<TreeNode>();
        //    TreeViewHelper.GetNodes(nodesList, tvWebResources, true);

        //    switch (actionName)
        //    {
        //        case "LoadWebResources":
        //            {
        //                LoadWebResourcesGeneral(null);
        //            }
        //            break;
        //        case "LoadWebResourcesFromSolution":
        //            {
        //                var sPicker = new SolutionPicker(service) {StartPosition = FormStartPosition.CenterParent};
        //                if (sPicker.ShowDialog(this) == DialogResult.OK)
        //                {
        //                    LoadWebResourcesGeneral(sPicker.SelectedSolution);
        //                }
        //            }
        //            break;
        //        case "Update":
        //            {
        //                UpdateWebResources(false, nodesList);
        //            }
        //            break;
        //        case "UpdateAndPublish":
        //            {
        //                UpdateWebResources(true, nodesList);
        //            }
        //            break;
        //        case "UpdateAndPublishAndAdd":
        //            {
        //                UpdateWebResources(true, nodesList, true);
        //            }
        //            break;
        //        case "Delete":
        //            {
        //                wrManager.DeleteWebResource(((WebResource)nodesList[0].Tag).WebResourceEntity);
        //                tvWebResources.Nodes.Remove(nodesList[0]);
        //            }
        //            break;
        //    }
        //}

        #endregion IMsCrmToolsPluginUserControl Members

        #region Methods
       
        #region CRM - Load web resources

        private void LoadWebResourcesToolStripMenuItemClick(object sender, EventArgs e)
        {
            tvWebResources.Nodes.Clear();

            ExecuteMethod<Entity>(LoadWebResourcesGeneral,null);
        }

        private void LoadWebResourcesFromASpecificSolutionToolStripMenuItemClick(object sender, EventArgs e)
        {
            ExecuteMethod(LoadWebResourceFromASpecificSolution);
        }

        public void LoadWebResourceFromASpecificSolution()
        {
            wrManager = new WebResourceManager(Service);
            var sPicker = new SolutionPicker(Service) { StartPosition = FormStartPosition.CenterParent };
            if (sPicker.ShowDialog(this) == DialogResult.OK)
            {
                LoadWebResourcesGeneral(sPicker.SelectedSolution);
            }
        }

        public void LoadWebResourcesGeneral(Entity specificSolution)
        {
            wrManager = new WebResourceManager(Service);
            tvWebResources.Nodes.Clear();

            var dialog = new WebResourceTypeSelectorDialog();
            if (dialog.ShowDialog(ParentForm) == DialogResult.OK)
            {
                var settings = new LoadCrmResourcesSettings
                {
                    SolutionId = specificSolution != null ? specificSolution.Id : Guid.Empty,
                    SolutionName = specificSolution != null ?specificSolution.GetAttributeValue<string>("friendlyname") : "",
                    SolutionVersion = specificSolution != null ?specificSolution.GetAttributeValue<string>("version") : "",
                    Types = dialog.TypesToLoad
                };

                SetWorkingState(true);
                tvWebResources.Nodes.Clear();

                WorkAsync("Loading web resources...",
                    e =>
                    {
                        Guid solutionId = e.Argument != null ? ((LoadCrmResourcesSettings)e.Argument).SolutionId : Guid.Empty;

                        RetrieveWebResources(solutionId, ((LoadCrmResourcesSettings)e.Argument).Types);

                        e.Result = e.Argument;
                    },
                    e =>
                    {
                        if (e.Error != null)
                        {
                            string errorMessage = CrmExceptionHelper.GetErrorMessage(e.Error, true);
                            MessageBox.Show(this, errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            tvWebResources.Enabled = true;

                            var currentSettings = (LoadCrmResourcesSettings)e.Result;
                            tssCurrentlyLoadedSolution.Visible = currentSettings.SolutionId != Guid.Empty;
                            tslCurrentlyLoadedSolution.Visible = currentSettings.SolutionId != Guid.Empty;
                            tslCurrentlyLoadedSolution.Text = string.Format("Solution loaded: {0} - v{1}", currentSettings.SolutionName, currentSettings.SolutionVersion);
                        }

                        tvWebResources.ExpandAll();
                        tvWebResources.TreeViewNodeSorter = new NodeSorter();
                        tvWebResources.Sort();
                        TvWebResourcesAfterSelect(null, null);

                        tsbClear.Visible = true;

                        SetWorkingState(false);
                    },
                    settings);
            }
        }

        private void RetrieveWebResources(Guid solutionId, List<int> types)
        {
            EntityCollection scripts = wrManager.RetrieveWebResources(solutionId, types);

            foreach (Entity script in scripts.Entities)
            {
                var wrObject = new WebResource(script);

                string[] nameParts = script["name"].ToString().Split('/');

                AddNode(nameParts, 0, tvWebResources, wrObject);
            }
        }

        private void AddNode(string[] nameParts, int index, object parent, WebResource wrObject)
        {
            if (index == 0)
            {
                if (((TreeView)parent).Nodes.Find(nameParts[index], false).Length == 0)
                {
                    var node = new TreeNode(nameParts[index]);
                    node.Name = node.Text;

                    if (index == nameParts.Length - 1)
                    {
                        node.Tag = wrObject;

                        int imageIndex = ((OptionSetValue)wrObject.WebResourceEntity["webresourcetype"]).Value + 1;
                        node.ImageIndex = imageIndex;
                        node.SelectedImageIndex = imageIndex;
                    }
                    else
                    {
                        node.ImageIndex = 0;
                        node.SelectedImageIndex = 0;
                    }

                    TreeViewDelegates.AddNode((TreeView)parent, node);

                    AddNode(nameParts, index + 1, node, wrObject);
                }
                else
                {
                    AddNode(nameParts, index + 1, ((TreeView)parent).Nodes.Find(nameParts[index], false)[0], wrObject);
                }
            }
            else if (index < nameParts.Length)
            {
                if (((TreeNode)parent).Nodes.Find(nameParts[index], false).Length == 0)
                {
                    var node = new TreeNode(nameParts[index]);
                    node.Name = node.Text;

                    if (index == nameParts.Length - 1)
                    {
                        node.Tag = wrObject;
                        int imageIndex = ((OptionSetValue)wrObject.WebResourceEntity["webresourcetype"]).Value + 1;
                        node.ImageIndex = imageIndex;
                        node.SelectedImageIndex = imageIndex;
                    }
                    else
                    {
                        if (index == 0)
                        {
                            node.ImageIndex = 0;
                            node.SelectedImageIndex = 0;
                        }
                        else
                        {
                            node.ImageIndex = 1;
                            node.SelectedImageIndex = 1;
                        }
                    }

                    TreeViewDelegates.AddNode((TreeNode)parent, node);
                    AddNode(nameParts, index + 1, node, wrObject);
                }
                else
                {
                    AddNode(nameParts, index + 1, ((TreeNode)parent).Nodes.Find(nameParts[index], false)[0], wrObject);
                }
            }
        }

        #endregion CRM - Load web resources

        #region CRM - Update web resources

        private void UpdateCheckedWebResourcesToolStripMenuItemClick(object sender, EventArgs e)
        {
            DoUpdateWebResources(false, false);
        }

        private void UpdateAndPublishCheckedWebResourcesToolStripMenuItemClick(object sender, EventArgs e)
        {
            DoUpdateWebResources(true, false);
        }

        private void UpdatePublishAndAddToSolutionToolStripMenuItemClick(object sender, EventArgs e)
        {
            DoUpdateWebResources(true, true);
        }

        private void SaveToCrmServerToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (TreeViewHelper.CheckOnlyThisNode(tvWebResources))
                return;

            DoUpdateWebResources(false, false);
        }

        private void SaveAndPublishToCrmServerToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (TreeViewHelper.CheckOnlyThisNode(tvWebResources))
                return;

            DoUpdateWebResources(true, false);
        }

        private void SavePublishAndAddToSolutionToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (TreeViewHelper.CheckOnlyThisNode(tvWebResources))
                return;

            DoUpdateWebResources(true, true);
        }

        private void DoUpdateWebResources(bool publish, bool addToSolution)
        {
            ExecuteMethod(DoUpdate, new []{publish, addToSolution});
        }

        private void DoUpdate(bool[] options)
        {
            try
            {
                wrManager = new WebResourceManager(Service);
            
                // Retrieve checked web resources
                var nodesList = new List<TreeNode>();
                TreeViewHelper.GetNodes(nodesList, tvWebResources, true);

                if (nodesList.Count == 0)
                {
                    MessageBox.Show(this, "Please check at least one web resource before using this function", "Warning",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                UpdateWebResources(options[0], nodesList, options[1]);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, "An error occured: " + error.ToString(), "error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void UpdateWebResources(bool publish, IEnumerable<TreeNode> nodes, bool addToSolution = false)
        {
            var solutionUniqueName = string.Empty;
            if (addToSolution)
            {
                var sPicker = new SolutionPicker(Service) { StartPosition = FormStartPosition.CenterParent };

                if (sPicker.ShowDialog(this) == DialogResult.OK)
                {
                    solutionUniqueName = sPicker.SelectedSolution["uniquename"].ToString();
                }
                else
                {
                    return;
                }
            }

            SetWorkingState(true);
            var parameters = new object[] { nodes, publish, solutionUniqueName };

            WorkAsync("Updating web resources...",
                (bw, e) =>
                {
                    var webResourceManager = new WebResourceManager(Service);
                    var idsToPublish = new List<Guid>();
                    var localNodes = (IEnumerable<TreeNode>)((object[])e.Argument)[0];

                    var wrDifferentFromServer = new List<TreeNode>();

                    foreach (TreeNode node in localNodes.Where(n => n.Tag != null))
                    {
                        var wr = (WebResource)node.Tag;
                        Entity serverVersion = null;
                        if (wr.WebResourceEntity != null && wr.WebResourceEntity.Id != Guid.Empty)
                        {
                            serverVersion = webResourceManager.RetrieveWebResource(wr.WebResourceEntity.Id);
                        }

                        if (serverVersion != null && serverVersion.GetAttributeValue<string>("content") != wr.InitialBase64)
                        {
                            wrDifferentFromServer.Add(node);
                        }
                        else
                        {
                            bw.ReportProgress(1, string.Format("Updating {0}...", wr.WebResourceEntity["name"]));

                            wr.WebResourceEntity.Id = webResourceManager.UpdateWebResource(wr.WebResourceEntity);
                            idsToPublish.Add(wr.WebResourceEntity.Id);
                            wr.InitialBase64 = wr.WebResourceEntity.GetAttributeValue<string>("content");
                        }
                    }

                    if (wrDifferentFromServer.Count > 0)
                    {
                        if (
                            CommonDelegates.DisplayMessageBox(null,
                                string.Format(
                                    "The following web resources were updated on the server by someone else:\r\n{0}\r\n\r\nAre you sure you want to update them with your content?",
                                    String.Join("\r\n", wrDifferentFromServer.Select(r => ((WebResource)r.Tag).WebResourceEntity.GetAttributeValue<string>("name")))),
                                "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            foreach (var resource in wrDifferentFromServer)
                            {
                                var wr = (WebResource)resource.Tag;

                                bw.ReportProgress(1, string.Format("Updating {0}...", wr.WebResourceEntity["name"]));

                                wr.WebResourceEntity.Id = webResourceManager.UpdateWebResource(wr.WebResourceEntity);
                                idsToPublish.Add(wr.WebResourceEntity.Id);
                                wr.InitialBase64 = wr.WebResourceEntity.GetAttributeValue<string>("content");
                            }
                        }
                    }

                    // if publish
                    if ((bool)((object[])e.Argument)[1] && wrDifferentFromServer.Count <= localNodes.Count())
                    {
                        bw.ReportProgress(2, "Publishing web resources...");

                        webResourceManager.PublishWebResources(idsToPublish);
                    }

                    if (((object[])e.Argument)[2].ToString().Length > 0 && wrDifferentFromServer.Count < localNodes.Count())
                    {
                        bw.ReportProgress(3, "Adding web resources to solution...");

                        webResourceManager.AddToSolution(idsToPublish, ((object[])e.Argument)[2].ToString());
                    }
                },
                e =>
                {
                    if (e.Error != null)
                    {
                        MessageBox.Show(this, "An error occured: " + e.Error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (tslResourceName.Text.Contains(" (not published)"))
                    {
                        tslResourceName.Text = tslResourceName.Text.Replace(" (not published)", "");
                        tslResourceName.ForeColor = Color.Black;
                    }

                    SetWorkingState(false);
                },
                e=>SetWorkingMessage(e.UserState.ToString()),
                parameters);
        }

        #endregion CRM - Update web resources

        #region DISK - Load web resources

        private void LoadWebResourcesToolStripMenuItem1Click(object sender, EventArgs e)
        {
            try
            {
                // Let the user decides where to find files
                // Let the user decides where to find files
                var fbd = new CustomFolderBrowserDialog(true);

                if (!string.IsNullOrEmpty(currentFolderForFiles))
                {
                    fbd.FolderPath = currentFolderForFiles;
                }
                
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    var extensionsToLoad = fbd.ExtensionsToLoad;
                    currentFolderForFiles = fbd.FolderPath;
                    tvWebResources.Nodes.Clear();
                    invalidFilenames = new List<string>();

                    var di = new DirectoryInfo(fbd.FolderPath);

                    foreach (DirectoryInfo diChild in di.GetDirectories("*_", SearchOption.TopDirectoryOnly))
                    {
                        if (WebResource.IsInvalidName(diChild.Name))
                        {
                            invalidFilenames.Add(diChild.FullName);
                            continue;
                        }

                        var rootFolderNode = new TreeNode(diChild.Name) { ImageIndex = 0, Tag = diChild.FullName };

                        tvWebResources.Nodes.Add(rootFolderNode);

                        TreeViewHelper.CreateFolderStructure(rootFolderNode, diChild, invalidFilenames, extensionsToLoad);
                    }

                    foreach (FileInfo fiChild in di.GetFiles("*.*", SearchOption.TopDirectoryOnly))
                    {
                        if (fiChild.Extension.Length == 0)
                        {
                            invalidFilenames.Add(fiChild.FullName);
                            continue;
                        }

                        if (WebResource.IsInvalidName(fiChild.Name) || !WebResource.ValidExtensions.Contains(fiChild.Extension.Remove(0, 1).ToLower()))
                        {
                            invalidFilenames.Add(fiChild.FullName);
                            continue;
                        }

                        if (extensionsToLoad.Contains(fiChild.Extension))
                        {
                            TreeViewHelper.CreateWebResourceNode(fiChild, tvWebResources);
                        }
                    }

                    tvWebResources.ExpandAll();
                    tvWebResources.TreeViewNodeSorter = new NodeSorter();
                    tvWebResources.Sort();

                    tsbClear.Visible = true;

                    if (invalidFilenames.Count > 0)
                    {
                        var errorDialog = new InvalidFileListDialog(invalidFilenames)
                        {
                            StartPosition =
                                FormStartPosition.CenterParent
                        };
                        errorDialog.ShowDialog(this);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(this, "Error while loading web resources: " + error.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void refreshFromDiskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedItem = tvWebResources.SelectedNode;
            
            if (selectedItem != null)
            {
                var tag = selectedItem.Tag;
                if (tag is string)
                {
                    invalidFilenames = new List<string>();

                    TreeViewHelper.UpdateFolderStructure(selectedItem, new DirectoryInfo(tag.ToString()),invalidFilenames, null);

                    if (invalidFilenames.Count > 0)
                    {
                        var errorDialog = new InvalidFileListDialog(invalidFilenames)
                        {
                            StartPosition =
                                FormStartPosition.CenterParent
                        };
                        errorDialog.ShowDialog(this);
                    }
                }
                else
                {
                    var resource = tag as WebResource;
                    if (resource != null)
                    {
                        var wr = resource;
                        wr.WebResourceEntity["content"] = Convert.ToBase64String(File.ReadAllBytes(wr.FilePath));
                        TvWebResourcesAfterSelect(tvWebResources, new TreeViewEventArgs(selectedItem));
                    }
                }
            }
        }

        #endregion DISK - Load web resources

        #region DISK - Save web resources

        private void SaveCheckedWebResourcesToDiskToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                var nodes = new List<TreeNode>();
                TreeViewHelper.GetNodes(nodes, tvWebResources, true);

                SaveWebResourcesToDisk(nodes, true);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, "Error while saving web resources: " + error.Message, "Error",
                             MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveAllWebResourcesToDiskToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                var nodes = new List<TreeNode>();
                TreeViewHelper.GetNodes(nodes, tvWebResources, true);

                SaveWebResourcesToDisk(nodes);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, "Error while saving web resources: " + error.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveWebResourcesToDisk(IEnumerable<TreeNode> nodes, bool withRoot = false)
        {
            var fbd = new CustomFolderBrowserDialog(true, false);

            if (!string.IsNullOrEmpty(Properties.Settings.Default.LastFolderUsed))
            {
                fbd.FolderPath = Properties.Settings.Default.LastFolderUsed;
            }

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.LastFolderUsed = fbd.FolderPath;
                Properties.Settings.Default.Save();
                foreach (var node in nodes)
                {
                    if (node.Tag != null && ((WebResource)node.Tag).WebResourceEntity != null)
                    {
                        var webResource = ((WebResource)node.Tag).WebResourceEntity;

                        if (webResource.Contains("content") && webResource["content"].ToString().Length > 0)
                        {
                            string[] partPath = webResource["name"].ToString().Split('/');
                            string path = fbd.FolderPath;

                            if (withRoot) {
                                for (int i = 0; i < partPath.Length - 1; i++) {
                                    path = Path.Combine(path, partPath[i]);

                                    if (!Directory.Exists(path))
                                        Directory.CreateDirectory(path);
                                }
                            }

                            path = Path.Combine(path, partPath[partPath.Length - 1]);
                        
                            byte[] bytes = Convert.FromBase64String(webResource["content"].ToString());
                            File.WriteAllBytes(path, bytes);

                            ((WebResource) node.Tag).FilePath = path;
                        }
                    }
                }
            }
        }

        #endregion DISK - Save web resources

        #region CRM/DISK - Delete Web resources

        private void DeleteToolStripMenuItemClick(object sender, EventArgs e)
        {
            ExecuteMethod(DeleteWebResource);
        }

        private void DeleteWebResource()
        {
            try
            {
                if (TreeViewHelper.CheckOnlyThisNode(tvWebResources))
                    return;

                TreeNode selectedNode = tvWebResources.SelectedNode;

                if (selectedNode.ImageIndex > 1)
                {
                    if (DialogResult.Yes == MessageBox.Show(this,
                                                            "This web resource will be deleted from the Crm server if you are connected and this web resource exists.\r\nAre you sure you want to delete this web resource?",
                                                            "Question",
                                                            MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Question))
                    {
                        var wr = selectedNode.Tag as WebResource;

                        if (wr != null && wr.WebResourceEntity != null && wr.WebResourceEntity.Id != Guid.Empty)
                        {
                            WorkAsync("Deleting web resource...",
                                e => wrManager.DeleteWebResource((Entity)e.Argument),
                                e =>
                                {
                                    if (e.Error != null)
                                    {
                                        MessageBox.Show(this, "An error occured: " + e.Error, "Error",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                    {
                                        tvWebResources.Nodes.Remove(selectedNode);
                                    }

                                    SetWorkingState(false);

                                },
                                wr.WebResourceEntity);
                        }
                        else
                        {
                            tvWebResources.Nodes.Remove(selectedNode);
                        }
                    }
                }
                else
                {
                    tvWebResources.Nodes.Remove(selectedNode);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(this, "Error while deleting web resource: " + error.Message, "Error",
                             MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion CRM/DISK - Delete Web resources

        #region TREEVIEW - Manage content

        private void TsbNewRootClick(object sender, EventArgs e)
        {
            TreeViewHelper.AddRoot(tvWebResources, this);
        }

        private void AddNewFolderToolStripMenuItemClick(object sender, EventArgs e)
        {
            TreeViewHelper.AddFolder(tvWebResources, this);
        }

        private void AddNewEmptyWebRessource(object sender, EventArgs e)
        {
            string extension = string.Empty;

            switch (((ToolStripMenuItem) sender).Name)
            {
                case "hTMLToolStripMenuItem":
                    extension = "html";
                    break;
                case "cSSToolStripMenuItem":
                    extension = "css";
                    break;
                case "scriptToolStripMenuItem":
                    extension = "js";
                    break;
                case "dataToolStripMenuItem":
                    extension = "xml";
                    break;
                case "xSLTToolStripMenuItem":
                    extension = "xslt";
                    break;
                default:
                    {
                        MessageBox.Show(this, "Can't determine web resource type requested!", "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
            }

            TreeViewHelper.CreateEmptyWebResource(extension, tvWebResources, this);
        }

        private void AddNewWebResourceToolStripMenuItemClick(object sender, EventArgs e)
        {
            TreeViewHelper.AddExistingWebResource(tvWebResources, this);
        }

        private void UpdateFromDiskToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (DialogResult.OK ==
                MessageBox.Show(this,
                                "You will now have to select a directory. Each web resources in the selected folder with a corresponding file in the directory selected (same name) will be updated with the local file content",
                                "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
            {
                string message = TreeViewHelper.UpdateNodesContentWithLocalFiles(tvWebResources.SelectedNode.Nodes);

                if (message.Length > 0)
                {
                    MessageBox.Show(this, message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } 
            }
        }

        private void PropertiesToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (TreeViewHelper.CheckOnlyThisNode(tvWebResources))
                return;

            tvWebResources.SelectedNode.Tag = ((WebResource) tvWebResources.SelectedNode.Tag).ShowProperties(Service, this);
        }

        private void CopyWebResourceNameToClipboardToolStripMenuItemClick(object sender, EventArgs e)
        {
            string name = tvWebResources.SelectedNode.Text;
            TreeNode parentNode = tvWebResources.SelectedNode.Parent;

            while (parentNode != null)
            {
                name = parentNode.Text + "/" + name;
                parentNode = parentNode.Parent;
            }
            
            Clipboard.SetText(name);

            MessageBox.Show(this,
                            string.Format("Web resource name ({0}) copied to clipboard", name),
                            "Information",
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Information);
        }

        private void getLatestVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedWr = (WebResource) tvWebResources.SelectedNode.Tag;
            if (selectedWr.WebResourceEntity == null || selectedWr.WebResourceEntity.Id == Guid.Empty)
            {
                MessageBox.Show(ParentForm,
                    "This web resource has not been synchronized to CRM server yet. You cannot get latest version",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            WorkAsync("",
                evt =>
                {
                    var wrm = new WebResourceManager(Service);
                    var wr = wrm.RetrieveWebResource((Guid) evt.Argument);

                    evt.Result = wr;
                },
                evt =>
                {
                    var wr = (Entity) evt.Result;

                    ((WebResource) tvWebResources.SelectedNode.Tag).WebResourceEntity = wr;
                    ((WebResource) tvWebResources.SelectedNode.Tag).InitialBase64 = wr.GetAttributeValue<string>("content");
                    TvWebResourcesAfterSelect(null, null);
                },
                selectedWr.WebResourceEntity.Id);
        }

        private void TbsClearTreeClick(object sender, EventArgs e)
        {
            tvWebResources.Nodes.Clear();
            panelControl.Controls.Clear();
            tslResourceName.Text = "";
            tslCurrentlyLoadedSolution.Text = "";
            toolStripScriptContent.Visible = false;
            tsbClear.Visible = false;
        }

        #endregion TREEVIEW - Manage content

        #region WEBRESOURCE CONTENT - Actions

        private void FileMenuSaveClick(object sender, EventArgs e)
        {
            if (((TabControl)(Parent).Parent).SelectedTab != Parent)
            {
                ((ToolStripDropDownItem)((ToolStrip)(((TabControl)(Parent).Parent).SelectedTab.Controls.Find("toolStripScriptContent", true)[0])).Items[0]).DropDownItems[0].PerformClick();
                return;
            }

            string content = ((IWebResourceControl)panelControl.Controls[0]).GetBase64WebResourceContent();

            ((WebResource)tvWebResources.SelectedNode.Tag).WebResourceEntity["content"] = content;

            fileMenuSave.Enabled = false;
            fileMenuUpdateAndPublish.Enabled = true;

            if (tslResourceName.Text.Contains(" (not saved)"))
            {
                tslResourceName.Text = tslResourceName.Text.Replace(" (not saved)", " (not published)");
                tslResourceName.ForeColor = Color.Blue;
            }
        }

        private void FileMenuReplaceClick(object sender, EventArgs e)
        {
            if (((TabControl)(Parent).Parent).SelectedTab != Parent)
            {
                ((ToolStripDropDownItem)((ToolStrip)(((TabControl)(Parent).Parent).SelectedTab.Controls.Find("toolStripScriptContent", true)[0])).Items[0]).DropDownItems[1].PerformClick();
                return;
            }

            try
            {
                var ctrl = ((IWebResourceControl)panelControl.Controls[0]);

                using (var ofd = new OpenFileDialog()) {
                    #region OpenFileDialog properties

                    OpenFileDialogSettings(ctrl, ofd);

                    #endregion OpenFileDialog properties

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        ctrl.ReplaceWithNewFile(ofd.FileName);
                    }
                }
            }
            catch (AccessViolationException error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        private void OpenFileDialogSettings(IWebResourceControl ctrl, OpenFileDialog ofd)
        {
            switch (ctrl.GetWebResourceType()) {
                case Enumerations.WebResourceType.Gif: {
                    ofd.Title = string.Format(OPENFILE_TITLE_MASK, "image");
                    ofd.Filter = "Gif file (*.gif)|*.gif";
                }
                    break;
                case Enumerations.WebResourceType.Jpg: {
                    ofd.Title = string.Format(OPENFILE_TITLE_MASK, "image");
                    ofd.Filter = "JPG file (*.jpg)|*.jpg";
                }
                    break;
                case Enumerations.WebResourceType.Png: {
                    ofd.Title = string.Format(OPENFILE_TITLE_MASK, "image");
                    ofd.Filter = "PNG file (*.png)|*.png";
                }
                    break;
                case Enumerations.WebResourceType.Ico: {
                    ofd.Title = string.Format(OPENFILE_TITLE_MASK, "icon");
                    ofd.Filter = "ICO file (*.ico)|*.ico";
                }
                    break;
                case Enumerations.WebResourceType.Script: {
                    ofd.Title = string.Format(OPENFILE_TITLE_MASK, "script file");
                    ofd.Filter = "Javascript file (*.js)|*.js";
                }
                    break;
                case Enumerations.WebResourceType.WebPage: {
                    ofd.Title = string.Format(OPENFILE_TITLE_MASK, "web page");
                    ofd.Filter = "Web page (*.html,*.htm)|*.html;*.htm";
                }
                    break;
                case Enumerations.WebResourceType.Css: {
                    ofd.Title = string.Format(OPENFILE_TITLE_MASK, "css file");
                    ofd.Filter = "Stylesheet (*.css)|*.css";
                }
                    break;
                case Enumerations.WebResourceType.Xsl: {
                    ofd.Title = string.Format(OPENFILE_TITLE_MASK, "xslt file");
                    ofd.Filter = "Transformation file (*.xslt)|*.xslt";
                }
                    break;
                case Enumerations.WebResourceType.Silverlight: {
                    ofd.Title = string.Format(OPENFILE_TITLE_MASK, "Silverlight application");
                    ofd.Filter = "Silverlight application file (*.xap)|*.xap";
                }
                    break;
            }
        }

        private void OpenCompareFileDialogSettings(IWebResourceControl ctrl, OpenFileDialog ofd)
        {
            switch (ctrl.GetWebResourceType())
            {
                case Enumerations.WebResourceType.Script:
                    {
                        ofd.Title = string.Format(OPENFILE_TITLE_MASK, "script file");
                        ofd.Filter = "Javascript file (*.js)|*.js";
                    }
                    break;
                case Enumerations.WebResourceType.WebPage:
                    {
                        ofd.Title = string.Format(OPENFILE_TITLE_MASK, "web page");
                        ofd.Filter = "Web page (*.html,*.htm)|*.html;*.htm";
                    }
                    break;
                case Enumerations.WebResourceType.Css:
                    {
                        ofd.Title = string.Format(OPENFILE_TITLE_MASK, "css file");
                        ofd.Filter = "Stylesheet (*.css)|*.css";
                    }
                    break;
                case Enumerations.WebResourceType.Xsl:
                    {
                        ofd.Title = string.Format(OPENFILE_TITLE_MASK, "xslt file");
                        ofd.Filter = "Transformation file (*.xslt)|*.xslt";
                    }
                    break;
                case Enumerations.WebResourceType.Data:
                    {
                        ofd.Title = string.Format(OPENFILE_TITLE_MASK, "xml file");
                        ofd.Filter = "Xml file (*.xml)|*.xml";
                    }
                    break;
            }
        }

        private void FileMenuUpdateAndPublishClick(object sender, EventArgs e)
        {
            if (((TabControl) (Parent).Parent).SelectedTab != Parent)
            {
                ((ToolStripDropDownItem)((ToolStrip)(((TabControl)(Parent).Parent).SelectedTab.Controls.Find("toolStripScriptContent", true)[0])).Items[0]).DropDownItems[2].PerformClick();
                return;
            }

            if (tvWebResources.SelectedNode != null)
            {
                ExecuteMethod(FileMenuUpdateAndPublish, tvWebResources.SelectedNode);
            }
        }

        private void FileMenuUpdateAndPublish(TreeNode node)
        {
            UpdateWebResources(true, new List<TreeNode> { node });
        }

        private void TsbMinifyJsClick(object sender, EventArgs e)
        {
            if (DialogResult.Yes ==
                MessageBox.Show(this,
                                "Are you sure you want to compress this script? After saving the compressed script, you won't be able to retrieve original content",
                                "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                ((CodeControl)panelControl.Controls[0]).MinifyJs();
        }

        private void tsbBeautify_Click(object sender, EventArgs e)
        {
            ((CodeControl) panelControl.Controls[0]).Beautify();
        }

        private void TsbPreviewHtmlClick(object sender, EventArgs e)
        {
            string content = ((IWebResourceControl)panelControl.Controls[0]).GetBase64WebResourceContent();

            var wpDialog = new WebPreviewDialog(content);
            wpDialog.ShowDialog();
        }

        private void FindToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (((TabControl)(Parent).Parent).SelectedTab != Parent)
            {
                ((ToolStripDropDownItem)((ToolStrip)(((TabControl)(Parent).Parent).SelectedTab.Controls.Find("toolStripScriptContent", true)[0])).Items[2]).DropDownItems[0].PerformClick();
                return;
            }

            if (panelControl.Controls.Count == 0)
                return;

            var control = ((IWebResourceControl) panelControl.Controls[0]);
            if (!(control is CodeControl)) return;

            ((CodeControl)control).Find(false, this);
        }

        private void ReplaceToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (((TabControl)(Parent).Parent).SelectedTab != Parent)
            {
                ((ToolStripDropDownItem)((ToolStrip)(((TabControl)(Parent).Parent).SelectedTab.Controls.Find("toolStripScriptContent", true)[0])).Items[2]).DropDownItems[1].PerformClick();
                return;
            }

            if (panelControl.Controls.Count == 0)
                return;

            var control = ((IWebResourceControl)panelControl.Controls[0]);
            if (!(control is CodeControl)) return;

            ((CodeControl)control).Find(true, this);
        }

        private void TsmCompareSettingsClick(object sender, EventArgs e)
        {
            OpenCompareSettings();
        }

        private void TsbCompareClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.CompareToolPath)) {
                OpenCompareSettings(false);
                return;
            }

            try {
                var ctrl = ((IWebResourceControl)panelControl.Controls[0]);
                var content = ((IWebResourceControl)panelControl.Controls[0]).GetBase64WebResourceContent();

                RemoveOldFiles();
                var crmFileToComapre = SaveContentFileToDisk(tvWebResources.SelectedNode.Text, content);

                using (var ofd = new OpenFileDialog()) {
                    OpenCompareFileDialogSettings(ctrl, ofd);

                    if (ofd.ShowDialog() == DialogResult.OK) {
                        var diskfileToCompare = ofd.FileName;
                        CompareFiles(crmFileToComapre, diskfileToCompare);
                    }
                }
            } catch (Exception error) {
                MessageBox.Show(string.Format("Error while performing the file compare.{0}Please go to the compare settings and validate that you configured the correct compare tool.{0}{0}Error: {1}",Environment.NewLine, error.Message),"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void OpenCompareSettings(bool isConfigured = true)
        {
            using (var compareDialog = new CompareSettingsDialog(isConfigured))
            {
                compareDialog.ShowDialog();
            }
        }

        private void RemoveOldFiles()
        {
            var directory = new DirectoryInfo(string.Format(@"{0}\CompareTemp", Environment.CurrentDirectory));
            if (!Directory.Exists(directory.FullName))
                return;

            Directory.Delete(directory.FullName, true);
        }

        private string SaveContentFileToDisk(string fileName, string content)
        {
            var path = string.Format(@"{0}\CompareTemp", Environment.CurrentDirectory);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var filePath = string.Format(@"{0}\{1}", path, fileName);
            File.WriteAllBytes(filePath, Convert.FromBase64String(content));
            return filePath;
        }

        private void CompareFiles(string crmFile, string diskFile)
        {
            var startInfo = new ProcessStartInfo(Properties.Settings.Default.CompareToolPath)
            {
                Arguments = string.Format("{0} \"{1}\" \"{2}\"", Properties.Settings.Default.CompareToolArgs, crmFile, diskFile)
            };
            Process.Start(startInfo);
        }

        #endregion WEBRESOURCE CONTENT - Actions

        #region TreeView Event handlers

        private void ChkSelectAllCheckedChanged(object sender, EventArgs e)
        {
            //tvWebResources.AfterCheck -= TvWebResourcesAfterCheck;

            foreach (TreeNode node in tvWebResources.Nodes)
                node.Checked = chkSelectAll.Checked;

            //tvWebResources.AfterCheck += TvWebResourcesAfterCheck;
        }

        private void TvWebResourcesAfterCheck(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode node in e.Node.Nodes)
            {
                node.Checked = e.Node.Checked;
            }
        }

        private void TvWebResourcesAfterSelect(object sender, TreeViewEventArgs e)
        {
            panelControl.Controls.Clear();
            if (tvWebResources.SelectedNode != null 
                && tvWebResources.SelectedNode.Tag != null
                && tvWebResources.SelectedNode.Tag is WebResource)
            {
                toolStripScriptContent.Visible = true;
                tslResourceName.Visible = true;

                // Displays script content
                Entity script = ((WebResource)tvWebResources.SelectedNode.Tag).WebResourceEntity;
                UserControl ctrl = null;

                switch (((OptionSetValue) script["webresourcetype"]).Value)
                {
                    case 1:
                        ctrl = new CodeControl(script.GetAttributeValue<string>("content"),
                                                Enumerations.WebResourceType.WebPage);
                        ((CodeControl) ctrl).WebResourceUpdated +=
                            MainFormWebResourceUpdated;
                        toolStripSeparatorMinifyJS.Visible = true;
                        tsbMinifyJS.Visible = false;
                        tsbBeautify.Visible = false;
                        tsbPreviewHtml.Visible = true;
                        tsSeparatorEdit.Visible = true;
                        tsddbEdit.Visible = true;
                        tsddbCompare.Visible = true;
                        break;

                    case 2:
                        ctrl = new CodeControl(script.GetAttributeValue<string>("content"),
                                                Enumerations.WebResourceType.Css);
                        ((CodeControl) ctrl).WebResourceUpdated += MainFormWebResourceUpdated;
                        tsbMinifyJS.Visible = false;
                        tsbBeautify.Visible = false;
                        tsbPreviewHtml.Visible = false;
                        tsSeparatorEdit.Visible = true;
                        tsddbEdit.Visible = true;
                        tsddbCompare.Visible = true;
                        break;
                    case 3:
                        ctrl = new CodeControl(script.GetAttributeValue<string>("content"),
                                                Enumerations.WebResourceType.Script);
                        ((CodeControl) ctrl).WebResourceUpdated +=
                            MainFormWebResourceUpdated;
                        toolStripSeparatorMinifyJS.Visible = true;
                        tsbMinifyJS.Visible = true;
                        tsbBeautify.Visible = true;
                        tsbPreviewHtml.Visible = false;
                        tsSeparatorEdit.Visible = true;
                        tsddbEdit.Visible = true;
                        tsddbCompare.Visible = true;
                        break;
                    case 4:
                        ctrl = new CodeControl(script.GetAttributeValue<string>("content"),
                                                Enumerations.WebResourceType.Data);
                        ((CodeControl) ctrl).WebResourceUpdated +=
                            MainFormWebResourceUpdated;
                        tsbMinifyJS.Visible = false;
                        tsbBeautify.Visible = false;
                        tsbPreviewHtml.Visible = false;
                        tsSeparatorEdit.Visible = true;
                        tsddbEdit.Visible = true;
                        tsddbCompare.Visible = true;
                        break;
                    case 5:
                        ctrl = new ImageControl(script.GetAttributeValue<string>("content"),
                                                Enumerations.WebResourceType.Png);
                        ((ImageControl) ctrl).WebResourceUpdated +=
                            MainFormWebResourceUpdated;
                        tsbMinifyJS.Visible = false;
                        tsbBeautify.Visible = false;
                        tsbPreviewHtml.Visible = false;
                        tsSeparatorEdit.Visible = false;
                        tsddbEdit.Visible = false;
                        tsddbCompare.Visible = false;
                        break;
                    case 6:
                        ctrl = new ImageControl(script.GetAttributeValue<string>("content"),
                                                Enumerations.WebResourceType.Jpg);
                        ((ImageControl) ctrl).WebResourceUpdated +=
                            MainFormWebResourceUpdated;
                        tsbMinifyJS.Visible = false;
                        tsbBeautify.Visible = false;
                        tsbPreviewHtml.Visible = false;
                        tsSeparatorEdit.Visible = false;
                        tsddbEdit.Visible = false;
                        tsddbCompare.Visible = false;
                        break;
                    case 7:
                        ctrl = new ImageControl(script.GetAttributeValue<string>("content"),
                                                Enumerations.WebResourceType.Gif);
                        ((ImageControl) ctrl).WebResourceUpdated +=
                            MainFormWebResourceUpdated;
                        tsbMinifyJS.Visible = false;
                        tsbBeautify.Visible = false;
                        tsbPreviewHtml.Visible = false;
                        tsSeparatorEdit.Visible = false;
                        tsddbEdit.Visible = false;
                        tsddbCompare.Visible = false;
                        break;
                    case 8:
                        ctrl = new UserControl();
                        tsSeparatorEdit.Visible = false;
                        tsddbEdit.Visible = false;
                        tsbPreviewHtml.Visible = false;
                        tsddbCompare.Visible = false;
                        break;
                    case 9:
                        ctrl = new CodeControl(script.GetAttributeValue<string>("content"),
                                                Enumerations.WebResourceType.Xsl);
                        ((CodeControl) ctrl).WebResourceUpdated +=
                            MainFormWebResourceUpdated;
                        tsbMinifyJS.Visible = false;
                        tsbBeautify.Visible = false;
                        tsbPreviewHtml.Visible = false;
                        tsSeparatorEdit.Visible = true;
                        tsddbEdit.Visible = true;
                        tsddbCompare.Visible = true;
                        break;
                    case 10:
                        ctrl = new IconControl(script.GetAttributeValue<string>("content"));
                        ((IconControl) ctrl).WebResourceUpdated +=
                            MainFormWebResourceUpdated;
                        tsbMinifyJS.Visible = false;
                        tsbBeautify.Visible = false;
                        tsbPreviewHtml.Visible = false;
                        tsSeparatorEdit.Visible = false;
                        tsddbEdit.Visible = false;
                        tsddbCompare.Visible = false;
                        break;
                }

                if (ctrl != null)
                {
                    ctrl.Dock = DockStyle.Fill;
                    panelControl.Controls.Add(ctrl);

                    fileMenuSave.Enabled = false;
                    fileMenuReplace.Enabled = true;
                    fileMenuUpdateAndPublish.Enabled = true;

                    tslResourceName.Text = script["name"].ToString();
                }
                else
                {
                    fileMenuSave.Enabled = false;
                    fileMenuReplace.Enabled = false;
                    fileMenuUpdateAndPublish.Enabled = false;

                    toolStripSeparatorMinifyJS.Visible = false;
                    tsbMinifyJS.Visible = false;
                    tsbPreviewHtml.Visible = false;

                    tslResourceName.Text = string.Empty;
                }
            }
            else
            {
                // Clear script content
                if (tvWebResources.SelectedNode != null) tvWebResources.SelectedNode.ContextMenuStrip = null;

                fileMenuSave.Enabled = false;
                fileMenuReplace.Enabled = false;
                fileMenuUpdateAndPublish.Enabled = false;
                toolStripScriptContent.Visible = false;
                tslResourceName.Visible = false;
            }
        }

        private void TvWebResourcesMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Select the clicked node
                tvWebResources.SelectedNode = tvWebResources.GetNodeAt(e.X, e.Y);

                if (tvWebResources.SelectedNode != null)
                {
                    switch (tvWebResources.SelectedNode.ImageIndex)
                    {
                        case 0:
                            {
                                addNewFolderToolStripMenuItem.Enabled = true;
                                addNewWebResourceToolStripMenuItem.Enabled = true;
                                addNewEmptyWebResourceToolStripMenuItem.Enabled = true;
                                deleteToolStripMenuItem.Enabled = tvWebResources.SelectedNode.Nodes.Count == 0;
                                saveToCRMServerToolStripMenuItem.Enabled = false;
                                saveAndPublishToCRMServerToolStripMenuItem.Enabled = false;
                                savePublishAndAddToSolutionToolStripMenuItem.Enabled = false;
                                propertiesToolStripMenuItem.Enabled = false;
                                updateFromDiskToolStripMenuItem.Enabled = true;
                                copyWebResourceNameToClipboardToolStripMenuItem.Enabled = false;
                                getLatestVersionToolStripMenuItem.Enabled = false;
                            }
                            break;
                        case 1:
                            {
                                addNewFolderToolStripMenuItem.Enabled = true;
                                addNewWebResourceToolStripMenuItem.Enabled = true;
                                addNewEmptyWebResourceToolStripMenuItem.Enabled = true;
                                deleteToolStripMenuItem.Enabled = tvWebResources.SelectedNode.Nodes.Count == 0;
                                saveToCRMServerToolStripMenuItem.Enabled = false;
                                saveAndPublishToCRMServerToolStripMenuItem.Enabled = false;
                                savePublishAndAddToSolutionToolStripMenuItem.Enabled = false;
                                propertiesToolStripMenuItem.Enabled = false;
                                updateFromDiskToolStripMenuItem.Enabled = true;
                                copyWebResourceNameToClipboardToolStripMenuItem.Enabled = false;
                                getLatestVersionToolStripMenuItem.Enabled = false;
                            }
                            break;
                        default:
                            {
                                addNewFolderToolStripMenuItem.Enabled = false;
                                addNewWebResourceToolStripMenuItem.Enabled = false;
                                addNewEmptyWebResourceToolStripMenuItem.Enabled = false;
                                deleteToolStripMenuItem.Enabled = true;
                                saveToCRMServerToolStripMenuItem.Enabled = true;
                                saveAndPublishToCRMServerToolStripMenuItem.Enabled = true;
                                savePublishAndAddToSolutionToolStripMenuItem.Enabled = true;
                                propertiesToolStripMenuItem.Enabled = true;
                                updateFromDiskToolStripMenuItem.Enabled = false;
                                copyWebResourceNameToClipboardToolStripMenuItem.Enabled = true;
                                getLatestVersionToolStripMenuItem.Enabled = true;
                            }
                            break;
                    }

                    if (tvWebResources.SelectedNode != null)
                    {
                        contextMenuStripTreeView.Show(tvWebResources, e.Location);
                    }
                }
            }
        }

        #endregion TreeView Event handlers

        void MainFormWebResourceUpdated(object sender, WebResourceUpdatedEventArgs e)
        {
            fileMenuSave.Enabled = e.IsDirty;
            fileMenuUpdateAndPublish.Enabled = !e.IsDirty;
            if (e.IsDirty)
            {
                if (!tslResourceName.Text.Contains(" (not saved)"))
                {
                    tslResourceName.ForeColor = Color.Red;
                    tslResourceName.Text += " (not saved)";
                }
            }
            else
            {
                tslResourceName.ForeColor = Color.Black;
                tslResourceName.Text = tslResourceName.Text.Split(' ')[0];
            }
        }

        void SetWorkingState(bool working)
        {
            tsbNewRoot.Enabled = !working;
            tsddCrmMenu.Enabled = !working;
            tsddFileMenu.Enabled = !working;
            tvWebResources.Enabled = !working;
            chkSelectAll.Enabled = !working;
            tsbClear.Enabled = !working;
            toolStripScriptContent.Enabled = !working;
            findUnusedWebResourcesToolStripMenuItem.Enabled = !working;

            fileMenuSave.Enabled = false;
            var selectedNode = tvWebResources.SelectedNode;
            if (selectedNode != null)
            {
                fileMenuReplace.Enabled = selectedNode.Tag != null;
                fileMenuUpdateAndPublish.Enabled = selectedNode.Tag != null;
            }

            Cursor = working ? Cursors.WaitCursor : Cursors.Default;
        }

        #endregion Methods

        #region ThisControl handler

        private void TsbCloseThisTabClick(object sender, EventArgs e)
        {
           CloseTool();
        }

        #endregion

        private void OpenWebResourceRecordInCrmApplicationToolStripMenuItemClick(object sender, EventArgs e)
        {
            var wr = ((WebResource) tvWebResources.SelectedNode.Tag).WebResourceEntity;

            if (wr.Id != Guid.Empty)
            {
                var url = ((OrganizationServiceProxy) ((OrganizationService)Service).InnerService).ServiceConfiguration.CurrentServiceEndpoint.Address.Uri
                                                              .AbsoluteUri.Replace(
                                                                  "/XRMServices/2011/Organization.svc",
                                                                  "/main.aspx?id=" + wr.Id.ToString("B") + "&etc=9333&pagetype=webresourceedit")
                                                              .Replace(".api", "");

                Process.Start(url);
            }
            else
            {
                MessageBox.Show(this, "This web resource does not exist on the CRM organization yet", "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void findUnusedWebResourcesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var nodes = new List<TreeNode>();
            TreeViewHelper.GetNodes(nodes, tvWebResources, false);

            WorkAsync("Starting analysis...",
                (bw, evt) =>
                {
                    var localNodes = (List<TreeNode>)evt.Argument;

                    var unusedWebResources = new List<Entity>();
                    int i = 1;
                    foreach (TreeNode node in localNodes)
                    {
                        var wr = ((WebResource)node.Tag).WebResourceEntity;

                        bw.ReportProgress((i * 100) / nodes.Count, "Analyzing web resource " + wr["name"] + "...");

                        if (!wrManager.HasDependencies(wr.Id))
                        {
                            unusedWebResources.Add(wr);
                        }
                        i++;
                    }

                    evt.Result = unusedWebResources;
                },
                evt =>
                {
                    var dialog = new UnusedWebResourcesListDialog((List<Entity>)evt.Result, Service);
                    dialog.ShowInTaskbar = true;
                    dialog.StartPosition = FormStartPosition.CenterParent;
                    dialog.ShowDialog(this);
                },
                evt => SetWorkingMessage(string.Format("{0}% - {1}", evt.ProgressPercentage, evt.UserState)),
                nodes);
        }

        private void tvWebResources_DragDrop(object sender, DragEventArgs e)
        {
            var errorList = new List<string>();
            var tv = (TreeView)sender;
            Point location = tv.PointToScreen(Point.Empty);
            var currentNode = tvWebResources.GetNodeAt(e.X - location.X, e.Y - location.Y);

            var files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (var file in files)
            {
                var fi = new FileInfo(file);

                var tempNode = currentNode;
                string name = tempNode.Text;
                while (tempNode.Parent != null)
                {
                    name = string.Format("{0}/{1}", tempNode.Parent.Text, name);
                    tempNode = tempNode.Parent;
                }

                //Test valid characters
                if (WebResource.IsInvalidName(fi.Name))
                {
                    errorList.Add(file);
                }
                else
                {
                    var webResource = new Entity("webresource");
                    webResource["content"] = Convert.ToBase64String(File.ReadAllBytes(file));
                    webResource["webresourcetype"] = new OptionSetValue(WebResource.GetTypeFromExtension(fi.Extension.Remove(0, 1)));
                    webResource["name"] = string.Format("{0}/{1}", name, fi.Name);
                    webResource["displayname"] = string.Format("{0}/{1}", name, fi.Name);
                    var wr = new WebResource(webResource, file);

                    var node = new TreeNode(fi.Name)
                    {
                        ImageIndex = WebResource.GetImageIndexFromExtension(fi.Extension.Remove(0, 1))
                    };
                    node.SelectedImageIndex = node.ImageIndex;
                    node.Tag = wr;

                    currentNode.Nodes.Add(node);

                    currentNode.Expand();
                }
            }

            if (errorList.Count > 0)
            {
                MessageBox.Show("Some file have not been added since their name does not match naming policy\r\n"
                                + string.Join("\r\n", errorList));
            }
        }

        private void tvWebResources_DragOver(object sender, DragEventArgs e)
        {
            var treeView = (TreeView)sender;
            Point treeViewLocation = treeView.PointToScreen(Point.Empty);
            var currentNode = treeView.GetNodeAt(e.X - treeViewLocation.X, e.Y - treeViewLocation.Y);

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);

                bool validExtensions = files.All(f => WebResource.ValidExtensions.Contains(new FileInfo(f).Extension.Remove(0, 1).ToLower()));
                bool validNode = currentNode != null && currentNode.ImageIndex <= 1;

                if (validNode)
                {
                    treeView.SelectedNode = currentNode;
                }

                if (files.Length > 0 && validExtensions && validNode)
                    e.Effect = DragDropEffects.All;
                else
                    e.Effect = DragDropEffects.None;
            }
            else
                e.Effect = DragDropEffects.None;
        }
    }
}
