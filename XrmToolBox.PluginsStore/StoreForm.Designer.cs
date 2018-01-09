using System;
using System.Windows.Forms;

namespace XrmToolBox.PluginsStore
{
    partial class StoreForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StoreForm));
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
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiUseLegacyPluginsStore = new System.Windows.Forms.ToolStripMenuItem();
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
            this.pnlNotif = new System.Windows.Forms.Panel();
            this.lblNotif = new System.Windows.Forms.Label();
            this.pbNotifIcon = new System.Windows.Forms.PictureBox();
            this.lvPlugins = new System.Windows.Forms.ListView();
            this.colCheckBox = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCurrent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAuthor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDownloadCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLastDownloadCoun = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.iiNotif = new System.Windows.Forms.ImageList(this.components);
            this.tsMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.pnlReleaseNotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scProperties)).BeginInit();
            this.scProperties.Panel1.SuspendLayout();
            this.scProperties.Panel2.SuspendLayout();
            this.scProperties.SuspendLayout();
            this.pnlNotif.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbNotifIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.tsMain.Size = new System.Drawing.Size(998, 25);
            this.tsMain.TabIndex = 0;
            this.tsMain.Text = "toolStrip1";
            // 
            // tsbLoadPlugins
            // 
            this.tsbLoadPlugins.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadPlugins.Image")));
            this.tsbLoadPlugins.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadPlugins.Name = "tsbLoadPlugins";
            this.tsbLoadPlugins.Size = new System.Drawing.Size(66, 22);
            this.tsbLoadPlugins.Text = "Refresh";
            this.tsbLoadPlugins.Click += new System.EventHandler(this.tsbLoadPlugins_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbInstall
            // 
            this.tsbInstall.Image = ((System.Drawing.Image)(resources.GetObject("tsbInstall.Image")));
            this.tsbInstall.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInstall.Name = "tsbInstall";
            this.tsbInstall.Size = new System.Drawing.Size(58, 22);
            this.tsbInstall.Text = "Install";
            this.tsbInstall.Click += new System.EventHandler(this.tsbInstall_Click);
            // 
            // tsbUninstall
            // 
            this.tsbUninstall.Image = ((System.Drawing.Image)(resources.GetObject("tsbUninstall.Image")));
            this.tsbUninstall.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUninstall.Name = "tsbUninstall";
            this.tsbUninstall.Size = new System.Drawing.Size(73, 22);
            this.tsbUninstall.Text = "Uninstall";
            this.tsbUninstall.Visible = false;
            this.tsbUninstall.Click += new System.EventHandler(this.tsbUninstall_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tslSearch
            // 
            this.tslSearch.Name = "tslSearch";
            this.tslSearch.Size = new System.Drawing.Size(42, 22);
            this.tslSearch.Text = "Search";
            // 
            // tstSearch
            // 
            this.tstSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstSearch.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.tstSearch.Name = "tstSearch";
            this.tstSearch.Size = new System.Drawing.Size(167, 25);
            this.tstSearch.Text = "Search by Title or Authors";
            this.tstSearch.Enter += new System.EventHandler(this.tstSearch_Enter);
            this.tstSearch.TextChanged += new System.EventHandler(this.tstSearch_TextChanged);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbShowThisScreenOnStartup
            // 
            this.tsbShowThisScreenOnStartup.CheckOnClick = true;
            this.tsbShowThisScreenOnStartup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbShowThisScreenOnStartup.Image = ((System.Drawing.Image)(resources.GetObject("tsbShowThisScreenOnStartup.Image")));
            this.tsbShowThisScreenOnStartup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowThisScreenOnStartup.Name = "tsbShowThisScreenOnStartup";
            this.tsbShowThisScreenOnStartup.Size = new System.Drawing.Size(164, 22);
            this.tsbShowThisScreenOnStartup.Text = "Show on XrmToolBox startup";
            this.tsbShowThisScreenOnStartup.Click += new System.EventHandler(this.tsbShowThisScreenOnStartup_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsddbOptions
            // 
            this.tsddbOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsddbOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiShowPluginsNotCompatible,
            this.tsmiShowNewPlugins,
            this.tsmiShowPluginsUpdate,
            this.tsmiShowInstalledPlugins,
            this.toolStripSeparator5,
            this.tsmiUseLegacyPluginsStore});
            this.tsddbOptions.Image = ((System.Drawing.Image)(resources.GetObject("tsddbOptions.Image")));
            this.tsddbOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbOptions.Name = "tsddbOptions";
            this.tsddbOptions.Size = new System.Drawing.Size(101, 22);
            this.tsddbOptions.Text = "Display options";
            // 
            // tsmiShowPluginsNotCompatible
            // 
            this.tsmiShowPluginsNotCompatible.Checked = true;
            this.tsmiShowPluginsNotCompatible.CheckOnClick = true;
            this.tsmiShowPluginsNotCompatible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiShowPluginsNotCompatible.Name = "tsmiShowPluginsNotCompatible";
            this.tsmiShowPluginsNotCompatible.Size = new System.Drawing.Size(229, 22);
            this.tsmiShowPluginsNotCompatible.Text = "Show plugins not compatible";
            this.tsmiShowPluginsNotCompatible.Click += new System.EventHandler(this.tsmiPluginDisplayOption_Click);
            // 
            // tsmiShowNewPlugins
            // 
            this.tsmiShowNewPlugins.Checked = true;
            this.tsmiShowNewPlugins.CheckOnClick = true;
            this.tsmiShowNewPlugins.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiShowNewPlugins.Name = "tsmiShowNewPlugins";
            this.tsmiShowNewPlugins.Size = new System.Drawing.Size(229, 22);
            this.tsmiShowNewPlugins.Text = "Show new plugins";
            this.tsmiShowNewPlugins.Click += new System.EventHandler(this.tsmiPluginDisplayOption_Click);
            // 
            // tsmiShowPluginsUpdate
            // 
            this.tsmiShowPluginsUpdate.Checked = true;
            this.tsmiShowPluginsUpdate.CheckOnClick = true;
            this.tsmiShowPluginsUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiShowPluginsUpdate.Name = "tsmiShowPluginsUpdate";
            this.tsmiShowPluginsUpdate.Size = new System.Drawing.Size(229, 22);
            this.tsmiShowPluginsUpdate.Text = "Show plugins update";
            this.tsmiShowPluginsUpdate.Click += new System.EventHandler(this.tsmiPluginDisplayOption_Click);
            // 
            // tsmiShowInstalledPlugins
            // 
            this.tsmiShowInstalledPlugins.Checked = true;
            this.tsmiShowInstalledPlugins.CheckOnClick = true;
            this.tsmiShowInstalledPlugins.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiShowInstalledPlugins.Name = "tsmiShowInstalledPlugins";
            this.tsmiShowInstalledPlugins.Size = new System.Drawing.Size(229, 22);
            this.tsmiShowInstalledPlugins.Text = "Show installed plugins";
            this.tsmiShowInstalledPlugins.Click += new System.EventHandler(this.tsmiPluginDisplayOption_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(226, 6);
            // 
            // tsmiUseLegacyPluginsStore
            // 
            this.tsmiUseLegacyPluginsStore.CheckOnClick = true;
            this.tsmiUseLegacyPluginsStore.Name = "tsmiUseLegacyPluginsStore";
            this.tsmiUseLegacyPluginsStore.Size = new System.Drawing.Size(229, 22);
            this.tsmiUseLegacyPluginsStore.Text = "Use Legacy Plugins Store";
            this.tsmiUseLegacyPluginsStore.Click += new System.EventHandler(this.tsmiUseLegacyPluginsStore_Click);
            // 
            // tsbProxySettings
            // 
            this.tsbProxySettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbProxySettings.Image = ((System.Drawing.Image)(resources.GetObject("tsbProxySettings.Image")));
            this.tsbProxySettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbProxySettings.Name = "tsbProxySettings";
            this.tsbProxySettings.Size = new System.Drawing.Size(23, 22);
            this.tsbProxySettings.Text = "Proxy settings";
            this.tsbProxySettings.Click += new System.EventHandler(this.tsbProxySettings_Click);
            // 
            // tsbCleanCacheFolder
            // 
            this.tsbCleanCacheFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCleanCacheFolder.Image = ((System.Drawing.Image)(resources.GetObject("tsbCleanCacheFolder.Image")));
            this.tsbCleanCacheFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCleanCacheFolder.Name = "tsbCleanCacheFolder";
            this.tsbCleanCacheFolder.Size = new System.Drawing.Size(23, 22);
            this.tsbCleanCacheFolder.Text = "Clean cache folder";
            this.tsbCleanCacheFolder.Click += new System.EventHandler(this.tsbCleanCacheFolder_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssProgress,
            this.tssLabel,
            this.tssPluginsCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 555);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 9, 0);
            this.statusStrip1.Size = new System.Drawing.Size(998, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssProgress
            // 
            this.tssProgress.Name = "tssProgress";
            this.tssProgress.Size = new System.Drawing.Size(133, 16);
            this.tssProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.tssProgress.Visible = false;
            // 
            // tssLabel
            // 
            this.tssLabel.Name = "tssLabel";
            this.tssLabel.Size = new System.Drawing.Size(0, 17);
            this.tssLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tssPluginsCount
            // 
            this.tssPluginsCount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tssPluginsCount.Name = "tssPluginsCount";
            this.tssPluginsCount.Size = new System.Drawing.Size(0, 17);
            this.tssPluginsCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlReleaseNotes
            // 
            this.pnlReleaseNotes.Controls.Add(this.scProperties);
            this.pnlReleaseNotes.Controls.Add(this.pnlNotif);
            this.pnlReleaseNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReleaseNotes.Location = new System.Drawing.Point(0, 0);
            this.pnlReleaseNotes.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.pnlReleaseNotes.Name = "pnlReleaseNotes";
            this.pnlReleaseNotes.Size = new System.Drawing.Size(998, 253);
            this.pnlReleaseNotes.TabIndex = 4;
            // 
            // scProperties
            // 
            this.scProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scProperties.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scProperties.Location = new System.Drawing.Point(0, 21);
            this.scProperties.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
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
            this.scProperties.Size = new System.Drawing.Size(998, 232);
            this.scProperties.SplitterDistance = 500;
            this.scProperties.SplitterWidth = 3;
            this.scProperties.TabIndex = 7;
            // 
            // lblProperties
            // 
            this.lblProperties.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProperties.Location = new System.Drawing.Point(0, 0);
            this.lblProperties.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblProperties.Name = "lblProperties";
            this.lblProperties.Size = new System.Drawing.Size(500, 16);
            this.lblProperties.TabIndex = 1;
            this.lblProperties.Text = "Plugin properties";
            // 
            // pnlReleaseNotesDetails
            // 
            this.pnlReleaseNotesDetails.AutoScroll = true;
            this.pnlReleaseNotesDetails.AutoScrollMinSize = new System.Drawing.Size(0, 1000);
            this.pnlReleaseNotesDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReleaseNotesDetails.Location = new System.Drawing.Point(0, 16);
            this.pnlReleaseNotesDetails.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.pnlReleaseNotesDetails.Name = "pnlReleaseNotesDetails";
            this.pnlReleaseNotesDetails.Size = new System.Drawing.Size(495, 216);
            this.pnlReleaseNotesDetails.TabIndex = 2;
            // 
            // lblReleaseNotes
            // 
            this.lblReleaseNotes.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblReleaseNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReleaseNotes.Location = new System.Drawing.Point(0, 0);
            this.lblReleaseNotes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblReleaseNotes.Name = "lblReleaseNotes";
            this.lblReleaseNotes.Size = new System.Drawing.Size(495, 16);
            this.lblReleaseNotes.TabIndex = 0;
            this.lblReleaseNotes.Text = "Release notes";
            // 
            // pnlNotif
            // 
            this.pnlNotif.BackColor = System.Drawing.SystemColors.Info;
            this.pnlNotif.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNotif.Controls.Add(this.lblNotif);
            this.pnlNotif.Controls.Add(this.pbNotifIcon);
            this.pnlNotif.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNotif.Location = new System.Drawing.Point(0, 0);
            this.pnlNotif.Margin = new System.Windows.Forms.Padding(2);
            this.pnlNotif.Name = "pnlNotif";
            this.pnlNotif.Size = new System.Drawing.Size(998, 21);
            this.pnlNotif.TabIndex = 3;
            this.pnlNotif.Visible = false;
            // 
            // lblNotif
            // 
            this.lblNotif.AutoSize = true;
            this.lblNotif.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNotif.Location = new System.Drawing.Point(21, 0);
            this.lblNotif.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNotif.Name = "lblNotif";
            this.lblNotif.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblNotif.Size = new System.Drawing.Size(66, 17);
            this.lblNotif.TabIndex = 1;
            this.lblNotif.Text = "[Notification]";
            this.lblNotif.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbNotifIcon
            // 
            this.pbNotifIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbNotifIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbNotifIcon.Location = new System.Drawing.Point(0, 0);
            this.pbNotifIcon.Margin = new System.Windows.Forms.Padding(2);
            this.pbNotifIcon.Name = "pbNotifIcon";
            this.pbNotifIcon.Size = new System.Drawing.Size(21, 19);
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
            this.colVersion,
            this.colCurrent,
            this.colDescription,
            this.colAuthor,
            this.colAction,
            this.colDownloadCount,
            this.colLastDownloadCoun});
            this.lvPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPlugins.FullRowSelect = true;
            this.lvPlugins.GridLines = true;
            this.lvPlugins.Location = new System.Drawing.Point(0, 0);
            this.lvPlugins.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lvPlugins.Name = "lvPlugins";
            this.lvPlugins.OwnerDraw = true;
            this.lvPlugins.Size = new System.Drawing.Size(998, 274);
            this.lvPlugins.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvPlugins.TabIndex = 5;
            this.lvPlugins.UseCompatibleStateImageBehavior = false;
            this.lvPlugins.View = System.Windows.Forms.View.Details;
            this.lvPlugins.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvPlugins_ColumnClick);
            this.lvPlugins.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lvPlugins_DrawColumnHeader);
            this.lvPlugins.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lvPlugins_DrawItem);
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
            // colLastDownloadCoun
            // 
            this.colLastDownloadCoun.Text = "Latest version Downloads";
            this.colLastDownloadCoun.Width = 100;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
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
            this.splitContainer1.Size = new System.Drawing.Size(998, 530);
            this.splitContainer1.SplitterDistance = 274;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 6;
            // 
            // iiNotif
            // 
            this.iiNotif.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iiNotif.ImageStream")));
            this.iiNotif.TransparentColor = System.Drawing.Color.Transparent;
            this.iiNotif.Images.SetKeyName(0, "notif_icn_info16.png");
            this.iiNotif.Images.SetKeyName(1, "notif_icn_alert16.png");
            this.iiNotif.Images.SetKeyName(2, "notif_icn_crit16.png");
            // 
            // StoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 577);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tsMain);
            this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Name = "StoreForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "XrmToolBox Plugins Store";
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
            this.pnlNotif.ResumeLayout(false);
            this.pnlNotif.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbNotifIcon)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
        private ColumnHeader colLastDownloadCoun;
        private ToolStripStatusLabel tssPluginsCount;
        private SplitContainer scProperties;
        private Label lblProperties;
        private Panel pnlReleaseNotesDetails;
        private Label lblReleaseNotes;
        private Panel pnlNotif;
        private Label lblNotif;
        private PictureBox pbNotifIcon;
        private ImageList iiNotif;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem tsmiUseLegacyPluginsStore;
    }
}