// PROJECT : MsCrmTools.SiteMapEditor
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MsCrmTools.SiteMapEditor.AppCode
{
    /// <summary>
    /// Clipboard to manage Treeview nodes action
    /// </summary>
    internal class Clipboard
    {
        #region Variables

        private bool _hasBeenPasted;

        private bool _isCutAction;

        /// <summary>
        /// Original index of treenode in memory
        /// </summary>
        private int _originalIndex;

        /// <summary>
        /// Original parent of treenode in memory
        /// </summary>
        private TreeNode _originalParentNode;

        /// <summary>
        /// TreeNode in memory
        /// </summary>
        private TreeNode _tempTreeNode;

        #endregion Variables

        #region Methods

        /// <summary>
        /// Copies the selected TreeNode
        /// </summary>
        /// <param name="node">selected TreeNode</param>
        /// <param name="isCut">Flag that indicates if the copy action is a cut action</param>
        internal void Copy(TreeNode node, bool isCut = false)
        {
            // If there was already a TreeNode in memory which is different
            // from the currently selected one, we replace the current in-memory
            // TreeNode to its original location
            if (!_hasBeenPasted && _tempTreeNode != null && _tempTreeNode != node && _isCutAction)
            {
                _originalParentNode.Nodes.Insert(_originalIndex, _tempTreeNode);
            }

            // Saves the current selected TreeNode information
            _tempTreeNode = node;
            _originalParentNode = node.Parent;
            _originalIndex = node.Index;

            _isCutAction = isCut;
            _hasBeenPasted = false;
        }

        /// <summary>
        /// Cuts the selected TreeNode
        /// </summary>
        /// <param name="node">Selected TreeNode</param>
        internal void Cut(TreeNode node)
        {
            Copy(node, true);

            node.Remove();
        }

        /// <summary>
        /// Checks if the in-memory TreeNode can be pasted to the selected TreeNode
        /// </summary>
        /// <param name="targetNodeName">TreeNode currently selected</param>
        /// <returns>Flag that indicates if the in-memory TreeNode can be pasted</returns>
        internal bool IsValidForPaste(string targetNodeName)
        {
            if (_tempTreeNode != null)
            {
                switch (targetNodeName)
                {
                    case "SiteMap":
                        return _tempTreeNode.Text.StartsWith("Area");

                    case "Area":
                        return _tempTreeNode.Text.StartsWith("Group") || _tempTreeNode.Text.StartsWith("Descriptions") || _tempTreeNode.Text.StartsWith("Titles");

                    case "Group":
                        return _tempTreeNode.Text.StartsWith("SubArea") || _tempTreeNode.Text.StartsWith("Descriptions") || _tempTreeNode.Text.StartsWith("Titles");

                    case "SubArea":
                        return _tempTreeNode.Text.StartsWith("Privilege") || _tempTreeNode.Text.StartsWith("Descriptions") || _tempTreeNode.Text.StartsWith("Titles");

                    case "Titles":
                        return _tempTreeNode.Text.StartsWith("Title");

                    case "Descriptions":
                        return _tempTreeNode.Text.StartsWith("Description");

                    default:
                        return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Pastes the in-memory TreeNode under the target TreeNode
        /// </summary>
        /// <param name="targetNode">Target TreeNode</param>
        internal void Paste(TreeNode targetNode)
        {
            if (_tempTreeNode == null)
                return;

            if (ValidatePaste(targetNode))
            {
                // The in-memory TreeNode is cloned to avoid having two occurences
                // of the same TreeNode in the TreeView
                var clonedNode = (TreeNode)_tempTreeNode.Clone();

                // If the target TreeNode already contains the in-memory TreeNode or
                // another TreeNode with the same Text, we need to change the name
                if (targetNode.Nodes.ContainsKey(_tempTreeNode.Text.Replace(" ", "")))
                {
                    clonedNode.Text = clonedNode.Text.Replace("(", "(Copy of ");

                    // Clone content
                    UpdateContentForCloning(clonedNode);

                    // Update ids
                    var ticks = DateTime.Now.Ticks;
                    UpdateIds(clonedNode, ref ticks);
                }

                targetNode.Nodes.Add(clonedNode);
                targetNode.TreeView.SelectedNode = clonedNode;

                // Clean the in-memory TreeNode
                // tempTreeNode = null;
                _hasBeenPasted = true;
            }
            else
            {
                MessageBox.Show("You can't paste this item under the selected node!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateContentForCloning(TreeNode node)
        {
            var attributes = (Dictionary<string, string>)node.Tag;
            var clonedAttributes = attributes.Keys.ToDictionary(key => key, key => attributes[key]);
            clonedAttributes.Remove("ResourceId");
            clonedAttributes.Remove("DescriptionResourceId");
            node.Tag = clonedAttributes;

            foreach (TreeNode childNode in node.Nodes)
            {
                UpdateContentForCloning(childNode);
            }
        }

        /// <summary>
        /// Updates all Ids for the current node tag
        /// </summary>
        /// <param name="node">Current treenode</param>
        /// <param name="idPart"> </param>
        private void UpdateIds(TreeNode node, ref long idPart)
        {
            var attributes = (Dictionary<string, string>)node.Tag;
            if (attributes.ContainsKey("Id"))
                attributes["Id"] = string.Format("{0}_{1}",
                                                 attributes["Id"],
                                                 idPart);

            node.Tag = attributes;

            idPart++;

            foreach (TreeNode childNode in node.Nodes)
            {
                UpdateIds(childNode, ref idPart);
            }
        }

        /// <summary>
        /// Validates if the TreeNode in memory can be pasted in the specified
        /// target TreeNode
        /// </summary>
        /// <param name="targetNode"></param>
        /// <returns></returns>
        private bool ValidatePaste(TreeNode targetNode)
        {
            if (targetNode.Text.StartsWith("SiteMap") && _tempTreeNode.Text.StartsWith("Area"))
                return true;
            if (targetNode.Text.StartsWith("Area") && (_tempTreeNode.Text.StartsWith("Group") ||
                _tempTreeNode.Text.StartsWith("Descriptions") && targetNode.Nodes.Find("Descriptions", false).Length == 0 ||
                 _tempTreeNode.Text.StartsWith("Titles") && targetNode.Nodes.Find("Titles", false).Length == 0))
                return true;
            if (targetNode.Text.StartsWith("Group") && (_tempTreeNode.Text.StartsWith("SubArea") ||
                _tempTreeNode.Text.StartsWith("Descriptions") && targetNode.Nodes.Find("Descriptions", false).Length == 0 ||
                 _tempTreeNode.Text.StartsWith("Titles") && targetNode.Nodes.Find("Titles", false).Length == 0))
                return true;
            if (targetNode.Text.StartsWith("SubArea") && (_tempTreeNode.Text.StartsWith("Privilege") ||
               _tempTreeNode.Text.StartsWith("Descriptions") && targetNode.Nodes.Find("Descriptions", false).Length == 0 ||
                _tempTreeNode.Text.StartsWith("Titles") && targetNode.Nodes.Find("Titles", false).Length == 0))
                return true;
            if (targetNode.Text.StartsWith("Descriptions") && _tempTreeNode.Text.StartsWith("Description"))
                return true;
            if (targetNode.Text.StartsWith("Titles") && _tempTreeNode.Text.StartsWith("Title"))
                return true;
            return false;
        }

        #endregion Methods
    }
}