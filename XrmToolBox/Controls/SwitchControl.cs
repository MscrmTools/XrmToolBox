using System;
using System.Drawing;
using System.Windows.Forms;
using XrmToolBox.AppCode;

namespace XrmToolBox.Controls
{
    public partial class SwitchControl : UserControl
    {
        private bool _checked;

        public SwitchControl()
        {
            InitializeComponent();
        }

        public event EventHandler OnCheckedChanged;

        public bool Checked
        {
            get
            {
                return _checked;
            }
            set
            {
                _checked = value;
                Invalidate();
                OnCheckedChanged?.Invoke(this, new EventArgs());
            }
        }

        public bool Enabled { get; set; } = true;

        private void SwitchControl_Click(object sender, EventArgs e)
        {
            Checked = !Checked;
        }

        private void SwitchControl_Paint(object sender, PaintEventArgs e)
        {
            using (Image img = (_checked ? Properties.Resources.on_button : Properties.Resources.off_button).ResizeImage(40, 40))
            {
                e.Graphics.DrawImage(img, new Point(4, Height / 2 - img.Height / 2));
            }
            var text = _checked ? "True" : "False";
            var textSize = TextRenderer.MeasureText(text, Font);

            e.Graphics.DrawString(text, Font, new SolidBrush(Enabled ? ForeColor : Color.LightGray), new Point(50, Height / 2 - textSize.Height / 2));
        }
    }
}