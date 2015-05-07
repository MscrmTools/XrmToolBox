// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace XrmToolBox.UserControls
{
    public partial class SmallPluginModel : PluginModel
    {
        #region Event Handlers

        public override event ClickedEventHandler Clicked;

        #endregion

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
                lblCount.ForeColor = secondaryColor;
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

        private new void MouseClick(object sender, EventArgs e)
        {
            if(Clicked != null)
                Clicked(this, new EventArgs());
        }

        private void LblTitleClick(object sender, EventArgs e)
        {
            MouseClick(null, null);
        }

        public void UpdateCount(int count)
        {
            lblCount.Text = count.ToString();
            Refresh();
        }
    }
}
