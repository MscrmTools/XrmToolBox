// PROJECT : MsCrmTools.WebResourcesManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using MsCrmTools.WebResourcesManager.AppCode;
using MsCrmTools.WebResourcesManager.DelegatesHelpers;
using MsCrmTools.WebResourcesManager.Forms;
using MsCrmTools.WebResourcesManager.Forms.Solutions;
using MsCrmTools.WebResourcesManager.UserControls;
using XrmToolBox;

namespace MsCrmTools.WebResourcesManager
{
    public partial class WebResourcesManagerControl : UserControl, IMsCrmToolsPluginUserControl
    {
        #region Variables

        /// <summary>
        /// Dynamics CRM 2011 Organization service
        /// </summary>
        private IOrganizationService service;

        /// <summary>
        /// Scripts Manager
        /// </summary>
        WebResourceManager wrManager;

        /// <summary>
        /// List of invalid filenames when creating or importing web resources
        /// </summary>
        private List<string> invalidFilenames;

        /// <summary>
        /// Information panel
        /// </summary>
        private Panel infoPanel;

        #endregion Variables

        #region Constructor

        public WebResourcesManagerControl()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region IMsCrmToolsPluginUserControl Members

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

        void IMsCrmToolsPluginUserControl.UpdateConnection(IOrganizationService newService, string actionName, object parameter = null)
        {
            service = newService;
            wrManager = new WebResourceManager(newService);
                        
            var nodesList = new List<TreeNode>();
            TreeViewHelper.GetNodes(nodesList, tvWebResources, true);

            switch (actionName)
            {
                case "LoadWebResources":
                    {
                        RetrieveWebResources();
                    }
                    break;
                case "Update":
                    {
                        UpdateWebResources(false, nodesList);
                    }
                    break;
                case "UpdateAndPublish":
                    {
                        UpdateWebResources(true, nodesList);
                    }
                    break;
                case "UpdateAndPublishAndAdd":
                    {
                        UpdateWebResources(true, nodesList, true);
                    }
                    break;
                case "Delete":
                    {
                        wrManager.DeleteWebResource(((WebResource)nodesList[0].Tag).WebResourceEntity);
                        tvWebResources.Nodes.Remove(nodesList[0]);
                    }
                    break;
            }
        }

        #endregion IMsCrmToolsPluginUserControl Members

        #region Methods
       
        #region CRM - Load web resources

        private void LoadWebResourcesToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (service == null)
            {
                tvWebResources.Nodes.Clear();

                if (OnRequestConnection != null)
                {
                    var args = new RequestConnectionEventArgs {ActionName = "LoadWebResources", Control = this};
                    OnRequestConnection(this, args);
                }
            }
            else
            {
                tvWebResources.Nodes.Clear();

                SetWorkingState(true);

                infoPanel = InformationPanel.GetInformationPanel(this, "Loading web resources...", 340, 100);

                var bwFillWebResources = new BackgroundWorker();
                bwFillWebResources.DoWork += BwFillWebResourcesDoWork;
                bwFillWebResources.RunWorkerCompleted += BwFillWebResourcesRunWorkerCompleted;
                bwFillWebResources.RunWorkerAsync();
            }
        }

        private void BwFillWebResourcesDoWork(object sender, DoWorkEventArgs e)
        {
            RetrieveWebResources();
        }

        private void BwFillWebResourcesRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                string errorMessage = CrmExceptionHelper.GetErrorMessage(e.Error, true);
                MessageBox.Show(this, errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                tvWebResources.Enabled = true;
            }

            infoPanel.Dispose();
            Controls.Remove(infoPanel);
            SetWorkingState(false);
        }

        private void RetrieveWebResources()
        {
            EntityCollection scripts = wrManager.RetrieveWebResources();

            //Todo ccsb.SetMessage(string.Empty);

            TreeViewDelegates.ClearNodes(tvWebResources);

            foreach (Entity script in scripts.Entities)
            {
                var wrObject = new WebResource(script);

                string[] nameParts = script["name"].ToString().Split('/');

                AddNode(nameParts, 0, tvWebResources, wrObject);
            }

            TreeViewDelegates.Sort(tvWebResources, new NodeSorter());
            TreeViewDelegates.ExpandAll(tvWebResources);
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
            try
            {
                // Retrieve checked web resources
                var nodesList = new List<TreeNode>();
                TreeViewHelper.GetNodes(nodesList, tvWebResources, true);

                if (nodesList.Count == 0)
                {
                    MessageBox.Show(this, "Please check at least one web resource before using this function", "Warning",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (service == null)
                {
                    if (OnRequestConnection != null)
                    {
                        string action = addToSolution ? "UpdateAndPublishAndAdd" : publish ? "UpdateAndPublish" : "Update";

                        var args = new RequestConnectionEventArgs { ActionName = action, Control = this };
                        OnRequestConnection(this, args);
                    }
                }
                else
                {
                    UpdateWebResources(publish, nodesList, addToSolution);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(this, "An error occured: " + error.ToString(), "error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void UpdateWebResources(bool publish, IEnumerable<TreeNode> nodes, bool addToSolution = false)
        {
            var webResources = new List<Entity>();
            foreach (TreeNode node in nodes)
            {
                if (node.Tag != null && ((WebResource)node.Tag).WebResourceEntity != null)
                {
                    webResources.Add(((WebResource)node.Tag).WebResourceEntity);
                }
            }

            var solutionUniqueName = string.Empty;
            if (addToSolution)
            {
                var sPicker = new SolutionPicker(service) { StartPosition = FormStartPosition.CenterParent };

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
            infoPanel = InformationPanel.GetInformationPanel(this, "Updating web resources...", 400, 100);

            var parameters = new object[] { webResources, publish, solutionUniqueName };

            var bw = new BackgroundWorker {WorkerReportsProgress = true};
            bw.DoWork += BwDoWork;
            bw.ProgressChanged += BwProgressChanged;
            bw.RunWorkerCompleted += BwRunWorkerCompleted;
            bw.RunWorkerAsync(parameters);
        }

        private void BwDoWork(object sender, DoWorkEventArgs e)
        {
            var bw = (BackgroundWorker) sender;
            var webResourceManager = new WebResourceManager(service);
            var idsToPublish = new List<Guid>();

            foreach (Entity wr in ((List<Entity>)((object[])e.Argument)[0]))
            {
                bw.ReportProgress(1, string.Format("Updating {0}...", wr["name"]));

                wr.Id = webResourceManager.UpdateWebResource(wr);

                idsToPublish.Add(wr.Id);
            }

            // if publish
            if ((bool)((object[])e.Argument)[1])
            {
                bw.ReportProgress(2,"Publishing web resources...");

                webResourceManager.PublishWebResources(idsToPublish);
            }

            if (((object[])e.Argument)[2].ToString().Length > 0)
            {
                bw.ReportProgress(3,"Adding web resources to solution...");

                webResourceManager.AddToSolution(idsToPublish, ((object[])e.Argument)[2].ToString());
            }
        }
        
        private void BwProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            InformationPanel.ChangeInformationPanelMessage(infoPanel, e.UserState.ToString());
        }
        
        private void BwRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(this, "An error occured: " + e.Error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            infoPanel.Dispose();
            Controls.Remove(infoPanel);
            SetWorkingState(false);
        }

        #endregion CRM - Update web resources

        #region DISK - Load web resources

        private void LoadWebResourcesToolStripMenuItem1Click(object sender, EventArgs e)
        {
            try
            {
                // Let the user decides where to find files
                var fbd = new FolderBrowserDialog
                {
                    Description = "Select the folder where the scripts files are located",
                    ShowNewFolderButton = true
                };

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    tvWebResources.Nodes.Clear();
                    invalidFilenames = new List<string>();

                    var di = new DirectoryInfo(fbd.SelectedPath);

                    foreach (DirectoryInfo diChild in di.GetDirectories("*_", SearchOption.TopDirectoryOnly))
                    {
                        if (WebResource.IsInvalidName(diChild.Name))
                        {
                            invalidFilenames.Add(diChild.FullName);
                            continue;
                        }

                        var rootFolderNode = new TreeNode(diChild.Name) { ImageIndex = 0 };

                        tvWebResources.Nodes.Add(rootFolderNode);

                        TreeViewHelper.CreateFolderStructure(rootFolderNode, diChild, invalidFilenames);
                    }

                    foreach (FileInfo fiChild in di.GetFiles("*.*", SearchOption.TopDirectoryOnly))
                    {
                        if (WebResource.IsInvalidName(fiChild.Name) || !WebResource.ValidExtensions.Contains(fiChild.Extension.Remove(0, 1)))
                        {
                            invalidFilenames.Add(fiChild.FullName);
                            continue;
                        }

                        TreeViewHelper.CreateWebResourceNode(fiChild, tvWebResources);
                    }

                    tvWebResources.ExpandAll();

                    tvWebResources.TreeViewNodeSorter = new NodeSorter();
                    tvWebResources.Sort();

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

        #endregion DISK - Load web resources

        #region DISK - Save web resources

        private void SaveCheckedWebResourcesToDiskToolStripMenuItemClick(object sender, EventArgs e)
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

        private void SaveAllWebResourcesToDiskToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                var nodes = new List<TreeNode>();
                TreeViewHelper.GetNodes(nodes, tvWebResources, false);

                SaveWebResourcesToDisk(nodes);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, "Error while saving web resources: " + error.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveWebResourcesToDisk(IEnumerable<TreeNode> nodes)
        {
            var fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                foreach (var node in nodes)
                {
                    if (node.Tag != null && ((WebResource)node.Tag).WebResourceEntity != null)
                    {
                        var webResource = ((WebResource)node.Tag).WebResourceEntity;

                        if (webResource.Contains("content") && webResource["content"].ToString().Length > 0)
                        {
                            string[] partPath = webResource["name"].ToString().Split('/');
                            string path = fbd.SelectedPath;

                            for (int i = 0; i < partPath.Length - 1; i++)
                            {
                                path = Path.Combine(path, partPath[i]);

                                if (!Directory.Exists(path))
                                    Directory.CreateDirectory(path);
                            }

                            path = Path.Combine(path, partPath[partPath.Length - 1]);
                            byte[] bytes = Convert.FromBase64String(webResource["content"].ToString());
                            File.WriteAllBytes(path, bytes);
                        }
                    }
                }
            }
        }

        #endregion DISK - Save web resources

        #region CRM/DISK - Delete Web resources

        private void DeleteToolStripMenuItemClick(object sender, EventArgs e)
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
                            var nodesList = new List<TreeNode> {selectedNode};

                            if (service == null)
                            {
                                if (OnRequestConnection != null)
                                {
                                    var args = new RequestConnectionEventArgs {ActionName = "Delete", Control = this};
                                    OnRequestConnection(this, args);
                                }
                            }
                            else
                            {
                                DeleteWebResource(nodesList);
                                tvWebResources.Nodes.Remove(selectedNode);
                            }
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

        private void DeleteWebResource(List<TreeNode> nodes)
        {
            SetWorkingState(true);
            
            infoPanel = InformationPanel.GetInformationPanel(this, "Deleting web resource...", 340, 100);

            var bwDelete = new BackgroundWorker();
            bwDelete.DoWork += BwDeleteDoWork;
            bwDelete.RunWorkerCompleted += BwDeleteRunWorkerCompleted;
            bwDelete.RunWorkerAsync(((WebResource)nodes[0].Tag).WebResourceEntity);
        }

        private void BwDeleteRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(this, "An error occured: " + e.Error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            infoPanel.Dispose();
            Controls.Remove(infoPanel);
            SetWorkingState(false);
        }

        private void BwDeleteDoWork(object sender, DoWorkEventArgs e)
        {
            wrManager.DeleteWebResource((Entity)e.Argument);
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

            tvWebResources.SelectedNode.Tag = ((WebResource) tvWebResources.SelectedNode.Tag).ShowProperties(service,
                                                                                                             this);
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

        #endregion TREEVIEW - Manage content

        #region WEBRESOURCE CONTENT - Actions

        private void TsbUploadClick(object sender, EventArgs e)
        {
            ((IWebResourceControl)panelControl.Controls[0]).ReplaceWithNewFile();
        }

        private void TsbSaveContentClick(object sender, EventArgs e)
        {
            string content = ((IWebResourceControl)panelControl.Controls[0]).GetBase64WebResourceContent();

            ((WebResource)tvWebResources.SelectedNode.Tag).WebResourceEntity["content"] = content;

            tsbSaveContent.Enabled = false;
            TvWebResourcesAfterSelect(null, null);
        }

        private void TsbPublishClick(object sender, EventArgs e)
        {
            if (tvWebResources.SelectedNode != null)
            {
                var nodesList = new List<TreeNode> { tvWebResources.SelectedNode };

                if (service == null)
                {
                    if (OnRequestConnection != null)
                    {
                        var args = new RequestConnectionEventArgs { ActionName = "UpdateAndPublish", Control = this };
                        OnRequestConnection(this, args);
                    }
                }
                else
                {
                    UpdateWebResources(true, nodesList);
                }
            }
        }

        private void TsbMinifyJsClick(object sender, EventArgs e)
        {
            if (DialogResult.Yes ==
                MessageBox.Show(this,
                                "Are you sure you want to compress this script? After saving the compressed script, you won't be able to retrieve original content",
                                "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                ((CodeControl)panelControl.Controls[0]).MinifyJs();
        }

        private void TsbPreviewHtmlClick(object sender, EventArgs e)
        {
            string content = ((IWebResourceControl)panelControl.Controls[0]).GetBase64WebResourceContent();

            var wpDialog = new WebPreviewDialog(content);
            wpDialog.ShowDialog();
        }

        #endregion WEBRESOURCE CONTENT - Actions

        #region TreeView Event handlers

        private void ChkSelectAllCheckedChanged(object sender, EventArgs e)
        {
            tvWebResources.AfterCheck -= TvWebResourcesAfterCheck;

            foreach (TreeNode node in tvWebResources.Nodes)
                node.Checked = chkSelectAll.Checked;

            tvWebResources.AfterCheck += TvWebResourcesAfterCheck;
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
            if (tvWebResources.SelectedNode != null && tvWebResources.SelectedNode.Tag != null)
            {
                toolStripScriptContent.Visible = true;
                lblResourceName.Visible = true;

                // Displays script content
                Entity script = ((WebResource)tvWebResources.SelectedNode.Tag).WebResourceEntity;
                UserControl ctrl = null;

                if (script.Contains("content") && script["content"] != null)
                {
                    switch (((OptionSetValue)script["webresourcetype"]).Value)
                    {
                        case 1:
                            ctrl = new CodeControl(script["content"].ToString(),
                                                   Enumerations.WebResourceType.WebPage);
                            ((CodeControl)ctrl).WebResourceUpdated +=
                                MainFormWebResourceUpdated;
                            toolStripSeparatorMinifyJS.Visible = true;
                            tsbMinifyJS.Visible = false;
                            tsbPreviewHtml.Visible = true;
                            break;

                        case 2:
                            ctrl = new CodeControl(script["content"].ToString(),
                                                   Enumerations.WebResourceType.Css);
                            ((CodeControl)ctrl).WebResourceUpdated += MainFormWebResourceUpdated;
                            tsbMinifyJS.Visible = false;
                            tsbPreviewHtml.Visible = false;
                            break;
                        case 3:
                            ctrl = new CodeControl(script["content"].ToString(),
                                                   Enumerations.WebResourceType.Script);
                            ((CodeControl)ctrl).WebResourceUpdated +=
                                MainFormWebResourceUpdated;
                            toolStripSeparatorMinifyJS.Visible = true;
                            tsbMinifyJS.Visible = true;
                            tsbPreviewHtml.Visible = false;
                            break;
                        case 4:
                            ctrl = new CodeControl(script["content"].ToString(),
                                                   Enumerations.WebResourceType.Data);
                            ((CodeControl)ctrl).WebResourceUpdated +=
                                MainFormWebResourceUpdated;
                            tsbMinifyJS.Visible = false;
                            tsbPreviewHtml.Visible = false;
                            break;
                        case 5:
                            ctrl = new ImageControl(script["content"].ToString(),
                                                    Enumerations.WebResourceType.Png);
                            ((ImageControl)ctrl).WebResourceUpdated +=
                                MainFormWebResourceUpdated;
                            tsbMinifyJS.Visible = false;
                            tsbPreviewHtml.Visible = false;
                            break;
                        case 6:
                            ctrl = new ImageControl(script["content"].ToString(),
                                                    Enumerations.WebResourceType.Jpg);
                            ((ImageControl)ctrl).WebResourceUpdated +=
                                MainFormWebResourceUpdated;
                            tsbMinifyJS.Visible = false;
                            tsbPreviewHtml.Visible = false;
                            break;
                        case 7:
                            ctrl = new ImageControl(script["content"].ToString(),
                                                    Enumerations.WebResourceType.Gif);
                            ((ImageControl)ctrl).WebResourceUpdated +=
                                MainFormWebResourceUpdated;
                            tsbMinifyJS.Visible = false;
                            tsbPreviewHtml.Visible = false;
                            break;
                        case 8:
                            ctrl = new UserControl();
                            break;
                        case 9:
                            ctrl = new CodeControl(script["content"].ToString(),
                                                   Enumerations.WebResourceType.Xsl);
                            ((CodeControl)ctrl).WebResourceUpdated +=
                                MainFormWebResourceUpdated;
                            tsbMinifyJS.Visible = false;
                            tsbPreviewHtml.Visible = false;
                            break;
                        case 10:
                            ctrl = new IconControl(script["content"].ToString());
                            ((IconControl)ctrl).WebResourceUpdated +=
                                MainFormWebResourceUpdated;
                            tsbMinifyJS.Visible = false;
                            tsbPreviewHtml.Visible = false;
                            break;
                    }
                }

                if (ctrl != null)
                {
                    ctrl.Size = panelControl.Size;
                    ctrl.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    panelControl.Controls.Add(ctrl);

                    tsbSaveContent.Enabled = false;
                    tsbUpload.Enabled = true;
                    tsbPublish.Enabled = true;

                    lblResourceName.Text = script["name"].ToString();
                }
                else
                {
                    tsbSaveContent.Enabled = false;
                    tsbUpload.Enabled = false;
                    tsbPublish.Enabled = false;

                    toolStripSeparatorMinifyJS.Visible = false;
                    tsbMinifyJS.Visible = false;
                    tsbPreviewHtml.Visible = false;

                    lblResourceName.Text = string.Empty;
                }
            }
            else
            {
                // Clear script content
                if (tvWebResources.SelectedNode != null) tvWebResources.SelectedNode.ContextMenuStrip = null;

                tsbSaveContent.Enabled = false;
                tsbUpload.Enabled = false;
                tsbPublish.Enabled = false;
                toolStripScriptContent.Visible = false;
                lblResourceName.Visible = false;
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
                                propertiesToolStripMenuItem.Enabled = true;
                                updateFromDiskToolStripMenuItem.Enabled = false;
                                copyWebResourceNameToClipboardToolStripMenuItem.Enabled = true;
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
            tsbSaveContent.Enabled = e.IsDirty;
        }

        void SetWorkingState(bool working)
        {
            tsbNewRoot.Enabled = !working;
            tsddCrmMenu.Enabled = !working;
            tsddFileMenu.Enabled = !working;
            tvWebResources.Enabled = !working;
            chkSelectAll.Enabled = !working;
            toolStripScriptContent.Enabled = !working;

            tsbSaveContent.Enabled = false;
            var selectedNode = tvWebResources.SelectedNode;
            if (selectedNode != null)
            {
                tsbUpload.Enabled = selectedNode.Tag != null;
                tsbPublish.Enabled = selectedNode.Tag != null;
            }

            Cursor = working ? Cursors.WaitCursor : Cursors.Default;
        }

        #endregion Methods

        #region ThisControl handler

        private void TsbCloseThisTabClick(object sender, EventArgs e)
        {
            const string message = "Are your sure you want to close this tab?";
            if (MessageBox.Show(message, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                OnCloseTool(this, null);
        }

        #endregion

    }
}
