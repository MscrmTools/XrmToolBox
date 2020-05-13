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

namespace XrmToolBox.Extensibility.UserControls
{
    public partial class LargePluginModel : PluginModel
    {
        #region Event Handlers

        public override event EventHandler<MouseEventArgs> Clicked;

        #endregion Event Handlers

        #region Constructors

        public LargePluginModel(int daysToShowNewRibbon)
        {
            InitializeComponent();
            numberOfDaysToShowNewRibbon = daysToShowNewRibbon;
        }

        public LargePluginModel(Image image, string title, string description, string company, string version, Color backColor, Color primaryColor, Color secondaryColor, int count, int daysToShowNewRibbon)
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            numberOfDaysToShowNewRibbon = daysToShowNewRibbon;

            picture.Image = image;
            lblTitle.Text = title;
            lblDescription.Text = description;
            lblAuthor.Text = "Author: " + company;
            lblVersion.Text = "Version: " + version;

            lblCount.ForeColor = primaryColor;
            lblTitle.ForeColor = primaryColor;
            lblDescription.ForeColor = primaryColor;
            lblAuthor.ForeColor = primaryColor;
            lblVersion.ForeColor = primaryColor;

            BackColor = backColor;

            if (count > 0)
            {
                lblCount.Text = count.ToString();
            }
            else
            {
                lblCount.Visible = false;
            }
        }

        #endregion Constructors

        public void UpdateCount(int count)
        {
            lblCount.Text = count.ToString();
        }

        private void LargePluginModel_Paint(object sender, PaintEventArgs e)
        {
            var time = new FileInfo(((Lazy<IXrmToolBoxPlugin, IPluginMetadata>)Tag).Value.GetType().Assembly.Location).CreationTime;

            var ctrl = (Control)sender;
            if (DateTime.Now - time < new TimeSpan(numberOfDaysToShowNewRibbon, 0, 0, 0))
            {
                e.Graphics.FillPolygon(new SolidBrush(Color.Green), new[]
                {
                    new Point(ctrl.Width, ctrl.Height),
                    new Point(ctrl.Width - ctrl.Height, 0),
                    new Point(ctrl.Width - ctrl.Height + 40, 0),
                    new Point(ctrl.Width, ctrl.Height - 40),
                }, FillMode.Winding);

                e.Graphics.DrawLine(new Pen(Color.White), new Point(ctrl.Width, ctrl.Height), new Point(ctrl.Width - ctrl.Height, 0));
                e.Graphics.DrawLine(new Pen(Color.White), new Point(ctrl.Width - ctrl.Height + 40, 0), new Point(ctrl.Width, ctrl.Height - 40));

                DrawRotatedTextAt(e.Graphics, 45, "NEW", ctrl.Width - ctrl.Height / 2, 15, new Font(new FontFamily("Segoe UI"), 12),
                    new SolidBrush(Color.White));
            }
        }

        private void LargePluginModel_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private new void MouseClick(object sender, MouseEventArgs e)
        {
            Clicked?.Invoke(this, e);
        }
    }
}