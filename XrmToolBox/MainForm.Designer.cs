using System.Drawing;

namespace XrmToolBox
{
    sealed partial class MainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbConnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbManageConnections = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbManageTabs = new System.Windows.Forms.ToolStripDropDownButton();
            this.closeCurrentTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllTabsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllTabsExceptActiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbOptions = new System.Windows.Forms.ToolStripButton();
            this.tsbPlugins = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslFilterPlugin = new System.Windows.Forms.ToolStripLabel();
            this.tstxtFilterPlugin = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHelp = new System.Windows.Forms.ToolStripDropDownButton();
            this.displayXrmToolBoxHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xrmToolBoxHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpSelectedPluginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayHelpPluginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutXrmToolBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCodePlex = new System.Windows.Forms.ToolStripDropDownButton();
            this.startADiscussionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GithubXrmToolBoxMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.githubPluginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.discussionPluginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CodePlexPluginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportABugPluginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startADiscussionPluginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rateThisToolPluginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDonate = new System.Windows.Forms.ToolStripDropDownButton();
            this.donateInUSDollarsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.donateInEuroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.donateInGBPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PaypalXrmToolBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PayPalSelectedPluginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.donateDollarPluginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.donateEuroPluginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.donateGbpPluginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.HomePageTab = new System.Windows.Forms.TabPage();
            this.pnlHelp = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pluginsCheckerImageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.HomePageTab.SuspendLayout();
            this.pnlHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbConnect,
            this.toolStripSeparator2,
            this.tsbManageConnections,
            this.toolStripSeparator5,
            this.tsbManageTabs,
            this.tsbOptions,
            this.tsbPlugins,
            this.toolStripSeparator1,
            this.tslFilterPlugin,
            this.tstxtFilterPlugin,
            this.toolStripSeparator6,
            this.tsbHelp,
            this.toolStripSeparator3,
            this.tsbCodePlex,
            this.toolStripSeparator4,
            this.tsbDonate});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1326, 32);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbConnect
            // 
            this.tsbConnect.Image = ((System.Drawing.Image)(resources.GetObject("tsbConnect.Image")));
            this.tsbConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbConnect.Name = "tsbConnect";
            this.tsbConnect.Size = new System.Drawing.Size(170, 29);
            this.tsbConnect.Text = "Connect to CRM";
            this.tsbConnect.Click += new System.EventHandler(this.TsbConnectClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbManageConnections
            // 
            this.tsbManageConnections.Image = ((System.Drawing.Image)(resources.GetObject("tsbManageConnections.Image")));
            this.tsbManageConnections.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbManageConnections.Name = "tsbManageConnections";
            this.tsbManageConnections.Size = new System.Drawing.Size(204, 29);
            this.tsbManageConnections.Text = "Manage connections";
            this.tsbManageConnections.Click += new System.EventHandler(this.tsbManageConnections_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbManageTabs
            // 
            this.tsbManageTabs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeCurrentTabToolStripMenuItem,
            this.closeAllTabsToolStripMenuItem,
            this.closeAllTabsExceptActiveToolStripMenuItem});
            this.tsbManageTabs.Image = ((System.Drawing.Image)(resources.GetObject("tsbManageTabs.Image")));
            this.tsbManageTabs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbManageTabs.Name = "tsbManageTabs";
            this.tsbManageTabs.Size = new System.Drawing.Size(89, 29);
            this.tsbManageTabs.Text = "Tabs";
            // 
            // closeCurrentTabToolStripMenuItem
            // 
            this.closeCurrentTabToolStripMenuItem.Name = "closeCurrentTabToolStripMenuItem";
            this.closeCurrentTabToolStripMenuItem.Size = new System.Drawing.Size(299, 30);
            this.closeCurrentTabToolStripMenuItem.Text = "Close current tab";
            this.closeCurrentTabToolStripMenuItem.Click += new System.EventHandler(this.closeCurrentTabToolStripMenuItem_Click);
            // 
            // closeAllTabsToolStripMenuItem
            // 
            this.closeAllTabsToolStripMenuItem.Name = "closeAllTabsToolStripMenuItem";
            this.closeAllTabsToolStripMenuItem.Size = new System.Drawing.Size(299, 30);
            this.closeAllTabsToolStripMenuItem.Text = "Close all";
            this.closeAllTabsToolStripMenuItem.Click += new System.EventHandler(this.CloseAllTabsToolStripMenuItemClick);
            // 
            // closeAllTabsExceptActiveToolStripMenuItem
            // 
            this.closeAllTabsExceptActiveToolStripMenuItem.Name = "closeAllTabsExceptActiveToolStripMenuItem";
            this.closeAllTabsExceptActiveToolStripMenuItem.Size = new System.Drawing.Size(299, 30);
            this.closeAllTabsExceptActiveToolStripMenuItem.Text = "Close all except active tab";
            this.closeAllTabsExceptActiveToolStripMenuItem.Click += new System.EventHandler(this.CloseAllTabsExceptActiveToolStripMenuItemClick);
            // 
            // tsbOptions
            // 
            this.tsbOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOptions.Image = ((System.Drawing.Image)(resources.GetObject("tsbOptions.Image")));
            this.tsbOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOptions.Name = "tsbOptions";
            this.tsbOptions.Size = new System.Drawing.Size(28, 29);
            this.tsbOptions.Text = "Settings";
            this.tsbOptions.Click += new System.EventHandler(this.TsbOptionsClick);
            // 
            // tsbPlugins
            // 
            this.tsbPlugins.Image = ((System.Drawing.Image)(resources.GetObject("tsbPlugins.Image")));
            this.tsbPlugins.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPlugins.Name = "tsbPlugins";
            this.tsbPlugins.Size = new System.Drawing.Size(142, 29);
            this.tsbPlugins.Text = "Plugins store";
            this.tsbPlugins.Click += new System.EventHandler(this.tsbPlugins_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // tslFilterPlugin
            // 
            this.tslFilterPlugin.Name = "tslFilterPlugin";
            this.tslFilterPlugin.Size = new System.Drawing.Size(64, 29);
            this.tslFilterPlugin.Text = "Search";
            // 
            // tstxtFilterPlugin
            // 
            this.tstxtFilterPlugin.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tstxtFilterPlugin.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tstxtFilterPlugin.Name = "tstxtFilterPlugin";
            this.tstxtFilterPlugin.Size = new System.Drawing.Size(223, 32);
            this.tstxtFilterPlugin.ToolTipText = "Filter by plugin name or company name";
            this.tstxtFilterPlugin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tstxtFilterPlugin_KeyDown);
            this.tstxtFilterPlugin.TextChanged += new System.EventHandler(this.tstxtFilterPlugin_TextChanged);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbHelp
            // 
            this.tsbHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayXrmToolBoxHelpToolStripMenuItem,
            this.xrmToolBoxHelpToolStripMenuItem,
            this.HelpSelectedPluginToolStripMenuItem,
            this.aboutXrmToolBoxToolStripMenuItem});
            this.tsbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbHelp.Image")));
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(91, 29);
            this.tsbHelp.Text = "Help";
            // 
            // displayXrmToolBoxHelpToolStripMenuItem
            // 
            this.displayXrmToolBoxHelpToolStripMenuItem.Name = "displayXrmToolBoxHelpToolStripMenuItem";
            this.displayXrmToolBoxHelpToolStripMenuItem.Size = new System.Drawing.Size(294, 30);
            this.displayXrmToolBoxHelpToolStripMenuItem.Text = "Display XrmToolBox help";
            this.displayXrmToolBoxHelpToolStripMenuItem.Click += new System.EventHandler(this.displayXrmToolBoxHelpToolStripMenuItem_Click);
            // 
            // xrmToolBoxHelpToolStripMenuItem
            // 
            this.xrmToolBoxHelpToolStripMenuItem.Name = "xrmToolBoxHelpToolStripMenuItem";
            this.xrmToolBoxHelpToolStripMenuItem.Size = new System.Drawing.Size(294, 30);
            this.xrmToolBoxHelpToolStripMenuItem.Text = "XrmToolBox";
            // 
            // HelpSelectedPluginToolStripMenuItem
            // 
            this.HelpSelectedPluginToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayHelpPluginToolStripMenuItem});
            this.HelpSelectedPluginToolStripMenuItem.Name = "HelpSelectedPluginToolStripMenuItem";
            this.HelpSelectedPluginToolStripMenuItem.Size = new System.Drawing.Size(294, 30);
            this.HelpSelectedPluginToolStripMenuItem.Text = "Selected Plugin";
            // 
            // displayHelpPluginToolStripMenuItem
            // 
            this.displayHelpPluginToolStripMenuItem.Name = "displayHelpPluginToolStripMenuItem";
            this.displayHelpPluginToolStripMenuItem.Size = new System.Drawing.Size(194, 30);
            this.displayHelpPluginToolStripMenuItem.Text = "Display help";
            this.displayHelpPluginToolStripMenuItem.Click += new System.EventHandler(this.displayHelpPluginToolStripMenuItem_Click);
            // 
            // aboutXrmToolBoxToolStripMenuItem
            // 
            this.aboutXrmToolBoxToolStripMenuItem.Name = "aboutXrmToolBoxToolStripMenuItem";
            this.aboutXrmToolBoxToolStripMenuItem.Size = new System.Drawing.Size(294, 30);
            this.aboutXrmToolBoxToolStripMenuItem.Text = "About XrmToolBox";
            this.aboutXrmToolBoxToolStripMenuItem.Click += new System.EventHandler(this.aboutXrmToolBoxToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbCodePlex
            // 
            this.tsbCodePlex.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startADiscussionToolStripMenuItem,
            this.GithubXrmToolBoxMenuItem,
            this.githubPluginMenuItem,
            this.CodePlexPluginMenuItem});
            this.tsbCodePlex.Image = ((System.Drawing.Image)(resources.GetObject("tsbCodePlex.Image")));
            this.tsbCodePlex.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCodePlex.Name = "tsbCodePlex";
            this.tsbCodePlex.Size = new System.Drawing.Size(129, 29);
            this.tsbCodePlex.Text = "Feedback";
            // 
            // startADiscussionToolStripMenuItem
            // 
            this.startADiscussionToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("startADiscussionToolStripMenuItem.Image")));
            this.startADiscussionToolStripMenuItem.Name = "startADiscussionToolStripMenuItem";
            this.startADiscussionToolStripMenuItem.Size = new System.Drawing.Size(361, 30);
            this.startADiscussionToolStripMenuItem.Text = "New issue / question / discussion";
            this.startADiscussionToolStripMenuItem.Click += new System.EventHandler(this.TsbDiscussClick);
            // 
            // GithubXrmToolBoxMenuItem
            // 
            this.GithubXrmToolBoxMenuItem.Name = "GithubXrmToolBoxMenuItem";
            this.GithubXrmToolBoxMenuItem.Size = new System.Drawing.Size(361, 30);
            this.GithubXrmToolBoxMenuItem.Text = "XrmToolbox";
            // 
            // githubPluginMenuItem
            // 
            this.githubPluginMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.discussionPluginToolStripMenuItem});
            this.githubPluginMenuItem.Name = "githubPluginMenuItem";
            this.githubPluginMenuItem.Size = new System.Drawing.Size(361, 30);
            this.githubPluginMenuItem.Text = "Selected Plugin";
            this.githubPluginMenuItem.Visible = false;
            // 
            // discussionPluginToolStripMenuItem
            // 
            this.discussionPluginToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("discussionPluginToolStripMenuItem.Image")));
            this.discussionPluginToolStripMenuItem.Name = "discussionPluginToolStripMenuItem";
            this.discussionPluginToolStripMenuItem.Size = new System.Drawing.Size(361, 30);
            this.discussionPluginToolStripMenuItem.Text = "New issue / question / discussion";
            this.discussionPluginToolStripMenuItem.Click += new System.EventHandler(this.discussionPluginToolStripMenuItem_Click);
            // 
            // CodePlexPluginMenuItem
            // 
            this.CodePlexPluginMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reportABugPluginToolStripMenuItem,
            this.startADiscussionPluginToolStripMenuItem,
            this.rateThisToolPluginToolStripMenuItem});
            this.CodePlexPluginMenuItem.Name = "CodePlexPluginMenuItem";
            this.CodePlexPluginMenuItem.Size = new System.Drawing.Size(361, 30);
            this.CodePlexPluginMenuItem.Text = "Selected Plugin";
            // 
            // reportABugPluginToolStripMenuItem
            // 
            this.reportABugPluginToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("reportABugPluginToolStripMenuItem.Image")));
            this.reportABugPluginToolStripMenuItem.Name = "reportABugPluginToolStripMenuItem";
            this.reportABugPluginToolStripMenuItem.Size = new System.Drawing.Size(388, 30);
            this.reportABugPluginToolStripMenuItem.Text = "Report a bug";
            this.reportABugPluginToolStripMenuItem.Click += new System.EventHandler(this.TsbReportBugPluginClick);
            // 
            // startADiscussionPluginToolStripMenuItem
            // 
            this.startADiscussionPluginToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("startADiscussionPluginToolStripMenuItem.Image")));
            this.startADiscussionPluginToolStripMenuItem.Name = "startADiscussionPluginToolStripMenuItem";
            this.startADiscussionPluginToolStripMenuItem.Size = new System.Drawing.Size(388, 30);
            this.startADiscussionPluginToolStripMenuItem.Text = "Start a discussion / Request a feature";
            this.startADiscussionPluginToolStripMenuItem.Click += new System.EventHandler(this.TsbDiscussPluginClick);
            // 
            // rateThisToolPluginToolStripMenuItem
            // 
            this.rateThisToolPluginToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("rateThisToolPluginToolStripMenuItem.Image")));
            this.rateThisToolPluginToolStripMenuItem.Name = "rateThisToolPluginToolStripMenuItem";
            this.rateThisToolPluginToolStripMenuItem.Size = new System.Drawing.Size(388, 30);
            this.rateThisToolPluginToolStripMenuItem.Text = "Rate this tool";
            this.rateThisToolPluginToolStripMenuItem.Click += new System.EventHandler(this.TsbRatePluginClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbDonate
            // 
            this.tsbDonate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.donateInUSDollarsToolStripMenuItem,
            this.donateInEuroToolStripMenuItem,
            this.donateInGBPToolStripMenuItem,
            this.PaypalXrmToolBoxToolStripMenuItem,
            this.PayPalSelectedPluginToolStripMenuItem});
            this.tsbDonate.Image = ((System.Drawing.Image)(resources.GetObject("tsbDonate.Image")));
            this.tsbDonate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDonate.Name = "tsbDonate";
            this.tsbDonate.Size = new System.Drawing.Size(112, 29);
            this.tsbDonate.Text = "Donate";
            // 
            // donateInUSDollarsToolStripMenuItem
            // 
            this.donateInUSDollarsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("donateInUSDollarsToolStripMenuItem.Image")));
            this.donateInUSDollarsToolStripMenuItem.Name = "donateInUSDollarsToolStripMenuItem";
            this.donateInUSDollarsToolStripMenuItem.Size = new System.Drawing.Size(261, 30);
            this.donateInUSDollarsToolStripMenuItem.Text = "Donate in US Dollars";
            this.donateInUSDollarsToolStripMenuItem.Click += new System.EventHandler(this.donateInUSDollarsToolStripMenuItem_Click);
            // 
            // donateInEuroToolStripMenuItem
            // 
            this.donateInEuroToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("donateInEuroToolStripMenuItem.Image")));
            this.donateInEuroToolStripMenuItem.Name = "donateInEuroToolStripMenuItem";
            this.donateInEuroToolStripMenuItem.Size = new System.Drawing.Size(261, 30);
            this.donateInEuroToolStripMenuItem.Text = "Donate in Euro";
            this.donateInEuroToolStripMenuItem.Click += new System.EventHandler(this.donateInEuroToolStripMenuItem_Click);
            // 
            // donateInGBPToolStripMenuItem
            // 
            this.donateInGBPToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("donateInGBPToolStripMenuItem.Image")));
            this.donateInGBPToolStripMenuItem.Name = "donateInGBPToolStripMenuItem";
            this.donateInGBPToolStripMenuItem.Size = new System.Drawing.Size(261, 30);
            this.donateInGBPToolStripMenuItem.Text = "Donate in GBP";
            this.donateInGBPToolStripMenuItem.Click += new System.EventHandler(this.donateInGBPToolStripMenuItem_Click);
            // 
            // PaypalXrmToolBoxToolStripMenuItem
            // 
            this.PaypalXrmToolBoxToolStripMenuItem.Name = "PaypalXrmToolBoxToolStripMenuItem";
            this.PaypalXrmToolBoxToolStripMenuItem.Size = new System.Drawing.Size(261, 30);
            this.PaypalXrmToolBoxToolStripMenuItem.Text = "XrmToolBox";
            // 
            // PayPalSelectedPluginToolStripMenuItem
            // 
            this.PayPalSelectedPluginToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.donateDollarPluginMenuItem,
            this.donateEuroPluginMenuItem,
            this.donateGbpPluginMenuItem});
            this.PayPalSelectedPluginToolStripMenuItem.Name = "PayPalSelectedPluginToolStripMenuItem";
            this.PayPalSelectedPluginToolStripMenuItem.Size = new System.Drawing.Size(261, 30);
            this.PayPalSelectedPluginToolStripMenuItem.Text = "SelectedPlugin";
            // 
            // donateDollarPluginMenuItem
            // 
            this.donateDollarPluginMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("donateDollarPluginMenuItem.Image")));
            this.donateDollarPluginMenuItem.Name = "donateDollarPluginMenuItem";
            this.donateDollarPluginMenuItem.Size = new System.Drawing.Size(261, 30);
            this.donateDollarPluginMenuItem.Text = "Donate in US Dollars";
            this.donateDollarPluginMenuItem.Click += new System.EventHandler(this.donateDollarPluginMenuItem_Click);
            // 
            // donateEuroPluginMenuItem
            // 
            this.donateEuroPluginMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("donateEuroPluginMenuItem.Image")));
            this.donateEuroPluginMenuItem.Name = "donateEuroPluginMenuItem";
            this.donateEuroPluginMenuItem.Size = new System.Drawing.Size(261, 30);
            this.donateEuroPluginMenuItem.Text = "Donate in Euro";
            this.donateEuroPluginMenuItem.Click += new System.EventHandler(this.donateEuroPluginMenuItem_Click);
            // 
            // donateGbpPluginMenuItem
            // 
            this.donateGbpPluginMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("donateGbpPluginMenuItem.Image")));
            this.donateGbpPluginMenuItem.Name = "donateGbpPluginMenuItem";
            this.donateGbpPluginMenuItem.Size = new System.Drawing.Size(261, 30);
            this.donateGbpPluginMenuItem.Text = "Donate in GBP";
            this.donateGbpPluginMenuItem.Click += new System.EventHandler(this.donateGbpPluginMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.HomePageTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 32);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1326, 886);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseClick);
            // 
            // HomePageTab
            // 
            this.HomePageTab.AutoScroll = true;
            this.HomePageTab.Controls.Add(this.pnlHelp);
            this.HomePageTab.Location = new System.Drawing.Point(4, 32);
            this.HomePageTab.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.HomePageTab.Name = "HomePageTab";
            this.HomePageTab.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.HomePageTab.Size = new System.Drawing.Size(1318, 850);
            this.HomePageTab.TabIndex = 0;
            this.HomePageTab.Text = "Home";
            this.HomePageTab.UseVisualStyleBackColor = true;
            // 
            // pnlHelp
            // 
            this.pnlHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHelp.Controls.Add(this.label2);
            this.pnlHelp.Controls.Add(this.label1);
            this.pnlHelp.Location = new System.Drawing.Point(4, 9);
            this.pnlHelp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlHelp.Name = "pnlHelp";
            this.pnlHelp.Size = new System.Drawing.Size(1298, 831);
            this.pnlHelp.TabIndex = 0;
            this.pnlHelp.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 78);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1286, 174);
            this.label2.TabIndex = 1;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(8, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1286, 51);
            this.label1.TabIndex = 0;
            this.label1.Text = "Oups... no plugin found!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pluginsCheckerImageList
            // 
            this.pluginsCheckerImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("pluginsCheckerImageList.ImageStream")));
            this.pluginsCheckerImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.pluginsCheckerImageList.Images.SetKeyName(0, "plugin.png");
            this.pluginsCheckerImageList.Images.SetKeyName(1, "plugin.png");
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1326, 918);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Opacity = 0D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XrmToolBox for Microsoft Dynamics CRM 2011 to 2016";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.HomePageTab.ResumeLayout(false);
            this.pnlHelp.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbConnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripDropDownButton tsbManageTabs;
        private System.Windows.Forms.ToolStripMenuItem closeAllTabsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllTabsExceptActiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripDropDownButton tsbCodePlex;
        private System.Windows.Forms.ToolStripMenuItem startADiscussionToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbOptions;
        private System.Windows.Forms.ToolStripDropDownButton tsbDonate;
        private System.Windows.Forms.ToolStripMenuItem donateInUSDollarsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem donateInEuroToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbManageConnections;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem donateInGBPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeCurrentTabToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage HomePageTab;
        private System.Windows.Forms.Panel pnlHelp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem GithubXrmToolBoxMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CodePlexPluginMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportABugPluginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startADiscussionPluginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rateThisToolPluginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem githubPluginMenuItem;
        private System.Windows.Forms.ToolStripMenuItem discussionPluginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PaypalXrmToolBoxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PayPalSelectedPluginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem donateDollarPluginMenuItem;
        private System.Windows.Forms.ToolStripMenuItem donateEuroPluginMenuItem;
        private System.Windows.Forms.ToolStripMenuItem donateGbpPluginMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripLabel tslFilterPlugin;
        private System.Windows.Forms.ToolStripTextBox tstxtFilterPlugin;
        private System.Windows.Forms.ToolStripDropDownButton tsbHelp;
        private System.Windows.Forms.ToolStripMenuItem displayXrmToolBoxHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xrmToolBoxHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpSelectedPluginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayHelpPluginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutXrmToolBoxToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbPlugins;
        private System.Windows.Forms.ImageList pluginsCheckerImageList;
    }
}

