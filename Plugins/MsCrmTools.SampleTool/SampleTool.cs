// PROJECT : MsCrmTools.SampleTool
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Crm.Sdk.Messages;
using System;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;

namespace MsCrmTools.SampleTool
{
    public partial class SampleTool : PluginControlBase, IGitHubPlugin, ICodePlexPlugin, IPayPalPlugin, IHelpPlugin, IStatusBarMessenger, IShortcutReceiver, IAboutPlugin, IDuplicatableTool, ISettingsPlugin, IPrivatePlugin, IMessageBusHost
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

        public override void ClosingPlugin(PluginCloseInfo info)
        {
            MessageBox.Show("Closing tool");

            base.ClosingPlugin(info);
        }

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
                        w.ReportProgress(10, "Un message...");
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
                        SendMessageToStatusBar(this, new StatusBarMessageEventArgs(e.ProgressPercentage, e.UserState.ToString()));
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

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            CancelWorker();
            tsbCancel.Enabled = false;
            MessageBox.Show("Cancelled");
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void tsbWhoAmI_Click(object sender, EventArgs e)
        {
            HideNotification();

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

        #region Shortcut Receiver implementation

        public void ReceiveKeyDownShortcut(KeyEventArgs e)
        {
            //   MessageBox.Show("A KeyDown event was received!");
        }

        public void ReceiveKeyPressShortcut(KeyPressEventArgs e)
        {
            // MessageBox.Show("A KeyPress event was received!");
        }

        public void ReceiveKeyUpShortcut(KeyEventArgs e)
        {
            //MessageBox.Show("A KeyUp event was received!");
        }

        public void ReceivePreviewKeyDownShortcut(PreviewKeyDownEventArgs e)
        {
            //MessageBox.Show("A PreviewKeyDown event was received!");
        }

        public void ReceiveShortcut(KeyEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        #endregion Shortcut Receiver implementation

        #region IDuplicatableTool implementation

        public event EventHandler<DuplicateToolArgs> DuplicateRequested;

        public void ApplyState(object state)
        {
            txtState.Text = state.ToString();
        }

        public object GetState()
        {
            return txtState.Text;
        }

        private void tsbDuplicate_Click(object sender, EventArgs e)
        {
            DuplicateRequested?.Invoke(this, new DuplicateToolArgs("My custom state", true));
        }

        #endregion IDuplicatableTool implementation

        #region ISettingsPlugin implementation

        public void ShowSettings()
        {
            MessageBox.Show(@"Settings should be displayed instead of this dialog");
        }

        #endregion ISettingsPlugin implementation

        #region IAboutPlugin implementation

        public void ShowAboutDialog()
        {
            MessageBox.Show(@"This is a sample tool", @"About Sample Tool", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        #endregion IAboutPlugin implementation

        private void btnCheckMultiSample_Click(object sender, EventArgs e)
        {
            var expectedPlugin = PluginManagerExtended.Instance.Plugins.FirstOrDefault(p =>
                p.Value is PluginBase pb && pb.GetId() == Guid.Parse("{64A4E4E3-CAF9-4896-983A-341A297DEAF3}")
            );

            MessageBox.Show(expectedPlugin == null ? "Tool is not installed" : "Tool is installed");
        }

        private void SampleTool_Load(object sender, EventArgs e)
        {
            ShowInfoNotification("This is a notification that can lead to XrmToolBox repository", new Uri("http://github.com/MscrmTools/XrmToolBox"));
        }

        #region IMessageBusHost

        public event EventHandler<MessageBusEventArgs> OnOutgoingMessage;

        public void OnIncomingMessage(MessageBusEventArgs message)
        {
            throw new NotImplementedException();
        }

        private void SendMessage(string message)
        {
            OnOutgoingMessage?.Invoke(this, new MessageBusEventArgs("targetPlugin", false));
        }

        #endregion IMessageBusHost
    }
}