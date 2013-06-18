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
    public partial class SmallPluginModel : UserControl
    {
        #region Delegates

        public delegate void ClickedEventHandler(object sender, EventArgs e);

        #endregion

        #region Event Handlers

        public event ClickedEventHandler Clicked;

        #endregion

        #region Constructors

        public SmallPluginModel()
        {
            InitializeComponent();
        }

        public SmallPluginModel(Image image, string title, string description, string company, string version, Color backColor, Color primaryColor, Color secondaryColor)
        {
            InitializeComponent();

            picture.Image = image;
            lblTitle.PrimaryFontColor = primaryColor;
            lblTitle.SecondaryFontColor = secondaryColor;
            lblTitle.Text = string.Format("{0} by {1} - {2}", title, company, version);

            var tip = new ToolTip();
            tip.SetToolTip(lblTitle, description);

                BackColor = backColor;
        }

        public SmallPluginModel(Stream imageStream, string title, string description, string company, string version, Color backColor, Color primaryColor, Color secondaryColor)
        {
            InitializeComponent();

            picture.Image = Image.FromStream(imageStream);
            lblTitle.PrimaryFontColor = primaryColor;
            lblTitle.SecondaryFontColor = secondaryColor;
            lblTitle.Text = string.Format("{0} by {1} - {2}", title, company, version);

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
    }
}
