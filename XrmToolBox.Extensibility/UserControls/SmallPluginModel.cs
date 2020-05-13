// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using XrmToolBox.Extensibility.Interfaces;
using TimeSpan = System.TimeSpan;

namespace XrmToolBox.Extensibility.UserControls
{
    public partial class SmallPluginModel : PluginModel
    {
        #region Event Handlers

        public override event EventHandler<MouseEventArgs> Clicked;

        #endregion Event Handlers

        #region Constructors

        public SmallPluginModel(int daysToShowNewRibbon)
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            numberOfDaysToShowNewRibbon = daysToShowNewRibbon;
        }

        public SmallPluginModel(Image image, string title, string description, string company, string version, Color backColor, Color primaryColor, Color secondaryColor, int count, int daysToShowNewRibbon)
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            numberOfDaysToShowNewRibbon = daysToShowNewRibbon;

            picture.Image = image;
            lblTitle.PrimaryFontColor = primaryColor;
            lblTitle.SecondaryFontColor = secondaryColor;
            lblTitle.Text = string.Format("{0} by {1} - {2}", title, company, version);

            if (count > 0)
            {
                lblCount.Text = count.ToString();
                lblCount.ForeColor = primaryColor;
            }
            else
            {
                lblCount.Visible = false;
            }

            var tip = new ToolTip();
            tip.SetToolTip(lblTitle, description);

            BackColor = backColor;
        }

        #endregion Constructors

        public void UpdateCount(int count)
        {
            lblCount.Text = count.ToString();
            Refresh();
        }

        private new void MouseClick(object sender, MouseEventArgs e)
        {
            Clicked?.Invoke(this, e);
        }

        private void SmallPluginModel_Paint(object sender, PaintEventArgs e)
        {
            var time = new FileInfo(((Lazy<IXrmToolBoxPlugin, IPluginMetadata>)Tag).Value.GetType().Assembly.Location)
                .CreationTime;

            var ctrl = (Control)sender;
            if (DateTime.Now - time < new TimeSpan(numberOfDaysToShowNewRibbon, 0, 0, 0))
            {
                e.Graphics.FillPolygon(new SolidBrush(Color.Green), new[]
                {
                    new Point(ctrl.Width, ctrl.Height),
                    new Point(ctrl.Width - ctrl.Height, 0),
                    new Point(ctrl.Width - ctrl.Height + 20, 0),
                    new Point(ctrl.Width, ctrl.Height - 20),
                }, FillMode.Winding);

                e.Graphics.DrawLine(new Pen(Color.White), new Point(ctrl.Width, ctrl.Height), new Point(ctrl.Width - ctrl.Height, 0));
                e.Graphics.DrawLine(new Pen(Color.White), new Point(ctrl.Width - ctrl.Height + 20, 0), new Point(ctrl.Width, ctrl.Height - 20));

                DrawRotatedTextAt(e.Graphics, 45, "NEW", ctrl.Width - ctrl.Height / 2, 5, new Font(new FontFamily("Segoe UI"), 8),
                    new SolidBrush(Color.White));

                lblCount.TextAlign = ContentAlignment.BottomLeft;
            }
        }

        private void SmallPluginModel_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}