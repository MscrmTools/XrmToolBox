// PROJECT : MsCrmTools.WebResourcesManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using MsCrmTools.WebResourcesManager.AppCode;
using MsCrmTools.WebResourcesManager.DelegatesHelpers;
using MsCrmTools.WebResourcesManager.Forms;
using MsCrmTools.WebResourcesManager.Forms.Solutions;
using MsCrmTools.WebResourcesManager.UserControls;
using XrmToolBox;
using XrmToolBox.Attributes;
using CrmExceptionHelper = XrmToolBox.CrmExceptionHelper;

[assembly: BackgroundColor("")]
[assembly: PrimaryFontColor("")]
[assembly: SecondaryFontColor("Gray")]
[assembly: SmallImageBase64("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")]
[assembly: BigImageBase64("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")]

namespace MsCrmTools.WebResourcesManager
{
    public partial class WebResourcesManager : UserControl, IMsCrmToolsPluginUserControl
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

        const string OPENFILE_TITLE_MASK = "Select the {0} to replace the existing web resource";

        private string currentFolderForFiles;

        #endregion Variables

        #region Constructor

        public WebResourcesManager()
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

        void IMsCrmToolsPluginUserControl.UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter = null)
        {
            service = newService;
            wrManager = new WebResourceManager(newService);
                        
            var nodesList = new List<TreeNode>();
            TreeViewHelper.GetNodes(nodesList, tvWebResources, true);

            switch (actionName)
            {
                case "LoadWebResources":
                    {
                        LoadWebResourcesGeneral(Guid.Empty);
                    }
                    break;
                case "LoadWebResourcesFromSolution":
                    {
                        var sPicker = new SolutionPicker(service) {StartPosition = FormStartPosition.CenterParent};
                        if (sPicker.ShowDialog(this) == DialogResult.OK)
                        {
                            LoadWebResourcesGeneral(sPicker.SelectedSolution.Id);
                        }
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
                LoadWebResourcesGeneral(Guid.Empty);
            }
        }

        private void LoadWebResourcesFromASpecificSolutionToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (service == null)
            {
                tvWebResources.Nodes.Clear();

                if (OnRequestConnection != null)
                {
                    var args = new RequestConnectionEventArgs { ActionName = "LoadWebResourcesFromSolution", Control = this };
                    OnRequestConnection(this, args);
                }
            }
            else
            {
                var sPicker = new SolutionPicker(service) {StartPosition = FormStartPosition.CenterParent};
                if (sPicker.ShowDialog(this) == DialogResult.OK)
                {
                    LoadWebResourcesGeneral(sPicker.SelectedSolution.Id);
                }
            }
        }

        private void LoadWebResourcesGeneral(Guid specificSolutionId)
        {
            tvWebResources.Nodes.Clear();

            var dialog = new WebResourceTypeSelectorDialog();
            if (dialog.ShowDialog(ParentForm) == DialogResult.OK)
            {
                var settings = new LoadCrmResourcesSettings
                {
                    SolutionId = specificSolutionId,
                    Types = dialog.TypesToLoad
                };

                SetWorkingState(true);

                infoPanel = InformationPanel.GetInformationPanel(this, "Loading web resources...", 340, 120);

                var bwFillWebResources = new BackgroundWorker();
                bwFillWebResources.DoWork += BwFillWebResourcesDoWork;
                bwFillWebResources.RunWorkerCompleted += BwFillWebResourcesRunWorkerCompleted;
                bwFillWebResources.RunWorkerAsync(settings);
            }
        }

        private void BwFillWebResourcesDoWork(object sender, DoWorkEventArgs e)
        {
            Guid solutionId = e.Argument != null ? ((LoadCrmResourcesSettings)e.Argument).SolutionId : Guid.Empty;

            RetrieveWebResources(solutionId, ((LoadCrmResourcesSettings)e.Argument).Types);
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

            tvWebResources.ExpandAll();
            tvWebResources.TreeViewNodeSorter = new NodeSorter();
            tvWebResources.Sort();

            infoPanel.Dispose();
            Controls.Remove(infoPanel);
            SetWorkingState(false);
        }

        private void RetrieveWebResources(Guid solutionId, List<int> types)
        {
            EntityCollection scripts = wrManager.RetrieveWebResources(solutionId, types);

            //Todo ccsb.SetMessage(string.Empty);

            TreeViewDelegates.ClearNodes(tvWebResources);

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
            infoPanel = InformationPanel.GetInformationPanel(this, "Updating web resources...", 400, 120);

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
                // Let the user decides where to find files
                var fbd = new CustomFolderBrowserDialog(true);

                if (!string.IsNullOrEmpty(currentFolderForFiles))
                {
                    fbd.FolderPath = currentFolderForFiles;
                }
                
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

                        var rootFolderNode = new TreeNode(diChild.Name) { ImageIndex = 0 };

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
            
            infoPanel = InformationPanel.GetInformationPanel(this, "Deleting web resource...", 340, 120);

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

        private void FileMenuSaveClick(object sender, EventArgs e)
        {
            string content = ((IWebResourceControl)panelControl.Controls[0]).GetBase64WebResourceContent();

            ((WebResource)tvWebResources.SelectedNode.Tag).WebResourceEntity["content"] = content;

            fileMenuSave.Enabled = false;
            //TvWebResourcesAfterSelect(null, null);

            if (lblResourceName.Text.Contains(" "))
            {
                lblResourceName.ForeColor = Color.Black;
                lblResourceName.Text = lblResourceName.Text.Split(' ')[0];
            }
        }

        private void FileMenuReplaceClick(object sender, EventArgs e)
        {
            try
            {
                var ctrl = ((IWebResourceControl)panelControl.Controls[0]);

                using (var ofd = new OpenFileDialog())
                {
                    #region OpenFileDialog properties

                    switch (ctrl.GetWebResourceType())
                    {
                        case Enumerations.WebResourceType.Gif:
                            {
                                ofd.Title = string.Format(OPENFILE_TITLE_MASK, "image");
                                ofd.Filter = "Gif file (*.gif)|*.gif";
                            }
                            break;
                        case Enumerations.WebResourceType.Jpg:
                            {
                                ofd.Title = string.Format(OPENFILE_TITLE_MASK, "image");
                                ofd.Filter = "JPG file (*.jpg)|*.jpg";
                            }
                            break;
                        case Enumerations.WebResourceType.Png:
                            {
                                ofd.Title = string.Format(OPENFILE_TITLE_MASK, "image");
                                ofd.Filter = "PNG file (*.png)|*.png";
                            }
                            break;
                        case Enumerations.WebResourceType.Ico:
                            {
                                ofd.Title = string.Format(OPENFILE_TITLE_MASK, "icon");
                                ofd.Filter = "ICO file (*.ico)|*.ico";
                            }
                            break;
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
                        case Enumerations.WebResourceType.Silverlight:
                            {
                                ofd.Title = string.Format(OPENFILE_TITLE_MASK, "Silverlight application");
                                ofd.Filter = "Silverlight application file (*.xap)|*.xap";
                            }
                            break;
                    }

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

        private void FileMenuUpdateAndPublishClick(object sender, EventArgs e)
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
            var control = ((IWebResourceControl) panelControl.Controls[0]);
            if (!(control is CodeControl)) return;

            ((CodeControl)control).Find(false, this);
        }

        private void ReplaceToolStripMenuItemClick(object sender, EventArgs e)
        {
            var control = ((IWebResourceControl)panelControl.Controls[0]);
            if (!(control is CodeControl)) return;

            ((CodeControl)control).Find(true, this);
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
            if (tvWebResources.SelectedNode != null && tvWebResources.SelectedNode.Tag != null)
            {
                toolStripScriptContent.Visible = true;
                lblResourceName.Visible = true;

                // Displays script content
                Entity script = ((WebResource)tvWebResources.SelectedNode.Tag).WebResourceEntity;
                UserControl ctrl = null;

                if (script.Contains("content") && script["content"] != null)
                {
                    switch (((OptionSetValue) script["webresourcetype"]).Value)
                    {
                        case 1:
                            ctrl = new CodeControl(script["content"].ToString(),
                                                   Enumerations.WebResourceType.WebPage);
                            ((CodeControl) ctrl).WebResourceUpdated +=
                                MainFormWebResourceUpdated;
                            toolStripSeparatorMinifyJS.Visible = true;
                            tsbMinifyJS.Visible = false;
                            tsbBeautify.Visible = false;
                            tsbPreviewHtml.Visible = true;
                            tsSeparatorEdit.Visible = true;
                            tsddbEdit.Visible = true;
                            break;

                        case 2:
                            ctrl = new CodeControl(script["content"].ToString(),
                                                   Enumerations.WebResourceType.Css);
                            ((CodeControl) ctrl).WebResourceUpdated += MainFormWebResourceUpdated;
                            tsbMinifyJS.Visible = false;
                            tsbBeautify.Visible = false;
                            tsbPreviewHtml.Visible = false;
                            tsSeparatorEdit.Visible = true;
                            tsddbEdit.Visible = true;
                            break;
                        case 3:
                            ctrl = new CodeControl(script["content"].ToString(),
                                                   Enumerations.WebResourceType.Script);
                            ((CodeControl) ctrl).WebResourceUpdated +=
                                MainFormWebResourceUpdated;
                            toolStripSeparatorMinifyJS.Visible = true;
                            tsbMinifyJS.Visible = true;
                            tsbBeautify.Visible = true;
                            tsbPreviewHtml.Visible = false;
                            tsSeparatorEdit.Visible = true;
                            tsddbEdit.Visible = true;
                            break;
                        case 4:
                            ctrl = new CodeControl(script["content"].ToString(),
                                                   Enumerations.WebResourceType.Data);
                            ((CodeControl) ctrl).WebResourceUpdated +=
                                MainFormWebResourceUpdated;
                            tsbMinifyJS.Visible = false;
                            tsbBeautify.Visible = false;
                            tsbPreviewHtml.Visible = false;
                            tsSeparatorEdit.Visible = true;
                            tsddbEdit.Visible = true;
                            break;
                        case 5:
                            ctrl = new ImageControl(script["content"].ToString(),
                                                    Enumerations.WebResourceType.Png);
                            ((ImageControl) ctrl).WebResourceUpdated +=
                                MainFormWebResourceUpdated;
                            tsbMinifyJS.Visible = false;
                            tsbBeautify.Visible = false;
                            tsbPreviewHtml.Visible = false;
                            tsSeparatorEdit.Visible = false;
                            tsddbEdit.Visible = false;
                            break;
                        case 6:
                            ctrl = new ImageControl(script["content"].ToString(),
                                                    Enumerations.WebResourceType.Jpg);
                            ((ImageControl) ctrl).WebResourceUpdated +=
                                MainFormWebResourceUpdated;
                            tsbMinifyJS.Visible = false;
                            tsbBeautify.Visible = false;
                            tsbPreviewHtml.Visible = false;
                            tsSeparatorEdit.Visible = false;
                            tsddbEdit.Visible = false;
                            break;
                        case 7:
                            ctrl = new ImageControl(script["content"].ToString(),
                                                    Enumerations.WebResourceType.Gif);
                            ((ImageControl) ctrl).WebResourceUpdated +=
                                MainFormWebResourceUpdated;
                            tsbMinifyJS.Visible = false;
                            tsbBeautify.Visible = false;
                            tsbPreviewHtml.Visible = false;
                            tsSeparatorEdit.Visible = false;
                            tsddbEdit.Visible = false;
                            break;
                        case 8:
                            ctrl = new UserControl();
                            tsSeparatorEdit.Visible = false;
                            tsddbEdit.Visible = false;
                            break;
                        case 9:
                            ctrl = new CodeControl(script["content"].ToString(),
                                                   Enumerations.WebResourceType.Xsl);
                            ((CodeControl) ctrl).WebResourceUpdated +=
                                MainFormWebResourceUpdated;
                            tsbMinifyJS.Visible = false;
                            tsbBeautify.Visible = false;
                            tsbPreviewHtml.Visible = false;
                            tsSeparatorEdit.Visible = true;
                            tsddbEdit.Visible = true;
                            break;
                        case 10:
                            ctrl = new IconControl(script["content"].ToString());
                            ((IconControl) ctrl).WebResourceUpdated +=
                                MainFormWebResourceUpdated;
                            tsbMinifyJS.Visible = false;
                            tsbBeautify.Visible = false;
                            tsbPreviewHtml.Visible = false;
                            tsSeparatorEdit.Visible = false;
                            tsddbEdit.Visible = false;
                            break;
                    }
                }

                if (ctrl != null)
                {
                    ctrl.Size = panelControl.Size;
                    ctrl.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    panelControl.Controls.Add(ctrl);

                    fileMenuSave.Enabled = false;
                    fileMenuReplace.Enabled = true;
                    fileMenuUpdateAndPublish.Enabled = true;

                    lblResourceName.Text = script["name"].ToString();
                }
                else
                {
                    fileMenuSave.Enabled = false;
                    fileMenuReplace.Enabled = false;
                    fileMenuUpdateAndPublish.Enabled = false;

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

                fileMenuSave.Enabled = false;
                fileMenuReplace.Enabled = false;
                fileMenuUpdateAndPublish.Enabled = false;
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
                                savePublishAndAddToSolutionToolStripMenuItem.Enabled = true;
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
            fileMenuSave.Enabled = e.IsDirty;

            if (e.IsDirty)
            {
                if (!lblResourceName.Text.Contains(" (not saved)"))
                {
                    lblResourceName.ForeColor = Color.Red;
                    lblResourceName.Text += " (not saved)";
                }
            }
            else
            {
                lblResourceName.ForeColor = Color.Black;
                lblResourceName.Text = lblResourceName.Text.Split(' ')[0];
            }
        }

        void SetWorkingState(bool working)
        {
            tsbNewRoot.Enabled = !working;
            tsddCrmMenu.Enabled = !working;
            tsddFileMenu.Enabled = !working;
            tvWebResources.Enabled = !working;
            chkSelectAll.Enabled = !working;
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
            const string message = "Are your sure you want to close this tab?";
            if (MessageBox.Show(message, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                OnCloseTool(this, null);
        }

        #endregion

        private void OpenWebResourceRecordInCrmApplicationToolStripMenuItemClick(object sender, EventArgs e)
        {
            var wr = ((WebResource) tvWebResources.SelectedNode.Tag).WebResourceEntity;

            if (wr.Id != Guid.Empty)
            {
                var url = ((OrganizationServiceProxy) ((OrganizationService)service).InnerService).ServiceConfiguration.CurrentServiceEndpoint.Address.Uri
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

            infoPanel = InformationPanel.GetInformationPanel(this, "Starting analysis...", 500, 120);

            var bwFindUnunsedResources = new BackgroundWorker();
            bwFindUnunsedResources.DoWork += BwFindUnunsedResourcesDoWork;
            bwFindUnunsedResources.ProgressChanged += BwFindUnunsedResourcesProgressChanged;
            bwFindUnunsedResources.RunWorkerCompleted += BwFindUnunsedResourcesRunWorkerCompleted;
            bwFindUnunsedResources.WorkerReportsProgress = true;
            bwFindUnunsedResources.RunWorkerAsync(nodes);
        }

        void BwFindUnunsedResourcesDoWork(object sender, DoWorkEventArgs e)
        {
            var bw = (BackgroundWorker) sender;
            var nodes = (List<TreeNode>) e.Argument;

            var unusedWebResources = new List<Entity>();
            int i = 1;
            foreach (TreeNode node in nodes)
            {
                var wr = ((WebResource)node.Tag).WebResourceEntity;

                bw.ReportProgress((i*100)/nodes.Count, "Analyzing web resource " + wr["name"] + "...");

                if (!wrManager.HasDependencies(wr.Id))
                {
                    unusedWebResources.Add(wr);
                }
                i++;
            }

            e.Result = unusedWebResources;
        }

        void BwFindUnunsedResourcesProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            InformationPanel.ChangeInformationPanelMessage(infoPanel, 
                string.Format("{0}% - {1}", e.ProgressPercentage, e.UserState));
        }

        void BwFindUnunsedResourcesRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            infoPanel.Dispose();
            Controls.Remove(infoPanel);

            var dialog = new UnusedWebResourcesListDialog((List<Entity>) e.Result, service);
            dialog.ShowInTaskbar = true;
            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.ShowDialog(this);
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
