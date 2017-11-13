// PROJECT : MsCrmTools.SampleTool
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Forms;
using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;

namespace MsCrmTools.SampleTool
{
    public partial class SampleToolMulti : MultipleConnectionsPluginControlBase, IGitHubPlugin, ICodePlexPlugin, IPayPalPlugin, IHelpPlugin, IStatusBarMessenger, IShortcutReceiver
    {
        #region Base tool implementation

        public SampleToolMulti()
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
            tsbCancel.Enabled = true;

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Retrieving your user id...",
                Work = (w, e) =>
                {
                    var list = new Dictionary<ConnectionDetail, Guid>();

                    // Process initial connection
                    var request = new WhoAmIRequest();
                    var response = (WhoAmIResponse)Service.Execute(request);

                    list.Add(ConnectionDetail, response.UserId);

                    // Process additional connections
                    foreach (var detail in AdditionalConnectionDetails)
                    {
                        if (w.CancellationPending)
                        {
                            e.Cancel = true;
                        }

                        w.ReportProgress(0, $"Processing organization {detail.OrganizationFriendlyName}...");

                        request = new WhoAmIRequest();
                        response = (WhoAmIResponse)detail.GetCrmServiceClient().Execute(request);

                        list.Add(detail, response.UserId);
                        e.Result = list;
                    }
                },
                ProgressChanged = e =>
                {
                    // If progress has to be notified to user, use the following method:
                    //SetWorkingMessage("Message to display");

                    // If progress has to be notified to user, through the
                    // status bar, use the following method
                    SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(e.UserState.ToString()));
                },
                PostWorkCallBack = e =>
                {
                    SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(""));
                    tsbCancel.Enabled = false;

                    var list = (Dictionary<ConnectionDetail, Guid>)e.Result;
                    foreach (ListViewItem item in listView1.Items)
                    {
                        item.SubItems.Add(list[(ConnectionDetail)item.Tag].ToString("B"));
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

        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            // Let base class handle update of connection
            base.UpdateConnection(newService, detail, actionName, parameter);

            // Change of primary connection
            if (string.IsNullOrEmpty(actionName))
            {
            }
            else
            {
                // or change of secondary connection
            }
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

        #region Shortcut Receiver implementation

        public void ReceiveShortcut(KeyEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        public void ReceiveKeyDownShortcut(KeyEventArgs e)
        {
            MessageBox.Show("A KeyDown event was received!");
        }

        public void ReceiveKeyPressShortcut(KeyPressEventArgs e)
        {
            MessageBox.Show("A KeyPress event was received!");
        }

        public void ReceiveKeyUpShortcut(KeyEventArgs e)
        {
            MessageBox.Show("A KeyUp event was received!");
        }

        public void ReceivePreviewKeyDownShortcut(PreviewKeyDownEventArgs e)
        {
            MessageBox.Show("A PreviewKeyDown event was received!");
        }

        #endregion Shortcut Receiver implementation

        private void SampleTool_Load(object sender, EventArgs e)
        {
            ShowInfoNotification("This is a notification that can lead to XrmToolBox repository", new Uri("http://github.com/MscrmTools/XrmToolBox"));
        }

        private void tsbAddTargetOrg_Click(object sender, EventArgs e)
        {
            AddAdditionalOrganization();
        }

        protected override void ConnectionDetailsUpdated(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var detail in e.NewItems.Cast<ConnectionDetail>())
                {
                    if (listView1.Items.Cast<ListViewItem>().Any(i => i.Tag == detail))
                    {
                        continue;
                    }

                    var item = new ListViewItem(detail.OrganizationFriendlyName) { Tag = detail };
                    listView1.Items.Add(item);
                }
            }
        }
    }
}