using System;
using System.Drawing;
using System.Windows.Forms;

namespace MsCrmTools.WebResourcesManager.New.EventHandlers
{
    public class WebResourceContextMenuRequestedEventArgs : EventArgs
    {
        public WebResourceContextMenuRequestedEventArgs(TreeNode node, Point location)
        {
            Location = location;
            Node = node;
        }

        public Point Location { get; set; }
        public TreeNode Node { get; set; }
    }
}