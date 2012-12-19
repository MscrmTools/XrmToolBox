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
    public partial class PluginModel : UserControl
    {
        #region Delegates

        public delegate void ClickedEventHandler(object sender, EventArgs e);

        #endregion

        #region Event Handlers

        public event ClickedEventHandler Clicked;

        #endregion

        #region Constructors

        public PluginModel()
        {
            InitializeComponent();
        }

        public PluginModel(Image image, string title, string description, string company, string version)
        {
            InitializeComponent();

            picture.Image = image;
            lblTitle.Text = title;
            lblDescription.Text = description;
            lblAuthor.Text = "Author: " + company;
            lblVersion.Text = "Version: " + version;
        }

        public PluginModel(Stream imageStream, string title, string description, string company, string version)
        {
            InitializeComponent();
         
            picture.Image = Image.FromStream(imageStream);
            lblTitle.Text = title;
            lblDescription.Text = description;
            lblAuthor.Text = "Author: " + company;
            lblVersion.Text = "Version: " + version;
        }

        #endregion Constructors

        private void MouseClick(object sender, EventArgs e)
        {
            if(Clicked != null)
                Clicked(this, new EventArgs());
        }
    }
}
