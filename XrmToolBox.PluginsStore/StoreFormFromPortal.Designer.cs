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
            this.tstSearch = new System.Windows.Forms.ToolStripTextBox();
            this.tslSearch = new System.Windows.Forms.ToolStripLabel();
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblToolLabelVersion = new System.Windows.Forms.Label();
            this.lblToolLabelAuthors = new System.Windows.Forms.Label();
            this.lblToolLabelFirstRelease = new System.Windows.Forms.Label();
            this.lblToolLabelLatestRelease = new System.Windows.Forms.Label();
            this.lblToolLabelDownloads = new System.Windows.Forms.Label();
            this.lblToolLabelProjectUrl = new System.Windows.Forms.Label();
            this.lblToolVersion = new System.Windows.Forms.Label();
            this.lblToolAuthors = new System.Windows.Forms.Label();
            this.lblToolFirstRelease = new System.Windows.Forms.Label();
            this.lblToolLatestRelease = new System.Windows.Forms.Label();
            this.lblToolDownloads = new System.Windows.Forms.Label();
            this.llToolProjectUrl = new System.Windows.Forms.LinkLabel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlToolHeader = new System.Windows.Forms.Panel();
            this.lblToolTitle = new System.Windows.Forms.Label();
            this.lblToolDescription = new System.Windows.Forms.Label();
            this.pbToolImage = new System.Windows.Forms.PictureBox();
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
            this.chkIsOpenSource = new System.Windows.Forms.CheckBox();
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
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlToolHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbToolImage)).BeginInit();
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
            this.tsMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbLoadPlugins,
            this.toolStripSeparator1,
            this.tsbInstall,
            this.tsbUninstall,
            this.toolStripSeparator2,
            this.tstSearch,
            this.tslSearch,
            this.toolStripSeparator4,
            this.tsbShowThisScreenOnStartup,
            this.toolStripSeparator3,
            this.tsddbOptions,
            this.tsbProxySettings,
            this.tsbCleanCacheFolder});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tsMain.Size = new System.Drawing.Size(1669, 34);
            this.tsMain.TabIndex = 0;
            this.tsMain.Text = "toolStrip1";
            // 
            // tsbLoadPlugins
            // 
            this.tsbLoadPlugins.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadPlugins.Image")));
            this.tsbLoadPlugins.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadPlugins.Name = "tsbLoadPlugins";
            this.tsbLoadPlugins.Size = new System.Drawing.Size(98, 29);
            this.tsbLoadPlugins.Text = "Refresh";
            this.tsbLoadPlugins.Click += new System.EventHandler(this.tsbLoadPlugins_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 34);
            // 
            // tsbInstall
            // 
            this.tsbInstall.Image = ((System.Drawing.Image)(resources.GetObject("tsbInstall.Image")));
            this.tsbInstall.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInstall.Name = "tsbInstall";
            this.tsbInstall.Size = new System.Drawing.Size(86, 29);
            this.tsbInstall.Text = "Install";
            this.tsbInstall.Click += new System.EventHandler(this.tsbInstall_Click);
            // 
            // tsbUninstall
            // 
            this.tsbUninstall.Image = ((System.Drawing.Image)(resources.GetObject("tsbUninstall.Image")));
            this.tsbUninstall.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUninstall.Name = "tsbUninstall";
            this.tsbUninstall.Size = new System.Drawing.Size(107, 29);
            this.tsbUninstall.Text = "Uninstall";
            this.tsbUninstall.Visible = false;
            this.tsbUninstall.Click += new System.EventHandler(this.tsbUninstall_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 34);
            // 
            // tstSearch
            // 
            this.tstSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstSearch.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.tstSearch.Name = "tstSearch";
            this.tstSearch.Size = new System.Drawing.Size(250, 34);
            this.tstSearch.Tag = "Search by Title or Authors";
            this.tstSearch.Text = "Search by Title or Authors";
            this.tstSearch.Enter += new System.EventHandler(this.tstSearch_Enter);
            this.tstSearch.Leave += new System.EventHandler(this.tstSearch_Leave);
            this.tstSearch.TextChanged += new System.EventHandler(this.tstSearch_TextChanged);
            // 
            // tslSearch
            // 
            this.tslSearch.Name = "tslSearch";
            this.tslSearch.Size = new System.Drawing.Size(64, 29);
            this.tslSearch.Text = "Search";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 34);
            // 
            // tsbShowThisScreenOnStartup
            // 
            this.tsbShowThisScreenOnStartup.CheckOnClick = true;
            this.tsbShowThisScreenOnStartup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbShowThisScreenOnStartup.Image = ((System.Drawing.Image)(resources.GetObject("tsbShowThisScreenOnStartup.Image")));
            this.tsbShowThisScreenOnStartup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowThisScreenOnStartup.Name = "tsbShowThisScreenOnStartup";
            this.tsbShowThisScreenOnStartup.Size = new System.Drawing.Size(247, 29);
            this.tsbShowThisScreenOnStartup.Text = "Show on XrmToolBox startup";
            this.tsbShowThisScreenOnStartup.Click += new System.EventHandler(this.tsbShowThisScreenOnStartup_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 34);
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
            this.tsddbOptions.Size = new System.Drawing.Size(154, 29);
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
            this.tsbProxySettings.Size = new System.Drawing.Size(34, 29);
            this.tsbProxySettings.Text = "Proxy settings";
            this.tsbProxySettings.Click += new System.EventHandler(this.tsbProxySettings_Click);
            // 
            // tsbCleanCacheFolder
            // 
            this.tsbCleanCacheFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCleanCacheFolder.Image = ((System.Drawing.Image)(resources.GetObject("tsbCleanCacheFolder.Image")));
            this.tsbCleanCacheFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCleanCacheFolder.Name = "tsbCleanCacheFolder";
            this.tsbCleanCacheFolder.Size = new System.Drawing.Size(34, 29);
            this.tsbCleanCacheFolder.Text = "Clean cache folder";
            this.tsbCleanCacheFolder.Click += new System.EventHandler(this.tsbCleanCacheFolder_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssProgress,
            this.tssLabel,
            this.tssPluginsCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 810);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 14, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1669, 22);
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
            this.pnlReleaseNotes.Size = new System.Drawing.Size(1514, 375);
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
            this.scProperties.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.scProperties.Panel1.Controls.Add(this.panel3);
            this.scProperties.Panel1MinSize = 500;
            // 
            // scProperties.Panel2
            // 
            this.scProperties.Panel2.Controls.Add(this.pnlReleaseNotesDetails);
            this.scProperties.Panel2.Controls.Add(this.lblReleaseNotes);
            this.scProperties.Panel2.Controls.Add(this.panel1);
            this.scProperties.Panel2.Controls.Add(this.lblRating);
            this.scProperties.Size = new System.Drawing.Size(1514, 344);
            this.scProperties.SplitterDistance = 700;
            this.scProperties.SplitterWidth = 5;
            this.scProperties.TabIndex = 7;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lblToolLabelVersion, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblToolLabelAuthors, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblToolLabelFirstRelease, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblToolLabelLatestRelease, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblToolLabelDownloads, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblToolLabelProjectUrl, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblToolVersion, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblToolAuthors, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblToolFirstRelease, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblToolLatestRelease, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblToolDownloads, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.llToolProjectUrl, 1, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 80);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 264);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // lblToolLabelVersion
            // 
            this.lblToolLabelVersion.AutoSize = true;
            this.lblToolLabelVersion.Location = new System.Drawing.Point(3, 0);
            this.lblToolLabelVersion.Name = "lblToolLabelVersion";
            this.lblToolLabelVersion.Size = new System.Drawing.Size(63, 20);
            this.lblToolLabelVersion.TabIndex = 0;
            this.lblToolLabelVersion.Text = "Version";
            // 
            // lblToolLabelAuthors
            // 
            this.lblToolLabelAuthors.AutoSize = true;
            this.lblToolLabelAuthors.Location = new System.Drawing.Point(3, 30);
            this.lblToolLabelAuthors.Name = "lblToolLabelAuthors";
            this.lblToolLabelAuthors.Size = new System.Drawing.Size(65, 20);
            this.lblToolLabelAuthors.TabIndex = 1;
            this.lblToolLabelAuthors.Text = "Authors";
            // 
            // lblToolLabelFirstRelease
            // 
            this.lblToolLabelFirstRelease.AutoSize = true;
            this.lblToolLabelFirstRelease.Location = new System.Drawing.Point(3, 60);
            this.lblToolLabelFirstRelease.Name = "lblToolLabelFirstRelease";
            this.lblToolLabelFirstRelease.Size = new System.Drawing.Size(96, 20);
            this.lblToolLabelFirstRelease.TabIndex = 2;
            this.lblToolLabelFirstRelease.Text = "First release";
            // 
            // lblToolLabelLatestRelease
            // 
            this.lblToolLabelLatestRelease.AutoSize = true;
            this.lblToolLabelLatestRelease.Location = new System.Drawing.Point(3, 90);
            this.lblToolLabelLatestRelease.Name = "lblToolLabelLatestRelease";
            this.lblToolLabelLatestRelease.Size = new System.Drawing.Size(110, 20);
            this.lblToolLabelLatestRelease.TabIndex = 3;
            this.lblToolLabelLatestRelease.Text = "Latest release";
            // 
            // lblToolLabelDownloads
            // 
            this.lblToolLabelDownloads.AutoSize = true;
            this.lblToolLabelDownloads.Location = new System.Drawing.Point(3, 120);
            this.lblToolLabelDownloads.Name = "lblToolLabelDownloads";
            this.lblToolLabelDownloads.Size = new System.Drawing.Size(132, 20);
            this.lblToolLabelDownloads.TabIndex = 4;
            this.lblToolLabelDownloads.Text = "Downloads count";
            // 
            // lblToolLabelProjectUrl
            // 
            this.lblToolLabelProjectUrl.AutoSize = true;
            this.lblToolLabelProjectUrl.Location = new System.Drawing.Point(3, 150);
            this.lblToolLabelProjectUrl.Name = "lblToolLabelProjectUrl";
            this.lblToolLabelProjectUrl.Size = new System.Drawing.Size(82, 20);
            this.lblToolLabelProjectUrl.TabIndex = 5;
            this.lblToolLabelProjectUrl.Text = "Project Url";
            // 
            // lblToolVersion
            // 
            this.lblToolVersion.AutoSize = true;
            this.lblToolVersion.Location = new System.Drawing.Point(403, 0);
            this.lblToolVersion.Name = "lblToolVersion";
            this.lblToolVersion.Size = new System.Drawing.Size(108, 20);
            this.lblToolVersion.TabIndex = 6;
            this.lblToolVersion.Text = "lblToolVersion";
            // 
            // lblToolAuthors
            // 
            this.lblToolAuthors.AutoSize = true;
            this.lblToolAuthors.Location = new System.Drawing.Point(403, 30);
            this.lblToolAuthors.Name = "lblToolAuthors";
            this.lblToolAuthors.Size = new System.Drawing.Size(110, 20);
            this.lblToolAuthors.TabIndex = 7;
            this.lblToolAuthors.Text = "lblToolAuthors";
            // 
            // lblToolFirstRelease
            // 
            this.lblToolFirstRelease.AutoSize = true;
            this.lblToolFirstRelease.Location = new System.Drawing.Point(403, 60);
            this.lblToolFirstRelease.Name = "lblToolFirstRelease";
            this.lblToolFirstRelease.Size = new System.Drawing.Size(144, 20);
            this.lblToolFirstRelease.TabIndex = 8;
            this.lblToolFirstRelease.Text = "lblToolFirstRelease";
            // 
            // lblToolLatestRelease
            // 
            this.lblToolLatestRelease.AutoSize = true;
            this.lblToolLatestRelease.Location = new System.Drawing.Point(403, 90);
            this.lblToolLatestRelease.Name = "lblToolLatestRelease";
            this.lblToolLatestRelease.Size = new System.Drawing.Size(158, 20);
            this.lblToolLatestRelease.TabIndex = 9;
            this.lblToolLatestRelease.Text = "lblToolLatestRelease";
            // 
            // lblToolDownloads
            // 
            this.lblToolDownloads.AutoSize = true;
            this.lblToolDownloads.Location = new System.Drawing.Point(403, 120);
            this.lblToolDownloads.Name = "lblToolDownloads";
            this.lblToolDownloads.Size = new System.Drawing.Size(133, 20);
            this.lblToolDownloads.TabIndex = 10;
            this.lblToolDownloads.Text = "lblToolDownloads";
            // 
            // llToolProjectUrl
            // 
            this.llToolProjectUrl.AutoSize = true;
            this.llToolProjectUrl.Location = new System.Drawing.Point(403, 150);
            this.llToolProjectUrl.Name = "llToolProjectUrl";
            this.llToolProjectUrl.Size = new System.Drawing.Size(80, 20);
            this.llToolProjectUrl.TabIndex = 11;
            this.llToolProjectUrl.TabStop = true;
            this.llToolProjectUrl.Text = "linkLabel1";
            this.llToolProjectUrl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pnlToolHeader);
            this.panel3.Controls.Add(this.pbToolImage);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(800, 80);
            this.panel3.TabIndex = 0;
            // 
            // pnlToolHeader
            // 
            this.pnlToolHeader.Controls.Add(this.lblToolTitle);
            this.pnlToolHeader.Controls.Add(this.lblToolDescription);
            this.pnlToolHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlToolHeader.Location = new System.Drawing.Point(80, 0);
            this.pnlToolHeader.Name = "pnlToolHeader";
            this.pnlToolHeader.Size = new System.Drawing.Size(720, 80);
            this.pnlToolHeader.TabIndex = 1;
            // 
            // lblToolTitle
            // 
            this.lblToolTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblToolTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToolTitle.Location = new System.Drawing.Point(0, 0);
            this.lblToolTitle.Name = "lblToolTitle";
            this.lblToolTitle.Size = new System.Drawing.Size(720, 60);
            this.lblToolTitle.TabIndex = 1;
            this.lblToolTitle.Text = "lblToolTitle";
            // 
            // lblToolDescription
            // 
            this.lblToolDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblToolDescription.Location = new System.Drawing.Point(0, 60);
            this.lblToolDescription.Name = "lblToolDescription";
            this.lblToolDescription.Size = new System.Drawing.Size(720, 20);
            this.lblToolDescription.TabIndex = 0;
            this.lblToolDescription.Text = "lblToolDescription";
            this.lblToolDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbToolImage
            // 
            this.pbToolImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbToolImage.Location = new System.Drawing.Point(0, 0);
            this.pbToolImage.Name = "pbToolImage";
            this.pbToolImage.Size = new System.Drawing.Size(80, 80);
            this.pbToolImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbToolImage.TabIndex = 0;
            this.pbToolImage.TabStop = false;
            // 
            // pnlReleaseNotesDetails
            // 
            this.pnlReleaseNotesDetails.AutoScroll = true;
            this.pnlReleaseNotesDetails.AutoScrollMinSize = new System.Drawing.Size(0, 1000);
            this.pnlReleaseNotesDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReleaseNotesDetails.Location = new System.Drawing.Point(0, 102);
            this.pnlReleaseNotesDetails.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlReleaseNotesDetails.Name = "pnlReleaseNotesDetails";
            this.pnlReleaseNotesDetails.Size = new System.Drawing.Size(709, 242);
            this.pnlReleaseNotesDetails.TabIndex = 16;
            // 
            // lblReleaseNotes
            // 
            this.lblReleaseNotes.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblReleaseNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReleaseNotes.Location = new System.Drawing.Point(0, 72);
            this.lblReleaseNotes.Name = "lblReleaseNotes";
            this.lblReleaseNotes.Size = new System.Drawing.Size(709, 30);
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
            this.panel1.Location = new System.Drawing.Point(0, 29);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(709, 43);
            this.panel1.TabIndex = 13;
            // 
            // lblNoRating
            // 
            this.lblNoRating.AutoSize = true;
            this.lblNoRating.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblNoRating.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoRating.Location = new System.Drawing.Point(142, 0);
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
            this.llRatePlugin.Location = new System.Drawing.Point(606, 0);
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
            this.pbStar.Size = new System.Drawing.Size(142, 43);
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
            this.lblRating.Size = new System.Drawing.Size(709, 29);
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
            this.pnlNotif.Size = new System.Drawing.Size(1514, 31);
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
            this.lvPlugins.Size = new System.Drawing.Size(1514, 396);
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
            this.splitContainer1.Size = new System.Drawing.Size(1514, 776);
            this.splitContainer1.SplitterDistance = 396;
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
            this.splitContainerMain.Location = new System.Drawing.Point(0, 34);
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
            this.splitContainerMain.Size = new System.Drawing.Size(1669, 776);
            this.splitContainerMain.SplitterDistance = 200;
            this.splitContainerMain.TabIndex = 7;
            // 
            // pnlCategories
            // 
            this.pnlCategories.AutoScroll = true;
            this.pnlCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCategories.Location = new System.Drawing.Point(10, 313);
            this.pnlCategories.Name = "pnlCategories";
            this.pnlCategories.Size = new System.Drawing.Size(131, 453);
            this.pnlCategories.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 269);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 44);
            this.label2.TabIndex = 4;
            this.label2.Text = "Categories";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkIsOpenSource);
            this.panel2.Controls.Add(this.chkToolsWithUpdate);
            this.panel2.Controls.Add(this.chkToolsInstalled);
            this.panel2.Controls.Add(this.chkToolsNotInstalled);
            this.panel2.Controls.Add(this.chkToolsNotCompatible);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(10, 54);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(131, 215);
            this.panel2.TabIndex = 2;
            // 
            // chkIsOpenSource
            // 
            this.chkIsOpenSource.AutoSize = true;
            this.chkIsOpenSource.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkIsOpenSource.Location = new System.Drawing.Point(0, 96);
            this.chkIsOpenSource.Name = "chkIsOpenSource";
            this.chkIsOpenSource.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.chkIsOpenSource.Size = new System.Drawing.Size(131, 34);
            this.chkIsOpenSource.TabIndex = 4;
            this.chkIsOpenSource.Text = "Open Source only";
            this.chkIsOpenSource.UseVisualStyleBackColor = true;
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
            this.label1.Size = new System.Drawing.Size(131, 44);
            this.label1.TabIndex = 0;
            this.label1.Text = "Display tools";
            // 
            // StoreFormFromPortal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1669, 832);
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
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.pnlToolHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbToolImage)).EndInit();
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
        private CheckBox chkIsOpenSource;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel3;
        private Panel pnlToolHeader;
        private Label lblToolTitle;
        private Label lblToolDescription;
        private PictureBox pbToolImage;
        private Label lblToolLabelVersion;
        private Label lblToolLabelAuthors;
        private Label lblToolLabelFirstRelease;
        private Label lblToolLabelLatestRelease;
        private Label lblToolLabelDownloads;
        private Label lblToolLabelProjectUrl;
        private Label lblToolVersion;
        private Label lblToolAuthors;
        private Label lblToolFirstRelease;
        private Label lblToolLatestRelease;
        private Label lblToolDownloads;
        private LinkLabel llToolProjectUrl;
    }
}