// PROJECT : MsCrmTools.ViewLayoutReplicator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System.Drawing;
using System.Windows.Forms;

namespace Tanguy.WinForm.Utilities.DelegatesHelpers
{
    public class TextBoxDelegates
    {
        public static string GetText(TextBox textBox)
        {
            string returnValue = string.Empty;

            MethodInvoker miGetText = delegate
            {
                returnValue = textBox.Text;
            };

            if (textBox.InvokeRequired)
            {
                textBox.Invoke(miGetText);
            }
            else
            {
                miGetText();
            }

            return returnValue;
        }

        public static void SetEnableState(TextBox textBox, bool enabled)
        {
            CommonDelegates.SetEnableState(textBox, enabled);
        }

        public static void SetFont(TextBox textBox, Font font)
        {
            MethodInvoker miSetFont = delegate
            {
                textBox.Font = font;
            };

            if (textBox.InvokeRequired)
            {
                textBox.Invoke(miSetFont);
            }
            else
            {
                miSetFont();
            }
        }

        public static void SetForeColor(TextBox textBox, Color color)
        {
            MethodInvoker miSetForeColor = delegate
            {
                textBox.ForeColor = color;
            };

            if (textBox.InvokeRequired)
            {
                textBox.Invoke(miSetForeColor);
            }
            else
            {
                miSetForeColor();
            }
        }

        public static void SetText(TextBox textBox, string text)
        {
            MethodInvoker miSetText = delegate
            {
                textBox.Text = text;
            };

            if (textBox.InvokeRequired)
            {
                textBox.Invoke(miSetText);
            }
            else
            {
                miSetText();
            }
        }
    }
}