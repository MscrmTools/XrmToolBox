﻿// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using McTools.Xrm.Connection;
using McTools.Xrm.Connection.WinForms;
using Microsoft.Xrm.Client;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using XrmToolBox.AppCode;
using XrmToolBox.Attributes;
using XrmToolBox.Forms;
using XrmToolBox.UserControls;

namespace XrmToolBox
{
    public sealed partial class MainForm : Form
    {
        #region Variables

        private FormHelper fHelper;

        private ConnectionManager cManager;

        private CrmConnectionStatusBar ccsb;

        private IOrganizationService service;

        private ConnectionDetail currentConnectionDetail;

        private PluginManager pManager;

        private Options currentOptions;

        private string currentReleaseNote;

        private Panel infoPanel;

        #endregion Variables

        #region Constructor

        public MainForm()
        {
            InitializeComponent();
            MouseWheel += (sender, e) => HomePageTab.Focus();

            currentOptions = Options.Load();
            Text = string.Format("{0} (v{1})", Text, Assembly.GetExecutingAssembly().GetName().Version);

            Hide();
            LaunchWelcomeMessage();
            ManageConnectionControl();
            Show();
            CheckForNewVersion();
        }

        #endregion Constructor

        #region Initialization methods

        private void LaunchWelcomeMessage()
        {
            var welcomeWorker = new BackgroundWorker();
            welcomeWorker.DoWork += (sender, e) =>
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                var blackScreen = new WelcomeDialog(version) { StartPosition = FormStartPosition.CenterScreen };
                blackScreen.ShowDialog();
            };
            welcomeWorker.RunWorkerCompleted += (sender, e) =>
            {
                if (e.Error != null)
                {
                    MessageBox.Show(e.Error.ToString());
                }
            };
            welcomeWorker.RunWorkerAsync();
        }

        private void ManageConnectionControl()
        {
            cManager = new ConnectionManager();
            cManager.RequestPassword += (sender, e) => fHelper.RequestPassword(e.ConnectionDetail);
            cManager.StepChanged += (sender, e) => ccsb.SetMessage(e.CurrentStep);
            cManager.ConnectionSucceed += (sender, e) =>
            {
                Controls.Remove(infoPanel);
                if (infoPanel != null) infoPanel.Dispose();

                currentConnectionDetail = e.ConnectionDetail;
                service = e.OrganizationService;
                ccsb.SetConnectionStatus(true, e.ConnectionDetail);
                ccsb.SetMessage(string.Empty);

                if (e.Parameter != null)
                {
                    var control = e.Parameter as UserControl;
                    if (control != null)
                    {
                        var realUserControl = control;
                        DisplayPluginControl(realUserControl);
                    }
                    else if (e.Parameter.ToString() == "ApplyConnectionToTabs" && tabControl1.TabPages.Count > 1)
                    {
                        ApplyConnectionToTabs();
                    }
                    else
                    {
                        var args = e.Parameter as RequestConnectionEventArgs;
                        if (args != null)
                        {
                            var userControl = (UserControl)args.Control;

                            args.Control.UpdateConnection(e.OrganizationService, currentConnectionDetail, args.ActionName, args.Parameter);

                            userControl.Parent.Text = string.Format("{0} ({1})",
                                userControl.Parent.Text.Split(' ')[0],
                                e.ConnectionDetail.ConnectionName);
                        }
                    }
                }
                else if (tabControl1.TabPages.Count > 1)
                {
                    ApplyConnectionToTabs();
                }
            };
            cManager.ConnectionFailed += (sender, e) =>
            {
                Controls.Remove(infoPanel);
                if (infoPanel != null) infoPanel.Dispose();

                MessageBox.Show(this, e.FailureReason, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                currentConnectionDetail = null;
                service = null;
                ccsb.SetConnectionStatus(false, null);
                ccsb.SetMessage(e.FailureReason);
            };
            fHelper = new FormHelper(this, cManager);
            ccsb = new CrmConnectionStatusBar(cManager, fHelper) { Dock = DockStyle.Bottom };
            Controls.Add(ccsb);
        }

        private void CheckForNewVersion()
        {
            var currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            var cvc = new CodeplexVersionChecker(currentVersion);
            cvc.OnCodePlexInforRetrieved += (sender, e) =>
            {
                var info = (CodePlexInfoRetrievedEventArgs)e;
                currentReleaseNote = info.Information.Rate;
                toolStrip1.Items.Insert(10, new ToolStripRateControl(new RateControl(currentReleaseNote)));

                if (!string.IsNullOrEmpty(info.Information.Version))
                {
                    if (currentOptions.LastUpdateCheck.Date != DateTime.Now.Date)
                    {
                        var nvForm = new NewVersionForm(currentVersion, info.Information.Version, info.Information.Description);
                        nvForm.ShowDialog(this);
                    }
                }
            };

            cvc.Run();
            currentOptions.LastUpdateCheck = DateTime.Now;
            currentOptions.Save();
        }

        #endregion Initialization methods

        #region Form events

        private void Form1Load(object sender, EventArgs e)
        {
            pManager = new PluginManager();
            pManager.LoadPlugins();

            DisplayPlugins();
        }

        private void DisplayPlugins()
        {
            if (pManager.Plugins.Count == 0)
            {
                pnlHelp.Visible = true;
                return;
            }
            
            var top = 4;
            int lastWidth = HomePageTab.Width - 28;

            HomePageTab.Controls.Clear();

            if (currentOptions.DisplayMostUsedFirst)
            {
                foreach (var item in currentOptions.MostUsedList.OrderByDescending(i => i.Count).ThenBy(i=>i.Name))
                {
                    var plugin = pManager.Plugins.FirstOrDefault(x => x.FullName == item.Name);
                    if (plugin != null && (currentOptions.HiddenPlugins == null || !currentOptions.HiddenPlugins.Contains(((AssemblyTitleAttribute)GetAssemblyAttribute(plugin.Assembly, typeof(AssemblyTitleAttribute))).Title)))
                    {
                        DisplayOnePlugin(plugin, ref top, lastWidth, item.Count);
                    }
                }

                foreach (var plugin in pManager.Plugins.OrderBy(p => ((AssemblyTitleAttribute)GetAssemblyAttribute(p.Assembly, typeof(AssemblyTitleAttribute))).Title))
                {
                    if (currentOptions.MostUsedList.All(i => i.Name != plugin.FullName) && (currentOptions.HiddenPlugins == null || !currentOptions.HiddenPlugins.Contains(((AssemblyTitleAttribute)GetAssemblyAttribute(plugin.Assembly, typeof(AssemblyTitleAttribute))).Title)))
                    {
                        DisplayOnePlugin(plugin, ref top, lastWidth);
                    }
                }
            }
            else
            {
                foreach (var plugin in pManager.Plugins.OrderBy(p => ((AssemblyTitleAttribute)GetAssemblyAttribute(p.Assembly, typeof(AssemblyTitleAttribute))).Title))
                {
                    if (currentOptions.HiddenPlugins == null || !currentOptions.HiddenPlugins.Contains(((AssemblyTitleAttribute)GetAssemblyAttribute(plugin.Assembly, typeof (AssemblyTitleAttribute))).Title))
                    {
                        DisplayOnePlugin(plugin, ref top, lastWidth);
                    }
                }
            }

            foreach (Control ctrl in HomePageTab.Controls)
            {
                ctrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            }
        }

        private Image GetImage(Type plugin, bool small = false)
        {
            // Default logo (no-logo)
            var thisAssembly = Assembly.GetExecutingAssembly();
            var logoStream = thisAssembly.GetManifestResourceStream(small ? "XrmToolBox.Images.nologo32.png" : "XrmToolBox.Images.nologo.png");
            if (logoStream == null)
            {
                throw new Exception("Unable to find no-logo stream!");
            }

            var logo = Image.FromStream(logoStream);
            
            // Old method
            var pluginControl = (IMsCrmToolsPluginUserControl)PluginManager.CreateInstance(plugin.Assembly.Location, plugin.FullName);
            if(pluginControl.PluginLogo != null)
            logo = pluginControl.PluginLogo;

            // Replace by new method if available
            var b64 = AssemblyAttributeHelper.GetStringAttributeValue(plugin.Assembly, small ? "SmallBase64Image" : "BigBase64Image");
            if (b64.Length > 0)
            {
                var bytes = Convert.FromBase64String(b64);
                var ms = new MemoryStream(bytes, 0, bytes.Length);
                ms.Write(bytes, 0, bytes.Length);
                logo = Image.FromStream(ms);
                ms.Close();
            }

            return logo;
        }

        private void DisplayOnePlugin(Type plugin, ref int top, int width, int count = -1)
        {

            var title = ((AssemblyTitleAttribute)GetAssemblyAttribute(plugin.Assembly, typeof(AssemblyTitleAttribute))).Title;
            var desc = ((AssemblyDescriptionAttribute)GetAssemblyAttribute(plugin.Assembly, typeof(AssemblyDescriptionAttribute))).Description;
            var author = ((AssemblyCompanyAttribute)GetAssemblyAttribute(plugin.Assembly, typeof(AssemblyCompanyAttribute))).Company;
            var version = plugin.Assembly.GetName().Version.ToString();

            var backColor = AssemblyAttributeHelper.GetColor(plugin.Assembly, typeof(BackgroundColorAttribute));
            var primaryColor = AssemblyAttributeHelper.GetColor(plugin.Assembly, typeof(PrimaryFontColorAttribute));
            var secondaryColor = AssemblyAttributeHelper.GetColor(plugin.Assembly, typeof(SecondaryFontColorAttribute));

            if (currentOptions.DisplayLargeIcons)
            {
                var pm = new PluginModel(GetImage(plugin), title, desc, author, version, backColor, primaryColor, count)
                {
                    Left = 4,
                    Top = top,
                    Width = width,
                    Tag = plugin,
                };

                pm.Clicked += PluginClicked;
                HomePageTab.Controls.Add(pm);
                top += 104;
            }
            else
            {
                var pm = new SmallPluginModel(GetImage(plugin, true), title, desc, author, version, backColor, primaryColor, secondaryColor, count)
                {
                    Left = 4,
                    Top = top,
                    Width = width,
                    Tag = plugin,
                };

                pm.Clicked += PluginClicked;
                HomePageTab.Controls.Add(pm);
                top += 54;
            }
        }

        private void PluginClicked(object sender, EventArgs e)
        {
           
            if (service == null && MessageBox.Show(this, "Do you want to connect to an organization first?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if(fHelper.AskForConnection(sender))
                    infoPanel = InformationPanel.GetInformationPanel(this, "Connecting...", 340, 120);
            }
            else
            {
                DisplayPluginControl((UserControl)sender);
            }
        }

        private void TsbConnectClick(object sender, EventArgs e)
        {
            if (fHelper.AskForConnection("ApplyConnectionToTabs"))
            {
                infoPanel = InformationPanel.GetInformationPanel(this, "Connecting...", 340, 120);
            }
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
        
        #endregion Plugin information retriever

        private void DisplayPluginControl(UserControl plugin)
        {
            try
            {
                var controlType = (Type) plugin.Tag;
                var pluginControl =
                    (UserControl) PluginManager.CreateInstance(controlType.Assembly.Location, controlType.FullName);

                if (service != null)
                {
                    var clonedService = new OrganizationService(CrmConnection.Parse(currentConnectionDetail.GetOrganizationCrmConnectionString()));
                    ((OrganizationServiceProxy)clonedService.InnerService).SdkClientVersion = currentConnectionDetail.OrganizationVersion;

                    ((IMsCrmToolsPluginUserControl) pluginControl).UpdateConnection(clonedService,
                        currentConnectionDetail);
                }

                ((IMsCrmToolsPluginUserControl) pluginControl).OnRequestConnection += MainForm_OnRequestConnection;
                ((IMsCrmToolsPluginUserControl) pluginControl).OnCloseTool += MainForm_OnCloseTool;

                string name = string.Format("{0} ({1})",
                    ((AssemblyTitleAttribute)
                        GetAssemblyAttribute(pluginControl.GetType().Assembly, typeof (AssemblyTitleAttribute))).Title,
                    currentConnectionDetail != null
                        ? currentConnectionDetail.ConnectionName
                        : "Not connected");

                var newTab = new TabPage(name);
                tabControl1.TabPages.Add(newTab);

                pluginControl.Dock = DockStyle.Fill;
                pluginControl.Width = newTab.Width;
                pluginControl.Height = newTab.Height;

                newTab.Controls.Add(pluginControl);

                tabControl1.SelectTab(tabControl1.TabPages.Count - 1);

                var pluginInOption =
                    currentOptions.MostUsedList.FirstOrDefault(i => i.Name == pluginControl.GetType().FullName);
                if (pluginInOption == null)
                {
                    pluginInOption = new PluginUseCount {Name = pluginControl.GetType().FullName, Count = 0};
                    currentOptions.MostUsedList.Add(pluginInOption);
                }

                pluginInOption.Count++;

                var p1 = plugin as SmallPluginModel;
                if (p1 != null)
                    p1.UpdateCount(pluginInOption.Count);
                else
                {
                    var p2 = plugin as PluginModel;
                    if (p2 != null)
                    {
                        p2.UpdateCount(pluginInOption.Count);
                    }
                }

                if (currentOptions.LastAdvertisementDisplay == new DateTime() ||
                    currentOptions.LastAdvertisementDisplay > DateTime.Now ||
                    currentOptions.LastAdvertisementDisplay.AddDays(7) < DateTime.Now)
                {
                    bool displayAdvertisement = true;
                    try
                    {
                        var assembly =
                            Assembly.LoadFile(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory +
                                              "\\McTools.StopAdvertisement.dll");
                        if (assembly != null)
                        {
                            Type type = assembly.GetType("McTools.StopAdvertisement.LicenseManager");
                            if (type != null)
                            {
                                MethodInfo methodInfo = type.GetMethod("IsValid");
                                if (methodInfo != null)
                                {
                                    object classInstance = Activator.CreateInstance(type, null);

                                    if ((bool) methodInfo.Invoke(classInstance, null))
                                    {
                                        displayAdvertisement = false;
                                    }
                                }
                            }
                        }
                    }
                    catch (FileNotFoundException)
                    {
                    }

                    if (displayAdvertisement)
                    {
                        var sc = new SupportScreen(currentReleaseNote);
                        sc.ShowDialog(this);
                        currentOptions.LastAdvertisementDisplay = DateTime.Now;
                    }
                }

                currentOptions.Save();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, "An error occured when trying to display this plugin: " + error.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void MainForm_OnCloseTool(object sender, EventArgs e)
        {
            RequestCloseTab((TabPage)((UserControl)sender).Parent, new PluginCloseInfo(ToolBoxCloseReason.PluginRequest));
        }

        private void MainForm_OnRequestConnection(object sender, EventArgs e)
        {
            if (fHelper.AskForConnection(e))
            {
                infoPanel = InformationPanel.GetInformationPanel(this, "Connecting...", 340, 120);
            }
        }

        private void ApplyConnectionToTabs()
        {
            var tabs = tabControl1.TabPages.Cast<TabPage>().Where(tab => tab.TabIndex != 0).ToList();

            var tcu = new TabConnectionUpdater(tabs) { StartPosition = FormStartPosition.CenterParent };

            if (tcu.ShowDialog() == DialogResult.OK)
            {
                foreach (TabPage tab in tcu.SelectedTabs)
                {
                    tab.GetPlugin().UpdateConnection(service, currentConnectionDetail);

                    tab.Text = string.Format("{0} ({1})",
                                        ((AssemblyTitleAttribute)GetAssemblyAttribute(tab.Controls[0].GetType().Assembly, typeof(AssemblyTitleAttribute))).Title,
                                        currentConnectionDetail != null
                                            ? currentConnectionDetail.ConnectionName
                                            : "Not connected");
                }
            }
        }

        #region Close Tabs/Plugins

        private IEnumerable<TabPage> GetPluginPages()
        {
            for (var i = tabControl1.TabPages.Count - 1; i > 0; i--)
            {
                yield return tabControl1.TabPages[i];
            }
        }

        private void closeCurrentTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.TabIndex != 0)
            {
                RequestCloseTab(tabControl1.TabPages[tabControl1.SelectedTab.TabIndex], new PluginCloseInfo(ToolBoxCloseReason.CloseCurrent));
            }
        }

        private void CloseAllTabsToolStripMenuItemClick(object sender, EventArgs e)
        {
            RequestCloseTabs(GetPluginPages(), new PluginCloseInfo(ToolBoxCloseReason.CloseAll));
        }

        private void CloseAllTabsExceptActiveToolStripMenuItemClick(object sender, EventArgs e)
        {
            RequestCloseTabs(GetPluginPages().Where(p => tabControl1.SelectedTab != p), new PluginCloseInfo(ToolBoxCloseReason.CloseAllExceptActive));
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var info = new PluginCloseInfo(e.CloseReason);
            RequestCloseTabs(GetPluginPages(), info);
            e.Cancel = info.Cancel;
        }

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            var tabControl = sender as TabControl;
            if (tabControl == null || e.Button != MouseButtons.Middle) { return; }

            var tabs = tabControl.TabPages;
            var tabPage = tabs.Cast<TabPage>()
                .Where((t, i) => tabControl.GetTabRect(i).Contains(e.Location))
                .FirstOrDefault();

            if (tabPage != null && tabControl1.TabPages[0] != tabPage)
            {
                RequestCloseTab(tabPage, new PluginCloseInfo(ToolBoxCloseReason.CloseMiddleClick));
            }
        }

        private void RequestCloseTabs(IEnumerable<TabPage> pages, PluginCloseInfo info)
        {
            foreach (var page in pages)
            {
                RequestCloseTab(page, info);
                if (info.Cancel) return;
            }
        }

        private void RequestCloseTab(TabPage page, PluginCloseInfo info)
        {
            var plugin = page.GetPlugin();
            plugin.ClosingPlugin(info);
            if (info.Cancel)
            {
                return;
            }
            CloseTab(page);
        }

        /// <summary>
        /// Only to be called from the RequestCloseTab
        /// </summary>
        /// <param name="page"></param>
        private void CloseTab(TabPage page)
        {
            tabControl1.TabPages.Remove(page);
            if (page.Controls.Count == 0)
            {
                return;
            }
            var plugin = page.Controls[0] as UserControl;
            if (plugin == null)
            {
                return;
            }

            plugin.Dispose();
        }

        #endregion // Close Tabs/Plugins

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

        private void donateInUSDollarsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            donate("EN", "USD");
        }

        private void donateInEuroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            donate("EN", "EUR");
        }

        private void donateInGBPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            donate("EN", "GBP");
        }

        private void donate(string language, string currency)
        {
            var url =
               string.Format(
                   "https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business={0}&lc={1}&item_name={2}&currency_code={3}&bn=PP%2dDonationsBF",
                   "tanguy92@hotmail.com",
                   language,
                   "Donation%20for%20MSCRM%20Tools%20-%20XrmToolBox",
                   currency);

            Process.Start(url);
        }

        private void TsbOptionsClick(object sender, EventArgs e)
        {
            var oDialog = new OptionsDialog(currentOptions);
            if (oDialog.ShowDialog(this) == DialogResult.OK)
            {
                bool reinitDisplay = currentOptions.DisplayMostUsedFirst != oDialog.Option.DisplayMostUsedFirst
                                     || currentOptions.MostUsedList.Count != oDialog.Option.MostUsedList.Count
                                     || currentOptions.DisplayLargeIcons != oDialog.Option.DisplayLargeIcons
                                     || !oDialog.Option.HiddenPlugins.SequenceEqual(currentOptions.HiddenPlugins);

              currentOptions = oDialog.Option;

               if (reinitDisplay)
                {
                    tabControl1.SelectedIndex = 0;
                    DisplayPlugins();
                }
            }
        }

        private void tsbManageConnections_Click(object sender, EventArgs e)
        {
            fHelper.DisplayConnectionsList(this);
        }
    }

    public static class Extensions
    {
        public static IMsCrmToolsPluginUserControl GetPlugin(this TabPage page)
        {
            return (IMsCrmToolsPluginUserControl)page.Controls[0];
        }
    }
}

