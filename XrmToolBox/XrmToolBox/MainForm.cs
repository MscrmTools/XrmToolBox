// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using McTools.Xrm.Connection;
using McTools.Xrm.Connection.WinForms;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using XrmToolBox.Forms;
using XrmToolBox.UserControls;

namespace XrmToolBox
{
    public sealed partial class MainForm : Form
    {
        #region Variables

        private readonly FormHelper fHelper;

        private readonly ConnectionManager cManager;

        private readonly CrmConnectionStatusBar ccsb;

        private IOrganizationService service;

        private ConnectionDetail currentConnectionDetail;

        private PluginManager pManager;

        #endregion Variables

        #region Constructor

        public MainForm()
        {
            InitializeComponent();

            Hide();
            StartPosition = FormStartPosition.CenterScreen;

            var welcomeWorker = new BackgroundWorker();
            welcomeWorker.DoWork += welcomeWorker_DoWork;
            welcomeWorker.RunWorkerCompleted += welcomeWorker_RunWorkerCompleted;
            welcomeWorker.RunWorkerAsync();
            
            MouseWheel += MainFormMouseWheel;

            Text = string.Format("{0} (v{1})", Text, Assembly.GetExecutingAssembly().GetName().Version);

            cManager = new ConnectionManager();
            cManager.ConnectionFailed += CManagerConnectionFailed;
            cManager.ConnectionSucceed += CManagerConnectionSucceed;
            cManager.RequestPassword += CManagerRequestPassword;
            cManager.StepChanged += CManagerStepChanged;


            fHelper = new FormHelper(this, cManager);

            ccsb = new CrmConnectionStatusBar(cManager, fHelper);

            Controls.Add(ccsb);

            Show();
        }

       
        void welcomeWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.ToString());
            }
        }

        void welcomeWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var welcomeScreen = new WelcomeScreen { StartPosition = FormStartPosition.CenterScreen };
            welcomeScreen.ShowDialog();
        }

        void MainFormMouseWheel(object sender, MouseEventArgs e)
        {
            HomePageTab.Focus();
        }

        #endregion Constructor

        #region Connection methods

        private void CManagerStepChanged(object sender, StepChangedEventArgs e)
        {
            ccsb.SetMessage(e.CurrentStep);
        }

        private bool CManagerRequestPassword(object sender, RequestPasswordEventArgs e)
        {
            return fHelper.RequestPassword(e.ConnectionDetail);
        }

        private void CManagerConnectionSucceed(object sender, ConnectionSucceedEventArgs e)
        {
            currentConnectionDetail = e.ConnectionDetail;
            service = e.OrganizationService;
            ccsb.SetConnectionStatus(true, e.ConnectionDetail);
            ccsb.SetMessage(string.Empty);

            //ApplyConnectionToOpenedPlugins();

            if(e.Parameter != null)
            {
                var control = e.Parameter as UserControl;
                if (control != null)
                {
                    var realUserControl = control;
                    DisplayPluginControl(realUserControl);
                }
                else if(e.Parameter.ToString() == "ApplyConnectionToTabs" && tabControl1.TabPages.Count > 1)
                {
                    ApplyConnectionToTabs();
                }
                else
                {
                    var args = e.Parameter as RequestConnectionEventArgs;
                    if (args != null)
                    {
                        var userControl = (UserControl) args.Control;

                        args.Control.UpdateConnection(e.OrganizationService, args.ActionName);

                        userControl.Parent.Text = string.Format("{0} ({1})",
                                                                userControl.Parent.Text.Split(' ')[0],
                                                                e.ConnectionDetail.ConnectionName);
                    }
                }
            }
        }

        private void CManagerConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            MessageBox.Show(this, e.FailureReason, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            currentConnectionDetail = null;
            service = null;
            ccsb.SetConnectionStatus(false, null);
            ccsb.SetMessage(e.FailureReason);
        }

        #endregion Connection methods

        #region Form events

        private void Form1Load(object sender, EventArgs e)
        {
            pManager = new PluginManager();
            pManager.LoadPlugins();

            DisplayPlugins();
        }

        private void DisplayPlugins()
        {
            var top = 4;
            int lastWidth = HomePageTab.Width - 28;

            HomePageTab.Controls.Clear();

            foreach (var plugin in pManager.Plugins.OrderBy(p => ((AssemblyTitleAttribute)GetAssemblyAttribute(p.Assembly, typeof(AssemblyTitleAttribute))).Title))
            {
                var title = ((AssemblyTitleAttribute)GetAssemblyAttribute(plugin.Assembly, typeof(AssemblyTitleAttribute))).Title;
                var desc = ((AssemblyDescriptionAttribute)GetAssemblyAttribute(plugin.Assembly, typeof(AssemblyDescriptionAttribute))).Description;
                var author = ((AssemblyCompanyAttribute)GetAssemblyAttribute(plugin.Assembly, typeof(AssemblyCompanyAttribute))).Company;
                var version = plugin.Assembly.GetName().Version.ToString();
                var logo = GetLogo(plugin);

                if (tsbView.Tag.ToString() == "1")
                {
                    var pm = new PluginModel(logo, title, desc, author, version)
                    {
                        Left = 4,
                        Top = top,
                        Width = lastWidth,
                        Tag = plugin
                    };

                    pm.Clicked += PluginClicked;
                    HomePageTab.Controls.Add(pm);
                    top += 104;
                }
                else
                {
                    var pm = new SmallPluginModel(logo, title, desc, author, version)
                    {
                        Left = 4,
                        Top = top,
                        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                        Width = HomePageTab.Width - 8,
                        Tag = plugin
                    };

                    pm.Clicked += PluginClicked;
                    HomePageTab.Controls.Add(pm);
                    top += 54;
                }
            }

            foreach (Control ctrl in HomePageTab.Controls)
            {
                ctrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            }
        }

        private void PluginClicked(object sender, EventArgs e)
        {
            var plugin = (UserControl)sender;
            var controlType = (Type)plugin.Tag;
            var pluginControl = (UserControl)PluginManager.CreateInstance(controlType.Assembly.Location, controlType.FullName);

            if (service == null && MessageBox.Show(this, "Do you want to connect to an organization first?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                fHelper.AskForConnection(pluginControl);
            }
            else
            {
                DisplayPluginControl(pluginControl);
            }
        }

        private void TsbConnectClick(object sender, EventArgs e)
        {
            fHelper.AskForConnection("ApplyConnectionToTabs");
        }

        private void TsbAboutClick(object sender, EventArgs e)
        {
            var aForm = new AboutForm { StartPosition = FormStartPosition.CenterParent };
            aForm.ShowDialog();
        }
        
        #endregion Form events

        #region Plugin information retriever

        private object GetAssemblyAttribute(Assembly assembly, Type attributeType)
        {
            return assembly.GetCustomAttributes(attributeType, true)[0];
        }

        private Image GetLogo(Type plugin)
        {
            var pluginControl = (IMsCrmToolsPluginUserControl)PluginManager.CreateInstance(plugin.Assembly.Location, plugin.FullName);

            var logo = pluginControl.PluginLogo;

            if (logo == null)
            {
                var currentAssembly = Assembly.GetExecutingAssembly();
                var logoStream = currentAssembly.GetManifestResourceStream("XrmToolBox.Images.nologo.png");

                if (logoStream == null)
                {
                    throw new Exception("Unable to find no-logo stream!");
                }

                logo = Image.FromStream(logoStream);
            }

            return logo;
        }

        #endregion Plugin information retriever

        private void DisplayPluginControl(UserControl pluginControl)
        {
            if (service != null)
            {
                var clonedService = CloneService(service);
                ((IMsCrmToolsPluginUserControl) pluginControl).UpdateConnection(clonedService);
            }

            ((IMsCrmToolsPluginUserControl)pluginControl).OnRequestConnection += MainForm_OnRequestConnection;
            ((IMsCrmToolsPluginUserControl)pluginControl).OnCloseTool += MainForm_OnCloseTool;
           
            string name = string.Format("{0} ({1})",
                                        ((AssemblyTitleAttribute)GetAssemblyAttribute(pluginControl.GetType().Assembly, typeof(AssemblyTitleAttribute))).Title,
                                        currentConnectionDetail != null
                                            ? currentConnectionDetail.ConnectionName
                                            : "Not connected");

            var newTab = new TabPage(name);
            tabControl1.TabPages.Add(newTab);

            pluginControl.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right;
            pluginControl.Width = newTab.Width;
            pluginControl.Height = newTab.Height;

            newTab.Controls.Add(pluginControl);

            tabControl1.SelectTab(tabControl1.TabPages.Count - 1);
        }

        void MainForm_OnCloseTool(object sender, EventArgs e)
        {
            var sourceControl = (UserControl) sender;
            tabControl1.TabPages.Remove((TabPage) sourceControl.Parent);
            sourceControl.Dispose();
        }

        private void MainForm_OnRequestConnection(object sender, EventArgs e)
        {
            fHelper.AskForConnection(e);
        }
        
        private void ApplyConnectionToTabs()
        {
            var tabs = tabControl1.TabPages.Cast<TabPage>().Where(tab => tab.TabIndex != 0).ToList();

            var tcu = new TabConnectionUpdater(tabs) {StartPosition = FormStartPosition.CenterParent};

            if(tcu.ShowDialog() == DialogResult.OK)
            {
                foreach(TabPage tab in tcu.SelectedTabs)
                {
                    ((IMsCrmToolsPluginUserControl)tab.Controls[0]).UpdateConnection(service);
           
                    tab.Text = string.Format("{0} ({1})",
                                        ((AssemblyTitleAttribute)GetAssemblyAttribute(tab.Controls[0].GetType().Assembly, typeof(AssemblyTitleAttribute))).Title,
                                        currentConnectionDetail != null
                                            ? currentConnectionDetail.ConnectionName
                                            : "Not connected");
                }
            }
        }

        private IOrganizationService CloneService(IOrganizationService orgService)
        {
            var baseService = (OrganizationServiceProxy)orgService;

            var newService =
                new OrganizationServiceProxy(baseService.ServiceConfiguration.CurrentServiceEndpoint.Address.Uri,
                                             baseService.HomeRealmUri,
                                             baseService.ClientCredentials,
                                             baseService.DeviceCredentials);

            return newService;
        }

        private void TsbViewClick(object sender, EventArgs e)
        {
            tsbView.Tag = tsbView.Tag.ToString() == "1" ? "2" : "1";
            DisplayPlugins();
        }

        private void TsbRateClick(object sender, EventArgs e)
        {
            Process.Start("http://xrmtoolbox.codeplex.com/releases");
        }

        private void TsbDiscussClick(object sender, EventArgs e)
        {
            Process.Start("http://xrmtoolbox.codeplex.com/discussions");
        }

        private void TsbReportBugClick(object sender, EventArgs e)
        {
            Process.Start("http://xrmtoolbox.codeplex.com/WorkItem/Create");
        }

        private void TsbDonateClick(object sender, EventArgs e)
        {
            var url = string.Format("https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business={0}&lc={1}&item_name={2}&currency_code={3}&bn=PP%2dDonationsBF",
                "tanguy92@hotmail.com",
                "FR",
                "Donation%20for%20MSCRM%20Tools%20-%20Xrm%20Tool%20Box",
                "EUR");

            Process.Start(url);
        }

        private void CloseAllTabsToolStripMenuItemClick(object sender, EventArgs e)
        {
            for (int i = tabControl1.TabPages.Count - 1; i > 0; i--)
            {
                tabControl1.TabPages.RemoveAt(i);
            }
        }

        private void CloseAllTabsExceptActiveToolStripMenuItemClick(object sender, EventArgs e)
        {
            for (int i = tabControl1.TabPages.Count - 1; i > 0; i--)
            {
                if(tabControl1.SelectedTab.TabIndex != i)
                    tabControl1.TabPages.RemoveAt(i);
            }
        }

       
    }
}
