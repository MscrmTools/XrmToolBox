// PROJECT : MsCrmTools.WebResourcesManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.IO;
using System.Windows.Forms;
using MsCrmTools.WebResourcesManager.AppCode;
using MsCrmTools.WebResourcesManager.Forms;

namespace MsCrmTools.WebResourcesManager.UserControls
{
    /// <summary>
    /// Control that displays a code web resource
    /// </summary>
    public partial class CodeControl : UserControl, IWebResourceControl
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

        readonly FindAndReplaceForm findForm = new FindAndReplaceForm();
               
        #endregion Variables

        #region Delegates

        public delegate void WebResourceUpdatedEventHandler(object sender, WebResourceUpdatedEventArgs e);

        #endregion

        #region Event Handlers

        public event WebResourceUpdatedEventHandler WebResourceUpdated;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class CodeControl
        /// </summary>
        /// <param name="content">Base64 content of the web resource</param>
        /// <param name="type">Web resource type</param>
        public CodeControl(string content, Enumerations.WebResourceType type)
        {
            InitializeComponent();

            // Converts base64 content to string 
            byte[] b = Convert.FromBase64String(content);
            innerContent = System.Text.Encoding.UTF8.GetString(b);
            originalContent = innerContent;
            innerType = type;
        }

        public void Find(bool replace, IWin32Window owner)
        {
            findForm.StartPosition = FormStartPosition.CenterParent;
            findForm.ShowFor(tecCode, replace, owner);
        }

        #endregion Constructor

        #region Handlers

        private void CodeControl_Load(object sender, EventArgs e)
        {
            try
            {
                tecCode.Text = innerContent;


                switch (innerType)
                {
                    case Enumerations.WebResourceType.Script:
                        {
                            tecCode.SetHighlighting("JavaScript");
                        }
                        break;
                    case Enumerations.WebResourceType.Data:
                        {
                            tecCode.SetHighlighting("XML");
                        }
                        break;
                    case Enumerations.WebResourceType.WebPage:
                        {
                            tecCode.SetHighlighting("HTML");
                        }
                        break;
                    case Enumerations.WebResourceType.Css:
                        {
                            tecCode.SetHighlighting("CSS");
                        }
                        break;
                    case Enumerations.WebResourceType.Xsl:
                        {
                            tecCode.SetHighlighting("HTML");
                        }
                        break;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("An error occured while loading this web resource: " + error.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tecCode_TextChanged(object sender, EventArgs e)
        {
            innerContent = tecCode.Text;
            SendSavedMessage();
        }

        #endregion Handlers

        #region Methods

        public string GetBase64WebResourceContent()
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(tecCode.Text);
         
            return Convert.ToBase64String(bytes);
        }

        public void MinifyJs()
        {
            try
            {
                tecCode.Text = Yahoo.Yui.Compressor.JavaScriptCompressor.Compress(tecCode.Text, false, true, false,
                                                                                  false, 200);
                tecCode.Refresh();
            }
            catch (Exception error)
            {
                MessageBox.Show(ParentForm, "Error while minifying code: " + error.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ReplaceWithNewFile(string filename)
        {
            try
            {
                string newContent = Convert.ToBase64String(File.ReadAllBytes(filename));
                innerContent = new StreamReader(filename).ReadToEnd();

                CodeControl_Load(null, null);

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