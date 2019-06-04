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
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelpXrmToolBox = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelpSelectedPlugin = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFeedback = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFeedbackXrmToolBox = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFeedbackSelectedPlugin = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDonate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDonateUsdXrmToolBox = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDonateEurXrmToolBox = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDonateGbpXrmToolBox = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDonateXrmToolBox = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDonateSelectedPlugin = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDonateUsdSelectedPlugin = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDonateEurSelectedPlugin = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDonateGbpSelectedPlugin = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.checkForUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAboutXrmToolBox = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAboutSelectedPlugin = new System.Windows.Forms.ToolStripMenuItem();
            this.tssOpenOrg = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOpenOrg = new System.Windows.Forms.ToolStripButton();
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
            this.toolStripDropDownButton2,
            this.tssOpenOrg,
            this.tsbOpenOrg});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.tsMain.Size = new System.Drawing.Size(1293, 37);
            this.tsMain.TabIndex = 3;
            this.tsMain.Text = "tsMain";
            // 
            // tsbConnect
            // 
            this.tsbConnect.Image = ((System.Drawing.Image)(resources.GetObject("tsbConnect.Image")));
            this.tsbConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbConnect.Name = "tsbConnect";
            this.tsbConnect.Size = new System.Drawing.Size(122, 34);
            this.tsbConnect.Text = "Connect";
            this.tsbConnect.Click += new System.EventHandler(this.tsbConnect_Click);
            // 
            // tssSearch
            // 
            this.tssSearch.Name = "tssSearch";
            this.tssSearch.Size = new System.Drawing.Size(6, 37);
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
            this.tsbManageWindows.Size = new System.Drawing.Size(119, 34);
            this.tsbManageWindows.Text = "Windows";
            this.tsbManageWindows.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsbManageWindows_DropDownItemClicked);
            // 
            // closeCurrentWindowToolStripMenuItem
            // 
            this.closeCurrentWindowToolStripMenuItem.Name = "closeCurrentWindowToolStripMenuItem";
            this.closeCurrentWindowToolStripMenuItem.Size = new System.Drawing.Size(385, 34);
            this.closeCurrentWindowToolStripMenuItem.Text = "Close current window";
            // 
            // closeAllWindowsToolStripMenuItem
            // 
            this.closeAllWindowsToolStripMenuItem.Name = "closeAllWindowsToolStripMenuItem";
            this.closeAllWindowsToolStripMenuItem.Size = new System.Drawing.Size(385, 34);
            this.closeAllWindowsToolStripMenuItem.Text = "Close all";
            // 
            // closeAllWindowsExceptActiveToolStripMenuItem
            // 
            this.closeAllWindowsExceptActiveToolStripMenuItem.Name = "closeAllWindowsExceptActiveToolStripMenuItem";
            this.closeAllWindowsExceptActiveToolStripMenuItem.Size = new System.Drawing.Size(385, 34);
            this.closeAllWindowsExceptActiveToolStripMenuItem.Text = "Close all except active window";
            this.closeAllWindowsExceptActiveToolStripMenuItem.ToolTipText = "Only applies to Plugin pages";
            // 
            // tssWindows
            // 
            this.tssWindows.Name = "tssWindows";
            this.tssWindows.Size = new System.Drawing.Size(382, 6);
            // 
            // tsmiShowStartPage
            // 
            this.tsmiShowStartPage.Name = "tsmiShowStartPage";
            this.tsmiShowStartPage.Size = new System.Drawing.Size(385, 34);
            this.tsmiShowStartPage.Text = "Show Start Page";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 37);
            // 
            // tsddbTools
            // 
            this.tsddbTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageConnectionsToolStripMenuItem,
            this.toolStripSeparator10,
            this.pluginsStoreToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.tsddbTools.Image = ((System.Drawing.Image)(resources.GetObject("tsddbTools.Image")));
            this.tsddbTools.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbTools.Name = "tsddbTools";
            this.tsddbTools.Size = new System.Drawing.Size(109, 34);
            this.tsddbTools.Text = "Tools";
            this.tsddbTools.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsddbTools_DropDownItemClicked);
            // 
            // manageConnectionsToolStripMenuItem
            // 
            this.manageConnectionsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("manageConnectionsToolStripMenuItem.Image")));
            this.manageConnectionsToolStripMenuItem.Name = "manageConnectionsToolStripMenuItem";
            this.manageConnectionsToolStripMenuItem.Size = new System.Drawing.Size(298, 34);
            this.manageConnectionsToolStripMenuItem.Text = "Manage connections";
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(295, 6);
            // 
            // pluginsStoreToolStripMenuItem
            // 
            this.pluginsStoreToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pluginsStoreToolStripMenuItem.Image")));
            this.pluginsStoreToolStripMenuItem.Name = "pluginsStoreToolStripMenuItem";
            this.pluginsStoreToolStripMenuItem.Size = new System.Drawing.Size(298, 34);
            this.pluginsStoreToolStripMenuItem.Text = "Plugins Store";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("settingsToolStripMenuItem.Image")));
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(298, 34);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiHelp,
            this.tsmiFeedback,
            this.toolStripSeparator11,
            this.tsmiDonate,
            this.toolStripSeparator12,
            this.checkForUpdateToolStripMenuItem,
            this.toolStripSeparator13,
            this.tsmiAbout});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(119, 34);
            this.toolStripDropDownButton2.Text = "About";
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiHelpXrmToolBox,
            this.tsmiHelpSelectedPlugin});
            this.tsmiHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsmiHelp.Image")));
            this.tsmiHelp.Name = "tsmiHelp";
            this.tsmiHelp.Size = new System.Drawing.Size(362, 34);
            this.tsmiHelp.Text = "Help";
            this.tsmiHelp.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // tsmiHelpXrmToolBox
            // 
            this.tsmiHelpXrmToolBox.Image = ((System.Drawing.Image)(resources.GetObject("tsmiHelpXrmToolBox.Image")));
            this.tsmiHelpXrmToolBox.Name = "tsmiHelpXrmToolBox";
            this.tsmiHelpXrmToolBox.Size = new System.Drawing.Size(332, 34);
            this.tsmiHelpXrmToolBox.Text = "Display XrmToolBox help";
            this.tsmiHelpXrmToolBox.Click += new System.EventHandler(this.displayXrmToolBoxHelpToolStripMenuItem_Click);
            // 
            // tsmiHelpSelectedPlugin
            // 
            this.tsmiHelpSelectedPlugin.Name = "tsmiHelpSelectedPlugin";
            this.tsmiHelpSelectedPlugin.Size = new System.Drawing.Size(332, 34);
            this.tsmiHelpSelectedPlugin.Tag = "Display {0} help";
            this.tsmiHelpSelectedPlugin.Text = "Selected Plugin";
            this.tsmiHelpSelectedPlugin.Click += new System.EventHandler(this.HelpSelectedPluginToolStripMenuItem_Click);
            // 
            // tsmiFeedback
            // 
            this.tsmiFeedback.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFeedbackXrmToolBox,
            this.tsmiFeedbackSelectedPlugin});
            this.tsmiFeedback.Image = ((System.Drawing.Image)(resources.GetObject("tsmiFeedback.Image")));
            this.tsmiFeedback.Name = "tsmiFeedback";
            this.tsmiFeedback.Size = new System.Drawing.Size(362, 34);
            this.tsmiFeedback.Text = "Feedback / Issue / Question";
            this.tsmiFeedback.Click += new System.EventHandler(this.feedbackToolStripMenuItem_Click);
            // 
            // tsmiFeedbackXrmToolBox
            // 
            this.tsmiFeedbackXrmToolBox.Image = ((System.Drawing.Image)(resources.GetObject("tsmiFeedbackXrmToolBox.Image")));
            this.tsmiFeedbackXrmToolBox.Name = "tsmiFeedbackXrmToolBox";
            this.tsmiFeedbackXrmToolBox.Size = new System.Drawing.Size(246, 34);
            this.tsmiFeedbackXrmToolBox.Text = "XrmToolbox";
            this.tsmiFeedbackXrmToolBox.Click += new System.EventHandler(this.GithubXrmToolBoxMenuItem_Click);
            // 
            // tsmiFeedbackSelectedPlugin
            // 
            this.tsmiFeedbackSelectedPlugin.Name = "tsmiFeedbackSelectedPlugin";
            this.tsmiFeedbackSelectedPlugin.Size = new System.Drawing.Size(246, 34);
            this.tsmiFeedbackSelectedPlugin.Text = "Selected Plugin";
            this.tsmiFeedbackSelectedPlugin.Visible = false;
            this.tsmiFeedbackSelectedPlugin.Click += new System.EventHandler(this.githubPluginMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(359, 6);
            // 
            // tsmiDonate
            // 
            this.tsmiDonate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDonateUsdXrmToolBox,
            this.tsmiDonateEurXrmToolBox,
            this.tsmiDonateGbpXrmToolBox,
            this.tsmiDonateXrmToolBox,
            this.tsmiDonateSelectedPlugin});
            this.tsmiDonate.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDonate.Image")));
            this.tsmiDonate.Name = "tsmiDonate";
            this.tsmiDonate.Size = new System.Drawing.Size(362, 34);
            this.tsmiDonate.Text = "Donate";
            // 
            // tsmiDonateUsdXrmToolBox
            // 
            this.tsmiDonateUsdXrmToolBox.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDonateUsdXrmToolBox.Image")));
            this.tsmiDonateUsdXrmToolBox.Name = "tsmiDonateUsdXrmToolBox";
            this.tsmiDonateUsdXrmToolBox.Size = new System.Drawing.Size(296, 34);
            this.tsmiDonateUsdXrmToolBox.Text = "Donate in US Dollars";
            this.tsmiDonateUsdXrmToolBox.Click += new System.EventHandler(this.donateInUSDollarsToolStripMenuItem_Click);
            // 
            // tsmiDonateEurXrmToolBox
            // 
            this.tsmiDonateEurXrmToolBox.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDonateEurXrmToolBox.Image")));
            this.tsmiDonateEurXrmToolBox.Name = "tsmiDonateEurXrmToolBox";
            this.tsmiDonateEurXrmToolBox.Size = new System.Drawing.Size(296, 34);
            this.tsmiDonateEurXrmToolBox.Text = "Donate in Euro";
            this.tsmiDonateEurXrmToolBox.Click += new System.EventHandler(this.donateInEuroToolStripMenuItem_Click);
            // 
            // tsmiDonateGbpXrmToolBox
            // 
            this.tsmiDonateGbpXrmToolBox.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDonateGbpXrmToolBox.Image")));
            this.tsmiDonateGbpXrmToolBox.Name = "tsmiDonateGbpXrmToolBox";
            this.tsmiDonateGbpXrmToolBox.Size = new System.Drawing.Size(296, 34);
            this.tsmiDonateGbpXrmToolBox.Text = "Donate in GBP";
            this.tsmiDonateGbpXrmToolBox.Click += new System.EventHandler(this.donateInGBPToolStripMenuItem_Click);
            // 
            // tsmiDonateXrmToolBox
            // 
            this.tsmiDonateXrmToolBox.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDonateXrmToolBox.Image")));
            this.tsmiDonateXrmToolBox.Name = "tsmiDonateXrmToolBox";
            this.tsmiDonateXrmToolBox.Size = new System.Drawing.Size(296, 34);
            this.tsmiDonateXrmToolBox.Text = "XrmToolBox";
            // 
            // tsmiDonateSelectedPlugin
            // 
            this.tsmiDonateSelectedPlugin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDonateUsdSelectedPlugin,
            this.tsmiDonateEurSelectedPlugin,
            this.tsmiDonateGbpSelectedPlugin});
            this.tsmiDonateSelectedPlugin.Name = "tsmiDonateSelectedPlugin";
            this.tsmiDonateSelectedPlugin.Size = new System.Drawing.Size(296, 34);
            this.tsmiDonateSelectedPlugin.Text = "SelectedPlugin";
            // 
            // tsmiDonateUsdSelectedPlugin
            // 
            this.tsmiDonateUsdSelectedPlugin.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDonateUsdSelectedPlugin.Image")));
            this.tsmiDonateUsdSelectedPlugin.Name = "tsmiDonateUsdSelectedPlugin";
            this.tsmiDonateUsdSelectedPlugin.Size = new System.Drawing.Size(296, 34);
            this.tsmiDonateUsdSelectedPlugin.Text = "Donate in US Dollars";
            this.tsmiDonateUsdSelectedPlugin.Click += new System.EventHandler(this.donateDollarPluginMenuItem_Click);
            // 
            // tsmiDonateEurSelectedPlugin
            // 
            this.tsmiDonateEurSelectedPlugin.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDonateEurSelectedPlugin.Image")));
            this.tsmiDonateEurSelectedPlugin.Name = "tsmiDonateEurSelectedPlugin";
            this.tsmiDonateEurSelectedPlugin.Size = new System.Drawing.Size(296, 34);
            this.tsmiDonateEurSelectedPlugin.Text = "Donate in Euro";
            this.tsmiDonateEurSelectedPlugin.Click += new System.EventHandler(this.donateEuroPluginMenuItem_Click);
            // 
            // tsmiDonateGbpSelectedPlugin
            // 
            this.tsmiDonateGbpSelectedPlugin.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDonateGbpSelectedPlugin.Image")));
            this.tsmiDonateGbpSelectedPlugin.Name = "tsmiDonateGbpSelectedPlugin";
            this.tsmiDonateGbpSelectedPlugin.Size = new System.Drawing.Size(296, 34);
            this.tsmiDonateGbpSelectedPlugin.Text = "Donate in GBP";
            this.tsmiDonateGbpSelectedPlugin.Click += new System.EventHandler(this.donateGbpPluginMenuItem_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(359, 6);
            // 
            // checkForUpdateToolStripMenuItem
            // 
            this.checkForUpdateToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("checkForUpdateToolStripMenuItem.Image")));
            this.checkForUpdateToolStripMenuItem.Name = "checkForUpdateToolStripMenuItem";
            this.checkForUpdateToolStripMenuItem.Size = new System.Drawing.Size(362, 34);
            this.checkForUpdateToolStripMenuItem.Text = "Check for update";
            this.checkForUpdateToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdateToolStripMenuItem_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(359, 6);
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAboutXrmToolBox,
            this.tsmiAboutSelectedPlugin});
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(362, 34);
            this.tsmiAbout.Text = "About";
            this.tsmiAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
            // 
            // tsmiAboutXrmToolBox
            // 
            this.tsmiAboutXrmToolBox.Image = global::XrmToolBox.Properties.Resources.logo_0016;
            this.tsmiAboutXrmToolBox.Name = "tsmiAboutXrmToolBox";
            this.tsmiAboutXrmToolBox.Size = new System.Drawing.Size(276, 34);
            this.tsmiAboutXrmToolBox.Text = "About XrmToolBox";
            this.tsmiAboutXrmToolBox.Click += new System.EventHandler(this.tsmiAboutXrmToolBox_Click);
            // 
            // tsmiAboutSelectedPlugin
            // 
            this.tsmiAboutSelectedPlugin.Name = "tsmiAboutSelectedPlugin";
            this.tsmiAboutSelectedPlugin.Size = new System.Drawing.Size(276, 34);
            this.tsmiAboutSelectedPlugin.Text = "Selected plugin";
            this.tsmiAboutSelectedPlugin.Click += new System.EventHandler(this.tsmiAboutSelectedPlugin_Click);
            // 
            // tssOpenOrg
            // 
            this.tssOpenOrg.Name = "tssOpenOrg";
            this.tssOpenOrg.Size = new System.Drawing.Size(6, 37);
            this.tssOpenOrg.Visible = false;
            // 
            // tsbOpenOrg
            // 
            this.tsbOpenOrg.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpenOrg.Image")));
            this.tsbOpenOrg.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenOrg.Name = "tsbOpenOrg";
            this.tsbOpenOrg.Size = new System.Drawing.Size(218, 34);
            this.tsbOpenOrg.Text = "Open organization";
            this.tsbOpenOrg.ToolTipText = "Opens the connected organization in your web browser";
            this.tsbOpenOrg.Visible = false;
            this.tsbOpenOrg.Click += new System.EventHandler(this.tsbOpenOrg_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.SystemColors.Info;
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 37);
            this.pnlTop.Margin = new System.Windows.Forms.Padding(4);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1293, 72);
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
            this.pnlSupport.Location = new System.Drawing.Point(0, 587);
            this.pnlSupport.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.pnlSupport.Name = "pnlSupport";
            this.pnlSupport.Size = new System.Drawing.Size(1293, 124);
            this.pnlSupport.TabIndex = 11;
            this.pnlSupport.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(875, 18);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(290, 76);
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
            this.llDonate.Location = new System.Drawing.Point(612, 79);
            this.llDonate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.llDonate.Name = "llDonate";
            this.llDonate.Size = new System.Drawing.Size(233, 30);
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
            this.label5.Location = new System.Drawing.Point(605, 11);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(259, 68);
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
            this.label4.Location = new System.Drawing.Point(180, 72);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(216, 36);
            this.label4.TabIndex = 17;
            this.label4.Text = "Donate on PayPal";
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(4, 6);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(627, 65);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "How can I help MscrmTools?";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // llDismiss
            // 
            this.llDismiss.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llDismiss.AutoSize = true;
            this.llDismiss.Location = new System.Drawing.Point(1208, 6);
            this.llDismiss.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.llDismiss.Name = "llDismiss";
            this.llDismiss.Size = new System.Drawing.Size(80, 25);
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
            this.pnlPluginsUpdate.Location = new System.Drawing.Point(0, 711);
            this.pnlPluginsUpdate.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.pnlPluginsUpdate.Name = "pnlPluginsUpdate";
            this.pnlPluginsUpdate.Size = new System.Drawing.Size(1293, 50);
            this.pnlPluginsUpdate.TabIndex = 11;
            this.pnlPluginsUpdate.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(117)))), ((int)(((byte)(188)))));
            this.label1.Location = new System.Drawing.Point(930, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(269, 50);
            this.label1.TabIndex = 13;
            this.label1.Tag = "";
            this.label1.Text = "Open Plugins Store";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Click += new System.EventHandler(this.openPluginsStoreButton_Click);
            // 
            // llClosePluginsUpdatePanel
            // 
            this.llClosePluginsUpdatePanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.llClosePluginsUpdatePanel.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llClosePluginsUpdatePanel.LinkColor = System.Drawing.Color.Black;
            this.llClosePluginsUpdatePanel.Location = new System.Drawing.Point(1267, 0);
            this.llClosePluginsUpdatePanel.Name = "llClosePluginsUpdatePanel";
            this.llClosePluginsUpdatePanel.Size = new System.Drawing.Size(26, 50);
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
            this.pbOpenPluginsStore.Location = new System.Drawing.Point(1206, 0);
            this.pbOpenPluginsStore.Name = "pbOpenPluginsStore";
            this.pbOpenPluginsStore.Size = new System.Drawing.Size(50, 50);
            this.pbOpenPluginsStore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbOpenPluginsStore.TabIndex = 11;
            this.pbOpenPluginsStore.TabStop = false;
            this.pbOpenPluginsStore.Click += new System.EventHandler(this.openPluginsStoreButton_Click);
            // 
            // lblPluginsUpdateAvailable
            // 
            this.lblPluginsUpdateAvailable.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblPluginsUpdateAvailable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPluginsUpdateAvailable.Location = new System.Drawing.Point(4, 3);
            this.lblPluginsUpdateAvailable.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPluginsUpdateAvailable.Name = "lblPluginsUpdateAvailable";
            this.lblPluginsUpdateAvailable.Size = new System.Drawing.Size(724, 50);
            this.lblPluginsUpdateAvailable.TabIndex = 10;
            this.lblPluginsUpdateAvailable.Tag = "{0} Update{1} {2} available for your plugins";
            this.lblPluginsUpdateAvailable.Text = "X Updates are available for your plugins";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.SystemColors.Info;
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 515);
            this.pnlBottom.Margin = new System.Windows.Forms.Padding(4);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1293, 72);
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
            this.pnlConnectLoading.Location = new System.Drawing.Point(-165, -81);
            this.pnlConnectLoading.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.pnlConnectLoading.Name = "pnlConnectLoading";
            this.pnlConnectLoading.Size = new System.Drawing.Size(1621, 921);
            this.pnlConnectLoading.TabIndex = 17;
            this.pnlConnectLoading.Visible = false;
            // 
            // lblConnecting
            // 
            this.lblConnecting.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblConnecting.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnecting.Location = new System.Drawing.Point(0, 508);
            this.lblConnecting.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConnecting.Name = "lblConnecting";
            this.lblConnecting.Size = new System.Drawing.Size(1621, 52);
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
            this.pbConnectionLoading.Location = new System.Drawing.Point(0, 290);
            this.pbConnectionLoading.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.pbConnectionLoading.Name = "pbConnectionLoading";
            this.pbConnectionLoading.Size = new System.Drawing.Size(1621, 196);
            this.pbConnectionLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbConnectionLoading.TabIndex = 0;
            this.pbConnectionLoading.TabStop = false;
            // 
            // dpMain
            // 
            this.dpMain.BackColor = System.Drawing.Color.White;
            this.dpMain.DefaultFloatWindowSize = new System.Drawing.Size(800, 600);
            this.dpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dpMain.Location = new System.Drawing.Point(0, 109);
            this.dpMain.Margin = new System.Windows.Forms.Padding(4);
            this.dpMain.Name = "dpMain";
            this.dpMain.Size = new System.Drawing.Size(1293, 406);
            this.dpMain.TabIndex = 21;
            this.dpMain.ActiveDocumentChanged += new System.EventHandler(this.dpMain_ActiveDocumentChanged);
            this.dpMain.ActiveContentChanged += new System.EventHandler(this.dpMain_ActiveContentChanged);
            this.dpMain.DocumentDragged += new System.EventHandler(this.dpMain_DocumentDragged);
            this.dpMain.ActivePaneChanged += new System.EventHandler(this.dpMain_ActivePaneChanged);
            // 
            // cmsMain
            // 
            this.cmsMain.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.cmsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsMainCloseThis,
            this.cmsMainCloseExceptThis,
            this.cmsMainCloseAll});
            this.cmsMain.Name = "cmsMain";
            this.cmsMain.Size = new System.Drawing.Size(276, 106);
            this.cmsMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmsMain_ItemClicked);
            // 
            // cmsMainCloseThis
            // 
            this.cmsMainCloseThis.Name = "cmsMainCloseThis";
            this.cmsMainCloseThis.Size = new System.Drawing.Size(275, 34);
            this.cmsMainCloseThis.Text = "Close this tab";
            // 
            // cmsMainCloseExceptThis
            // 
            this.cmsMainCloseExceptThis.Name = "cmsMainCloseExceptThis";
            this.cmsMainCloseExceptThis.Size = new System.Drawing.Size(275, 34);
            this.cmsMainCloseExceptThis.Text = "Close all but this tab";
            // 
            // cmsMainCloseAll
            // 
            this.cmsMainCloseAll.Name = "cmsMainCloseAll";
            this.cmsMainCloseAll.Size = new System.Drawing.Size(275, 34);
            this.cmsMainCloseAll.Text = "Close all";
            // 
            // NewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1293, 761);
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
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "NewForm";
            this.Opacity = 0D;
            this.Text = "XrmToolBox for Microsoft Dynamics 365 for CE and PowerApps Common Data Service" ;
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

        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.ToolStripSeparator tssSearch;
        private System.Windows.Forms.ToolStripSeparator tssOpenOrg;
        private System.Windows.Forms.ToolStripButton tsbOpenOrg;
        private System.Windows.Forms.Panel pnlSupport;
        private System.Windows.Forms.Panel pnlPluginsUpdate;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.LinkLabel llDonate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.LinkLabel llDismiss;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlConnectLoading;
        private System.Windows.Forms.Label lblConnecting;
        private System.Windows.Forms.PictureBox pbConnectionLoading;
        private System.Windows.Forms.ToolStripButton tsbConnect;
        private System.Windows.Forms.ToolStripDropDownButton tsbManageWindows;
        private System.Windows.Forms.ToolStripMenuItem closeCurrentWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllWindowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllWindowsExceptActiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton tsddbTools;
        private System.Windows.Forms.ToolStripMenuItem manageConnectionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem pluginsStoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelpXrmToolBox;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelpSelectedPlugin;
        private System.Windows.Forms.ToolStripMenuItem tsmiFeedback;
        private System.Windows.Forms.ToolStripMenuItem tsmiFeedbackXrmToolBox;
        private System.Windows.Forms.ToolStripMenuItem tsmiFeedbackSelectedPlugin;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem tsmiDonate;
        private System.Windows.Forms.ToolStripMenuItem tsmiDonateUsdXrmToolBox;
        private System.Windows.Forms.ToolStripMenuItem tsmiDonateEurXrmToolBox;
        private System.Windows.Forms.ToolStripMenuItem tsmiDonateGbpXrmToolBox;
        private System.Windows.Forms.ToolStripMenuItem tsmiDonateXrmToolBox;
        private System.Windows.Forms.ToolStripMenuItem tsmiDonateSelectedPlugin;
        private System.Windows.Forms.ToolStripMenuItem tsmiDonateUsdSelectedPlugin;
        private System.Windows.Forms.ToolStripMenuItem tsmiDonateEurSelectedPlugin;
        private System.Windows.Forms.ToolStripMenuItem tsmiDonateGbpSelectedPlugin;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dpMain;
        private System.Windows.Forms.ToolStripSeparator tssWindows;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowStartPage;
        private System.Windows.Forms.ToolStripMenuItem tsmiAboutXrmToolBox;
        private System.Windows.Forms.ToolStripMenuItem tsmiAboutSelectedPlugin;
        private System.Windows.Forms.ContextMenuStrip cmsMain;
        private System.Windows.Forms.ToolStripMenuItem cmsMainCloseThis;
        private System.Windows.Forms.ToolStripMenuItem cmsMainCloseExceptThis;
        private System.Windows.Forms.ToolStripMenuItem cmsMainCloseAll;
        private System.Windows.Forms.Label lblPluginsUpdateAvailable;
        private System.Windows.Forms.PictureBox pbOpenPluginsStore;
        private System.Windows.Forms.LinkLabel llClosePluginsUpdatePanel;
        private System.Windows.Forms.Label label1;
    }
}