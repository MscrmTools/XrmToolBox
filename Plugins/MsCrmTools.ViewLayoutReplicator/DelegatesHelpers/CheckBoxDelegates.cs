// PROJECT : MsCrmTools.ViewLayoutReplicator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System.Windows.Forms;

namespace Tanguy.WinForm.Utilities.DelegatesHelpers
{
    public class CheckBoxDelegates
    {
        public static bool GetCheckedState(CheckBox checkBox)
        {
            bool returnValue = false;

            MethodInvoker miGetCheckedState = delegate
            {
                returnValue = checkBox.Checked;
            };

            if (checkBox.InvokeRequired)
            {
                checkBox.Invoke(miGetCheckedState);
            }
            else
            {
                miGetCheckedState();
            }

            return returnValue;
        }

        public static void SetCheckedState(CheckBox checkBox, bool isChecked)
        {
            MethodInvoker miSetCheckedState = delegate
            {
                checkBox.Checked = isChecked;
            };

            if (checkBox.InvokeRequired)
            {
                checkBox.Invoke(miSetCheckedState);
            }
            else
            {
                miSetCheckedState();
            }
        }

        public static void SetEnableState(CheckBox checkBox, bool enabled)
        {
            CommonDelegates.SetEnableState(checkBox, enabled);
        }
    }
}