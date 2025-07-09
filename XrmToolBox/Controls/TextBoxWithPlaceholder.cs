using System;
using System.Drawing;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace XrmToolBox.Controls
{
    public class TextBoxWithPlaceholder : TextBox
    {
        private string placeholder;
        private bool silent;

        public TextBoxWithPlaceholder()
        {
            ForeColor = Color.LightGray;
            Enter += TextBoxWithPlaceholder_Enter;
            Leave += TextBoxWithPlaceholder_Leave;
            GotFocus += TextBoxWithPlaceholder_GotFocus;
            Tag = true;
        }

        public string Placeholder
        {
            get { return placeholder; }
            set
            {
                placeholder = value;
                base.Text = value;
            }
        }

        public override string Text
        {
            get
            {
                if ((bool)Tag)
                    return string.Empty;
                return base.Text;
            }
            set => base.Text = value;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (!silent)
            {
                base.OnTextChanged(e);
            }

            ForeColor = CustomTheme.Instance.IsActive ? CustomTheme.Instance.ForeColor2 : SystemColors.ControlText;
        }

        private void TextBoxWithPlaceholder_Enter(object sender, EventArgs e)
        {
            if ((bool)Tag)
            {
                Tag = false;
                silent = true;
                Text = "";
                ForeColor = CustomTheme.Instance.IsActive ? CustomTheme.Instance.ForeColor2 : SystemColors.ControlText;
                silent = false;
            }
        }

        private void TextBoxWithPlaceholder_GotFocus(object sender, EventArgs e)
        {
            TextBoxWithPlaceholder_Enter(sender, e);
        }

        private void TextBoxWithPlaceholder_Leave(object sender, EventArgs e)
        {
            if (Text.Length == 0)
            {
                Tag = true;
                silent = true;
                Text = placeholder;
                ForeColor = CustomTheme.Instance.IsActive ? CustomTheme.Instance.ForeColor1 : Color.LightGray;
                silent = false;
            }
        }
    }
}