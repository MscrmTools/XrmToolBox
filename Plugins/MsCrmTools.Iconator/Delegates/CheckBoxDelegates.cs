// PROJECT : MsCrmTools.Iconator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System.Windows.Forms;

namespace MsCrmTools.Iconator.Delegates
{
    public class CheckBoxDelegates
    {
        public static bool IsChecked(CheckBox cbox)
        {
            var isChecked = false;

            MethodInvoker miIsChecked = delegate
                {
                    isChecked = cbox.Checked;
                };

            if (cbox.InvokeRequired)
            {
                cbox.Invoke(miIsChecked);
            }
            else
            {
                miIsChecked();
            }

            return isChecked;
        }
    }
}