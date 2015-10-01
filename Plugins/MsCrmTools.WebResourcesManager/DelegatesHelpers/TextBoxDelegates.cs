// PROJECT : MsCrmTools.WebResourcesManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System.Windows.Forms;

namespace MsCrmTools.WebResourcesManager.DelegatesHelpers
{
    public class TextBoxDelegates
    {
        internal static void SetText(TextBox control, string text)
        {
            MethodInvoker miSetText = delegate
            {
                control.Text = text;
            };

            if (control.InvokeRequired)
            {
                control.Invoke(miSetText);
            }
            else
            {
                miSetText();
            }
        }
    }
}