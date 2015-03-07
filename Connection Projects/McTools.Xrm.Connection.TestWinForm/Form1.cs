using System;
using System.ComponentModel;
using System.Windows.Forms;
using McTools.Xrm.Connection.WinForms;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace McTools.Xrm.Connection.TestWinForm
{
    public partial class Form1 : Form
    {
        #region Variables

        /// <summary>
        /// Dynamics CRM 2011 organization service
        /// </summary>
        IOrganizationService service;

        /// <summary>
        /// Connection control
        /// </summary>
        CrmConnectionStatusBar ccsb;

        /// <summary>
        /// Connection manager
        /// </summary>
        ConnectionManager cManager;

        private FormHelper formHelper;

        #endregion Variables

        #region Constructor

        public Form1()
        {
            InitializeComponent();

            // Create the connection manager with its events
            this.cManager = ConnectionManager.Instance;
            this.cManager.ConnectionSucceed += new ConnectionManager.ConnectionSucceedEventHandler(cManager_ConnectionSucceed);
            this.cManager.ConnectionFailed += new ConnectionManager.ConnectionFailedEventHandler(cManager_ConnectionFailed);
            this.cManager.StepChanged += new ConnectionManager.StepChangedEventHandler(cManager_StepChanged);
            this.cManager.RequestPassword += new ConnectionManager.RequestPasswordEventHandler(cManager_RequestPassword);
            this.cManager.UseProxy += new ConnectionManager.UseProxyEventHandler(cManager_UseProxy);
            formHelper = new FormHelper(this);

            // Instantiate and add the connection control to the form
            ccsb = new CrmConnectionStatusBar(formHelper);
            this.Controls.Add(ccsb);

            this.ccsb.SetMessage("A message to display...");
        }

        void cManager_UseProxy(object sender, UseProxyEventArgs e)
        {
            formHelper.SelectProxy();
        }

        bool cManager_RequestPassword(object sender, RequestPasswordEventArgs e)
        {
            return formHelper.RequestPassword(e.ConnectionDetail);
        }

        #endregion Constructor

        #region Connection event handlers

        /// <summary>
        /// Occurs when the connection manager sends a step change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cManager_StepChanged(object sender, StepChangedEventArgs e)
        {
            this.ccsb.SetMessage(e.CurrentStep);
        }

        /// <summary>
        /// Occurs when the connection to a server failed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cManager_ConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            // Set error message
            this.ccsb.SetMessage("Error: " + e.FailureReason);
            
            // Clear the current organization service
            this.service = null;
        }

        /// <summary>
        /// Occurs when the connection to a server succeed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cManager_ConnectionSucceed(object sender, ConnectionSucceedEventArgs e)
        {
            ccsb.RebuildConnectionList();

            // Store connection Organization Service
            this.service = e.OrganizationService;
            
            // Displays connection status
            this.ccsb.SetConnectionStatus(true, e.ConnectionDetail);

            // Clear the current action message
            this.ccsb.SetMessage(string.Empty);

            // Do action if needed
            if (e.Parameter != null)
            {
                if (e.Parameter.ToString() == "WhoAmI")
                {
                    WhoAmI();
                }
            }
        }

        #endregion Connection event handlers

        #region WhoAmI Sample methods

        private void btnWhoAmI_Click(object sender, EventArgs e)
        {
            if (this.service == null)
            {
                formHelper.AskForConnection("WhoAmI");
            }
            else
            {
                WhoAmI();
            }
        }

        private void WhoAmI()
        {
            WhoAmIRequest request = new WhoAmIRequest();
            WhoAmIResponse response = (WhoAmIResponse)this.service.Execute(request);

            MessageBox.Show(this, "Your ID is: " + response.UserId.ToString("B"));
        }

        #endregion WhoAmI Sample methods

        private void tsbManageConnections_Click(object sender, EventArgs e)
        {
            formHelper.DisplayConnectionsList(this);
        }
    }
}
