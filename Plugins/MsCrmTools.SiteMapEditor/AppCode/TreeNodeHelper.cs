// PROJECT : MsCrmTools.SiteMapEditor
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace MsCrmTools.SiteMapEditor.AppCode
{
    /// <summary>
    /// Class that helps to manage TreeNode from SiteMap Treeview
    /// </summary>
    internal class TreeNodeHelper
    {
        #region Methods

        /// <summary>
        /// Adds a context menu to a TreeNode control
        /// </summary>
        /// <param name="node">TreeNode where to add the context menu</param>
        /// <param name="form">Current application form</param>
        public static void AddContextMenu(TreeNode node, SiteMapEditor form)
        {
            var collec = (Dictionary<string, string>)node.Tag;

            HideAllContextMenuItems(form.nodeMenu);

            switch (node.Text.Split(' ')[0])
            {
                case "SiteMap":
                    {
                        form.addSystemAreaToolStripMenuItem.Visible = true;
                        form.toolStripSeparatorSystem.Visible = true;
                        form.addAreaToolStripMenuItem.Visible = true;

                        form.toolStripSeparatorAction.Visible = true;
                        form.deleteToolStripMenuItem.Visible = true;
                        form.deleteToolStripMenuItem.Enabled = true;

                        form.pasteToolStripMenuItem.Enabled = form.clipboard.IsValidForPaste("SiteMap");
                    }
                    break;

                case "Area":
                    {
                        form.addGroupToolStripMenuItem.Visible = true;
                        form.toolStripSeparatorSystem.Visible = true;
                        form.addSystemGroupToolStripMenuItem.Visible = true;
                        form.toolStripSeparatorAction.Visible = true;
                        form.deleteToolStripMenuItem.Visible = true;
                        form.deleteToolStripMenuItem.Enabled = true;
                        form.addDescriptionsToolStripMenuItem.Visible = true;
                        form.addDescriptionsToolStripMenuItem.Enabled = node.Nodes.Find("Descriptions", false).Length == 0;
                        form.addTitlesToolStripMenuItem.Visible = true;
                        form.addTitlesToolStripMenuItem.Enabled = node.Nodes.Find("Titles", false).Length == 0;

                        form.disableToolStripMenuItem.Visible = true;
                        form.disableToolStripMenuItem.Enabled = true;
                        form.disableToolStripMenuItem.Text = collec.ContainsKey("_disabled") ? "Enable" : "Disable";

                        form.cutToolStripMenuItem.Enabled = true;
                        form.copyToolStripMenuItem.Enabled = true;
                        form.pasteToolStripMenuItem.Enabled = form.clipboard.IsValidForPaste("Area");
                    }
                    break;

                case "Group":
                    {
                        form.addSystemSubAreaToolStripMenuItem.Visible = true;
                        form.toolStripSeparatorSystem.Visible = true;
                        form.addSubAreaToolStripMenuItem.Visible = true;
                        form.addDescriptionsToolStripMenuItem.Visible = true;
                        form.addDescriptionsToolStripMenuItem.Enabled = node.Nodes.Find("Descriptions", false).Length == 0;
                        form.addTitlesToolStripMenuItem.Visible = true;
                        form.addTitlesToolStripMenuItem.Enabled = node.Nodes.Find("Titles", false).Length == 0;
                        form.toolStripSeparatorAction.Visible = true;
                        form.deleteToolStripMenuItem.Visible = true;
                        form.deleteToolStripMenuItem.Enabled = true;

                        form.disableToolStripMenuItem.Visible = true;
                        form.disableToolStripMenuItem.Enabled = true;
                        form.disableToolStripMenuItem.Text = collec.ContainsKey("_disabled") ? "Enable" : "Disable";

                        form.cutToolStripMenuItem.Enabled = true;
                        form.copyToolStripMenuItem.Enabled = true;
                        form.pasteToolStripMenuItem.Enabled = form.clipboard.IsValidForPaste("Group");

                        foreach (TreeNode childNode in node.Nodes)
                        {
                            if (childNode.Text == "Descriptions")
                            {
                                form.addDescriptionsToolStripMenuItem.Enabled = false;
                                break;
                            }

                            if (childNode.Text == "Titles")
                            {
                                form.addTitlesToolStripMenuItem.Enabled = false;
                                break;
                            }
                        }
                    }
                    break;

                case "SubArea":
                    {
                        form.addPrivilegeToolStripMenuItem.Visible = true;
                        form.addDescriptionsToolStripMenuItem.Visible = true;
                        form.addDescriptionsToolStripMenuItem.Enabled = node.Nodes.Find("Descriptions", false).Length == 0;
                        form.addTitlesToolStripMenuItem.Visible = true;
                        form.addTitlesToolStripMenuItem.Enabled = node.Nodes.Find("Titles", false).Length == 0;
                        form.toolStripSeparatorAction.Visible = true;
                        form.deleteToolStripMenuItem.Visible = true;
                        form.deleteToolStripMenuItem.Enabled = true;

                        form.disableToolStripMenuItem.Visible = true;
                        form.disableToolStripMenuItem.Enabled = true;
                        form.disableToolStripMenuItem.Text = collec.ContainsKey("_disabled") ? "Enable" : "Disable";

                        form.cutToolStripMenuItem.Enabled = true;
                        form.copyToolStripMenuItem.Enabled = true;
                        form.pasteToolStripMenuItem.Enabled = form.clipboard.IsValidForPaste("SubArea");

                        foreach (TreeNode childNode in node.Nodes)
                        {
                            if (childNode.Text == "Titles")
                            {
                                form.addTitlesToolStripMenuItem.Enabled = false;
                                break;
                            }
                        }
                    }
                    break;

                case "Privilege":
                    {
                        form.deleteToolStripMenuItem.Visible = true;
                        form.deleteToolStripMenuItem.Enabled = true;
                        form.toolStripSeparatorBeginOfEdition.Visible = false;

                        form.disableToolStripMenuItem.Visible = true;
                        form.disableToolStripMenuItem.Enabled = true;
                        form.disableToolStripMenuItem.Text = collec.ContainsKey("_disabled") ? "Enable" : "Disable";

                        form.toolStripSeparatorAction.Visible = true;
                        form.cutToolStripMenuItem.Enabled = true;
                        form.copyToolStripMenuItem.Enabled = true;
                    }
                    break;

                case "Titles":
                    {
                        form.addTitleToolStripMenuItem.Visible = true;
                        form.toolStripSeparatorAction.Visible = true;
                        form.deleteToolStripMenuItem.Visible = true;
                        form.deleteToolStripMenuItem.Enabled = true;

                        form.cutToolStripMenuItem.Enabled = true;
                        form.copyToolStripMenuItem.Enabled = true;
                        form.pasteToolStripMenuItem.Enabled = form.clipboard.IsValidForPaste("Titles");
                    }
                    break;

                case "Descriptions":
                    {
                        form.addDescriptionToolStripMenuItem.Visible = true;
                        form.toolStripSeparatorAction.Visible = true;
                        form.deleteToolStripMenuItem.Visible = true;
                        form.deleteToolStripMenuItem.Enabled = true;

                        form.cutToolStripMenuItem.Enabled = true;
                        form.copyToolStripMenuItem.Enabled = true;
                        form.pasteToolStripMenuItem.Enabled = form.clipboard.IsValidForPaste("Descriptions");
                    }
                    break;

                case "Title":
                case "Description":
                    {
                        form.deleteToolStripMenuItem.Visible = true;
                        form.toolStripSeparatorAction.Visible = true;

                        form.toolStripSeparatorBeginOfEdition.Visible = false;
                        form.cutToolStripMenuItem.Enabled = true;
                        form.copyToolStripMenuItem.Enabled = true;

                        if (node.Parent != null && node.Parent.Nodes.Count == 1)
                            form.deleteToolStripMenuItem.Enabled = false;
                    }
                    break;
            }

            node.ContextMenuStrip = form.nodeMenu;
        }

        /// <summary>
        /// Adds a new TreeNode to the parent object from the XmlNode information
        /// </summary>
        /// <param name="parentObject">Object (TreeNode or TreeView) where to add a new TreeNode</param>
        /// <param name="xmlNode">Xml node from the sitemap</param>
        /// <param name="form">Current application form</param>
        /// <param name="isDisabled"> </param>
        public static void AddTreeViewNode(object parentObject, XmlNode xmlNode, SiteMapEditor form, bool isDisabled = false)
        {
            TreeNode node = new TreeNode(xmlNode.Name);

            Dictionary<string, string> attributes = new Dictionary<string, string>();

            foreach (XmlAttribute attr in xmlNode.Attributes)
            {
                attributes.Add(attr.Name, attr.Value);
            }

            if (xmlNode.Attributes["Id"] != null)
            {
                node.Text += " (" + xmlNode.Attributes["Id"].Value + ")";
            }
            if (xmlNode.Attributes["LCID"] != null)
            {
                node.Text += " (" + xmlNode.Attributes["LCID"].Value + ")";
            }

            node.Name = node.Text.Replace(" ", "");

            if (isDisabled)
            {
                node.ToolTipText =
                    "This node is disabled and won't appear in Microsoft Dynamics CRM 2011. Right click this node and enable it and make it appear on Microsoft Dynamics CRM 2011";
                node.ForeColor = Color.Gray;
                node.Text += " - disabled";
                attributes.Add("_disabled", "true");
            }

            node.Tag = attributes;

            AddContextMenu(node, form);

            if (parentObject is TreeView)
            {
                ((TreeView)parentObject).Nodes.Add(node);
            }
            else if (parentObject is TreeNode)
            {
                ((TreeNode)parentObject).Nodes.Add(node);
            }
            else
            {
                throw new Exception("AddTreeViewNode: Unsupported control type");
            }

            foreach (XmlNode childNode in xmlNode.ChildNodes)
            {
                if (childNode.NodeType != XmlNodeType.Comment)
                {
                    AddTreeViewNode(node, childNode, form);
                }
                else
                {
                    var commentDoc = new XmlDocument();
                    commentDoc.LoadXml(childNode.InnerText);

                    AddTreeViewNode(node, commentDoc.DocumentElement, form, true);
                }
            }
        }

        /// <summary>
        /// Hides all items from a context menu
        /// </summary>
        /// <param name="cm">Context menu to clean</param>
        public static void HideAllContextMenuItems(ContextMenuStrip cm)
        {
            foreach (ToolStripItem o in cm.Items)
            {
                if (o.Text == "Cut" || o.Text == "Copy" || o.Text == "Paste")
                {
                    o.Enabled = false;
                }
                else if (o.Name == "toolStripSeparatorBeginOfEdition" || o is ToolStripSeparator)
                {
                    o.Visible = true;
                }
                else
                {
                    o.Visible = false;
                }
            }
        }

        #endregion Methods
    }
}