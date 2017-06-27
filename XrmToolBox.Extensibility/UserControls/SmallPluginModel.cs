// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Drawing;
using System.Windows.Forms;

namespace XrmToolBox.Extensibility.UserControls
{
    public partial class SmallPluginModel : PluginModel
    {
        #region Event Handlers

        public override event EventHandler<MouseEventArgs> Clicked;

        #endregion Event Handlers

        #region Constructors

        public SmallPluginModel()
        {
            InitializeComponent();
        }

        public SmallPluginModel(Image image, string title, string description, string company, string version, Color backColor, Color primaryColor, Color secondaryColor, int count)
        {
            InitializeComponent();

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
            if (Clicked != null)
                Clicked(this, e);
        }
    }
}