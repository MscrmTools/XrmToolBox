// PROJECT : MsCrmTools.SolutionImport
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System.Windows.Forms;

namespace Tanguy.WinForm.Utilities.DelegatesHelpers
{
    public class CommonDelegates
    {
        public static DialogResult DisplayMessageBox(Form form, string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            DialogResult result = DialogResult.Ignore;

            MethodInvoker DisplayMessage = delegate
            {
                result = MessageBox.Show(form, message, caption, buttons, icon);
            };

            if (form.InvokeRequired)
            {
                form.Invoke(DisplayMessage);
                return result;
            }
            else
            {
                DisplayMessage();
                return result;
            }
        }

        public static void SetCursor(Control control, Cursor cursor)
        {
            MethodInvoker miSetCursor = delegate
            {
                control.Cursor = cursor;
            };

            if (control.InvokeRequired)
            {
                control.Invoke(miSetCursor);
            }
            else
            {
                miSetCursor();
            }
        }

        public static void SetFocus(Control control)
        {
            MethodInvoker miSetFocus = delegate
            {
                control.Focus();
            };

            if (control.InvokeRequired)
            {
                control.Invoke(miSetFocus);
            }
            else
            {
                miSetFocus();
            }
        }

        internal static void SetEnableState(Control control, bool enabled)
        {
            MethodInvoker miSetEnableState = delegate
            {
                control.Enabled = enabled;
            };

            if (control.InvokeRequired)
            {
                control.Invoke(miSetEnableState);
            }
            else
            {
                miSetEnableState();
            }
        }
    }
}