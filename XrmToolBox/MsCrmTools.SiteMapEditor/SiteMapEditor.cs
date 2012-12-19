// PROJECT : MsCrmTools.SiteMapEditor
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using MsCrmTools.SiteMapEditor.AppCode;
using MsCrmTools.SiteMapEditor.Controls;
using MsCrmTools.SiteMapEditor.Forms;
using SiteMapEditor.Controls;
using Tanguy.WinForm.Utilities.DelegatesHelpers;
using XrmToolBox;
using Clipboard = MsCrmTools.SiteMapEditor.AppCode.Clipboard;

namespace MsCrmTools.SiteMapEditor
{
    public partial class SiteMapEditor : UserControl, IMsCrmToolsPluginUserControl
    {
        internal static IOrganizationService service;
        internal static List<EntityMetadata> entityCache;
        internal static List<Entity> webResourcesHtmlCache;
        internal static List<Entity> webResourcesImageCache;
        internal Clipboard clipboard = new Clipboard();

        private Panel infoPanel;

        private Entity siteMap;
        private XmlDocument siteMapDoc;


        public SiteMapEditor()
        {
            InitializeComponent();
        }

        #region Main ToolStrip Menu

        private void TsbMainOpenSiteMapClick(object sender, EventArgs e)
        {
            if (service == null)
            {
                if (OnRequestConnection != null)
                {
                    var args = new RequestConnectionEventArgs
                                   {
                                       ActionName = "LoadSiteMap",
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
                LoadSiteMap();
            }
        }

        private void TsbMainImportClick(object sender, EventArgs e)
        {
            if (service == null)
            {
                if (OnRequestConnection != null)
                {
                    var args = new RequestConnectionEventArgs
                                   {
                                       ActionName = "UpdateSiteMap",
                                       Control = this,
                                       Parameter = null
                                   };

                    OnRequestConnection(this, args);
                }
            }
            else
            {
                UpdateSiteMap();
            }
        }

        private void ToolStripButtonLoadSiteMapFromDiskClick(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
                          {
                              Title = "Select a Xml file representing a SiteMap",
                              Filter = "Xml file (*.xml)|*.xml"
                          };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                EnableControls(false);

                siteMapDoc = new XmlDocument();
                siteMapDoc.Load(ofd.FileName);

                if (siteMapDoc.DocumentElement.Name != "SiteMap" ||
                    siteMapDoc.DocumentElement.ChildNodes.Count > 0 &&
                    siteMapDoc.DocumentElement.ChildNodes[0].Name == "SiteMap")
                {
                    MessageBox.Show(this, "Invalid Xml: SiteMap Xml root must be SiteMap!", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                    tsbMainOpenSiteMap.Enabled = true;
                    toolStripButtonLoadSiteMapFromDisk.Enabled = true;
                }
                else
                {
                    DisplaySiteMap();
                    EnableControls(true);
                }
            }
        }

        private void ToolStripButtonSaveSiteMapToDiskClick(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog
                          {
                              Title = "Select a location to save the SiteMap as a Xml file",
                              Filter = "Xml file (*.xml)|*.xml",
                              FileName = "SiteMap.xml"
                          };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                EnableControls(false);

                // Build the Xml SiteMap from SiteMap TreeView
                var doc = new XmlDocument();
                XmlNode rootNode = doc.CreateElement("SiteMap");
                doc.AppendChild(rootNode);

                AddXmlNode(tvSiteMap.Nodes[0], rootNode);

                if (siteMap != null)
                {
                    siteMap["sitemapxml"] = doc.SelectSingleNode("SiteMap/SiteMap").OuterXml;
                }

                siteMapDoc.LoadXml(doc.SelectSingleNode("SiteMap/SiteMap").OuterXml);

                siteMapDoc.Save(sfd.FileName);

                EnableControls(true);

                MessageBox.Show(this, "SiteMap saved!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ResetSiteMapToDefaultToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (DialogResult.Yes ==
                MessageBox.Show(this,
                                "Are your sure you want to reset the SiteMap?\r\n\r\nChanges will take effect only if you update the SiteMap.",
                                "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                Assembly myAssembly = Assembly.GetExecutingAssembly();
                using (
                    var reader =
                        new StreamReader(
                            myAssembly.GetManifestResourceStream("MsCrmTools.SiteMapEditor.Resources.sitemap.xml")))
                {
                    var doc = new XmlDocument();
                    doc.LoadXml(reader.ReadToEnd());

                    siteMapDoc = new XmlDocument();
                    siteMapDoc.LoadXml(doc.SelectSingleNode("ImportExportXml/SiteMap/SiteMap").OuterXml);
                }

                DisplaySiteMap();
            }
        }

        #endregion Main ToolStrip Menu

        #region TreeView ToolStrip Menu

        private void ToolStripButtonMoveDownClick(object sender, EventArgs e)
        {
            toolStripButtonMoveDown.Enabled = false;

            TreeNode tnmNode = tvSiteMap.SelectedNode;
            TreeNode tnmNextNode = tnmNode.NextNode;

            if (tnmNextNode != null)
            {
                int idxBegin = tnmNode.Index;
                int idxEnd = tnmNextNode.Index;
                TreeNode tnmNodeParent = tnmNode.Parent;
                if (tnmNodeParent != null)
                {
                    tnmNode.Remove();
                    tnmNextNode.Remove();

                    tnmNodeParent.Nodes.Insert(idxBegin, tnmNextNode);
                    tnmNodeParent.Nodes.Insert(idxEnd, tnmNode);

                    tvSiteMap.SelectedNode = tnmNode;
                }
            }

            toolStripButtonMoveDown.Enabled = true;
        }

        private void ToolStripButtonMoveUpClick(object sender, EventArgs e)
        {
            toolStripButtonMoveUp.Enabled = false;

            TreeNode tnmNode = tvSiteMap.SelectedNode;
            TreeNode tnmPreviousNode = tnmNode.PrevNode;

            if (tnmPreviousNode != null)
            {
                int idxBegin = tnmNode.Index;
                int idxEnd = tnmPreviousNode.Index;
                TreeNode tnmNodeParent = tnmNode.Parent;
                if (tnmNodeParent != null)
                {
                    tnmNode.Remove();
                    tnmPreviousNode.Remove();

                    tnmNodeParent.Nodes.Insert(idxEnd, tnmNode);
                    tnmNodeParent.Nodes.Insert(idxBegin, tnmPreviousNode);

                    tvSiteMap.SelectedNode = tnmNode;
                }
            }

            toolStripButtonMoveUp.Enabled = true;
        }

        private void ToolStripButtonDeleteClick(object sender, EventArgs e)
        {
            tvSiteMap.SelectedNode.Remove();
        }

        private void ToolStripButtonDisplayXmlClick(object sender, EventArgs e)
        {
            TreeNode selectedNode = tvSiteMap.SelectedNode;
            var collec = (Dictionary<string, string>) selectedNode.Tag;

            var doc = new XmlDocument();
            doc.AppendChild(doc.CreateElement(selectedNode.Text.Split(' ')[0]));

            foreach (string key in collec.Keys)
            {
                XmlAttribute attr = doc.CreateAttribute(key);
                attr.Value = collec[key];

                doc.DocumentElement.Attributes.Append(attr);
            }

            foreach (TreeNode node in selectedNode.Nodes)
            {
                AddXmlNode(node, doc.DocumentElement);
            }

            var xcdDialog = new XmlContentDisplayDialog(doc.OuterXml);
            xcdDialog.StartPosition = FormStartPosition.CenterParent;
            xcdDialog.ShowDialog();
        }

        private void ToolStripButtonAddXmlClick(object sender, EventArgs e)
        {
            try
            {
                TreeNode selectedNode = tvSiteMap.SelectedNode;

                var axForm = new AddXmlForm();
                axForm.StartPosition = FormStartPosition.CenterParent;

                if (axForm.ShowDialog() == DialogResult.OK)
                {
                    XmlNode resultNode = axForm.AddedXmlNode;

                    switch (resultNode.Name)
                    {
                        case "Area":
                            {
                                if (!selectedNode.Text.StartsWith("SiteMap"))
                                {
                                    throw new Exception(
                                        "Invalid Xml content for SiteMap node!\r\n\r\n'Area' Xml content is allowed only for 'SiteMap'.");
                                }
                            }
                            break;
                        case "Group":
                            {
                                if (!selectedNode.Text.StartsWith("Area"))
                                {
                                    throw new Exception(
                                        "Invalid Xml content for Area node!\r\n\r\n'Group' Xml content is allowed only for 'Area'.");
                                }
                            }
                            break;
                        case "SubArea":
                            {
                                if (!selectedNode.Text.StartsWith("Group"))
                                {
                                    throw new Exception(
                                        "Invalid Xml content for Group node!\r\n\r\n'SubArea' Xml content is allowed only for 'Group'.");
                                }
                            }
                            break;
                        case "Titles":
                            {
                                if (!selectedNode.Text.StartsWith("Group") && !selectedNode.Text.StartsWith("SubArea") &&
                                    !selectedNode.Text.StartsWith("Area"))
                                {
                                    throw new Exception("Invalid Xml content for " + selectedNode.Text.Split(' ')[0] +
                                                        " node!\r\n\r\n'Titles' Xml content is allowed only for 'Area', 'Group' and 'SubArea'.");
                                }
                            }
                            break;
                        case "Descriptions":
                            {
                                if (!selectedNode.Text.StartsWith("Group") && !selectedNode.Text.StartsWith("SubArea") &&
                                    !selectedNode.Text.StartsWith("Area"))
                                {
                                    throw new Exception("Invalid Xml content for " + selectedNode.Text.Split(' ')[0] +
                                                        " node!\r\n\r\n'Descriptions' Xml content is allowed only for 'Area', 'Group' and 'SubArea'.");
                                }
                            }
                            break;
                        case "Title":
                            {
                                if (!selectedNode.Text.StartsWith("Titles"))
                                {
                                    throw new Exception(
                                        "Invalid Xml content for Titles node!\r\n\r\n'Title' Xml content is allowed only for 'Titles'.");
                                }
                            }
                            break;
                        case "Description":
                            {
                                if (!selectedNode.Text.StartsWith("Descriptions"))
                                {
                                    throw new Exception(
                                        "Invalid Xml content for Descriptions node!\r\n\r\n'Description' Xml content is allowed only for 'Descriptions'.");
                                }
                            }
                            break;
                        case "Privilege":
                            {
                                if (!selectedNode.Text.StartsWith("SubArea"))
                                {
                                    throw new Exception(
                                        "Invalid Xml content for SubArea node!\r\n\r\n'Privilege' Xml content is allowed only for 'SubArea'.");
                                }
                            }
                            break;
                        default:
                            throw new Exception("Unsupported Xml content!");
                    }

                    TreeNodeHelper.AddTreeViewNode(selectedNode, resultNode, this);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion TreeView ToolStrip Menu

        #region TreeView Handlers

        private void TvSiteMapNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode selectedNode = e.Node;
            selectedNode.TreeView.SelectedNode = selectedNode;
            var collec = (Dictionary<string, string>) selectedNode.Tag;

            TreeNodeHelper.AddContextMenu(e.Node, this);
            Control existingControl = panelContainer.Controls.Count > 0 ? panelContainer.Controls[0] : null;

            switch (selectedNode.Text.Split(' ')[0])
            {
                case "SiteMap":
                    {
                        var ctrl = new SiteMapControl(collec);
                        ctrl.Saved += CtrlSaved;

                        panelContainer.Controls.Add(ctrl);
                        ctrl.BringToFront();
                        if (existingControl != null) panelContainer.Controls.Remove(existingControl);
                        tsbItemSave.Visible = true;
                    }
                    break;
                case "Area":
                    {
                        if (collec.Count == 0) collec.Add("Id", string.Format("tempId_{0}", DateTime.Now.Ticks));
                        var ctrl = new AreaControl(collec);
                        ctrl.Saved += CtrlSaved;

                        panelContainer.Controls.Add(ctrl);
                        ctrl.BringToFront();
                        if (existingControl != null) panelContainer.Controls.Remove(existingControl);
                        tsbItemSave.Visible = true;
                    }
                    break;
                case "SubArea":
                    {
                        if (collec.Count == 0) collec.Add("Id", string.Format("tempId_{0}", DateTime.Now.Ticks));
                        var ctrl = new SubAreaControl(collec);
                        ctrl.Saved += CtrlSaved;

                        panelContainer.Controls.Add(ctrl);
                        ctrl.BringToFront();
                        if (existingControl != null) panelContainer.Controls.Remove(existingControl);
                        tsbItemSave.Visible = true;
                    }
                    break;
                case "Group":
                    {
                        if (collec.Count == 0) collec.Add("Id", string.Format("tempId_{0}", DateTime.Now.Ticks));
                        var ctrl = new GroupControl(collec);
                        ctrl.Saved += CtrlSaved;

                        panelContainer.Controls.Add(ctrl);
                        ctrl.BringToFront();
                        if (existingControl != null) panelContainer.Controls.Remove(existingControl);
                        tsbItemSave.Visible = true;
                    }
                    break;
                case "Privilege":
                    {
                        var ctrl = new PrivilegeControl(collec);
                        ctrl.Saved += CtrlSaved;

                        panelContainer.Controls.Add(ctrl);
                        ctrl.BringToFront();
                        if (existingControl != null) panelContainer.Controls.Remove(existingControl);
                        tsbItemSave.Visible = true;
                    }
                    break;
                case "Description":
                    {
                        var ctrl = new DescriptionControl(collec);
                        ctrl.Saved += CtrlSaved;

                        panelContainer.Controls.Add(ctrl);
                        ctrl.BringToFront();
                        if (existingControl != null) panelContainer.Controls.Remove(existingControl);
                        tsbItemSave.Visible = true;
                    }
                    break;
                case "Title":
                    {
                        var ctrl = new TitleControl(collec);
                        ctrl.Saved += CtrlSaved;

                        panelContainer.Controls.Add(ctrl);
                        ctrl.BringToFront();
                        if (existingControl != null) panelContainer.Controls.Remove(existingControl);
                        tsbItemSave.Visible = true;
                    }
                    break;
                default:
                    {
                        panelContainer.Controls.Clear();
                        tsbItemSave.Visible = false;
                    }
                    break;
            }

            ManageMenuDisplay();
        }

        private void TvSiteMapAfterSelect(object sender, TreeViewEventArgs e)
        {
            var e2 = new TreeNodeMouseClickEventArgs(e.Node, MouseButtons.Left, 1, 0, 0);

            TvSiteMapNodeMouseClick(tvSiteMap, e2);
        }

        private void TvSiteMapKeyDown(object sender, KeyEventArgs e)
        {
            if (tvSiteMap.SelectedNode.Text != "SiteMap")
            {
                // Cut
                if (e.Control && e.KeyCode.ToString() == "X")
                    clipboard.Cut(tvSiteMap.SelectedNode);

                // Copy
                if (e.Control && e.KeyCode.ToString() == "C")
                    clipboard.Copy(tvSiteMap.SelectedNode);

                // Delete
                if (e.Control && e.KeyCode.ToString() == "D")
                    if (tvSiteMap.SelectedNode != null && tvSiteMap.SelectedNode.Text != "SiteMap")
                        ToolStripButtonDeleteClick(null, null);

                // Move Up
                if (e.Control && e.KeyCode == Keys.Up)
                    if (tvSiteMap.SelectedNode != null && tvSiteMap.SelectedNode.Parent != null &&
                        tvSiteMap.SelectedNode.Index != 0)
                        ToolStripButtonMoveUpClick(null, null);

                // Move Down
                if (e.Control && e.KeyCode == Keys.Down)
                    if (tvSiteMap.SelectedNode != null && tvSiteMap.SelectedNode.Parent != null &&
                        tvSiteMap.SelectedNode.Index != tvSiteMap.SelectedNode.Parent.Nodes.Count - 1)
                        ToolStripButtonMoveDownClick(null, null);
            }

            // Paste
            if (e.Control && e.KeyCode.ToString() == "V")
                clipboard.Paste(tvSiteMap.SelectedNode);
        }

        #endregion TreeView Handlers

        #region SiteMap Component Handlers

        private void TsbItemSaveClick(object sender, EventArgs e)
        {
            ((ISiteMapSavable) panelContainer.Controls[0]).Save();

            var nodeAttributesCollection = (Dictionary<string, string>) tvSiteMap.SelectedNode.Tag;

            if (nodeAttributesCollection.ContainsKey("Id"))
            {
                if (tvSiteMap.SelectedNode.Text.Split(' ').Length == 1)
                    tvSiteMap.SelectedNode.Text += " (" +
                                                   ((Dictionary<string, string>) tvSiteMap.SelectedNode.Tag)["Id"] + ")";
                else
                    tvSiteMap.SelectedNode.Text = tvSiteMap.SelectedNode.Text.Split(' ')[0] + " (" +
                                                  ((Dictionary<string, string>) tvSiteMap.SelectedNode.Tag)["Id"] + ")";

                tvSiteMap.SelectedNode.Name = tvSiteMap.SelectedNode.Text.Replace(" ", "");
            }

            if (nodeAttributesCollection.ContainsKey("LCID"))
            {
                tvSiteMap.SelectedNode.Text = tvSiteMap.SelectedNode.Text.Split(' ')[0] + " (" +
                                              ((Dictionary<string, string>) tvSiteMap.SelectedNode.Tag)["LCID"] + ")";

                tvSiteMap.SelectedNode.Name = tvSiteMap.SelectedNode.Text.Replace(" ", "");
            }
        }

        #endregion SiteMap Component Handlers

        #region ContextMenu Handlers

        private void NodeMenuItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name.Contains("System"))
            {
                string nodeText = e.ClickedItem.Name.Remove(0, 9);
                nodeText = nodeText.Substring(0, nodeText.IndexOf("ToolStripMenuItem"));

                var smcPicker = new SiteMapComponentPicker(nodeText);
                smcPicker.StartPosition = FormStartPosition.CenterParent;

                if (smcPicker.ShowDialog() == DialogResult.OK)
                {
                    var collec = new Dictionary<string, string>();

                    foreach (XmlAttribute attr in smcPicker.SelectedNode.Attributes)
                    {
                        collec.Add(attr.Name, attr.Value);
                    }

                    string newNodeText = smcPicker.SelectedNode.Name + " (" + collec["Id"] + ")";

                    if (tvSiteMap.SelectedNode.Nodes.Find(newNodeText.Replace(" ", ""), false).Length > 0)
                    {
                        MessageBox.Show(this, "The selected tree node is already present!", "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        var newNode = new TreeNode(newNodeText);
                        newNode.Tag = collec;
                        newNode.Name = newNodeText.Replace(" ", "");

                        foreach (XmlNode childNode in smcPicker.SelectedNode.ChildNodes)
                        {
                            TreeNodeHelper.AddTreeViewNode(newNode, childNode, this);
                        }

                        var e2 = new TreeNodeMouseClickEventArgs(newNode, MouseButtons.Left, 1, 0, 0);

                        tvSiteMap.SelectedNode.Nodes.Add(newNode);
                        TvSiteMapNodeMouseClick(tvSiteMap, e2);
                    }
                }
            }
            else if (e.ClickedItem.Text == "Delete")
            {
                tvSiteMap.SelectedNode.Remove();
            }
            else if (e.ClickedItem.Text == "Disable")
            {
                ((Dictionary<string, string>) tvSiteMap.SelectedNode.Tag).Add("_disabled", "true");
                tvSiteMap.SelectedNode.ForeColor = Color.Gray;
                tvSiteMap.SelectedNode.Text += " - disabled";
                tvSiteMap.SelectedNode.ToolTipText =
                    "This node is disabled and won't appear in Microsoft Dynamics CRM 2011. Right click this node and enable it and make it appear on Microsoft Dynamics CRM 2011";
            }
            else if (e.ClickedItem.Text == "Enable")
            {
                ((Dictionary<string, string>) tvSiteMap.SelectedNode.Tag).Remove("_disabled");
                tvSiteMap.SelectedNode.ForeColor = Color.Black;
                tvSiteMap.SelectedNode.Text = tvSiteMap.SelectedNode.Text.Replace(" - disabled", "");
                tvSiteMap.SelectedNode.ToolTipText = null;
            }
            else if (e.ClickedItem.Text == "Cut" || e.ClickedItem.Text == "Copy" || e.ClickedItem.Text == "Paste")
            {
                if (e.ClickedItem.Text == "Cut")
                    clipboard.Cut(tvSiteMap.SelectedNode);
                else if (e.ClickedItem.Text == "Copy")
                    clipboard.Copy(tvSiteMap.SelectedNode);
                else
                    clipboard.Paste(tvSiteMap.SelectedNode);
            }
            else
            {
                string nodeText = e.ClickedItem.Name.Remove(0, 3);
                nodeText = nodeText.Substring(0, nodeText.IndexOf("ToolStripMenuItem"));

                var newNode = new TreeNode(nodeText);
                newNode.Tag = new Dictionary<string, string>();
                newNode.Name = newNode.Text.Replace(" ", "");

                if (newNode.Text == "Descriptions" || newNode.Text == "Titles")
                {
                    var newSubNode = new TreeNode(newNode.Text.Remove(newNode.Text.Length - 1, 1));
                    newSubNode.Name = newSubNode.Text.Replace(" ", "");
                    newNode.Nodes.Add(newSubNode);

                    var e2 = new TreeNodeMouseClickEventArgs(newSubNode, MouseButtons.Left, 1, 0, 0);
                    tvSiteMap.SelectedNode.Nodes.Add(newNode);
                    TvSiteMapNodeMouseClick(tvSiteMap, e2);
                }
                else
                {
                    var e2 = new TreeNodeMouseClickEventArgs(newNode, MouseButtons.Left, 1, 0, 0);
                    tvSiteMap.SelectedNode.Nodes.Add(newNode);
                    TvSiteMapNodeMouseClick(tvSiteMap, e2);
                }
            }
        }

        #endregion ContextMenu Handlers

        #region Load SiteMap Methods

        private void LoadSiteMap()
        {
            CommonDelegates.SetCursor(this, Cursors.WaitCursor);
            EnableControls(false);

            infoPanel = InformationPanel.GetInformationPanel(this, "Loading SiteMap...", 340, 100);

            var loadSiteMapWorker = new BackgroundWorker();
            loadSiteMapWorker.RunWorkerCompleted += LoadSiteMapWorkerRunWorkerCompleted;
            loadSiteMapWorker.DoWork += LoadSiteMapWorkerDoWork;
            loadSiteMapWorker.RunWorkerAsync();
        }

        private void LoadSiteMapWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var qe = new QueryExpression("sitemap");
            qe.ColumnSet = new ColumnSet(true);

            EntityCollection ec = service.RetrieveMultiple(qe);

            siteMap = ec[0];
            siteMapDoc = new XmlDocument();
            siteMapDoc.LoadXml(ec[0]["sitemapxml"].ToString());

            DisplaySiteMap();

            CommonDelegates.SetCursor(this, Cursors.Default);
            EnableControls(true);
        }

        private void LoadSiteMapWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CommonDelegates.SetCursor(this, Cursors.Default);
            EnableControls(true);
            infoPanel.Dispose();
            Controls.Remove(infoPanel);
        }

        #endregion Load SiteMap Methods

        #region Update SiteMap Methods

        private void UpdateSiteMap()
        {
            if (siteMap == null)
            {
                var qe = new QueryExpression("sitemap");
                qe.ColumnSet = new ColumnSet(true);

                EntityCollection ec = service.RetrieveMultiple(qe);

                siteMap = ec[0];
                siteMapDoc = new XmlDocument();
                siteMapDoc.LoadXml(ec[0]["sitemapxml"].ToString());
            }

            CommonDelegates.SetCursor(this, Cursors.WaitCursor);
            EnableControls(false);

            infoPanel = InformationPanel.GetInformationPanel(this, "Updating Sitemap...", 340, 100);

            var updateWorker = new BackgroundWorker();
            updateWorker.RunWorkerCompleted += UpdateWorkerRunWorkerCompleted;
            updateWorker.DoWork += UpdateWorkerDoWork;
            updateWorker.RunWorkerAsync();
        }

        private void UpdateWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            // Build the Xml SiteMap from SiteMap TreeView
            var doc = new XmlDocument();
            XmlNode rootNode = doc.CreateElement("SiteMap");
            doc.AppendChild(rootNode);

            AddXmlNode(tvSiteMap.Nodes[0], rootNode);

            siteMap["sitemapxml"] = doc.SelectSingleNode("SiteMap/SiteMap").OuterXml;
            siteMapDoc.LoadXml(doc.SelectSingleNode("SiteMap/SiteMap").OuterXml);

            service.Update(siteMap);

            var request = new PublishXmlRequest();
            request.ParameterXml = "<importexportxml><sitemaps><sitemap></sitemap></sitemaps></importexportxml>";
            service.Execute(request);
        }

        private void UpdateWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                CommonDelegates.DisplayMessageBox(ParentForm, "Error while updating SiteMap: " + e.Error.Message,
                                                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            infoPanel.Dispose();
            Controls.Remove(infoPanel);

            CommonDelegates.SetCursor(this, Cursors.Default);
            EnableControls(true);
        }

        #endregion Update SiteMap Methods

        #region Others

        /// <summary>
        ///     Loads the SiteMap from the extracted Xml solution files
        /// </summary>
        private void DisplaySiteMap()
        {
            XmlNode siteMapXmlNode = null;

            MethodInvoker miReadSiteMap = delegate { siteMapXmlNode = siteMapDoc.DocumentElement; };

            if (InvokeRequired)
                Invoke(miReadSiteMap);
            else
                miReadSiteMap();

            MethodInvoker miFillTreeView = delegate
                                               {
                                                   tvSiteMap.Nodes.Clear();

                                                   TreeNodeHelper.AddTreeViewNode(tvSiteMap, siteMapXmlNode, this);

                                                   ManageMenuDisplay();
                                                   tvSiteMap.Nodes[0].Expand();
                                               };

            if (tvSiteMap.InvokeRequired)
            {
                tvSiteMap.Invoke(miFillTreeView);
            }
            else
            {
                miFillTreeView();
            }
        }

        /// <summary>
        ///     When SiteMap component properties are saved, they are
        ///     copied in the current selected TreeNode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CtrlSaved(object sender, SaveEventArgs e)
        {
            tvSiteMap.SelectedNode.Tag = e.AttributeCollection;
        }

        /// <summary>
        ///     Manages which controls should be visible/enabled
        /// </summary>
        private void ManageMenuDisplay()
        {
            TreeNode selectedNode = tvSiteMap.SelectedNode;

            tsbItemSave.Enabled = selectedNode != null;
            toolStripButtonDelete.Enabled = selectedNode != null && selectedNode.Text != "SiteMap";
            toolStripButtonMoveUp.Enabled = selectedNode != null && selectedNode.Parent != null &&
                                            selectedNode.Index != 0;
            toolStripButtonMoveDown.Enabled = selectedNode != null && selectedNode.Parent != null &&
                                              selectedNode.Index != selectedNode.Parent.Nodes.Count - 1;
            toolStripButtonAddXml.Enabled = selectedNode != null && selectedNode.Text != "Title" &&
                                            selectedNode.Text != "Description" && selectedNode.Text != "Privilege";
            toolStripButtonDisplayXml.Enabled = selectedNode != null;

            toolStripDropDownButtonMoreActions.Enabled = tvSiteMap.Nodes.Count > 0;
            tsbUpdateSiteMap.Enabled = tvSiteMap.Nodes.Count > 0;
            toolStripButtonSaveSiteMapToDisk.Enabled = tvSiteMap.Nodes.Count > 0;
        }

        /// <summary>
        ///     Add the specified TreeNode properties in a XmlNode
        /// </summary>
        /// <param name="currentNode">TreeNode to add to the Xml</param>
        /// <param name="parentXmlNode">XmlNode where to add data</param>
        private void AddXmlNode(TreeNode currentNode, XmlNode parentXmlNode)
        {
            XmlNode newNode = parentXmlNode.OwnerDocument.CreateElement(currentNode.Text.Split(' ')[0]);

            var collec = (Dictionary<string, string>) currentNode.Tag;

            foreach (string key in collec.Keys)
            {
                if (key != "_disabled")
                {
                    XmlAttribute attr = parentXmlNode.OwnerDocument.CreateAttribute(key);
                    attr.Value = collec[key];

                    newNode.Attributes.Append(attr);
                }
            }

            TreeNode titles = null;
            TreeNode descriptions = null;
            var others = new List<TreeNode>();

            foreach (TreeNode childNode in currentNode.Nodes)
            {
                if (childNode.Text == "Titles")
                    titles = childNode;
                else if (childNode.Text == "Descriptions")
                    descriptions = childNode;
                else
                    others.Add(childNode);
            }

            if (titles != null)
                AddXmlNode(titles, newNode);
            if (descriptions != null)
                AddXmlNode(descriptions, newNode);
            foreach (TreeNode otherNode in others)
                AddXmlNode(otherNode, newNode);

            if (collec.ContainsKey("_disabled"))
            {
                XmlComment comment = parentXmlNode.OwnerDocument.CreateComment(newNode.OuterXml);
                parentXmlNode.AppendChild(comment);
            }
            else
            {
                parentXmlNode.AppendChild(newNode);
            }
        }

        /// <summary>
        ///     Enables or disables specific controls
        /// </summary>
        /// <param name="enabled">Flag that indicates if controls must be enabled</param>
        internal void EnableControls(bool enabled)
        {
            MethodInvoker mi = delegate
                                   {
                                       tsbMainOpenSiteMap.Enabled = enabled;
                                       tsbUpdateSiteMap.Enabled = enabled;
                                       toolStripButtonSaveSiteMapToDisk.Enabled = enabled;
                                       toolStripButtonLoadSiteMapFromDisk.Enabled = enabled;
                                       toolStripDropDownButtonMoreActions.Enabled = enabled;
                                       gbSiteMap.Enabled = enabled;
                                       gbProperties.Enabled = enabled;
                                   };

            if (InvokeRequired)
            {
                Invoke(mi);
            }
            else
            {
                mi();
            }
        }

        #endregion Others

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

            if (actionName == "LoadSiteMap")
            {
                LoadSiteMap();
            }
            if (actionName == "UpdateSiteMap")
            {
                UpdateSiteMap();
            }
        }

        private void TsbCloseThisTabClick(object sender, EventArgs e)
        {
            const string message = "Are your sure you want to close this tab?";
            if (MessageBox.Show(message, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.Yes)
                OnCloseTool(this, null);
        }
    }
}