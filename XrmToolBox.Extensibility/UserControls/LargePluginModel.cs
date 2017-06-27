// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Drawing;
using System.Windows.Forms;

namespace XrmToolBox.Extensibility.UserControls
{
    public partial class LargePluginModel : PluginModel
    {
        #region Event Handlers

        public override event EventHandler<MouseEventArgs> Clicked;

        #endregion Event Handlers

        #region Constructors

        public LargePluginModel()
        {
            InitializeComponent();
        }

        public LargePluginModel(Image image, string title, string description, string company, string version, Color backColor, Color primaryColor, Color secondaryColor, int count)
        {
            InitializeComponent();

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

        private new void MouseClick(object sender, MouseEventArgs e)
        {
            if (Clicked != null)
                Clicked(this, e);
        }
    }
}