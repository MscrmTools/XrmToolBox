// PROJECT : MsCrmTools.ViewLayoutReplicator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System.Windows.Forms;

namespace Tanguy.WinForm.Utilities.DelegatesHelpers
{
    public class RadioButtonDelegates
    {
        public static bool GetCheckedState(RadioButton radioButton)
        {
            bool returnValue = false;

            MethodInvoker miGetCheckedState = delegate
            {
                returnValue = radioButton.Checked;
            };

            if (radioButton.InvokeRequired)
            {
                radioButton.Invoke(miGetCheckedState);
            }
            else
            {
                miGetCheckedState();
            }

            return returnValue;
        }

        public static void SetCheckedState(RadioButton radioButton, bool isChecked)
        {
            MethodInvoker miSetCheckedState = delegate
            {
                radioButton.Checked = isChecked;
            };

            if (radioButton.InvokeRequired)
            {
                radioButton.Invoke(miSetCheckedState);
            }
            else
            {
                miSetCheckedState();
            }
        }

        public static void SetEnableState(RadioButton radioButton, bool enabled)
        {
            CommonDelegates.SetEnableState(radioButton, enabled);
        }
    }
}