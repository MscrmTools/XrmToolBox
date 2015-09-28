// PROJECT : MsCrmTools.SolutionImport
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System.Windows.Forms;

namespace Tanguy.WinForm.Utilities.DelegatesHelpers
{
    internal class GroupBoxDelegates
    {
        public static void SetEnableState(GroupBox gb, bool enabled)
        {
            MethodInvoker mi = delegate
            {
                gb.Enabled = enabled;
            };

            if (gb.InvokeRequired)
            {
                gb.Invoke(mi);
            }
            else
            {
                mi();
            }
        }
    }
}