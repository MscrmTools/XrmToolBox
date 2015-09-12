using Microsoft.Xrm.Sdk;
using MsCrmTools.FormLibrariesManager.AppCode;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MsCrmTools.FormLibrariesManager.UserControls
{
    public partial class CrmScriptsTreeView : UserControl
    {
        public CrmScriptsTreeView()
        {
            InitializeComponent();
        }

        public IOrganizationService Service { get; set; }

        public List<Entity> GetSelectedScripts()
        {
            var scripts = new List<Entity>();

            GetNodes(scripts, ScriptsTreeView, true);

            return scripts;
        }

        public void LoadScripts(List<Entity> scripts)
        {
            ScriptsTreeView.Nodes.Clear();

            foreach (Entity script in scripts)
            {
                string[] nameParts = script["name"].ToString().Split('/');

                AddNode(nameParts, 0, ScriptsTreeView, script);
            }

            ScriptsTreeView.TreeViewNodeSorter = new NodeSorter();
            ScriptsTreeView.Sort();
            ScriptsTreeView.ExpandAll();
            ScriptsTreeView.Invalidate();
        }

        private void AddNode(string[] nameParts, int index, object parent, Entity script)
        {
            if (index == 0)
            {
                if (((TreeView)parent).Nodes.Find(nameParts[index], false).Length == 0)
                {
                    var node = new TreeNode(nameParts[index]);
                    node.Name = node.Text;

                    if (index == nameParts.Length - 1)
                    {
                        node.Tag = script;

                        int imageIndex = ((OptionSetValue)script["webresourcetype"]).Value + 1;
                        node.ImageIndex = imageIndex;
                        node.SelectedImageIndex = imageIndex;
                    }
                    else
                    {
                        node.ImageIndex = 0;
                        node.SelectedImageIndex = 0;
                    }

                    TreeViewDelegates.AddNode((TreeView)parent, node);

                    AddNode(nameParts, index + 1, node, script);
                }
                else
                {
                    AddNode(nameParts, index + 1, ((TreeView)parent).Nodes.Find(nameParts[index], false)[0], script);
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
                        node.Tag = script;
                        int imageIndex = ((OptionSetValue)script["webresourcetype"]).Value + 1;
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
                    AddNode(nameParts, index + 1, node, script);
                }
                else
                {
                    AddNode(nameParts, index + 1, ((TreeNode)parent).Nodes.Find(nameParts[index], false)[0], script);
                }
            }
        }

        private void CheckTreeNode(TreeNode node)
        {
            foreach (TreeNode childNode in node.Nodes)
            {
                childNode.Checked = node.Checked;
            }
        }

        private void GetNodes(List<Entity> scripts, object o, bool onlyCheckedNodes)
        {
            var tView = o as TreeView;
            if (tView != null)
            {
                foreach (TreeNode node in tView.Nodes)
                {
                    if (onlyCheckedNodes && node.Checked || !onlyCheckedNodes)
                        if (node.Tag != null)
                        {
                            scripts.Add((Entity)node.Tag);
                        }

                    GetNodes(scripts, node, onlyCheckedNodes);
                }
            }
            else
            {
                foreach (TreeNode node in ((TreeNode)o).Nodes)
                {
                    if (onlyCheckedNodes && node.Checked || !onlyCheckedNodes)
                        if (node.Tag != null)
                        {
                            scripts.Add((Entity)node.Tag);
                        }

                    GetNodes(scripts, node, onlyCheckedNodes);
                }
            }
        }

        private void ScriptsTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            CheckTreeNode(e.Node);
        }
    }
}