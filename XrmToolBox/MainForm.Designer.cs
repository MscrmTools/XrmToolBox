﻿using System.Drawing;

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
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
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
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAbout = new System.Windows.Forms.ToolStripButton();
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
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tslFilterPlugin = new System.Windows.Forms.ToolStripLabel();
            this.tstxtFilterPlugin = new System.Windows.Forms.ToolStripTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.HomePageTab = new System.Windows.Forms.TabPage();
            this.pnlHelp = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.HomePageTab.SuspendLayout();
            this.pnlHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "access.png");
            this.imageList1.Images.SetKeyName(1, "acroread.png");
            this.imageList1.Images.SetKeyName(2, "agent.png");
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
            this.toolStripSeparator1,
            this.tslFilterPlugin,
            this.tstxtFilterPlugin,
            this.toolStripSeparator6,
            this.tsbAbout,
            this.toolStripSeparator3,
            this.tsbCodePlex,
            this.toolStripSeparator4,
            this.tsbDonate});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(884, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbConnect
            // 
            this.tsbConnect.Image = ((System.Drawing.Image)(resources.GetObject("tsbConnect.Image")));
            this.tsbConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbConnect.Name = "tsbConnect";
            this.tsbConnect.Size = new System.Drawing.Size(115, 22);
            this.tsbConnect.Text = "Connect to CRM";
            this.tsbConnect.Click += new System.EventHandler(this.TsbConnectClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbManageConnections
            // 
            this.tsbManageConnections.Image = ((System.Drawing.Image)(resources.GetObject("tsbManageConnections.Image")));
            this.tsbManageConnections.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbManageConnections.Name = "tsbManageConnections";
            this.tsbManageConnections.Size = new System.Drawing.Size(138, 22);
            this.tsbManageConnections.Text = "Manage connections";
            this.tsbManageConnections.Click += new System.EventHandler(this.tsbManageConnections_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
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
            this.tsbManageTabs.Size = new System.Drawing.Size(61, 22);
            this.tsbManageTabs.Text = "Tabs";
            // 
            // closeCurrentTabToolStripMenuItem
            // 
            this.closeCurrentTabToolStripMenuItem.Name = "closeCurrentTabToolStripMenuItem";
            this.closeCurrentTabToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.closeCurrentTabToolStripMenuItem.Text = "Close current tab";
            this.closeCurrentTabToolStripMenuItem.Click += new System.EventHandler(this.closeCurrentTabToolStripMenuItem_Click);
            // 
            // closeAllTabsToolStripMenuItem
            // 
            this.closeAllTabsToolStripMenuItem.Name = "closeAllTabsToolStripMenuItem";
            this.closeAllTabsToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.closeAllTabsToolStripMenuItem.Text = "Close all";
            this.closeAllTabsToolStripMenuItem.Click += new System.EventHandler(this.CloseAllTabsToolStripMenuItemClick);
            // 
            // closeAllTabsExceptActiveToolStripMenuItem
            // 
            this.closeAllTabsExceptActiveToolStripMenuItem.Name = "closeAllTabsExceptActiveToolStripMenuItem";
            this.closeAllTabsExceptActiveToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.closeAllTabsExceptActiveToolStripMenuItem.Text = "Close all except active tab";
            this.closeAllTabsExceptActiveToolStripMenuItem.Click += new System.EventHandler(this.CloseAllTabsExceptActiveToolStripMenuItemClick);
            // 
            // tsbOptions
            // 
            this.tsbOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOptions.Image = ((System.Drawing.Image)(resources.GetObject("tsbOptions.Image")));
            this.tsbOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOptions.Name = "tsbOptions";
            this.tsbOptions.Size = new System.Drawing.Size(23, 22);
            this.tsbOptions.Text = "Settings";
            this.tsbOptions.Click += new System.EventHandler(this.TsbOptionsClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbAbout
            // 
            this.tsbAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAbout.Image = ((System.Drawing.Image)(resources.GetObject("tsbAbout.Image")));
            this.tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAbout.Name = "tsbAbout";
            this.tsbAbout.Size = new System.Drawing.Size(23, 22);
            this.tsbAbout.Text = "About";
            this.tsbAbout.Click += new System.EventHandler(this.TsbAboutClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
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
            this.tsbCodePlex.Size = new System.Drawing.Size(86, 22);
            this.tsbCodePlex.Text = "Feedback";
            // 
            // startADiscussionToolStripMenuItem
            // 
            this.startADiscussionToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("startADiscussionToolStripMenuItem.Image")));
            this.startADiscussionToolStripMenuItem.Name = "startADiscussionToolStripMenuItem";
            this.startADiscussionToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.startADiscussionToolStripMenuItem.Text = "New issue / question / discussion";
            this.startADiscussionToolStripMenuItem.Click += new System.EventHandler(this.TsbDiscussClick);
            // 
            // GithubXrmToolBoxMenuItem
            // 
            this.GithubXrmToolBoxMenuItem.Name = "GithubXrmToolBoxMenuItem";
            this.GithubXrmToolBoxMenuItem.Size = new System.Drawing.Size(250, 22);
            this.GithubXrmToolBoxMenuItem.Text = "XrmToolbox";
            // 
            // githubPluginMenuItem
            // 
            this.githubPluginMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.discussionPluginToolStripMenuItem});
            this.githubPluginMenuItem.Name = "githubPluginMenuItem";
            this.githubPluginMenuItem.Size = new System.Drawing.Size(250, 22);
            this.githubPluginMenuItem.Text = "Selected Plugin";
            this.githubPluginMenuItem.Visible = false;
            // 
            // discussionPluginToolStripMenuItem
            // 
            this.discussionPluginToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("discussionPluginToolStripMenuItem.Image")));
            this.discussionPluginToolStripMenuItem.Name = "discussionPluginToolStripMenuItem";
            this.discussionPluginToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
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
            this.CodePlexPluginMenuItem.Size = new System.Drawing.Size(250, 22);
            this.CodePlexPluginMenuItem.Text = "Selected Plugin";
            // 
            // reportABugPluginToolStripMenuItem
            // 
            this.reportABugPluginToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("reportABugPluginToolStripMenuItem.Image")));
            this.reportABugPluginToolStripMenuItem.Name = "reportABugPluginToolStripMenuItem";
            this.reportABugPluginToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.reportABugPluginToolStripMenuItem.Text = "Report a bug";
            this.reportABugPluginToolStripMenuItem.Click += new System.EventHandler(this.TsbReportBugPluginClick);
            // 
            // startADiscussionPluginToolStripMenuItem
            // 
            this.startADiscussionPluginToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("startADiscussionPluginToolStripMenuItem.Image")));
            this.startADiscussionPluginToolStripMenuItem.Name = "startADiscussionPluginToolStripMenuItem";
            this.startADiscussionPluginToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.startADiscussionPluginToolStripMenuItem.Text = "Start a discussion / Request a feature";
            this.startADiscussionPluginToolStripMenuItem.Click += new System.EventHandler(this.TsbDiscussPluginClick);
            // 
            // rateThisToolPluginToolStripMenuItem
            // 
            this.rateThisToolPluginToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("rateThisToolPluginToolStripMenuItem.Image")));
            this.rateThisToolPluginToolStripMenuItem.Name = "rateThisToolPluginToolStripMenuItem";
            this.rateThisToolPluginToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.rateThisToolPluginToolStripMenuItem.Text = "Rate this tool";
            this.rateThisToolPluginToolStripMenuItem.Click += new System.EventHandler(this.TsbRatePluginClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
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
            this.tsbDonate.Size = new System.Drawing.Size(74, 22);
            this.tsbDonate.Text = "Donate";
            // 
            // donateInUSDollarsToolStripMenuItem
            // 
            this.donateInUSDollarsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("donateInUSDollarsToolStripMenuItem.Image")));
            this.donateInUSDollarsToolStripMenuItem.Name = "donateInUSDollarsToolStripMenuItem";
            this.donateInUSDollarsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.donateInUSDollarsToolStripMenuItem.Text = "Donate in US Dollars";
            this.donateInUSDollarsToolStripMenuItem.Click += new System.EventHandler(this.donateInUSDollarsToolStripMenuItem_Click);
            // 
            // donateInEuroToolStripMenuItem
            // 
            this.donateInEuroToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("donateInEuroToolStripMenuItem.Image")));
            this.donateInEuroToolStripMenuItem.Name = "donateInEuroToolStripMenuItem";
            this.donateInEuroToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.donateInEuroToolStripMenuItem.Text = "Donate in Euro";
            this.donateInEuroToolStripMenuItem.Click += new System.EventHandler(this.donateInEuroToolStripMenuItem_Click);
            // 
            // donateInGBPToolStripMenuItem
            // 
            this.donateInGBPToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("donateInGBPToolStripMenuItem.Image")));
            this.donateInGBPToolStripMenuItem.Name = "donateInGBPToolStripMenuItem";
            this.donateInGBPToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.donateInGBPToolStripMenuItem.Text = "Donate in GBP";
            this.donateInGBPToolStripMenuItem.Click += new System.EventHandler(this.donateInGBPToolStripMenuItem_Click);
            // 
            // PaypalXrmToolBoxToolStripMenuItem
            // 
            this.PaypalXrmToolBoxToolStripMenuItem.Name = "PaypalXrmToolBoxToolStripMenuItem";
            this.PaypalXrmToolBoxToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.PaypalXrmToolBoxToolStripMenuItem.Text = "XrmToolBox";
            // 
            // PayPalSelectedPluginToolStripMenuItem
            // 
            this.PayPalSelectedPluginToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.donateDollarPluginMenuItem,
            this.donateEuroPluginMenuItem,
            this.donateGbpPluginMenuItem});
            this.PayPalSelectedPluginToolStripMenuItem.Name = "PayPalSelectedPluginToolStripMenuItem";
            this.PayPalSelectedPluginToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.PayPalSelectedPluginToolStripMenuItem.Text = "SelectedPlugin";
            // 
            // donateDollarPluginMenuItem
            // 
            this.donateDollarPluginMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("donateDollarPluginMenuItem.Image")));
            this.donateDollarPluginMenuItem.Name = "donateDollarPluginMenuItem";
            this.donateDollarPluginMenuItem.Size = new System.Drawing.Size(181, 22);
            this.donateDollarPluginMenuItem.Text = "Donate in US Dollars";
            this.donateDollarPluginMenuItem.Click += new System.EventHandler(this.donateDollarPluginMenuItem_Click);
            // 
            // donateEuroPluginMenuItem
            // 
            this.donateEuroPluginMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("donateEuroPluginMenuItem.Image")));
            this.donateEuroPluginMenuItem.Name = "donateEuroPluginMenuItem";
            this.donateEuroPluginMenuItem.Size = new System.Drawing.Size(181, 22);
            this.donateEuroPluginMenuItem.Text = "Donate in Euro";
            this.donateEuroPluginMenuItem.Click += new System.EventHandler(this.donateEuroPluginMenuItem_Click);
            // 
            // donateGbpPluginMenuItem
            // 
            this.donateGbpPluginMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("donateGbpPluginMenuItem.Image")));
            this.donateGbpPluginMenuItem.Name = "donateGbpPluginMenuItem";
            this.donateGbpPluginMenuItem.Size = new System.Drawing.Size(181, 22);
            this.donateGbpPluginMenuItem.Text = "Donate in GBP";
            this.donateGbpPluginMenuItem.Click += new System.EventHandler(this.donateGbpPluginMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 32);
            // 
            // tslFilterPlugin
            // 
            this.tslFilterPlugin.Name = "tslFilterPlugin";
            this.tslFilterPlugin.Size = new System.Drawing.Size(64, 29);
            this.tslFilterPlugin.Text = "Search";
            // 
            // tstxtFilterPlugin
            // 
            this.tstxtFilterPlugin.Name = "tstxtFilterPlugin";
            this.tstxtFilterPlugin.Size = new System.Drawing.Size(150, 32);
            this.tstxtFilterPlugin.ToolTipText = "Filter by plugin name or company name";
            this.tstxtFilterPlugin.TextChanged += new System.EventHandler(this.tstxtFilterPlugin_TextChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.HomePageTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(884, 587);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseClick);
            // 
            // HomePageTab
            // 
            this.HomePageTab.AutoScroll = true;
            this.HomePageTab.Controls.Add(this.pnlHelp);
            this.HomePageTab.Location = new System.Drawing.Point(4, 22);
            this.HomePageTab.Name = "HomePageTab";
            this.HomePageTab.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.HomePageTab.Size = new System.Drawing.Size(876, 561);
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
            this.pnlHelp.Location = new System.Drawing.Point(3, 6);
            this.pnlHelp.Name = "pnlHelp";
            this.pnlHelp.Size = new System.Drawing.Size(865, 550);
            this.pnlHelp.TabIndex = 0;
            this.pnlHelp.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(857, 116);
            this.label2.TabIndex = 1;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(857, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "Oups... no plugin found!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(884, 612);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XrmToolBox for Microsoft Dynamics CRM 2011/2013/2015";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.HomePageTab.ResumeLayout(false);
            this.pnlHelp.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbConnect;
        private System.Windows.Forms.ToolStripButton tsbAbout;
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
    }
}

