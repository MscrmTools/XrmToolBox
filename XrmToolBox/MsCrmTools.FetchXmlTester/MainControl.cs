// PROJECT : MsCrmTools.FetchXmlTester
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CSRichTextBoxSyntaxHighlighting;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using XrmToolBox;

namespace MsCrmTools.FetchXmlTester
{
    public partial class MainControl : UserControl, IMsCrmToolsPluginUserControl
    {
        #region Variables

        /// <summary>
        /// Microsoft Dynamics CRM 2011 Organization Service
        /// </summary>
        private IOrganizationService service;

        /// <summary>
        /// Panel used to display progress information
        /// </summary>
        private Panel infoPanel;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class <see cref="MainControl"/>
        /// </summary>
        public MainControl()
        {
            InitializeComponent();

            XMLViewerSettings viewerSetting = new XMLViewerSettings
            {
                AttributeKey = Color.Red,
                AttributeValue = Color.Blue,
                Tag = Color.Blue,
                Element = Color.DarkRed,
                Value = Color.Black,
            };

            //viewer.Settings = viewerSetting;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets the organization service used by the tool
        /// </summary>
        public IOrganizationService Service
        {
            get { return service; }
        }

        /// <summary>
        /// Gets the logo to display in the tools list
        /// </summary>
        public Image PluginLogo
        {
            get { return toolImageList.Images[0]; }
        }

        #endregion

        #region EventHandlers

        /// <summary>
        /// EventHandler to request a connection to an organization
        /// </summary>
        public event EventHandler OnRequestConnection;

        /// <summary>
        /// EventHandler to close the current tool
        /// </summary>
        public event EventHandler OnCloseTool;

        #endregion EventHandlers

        #region Methods

        /// <summary>
        /// Updates the organization service used by the tool
        /// </summary>
        /// <param name="newService">Organization service</param>
        /// <param name="actionName">Action that requested a service update</param>
        /// <param name="parameter">Parameter passed when requesting a service update</param>
        public void UpdateConnection(IOrganizationService newService, string actionName = "", object parameter = null)
        {
            service = newService;

            if (actionName == "Execute")
            {
                ProcessFetchXml();
            }
        }

        private void TsbExecuteClick(object sender, EventArgs e)
        {
            if (txtRequest.Text.Length == 0)
            {
                MessageBox.Show(this, "Please provide a fetchXml query before trying to execute it!", "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (service == null)
            {
                if (OnRequestConnection != null)
                {
                    var args = new RequestConnectionEventArgs { ActionName = "Execute", Control = this, Parameter = null };
                    OnRequestConnection(this, args);
                }
            }
            else
            {
                ProcessFetchXml();
            }
        }

        private void ProcessFetchXml()
        {
            infoPanel = InformationPanel.GetInformationPanel(this, "Executing request...", 340, 100);

            var worker = new BackgroundWorker();
            worker.DoWork += WorkerDoWork;
            worker.RunWorkerCompleted += WorkerRunWorkerCompleted;
            worker.RunWorkerAsync(txtRequest.Text);
        }
    
        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var request = new ExecuteFetchRequest{FetchXml = e.Argument.ToString()};
            var response = (ExecuteFetchResponse)service.Execute(request);

            e.Result = response.FetchXmlResult;
        }

        private void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            infoPanel.Dispose();
            infoPanel.Dispose();
            Controls.Remove(infoPanel);

            if (e.Error == null)
            {
                txtResponse.Text = IndentXMLString(e.Result.ToString());
                tabControl1.SelectedTab = tabPage2;
            }
            else
            {
                MessageBox.Show(this, "An error occured: " + e.Error.Message, "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void TsbCloseClick(object sender, EventArgs e)
        {
            const string message = "Are your sure you want to close this tab?";
            if (MessageBox.Show(message, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                OnCloseTool(this, null);
        }

        private string IndentXMLString(string xml)
        {
            var ms = new MemoryStream();
            var xtw = new XmlTextWriter(ms, Encoding.Unicode);
            var doc = new XmlDocument();

            try
            {
                doc.LoadXml(xml);

                xtw.Formatting = Formatting.Indented;
                doc.WriteContentTo(xtw);
                xtw.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                var sr = new StreamReader(ms);
                return sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        #endregion Methods

        private void ToolStripButton1Click(object sender, EventArgs e)
        {
            try
            {
                if (((XMLViewer)tabControl1.SelectedTab.Controls[0]).Text.Length == 0)
                {
                    MessageBox.Show(this, "Please provide a valid Xml before trying to format it!", "Warning",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ((XMLViewer)tabControl1.SelectedTab.Controls[0]).Process(true);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, "An error occured: " + error.Message, "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

       
    }
}
