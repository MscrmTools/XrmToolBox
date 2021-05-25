using System.Windows.Forms;

namespace XrmToolBox.New
{
    partial class NewForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewForm));
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbConnect = new System.Windows.Forms.ToolStripButton();
            this.tssSearch = new System.Windows.Forms.ToolStripSeparator();
            this.tsbManageWindows = new System.Windows.Forms.ToolStripDropDownButton();
            this.closeCurrentWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllWindowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllWindowsExceptActiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tssWindows = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiShowStartPage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsddbTools = new System.Windows.Forms.ToolStripDropDownButton();
            this.manageConnectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.pluginsStoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiXtbSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiToolSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsddbHelp = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiXtbHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPluginHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tssHelp = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiXtbFeedback = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPluginFeedback = new System.Windows.Forms.ToolStripMenuItem();
            this.tssFeedback = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiXtbDonate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDonateUsdXrmToolBox = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDonateEurXrmToolBox = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDonateGbpXrmToolBox = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPluginDonate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDonateUsdSelectedPlugin = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDonateEurSelectedPlugin = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDonateGbpSelectedPlugin = new System.Windows.Forms.ToolStripMenuItem();
            this.tssDonate = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCheckForUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiXtbAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPluginAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tssOpenOrg = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOpenOrg = new System.Windows.Forms.ToolStripButton();
            this.tsbImpersonate = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDonate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDonateXrmToolBox = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDonateSelectedPlugin = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.checkForUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiChangeTabConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlSupport = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.llDonate = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.llDismiss = new System.Windows.Forms.LinkLabel();
            this.pnlPluginsUpdate = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.llClosePluginsUpdatePanel = new System.Windows.Forms.LinkLabel();
            this.pbOpenPluginsStore = new System.Windows.Forms.PictureBox();
            this.lblPluginsUpdateAvailable = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlConnectLoading = new System.Windows.Forms.Panel();
            this.lblConnecting = new System.Windows.Forms.Label();
            this.pbConnectionLoading = new System.Windows.Forms.PictureBox();
            this.dpMain = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.cmsMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsMainCloseThis = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMainCloseExceptThis = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMainCloseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMainDuplicateTool = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMainDuplicateToolWithConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMain.SuspendLayout();
            this.pnlSupport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.pnlPluginsUpdate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOpenPluginsStore)).BeginInit();
            this.pnlConnectLoading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbConnectionLoading)).BeginInit();
            this.cmsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsMain
            // 
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbConnect,
            this.tssSearch,
            this.tsbManageWindows,
            this.toolStripSeparator1,
            this.tsddbTools,
            this.tsddbHelp,
            this.tssOpenOrg,
            this.tsbOpenOrg,
            this.tsbImpersonate});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(1058, 34);
            this.tsMain.TabIndex = 3;
            this.tsMain.Text = "tsMain";
            // 
            // tsbConnect
            // 
            this.tsbConnect.Image = global::XrmToolBox.Properties.Resources.Connect16;
            this.tsbConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbConnect.Name = "tsbConnect";
            this.tsbConnect.Size = new System.Drawing.Size(97, 29);
            this.tsbConnect.Text = "Connect";
            this.tsbConnect.Click += new System.EventHandler(this.tsbConnect_Click);
            // 
            // tssSearch
            // 
            this.tssSearch.Name = "tssSearch";
            this.tssSearch.Size = new System.Drawing.Size(6, 34);
            // 
            // tsbManageWindows
            // 
            this.tsbManageWindows.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeCurrentWindowToolStripMenuItem,
            this.closeAllWindowsToolStripMenuItem,
            this.closeAllWindowsExceptActiveToolStripMenuItem,
            this.tssWindows,
            this.tsmiShowStartPage});
            this.tsbManageWindows.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbManageWindows.Name = "tsbManageWindows";
            this.tsbManageWindows.Size = new System.Drawing.Size(104, 29);
            this.tsbManageWindows.Text = "Windows";
            this.tsbManageWindows.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsbManageWindows_DropDownItemClicked);
            // 
            // closeCurrentWindowToolStripMenuItem
            // 
            this.closeCurrentWindowToolStripMenuItem.Name = "closeCurrentWindowToolStripMenuItem";
            this.closeCurrentWindowToolStripMenuItem.Size = new System.Drawing.Size(352, 34);
            this.closeCurrentWindowToolStripMenuItem.Text = "Close current window";
            // 
            // closeAllWindowsToolStripMenuItem
            // 
            this.closeAllWindowsToolStripMenuItem.Name = "closeAllWindowsToolStripMenuItem";
            this.closeAllWindowsToolStripMenuItem.Size = new System.Drawing.Size(352, 34);
            this.closeAllWindowsToolStripMenuItem.Text = "Close all";
            // 
            // closeAllWindowsExceptActiveToolStripMenuItem
            // 
            this.closeAllWindowsExceptActiveToolStripMenuItem.Name = "closeAllWindowsExceptActiveToolStripMenuItem";
            this.closeAllWindowsExceptActiveToolStripMenuItem.Size = new System.Drawing.Size(352, 34);
            this.closeAllWindowsExceptActiveToolStripMenuItem.Text = "Close all except active window";
            this.closeAllWindowsExceptActiveToolStripMenuItem.ToolTipText = "Only applies to Tool pages";
            // 
            // tssWindows
            // 
            this.tssWindows.Name = "tssWindows";
            this.tssWindows.Size = new System.Drawing.Size(349, 6);
            // 
            // tsmiShowStartPage
            // 
            this.tsmiShowStartPage.Name = "tsmiShowStartPage";
            this.tsmiShowStartPage.Size = new System.Drawing.Size(352, 34);
            this.tsmiShowStartPage.Text = "Show Start Page";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 34);
            // 
            // tsddbTools
            // 
            this.tsddbTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageConnectionsToolStripMenuItem,
            this.toolStripSeparator10,
            this.pluginsStoreToolStripMenuItem,
            this.tsmiXtbSettings,
            this.tsmiToolSettings});
            this.tsddbTools.Image = ((System.Drawing.Image)(resources.GetObject("tsddbTools.Image")));
            this.tsddbTools.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbTools.Name = "tsddbTools";
            this.tsddbTools.Size = new System.Drawing.Size(155, 29);
            this.tsddbTools.Text = "Configuration";
            this.tsddbTools.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsddbTools_DropDownItemClicked);
            // 
            // manageConnectionsToolStripMenuItem
            // 
            this.manageConnectionsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("manageConnectionsToolStripMenuItem.Image")));
            this.manageConnectionsToolStripMenuItem.Name = "manageConnectionsToolStripMenuItem";
            this.manageConnectionsToolStripMenuItem.Size = new System.Drawing.Size(278, 34);
            this.manageConnectionsToolStripMenuItem.Text = "Manage connections";
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(275, 6);
            // 
            // pluginsStoreToolStripMenuItem
            // 
            this.pluginsStoreToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pluginsStoreToolStripMenuItem.Image")));
            this.pluginsStoreToolStripMenuItem.Name = "pluginsStoreToolStripMenuItem";
            this.pluginsStoreToolStripMenuItem.Size = new System.Drawing.Size(278, 34);
            this.pluginsStoreToolStripMenuItem.Text = "Tool Library";
            // 
            // tsmiXtbSettings
            // 
            this.tsmiXtbSettings.Image = ((System.Drawing.Image)(resources.GetObject("tsmiXtbSettings.Image")));
            this.tsmiXtbSettings.Name = "tsmiXtbSettings";
            this.tsmiXtbSettings.Size = new System.Drawing.Size(278, 34);
            this.tsmiXtbSettings.Text = "Settings";
            // 
            // tsmiToolSettings
            // 
            this.tsmiToolSettings.Name = "tsmiToolSettings";
            this.tsmiToolSettings.Size = new System.Drawing.Size(278, 34);
            this.tsmiToolSettings.Tag = "Settings for {0}";
            this.tsmiToolSettings.Text = "Settings for {0}";
            this.tsmiToolSettings.Visible = false;
            // 
            // tsddbHelp
            // 
            this.tsddbHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiXtbHelp,
            this.tsmiPluginHelp,
            this.tssHelp,
            this.tsmiXtbFeedback,
            this.tsmiPluginFeedback,
            this.tssFeedback,
            this.tsmiXtbDonate,
            this.tsmiPluginDonate,
            this.tssDonate,
            this.tsmiCheckForUpdate,
            this.tsmiXtbAbout,
            this.tsmiPluginAbout});
            this.tsddbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsddbHelp.Image")));
            this.tsddbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbHelp.Name = "tsddbHelp";
            this.tsddbHelp.Size = new System.Drawing.Size(83, 29);
            this.tsddbHelp.Text = "Help";
            // 
            // tsmiXtbHelp
            // 
            this.tsmiXtbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsmiXtbHelp.Image")));
            this.tsmiXtbHelp.Name = "tsmiXtbHelp";
            this.tsmiXtbHelp.Size = new System.Drawing.Size(317, 34);
            this.tsmiXtbHelp.Text = "XrmToolBox help";
            this.tsmiXtbHelp.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // tsmiPluginHelp
            // 
            this.tsmiPluginHelp.Name = "tsmiPluginHelp";
            this.tsmiPluginHelp.Size = new System.Drawing.Size(317, 34);
            this.tsmiPluginHelp.Text = "Plugin help";
            this.tsmiPluginHelp.Click += new System.EventHandler(this.HelpSelectedPluginToolStripMenuItem_Click);
            // 
            // tssHelp
            // 
            this.tssHelp.Name = "tssHelp";
            this.tssHelp.Size = new System.Drawing.Size(314, 6);
            // 
            // tsmiXtbFeedback
            // 
            this.tsmiXtbFeedback.Name = "tsmiXtbFeedback";
            this.tsmiXtbFeedback.Size = new System.Drawing.Size(317, 34);
            this.tsmiXtbFeedback.Text = "Feedback for XrmToolBox";
            this.tsmiXtbFeedback.Click += new System.EventHandler(this.GithubXrmToolBoxMenuItem_Click);
            // 
            // tsmiPluginFeedback
            // 
            this.tsmiPluginFeedback.Name = "tsmiPluginFeedback";
            this.tsmiPluginFeedback.Size = new System.Drawing.Size(317, 34);
            this.tsmiPluginFeedback.Text = "Feedback for Plugin";
            this.tsmiPluginFeedback.Click += new System.EventHandler(this.githubPluginMenuItem_Click);
            // 
            // tssFeedback
            // 
            this.tssFeedback.Name = "tssFeedback";
            this.tssFeedback.Size = new System.Drawing.Size(314, 6);
            // 
            // tsmiXtbDonate
            // 
            this.tsmiXtbDonate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDonateUsdXrmToolBox,
            this.tsmiDonateEurXrmToolBox,
            this.tsmiDonateGbpXrmToolBox});
            this.tsmiXtbDonate.Name = "tsmiXtbDonate";
            this.tsmiXtbDonate.Size = new System.Drawing.Size(317, 34);
            this.tsmiXtbDonate.Text = "Donate for XrmToolBox";
            // 
            // tsmiDonateUsdXrmToolBox
            // 
            this.tsmiDonateUsdXrmToolBox.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDonateUsdXrmToolBox.Image")));
            this.tsmiDonateUsdXrmToolBox.Name = "tsmiDonateUsdXrmToolBox";
            this.tsmiDonateUsdXrmToolBox.Size = new System.Drawing.Size(278, 34);
            this.tsmiDonateUsdXrmToolBox.Text = "Donate in US Dollars";
            this.tsmiDonateUsdXrmToolBox.Click += new System.EventHandler(this.donateInUSDollarsToolStripMenuItem_Click);
            // 
            // tsmiDonateEurXrmToolBox
            // 
            this.tsmiDonateEurXrmToolBox.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDonateEurXrmToolBox.Image")));
            this.tsmiDonateEurXrmToolBox.Name = "tsmiDonateEurXrmToolBox";
            this.tsmiDonateEurXrmToolBox.Size = new System.Drawing.Size(278, 34);
            this.tsmiDonateEurXrmToolBox.Text = "Donate in Euro";
            this.tsmiDonateEurXrmToolBox.Click += new System.EventHandler(this.donateInEuroToolStripMenuItem_Click);
            // 
            // tsmiDonateGbpXrmToolBox
            // 
            this.tsmiDonateGbpXrmToolBox.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDonateGbpXrmToolBox.Image")));
            this.tsmiDonateGbpXrmToolBox.Name = "tsmiDonateGbpXrmToolBox";
            this.tsmiDonateGbpXrmToolBox.Size = new System.Drawing.Size(278, 34);
            this.tsmiDonateGbpXrmToolBox.Text = "Donate in GBP";
            this.tsmiDonateGbpXrmToolBox.Click += new System.EventHandler(this.donateInGBPToolStripMenuItem_Click);
            // 
            // tsmiPluginDonate
            // 
            this.tsmiPluginDonate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDonateUsdSelectedPlugin,
            this.tsmiDonateEurSelectedPlugin,
            this.tsmiDonateGbpSelectedPlugin});
            this.tsmiPluginDonate.Name = "tsmiPluginDonate";
            this.tsmiPluginDonate.Size = new System.Drawing.Size(317, 34);
            this.tsmiPluginDonate.Text = "Donate for Plugin";
            // 
            // tsmiDonateUsdSelectedPlugin
            // 
            this.tsmiDonateUsdSelectedPlugin.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDonateUsdSelectedPlugin.Image")));
            this.tsmiDonateUsdSelectedPlugin.Name = "tsmiDonateUsdSelectedPlugin";
            this.tsmiDonateUsdSelectedPlugin.Size = new System.Drawing.Size(278, 34);
            this.tsmiDonateUsdSelectedPlugin.Text = "Donate in US Dollars";
            this.tsmiDonateUsdSelectedPlugin.Click += new System.EventHandler(this.donateDollarPluginMenuItem_Click);
            // 
            // tsmiDonateEurSelectedPlugin
            // 
            this.tsmiDonateEurSelectedPlugin.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDonateEurSelectedPlugin.Image")));
            this.tsmiDonateEurSelectedPlugin.Name = "tsmiDonateEurSelectedPlugin";
            this.tsmiDonateEurSelectedPlugin.Size = new System.Drawing.Size(278, 34);
            this.tsmiDonateEurSelectedPlugin.Text = "Donate in Euro";
            this.tsmiDonateEurSelectedPlugin.Click += new System.EventHandler(this.donateEuroPluginMenuItem_Click);
            // 
            // tsmiDonateGbpSelectedPlugin
            // 
            this.tsmiDonateGbpSelectedPlugin.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDonateGbpSelectedPlugin.Image")));
            this.tsmiDonateGbpSelectedPlugin.Name = "tsmiDonateGbpSelectedPlugin";
            this.tsmiDonateGbpSelectedPlugin.Size = new System.Drawing.Size(278, 34);
            this.tsmiDonateGbpSelectedPlugin.Text = "Donate in GBP";
            this.tsmiDonateGbpSelectedPlugin.Click += new System.EventHandler(this.donateGbpPluginMenuItem_Click);
            // 
            // tssDonate
            // 
            this.tssDonate.Name = "tssDonate";
            this.tssDonate.Size = new System.Drawing.Size(314, 6);
            // 
            // tsmiCheckForUpdate
            // 
            this.tsmiCheckForUpdate.Image = ((System.Drawing.Image)(resources.GetObject("tsmiCheckForUpdate.Image")));
            this.tsmiCheckForUpdate.Name = "tsmiCheckForUpdate";
            this.tsmiCheckForUpdate.Size = new System.Drawing.Size(317, 34);
            this.tsmiCheckForUpdate.Text = "Check for update";
            this.tsmiCheckForUpdate.Click += new System.EventHandler(this.checkForUpdateToolStripMenuItem_Click);
            // 
            // tsmiXtbAbout
            // 
            this.tsmiXtbAbout.Name = "tsmiXtbAbout";
            this.tsmiXtbAbout.Size = new System.Drawing.Size(317, 34);
            this.tsmiXtbAbout.Text = "About XrmToolBox";
            this.tsmiXtbAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
            // 
            // tsmiPluginAbout
            // 
            this.tsmiPluginAbout.Name = "tsmiPluginAbout";
            this.tsmiPluginAbout.Size = new System.Drawing.Size(317, 34);
            this.tsmiPluginAbout.Text = "About Plugin";
            this.tsmiPluginAbout.Click += new System.EventHandler(this.tsmiAboutSelectedPlugin_Click);
            // 
            // tssOpenOrg
            // 
            this.tssOpenOrg.Name = "tssOpenOrg";
            this.tssOpenOrg.Size = new System.Drawing.Size(6, 34);
            this.tssOpenOrg.Visible = false;
            // 
            // tsbOpenOrg
            // 
            this.tsbOpenOrg.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpenOrg.Image")));
            this.tsbOpenOrg.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenOrg.Name = "tsbOpenOrg";
            this.tsbOpenOrg.Size = new System.Drawing.Size(180, 29);
            this.tsbOpenOrg.Text = "Open organization";
            this.tsbOpenOrg.ToolTipText = "Opens the connected organization in your web browser";
            this.tsbOpenOrg.Visible = false;
            this.tsbOpenOrg.Click += new System.EventHandler(this.tsbOpenOrg_Click);
            // 
            // tsbImpersonate
            // 
            this.tsbImpersonate.Image = ((System.Drawing.Image)(resources.GetObject("tsbImpersonate.Image")));
            this.tsbImpersonate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImpersonate.Name = "tsbImpersonate";
            this.tsbImpersonate.Size = new System.Drawing.Size(132, 29);
            this.tsbImpersonate.Text = "Impersonate";
            this.tsbImpersonate.ToolTipText = "Impersonate as another user in the organization/environment\n\nBe careful! If the c" +
    "urrent connection is already used by multiple tools, they will used this imperso" +
    "nation";
            this.tsbImpersonate.Visible = false;
            this.tsbImpersonate.Click += new System.EventHandler(this.tsbImpersonate_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(23, 23);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(332, 6);
            // 
            // tsmiDonate
            // 
            this.tsmiDonate.Name = "tsmiDonate";
            this.tsmiDonate.Size = new System.Drawing.Size(32, 19);
            // 
            // tsmiDonateXrmToolBox
            // 
            this.tsmiDonateXrmToolBox.Name = "tsmiDonateXrmToolBox";
            this.tsmiDonateXrmToolBox.Size = new System.Drawing.Size(32, 19);
            // 
            // tsmiDonateSelectedPlugin
            // 
            this.tsmiDonateSelectedPlugin.Name = "tsmiDonateSelectedPlugin";
            this.tsmiDonateSelectedPlugin.Size = new System.Drawing.Size(32, 19);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(332, 6);
            // 
            // checkForUpdateToolStripMenuItem
            // 
            this.checkForUpdateToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("checkForUpdateToolStripMenuItem.Image")));
            this.checkForUpdateToolStripMenuItem.Name = "checkForUpdateToolStripMenuItem";
            this.checkForUpdateToolStripMenuItem.Size = new System.Drawing.Size(335, 34);
            this.checkForUpdateToolStripMenuItem.Text = "Check for update";
            this.checkForUpdateToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdateToolStripMenuItem_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(332, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(360, 6);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(360, 6);
            // 
            // tsmiChangeTabConnection
            // 
            this.tsmiChangeTabConnection.Name = "tsmiChangeTabConnection";
            this.tsmiChangeTabConnection.Size = new System.Drawing.Size(363, 32);
            this.tsmiChangeTabConnection.Text = "Change connection";
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.SystemColors.Info;
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 34);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1058, 60);
            this.pnlTop.TabIndex = 4;
            this.pnlTop.Visible = false;
            // 
            // pnlSupport
            // 
            this.pnlSupport.BackColor = System.Drawing.Color.White;
            this.pnlSupport.Controls.Add(this.pictureBox2);
            this.pnlSupport.Controls.Add(this.llDonate);
            this.pnlSupport.Controls.Add(this.label5);
            this.pnlSupport.Controls.Add(this.label4);
            this.pnlSupport.Controls.Add(this.lblTitle);
            this.pnlSupport.Controls.Add(this.llDismiss);
            this.pnlSupport.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSupport.Location = new System.Drawing.Point(0, 489);
            this.pnlSupport.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.pnlSupport.Name = "pnlSupport";
            this.pnlSupport.Size = new System.Drawing.Size(1058, 103);
            this.pnlSupport.TabIndex = 11;
            this.pnlSupport.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(716, 15);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(237, 63);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 22;
            this.pictureBox2.TabStop = false;
            // 
            // llDonate
            // 
            this.llDonate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.llDonate.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.llDonate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.llDonate.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.llDonate.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.llDonate.Location = new System.Drawing.Point(501, 66);
            this.llDonate.Name = "llDonate";
            this.llDonate.Size = new System.Drawing.Size(191, 25);
            this.llDonate.TabIndex = 21;
            this.llDonate.TabStop = true;
            this.llDonate.Text = "Click here to donate";
            this.llDonate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.llDonate.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.llDonate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDonate_LinkClicked);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(495, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(212, 57);
            this.label5.TabIndex = 20;
            this.label5.Text = "XrmToolBox is free and it won\'t change. But you can show your support by making a" +
    " donation to its author.";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(147, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(180, 30);
            this.label4.TabIndex = 17;
            this.label4.Text = "Donate on PayPal";
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(3, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(677, 54);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "How can I help XrmToolBox ?";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // llDismiss
            // 
            this.llDismiss.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llDismiss.AutoSize = true;
            this.llDismiss.Location = new System.Drawing.Point(988, 5);
            this.llDismiss.Name = "llDismiss";
            this.llDismiss.Size = new System.Drawing.Size(64, 20);
            this.llDismiss.TabIndex = 0;
            this.llDismiss.TabStop = true;
            this.llDismiss.Text = "Dismiss";
            this.llDismiss.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDismiss_LinkClicked);
            // 
            // pnlPluginsUpdate
            // 
            this.pnlPluginsUpdate.BackColor = System.Drawing.Color.AliceBlue;
            this.pnlPluginsUpdate.Controls.Add(this.label1);
            this.pnlPluginsUpdate.Controls.Add(this.llClosePluginsUpdatePanel);
            this.pnlPluginsUpdate.Controls.Add(this.pbOpenPluginsStore);
            this.pnlPluginsUpdate.Controls.Add(this.lblPluginsUpdateAvailable);
            this.pnlPluginsUpdate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPluginsUpdate.Location = new System.Drawing.Point(0, 592);
            this.pnlPluginsUpdate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.pnlPluginsUpdate.Name = "pnlPluginsUpdate";
            this.pnlPluginsUpdate.Size = new System.Drawing.Size(1058, 42);
            this.pnlPluginsUpdate.TabIndex = 11;
            this.pnlPluginsUpdate.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(117)))), ((int)(((byte)(188)))));
            this.label1.Location = new System.Drawing.Point(761, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 42);
            this.label1.TabIndex = 13;
            this.label1.Tag = "";
            this.label1.Text = "Open Tool Library";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Click += new System.EventHandler(this.openPluginsStoreButton_Click);
            // 
            // llClosePluginsUpdatePanel
            // 
            this.llClosePluginsUpdatePanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.llClosePluginsUpdatePanel.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llClosePluginsUpdatePanel.LinkColor = System.Drawing.Color.Black;
            this.llClosePluginsUpdatePanel.Location = new System.Drawing.Point(1037, 0);
            this.llClosePluginsUpdatePanel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.llClosePluginsUpdatePanel.Name = "llClosePluginsUpdatePanel";
            this.llClosePluginsUpdatePanel.Size = new System.Drawing.Size(21, 42);
            this.llClosePluginsUpdatePanel.TabIndex = 12;
            this.llClosePluginsUpdatePanel.TabStop = true;
            this.llClosePluginsUpdatePanel.Text = "X";
            this.llClosePluginsUpdatePanel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.llClosePluginsUpdatePanel.VisitedLinkColor = System.Drawing.Color.Black;
            this.llClosePluginsUpdatePanel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llClosePluginsUpdatePanel_LinkClicked);
            // 
            // pbOpenPluginsStore
            // 
            this.pbOpenPluginsStore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbOpenPluginsStore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbOpenPluginsStore.Image = global::XrmToolBox.Properties.Resources.PluginsStore64;
            this.pbOpenPluginsStore.Location = new System.Drawing.Point(987, 0);
            this.pbOpenPluginsStore.Margin = new System.Windows.Forms.Padding(2);
            this.pbOpenPluginsStore.Name = "pbOpenPluginsStore";
            this.pbOpenPluginsStore.Size = new System.Drawing.Size(41, 42);
            this.pbOpenPluginsStore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbOpenPluginsStore.TabIndex = 11;
            this.pbOpenPluginsStore.TabStop = false;
            this.pbOpenPluginsStore.Click += new System.EventHandler(this.openPluginsStoreButton_Click);
            // 
            // lblPluginsUpdateAvailable
            // 
            this.lblPluginsUpdateAvailable.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblPluginsUpdateAvailable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPluginsUpdateAvailable.Location = new System.Drawing.Point(3, 2);
            this.lblPluginsUpdateAvailable.Name = "lblPluginsUpdateAvailable";
            this.lblPluginsUpdateAvailable.Size = new System.Drawing.Size(592, 42);
            this.lblPluginsUpdateAvailable.TabIndex = 10;
            this.lblPluginsUpdateAvailable.Tag = "{0} Update{1} {2} available for your tools";
            this.lblPluginsUpdateAvailable.Text = "X Updates are available for your tools";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.SystemColors.Info;
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 429);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1058, 60);
            this.pnlBottom.TabIndex = 12;
            this.pnlBottom.Visible = false;
            // 
            // pnlConnectLoading
            // 
            this.pnlConnectLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlConnectLoading.BackColor = System.Drawing.Color.Transparent;
            this.pnlConnectLoading.Controls.Add(this.lblConnecting);
            this.pnlConnectLoading.Controls.Add(this.pbConnectionLoading);
            this.pnlConnectLoading.Location = new System.Drawing.Point(-135, -67);
            this.pnlConnectLoading.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.pnlConnectLoading.Name = "pnlConnectLoading";
            this.pnlConnectLoading.Size = new System.Drawing.Size(1326, 767);
            this.pnlConnectLoading.TabIndex = 17;
            this.pnlConnectLoading.Visible = false;
            // 
            // lblConnecting
            // 
            this.lblConnecting.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblConnecting.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnecting.Location = new System.Drawing.Point(0, 423);
            this.lblConnecting.Name = "lblConnecting";
            this.lblConnecting.Size = new System.Drawing.Size(1326, 43);
            this.lblConnecting.TabIndex = 1;
            this.lblConnecting.Tag = "Please wait while XrmToolBox is connecting to {0}";
            this.lblConnecting.Text = "Please wait while XrmToolBox is connecting to {0}";
            this.lblConnecting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbConnectionLoading
            // 
            this.pbConnectionLoading.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbConnectionLoading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbConnectionLoading.Image = ((System.Drawing.Image)(resources.GetObject("pbConnectionLoading.Image")));
            this.pbConnectionLoading.Location = new System.Drawing.Point(0, 242);
            this.pbConnectionLoading.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.pbConnectionLoading.Name = "pbConnectionLoading";
            this.pbConnectionLoading.Size = new System.Drawing.Size(1326, 163);
            this.pbConnectionLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbConnectionLoading.TabIndex = 0;
            this.pbConnectionLoading.TabStop = false;
            // 
            // dpMain
            // 
            this.dpMain.BackColor = System.Drawing.Color.White;
            this.dpMain.DefaultFloatWindowSize = new System.Drawing.Size(800, 600);
            this.dpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dpMain.Location = new System.Drawing.Point(0, 94);
            this.dpMain.Name = "dpMain";
            this.dpMain.Size = new System.Drawing.Size(1058, 335);
            this.dpMain.TabIndex = 21;
            this.dpMain.ActiveDocumentChanged += new System.EventHandler(this.dpMain_ActiveDocumentChanged);
            this.dpMain.ActiveContentChanged += new System.EventHandler(this.dpMain_ActiveContentChanged);
            this.dpMain.DocumentDragged += new System.EventHandler(this.dpMain_DocumentDragged);
            this.dpMain.ActivePaneChanged += new System.EventHandler(this.dpMain_ActivePaneChanged);
            // 
            // cmsMain
            // 
            this.cmsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsMainCloseThis,
            this.cmsMainCloseExceptThis,
            this.cmsMainCloseAll,
            this.toolStripSeparator3,
            this.cmsMainDuplicateTool,
            this.cmsMainDuplicateToolWithConnection,
            this.toolStripSeparator2,
            this.tsmiChangeTabConnection});
            this.cmsMain.Name = "cmsMain";
            this.cmsMain.Size = new System.Drawing.Size(364, 241);
            this.cmsMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmsMain_ItemClicked);
            // 
            // cmsMainCloseThis
            // 
            this.cmsMainCloseThis.Name = "cmsMainCloseThis";
            this.cmsMainCloseThis.Size = new System.Drawing.Size(363, 32);
            this.cmsMainCloseThis.Text = "Close this tab";
            // 
            // cmsMainCloseExceptThis
            // 
            this.cmsMainCloseExceptThis.Name = "cmsMainCloseExceptThis";
            this.cmsMainCloseExceptThis.Size = new System.Drawing.Size(363, 32);
            this.cmsMainCloseExceptThis.Text = "Close all but this tab";
            // 
            // cmsMainCloseAll
            // 
            this.cmsMainCloseAll.Name = "cmsMainCloseAll";
            this.cmsMainCloseAll.Size = new System.Drawing.Size(363, 32);
            this.cmsMainCloseAll.Text = "Close all";
            // 
            // cmsMainDuplicateTool
            // 
            this.cmsMainDuplicateTool.Name = "cmsMainDuplicateTool";
            this.cmsMainDuplicateTool.Size = new System.Drawing.Size(363, 32);
            this.cmsMainDuplicateTool.Text = "Duplicate Tool";
            // 
            // cmsMainDuplicateToolWithConnection
            // 
            this.cmsMainDuplicateToolWithConnection.Name = "cmsMainDuplicateToolWithConnection";
            this.cmsMainDuplicateToolWithConnection.Size = new System.Drawing.Size(363, 32);
            this.cmsMainDuplicateToolWithConnection.Text = "Duplicate Tool with new connection";
            // 
            // NewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1058, 634);
            this.Controls.Add(this.dpMain);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlSupport);
            this.Controls.Add(this.pnlPluginsUpdate);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.pnlConnectLoading);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Name = "NewForm";
            this.Opacity = 0D;
            this.Text = "XrmToolBox for Microsoft Dataverse and Microsoft Dynamics 365";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewForm_FormClosing);
            this.Load += new System.EventHandler(this.NewForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NewForm_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NewForm_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.NewForm_KeyUp);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.NewForm_PreviewKeyDown);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.pnlSupport.ResumeLayout(false);
            this.pnlSupport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.pnlPluginsUpdate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbOpenPluginsStore)).EndInit();
            this.pnlConnectLoading.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbConnectionLoading)).EndInit();
            this.cmsMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.ContextMenuStrip cmsMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblConnecting;
        private System.Windows.Forms.Label lblPluginsUpdateAvailable;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.LinkLabel llClosePluginsUpdatePanel;
        private System.Windows.Forms.LinkLabel llDismiss;
        private System.Windows.Forms.LinkLabel llDonate;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlConnectLoading;
        private System.Windows.Forms.Panel pnlPluginsUpdate;
        private System.Windows.Forms.Panel pnlSupport;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.PictureBox pbConnectionLoading;
        private System.Windows.Forms.PictureBox pbOpenPluginsStore;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tsbConnect;
        private System.Windows.Forms.ToolStripButton tsbImpersonate;
        private System.Windows.Forms.ToolStripButton tsbOpenOrg;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripDropDownButton tsbManageWindows;
        private System.Windows.Forms.ToolStripDropDownButton tsddbHelp;
        private System.Windows.Forms.ToolStripDropDownButton tsddbTools;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllWindowsExceptActiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllWindowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeCurrentWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cmsMainCloseAll;
        private System.Windows.Forms.ToolStripMenuItem cmsMainCloseExceptThis;
        private System.Windows.Forms.ToolStripMenuItem cmsMainCloseThis;
        private System.Windows.Forms.ToolStripMenuItem cmsMainDuplicateTool;
        private System.Windows.Forms.ToolStripMenuItem cmsMainDuplicateToolWithConnection;
        private System.Windows.Forms.ToolStripMenuItem manageConnectionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pluginsStoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiChangeTabConnection;
        private System.Windows.Forms.ToolStripMenuItem tsmiCheckForUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsmiDonate;
        private System.Windows.Forms.ToolStripMenuItem tsmiDonateEurSelectedPlugin;
        private System.Windows.Forms.ToolStripMenuItem tsmiDonateEurXrmToolBox;
        private System.Windows.Forms.ToolStripMenuItem tsmiDonateGbpSelectedPlugin;
        private System.Windows.Forms.ToolStripMenuItem tsmiDonateGbpXrmToolBox;
        private System.Windows.Forms.ToolStripMenuItem tsmiDonateSelectedPlugin;
        private System.Windows.Forms.ToolStripMenuItem tsmiDonateUsdSelectedPlugin;
        private System.Windows.Forms.ToolStripMenuItem tsmiDonateUsdXrmToolBox;
        private System.Windows.Forms.ToolStripMenuItem tsmiDonateXrmToolBox;

        private System.Windows.Forms.ToolStripMenuItem tsmiPluginAbout;
        private System.Windows.Forms.ToolStripMenuItem tsmiPluginDonate;
        private System.Windows.Forms.ToolStripMenuItem tsmiPluginFeedback;
        private System.Windows.Forms.ToolStripMenuItem tsmiPluginHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowStartPage;
        private System.Windows.Forms.ToolStripMenuItem tsmiToolSettings;
        private System.Windows.Forms.ToolStripMenuItem tsmiXtbAbout;
        private System.Windows.Forms.ToolStripMenuItem tsmiXtbDonate;
        private System.Windows.Forms.ToolStripMenuItem tsmiXtbFeedback;
        private System.Windows.Forms.ToolStripMenuItem tsmiXtbHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmiXtbSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator tssDonate;
        private System.Windows.Forms.ToolStripSeparator tssFeedback;
        private System.Windows.Forms.ToolStripSeparator tssHelp;
        private System.Windows.Forms.ToolStripSeparator tssOpenOrg;
        private System.Windows.Forms.ToolStripSeparator tssSearch;
        private System.Windows.Forms.ToolStripSeparator tssWindows;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dpMain;
    }
}