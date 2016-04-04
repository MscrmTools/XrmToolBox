// PROJECT : MsCrmTools.ViewLayoutReplicator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System.Windows.Forms;

namespace Tanguy.WinForm.Utilities.DelegatesHelpers
{
    public class ButtonDelegates
    {
        public static void SetEnableState(Button button, bool enabled)
        {
            CommonDelegates.SetEnableState(button, enabled);
        }

        public static void SetText(Button button, string text)
        {
            MethodInvoker miSetText = delegate
            {
                button.Text = text;
            };

            if (button.InvokeRequired)
            {
                button.Invoke(miSetText);
            }
            else
            {
                miSetText();
            }
        }
    }
}