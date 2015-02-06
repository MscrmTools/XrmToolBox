// PROJECT : MsCrmTools.WebResourcesManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MsCrmTools.WebResourcesManager.AppCode;

namespace MsCrmTools.WebResourcesManager.UserControls
{
    /// <summary>
    /// Control that displays an image web resource
    /// </summary>
    public partial class ImageControl : UserControl, IWebResourceControl
    {
        #region Variables

        /// <summary>
        /// Type of web resource
        /// </summary>
        readonly Enumerations.WebResourceType innerType;

        /// <summary>
        /// Base64 content of the web resource when loading this control
        /// </summary>
        readonly string originalContent;

        /// <summary>
        /// Base64 content of the web resource
        /// </summary>
        string innerContent;

        #endregion Variables

        #region Delegates

        public delegate void WebResourceUpdatedEventHandler(object sender, WebResourceUpdatedEventArgs e);

        #endregion

        #region Event Handlers

        public event WebResourceUpdatedEventHandler WebResourceUpdated;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class ImageControl
        /// </summary>
        /// <param name="content">Base64 content of the web resource</param>
        /// <param name="type">Web resource type</param>
        public ImageControl(string content, Enumerations.WebResourceType type)
        {
            InitializeComponent();

            innerType = type;
            originalContent = content;
            innerContent = content;
        }

        #endregion Constructor

        #region Handlers

        private void ImageControl_Load(object sender, EventArgs e)
        {
            try
            {
                if (innerContent == null) return;

                string imageBase64 = innerContent;
                byte[] imageBytes = Convert.FromBase64String(imageBase64);

                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    pictureBox1.Image = Image.FromStream(ms, true, true);
                }

                pictureBox1.Height = pictureBox1.Image.Size.Height;
                pictureBox1.Width = pictureBox1.Image.Size.Width;

                if (pictureBox1.Width > panel1.Width)
                    pictureBox1.Width = panel1.Width;

                pictureBox1.Location = new Point(
                    panel1.Width/2 - pictureBox1.Width/2,
                    panel1.Height/2 - pictureBox1.Height/2);
            }
            catch (Exception error)
            {
                MessageBox.Show("An error occured while loading this web resource: " + error.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion Handlers

        #region Methods

       
        public string GetBase64WebResourceContent()
        {
            return innerContent;
        }

        public void ReplaceWithNewFile(string filename)
        {
            try
            {
                innerContent = Convert.ToBase64String(File.ReadAllBytes(filename));
                ImageControl_Load(null, null);

                SendSavedMessage();
            }
            catch (Exception error)
            {
                MessageBox.Show(ParentForm, "Error while updating file: " + error.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Enumerations.WebResourceType GetWebResourceType()
        {
            return innerType;
        }

        private void SendSavedMessage()
        {
            var wrueArgs = new WebResourceUpdatedEventArgs
                                                       { 
                Base64Content = innerContent,
                IsDirty = (innerContent != originalContent),
                Type = innerType
            };

            if (WebResourceUpdated != null)
            {
                WebResourceUpdated(this, wrueArgs);
            }
        }

        #endregion Methods
    }
}
