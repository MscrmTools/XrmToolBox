using System;
using System.Drawing;
using System.Windows.Forms;

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
        }

        public string Placeholder
        {
            get { return placeholder; }
            set
            {
                placeholder = value;
                Text = value;
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (!silent)
            {
                base.OnTextChanged(e);
            }
        }

        private void TextBoxWithPlaceholder_Enter(object sender, EventArgs e)
        {
            if (Text == placeholder)
            {
                silent = true;
                Text = "";
                ForeColor = SystemColors.ControlText;
                silent = false;
            }
        }

        private void TextBoxWithPlaceholder_Leave(object sender, EventArgs e)
        {
            if (Text.Length == 0)
            {
                silent = true;
                Text = placeholder;
                ForeColor = Color.LightGray;
                silent = false;
            }
        }
    }
}