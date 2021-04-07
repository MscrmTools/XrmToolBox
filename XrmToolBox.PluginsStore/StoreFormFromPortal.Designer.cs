using System;
using System.Windows.Forms;

namespace XrmToolBox.PluginsStore
{
    partial class StoreFormFromPortal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StoreFormFromPortal));
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbLoadPlugins = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbInstall = new System.Windows.Forms.ToolStripButton();
            this.tsbUninstall = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tslSearch = new System.Windows.Forms.ToolStripLabel();
            this.tstSearch = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbShowThisScreenOnStartup = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsddbOptions = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiShowPluginsNotCompatible = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowNewPlugins = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowPluginsUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowInstalledPlugins = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbProxySettings = new System.Windows.Forms.ToolStripButton();
            this.tsbCleanCacheFolder = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.tssLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssPluginsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlReleaseNotes = new System.Windows.Forms.Panel();
            this.scProperties = new System.Windows.Forms.SplitContainer();
            this.lblProperties = new System.Windows.Forms.Label();
            this.pnlReleaseNotesDetails = new System.Windows.Forms.Panel();
            this.lblReleaseNotes = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblNoRating = new System.Windows.Forms.Label();
            this.llRatePlugin = new System.Windows.Forms.LinkLabel();
            this.pbStar = new System.Windows.Forms.PictureBox();
            this.lblRating = new System.Windows.Forms.Label();
            this.pnlNotif = new System.Windows.Forms.Panel();
            this.lblNotif = new System.Windows.Forms.Label();
            this.pbNotifIcon = new System.Windows.Forms.PictureBox();
            this.lvPlugins = new System.Windows.Forms.ListView();
            this.colCheckBox = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRating = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCurrent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLatestDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAuthor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDownloadCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAverageDownloadCoun = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.iiNotif = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ilImages = new System.Windows.Forms.ImageList(this.components);
            this.ilImages24 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.pnlCategories = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkToolsWithUpdate = new System.Windows.Forms.CheckBox();
            this.chkToolsInstalled = new System.Windows.Forms.CheckBox();
            this.chkToolsNotInstalled = new System.Windows.Forms.CheckBox();
            this.chkToolsNotCompatible = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tsMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.pnlReleaseNotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scProperties)).BeginInit();
            this.scProperties.Panel1.SuspendLayout();
            this.scProperties.Panel2.SuspendLayout();
            this.scProperties.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStar)).BeginInit();
            this.pnlNotif.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbNotifIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsMain
            // 
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbLoadPlugins,
            this.toolStripSeparator1,
            this.tsbInstall,
            this.tsbUninstall,
            this.toolStripSeparator2,
            this.tslSearch,
            this.tstSearch,
            this.toolStripSeparator4,
            this.tsbShowThisScreenOnStartup,
            this.toolStripSeparator3,
            this.tsddbOptions,
            this.tsbProxySettings,
            this.tsbCleanCacheFolder});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tsMain.Size = new System.Drawing.Size(1811, 46);
            this.tsMain.TabIndex = 0;
            this.tsMain.Text = "toolStrip1";
            // 
            // tsbLoadPlugins
            // 
            this.tsbLoadPlugins.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadPlugins.Image")));
            this.tsbLoadPlugins.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadPlugins.Name = "tsbLoadPlugins";
            this.tsbLoadPlugins.Size = new System.Drawing.Size(90, 41);
            this.tsbLoadPlugins.Text = "Refresh";
            this.tsbLoadPlugins.Click += new System.EventHandler(this.tsbLoadPlugins_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 46);
            // 
            // tsbInstall
            // 
            this.tsbInstall.Image = ((System.Drawing.Image)(resources.GetObject("tsbInstall.Image")));
            this.tsbInstall.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInstall.Name = "tsbInstall";
            this.tsbInstall.Size = new System.Drawing.Size(78, 41);
            this.tsbInstall.Text = "Install";
            this.tsbInstall.Click += new System.EventHandler(this.tsbInstall_Click);
            // 
            // tsbUninstall
            // 
            this.tsbUninstall.Image = ((System.Drawing.Image)(resources.GetObject("tsbUninstall.Image")));
            this.tsbUninstall.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUninstall.Name = "tsbUninstall";
            this.tsbUninstall.Size = new System.Drawing.Size(99, 41);
            this.tsbUninstall.Text = "Uninstall";
            this.tsbUninstall.Visible = false;
            this.tsbUninstall.Click += new System.EventHandler(this.tsbUninstall_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 46);
            // 
            // tslSearch
            // 
            this.tslSearch.Name = "tslSearch";
            this.tslSearch.Size = new System.Drawing.Size(64, 41);
            this.tslSearch.Text = "Search";
            // 
            // tstSearch
            // 
            this.tstSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tstSearch.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.tstSearch.Name = "tstSearch";
            this.tstSearch.Size = new System.Drawing.Size(250, 46);
            this.tstSearch.Tag = "Search by Title or Authors";
            this.tstSearch.Text = "Search by Title or Authors";
            this.tstSearch.Enter += new System.EventHandler(this.tstSearch_Enter);
            this.tstSearch.Leave += new System.EventHandler(this.tstSearch_Leave);
            this.tstSearch.TextChanged += new System.EventHandler(this.tstSearch_TextChanged);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 46);
            // 
            // tsbShowThisScreenOnStartup
            // 
            this.tsbShowThisScreenOnStartup.CheckOnClick = true;
            this.tsbShowThisScreenOnStartup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbShowThisScreenOnStartup.Image = ((System.Drawing.Image)(resources.GetObject("tsbShowThisScreenOnStartup.Image")));
            this.tsbShowThisScreenOnStartup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowThisScreenOnStartup.Name = "tsbShowThisScreenOnStartup";
            this.tsbShowThisScreenOnStartup.Size = new System.Drawing.Size(247, 41);
            this.tsbShowThisScreenOnStartup.Text = "Show on XrmToolBox startup";
            this.tsbShowThisScreenOnStartup.Click += new System.EventHandler(this.tsbShowThisScreenOnStartup_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 46);
            // 
            // tsddbOptions
            // 
            this.tsddbOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsddbOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiShowPluginsNotCompatible,
            this.tsmiShowNewPlugins,
            this.tsmiShowPluginsUpdate,
            this.tsmiShowInstalledPlugins});
            this.tsddbOptions.Image = ((System.Drawing.Image)(resources.GetObject("tsddbOptions.Image")));
            this.tsddbOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbOptions.Name = "tsddbOptions";
            this.tsddbOptions.Size = new System.Drawing.Size(154, 41);
            this.tsddbOptions.Text = "Display options";
            // 
            // tsmiShowPluginsNotCompatible
            // 
            this.tsmiShowPluginsNotCompatible.Checked = true;
            this.tsmiShowPluginsNotCompatible.CheckOnClick = true;
            this.tsmiShowPluginsNotCompatible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiShowPluginsNotCompatible.Name = "tsmiShowPluginsNotCompatible";
            this.tsmiShowPluginsNotCompatible.Size = new System.Drawing.Size(329, 34);
            this.tsmiShowPluginsNotCompatible.Text = "Show tools not compatible";
            this.tsmiShowPluginsNotCompatible.Click += new System.EventHandler(this.tsmiPluginDisplayOption_Click);
            // 
            // tsmiShowNewPlugins
            // 
            this.tsmiShowNewPlugins.Checked = true;
            this.tsmiShowNewPlugins.CheckOnClick = true;
            this.tsmiShowNewPlugins.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiShowNewPlugins.Name = "tsmiShowNewPlugins";
            this.tsmiShowNewPlugins.Size = new System.Drawing.Size(329, 34);
            this.tsmiShowNewPlugins.Text = "Show tools not installed";
            this.tsmiShowNewPlugins.Click += new System.EventHandler(this.tsmiPluginDisplayOption_Click);
            // 
            // tsmiShowPluginsUpdate
            // 
            this.tsmiShowPluginsUpdate.Checked = true;
            this.tsmiShowPluginsUpdate.CheckOnClick = true;
            this.tsmiShowPluginsUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiShowPluginsUpdate.Name = "tsmiShowPluginsUpdate";
            this.tsmiShowPluginsUpdate.Size = new System.Drawing.Size(329, 34);
            this.tsmiShowPluginsUpdate.Text = "Show tools update";
            this.tsmiShowPluginsUpdate.Click += new System.EventHandler(this.tsmiPluginDisplayOption_Click);
            // 
            // tsmiShowInstalledPlugins
            // 
            this.tsmiShowInstalledPlugins.Checked = true;
            this.tsmiShowInstalledPlugins.CheckOnClick = true;
            this.tsmiShowInstalledPlugins.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiShowInstalledPlugins.Name = "tsmiShowInstalledPlugins";
            this.tsmiShowInstalledPlugins.Size = new System.Drawing.Size(329, 34);
            this.tsmiShowInstalledPlugins.Text = "Show installed tools";
            this.tsmiShowInstalledPlugins.Click += new System.EventHandler(this.tsmiPluginDisplayOption_Click);
            // 
            // tsbProxySettings
            // 
            this.tsbProxySettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbProxySettings.Image = ((System.Drawing.Image)(resources.GetObject("tsbProxySettings.Image")));
            this.tsbProxySettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbProxySettings.Name = "tsbProxySettings";
            this.tsbProxySettings.Size = new System.Drawing.Size(34, 41);
            this.tsbProxySettings.Text = "Proxy settings";
            this.tsbProxySettings.Click += new System.EventHandler(this.tsbProxySettings_Click);
            // 
            // tsbCleanCacheFolder
            // 
            this.tsbCleanCacheFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCleanCacheFolder.Image = ((System.Drawing.Image)(resources.GetObject("tsbCleanCacheFolder.Image")));
            this.tsbCleanCacheFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCleanCacheFolder.Name = "tsbCleanCacheFolder";
            this.tsbCleanCacheFolder.Size = new System.Drawing.Size(34, 41);
            this.tsbCleanCacheFolder.Text = "Clean cache folder";
            this.tsbCleanCacheFolder.Click += new System.EventHandler(this.tsbCleanCacheFolder_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssProgress,
            this.tssLabel,
            this.tssPluginsCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 875);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 14, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1509, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssProgress
            // 
            this.tssProgress.Name = "tssProgress";
            this.tssProgress.Size = new System.Drawing.Size(200, 14);
            this.tssProgress.Step = 1;
            this.tssProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.tssProgress.Visible = false;
            // 
            // tssLabel
            // 
            this.tssLabel.Name = "tssLabel";
            this.tssLabel.Size = new System.Drawing.Size(0, 15);
            this.tssLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tssPluginsCount
            // 
            this.tssPluginsCount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tssPluginsCount.Name = "tssPluginsCount";
            this.tssPluginsCount.Size = new System.Drawing.Size(0, 15);
            this.tssPluginsCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlReleaseNotes
            // 
            this.pnlReleaseNotes.Controls.Add(this.scProperties);
            this.pnlReleaseNotes.Controls.Add(this.pnlNotif);
            this.pnlReleaseNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReleaseNotes.Location = new System.Drawing.Point(0, 0);
            this.pnlReleaseNotes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlReleaseNotes.Name = "pnlReleaseNotes";
            this.pnlReleaseNotes.Size = new System.Drawing.Size(1656, 479);
            this.pnlReleaseNotes.TabIndex = 4;
            // 
            // scProperties
            // 
            this.scProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scProperties.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scProperties.Location = new System.Drawing.Point(0, 31);
            this.scProperties.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.scProperties.Name = "scProperties";
            // 
            // scProperties.Panel1
            // 
            this.scProperties.Panel1.AutoScroll = true;
            this.scProperties.Panel1.Controls.Add(this.lblProperties);
            this.scProperties.Panel1MinSize = 500;
            // 
            // scProperties.Panel2
            // 
            this.scProperties.Panel2.Controls.Add(this.pnlReleaseNotesDetails);
            this.scProperties.Panel2.Controls.Add(this.lblReleaseNotes);
            this.scProperties.Panel2.Controls.Add(this.panel1);
            this.scProperties.Panel2.Controls.Add(this.lblRating);
            this.scProperties.Size = new System.Drawing.Size(1656, 448);
            this.scProperties.SplitterDistance = 500;
            this.scProperties.SplitterWidth = 5;
            this.scProperties.TabIndex = 7;
            // 
            // lblProperties
            // 
            this.lblProperties.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProperties.Location = new System.Drawing.Point(0, 0);
            this.lblProperties.Name = "lblProperties";
            this.lblProperties.Size = new System.Drawing.Size(500, 25);
            this.lblProperties.TabIndex = 1;
            this.lblProperties.Text = "Tool properties";
            // 
            // pnlReleaseNotesDetails
            // 
            this.pnlReleaseNotesDetails.AutoScroll = true;
            this.pnlReleaseNotesDetails.AutoScrollMinSize = new System.Drawing.Size(0, 1000);
            this.pnlReleaseNotesDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReleaseNotesDetails.Location = new System.Drawing.Point(0, 87);
            this.pnlReleaseNotesDetails.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlReleaseNotesDetails.Name = "pnlReleaseNotesDetails";
            this.pnlReleaseNotesDetails.Size = new System.Drawing.Size(1151, 361);
            this.pnlReleaseNotesDetails.TabIndex = 16;
            // 
            // lblReleaseNotes
            // 
            this.lblReleaseNotes.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblReleaseNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReleaseNotes.Location = new System.Drawing.Point(0, 62);
            this.lblReleaseNotes.Name = "lblReleaseNotes";
            this.lblReleaseNotes.Size = new System.Drawing.Size(1151, 25);
            this.lblReleaseNotes.TabIndex = 15;
            this.lblReleaseNotes.Text = "Release notes";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.lblNoRating);
            this.panel1.Controls.Add(this.llRatePlugin);
            this.panel1.Controls.Add(this.pbStar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1151, 37);
            this.panel1.TabIndex = 13;
            // 
            // lblNoRating
            // 
            this.lblNoRating.AutoSize = true;
            this.lblNoRating.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblNoRating.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoRating.Location = new System.Drawing.Point(180, 0);
            this.lblNoRating.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblNoRating.Name = "lblNoRating";
            this.lblNoRating.Size = new System.Drawing.Size(148, 29);
            this.lblNoRating.TabIndex = 6;
            this.lblNoRating.Text = "No rating yet";
            this.lblNoRating.Visible = false;
            // 
            // llRatePlugin
            // 
            this.llRatePlugin.AutoSize = true;
            this.llRatePlugin.Dock = System.Windows.Forms.DockStyle.Right;
            this.llRatePlugin.Location = new System.Drawing.Point(1048, 0);
            this.llRatePlugin.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.llRatePlugin.Name = "llRatePlugin";
            this.llRatePlugin.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.llRatePlugin.Size = new System.Drawing.Size(103, 26);
            this.llRatePlugin.TabIndex = 5;
            this.llRatePlugin.TabStop = true;
            this.llRatePlugin.Text = "Rate this tool";
            this.llRatePlugin.VisitedLinkColor = System.Drawing.Color.Blue;
            this.llRatePlugin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llRatePlugin_LinkClicked);
            // 
            // pbStar
            // 
            this.pbStar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbStar.Image = ((System.Drawing.Image)(resources.GetObject("pbStar.Image")));
            this.pbStar.Location = new System.Drawing.Point(0, 0);
            this.pbStar.Margin = new System.Windows.Forms.Padding(5);
            this.pbStar.Name = "pbStar";
            this.pbStar.Size = new System.Drawing.Size(180, 37);
            this.pbStar.TabIndex = 0;
            this.pbStar.TabStop = false;
            this.pbStar.Visible = false;
            // 
            // lblRating
            // 
            this.lblRating.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRating.Location = new System.Drawing.Point(0, 0);
            this.lblRating.Name = "lblRating";
            this.lblRating.Size = new System.Drawing.Size(1151, 25);
            this.lblRating.TabIndex = 7;
            this.lblRating.Text = "Rating";
            // 
            // pnlNotif
            // 
            this.pnlNotif.BackColor = System.Drawing.SystemColors.Info;
            this.pnlNotif.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNotif.Controls.Add(this.lblNotif);
            this.pnlNotif.Controls.Add(this.pbNotifIcon);
            this.pnlNotif.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNotif.Location = new System.Drawing.Point(0, 0);
            this.pnlNotif.Name = "pnlNotif";
            this.pnlNotif.Size = new System.Drawing.Size(1656, 31);
            this.pnlNotif.TabIndex = 3;
            this.pnlNotif.Visible = false;
            // 
            // lblNotif
            // 
            this.lblNotif.AutoSize = true;
            this.lblNotif.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNotif.Location = new System.Drawing.Point(32, 0);
            this.lblNotif.Name = "lblNotif";
            this.lblNotif.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lblNotif.Size = new System.Drawing.Size(96, 26);
            this.lblNotif.TabIndex = 1;
            this.lblNotif.Text = "[Notification]";
            this.lblNotif.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbNotifIcon
            // 
            this.pbNotifIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbNotifIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbNotifIcon.Location = new System.Drawing.Point(0, 0);
            this.pbNotifIcon.Name = "pbNotifIcon";
            this.pbNotifIcon.Size = new System.Drawing.Size(32, 29);
            this.pbNotifIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbNotifIcon.TabIndex = 0;
            this.pbNotifIcon.TabStop = false;
            // 
            // lvPlugins
            // 
            this.lvPlugins.CheckBoxes = true;
            this.lvPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCheckBox,
            this.colTitle,
            this.colRating,
            this.colVersion,
            this.colCurrent,
            this.colLatestDate,
            this.colDescription,
            this.colAuthor,
            this.colAction,
            this.colDownloadCount,
            this.colAverageDownloadCoun});
            this.lvPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPlugins.FullRowSelect = true;
            this.lvPlugins.GridLines = true;
            this.lvPlugins.HideSelection = false;
            this.lvPlugins.Location = new System.Drawing.Point(0, 0);
            this.lvPlugins.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lvPlugins.Name = "lvPlugins";
            this.lvPlugins.OwnerDraw = true;
            this.lvPlugins.Size = new System.Drawing.Size(1656, 511);
            this.lvPlugins.SmallImageList = this.iiNotif;
            this.lvPlugins.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvPlugins.TabIndex = 5;
            this.lvPlugins.UseCompatibleStateImageBehavior = false;
            this.lvPlugins.View = System.Windows.Forms.View.Details;
            this.lvPlugins.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvPlugins_ColumnClick);
            this.lvPlugins.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lvPlugins_DrawColumnHeader);
            this.lvPlugins.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lvPlugins_DrawSubItem);
            this.lvPlugins.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvPlugins_ItemChecked);
            this.lvPlugins.SelectedIndexChanged += new System.EventHandler(this.lvPlugins_SelectedIndexChanged);
            // 
            // colCheckBox
            // 
            this.colCheckBox.Text = "";
            this.colCheckBox.Width = 26;
            // 
            // colTitle
            // 
            this.colTitle.Text = "Title";
            this.colTitle.Width = 217;
            // 
            // colRating
            // 
            this.colRating.Text = "Rating";
            this.colRating.Width = 120;
            // 
            // colVersion
            // 
            this.colVersion.Text = "Latest Version";
            this.colVersion.Width = 100;
            // 
            // colCurrent
            // 
            this.colCurrent.Text = "Installed Version";
            this.colCurrent.Width = 100;
            // 
            // colLatestDate
            // 
            this.colLatestDate.Text = "Release date";
            this.colLatestDate.Width = 100;
            // 
            // colDescription
            // 
            this.colDescription.Text = "Description";
            this.colDescription.Width = 317;
            // 
            // colAuthor
            // 
            this.colAuthor.Text = "Author";
            this.colAuthor.Width = 133;
            // 
            // colAction
            // 
            this.colAction.Text = "Action";
            // 
            // colDownloadCount
            // 
            this.colDownloadCount.Text = "All Downloads";
            this.colDownloadCount.Width = 100;
            // 
            // colAverageDownloadCoun
            // 
            this.colAverageDownloadCoun.Text = "Average Download Count";
            this.colAverageDownloadCoun.Width = 100;
            // 
            // iiNotif
            // 
            this.iiNotif.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iiNotif.ImageStream")));
            this.iiNotif.TransparentColor = System.Drawing.Color.Transparent;
            this.iiNotif.Images.SetKeyName(0, "notif_icn_info16.png");
            this.iiNotif.Images.SetKeyName(1, "notif_icn_alert16.png");
            this.iiNotif.Images.SetKeyName(2, "notif_icn_crit16.png");
            this.iiNotif.Images.SetKeyName(3, "new.png");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvPlugins);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlReleaseNotes);
            this.splitContainer1.Size = new System.Drawing.Size(1656, 995);
            this.splitContainer1.SplitterDistance = 511;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 6;
            // 
            // ilImages
            // 
            this.ilImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilImages.ImageStream")));
            this.ilImages.TransparentColor = System.Drawing.Color.Transparent;
            this.ilImages.Images.SetKeyName(0, "star_5.png");
            // 
            // ilImages24
            // 
            this.ilImages24.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilImages24.ImageStream")));
            this.ilImages24.TransparentColor = System.Drawing.Color.Transparent;
            this.ilImages24.Images.SetKeyName(0, "star24_5.png");
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 55);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.pnlCategories);
            this.splitContainerMain.Panel1.Controls.Add(this.label2);
            this.splitContainerMain.Panel1.Controls.Add(this.panel2);
            this.splitContainerMain.Panel1.Controls.Add(this.label1);
            this.splitContainerMain.Panel1.Padding = new System.Windows.Forms.Padding(10);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainerMain.Size = new System.Drawing.Size(1811, 995);
            this.splitContainerMain.SplitterDistance = 151;
            this.splitContainerMain.TabIndex = 7;
            // 
            // pnlCategories
            // 
            this.pnlCategories.AutoScroll = true;
            this.pnlCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCategories.Location = new System.Drawing.Point(10, 188);
            this.pnlCategories.Name = "pnlCategories";
            this.pnlCategories.Size = new System.Drawing.Size(131, 797);
            this.pnlCategories.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 159);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 29);
            this.label2.TabIndex = 4;
            this.label2.Text = "Categories";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkToolsWithUpdate);
            this.panel2.Controls.Add(this.chkToolsInstalled);
            this.panel2.Controls.Add(this.chkToolsNotInstalled);
            this.panel2.Controls.Add(this.chkToolsNotCompatible);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(10, 39);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(131, 120);
            this.panel2.TabIndex = 2;
            // 
            // chkToolsWithUpdate
            // 
            this.chkToolsWithUpdate.AutoSize = true;
            this.chkToolsWithUpdate.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkToolsWithUpdate.Location = new System.Drawing.Point(0, 72);
            this.chkToolsWithUpdate.Name = "chkToolsWithUpdate";
            this.chkToolsWithUpdate.Size = new System.Drawing.Size(131, 24);
            this.chkToolsWithUpdate.TabIndex = 3;
            this.chkToolsWithUpdate.Text = "with update";
            this.chkToolsWithUpdate.UseVisualStyleBackColor = true;
            this.chkToolsWithUpdate.Click += new System.EventHandler(this.chkTools_Click);
            // 
            // chkToolsInstalled
            // 
            this.chkToolsInstalled.AutoSize = true;
            this.chkToolsInstalled.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkToolsInstalled.Location = new System.Drawing.Point(0, 48);
            this.chkToolsInstalled.Name = "chkToolsInstalled";
            this.chkToolsInstalled.Size = new System.Drawing.Size(131, 24);
            this.chkToolsInstalled.TabIndex = 2;
            this.chkToolsInstalled.Text = "Installed";
            this.chkToolsInstalled.UseVisualStyleBackColor = true;
            this.chkToolsInstalled.Click += new System.EventHandler(this.chkTools_Click);
            // 
            // chkToolsNotInstalled
            // 
            this.chkToolsNotInstalled.AutoSize = true;
            this.chkToolsNotInstalled.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkToolsNotInstalled.Location = new System.Drawing.Point(0, 24);
            this.chkToolsNotInstalled.Name = "chkToolsNotInstalled";
            this.chkToolsNotInstalled.Size = new System.Drawing.Size(131, 24);
            this.chkToolsNotInstalled.TabIndex = 1;
            this.chkToolsNotInstalled.Text = "Not installed";
            this.chkToolsNotInstalled.UseVisualStyleBackColor = true;
            this.chkToolsNotInstalled.Click += new System.EventHandler(this.chkTools_Click);
            // 
            // chkToolsNotCompatible
            // 
            this.chkToolsNotCompatible.AutoSize = true;
            this.chkToolsNotCompatible.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkToolsNotCompatible.Location = new System.Drawing.Point(0, 0);
            this.chkToolsNotCompatible.Name = "chkToolsNotCompatible";
            this.chkToolsNotCompatible.Size = new System.Drawing.Size(131, 24);
            this.chkToolsNotCompatible.TabIndex = 0;
            this.chkToolsNotCompatible.Text = "Not compatible";
            this.chkToolsNotCompatible.UseVisualStyleBackColor = true;
            this.chkToolsNotCompatible.Click += new System.EventHandler(this.chkTools_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Display tools";
            // 
            // StoreFormFromPortal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1509, 897);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tsMain);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "StoreFormFromPortal";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "XrmToolBox Tool Library";
            this.Load += new System.EventHandler(this.PluginsChecker_Load);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.pnlReleaseNotes.ResumeLayout(false);
            this.scProperties.Panel1.ResumeLayout(false);
            this.scProperties.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scProperties)).EndInit();
            this.scProperties.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStar)).EndInit();
            this.pnlNotif.ResumeLayout(false);
            this.pnlNotif.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbNotifIcon)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton tsbLoadPlugins;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbInstall;
        private System.Windows.Forms.ToolStripProgressBar tssProgress;
        private System.Windows.Forms.ToolStripStatusLabel tssLabel;
        private System.Windows.Forms.ToolStripButton tsbShowThisScreenOnStartup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Panel pnlReleaseNotes;
        private System.Windows.Forms.ColumnHeader colCheckBox;
        private System.Windows.Forms.ColumnHeader colTitle;
        private System.Windows.Forms.ColumnHeader colVersion;
        private System.Windows.Forms.ColumnHeader colCurrent;
        private System.Windows.Forms.ColumnHeader colDescription;
        private System.Windows.Forms.ColumnHeader colAuthor;
        private System.Windows.Forms.ColumnHeader colAction;
        private System.Windows.Forms.ColumnHeader colDownloadCount;
        private System.Windows.Forms.ListView lvPlugins;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripDropDownButton tsddbOptions;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowPluginsNotCompatible;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowNewPlugins;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowPluginsUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowInstalledPlugins;
        private System.Windows.Forms.ToolStripButton tsbProxySettings;
        private System.Windows.Forms.ToolStripButton tsbCleanCacheFolder;
        private System.Windows.Forms.ToolStripLabel tslSearch;
        private System.Windows.Forms.ToolStripTextBox tstSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private ToolStripButton tsbUninstall;
        private ColumnHeader colAverageDownloadCoun;
        private ToolStripStatusLabel tssPluginsCount;
        private SplitContainer scProperties;
        private Label lblProperties;
        private Panel pnlNotif;
        private Label lblNotif;
        private PictureBox pbNotifIcon;
        private ImageList iiNotif;
        private ImageList ilImages;
        private ColumnHeader colRating;
        private Label lblReleaseNotes;
        private Panel panel1;
        private LinkLabel llRatePlugin;
        private PictureBox pbStar;
        private Label lblRating;
        private ImageList ilImages24;
        private Panel pnlReleaseNotesDetails;
        private Label lblNoRating;
        private ColumnHeader colLatestDate;
        private SplitContainer splitContainerMain;
        private Label label1;
        private Panel pnlCategories;
        private Label label2;
        private Panel panel2;
        private CheckBox chkToolsWithUpdate;
        private CheckBox chkToolsInstalled;
        private CheckBox chkToolsNotInstalled;
        private CheckBox chkToolsNotCompatible;
    }
}