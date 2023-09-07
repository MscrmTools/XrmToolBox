using McTools.Xrm.Connection;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace XrmToolBox.Controls
{
    public partial class HighlightItem : UserControl
    {
        private Color from;
        private Color to;

        public HighlightItem()
        {
            InitializeComponent();
        }

        public HighlightItem(ConnectionDetail detail)
        {
            InitializeComponent();

            SetContent(detail);
        }

        public HighlightItem(Color from, Color to)
        {
            InitializeComponent();

            Controls.Clear();

            this.from = from;
            this.to = to;

            Paint += HighlightItem_Paint;
        }

        private void HighlightItem_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush linGrBrush = new LinearGradientBrush(
                    new Point(0, 10),
                    new Point(Width, 10),
                    from,   // Opaque red
                    to);  // Opaque blue

            e.Graphics.FillRectangle(linGrBrush, 0, 0, Width, Height);
        }

        private void HighlightItem_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void SetContent(ConnectionDetail detail)
        {
            BackColor = detail.EnvironmentHighlightingInfo?.Color ?? DefaultBackColor;
            lblName.ForeColor = detail.EnvironmentHighlightingInfo?.TextColor ?? DefaultForeColor;
            lblName.Text = detail.ConnectionName;

            if (detail.ParentConnectionFile?.Base64Image != null)
            {
                byte[] bytes = Convert.FromBase64String(detail.ParentConnectionFile.Base64Image);

                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    pbLogo.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbLogo.Image = Image.FromStream(ms);
                }
            }
            else
            {
                pbLogo.Visible = false;
            }

            Width = 10 + 8 + TextRenderer.MeasureText(lblName.Text, lblName.Font).Width + (pbLogo.Visible ? pbLogo.Width : 0);
        }
    }
}