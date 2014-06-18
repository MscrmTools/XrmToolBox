// PROJECT : MsCrmTools.FetchXmlBuilder
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.FetchXmlBuilder.UserControls;
using XrmToolBox;

namespace MsCrmTools.FetchXmlBuilder
{
    public partial class MainControl : UserControl, IMsCrmToolsPluginUserControl
    {
        #region Variables

        /// <summary>
        /// Microsoft Dynamics CRM 2011 Organization Service
        /// </summary>
        private IOrganizationService service;

        /// <summary>
        /// Panel used to display progress information
        /// </summary>
        private Panel infoPanel;

        private XmlDocument fetchDoc;

        public Dictionary<string, EntityMetadata> EntityCache;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class <see cref="MainControl"/>
        /// </summary>
        public MainControl()
        {
            InitializeComponent();

            EntityCache = new Dictionary<string, EntityMetadata>();
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets the organization service used by the tool
        /// </summary>
        public IOrganizationService Service
        {
            get { return service; }
        }

        /// <summary>
        /// Gets the logo to display in the tools list
        /// </summary>
        public Image PluginLogo
        {
            get { return toolImageList.Images[0]; }
        }

        #endregion

        #region EventHandlers

        /// <summary>
        /// EventHandler to request a connection to an organization
        /// </summary>
        public event EventHandler OnRequestConnection;

        /// <summary>
        /// EventHandler to close the current tool
        /// </summary>
        public event EventHandler OnCloseTool;

        #endregion EventHandlers

        #region Methods

        /// <summary>
        /// Updates the organization service used by the tool
        /// </summary>
        /// <param name="newService">Organization service</param>
        /// <param name="actionName">Action that requested a service update</param>
        /// <param name="parameter">Parameter passed when requesting a service update</param>
        public void UpdateConnection(IOrganizationService newService, string actionName = "", object parameter = null)
        {
            service = newService;

            if (actionName == "LoadMetadata")
            {
                LoadMetadata();
            }
        }

        private void TsbLoadMetadataClick(object sender, EventArgs e)
        {
            if (service == null)
            {
                if (OnRequestConnection != null)
                {
                    var args = new RequestConnectionEventArgs { ActionName = "LoadMetadata", Control = this, Parameter = null };
                    OnRequestConnection(this, args);
                }
            }
            else
            {
                LoadMetadata();
            }
        }

        private void LoadMetadata()
        {
            infoPanel = InformationPanel.GetInformationPanel(this, "Loading metadata...", 340, 100);

            var worker = new BackgroundWorker();
            worker.DoWork += WorkerDoWork;
            worker.RunWorkerCompleted += WorkerRunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var request = new RetrieveAllEntitiesRequest
                              {
                                  EntityFilters = EntityFilters.Attributes | EntityFilters.Relationships
                              };

            var response = (RetrieveAllEntitiesResponse) service.Execute(request);

            foreach (var emd in response.EntityMetadata)
            {
                EntityCache.Add(emd.LogicalName, emd);
            }
        }

        private void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            infoPanel.Dispose();
            Controls.Remove(infoPanel);

        }

        private void TsbCloseClick(object sender, EventArgs e)
        {
            if (OnCloseTool != null)
            {
                OnCloseTool(this, null);
            }
        }

        #endregion Methods

        private void MainControlLoad(object sender, EventArgs e)
        {
            fetchDoc = new XmlDocument();
            fetchDoc.LoadXml("<fetch mapping=\"logical\"><entity name=\"account\"/></fetch>");

            BuildTreeView();
        }

        private void BuildTreeView()
        {
            BuildTreeNode(fetchDoc.DocumentElement, treeView1);
        }

        private void BuildTreeNode(XmlNode node, TreeView treeView)
        {
            var tNode = new TreeNode(node.Name) { Tag = node };
            treeView.Nodes.Add(tNode);

            foreach (XmlNode childNode in node.ChildNodes)
            {
                BuildTreeNode(childNode, tNode);
            }
        }

        private void BuildTreeNode(XmlNode node, TreeNode parentNode)
        {
            var tNode = new TreeNode(node.Name) { Tag = node, ContextMenuStrip = cmsTree };
            parentNode.Nodes.Add(tNode);

            foreach (XmlNode childNode in node.ChildNodes)
            {
                BuildTreeNode(childNode, tNode);
            }
        }

        private void TreeView1NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            pnlProperties.Controls.Clear();
            treeView1.SelectedNode = e.Node;

            switch (e.Node.Text)
            {
                case "fetch":
                    {
                        var ctrl = new FetchControl((XmlNode) e.Node.Tag);
                        pnlProperties.Controls.Add(ctrl);
                    }
                    break;
                case "entity":
                    {
                        var ctrl = new EntityControl((XmlNode)e.Node.Tag, EntityCache);
                        pnlProperties.Controls.Add(ctrl);
                    }
                    break;
                case "filter":
                    {
                        var ctrl = new FilterControl((XmlNode)e.Node.Tag);
                        pnlProperties.Controls.Add(ctrl);
                    }
                    break;
                case "condition":
                    {
                        var ctrl = new ConditionControl((XmlNode)e.Node.Tag);
                        pnlProperties.Controls.Add(ctrl);
                    }
                    break;
                case "order":
                    {
                        var ctrl = new OrderControl((XmlNode)e.Node.Tag);
                        pnlProperties.Controls.Add(ctrl);
                    }
                    break;
                case "link-entity":
                    {
                        var ctrl = new LinkEntityControl((XmlNode)e.Node.Tag);
                        pnlProperties.Controls.Add(ctrl);
                    }
                    break;
            }

            if (e.Button != MouseButtons.Right)
                return;

            switch (e.Node.Text)
            {
                case "fetch":
                    {
                        cmsTree.Visible = false;
                    }
                    break;
                case "entity":
                    {
                        cmsTree.Visible = true;
                        tsmiAddCondition.Visible = false;
                        tsmiAddFilter.Visible = true;
                        tsmiAddLinkEntity.Visible = true;
                        tsmiAddOrder.Visible = true;
                        toolStripSeparator1.Visible = false;
                        tsmiDelete.Visible = false;
                    }
                    break;
                case "filter":
                    {
                        cmsTree.Visible = true;
                        tsmiAddCondition.Visible = true;
                        tsmiAddFilter.Visible = true;
                        tsmiAddLinkEntity.Visible = false;
                        tsmiAddOrder.Visible = false;
                        toolStripSeparator1.Visible = true;
                        tsmiDelete.Visible = true;
                    }
                    break;
                case "condition":
                    {
                        cmsTree.Visible = true;
                        tsmiAddCondition.Visible = false;
                        tsmiAddFilter.Visible = false;
                        tsmiAddLinkEntity.Visible = false;
                        tsmiAddOrder.Visible = false;
                        toolStripSeparator1.Visible = false;
                        tsmiDelete.Visible = true;
                    }
                    break;
                case "order":
                    {
                        cmsTree.Visible = true;
                        tsmiAddCondition.Visible = false;
                        tsmiAddFilter.Visible = false;
                        tsmiAddLinkEntity.Visible = false;
                        tsmiAddOrder.Visible = false;
                        toolStripSeparator1.Visible = false;
                        tsmiDelete.Visible = true;
                    }
                    break;
                case "link-entity":
                    {
                        cmsTree.Visible = true;
                        tsmiAddCondition.Visible = false;
                        tsmiAddFilter.Visible = true;
                        tsmiAddLinkEntity.Visible = true;
                        tsmiAddOrder.Visible = false;
                        toolStripSeparator1.Visible = true;
                        tsmiDelete.Visible = true;
                    }
                    break;
            }
        }

        private void Button1Click(object sender, EventArgs e)
        {
            if (pnlProperties.Controls.Count == 1)
            {
                var ctrl = (IFetchXmlControl) pnlProperties.Controls[0];

                treeView1.SelectedNode.Tag = ctrl.GetNode();
            }

            textBox1.Text = ((XmlNode) treeView1.TopNode.Tag).OuterXml;
        }

        private void TsmiAddFilterClick(object sender, EventArgs e)
        {
            var currentNode = treeView1.SelectedNode;

            XmlNode filterNode = fetchDoc.CreateElement("filter");

            ((XmlNode) currentNode.Parent.Tag).AppendChild(filterNode);

            BuildTreeNode(filterNode, currentNode);
        }

        private void TsmiAddConditionClick(object sender, EventArgs e)
        {
            var currentNode = treeView1.SelectedNode;

            XmlNode conditionNode = fetchDoc.CreateElement("condition");

            ((XmlNode)currentNode.Parent.Tag).AppendChild(conditionNode);

            BuildTreeNode(conditionNode, currentNode);
        }

        private void TsmiAddOrderClick(object sender, EventArgs e)
        {
            var currentNode = treeView1.SelectedNode;

            XmlNode orderNode = fetchDoc.CreateElement("order");

            ((XmlNode)currentNode.Parent.Tag).AppendChild(orderNode);

            BuildTreeNode(orderNode, currentNode);
        }

        private void TsmiAddLinkEntityClick(object sender, EventArgs e)
        {
            var currentNode = treeView1.SelectedNode;

            XmlNode linkEntityNode = fetchDoc.CreateElement("link-entity");

            ((XmlNode)currentNode.Parent.Tag).AppendChild(linkEntityNode);

            BuildTreeNode(linkEntityNode, currentNode);
        }

        private void TsmiDeleteClick(object sender, EventArgs e)
        {
            var currentNode = treeView1.SelectedNode;
            var xmlNode = (XmlNode) currentNode.Tag;
            xmlNode.ParentNode.RemoveChild(xmlNode);
            currentNode.Remove();
        }

        
    }
}
