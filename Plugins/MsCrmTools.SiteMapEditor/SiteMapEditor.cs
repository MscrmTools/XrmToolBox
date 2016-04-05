// PROJECT : MsCrmTools.SiteMapEditor
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using MsCrmTools.SiteMapEditor.AppCode;
using MsCrmTools.SiteMapEditor.Controls;
using MsCrmTools.SiteMapEditor.Forms;
using SiteMapEditor.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using XrmToolBox.Extensibility;
using Clipboard = MsCrmTools.SiteMapEditor.AppCode.Clipboard;

namespace MsCrmTools.SiteMapEditor
{
    public partial class SiteMapEditor : PluginControlBase
    {
        internal Clipboard clipboard = new Clipboard();
        internal List<EntityMetadata> entityCache;
        internal List<Entity> webResourcesHtmlCache;
        internal List<Entity> webResourcesImageCache;
        private Entity siteMap;
        private XmlDocument siteMapDoc;

        public SiteMapEditor()
        {
            InitializeComponent();
        }

        #region Main ToolStrip Menu

        private void resetCRM2013SiteMapToDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ConnectionDetail.OrganizationMajorVersion != 6)
            {
                if (DialogResult.No == MessageBox.Show(this,
                    "Your current organization is not a CRM 2013 organization! Are you sure you want to continue?",
                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    return;
            }

            ResetSiteMap(2013);
        }

        private void resetCRM2015SiteMapToDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ConnectionDetail.OrganizationMajorVersion != 7 ||
                ConnectionDetail.OrganizationMajorVersion == 7 && ConnectionDetail.OrganizationMinorVersion != 0)
            {
                if (DialogResult.No == MessageBox.Show(this,
                    "Your current organization is not a CRM 2015 organization! Are you sure you want to continue?",
                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    return;
            }

            ResetSiteMap(2015);
        }

        private void resetCRM2015Update1SiteMapToDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ConnectionDetail.OrganizationMajorVersion != 7 ||
                ConnectionDetail.OrganizationMajorVersion == 7 && ConnectionDetail.OrganizationMinorVersion != 1)
            {
                if (DialogResult.No == MessageBox.Show(this,
                    "Your current organization is not a CRM 2015 Update 1 organization! Are you sure you want to continue?",
                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    return;
            }

            ResetSiteMap("2015SP1");
        }

        private void resetCRM2016SiteMapToDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ConnectionDetail.OrganizationMajorVersion != 8)
            {
                if (DialogResult.No == MessageBox.Show(this,
                    "Your current organization is not a CRM 2016 organization! Are you sure you want to continue?",
                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    return;
            }

            ResetSiteMap("2016");
        }

        private void ResetSiteMap(object version)
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
                            myAssembly.GetManifestResourceStream(
                                string.Format("MsCrmTools.SiteMapEditor.Resources.sitemap{0}.xml", version))))
                {
                    var doc = new XmlDocument();
                    doc.LoadXml(reader.ReadToEnd());

                    siteMapDoc = new XmlDocument();
                    siteMapDoc.LoadXml(doc.DocumentElement.OuterXml);
                }

                DisplaySiteMap();
            }
        }

        private void ResetSiteMapToDefaultToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (ConnectionDetail.OrganizationMajorVersion != 5)
            {
                if (DialogResult.No == MessageBox.Show(this,
                    "Your current organization is not a CRM 2011 organization! Are you sure you want to continue?",
                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    return;
            }

            ResetSiteMap(2011);
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
                    if (Service != null && entityCache == null)
                    {
                        WorkAsync(new WorkAsyncInfo
                        {
                            Message = "Loading Entities...",
                            Work = (bw, evt) =>
                            { // Recherche des métadonnées
                                entityCache = new List<EntityMetadata>();
                                webResourcesHtmlCache = new List<Entity>();

                                var request = new RetrieveAllEntitiesRequest
                                {
                                    EntityFilters = EntityFilters.Entity
                                };

                                var response = (RetrieveAllEntitiesResponse)Service.Execute(request);

                                foreach (var emd in response.EntityMetadata)
                                {
                                    entityCache.Add(emd);
                                }
                                // Fin Recherche des métadonnées

                                bw.ReportProgress(0, "Loading web resources...");
                                // Rercherche des images

                                webResourcesImageCache = new List<Entity>();

                                var wrQuery = new QueryExpression("webresource");
                                wrQuery.Criteria.AddCondition("webresourcetype", ConditionOperator.In,
                                    new object[] { 1, 5, 6, 7 });
                                wrQuery.ColumnSet.AllColumns = true;

                                EntityCollection results = Service.RetrieveMultiple(wrQuery);

                                foreach (Entity webresource in results.Entities)
                                {
                                    if (webresource.GetAttributeValue<OptionSetValue>("webresourcetype").Value == 1)
                                    {
                                        webResourcesHtmlCache.Add(webresource);
                                    }
                                    else
                                    {
                                        webResourcesImageCache.Add(webresource);
                                    }
                                }
                            },
                            PostWorkCallBack = evt =>
                            {
                                DisplaySiteMap();
                                EnableControls(true);
                            },
                            ProgressChanged = evt => { SetWorkingMessage(evt.UserState.ToString()); }
                        });
                    }
                    else
                    {
                        DisplaySiteMap();
                        EnableControls(true);
                    }
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

        private void TsbMainImportClick(object sender, EventArgs e)
        {
            ExecuteMethod(UpdateSiteMap);
        }

        private void TsbMainOpenSiteMapClick(object sender, EventArgs e)
        {
            ExecuteMethod(LoadSiteMap);
        }

        #endregion Main ToolStrip Menu

        #region TreeView ToolStrip Menu

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

        private void ToolStripButtonDeleteClick(object sender, EventArgs e)
        {
            tvSiteMap.SelectedNode.Remove();
        }

        private void ToolStripButtonDisplayXmlClick(object sender, EventArgs e)
        {
            TreeNode selectedNode = tvSiteMap.SelectedNode;
            var collec = (Dictionary<string, string>)selectedNode.Tag;

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

        private void ToolStripButtonMoveDownClick(object sender, EventArgs e)
        {
            toolStripButtonMoveDown.Click -= ToolStripButtonMoveDownClick;
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
                tnmNodeParent = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }

            toolStripButtonMoveDown.Enabled = true;
            toolStripButtonMoveDown.Click += ToolStripButtonMoveDownClick;
        }

        private void ToolStripButtonMoveUpClick(object sender, EventArgs e)
        {
            toolStripButtonMoveUp.Click -= ToolStripButtonMoveUpClick;
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

                tnmNodeParent = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }

            toolStripButtonMoveUp.Enabled = true;
            toolStripButtonMoveUp.Click += ToolStripButtonMoveUpClick;
        }

        #endregion TreeView ToolStrip Menu

        #region TreeView Handlers

        private void TvSiteMapAfterSelect(object sender, TreeViewEventArgs e)
        {
            var e2 = new TreeNodeMouseClickEventArgs(e.Node, MouseButtons.Left, 1, 0, 0);

            TvSiteMapNodeMouseClick(tvSiteMap, e2);
        }

        private void TvSiteMapKeyDown(object sender, KeyEventArgs e)
        {
            if (tvSiteMap.SelectedNode == null)
                return;

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

        private void TvSiteMapNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode selectedNode = e.Node;
            selectedNode.TreeView.SelectedNode = selectedNode;
            var collec = (Dictionary<string, string>)selectedNode.Tag;

            TreeNodeHelper.AddContextMenu(e.Node, this);
            Control existingControl = panelContainer.Controls.Count > 0 ? panelContainer.Controls[0] : null;

            if (existingControl != null)
            {
                panelContainer.Controls.Remove(existingControl);
                existingControl.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }

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
                        var ctrl = new AreaControl(collec, webResourcesImageCache, webResourcesHtmlCache, Service);
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
                        var ctrl = new SubAreaControl(collec, entityCache, webResourcesImageCache, webResourcesHtmlCache,
                            Service);
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

        #endregion TreeView Handlers

        #region SiteMap Component Handlers

        private void TsbItemSaveClick(object sender, EventArgs e)
        {
            ((ISiteMapSavable)panelContainer.Controls[0]).Save();

            var nodeAttributesCollection = (Dictionary<string, string>)tvSiteMap.SelectedNode.Tag;

            if (nodeAttributesCollection.ContainsKey("Id"))
            {
                if (tvSiteMap.SelectedNode.Text.Split(' ').Length == 1)
                    tvSiteMap.SelectedNode.Text += " (" +
                                                   ((Dictionary<string, string>)tvSiteMap.SelectedNode.Tag)["Id"] + ")";
                else
                    tvSiteMap.SelectedNode.Text = tvSiteMap.SelectedNode.Text.Split(' ')[0] + " (" +
                                                  ((Dictionary<string, string>)tvSiteMap.SelectedNode.Tag)["Id"] + ")";

                tvSiteMap.SelectedNode.Name = tvSiteMap.SelectedNode.Text.Replace(" ", "");
            }

            if (nodeAttributesCollection.ContainsKey("LCID"))
            {
                tvSiteMap.SelectedNode.Text = tvSiteMap.SelectedNode.Text.Split(' ')[0] + " (" +
                                              ((Dictionary<string, string>)tvSiteMap.SelectedNode.Tag)["LCID"] + ")";

                tvSiteMap.SelectedNode.Name = tvSiteMap.SelectedNode.Text.Replace(" ", "");
            }

            if (nodeAttributesCollection.ContainsKey("_disabled") && nodeAttributesCollection["_disabled"] == "true")
            {
                tvSiteMap.SelectedNode.Text += " - disabled";
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

                string version = "2011";
                switch (ConnectionDetail.OrganizationMajorVersion)
                {
                    case 5:
                        version = "2011";
                        break;

                    case 6:
                        version = "2013";
                        break;

                    case 7:
                        if (ConnectionDetail.OrganizationMinorVersion == 0)
                            version = "2015";
                        else
                            version = "2015SP1";
                        break;
                }

                var smcPicker = new SiteMapComponentPicker(nodeText, version);
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
                ((Dictionary<string, string>)tvSiteMap.SelectedNode.Tag).Add("_disabled", "true");
                tvSiteMap.SelectedNode.ForeColor = Color.Gray;
                tvSiteMap.SelectedNode.Text += " - disabled";
                tvSiteMap.SelectedNode.ToolTipText =
                    "This node is disabled and won't appear in Microsoft Dynamics CRM 2011. Right click this node and enable it and make it appear on Microsoft Dynamics CRM 2011";
            }
            else if (e.ClickedItem.Text == "Enable")
            {
                ((Dictionary<string, string>)tvSiteMap.SelectedNode.Tag).Remove("_disabled");
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

        public void LoadCrmItems()
        {
            EnableControls(false);

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading Entities...",
                Work = (bw, e) =>
                {
                    // Recherche des métadonnées
                    entityCache = new List<EntityMetadata>();
                    webResourcesHtmlCache = new List<Entity>();

                    var request = new RetrieveAllEntitiesRequest
                    {
                        EntityFilters = EntityFilters.Entity
                    };

                    var response = (RetrieveAllEntitiesResponse)Service.Execute(request);

                    foreach (var emd in response.EntityMetadata)
                    {
                        entityCache.Add(emd);
                    }
                    // Fin Recherche des métadonnées

                    bw.ReportProgress(0, "Loading web resources...");
                    // Rercherche des images

                    webResourcesImageCache = new List<Entity>();

                    var wrQuery = new QueryExpression("webresource");
                    wrQuery.Criteria.AddCondition("webresourcetype", ConditionOperator.In, new object[] { 1, 5, 6, 7 });
                    wrQuery.ColumnSet.AllColumns = true;

                    EntityCollection results = Service.RetrieveMultiple(wrQuery);

                    foreach (Entity webresource in results.Entities)
                    {
                        if (webresource.GetAttributeValue<OptionSetValue>("webresourcetype").Value == 1)
                        {
                            webResourcesHtmlCache.Add(webresource);
                        }
                        else
                        {
                            webResourcesImageCache.Add(webresource);
                        }
                    }
                },
                PostWorkCallBack = e =>
                {
                    DisplaySiteMap();
                    EnableControls(true);
                },
                ProgressChanged = e => { SetWorkingMessage(e.UserState.ToString()); }
            });
        }

        public void LoadSiteMap()
        {
            EnableControls(false);

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading SiteMap...",
                Work = (bw, e) =>
                {
                    var qe = new QueryExpression("sitemap");
                    qe.ColumnSet = new ColumnSet(true);

                    EntityCollection ec = Service.RetrieveMultiple(qe);

                    siteMap = ec[0];
                    siteMapDoc = new XmlDocument();
                    siteMapDoc.LoadXml(ec[0]["sitemapxml"].ToString());
                },
                PostWorkCallBack = e =>
                {
                    DisplaySiteMap();
                    EnableControls(true);
                    LoadCrmItems();
                },
                ProgressChanged = e => { SetWorkingMessage(e.UserState.ToString()); }
            });
        }

        #endregion Load SiteMap Methods

        #region Update SiteMap Methods

        private void UpdateSiteMap()
        {
            if (siteMap == null)
            {
                var qe = new QueryExpression("sitemap");
                qe.ColumnSet = new ColumnSet(true);

                EntityCollection ec = Service.RetrieveMultiple(qe);

                siteMap = ec[0];
                siteMapDoc = new XmlDocument();
                siteMapDoc.LoadXml(ec[0]["sitemapxml"].ToString());
            }

            EnableControls(false);

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Updating Sitemap...",
                Work = (bw, e) =>
                {
                    // Build the Xml SiteMap from SiteMap TreeView
                    var doc = new XmlDocument();
                    XmlNode rootNode = doc.CreateElement("SiteMap");
                    doc.AppendChild(rootNode);

                    AddXmlNode(tvSiteMap.Nodes[0], rootNode);

                    siteMap["sitemapxml"] = doc.SelectSingleNode("SiteMap/SiteMap").OuterXml;
                    siteMapDoc.LoadXml(doc.SelectSingleNode("SiteMap/SiteMap").OuterXml);

                    Service.Update(siteMap);

                    var request = new PublishXmlRequest();
                    request.ParameterXml = "<importexportxml><sitemaps><sitemap></sitemap></sitemaps></importexportxml>";
                    Service.Execute(request);
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        if (e.Error.Message.Contains("DefaultDashboard"))
                        {
                            MessageBox.Show(ParentForm,
                                "Error while updating SiteMap: Defining 'DefaultDashboard' attribute on 'SubArea' element is only available in CRM 2013 and Microsoft Dynamics CRM Online Fall '13 Service Update",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show(ParentForm, "Error while updating SiteMap: " + e.Error.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    EnableControls(true);
                }
            });
        }

        #endregion Update SiteMap Methods

        #region Others

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

        /// <summary>
        ///     Add the specified TreeNode properties in a XmlNode
        /// </summary>
        /// <param name="currentNode">TreeNode to add to the Xml</param>
        /// <param name="parentXmlNode">XmlNode where to add data</param>
        /// <param name="hasDisabledParent">Indicates if a parent node is already disabled</param>
        private void AddXmlNode(TreeNode currentNode, XmlNode parentXmlNode, bool hasDisabledParent = false)
        {
            string newNodeName;
            if (currentNode.Text.StartsWith("#"))
            {
                newNodeName = currentNode.Text.Remove(0, 2).Trim();
                XmlComment comment = parentXmlNode.OwnerDocument.CreateComment(newNodeName);
                parentXmlNode.AppendChild(comment);
                return;
            }

            newNodeName = currentNode.Text.Split(' ')[0];

            XmlNode newNode = parentXmlNode.OwnerDocument.CreateElement(newNodeName);

            var collec = (Dictionary<string, string>)currentNode.Tag;

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
                AddXmlNode(titles, newNode, hasDisabledParent || collec.ContainsKey("_disabled"));
            if (descriptions != null)
                AddXmlNode(descriptions, newNode, hasDisabledParent || collec.ContainsKey("_disabled"));
            foreach (TreeNode otherNode in others)
                AddXmlNode(otherNode, newNode, hasDisabledParent || collec.ContainsKey("_disabled"));

            if (collec.ContainsKey("_disabled") && !hasDisabledParent)
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
        ///     Loads the SiteMap from the extracted Xml solution files
        /// </summary>
        private void DisplaySiteMap()
        {
            XmlNode siteMapXmlNode = siteMapDoc.DocumentElement;
            tvSiteMap.Nodes.Clear();

            TreeNodeHelper.AddTreeViewNode(tvSiteMap, siteMapXmlNode, this);

            ManageMenuDisplay();
            tvSiteMap.Nodes[0].Expand();
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

        #endregion Others

        private void loadEntitiesAndWebResourcesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadCrmItems);
        }

        private void TsbCloseThisTabClick(object sender, EventArgs e)
        {
            CloseTool();
        }
    }
}