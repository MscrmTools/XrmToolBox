// PROJECT : MsCrmTools.WebResourcesManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using MsCrmTools.WebResourcesManager.AppCode;
using System;
using System.IO;
using System.Windows.Forms;

namespace MsCrmTools.WebResourcesManager.UserControls
{
    /// <summary>
    /// Control that "displays" a Silverlight web resource
    /// </summary>
    public partial class SilverlightControl : UserControl, IWebResourceControl
    {
        #region Variables

        /// <summary>
        /// Base64 content of the web resource when loading this control
        /// </summary>
        private readonly string originalContent;

        /// <summary>
        /// Base64 content of the web resource
        /// </summary>
        private string innerContent;

        #endregion Variables

        #region Delegates

        public delegate void WebResourceUpdatedEventHandler(object sender, WebResourceUpdatedEventArgs e);

        #endregion Delegates

        #region Event Handlers

        public event WebResourceUpdatedEventHandler WebResourceUpdated;

        #endregion Event Handlers

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class SilverlightControl
        /// </summary>
        /// <param name="resource">Base64 content of the web resource</param>
        public SilverlightControl(string content)
        {
            InitializeComponent();

            originalContent = content;
            innerContent = content;
        }

        #endregion Constructor

        #region Methods

        public string GetBase64WebResourceContent()
        {
            return innerContent;
        }

        public Enumerations.WebResourceType GetWebResourceType()
        {
            throw new NotImplementedException();
        }

        public void ReplaceWithNewFile(string filename)
        {
            try
            {
                innerContent = Convert.ToBase64String(File.ReadAllBytes(filename));
                SendSavedMessage();
            }
            catch (Exception error)
            {
                MessageBox.Show(ParentForm, "Error while updating file: " + error.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SendSavedMessage()
        {
            var wrueArgs = new WebResourceUpdatedEventArgs
                               {
                                   Base64Content = innerContent,
                                   IsDirty = (innerContent != originalContent),
                                   Type = Enumerations.WebResourceType.Silverlight
                               };

            if (WebResourceUpdated != null)
            {
                WebResourceUpdated(this, wrueArgs);
            }
        }

        #endregion Methods
    }
}