// PROJECT : MsCrmTools.SampleTool
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.Windows.Forms;
using XrmToolBox.CustomControls.Dialogs;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;

namespace MsCrmTools.SampleTool
{
    public partial class SampleTool : PluginControlBase, IGitHubPlugin, ICodePlexPlugin, IPayPalPlugin, IHelpPlugin, IStatusBarMessenger, IShortcutReceiver, IAboutPlugin
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
                // MessageBox.Show("Settings found!");

                toolStripLabelMessages.Text = "Settings found!";

            }

            LogInfo("An information message");
            LogWarning("A warning message");
            LogError("An error message");
        }

        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;

        public override void ClosingPlugin(PluginCloseInfo info)
        {
            // MessageBox.Show("Closing tool");
            toolStripLabelMessages.Text = "Closing tool";

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

        private void SampleTool_Load(object sender, EventArgs e)
        {
            ShowInfoNotification("This is a notification that can lead to XrmToolBox repository", new Uri("http://github.com/MscrmTools/XrmToolBox"));
            
            EntCollListViewList.UpdateConnection(Service);
            EntCollListViewFetch.UpdateConnection(Service);
            EntityDropdownControl.UpdateConnection(Service);
            AttribMetadataListView.UpdateConnection(Service);
            EntityCollListViewLoadEntity.UpdateConnection(Service);
            AttribMetadataDropdown.UpdateConnection(Service);

            int width = (int)ClientSize.Width / 2;
            splitterXmlViewer.SplitterDistance =
            splitterXmlViewerControl.SplitterDistance = 
            splitContainer1.SplitterDistance = width;

            xmlViewerFetch.Process();
        }

        #region IAboutPlugin implementation

        public void ShowAboutDialog()
        {
            MessageBox.Show(@"This is a sample plugin", @"About Sample Plugin", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        #endregion IAboutPlugin implementation

        private void btnSolutionPickerDialog_Click(object sender, EventArgs e)
        {
            var dialog = new SolutionPickerDialog(Service)
            {
                DisplayDefaultSolution = true,
                DisplayManagedSolutions = false,
                DialogTitle = "My custom title for solution picker",
                DisplaySearch = true,
                DisplayHeader = true,
                HeaderText = "My custom title"
            };
            dialog.ShowDialog();

            EntCollListViewList.LoadData(dialog.SelectedSolutions);
        }

        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            EntCollListViewList.UpdateConnection(newService);
            EntCollListViewFetch.UpdateConnection(newService);
            EntityDropdownControl.UpdateConnection(newService);
            AttribMetadataListView.UpdateConnection(newService);
            EntityCollListViewLoadEntity.UpdateConnection(newService);
            AttribMetadataDropdown.UpdateConnection(newService);

            base.UpdateConnection(newService, detail, actionName, parameter);
        }

        private void btnLoadFetch_Click(object sender, EventArgs e)
        {
            var fetchXml = xmlViewerFetch.Text;

            EntCollListViewFetch.LoadData(fetchXml);
        }

        private void EntCollListViewFetch_LoadDataComplete(object sender, EventArgs e)
        {
            var entitites = EntCollListViewFetch.AllEntities;

            if (entitites.Count > 0) {
                crmGridView1.DataSource = new EntityCollection(EntCollListViewFetch.AllEntities) {
                    EntityName = entitites[0].LogicalName
                };
            }
        }
        private void buttonReloadEntities_Click(object sender, EventArgs e)
        {
            EntityDropdownControl.LoadData();
        }

        private void EntityDropdownControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // load the list of attributes
            if (EntityDropdownControl.SelectedEntity != null)
            {
                AttribMetadataListView.ParentEntity = EntityDropdownControl.SelectedEntity;
                AttribMetadataListView.LoadData();

                AttribMetadataDropdown.ParentEntity = EntityDropdownControl.SelectedEntity;
                AttribMetadataDropdown.LoadData();
            }
        }

        private void buttonExecQuery_Click(object sender, EventArgs e)
        {
            var ent = EntityDropdownControl.SelectedEntity;
            var attribs = AttribMetadataListView.CheckedAttributes;

            EntityCollListViewLoadEntity.Columns.Clear();
            EntityCollListViewLoadEntity.LoadData(ent.LogicalName, attribs.ConvertAll(a => a.LogicalName), (int)numericUpDown1.Value);
        }
    }
}