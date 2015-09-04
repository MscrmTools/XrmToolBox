using System.Collections;
using System.Windows.Forms;

namespace MsCrmTools.FormLibrariesManager.AppCode
{
    public class TreeViewDelegates
    {
        public static void AddNode(TreeView treeview, TreeNode node)
        {
            MethodInvoker mi = delegate
            {
                treeview.Nodes.Add(node);
            };

            if (treeview.InvokeRequired)
            {
                treeview.Invoke(mi);
            }
            else
            {
                mi();
            }
        }

        public static void AddNode(TreeNode parentNode, TreeNode node)
        {
            MethodInvoker mi = delegate
            {
                parentNode.Nodes.Add(node);
            };

            if (parentNode.TreeView.InvokeRequired)
            {
                parentNode.TreeView.Invoke(mi);
            }
            else
            {
                mi();
            }
        }

        public static void ClearNodes(TreeView treeview)
        {
            MethodInvoker mi = delegate
            {
                treeview.Nodes.Clear();
            };

            if (treeview.InvokeRequired)
            {
                treeview.Invoke(mi);
            }
            else
            {
                mi();
            }
        }

        public static void ExpandAll(TreeView treeview)
        {
            MethodInvoker mi = delegate
            {
                treeview.ExpandAll();
            };

            if (treeview.InvokeRequired)
            {
                treeview.Invoke(mi);
            }
            else
            {
                mi();
            }
        }

        public static TreeNodeCollection GetNodes(TreeView treeview)
        {
            TreeNodeCollection collection = null;

            MethodInvoker mi = delegate
            {
                collection = treeview.Nodes;
            };

            if (treeview.InvokeRequired)
            {
                treeview.Invoke(mi);
            }
            else
            {
                mi();
            }

            return collection;
        }

        public static TreeNode GetSelectedNode(TreeView treeview)
        {
            TreeNode node = null;

            MethodInvoker mi = delegate
            {
                node = treeview.SelectedNode;
            };

            if (treeview.InvokeRequired)
                treeview.Invoke(mi);
            else
                mi();

            return node;
        }

        public static void Sort(TreeView treeview, IComparer nodeSorter)
        {
            MethodInvoker mi = delegate
            {
                treeview.TreeViewNodeSorter = nodeSorter;
                treeview.Sort();
            };

            if (treeview.InvokeRequired)
            {
                treeview.Invoke(mi);
            }
            else
            {
                mi();
            }
        }
    }
}