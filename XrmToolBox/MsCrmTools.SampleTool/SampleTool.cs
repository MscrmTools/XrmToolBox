// PROJECT : MsCrmTools.SampleTool
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata.Query;
using Microsoft.Xrm.Sdk.Query;
using XrmToolBox;

namespace MsCrmTools.SampleTool
{
    public partial class SampleTool : UserControl, IMsCrmToolsPluginUserControl
    {
        private IOrganizationService service;

        private Panel infoPanel;

        public SampleTool()
        {
            InitializeComponent();
        }

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
            get { return null; }
        }

        /// <summary>
        /// EventHandler to request a connection to an organization
        /// </summary>
        public event EventHandler OnRequestConnection;

        /// <summary>
        /// EventHandler to close the current tool
        /// </summary>
        public event EventHandler OnCloseTool;

        /// <summary>
        /// Updates the organization service used by the tool
        /// </summary>
        /// <param name="newService">Organization service</param>
        /// <param name="actionName">Action that requested a service update</param>
        /// <param name="parameter">Parameter passed when requesting a service update</param>
        public void UpdateConnection(IOrganizationService newService, string actionName = "", object parameter = null)
        {
            service = newService;

            if (actionName == "WhoAmI")
            {
                ProcessWhoAmI();
            }
        }

        private void BtnWhoAmIClick(object sender, EventArgs e)
        {
            if (service == null)
            {
                if (OnRequestConnection != null)
                {
                    var args = new RequestConnectionEventArgs {ActionName = "WhoAmI", Control = this};
                    OnRequestConnection(this, args);
                }
            }
            else
            {
                ProcessWhoAmI();
            }
        }

        private void ProcessWhoAmI()
        {
            infoPanel = InformationPanel.GetInformationPanel(this, "Retrieving your user id...", 340, 100);

            var worker = new BackgroundWorker();
            worker.DoWork += WorkerDoWork;
            worker.RunWorkerCompleted += WorkerRunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var request = new WhoAmIRequest();
            var response = (WhoAmIResponse)service.Execute(request);

            e.Result = response.UserId;
        }

        void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            infoPanel.Dispose();
            infoPanel.Dispose();
            Controls.Remove(infoPanel);

            MessageBox.Show(string.Format("You are {0}", (Guid)e.Result));
        }

        private void BtnCloseClick(object sender, EventArgs e)
        {
            if (OnCloseTool != null)
            {
                OnCloseTool(this, null);
            }
        }
    }
}
