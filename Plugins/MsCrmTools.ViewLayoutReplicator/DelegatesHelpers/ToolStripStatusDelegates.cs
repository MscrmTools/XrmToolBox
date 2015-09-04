// PROJECT : MsCrmTools.ViewLayoutReplicator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System.Windows.Forms;

namespace Tanguy.WinForm.Utilities.DelegatesHelpers
{
    internal class ToolStripStatusDelegates
    {
        public static void SetText(ToolStripStatusLabel statusLabel, string text)
        {
            MethodInvoker miSetText = delegate
            {
                statusLabel.Text = text;
            };

            if (statusLabel.Owner.InvokeRequired)
            {
                statusLabel.Owner.Invoke(miSetText);
            }
            else
            {
                miSetText();
            }
        }

        public static void SetText(ToolStripDropDownButton dropdownButton, string text)
        {
            MethodInvoker miSetText = delegate
            {
                dropdownButton.Text = text;
            };

            if (dropdownButton.Owner.InvokeRequired)
            {
                dropdownButton.Owner.Invoke(miSetText);
            }
            else
            {
                miSetText();
            }
        }
    }
}