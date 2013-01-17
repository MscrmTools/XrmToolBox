// PROJECT : MsCrmTools.WebResourcesManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using MsCrmTools.WebResourcesManager.Forms;

namespace MsCrmTools.WebResourcesManager.AppCode
{
    class TreeViewHelper
    {
        #region Folders and TreeNodes methods

        /// <summary>
        /// Create an item (script file or folder) on disk depending on specifed
        /// TreeNode
        /// </summary>
        /// <param name="node">TreeNode element</param>
        /// <param name="folderPath">Current physical path</param>
        public static void CreateItem(TreeNode node, string folderPath)
        {
            // If current node has child, it is a folder, not a script
            if (node.Nodes.Count > 0)
            {
                var doContinue = node.Nodes.Cast<TreeNode>().Any(childNode => childNode.Checked);

                // Check if current node has child nodes checked. If not, no need
                // to create a folder on disk

                if (doContinue)
                {
                    Directory.CreateDirectory(Path.Combine(folderPath,node.Text));

                    foreach (TreeNode childNode in node.Nodes)
                    {
                        if (node.Checked)
                        {
                            CreateItem(childNode, Path.Combine(folderPath, node.Text));
                        }
                    }
                }
            }
            else
            {
                // If checked, create a script file on disk
                if (node.Checked)
                {
                    var webResource = ((WebResource)node.Tag).WebResourceEntity;

                    using (var writer = new StreamWriter(Path.Combine(folderPath, node.Text), false))
                    {
                        if (webResource.Contains("content"))
                            writer.Write(WebResourceManager.GetContentFromBase64String(webResource["content"].ToString()));
                        else
                            writer.WriteLine(string.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// Create a TreeView folder structure depending on the DirectoryInfo
        /// element in parameters
        /// </summary>
        /// <param name="parentFolderNode">Current TreeNode</param>
        /// <param name="di">Current physical directory info</param>
        /// <param name="invalidFilenames"></param>
        /// <param name="inValidWrNameRegex"></param>
        public static void CreateFolderStructure(TreeNode parentFolderNode, DirectoryInfo di, List<string> invalidFilenames)
        {
            foreach (DirectoryInfo diChild in di.GetDirectories())
            {
                if (WebResource.IsInvalidName(diChild.Name))
                {
                    invalidFilenames.Add(diChild.FullName);
                    continue;
                }
                
                // If the current physical directory has sub directories or 
                // javascript file, a new TreeNode has to be created
                if (diChild.GetDirectories().Length > 0 || diChild.GetFiles("*.*").Length > 0)
                {
                    var folderNode = new TreeNode(diChild.Name) {ImageIndex = 1, SelectedImageIndex = 1};

                    parentFolderNode.Nodes.Add(folderNode);

                    CreateFolderStructure(folderNode, diChild, invalidFilenames);
                }
            }

            foreach (FileInfo fiChild in di.GetFiles("*.*", SearchOption.TopDirectoryOnly))
            {
                if (WebResource.IsInvalidName(fiChild.Name) || !WebResource.ValidExtensions.Contains(fiChild.Extension.Remove(0, 1)))
                {
                    invalidFilenames.Add(fiChild.FullName);
                    continue;
                }

                // Create a TreeNode for each javascript file
                CreateWebResourceNode(fiChild, parentFolderNode);
            }
        }

        /// <summary>
        /// Create a TreeNode for the javascript file passed in parameter
        /// </summary>
        /// <param name="fiChild">Javascript FileInfo</param>
        /// <param name="parent">Parent element (TreeView or TreeNode)</param>
        public static void CreateWebResourceNode(FileInfo fiChild, object parent)
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

                if(parentNode != null)
                    fileName = string.Format("{0}/{1}", parentNode.Text, fileName);
            }

            int imageIndex = 0;

            switch (fiChild.Extension.ToLower().Remove(0, 1))
            {
                case "html":
                case "htm":
                    imageIndex = 2;
                    break;
                case "css":
                    imageIndex = 3;
                    break;
                case "js":
                    imageIndex = 4;
                    break;
                case "png":
                    imageIndex = 6;
                    break;
                case "jpg":
                    imageIndex = 7;
                    break;
                case "gif":
                    imageIndex = 8;
                    break;
                case "xap":
                    imageIndex = 9;
                    break;
                case "xsl":
                    imageIndex = 10;
                    break;
                case "ico":
                    imageIndex = 11;
                    break;
            }


            // Create new virtual web resource
            var script = new Entity("webresource");
            script["name"] = fileName;
            script["webresourcetype"] = new OptionSetValue(imageIndex - 1);

            // Add content
            script["content"] = Convert.ToBase64String(File.ReadAllBytes(fiChild.FullName));

            var scriptObject = new WebResource(script, fiChild.FullName);

            var node = new TreeNode
            {
                Text = scriptName,
                Tag = scriptObject,
                ImageIndex = imageIndex,
                SelectedImageIndex = imageIndex
            };

            var node1 = parent as TreeNode;
            if (node1 != null)
            {
                node1.Nodes.Add(node);
            }
            else
            {
                ((TreeView)parent).Nodes.Add(node);
            }
        }

        #endregion Folders and TreeNodes methods

        /// <summary>
        /// Propagate the current node checked state to the child nodes
        /// </summary>
        /// <param name="node">Current TreeNode</param>
        /// <param name="status">Checked state</param>
        public static void ChangeChildNodeCheckStatus(TreeNode node, bool status)
        {
            node.Checked = status;

            foreach (TreeNode childNode in node.Nodes)
            {
                ChangeChildNodeCheckStatus(childNode, status);
            }
        }

        /// <summary>
        /// Propagate current node checked state to its parent node
        /// </summary>
        /// <param name="node">Current TreeNode</param>
        /// <param name="status">Checked state</param>
        public static void ChangeParentNodeCheckStatus(TreeNode node, bool status)
        {
            node.Checked = status;

            if (node.Parent != null)
            {
                int childNodesCheckedCount = 0;
                childNodesCheckedCount = CountCheckNodes(node.Parent, childNodesCheckedCount);

                if (status == false)
                {
                    if (childNodesCheckedCount == 1)
                    {
                        node.Parent.Checked = false;
                        ChangeParentNodeCheckStatus(node.Parent, false);
                    }
                }
                else
                {
                    node.Parent.Checked = true;
                    ChangeParentNodeCheckStatus(node.Parent, true);
                }
            }
        }

        /// <summary>
        /// Counts the number of checked TreeNodes
        /// </summary>
        /// <param name="node">Current TreeNode</param>
        /// <param name="count">Number of checked TreeNodes</param>
        /// <returns>Number of checked TreeNodes updated</returns>
        public static int CountCheckNodes(TreeNode node, int count)
        {
            if (node.Checked)
                count++;

            foreach (TreeNode childNode in node.Nodes)
                count = CountCheckNodes(childNode, count);

            return count;
        }

        internal static void GetNodes(List<TreeNode> nodes, object o, bool onlyCheckedNodes)
        {
            var tView = o as TreeView;
            if (tView != null)
            {
                foreach (TreeNode node in tView.Nodes)
                {
                    if (onlyCheckedNodes && node.Checked || !onlyCheckedNodes)
                        if (node.Tag != null)
                        {
                            string name = GetName(node);
                            ((WebResource)node.Tag).WebResourceEntity["name"] = name;

                            nodes.Add(node);
                        }

                    GetNodes(nodes, node, onlyCheckedNodes);
                }
            }
            else
            {
                foreach (TreeNode node in ((TreeNode)o).Nodes)
                {
                    if (onlyCheckedNodes && node.Checked || !onlyCheckedNodes)
                        if (node.Tag != null)
                        {
                            string name = GetName(node);
                            ((WebResource)node.Tag).WebResourceEntity["name"] = name;

                            nodes.Add(node);
                        }

                    GetNodes(nodes, node, onlyCheckedNodes);
                }
            }
        }

        private static string GetName(TreeNode node)
        {
            string name = node.Text;

            while (node.Parent != null)
            {
                node = node.Parent;
                name = string.Format("{0}/{1}", node.Text, name);
            }

            return name;
        }

        internal static void AddRoot(TreeView tv, Control mainControl)
        {
            var nrd = new NewRootDialog { StartPosition = FormStartPosition.CenterParent };

            if (nrd.ShowDialog(mainControl) == DialogResult.OK)
            {
                var rootNode = new TreeNode(nrd.RootName) { ImageIndex = 0, SelectedImageIndex = 0 };

                tv.Nodes.Add(rootNode);

                tv.TreeViewNodeSorter = new NodeSorter();
                tv.Sort();
            }
        }

        internal static void AddFolder(TreeView tvWebResources, Control mainControl)
        {
            var selectedNode = tvWebResources.SelectedNode;
            var nfd = new NewFolderDialog { StartPosition = FormStartPosition.CenterParent };

            if (nfd.ShowDialog(mainControl) == DialogResult.OK)
            {
                var parts = nfd.FolderName.Split('/');
                var currentNode = selectedNode;

                foreach (var part in parts.Where(x => x.Length > 0))
                {
                    var node = new TreeNode(part.Trim()) { ImageIndex = 1, SelectedImageIndex = 1 };

                    currentNode.Nodes.Add(node);
                    tvWebResources.SelectedNode = node;
                    currentNode = node;
                }
            }

            tvWebResources.TreeViewNodeSorter = new NodeSorter();
            tvWebResources.Sort();
        }

        internal static void CreateEmptyWebResource(string extension, TreeView tv, Control mainControl)
        {
            var callerNode = tv.SelectedNode;

            var nwrDialog = new NewWebResourceDialog(extension) { StartPosition = FormStartPosition.CenterParent };

            if (nwrDialog.ShowDialog(mainControl) == DialogResult.OK)
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

        internal static void AddExistingWebResource(TreeView tv, Control mainControl)
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

           if (ofd.ShowDialog(mainControl) == DialogResult.OK)
           {
               var errorList = new List<string>();

               foreach (string fileName in ofd.FileNames)
               {
                   var fi = new FileInfo(fileName);

                   //Test valid characters
                   if (WebResource.IsInvalidName(fi.Name))
                   {
                       errorList.Add(fileName);
                   }
                   else
                   {
                       var webResource = new Entity("webresource");
                       webResource["content"] = Convert.ToBase64String(File.ReadAllBytes(fileName));
                       webResource["webresourcetype"] =
                           new OptionSetValue(WebResource.GetTypeFromExtension(fi.Extension.Remove(0, 1)));
                       webResource["name"] = string.Format("{0}/{1}", name, fi.Name);
                       webResource["displayname"] = string.Format("{0}/{1}", name, fi.Name);
                       var wr = new WebResource(webResource, fileName);

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

        internal static bool CheckOnlyThisNode(TreeView tv)
        {
            TreeNode node = tv.SelectedNode;

            // If no item is selected, return
            if (node == null)
                return true;

            // Retrieve checked web resources
            var nodesList = new List<TreeNode>();
            TreeViewHelper.GetNodes(nodesList, tv, true);

            // Uncheck all items
            foreach (var childNode in nodesList)
            {
                childNode.Checked = false;
            }

            // Check the selected item
            node.Checked = true;
            return false;
        }

        internal static string UpdateNodesContentWithLocalFiles(TreeNodeCollection nodes)
        {
            var fbDialog = new FolderBrowserDialog();
            if (fbDialog.ShowDialog() == DialogResult.OK)
            {
                var sBuilder = new StringBuilder();

                UpdateNodesContentWithLocalFiles(nodes, fbDialog.SelectedPath, new DirectoryInfo(fbDialog.SelectedPath).Name, sBuilder);

                if (sBuilder.Length == 0)
                {
                    sBuilder.AppendLine("No files were updated!");
                }

                return string.Format("The following web resources have been updated with local files content:\r\n{0}", sBuilder.ToString());
            }

            return string.Empty;
        }
        
        private static void UpdateNodesContentWithLocalFiles(TreeNodeCollection nodes, string folderPath, string initialFolderName, StringBuilder sBuilder)
        {
            var folder = new DirectoryInfo(folderPath);
            var files = new List<FileInfo>(folder.GetFiles());
            var directories = new List<DirectoryInfo>(folder.GetDirectories());

            foreach (TreeNode node in nodes)
            {
                var name = node.Text;

                if (node.ImageIndex == 0 || node.ImageIndex == 1)
                {
                    var di = directories.FirstOrDefault(d => d.Name == name);
                    if (di != null)
                    {
                        UpdateNodesContentWithLocalFiles(node.Nodes, di.FullName, initialFolderName, sBuilder);
                    }
                }
                else
                {
                    var file = files.FirstOrDefault(f => f.Name.ToLower() == name.ToLower());
                    if (file != null)
                    {
                        var bytes = File.ReadAllBytes(file.FullName);
                        var content = Convert.ToBase64String(bytes);

                        ((WebResource)node.Tag).WebResourceEntity["content"] = content;

                        string nameToDisplay = name;
                        var currentDirectory = file.Directory;

                        while (initialFolderName != currentDirectory.Name)
                        {
                            nameToDisplay = currentDirectory.Name + "\\" + nameToDisplay;
                            currentDirectory = currentDirectory.Parent;
                        }

                        sBuilder.AppendLine(" - " + nameToDisplay);
                    }
                }
            }
        }
    }
}
