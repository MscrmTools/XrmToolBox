using System;
using System.Drawing;
using System.Windows.Forms;
using XrmToolBox.AppCode;

namespace XrmToolBox.Controls
{
    public partial class NavLeftItem : UserControl
    {
        private bool _selected;

        public NavLeftItem()
        {
            InitializeComponent();

            Cursor = Cursors.Hand;
        }

        public event EventHandler OnSelectedChanged;

        public int DisplayIndex { get; set; }
        public bool Expanded { get; set; } = true;
        public Image Image { get; set; }
        public int Index { get; set; }
        public Panel Panel { get; set; }

        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                Invalidate();
                OnSelectedChanged?.Invoke(this, new EventArgs());
            }
        }

        private void NavLeftItem_Click(object sender, EventArgs e)
        {
            Selected = !Selected;
        }

        private void NavLeftItem_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(_selected ? new SolidBrush(Color.White) : new SolidBrush(SystemColors.Control), e.ClipRectangle);

            if (Image != null)
            {
                using (var resizedImage = Image.ResizeImage(24, 24))
                {
                    e.Graphics.DrawImage(resizedImage, new Point(10, Height / 2 - resizedImage.Height / 2));
                }
            }

            if (Expanded)
            {
                using (SolidBrush brush = new SolidBrush(ForeColor))
                {
                    StringFormat _stringFlags = new StringFormat();
                    _stringFlags.Alignment = StringAlignment.Near;
                    _stringFlags.LineAlignment = StringAlignment.Center;
                    e.Graphics.DrawString(Text, Font, brush, new Point(50, 22), new StringFormat(_stringFlags));
                }
            }
        }
    }
}