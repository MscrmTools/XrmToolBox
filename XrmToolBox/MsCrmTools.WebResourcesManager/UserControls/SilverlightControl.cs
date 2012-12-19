﻿// PROJECT : MsCrmTools.WebResourcesManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.IO;
using System.Windows.Forms;
using MsCrmTools.WebResourcesManager.AppCode;

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

        public void ReplaceWithNewFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select a file to replace the existing web resource";
            ofd.Filter = "Silverlight application file (*.xap)|*.xap";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                innerContent = Convert.ToBase64String(File.ReadAllBytes(ofd.FileName));

                SendSavedMessage();
            }
        }

        private void SendSavedMessage()
        {
            WebResourceUpdatedEventArgs wrueArgs = new WebResourceUpdatedEventArgs
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

        #endregion  Methods
    }
}
