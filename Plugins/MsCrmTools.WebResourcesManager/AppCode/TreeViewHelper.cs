// PROJECT : MsCrmTools.WebResourcesManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using MsCrmTools.WebResourcesManager.Forms;
using MsCrmTools.WebResourcesManager.New.UserControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MsCrmTools.WebResourcesManager.AppCode
{
    internal class TreeViewHelper
    {
        internal static bool CheckOnlyThisNode(WebresourceTreeView tv)
        {
            TreeNode node = tv.SelectedNode;

            // If no item is selected, return
            if (node == null)
                return true;

            // Retrieve checked web resources
            var resources = tv.GetCheckedResources();

            // Uncheck all items
            foreach (var resource in resources)
            {
                resource.Node.Checked = false;
            }

            // Check the selected item
            node.Checked = true;
            return false;
        }

        internal static string UpdateNodesContentWithLocalFiles(TreeNodeCollection nodes)
        {
            var fbDialog = new CustomFolderBrowserDialog(true, false);
            if (fbDialog.ShowDialog() == DialogResult.OK)
            {
                var sBuilder = new StringBuilder();

                UpdateNodesContentWithLocalFiles(nodes, fbDialog.FolderPath, new DirectoryInfo(fbDialog.FolderPath).Name, sBuilder);

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

                        var wr = node.Tag as WebResource;
                        if (wr != null)
                        {
                            wr.Entity["content"] = content;
                            wr.RefreshAssociatedContent();

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
}