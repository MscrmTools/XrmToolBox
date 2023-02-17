using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using XrmToolBox.AppCode;

namespace XrmToolBox.Controls
{
    public partial class TextBoxSettingsControl : TextBoxSettingsControl<string>, ISettingsControl
    {
        public TextBoxSettingsControl() : base()
        {
        }
    }

    public partial class TextBoxSettingsControl<T> : UserControl, ISettingsControl
    {
        private bool loaded;
        private Regex rg;

        public TextBoxSettingsControl()
        {
            InitializeComponent();
        }

        public event EventHandler<SettingsPropertyEventArgs> OnSettingsPropertyChanged;

        public string Description { get; set; }
        public bool IsPassword { get; set; }
        public bool Multiline { get; set; }
        public string PropertyName { get; set; }
        public bool Readonly { get; set; }
        public string Title { get; set; }
        public string ValidationRegEx { get; set; }

        private void SwitchSettingsControl_Load(object sender, EventArgs e)
        {
            txtText.ReadOnly = Readonly;

            if (IsPassword) txtText.PasswordChar = '*';

            txtText.Text = Text;
            txtText.Multiline = Multiline;
            if (Multiline)
            {
                txtText.Height = 80;
                txtText.ScrollBars = ScrollBars.Vertical;
            }

            lblTitle.Text = Title;

            lblDescription.Text = Description;

            lblTitle.TextAlign = ContentAlignment.TopLeft;
            lblTitle.Height = TextRenderer.MeasureText(Title, lblTitle.Font).Height + 20;

            if (string.IsNullOrEmpty(Description))
            {
                lblDescription.Height = 0;
            }
            else
            {
                lblDescription.Height = TextRenderer.MeasureText(lblDescription.Text, lblDescription.Font).Height;
            }

            Height = lblTitle.Height + txtText.Height + lblDescription.Height;

            loaded = true;
        }

        private void TxtText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtText_Leave(this, e);
            }
        }

        private void txtText_Leave(object sender, EventArgs e)
        {
            if (!loaded) return;
            if (txtText.Text == Text) return;

            if (!string.IsNullOrEmpty(ValidationRegEx))
            {
                rg = new Regex(ValidationRegEx);

                if (rg.IsMatch(txtText.Text))
                {
                    Text = txtText.Text;
                    OnSettingsPropertyChanged?.Invoke(this, new SettingsPropertyEventArgs(PropertyName, (T)Convert.ChangeType(txtText.Text, typeof(T), CultureInfo.InvariantCulture)));
                    return;
                }
            }

            Text = txtText.Text;

            OnSettingsPropertyChanged?.Invoke(this, new SettingsPropertyEventArgs(PropertyName, txtText.Text));
        }

        private void txtText_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ValidationRegEx))
            {
                rg = new Regex(ValidationRegEx);

                txtText.ForeColor = rg.IsMatch(txtText.Text) ? SystemColors.ControlText : Color.Red;
            }
        }
    }
}