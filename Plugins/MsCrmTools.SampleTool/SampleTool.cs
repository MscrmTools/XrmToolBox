// PROJECT : MsCrmTools.SampleTool
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Windows.Forms;
using Microsoft.Crm.Sdk.Messages;
using XrmToolBox;

namespace MsCrmTools.SampleTool
{
    public partial class SampleTool : PluginBase, IGitHubPlugin, ICodePlexPlugin, IPayPalPlugin
    {
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

                },
                "Retrieving your user id...",
                340,
                150);
        }

        private void BtnCloseClick(object sender, EventArgs e)
        {
            CloseTool();
        }

        public string UserName
        {
            get { return "GithubUserName"; }
        }

        public string RepositoryName
        {
            get { return "GithubRepositoryName"; }
        }

        public string CodePlexUrlName
        {
            get { return "CodePlex"; }
        }

        public string EmailAccount
        {
            get { return "paypal@paypal.com"; }
        }

        public string DonationDescription
        {
            get { return "paypal description"; }
        }
    }
}
