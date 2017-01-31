// PROJECT : MsCrmTools.SampleTool
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Windows.Forms;
using Microsoft.Crm.Sdk.Messages;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;

namespace MsCrmTools.SampleTool
{
    public partial class SampleTool : PluginControlBase, IGitHubPlugin, ICodePlexPlugin, IPayPalPlugin, IHelpPlugin, IStatusBarMessenger
    {
        #region Base tool implementation

        public SampleTool()
        {
            InitializeComponent();

            var settings = new Settings
            {
                Var1 = "settings string",
                Var2 = true
            };
            SettingsManager.Instance.Save(typeof(SampleTool), settings);

            Settings settings2;

            if (SettingsManager.Instance.TryLoad(typeof(SampleTool), out settings2))
            {
                MessageBox.Show("Settings found!");
            }

            LogInfo("An information message");
            LogWarning("A warning message");
            LogError("An error message");

      
        }

        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;

        public void ProcessWhoAmI()
        {
            bool isMultipleCallChecked = cbMultipleCalls.Checked;
            tsbCancel.Enabled = true;

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Retrieving your user id...",
                Work = (w, e) =>
                {
                    // If option multiple calls is selected,
                    // the while loop is just here to illustrate the possibility to cancel
                    // a long running process made of multiple calls
                    do
                    {
                        if (w.CancellationPending)
                        {
                            e.Cancel = true;
                        }
                        var request = new WhoAmIRequest();
                        var response = (WhoAmIResponse)Service.Execute(request);

                        e.Result = response.UserId;
                    } while ((e.Cancel == false) && (isMultipleCallChecked));
                },
                ProgressChanged = e =>
                {
                    // If progress has to be notified to user, use the following method:
                    //SetWorkingMessage("Message to display");

                    // If progress has to be notified to user, through the
                    // status bar, use the following method
                    if (SendMessageToStatusBar != null)
                        SendMessageToStatusBar(this, new StatusBarMessageEventArgs(50, "progress at 50%"));
                },
                PostWorkCallBack = e =>
                {
                    tsbCancel.Enabled = false;
                    if (!e.Cancelled)
                    {
                        var logger = new LogManager(typeof(SampleTool), ConnectionDetail);
                        logger.LogInfo("You are {0}", (Guid)e.Result);


                        MessageBox.Show(string.Format("You are {0}", (Guid)e.Result));
                    }
                },
                AsyncArgument = null,
                IsCancelable = true,
                MessageWidth = 340,
                MessageHeight = 150
            });

        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        public override void ClosingPlugin(PluginCloseInfo info)
        {
            //MessageBox.Show("Custom logic here");

            base.ClosingPlugin(info);
        }

        private void tsbWhoAmI_Click(object sender, EventArgs e)
        {
            HideNotification();
          
            ExecuteMethod(ProcessWhoAmI);
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            CancelWorker();
            tsbCancel.Enabled = false;
            MessageBox.Show("Cancelled");
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

        private void SampleTool_Load(object sender, EventArgs e)
        {
            ShowInfoNotification("This is a notification that can lead to XrmToolBox reposiotry", new Uri("http://github.com/MscrmTools/XrmToolBox"));
        }
    }
}