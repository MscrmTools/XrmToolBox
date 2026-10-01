using System;
using System.Drawing;
using System.Windows.Forms;
using XrmToolBox.AppCode;
using XrmToolBox.Extensibility;

namespace XrmToolBox.Controls
{
    public partial class NavLeftItem : UserControl
    {
        private bool _hover;
        private bool _selected;

        public NavLeftItem()
        {
            InitializeComponent();

            Cursor = Cursors.Hand;
            GotFocus += NavLeftItem_GotFocus;
            LostFocus += NavLeftItem_LostFocus;
            KeyDown += NavLeftItem_KeyDown;
            MouseHover += NavLeftItem_Enter;
            MouseLeave += NavLeftItem_Leave;
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

        public bool SelectedOnFocus { get; set; }

        public bool UseCustomHighlightColor { get; set; }

        private void NavLeftItem_Click(object sender, EventArgs e)
        {
            Selected = !Selected;
            Invalidate();
        }

        private void NavLeftItem_Enter(object sender, EventArgs e)
        {
            _hover = true;
            Invalidate();
        }

        private void NavLeftItem_GotFocus(object sender, EventArgs e)
        {
            BackColor = CustomTheme.Instance.IsActive ? CustomTheme.Instance.HighlightColor : SystemColors.Highlight;

            if (SelectedOnFocus)
            {
                Selected = !Selected;
            }
        }

        private void NavLeftItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NavLeftItem_Click(sender, e);
            }
        }

        private void NavLeftItem_Leave(object sender, EventArgs e)
        {
            _hover = false;
            Invalidate();
        }

        private void NavLeftItem_LostFocus(object sender, EventArgs e)
        {
            BackColor = CustomTheme.Instance.IsActive ? CustomTheme.Instance.Background1 : SystemColors.Control;
        }

        private void NavLeftItem_Paint(object sender, PaintEventArgs e)
        {
            var backBrush = new SolidBrush(CustomTheme.Instance.IsActive ? CustomTheme.Instance.Background1 : SystemColors.Control);
            if (_selected || _hover) backBrush.Color = CustomTheme.Instance.IsActive ? CustomTheme.Instance.HighlightColor : Color.White;
            if (Focused && !UseCustomHighlightColor) backBrush.Color = CustomTheme.Instance.IsActive ? CustomTheme.Instance.HighlightColor : SystemColors.Highlight;

            using (backBrush)
            {
                e.Graphics.FillRectangle(backBrush, e.ClipRectangle);

                if (Image != null)
                {
                    using (var resizedImage = Image.ResizeImage(24, 24))
                    {
                        e.Graphics.DrawImage(resizedImage, new Point(10, Height / 2 - resizedImage.Height / 2));
                    }
                }

                if (Expanded)
                {
                    using (SolidBrush brush = new SolidBrush(Focused && !UseCustomHighlightColor ? (CustomTheme.Instance.IsActive ? CustomTheme.Instance.ForeColor2 : Color.White) : ForeColor))
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
}