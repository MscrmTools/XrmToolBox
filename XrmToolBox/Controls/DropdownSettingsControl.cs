using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using XrmToolBox.AppCode;

namespace XrmToolBox.Controls
{
    public partial class DropdownSettingsControl : UserControl, ISettingsControl
    {
        private bool loaded;

        public DropdownSettingsControl()
        {
            InitializeComponent();
        }

        public event EventHandler<SettingsPropertyEventArgs> OnSettingsPropertyChanged;

        public string Description { get; set; }
        public Type EnumType { get; set; }
        public string PropertyName { get; set; }
        public string Title { get; set; }

        [Browsable(false)]
        public Enum Value { get; set; }

        private void cbbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!loaded) return;

            var items = Enum.GetValues(EnumType);
            foreach (var item in items)
            {
                string desc = GetEnumDescription((Enum)item);

                if (cbbList.SelectedItem.ToString() == desc)
                {
                    Value = (Enum)item;
                    OnSettingsPropertyChanged?.Invoke(this, new SettingsPropertyEventArgs(PropertyName, (Enum)item));
                }
            }
        }

        private string GetEnumDescription(Enum value)
        {
            FieldInfo fi = EnumType.GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

        private void SwitchSettingsControl_Load(object sender, EventArgs e)
        {
            lblTitle.Text = Title;
            lblDescription.Text = Description;

            try
            {
                var items = Enum.GetValues(EnumType);
                foreach (var item in items)
                {
                    cbbList.Items.Add(GetEnumDescription((Enum)item));

                    if (item.ToString() == Value.ToString())
                    {
                        cbbList.SelectedIndex = cbbList.Items.Count - 1;
                    }
                }
            }
            catch { }

            lblTitle.Height = TextRenderer.MeasureText(lblTitle.Text, lblTitle.Font).Height;
            cbbList.Height = 40;

            if (string.IsNullOrEmpty(Description))
            {
                lblDescription.Height = 0;
            }
            else
            {
                lblDescription.Height = TextRenderer.MeasureText(lblDescription.Text, lblDescription.Font).Height;
            }

            Height = lblTitle.Height + cbbList.Height + lblDescription.Height + 20;

            loaded = true;
        }
    }
}