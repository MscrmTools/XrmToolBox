// PROJECT : MsCrmTools.SampleTool
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Crm.Sdk.Messages;
using System;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;

namespace MsCrmTools.SampleTool
{
    public partial class SampleTool : PluginControlBase, IGitHubPlugin, ICodePlexPlugin, IPayPalPlugin, IHelpPlugin, IStatusBarMessager
    {
        #region Base tool implementation

        public SampleTool()
        {
            InitializeComponent();
        }

        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;

        public void ProcessWhoAmI()
        {
            WorkAsync("Retrieving your user id...", (w, e) =>
            {
                while (e.Cancel == false)
                {
                    if (w.CancellationPending)
                    {
                        e.Cancel = true;
                    }
                    var request = new WhoAmIRequest();
                    var response = (WhoAmIResponse)Service.Execute(request);

                    e.Result = response.UserId;
                }
            },
                e =>
                {
                    if (!e.Cancelled)
                    {
                        MessageBox.Show(string.Format("You are {0}", (Guid)e.Result));
                    }
                },
                e =>
                {
                    // If progress has to be notified to user, use the following method:
                    //SetWorkingMessage("Message to display");

                    // If progress has to be notified to user, through the
                    // status bar, use the following method
                    //SendMessageToStatusBar(this, new StatusBarMessageEventArgs(50, "progress at 50%"));
                },
                null,
                true,
                340,
                150);
        }

        private void BtnCloseClick(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void BtnWhoAmIClick(object sender, EventArgs e)
        {
            ExecuteMethod(ProcessWhoAmI);
        }

        #endregion Base tool implementation

        #region Github implementation

        public string RepositoryName
        {
            get { return "GithubRepositoryName"; }
        }

        public string UserName
        {
            get { return "GithubUserName"; }
        }

        #endregion Github implementation

        #region CodePlex implementation

        public string CodePlexUrlName
        {
            get { return "CodePlex"; }
        }

        #endregion CodePlex implementation

        #region PayPal implementation

        public string DonationDescription
        {
            get { return "paypal description"; }
        }

        public string EmailAccount
        {
            get { return "paypal@paypal.com"; }
        }

        #endregion PayPal implementation

        #region Help implementation

        public string HelpUrl
        {
            get { return "http://www.google.com"; }
        }

        #endregion Help implementation

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelWorker();

            MessageBox.Show("Cancelled");
        }
    }
}