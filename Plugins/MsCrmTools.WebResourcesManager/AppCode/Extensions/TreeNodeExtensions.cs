using System;
using System.Windows.Forms;

namespace MsCrmTools.WebResourcesManager.New.Extensions
{
    public static class TreeNodeExtensions
    {
        public static T GetCastedTag<T>(this TreeNode node)
        {
            if (node.Tag.GetType() == typeof(T))
            {
                return (T)node.Tag;
            }

            throw new Exception("This node tag does not have the type expected");
        }
    }
}