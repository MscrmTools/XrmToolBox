// PROJECT : MsCrmTools.SampleTool
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using Microsoft.Crm.Sdk.Messages;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace MsCrmTools.SampleTool
{
   public partial class SampleTool : PluginControlBase, IGitHubPlugin, ICodePlexPlugin, IPayPalPlugin
    {
        #region Base tool implementation

        public SampleTool()
        {
            InitializeComponent();
        }

        private void BtnWhoAmIClick(object sender, EventArgs e)
        {
            ExecuteMethod(ProcessWhoAmI);
        }

        public void ProcessWhoAmI()
        {
            WorkAsync(null, (w, e) =>
            {
                var request = new WhoAmIRequest();
                var response = (WhoAmIResponse) Service.Execute(request);

                e.Result = response.UserId;
            },
                e =>
                {
                    MessageBox.Show(string.Format("You are {0}", (Guid) e.Result));
                },
                e =>
                {
                    // If progress has to be notified to user, use the following method:
                    //SetWorkingMessage("Message to display");
                },
                "Retrieving your user id...",
                340,
                150);
        }

        private void BtnCloseClick(object sender, EventArgs e)
        {
            CloseTool();
        }

        #endregion Base tool implementation

        #region Github implementation

        public string UserName
        {
            get { return "GithubUserName"; }
        }

        public string RepositoryName
        {
            get { return "GithubRepositoryName"; }
        }

        #endregion Github implementation

        #region CodePlex implementation

        public string CodePlexUrlName
        {
            get { return "CodePlex"; }
        }

        #endregion CodePlex implementation

        #region PayPal implementation
        
        public string EmailAccount
        {
            get { return "paypal@paypal.com"; }
        }

        public string DonationDescription
        {
            get { return "paypal description"; }
        }

        #endregion PayPal implementation
    }
}
