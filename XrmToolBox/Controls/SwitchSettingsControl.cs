using System;
using System.Windows.Forms;
using XrmToolBox.AppCode;

namespace XrmToolBox.Controls
{
    public partial class SwitchSettingsControl : UserControl, ISettingsControl
    {
        private bool loaded;

        public SwitchSettingsControl()
        {
            InitializeComponent();

            Options.Instance.OnSettingsChanged += Instance_OnSettingsChanged;
        }

        public event EventHandler<SettingsPropertyEventArgs> OnSettingsPropertyChanged;

        public bool Checked { get; set; }

        public string Description { get; set; }

        public string PropertyName { get; set; }

        public string Title { get; set; }

        public void ApplyHeight()
        {
            lblTitle.Height = TextRenderer.MeasureText(Title, lblTitle.Font).Height;
            switchControl1.Height = 40;

            if (string.IsNullOrEmpty(Description))
            {
                lblDescription.Height = 0;
            }
            else
            {
                lblDescription.Height = TextRenderer.MeasureText(lblDescription.Text, lblDescription.Font).Height;
            }

            Height = lblTitle.Height + switchControl1.Height + lblDescription.Height + 40;
        }

        private void Instance_OnSettingsChanged(object sender, SettingsPropertyEventArgs e)
        {
            if (!string.IsNullOrEmpty(PropertyName) && e.PropertyName == PropertyName)
            {
                Checked = (bool)e.Value;
                switchControl1.OnCheckedChanged -= SwitchControl1_OnCheckedChanged;
                switchControl1.Checked = Checked;
                switchControl1.OnCheckedChanged += SwitchControl1_OnCheckedChanged;
            }
        }

        private void SwitchControl1_OnCheckedChanged(object sender, EventArgs e)
        {
            Checked = ((SwitchControl)sender).Checked;

            if (!loaded) return;

            OnSettingsPropertyChanged?.Invoke(this, new SettingsPropertyEventArgs(PropertyName, Checked));
        }

        private void SwitchSettingsControl_EnabledChanged(object sender, EventArgs e)
        {
            switchControl1.Enabled = Enabled;
            switchControl1.Invalidate();
        }

        private void SwitchSettingsControl_Load(object sender, EventArgs e)
        {
            switchControl1.Checked = Checked;
            lblTitle.Text = Title;
            lblDescription.Text = Description;

            switchControl1.OnCheckedChanged += SwitchControl1_OnCheckedChanged;

            ApplyHeight();

            loaded = true;
        }
    }
}