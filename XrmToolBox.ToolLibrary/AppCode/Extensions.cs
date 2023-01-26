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

        public static void SetAutoWidth(this LinkLabel label)
        {
            label.Width = TextRenderer.MeasureText(label.Text, label.Font).Width;
        }

        public static void SetAutoWidth(this ComboBox combobox)
        {
            string longestWord = "";

            foreach (object item in combobox.Items)
            {
                if (item.ToString().Length > longestWord.Length)
                {
                    longestWord = item.ToString();
                }
            }

            combobox.Width = TextRenderer.MeasureText(longestWord, combobox.Font).Width + 16;
        }
    }
}