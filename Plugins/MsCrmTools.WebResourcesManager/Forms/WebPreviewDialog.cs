// PROJECT : MsCrmTools.WebResourcesManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Windows.Forms;

namespace MsCrmTools.WebResourcesManager.Forms
{
    public partial class WebPreviewDialog : Form
    {
        private readonly string _webResourceContent;

        public WebPreviewDialog(string webResourceContent)
        {
            InitializeComponent();

            _webResourceContent = webResourceContent;
        }

        private void WebPreviewDialogLoad(object sender, EventArgs e)
        {
            byte[] b = Convert.FromBase64String(_webResourceContent);
            string innerContent = System.Text.Encoding.UTF8.GetString(b);

            wBrowser.DocumentText = innerContent;
            wBrowser.ScriptErrorsSuppressed = true;
        }
    }
}