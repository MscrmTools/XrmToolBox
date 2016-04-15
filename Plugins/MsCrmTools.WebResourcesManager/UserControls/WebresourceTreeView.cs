using Microsoft.Xrm.Sdk;
using MsCrmTools.WebResourcesManager.AppCode;
using MsCrmTools.WebResourcesManager.Forms;
using MsCrmTools.WebResourcesManager.New.EventHandlers;
using MsCrmTools.WebResourcesManager.New.Extensions;
using MsCrmTools.WebResourcesManager.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MsCrmTools.WebResourcesManager.New.UserControls
{
    public partial class WebresourceTreeView : UserControl
    {
        /// <summary>
        /// Initialize a new instance of class <see cref="WebresourceTreeView"/>
        /// </summary>
        public WebresourceTreeView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event when this control request filtering the context menu
        /// </summary>
        public event EventHandler<WebResourceContextMenuRequestedEventArgs> WebResourceContextMenuRequested;

        /// <summary>
        /// Event when a web resource is selected in the treeview
        /// </summary>
        public event EventHandler<WebResourceSelectedEventArgs> WebResourceSelected;

        public TreeNode SelectedNode => tv.SelectedNode;
        public WebResource SelectedResource => tv.SelectedNode.Tag == null || tv.SelectedNode.Tag is string ? null : tv.SelectedNode.GetCastedTag<WebResource>();

        /// <summary>
        /// Get or set the Organization service
        /// </summary>
        public IOrganizationService Service { get; set; }

        /// <summary>
        /// Get the list of all web resources
        /// </summary>
        public List<WebResource> WebResources { get; private set; }

        /// <summary>
        /// Add existing files from disk as new web resources
        /// </summary>
        public void AddExistingWebResource()
        {
            TreeNode selectedNode = tv.SelectedNode;
            TreeNode tempNode = selectedNode;

            string name = tempNode.Text;
            while (tempNode.Parent != null)
            {
                name = string.Format("{0}/{1}", tempNode.Parent.Text, name);
                tempNode = tempNode.Parent;
            }

            var ofd = new OpenFileDialog { Multiselect = true, Title = "Select file(s) for web resource(s)" };

            if (ofd.ShowDialog(ParentForm) == DialogResult.OK)
            {
                var errorList = new List<string>();

                foreach (string fileName in ofd.FileNames)
                {
                    var fi = new FileInfo(fileName);

                    //Test valid characters
                    if (WebResource.IsNameValid(fi.Name))
                    {
                        var wr = WebResource.LoadWebResourceFromDisk(fileName, string.Format("{0}/{1}", name, fi.Name), string.Format("{0}/{1}", name, fi.Name));
                        var node = new TreeNode(fi.Name)
                        {
                            ImageIndex =
                                WebResource.GetImageIndexFromExtension(fi.Extension.Remove(0,
                                                                               1))
                        };
                        node.SelectedImageIndex = node.ImageIndex;
                        node.Tag = wr;

                        selectedNode.Nodes.Add(node);

                        selectedNode.Expand();
                    }
                    else
                    {
                        errorList.Add(fileName);
                    }

                    if (errorList.Count > 0)
                    {
                        MessageBox.Show("Some file have not been added since their name does not match naming policy\r\n"
                                        + string.Join("\r\n", errorList));
                    }
                }
            }

            tv.TreeViewNodeSorter = new NodeSorter();
            tv.Sort();
        }

        /// <summary>
        /// Clear treeview content
        /// </summary>
        public void ClearNodes()
        {
            tv.Nodes.Clear();
        }

        /// <summary>
        /// Create a new empty web resource
        /// </summary>
        /// <param name="extension">Type of web resource file to create</param>
        public void CreateEmptyWebResource(string extension)
        {
            var callerNode = tv.SelectedNode;

            var nwrDialog = new NewWebResourceDialog(extension) { StartPosition = FormStartPosition.CenterParent };

            if (nwrDialog.ShowDialog(ParentForm) == DialogResult.OK)
            {
                var tempNode = callerNode;
                string name = callerNode.Text;
                while (tempNode.Parent != null)
                {
                    name = string.Format("{0}/{1}", tempNode.Parent.Text, name);
                    tempNode = tempNode.Parent;
                }

                var webResource = new Entity("webresource");
                webResource["content"] = "";
                webResource["webresourcetype"] = new OptionSetValue(WebResource.GetTypeFromExtension(extension));
                webResource["name"] = string.Format("{0}/{1}", name, string.Format("{0}.{1}", nwrDialog.WebResourceName, extension));
                var wr = new WebResource(webResource, null);

                var parts = nwrDialog.WebResourceName.Split('/');

                for (int i = 0; i < parts.Length; i++)
                {
                    if (i != parts.Length - 1)
                    {
                        var folderNode = new TreeNode(parts[i]) { ImageIndex = 1, SelectedImageIndex = 1 };

                        callerNode.Nodes.Add(folderNode);
                        callerNode.Expand();
                        callerNode = folderNode;
                    }
                    else
                    {
                        var node = new TreeNode(string.Format("{0}.{1}", parts[i], extension))
                        {
                            ImageIndex =
                                WebResource.GetImageIndexFromExtension
                                (extension)
                        };
                        node.SelectedImageIndex = node.ImageIndex;
                        node.Tag = wr;

                        callerNode.Nodes.Add(node);
                        callerNode.Expand();
                    }
                }
            }
        }

        /// <summary>
        /// Create a new folder in the treeview
        /// </summary>
        public void CreateFolder()
        {
            var selectedNode = tv.SelectedNode;
            var nfd = new NewFolderDialog { StartPosition = FormStartPosition.CenterParent };

            if (nfd.ShowDialog(ParentForm) == DialogResult.OK)
            {
                var parts = nfd.FolderName.Split('/');
                var currentNode = selectedNode;

                foreach (var part in parts.Where(x => x.Length > 0))
                {
                    var node = new TreeNode(part.Trim()) { ImageIndex = 1, SelectedImageIndex = 1 };

                    currentNode.Nodes.Add(node);
                    tv.SelectedNode = node;
                    currentNode = node;
                }
            }

            tv.TreeViewNodeSorter = new NodeSorter();
            tv.Sort();
        }

        /// <summary>
        /// Create a new root in the treeview
        /// </summary>
        public void CreateRoot()
        {
            var nrd = new NewRootDialog { StartPosition = FormStartPosition.CenterParent };

            if (nrd.ShowDialog(ParentForm) == DialogResult.OK)
            {
                var rootNode = new TreeNode(nrd.RootName) { ImageIndex = 0, SelectedImageIndex = 0 };

                tv.Nodes.Add(rootNode);

                tv.TreeViewNodeSorter = new NodeSorter();
                tv.Sort();
            }
        }

        /// <summary>
        /// Display stored web resources in the treeview
        /// </summary>
        public void DisplayWebResources()
        {
            tv.Nodes.Clear();

            foreach (var webResource in WebResources)
            {
                string[] nameParts = webResource.Entity.GetAttributeValue<string>("name").Split('/');

                AddNode(nameParts, 0, tv, webResource);
            }

            tv.ExpandAll();
            tv.TreeViewNodeSorter = new NodeSorter();
            tv.Sort();
        }

        /// <summary>
        /// Get a list of all web resources in the treeview
        /// </summary>
        /// <returns>List of all web resources</returns>
        public List<WebResource> GetAllResources()
        {
            var nodes = new List<TreeNode>();

            GetNodes(nodes, tv, false);

            return nodes.Select(n => (WebResource)n.Tag).ToList();
        }

        /// <summary>
        /// Get a list of web resources selected in the treeview
        /// </summary>
        /// <returns>List of selected web resources</returns>
        public List<WebResource> GetCheckedResources()
        {
            var nodes = new List<TreeNode>();

            GetNodes(nodes, tv, true);

            return nodes.Select(n => (WebResource)n.Tag).ToList();
        }

        /// <summary>
        /// Retrieve from disk and store web resources in the current control
        /// </summary>
        /// <param name="folderPath">Path of the folder from where to load the resource</param>
        /// <param name="extensionsToLoad">Extensions valid for files to load</param>
        public List<string> LoadWebResourcesFromDisk(string folderPath, List<string> extensionsToLoad)
        {
            tv.Nodes.Clear();

            var invalidFilenames = new List<string>();

            var di = new DirectoryInfo(folderPath);

            // Only folder ending with "_" character are processed. They represent
            // the prefix of customizations
            foreach (DirectoryInfo diChild in di.GetDirectories("*_", SearchOption.TopDirectoryOnly))
            {
                if (WebResource.IsNameValid(diChild.Name))
                {
                    // Create a root treenode
                    var rootFolderNode = new TreeNode(diChild.Name)
                    {
                        ImageIndex = 0,
                        Tag = diChild.FullName
                    };
                    tv.Nodes.Add(rootFolderNode);

                    // Add child folders
                    CreateFolderStructure(rootFolderNode, diChild, invalidFilenames, extensionsToLoad);
                }
                else
                {
                    invalidFilenames.Add(diChild.FullName);
                }
            }

            // For each files that wouldn't use virtual folders structure, all
            // files are processed
            foreach (FileInfo fiChild in di.GetFiles("*.*", SearchOption.TopDirectoryOnly))
            {
                // We cannot process files without extensions since we
                // won't be able to identify the type of file
                if (fiChild.Extension.Length == 0)
                {
                    invalidFilenames.Add(fiChild.FullName);
                    continue;
                }

                // Do not process files with invalid names or extensions not related
                // to web resources
                if (WebResource.IsNameValid(fiChild.Name) && WebResource.IsValidExtension(fiChild.Extension))
                {
                    // If the file is of type we want to load, the node is created
                    if (extensionsToLoad.Contains(fiChild.Extension))
                    {
                        CreateWebResourceNode(fiChild, tv);
                    }
                }
                else if (!WebResource.IsNameValid(fiChild.Name) || !WebResource.SkipErrorForInvalidExtension(fiChild.Extension))
                {
                    invalidFilenames.Add(fiChild.FullName);    
                }
            }

            tv.ExpandAll();
            tv.TreeViewNodeSorter = new NodeSorter();
            tv.Sort();

            return invalidFilenames;
        }

        /// <summary>
        /// Retrieve and store web resources in the current control
        /// </summary>
        /// <param name="solutionId">Solution unique identifier</param>
        /// <param name="typesToLoad">Web resources types to load</param>
        public void LoadWebResourcesFromServer(Guid solutionId, List<int> typesToLoad)
        {
            var wrManager = new AppCode.WebResourceManager(Service);
            var webResourcesCollection = wrManager.RetrieveWebResources(solutionId, typesToLoad);
            WebResources = webResourcesCollection.Entities.Select(e => new WebResource(e)).ToList();
        }

        /// <summary>
        /// Refresh content of the selected node from disk
        /// </summary>
        /// <returns>List of invalid files names found</returns>
        public List<string> RefreshFromDisk()
        {
            var invalidFilenames = new List<string>();
            var selectedItem = tv.SelectedNode;

            if (selectedItem != null)
            {
                var tag = selectedItem.Tag;
                if (tag is string)
                {
                    UpdateFolderStructure(selectedItem, new DirectoryInfo(tag.ToString()), invalidFilenames, null);
                }
                else
                {
                    var resource = tag as WebResource;
                    if (resource != null)
                    {
                        if (string.IsNullOrEmpty(resource.FilePath))
                        {
                            throw new Exception("Cannot refresh from disk if FilePath property is not set for the web resource");
                        }

                        resource.Entity["content"] = Convert.ToBase64String(File.ReadAllBytes(resource.FilePath));
                        resource.RefreshAssociatedContent();
                        tv_AfterSelect(tv, new TreeViewEventArgs(selectedItem));
                    }
                }
            }

            return invalidFilenames;
        }

        /// <summary>
        /// Remove node from the treeview
        /// </summary>
        /// <param name="node">Node to remove</param>
        public void RemoveNode(TreeNode node)
        {
            tv.Nodes.Remove(node);
        }

        /// <summary>
        /// Add a node in the current parent node in the treeview
        /// </summary>
        /// <param name="nameParts">Web resource name parts</param>
        /// <param name="index">Index of the part to use</param>
        /// <param name="parent">Parent item (TreeView or TreeNode)</param>
        /// <param name="webResource">Web resource content</param>
        private void AddNode(string[] nameParts, int index, object parent, WebResource webResource)
        {
            if (index == 0)
            {
                var treeview = (TreeView)parent;

                // If the current root for the web resource exists
                if (treeview.Nodes.Find(nameParts[index], false).Length == 0)
                {
                    var node = new TreeNode(nameParts[index]);
                    node.Name = node.Text;

                    // If the name part is the last one, it is the web resource
                    // so it is stored in the Tag property for later usage
                    if (index == nameParts.Length - 1)
                    {
                        node.Tag = webResource;

                        int imageIndex = webResource.Entity.GetAttributeValue<OptionSetValue>("webresourcetype").Value + 1;
                        node.ImageIndex = imageIndex;
                        node.SelectedImageIndex = imageIndex;
                    }
                    else
                    {
                        // 0 is image index for Root image
                        node.ImageIndex = 0;
                        node.SelectedImageIndex = 0;
                    }

                    treeview.Nodes.Add(node);

                    AddNode(nameParts, index + 1, node, webResource);
                }
                else
                {
                    AddNode(nameParts, index + 1, treeview.Nodes.Find(nameParts[index], false)[0], webResource);
                }
            }
            else if (index < nameParts.Length)
            {
                var treenode = (TreeNode)parent;

                if (treenode.Nodes.Find(nameParts[index], false).Length == 0)
                {
                    var node = new TreeNode(nameParts[index]);
                    node.Name = node.Text;

                    // If the name part is the last one, it is the web resource
                    // so it is stored in the Tag property for later usage
                    if (index == nameParts.Length - 1)
                    {
                        node.Tag = webResource;
                        int imageIndex = webResource.Entity.GetAttributeValue<OptionSetValue>("webresourcetype").Value + 1;
                        node.ImageIndex = imageIndex;
                        node.SelectedImageIndex = imageIndex;

                        webResource.Node = node;
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
                            // 0 is image index for Folder image
                            node.ImageIndex = 1;
                            node.SelectedImageIndex = 1;
                        }
                    }

                    treenode.Nodes.Add(node);

                    AddNode(nameParts, index + 1, node, webResource);
                }
                else
                {
                    AddNode(nameParts, index + 1, treenode.Nodes.Find(nameParts[index], false)[0], webResource);
                }
            }
        }

        /// <summary>
        /// Event when the checkbox "Select/Unselect all" is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelectAll_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in tv.Nodes)
                node.Checked = chkSelectAll.Checked;
        }

        /// <summary>
        /// Create a TreeView folder structure depending on the DirectoryInfo
        /// element in parameters
        /// </summary>
        /// <param name="parentFolderNode">Current TreeNode</param>
        /// <param name="di">Current physical directory info</param>
        /// <param name="invalidFilenames"></param>
        /// <param name="extensionsToLoad"></param>
        private void CreateFolderStructure(TreeNode parentFolderNode, DirectoryInfo di, List<string> invalidFilenames, List<string> extensionsToLoad)
        {
            foreach (DirectoryInfo diChild in di.GetDirectories())
            {
                if (!WebResource.IsNameValid(diChild.Name))
                {
                    invalidFilenames.Add(diChild.FullName);
                    continue;
                }

                // If the current physical directory has sub directories or
                // javascript file, a new TreeNode has to be created
                if (diChild.GetDirectories().Length > 0 || diChild.GetFiles("*.*").Length > 0)
                {
                    var folderNode = new TreeNode(diChild.Name) { ImageIndex = 1, SelectedImageIndex = 1, Tag = diChild.FullName, Name = diChild.Name };

                    parentFolderNode.Nodes.Add(folderNode);

                    CreateFolderStructure(folderNode, diChild, invalidFilenames, extensionsToLoad);
                }
            }

            foreach (FileInfo fiChild in di.GetFiles("*.*", SearchOption.TopDirectoryOnly))
            {
                if (WebResource.IsNameValid(fiChild.Name) && WebResource.IsValidExtension(fiChild.Extension))
                {
                    if (extensionsToLoad == null || extensionsToLoad.Contains(fiChild.Extension))
                    {
                        // Create a TreeNode for each javascript file
                        CreateWebResourceNode(fiChild, parentFolderNode);
                    }
                } else if (!WebResource.IsNameValid(fiChild.Name) || !WebResource.SkipErrorForInvalidExtension(fiChild.Extension))
                {
                    invalidFilenames.Add(fiChild.FullName);
                }
            }
        }

        /// <summary>
        /// Create a TreeNode for the javascript file passed in parameter
        /// </summary>
        /// <param name="fiChild">Javascript FileInfo</param>
        /// <param name="parent">Parent element (TreeView or TreeNode)</param>
        private void CreateWebResourceNode(FileInfo fiChild, object parent)
        {
            string scriptName = fiChild.Name;
            string fileName = scriptName;

            // Creating the script "name" attribute which depends on the current
            // TreeView structure
            var treeNode = parent as TreeNode;
            if (treeNode != null)
            {
                var parentNode = treeNode;

                while (parentNode != null && !parentNode.Text.EndsWith("_"))
                {
                    fileName = string.Format("{0}/{1}", parentNode.Text, fileName);
                    parentNode = parentNode.Parent;
                }

                if (parentNode != null)
                    fileName = string.Format("{0}/{1}", parentNode.Text, fileName);
            }

            // Generate display name (Credit to badhabits)
            var lastSlash = fileName.LastIndexOf("/", StringComparison.Ordinal);
            var displayName = lastSlash > -1
                ? fileName.Substring(lastSlash + 1)
                : fileName;

            // Create new virtual web resource
            var resource = WebResource.LoadWebResourceFromDisk(fiChild.FullName, fileName, displayName);

            var imageIndex = WebResource.GetImageIndexFromExtension(fiChild.Extension);
            var node = new TreeNode
            {
                Text = scriptName,
                Name = scriptName,
                Tag = resource,
                ImageIndex = imageIndex,
                SelectedImageIndex = imageIndex
            };

            resource.Node = node;

            var parentTreeNode = parent as TreeNode;
            if (parentTreeNode != null)
            {
                parentTreeNode.Nodes.Add(node);
            }
            else
            {
                ((TreeView)parent).Nodes.Add(node);
            }
        }

        /// <summary>
        /// Obtain the full name for a web resource
        /// </summary>
        /// <param name="node">Node for which name is required</param>
        /// <returns>Name of the web resource related to the node</returns>
        private string GetName(TreeNode node)
        {
            string name = node.Text;

            while (node.Parent != null)
            {
                node = node.Parent;
                name = string.Format("{0}/{1}", node.Text, name);
            }

            return name;
        }

        /// <summary>
        /// Get a list of nodes from the treeview
        /// </summary>
        /// <param name="nodes">List of nodes to fill</param>
        /// <param name="parent">Current parent item</param>
        /// <param name="onlyCheckedNodes">Define if only checked item should be returned</param>
        private void GetNodes(ICollection<TreeNode> nodes, object parent, bool onlyCheckedNodes)
        {
            var tView = parent as TreeView;
            if (tView != null)
            {
                foreach (TreeNode node in tView.Nodes)
                {
                    if (onlyCheckedNodes && node.Checked || !onlyCheckedNodes)
                    {
                        var wr = node.Tag as WebResource;
                        if (wr != null)
                        {
                            string name = GetName(node);
                            wr.Entity["name"] = name;

                            nodes.Add(node);
                        }
                    }

                    GetNodes(nodes, node, onlyCheckedNodes);
                }
            }
            else
            {
                foreach (TreeNode node in ((TreeNode)parent).Nodes)
                {
                    if (onlyCheckedNodes && node.Checked || !onlyCheckedNodes)
                        if (node.Tag is WebResource)
                        {
                            string name = GetName(node);
                            ((WebResource)node.Tag).Entity["name"] = name;

                            nodes.Add(node);
                        }

                    GetNodes(nodes, node, onlyCheckedNodes);
                }
            }
        }

        private void llCollapseAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tv.CollapseAll();
        }

        private void llExpandAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tv.ExpandAll();
        }

        private void tv_AfterCheck(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode node in e.Node.Nodes)
            {
                node.Checked = e.Node.Checked;
            }
        }

        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (WebResourceSelected != null)
            {
                WebResourceSelected(this, new WebResourceSelectedEventArgs(SelectedResource));
            }
        }

        private void tv_DragDrop(object sender, DragEventArgs e)
        {
            var errorList = new List<string>();

            // Retrieve the current selected node
            var treeview = (TreeView)sender;
            var location = tv.PointToScreen(Point.Empty);
            var currentNode = treeview.GetNodeAt(e.X - location.X, e.Y - location.Y);

            var files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (var file in files)
            {
                var fi = new FileInfo(file);
                string nodeObjectName = GetName(currentNode);

                // Test valid characters
                if (WebResource.IsNameValid(fi.Name))
                {
                    // Create CRM web resource
                    var newWebResource = WebResource.LoadWebResourceFromDisk(file, string.Format("{0}/{1}", nodeObjectName, fi.Name));

                    // Create file if the current node has a filepath in its tag
                    // this means, wen resources come from disk
                    if (currentNode.Tag != null && currentNode.Tag is string &&
                        Directory.Exists(currentNode.Tag.ToString()))
                    {
                        var resultingFileName = Path.Combine(currentNode.Tag.ToString(), fi.Name);

                        if (resultingFileName.ToLower() != fi.FullName.ToLower())
                        {
                            if (DialogResult.Yes == MessageBox.Show(
                                    "Would you like to also copy this file to folder '" + currentNode.Tag + "'?",
                                    "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            {
                                File.WriteAllBytes(resultingFileName, File.ReadAllBytes(file));
                            }
                        }
                    }

                    var node = new TreeNode(fi.Name)
                    {
                        ImageIndex = WebResource.GetImageIndexFromExtension(fi.Extension.Remove(0, 1))
                    };
                    node.SelectedImageIndex = node.ImageIndex;
                    node.Tag = newWebResource;

                    newWebResource.Node = node;

                    currentNode.Nodes.Add(node);
                    currentNode.Expand();
                }
                else
                {
                    errorList.Add(file);
                }
            }

            if (errorList.Any())
            {
                MessageBox.Show(ParentForm,
                    Resources.WebresourceTreeView_InvalidFileNameWarningMessage + string.Join("\r\n", errorList),
                    Resources.MessageBox_WarningTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void tv_DragOver(object sender, DragEventArgs e)
        {
            // Retrieve the current selected node
            var treeview = (TreeView)sender;
            var treeViewLocation = treeview.PointToScreen(Point.Empty);
            var currentNode = treeview.GetNodeAt(e.X - treeViewLocation.X, e.Y - treeViewLocation.Y);

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // File must be an expected file format
                bool isExtensionValid = files.All(f => WebResource.IsValidExtension(Path.GetExtension(f)));

                // Destination node must be a Root or Folder node
                bool isNodeValid = currentNode != null && currentNode.ImageIndex <= 1;

                if (isNodeValid)
                {
                    treeview.SelectedNode = currentNode;
                }

                e.Effect = files.Length > 0 && isExtensionValid && isNodeValid
                    ? DragDropEffects.All
                    : DragDropEffects.None;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void tv_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            if (WebResourceContextMenuRequested != null)
            {
                var targetNode = tv.GetNodeAt(e.X, e.Y);

                if (targetNode == null)
                {
                    return;
                }

                tv.SelectedNode = targetNode;

                WebResourceContextMenuRequested(this, new WebResourceContextMenuRequestedEventArgs(tv.SelectedNode, e.Location));
            }
        }

        private void UpdateFolderStructure(TreeNode parentFolderNode, DirectoryInfo di, List<string> invalidFilenames, List<string> extensionsToLoad)
        {
            var subNodes = parentFolderNode.Nodes;
            var subFolders = di.GetDirectories();

            foreach (var subFolder in subFolders)
            {
                if (!subNodes.ContainsKey(subFolder.Name) || subNodes[subFolder.Name].ImageIndex != 1)
                {
                    var folderNode = new TreeNode(subFolder.Name) { ImageIndex = 1, SelectedImageIndex = 1, Tag = subFolder.FullName, Name = subFolder.Name };
                    parentFolderNode.Nodes.Add(folderNode);

                    UpdateFolderStructure(folderNode, subFolder, invalidFilenames, extensionsToLoad);
                }
                else
                {
                    UpdateFolderStructure(subNodes[subFolder.Name], subFolder, invalidFilenames, extensionsToLoad);
                }
            }

            foreach (FileInfo fiChild in di.GetFiles("*.*", SearchOption.TopDirectoryOnly))
            {
                if (WebResource.IsNameValid(fiChild.Name) && WebResource.IsValidExtension(fiChild.Extension))
                {
                    if (extensionsToLoad == null || extensionsToLoad.Contains(fiChild.Extension))
                    {
                        if (!subNodes.ContainsKey(fiChild.Name) || subNodes[fiChild.Name].ImageIndex <= 1)
                        {
                            CreateWebResourceNode(fiChild, parentFolderNode);
                        }
                        else
                        {
                            var wr = (WebResource)subNodes[fiChild.Name].Tag;
                            wr.Entity["content"] = Convert.ToBase64String(File.ReadAllBytes(wr.FilePath));
                            wr.RefreshAssociatedContent();
                        }
                    }
                }
                else  if (!WebResource.IsNameValid(fiChild.Name) || !WebResource.SkipErrorForInvalidExtension(fiChild.Extension))
                {
                    invalidFilenames.Add(fiChild.FullName);
                }
            }
        }
    }
}