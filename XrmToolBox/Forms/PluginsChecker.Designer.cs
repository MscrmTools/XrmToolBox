namespace XrmToolBox.Forms
{
    partial class PluginsChecker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginsChecker));
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbLoadPlugins = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbInstall = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbShowThisScreenOnStartup = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsddbOptions = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiShowPluginsNotCompatible = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowNewPlugins = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowPluginsUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowInstalledPlugins = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.tssLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlReleaseNotes = new System.Windows.Forms.Panel();
            this.scProperties = new System.Windows.Forms.SplitContainer();
            this.lblProperties = new System.Windows.Forms.Label();
            this.pnlReleaseNotesDetails = new System.Windows.Forms.Panel();
            this.lblReleaseNotes = new System.Windows.Forms.Label();
            this.lvPlugins = new System.Windows.Forms.ListView();
            this.colTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCurrent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAuthor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDownloadCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tsMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.pnlReleaseNotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scProperties)).BeginInit();
            this.scProperties.Panel1.SuspendLayout();
            this.scProperties.Panel2.SuspendLayout();
            this.scProperties.SuspendLayout();
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
            this.toolStripSeparator2,
            this.tsbShowThisScreenOnStartup,
            this.toolStripSeparator3,
            this.tsddbOptions});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.tsMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tsMain.Size = new System.Drawing.Size(1497, 32);
            this.tsMain.TabIndex = 0;
            this.tsMain.Text = "toolStrip1";
            // 
            // tsbLoadPlugins
            // 
            this.tsbLoadPlugins.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadPlugins.Image")));
            this.tsbLoadPlugins.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadPlugins.Name = "tsbLoadPlugins";
            this.tsbLoadPlugins.Size = new System.Drawing.Size(256, 29);
            this.tsbLoadPlugins.Text = "Reload plugins from NuGet";
            this.tsbLoadPlugins.Click += new System.EventHandler(this.tsbLoadPlugins_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbInstall
            // 
            this.tsbInstall.Image = ((System.Drawing.Image)(resources.GetObject("tsbInstall.Image")));
            this.tsbInstall.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInstall.Name = "tsbInstall";
            this.tsbInstall.Size = new System.Drawing.Size(244, 29);
            this.tsbInstall.Text = "Install selected package(s)";
            this.tsbInstall.Click += new System.EventHandler(this.tsbInstall_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbShowThisScreenOnStartup
            // 
            this.tsbShowThisScreenOnStartup.CheckOnClick = true;
            this.tsbShowThisScreenOnStartup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbShowThisScreenOnStartup.Image = ((System.Drawing.Image)(resources.GetObject("tsbShowThisScreenOnStartup.Image")));
            this.tsbShowThisScreenOnStartup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowThisScreenOnStartup.Name = "tsbShowThisScreenOnStartup";
            this.tsbShowThisScreenOnStartup.Size = new System.Drawing.Size(335, 29);
            this.tsbShowThisScreenOnStartup.Text = "Show this screen on XrmToolBox startup";
            this.tsbShowThisScreenOnStartup.Click += new System.EventHandler(this.tsbShowThisScreenOnStartup_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 32);
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
            this.tsmiShowPluginsNotCompatible.Size = new System.Drawing.Size(330, 30);
            this.tsmiShowPluginsNotCompatible.Text = "Show plugins not compatible";
            this.tsmiShowPluginsNotCompatible.Click += new System.EventHandler(this.tsmiPluginDisplayOption_Click);
            // 
            // tsmiShowNewPlugins
            // 
            this.tsmiShowNewPlugins.Checked = true;
            this.tsmiShowNewPlugins.CheckOnClick = true;
            this.tsmiShowNewPlugins.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiShowNewPlugins.Name = "tsmiShowNewPlugins";
            this.tsmiShowNewPlugins.Size = new System.Drawing.Size(330, 30);
            this.tsmiShowNewPlugins.Text = "Show new plugins";
            this.tsmiShowNewPlugins.Click += new System.EventHandler(this.tsmiPluginDisplayOption_Click);
            // 
            // tsmiShowPluginsUpdate
            // 
            this.tsmiShowPluginsUpdate.Checked = true;
            this.tsmiShowPluginsUpdate.CheckOnClick = true;
            this.tsmiShowPluginsUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiShowPluginsUpdate.Name = "tsmiShowPluginsUpdate";
            this.tsmiShowPluginsUpdate.Size = new System.Drawing.Size(330, 30);
            this.tsmiShowPluginsUpdate.Text = "Show plugins update";
            this.tsmiShowPluginsUpdate.Click += new System.EventHandler(this.tsmiPluginDisplayOption_Click);
            // 
            // tsmiShowInstalledPlugins
            // 
            this.tsmiShowInstalledPlugins.Checked = true;
            this.tsmiShowInstalledPlugins.CheckOnClick = true;
            this.tsmiShowInstalledPlugins.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiShowInstalledPlugins.Name = "tsmiShowInstalledPlugins";
            this.tsmiShowInstalledPlugins.Size = new System.Drawing.Size(330, 30);
            this.tsmiShowInstalledPlugins.Text = "Show installed plugins";
            this.tsmiShowInstalledPlugins.Click += new System.EventHandler(this.tsmiPluginDisplayOption_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssProgress,
            this.tssLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 865);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 14, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1497, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssProgress
            // 
            this.tssProgress.Name = "tssProgress";
            this.tssProgress.Size = new System.Drawing.Size(200, 17);
            this.tssProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.tssProgress.Visible = false;
            // 
            // tssLabel
            // 
            this.tssLabel.Name = "tssLabel";
            this.tssLabel.Size = new System.Drawing.Size(0, 17);
            this.tssLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlReleaseNotes
            // 
            this.pnlReleaseNotes.Controls.Add(this.scProperties);
            this.pnlReleaseNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReleaseNotes.Location = new System.Drawing.Point(0, 0);
            this.pnlReleaseNotes.Name = "pnlReleaseNotes";
            this.pnlReleaseNotes.Size = new System.Drawing.Size(1497, 386);
            this.pnlReleaseNotes.TabIndex = 4;
            // 
            // scProperties
            // 
            this.scProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scProperties.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scProperties.Location = new System.Drawing.Point(0, 0);
            this.scProperties.Name = "scProperties";
            // 
            // scProperties.Panel1
            // 
            this.scProperties.Panel1.AutoScroll = true;
            this.scProperties.Panel1.Controls.Add(this.lblProperties);
            // 
            // scProperties.Panel2
            // 
            this.scProperties.Panel2.Controls.Add(this.pnlReleaseNotesDetails);
            this.scProperties.Panel2.Controls.Add(this.lblReleaseNotes);
            this.scProperties.Size = new System.Drawing.Size(1497, 386);
            this.scProperties.SplitterDistance = 502;
            this.scProperties.TabIndex = 2;
            // 
            // lblProperties
            // 
            this.lblProperties.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProperties.Location = new System.Drawing.Point(0, 0);
            this.lblProperties.Name = "lblProperties";
            this.lblProperties.Size = new System.Drawing.Size(502, 25);
            this.lblProperties.TabIndex = 1;
            this.lblProperties.Text = "Plugin properties";
            // 
            // pnlReleaseNotesDetails
            // 
            this.pnlReleaseNotesDetails.AutoScroll = true;
            this.pnlReleaseNotesDetails.AutoScrollMinSize = new System.Drawing.Size(0, 1000);
            this.pnlReleaseNotesDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReleaseNotesDetails.Location = new System.Drawing.Point(0, 25);
            this.pnlReleaseNotesDetails.Name = "pnlReleaseNotesDetails";
            this.pnlReleaseNotesDetails.Size = new System.Drawing.Size(991, 361);
            this.pnlReleaseNotesDetails.TabIndex = 2;
            // 
            // lblReleaseNotes
            // 
            this.lblReleaseNotes.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblReleaseNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReleaseNotes.Location = new System.Drawing.Point(0, 0);
            this.lblReleaseNotes.Name = "lblReleaseNotes";
            this.lblReleaseNotes.Size = new System.Drawing.Size(991, 25);
            this.lblReleaseNotes.TabIndex = 0;
            this.lblReleaseNotes.Text = "Release notes";
            // 
            // lvPlugins
            // 
            this.lvPlugins.CheckBoxes = true;
            this.lvPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTitle,
            this.colVersion,
            this.colCurrent,
            this.colDescription,
            this.colAuthor,
            this.colAction,
            this.colDownloadCount});
            this.lvPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPlugins.FullRowSelect = true;
            this.lvPlugins.GridLines = true;
            this.lvPlugins.Location = new System.Drawing.Point(0, 0);
            this.lvPlugins.Name = "lvPlugins";
            this.lvPlugins.Size = new System.Drawing.Size(1497, 443);
            this.lvPlugins.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvPlugins.TabIndex = 5;
            this.lvPlugins.UseCompatibleStateImageBehavior = false;
            this.lvPlugins.View = System.Windows.Forms.View.Details;
            this.lvPlugins.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvPlugins_ColumnClick);
            this.lvPlugins.SelectedIndexChanged += new System.EventHandler(this.lvPlugins_SelectedIndexChanged);
            // 
            // colTitle
            // 
            this.colTitle.Text = "Title";
            this.colTitle.Width = 217;
            // 
            // colVersion
            // 
            this.colVersion.Text = "Version";
            this.colVersion.Width = 100;
            // 
            // colCurrent
            // 
            this.colCurrent.Text = "Current Version";
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
            this.colDownloadCount.Text = "Downloads";
            this.colDownloadCount.Width = 100;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 32);
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
            this.splitContainer1.Size = new System.Drawing.Size(1497, 833);
            this.splitContainer1.SplitterDistance = 443;
            this.splitContainer1.TabIndex = 6;
            // 
            // PluginsChecker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1497, 887);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tsMain);
            this.Name = "PluginsChecker";
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
        private System.Windows.Forms.Label lblReleaseNotes;
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
        private System.Windows.Forms.SplitContainer scProperties;
        private System.Windows.Forms.Label lblProperties;
        private System.Windows.Forms.Panel pnlReleaseNotesDetails;
    }
}