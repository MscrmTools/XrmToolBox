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

        public static Version Simplify(this Version version)
        {
            if (version.Revision != -1)
                return version;

            return new Version(
                version.Major == -1 ? 0 : version.Major,
                version.Minor == -1 ? 0 : version.Minor,
                version.Build == -1 ? 0 : version.Build,
                version.Revision == -1 ? 0 : version.Revision);
        }
    }
}