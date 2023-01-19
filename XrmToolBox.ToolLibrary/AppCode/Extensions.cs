using System;
using System.Windows.Forms;

namespace XrmToolBox.ToolLibrary.AppCode
{
    internal static class Extensions
    {
        public static void SetAutoWidth(this Label label)
        {
            label.Width = TextRenderer.MeasureText(label.Text, label.Font).Width;
        }
    }
}