namespace MsCrmTools.ChartManager
{
    partial class MainControl
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

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainControl));
            this.gbEntities = new System.Windows.Forms.GroupBox();
            this.lblSearchEntity = new System.Windows.Forms.Label();
            this.txtSearchEntity = new System.Windows.Forms.TextBox();
            this.lvEntities = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbCloseThisTab = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLoadEntities = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEditChart = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExportCharts = new System.Windows.Forms.ToolStripButton();
            this.tsbImportCharts = new System.Windows.Forms.ToolStripDropDownButton();
            this.importChartsFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importChartsFromFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gbCharts = new System.Windows.Forms.GroupBox();
            this.lvCharts = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbEntities.SuspendLayout();
            this.tsMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbCharts.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbEntities
            // 
            this.gbEntities.Controls.Add(this.lblSearchEntity);
            this.gbEntities.Controls.Add(this.txtSearchEntity);
            this.gbEntities.Controls.Add(this.lvEntities);
            this.gbEntities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbEntities.Enabled = false;
            this.gbEntities.Location = new System.Drawing.Point(0, 0);
            this.gbEntities.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbEntities.Name = "gbEntities";
            this.gbEntities.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbEntities.Size = new System.Drawing.Size(404, 756);
            this.gbEntities.TabIndex = 89;
            this.gbEntities.TabStop = false;
            this.gbEntities.Text = "Entities";
            // 
            // lblSearchEntity
            // 
            this.lblSearchEntity.AutoSize = true;
            this.lblSearchEntity.Location = new System.Drawing.Point(9, 29);
            this.lblSearchEntity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearchEntity.Name = "lblSearchEntity";
            this.lblSearchEntity.Size = new System.Drawing.Size(64, 20);
            this.lblSearchEntity.TabIndex = 81;
            this.lblSearchEntity.Text = "Search:";
            // 
            // txtSearchEntity
            // 
            this.txtSearchEntity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchEntity.Location = new System.Drawing.Point(84, 25);
            this.txtSearchEntity.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSearchEntity.Name = "txtSearchEntity";
            this.txtSearchEntity.Size = new System.Drawing.Size(309, 26);
            this.txtSearchEntity.TabIndex = 80;
            this.txtSearchEntity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnSearchKeyUp);
            // 
            // lvEntities
            // 
            this.lvEntities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvEntities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvEntities.FullRowSelect = true;
            this.lvEntities.HideSelection = false;
            this.lvEntities.Location = new System.Drawing.Point(9, 65);
            this.lvEntities.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lvEntities.Name = "lvEntities";
            this.lvEntities.Size = new System.Drawing.Size(384, 686);
            this.lvEntities.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvEntities.TabIndex = 79;
            this.lvEntities.UseCompatibleStateImageBehavior = false;
            this.lvEntities.View = System.Windows.Forms.View.Details;
            this.lvEntities.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListViewColumnClick);
            this.lvEntities.SelectedIndexChanged += new System.EventHandler(this.lvEntities_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Display name";
            this.columnHeader1.Width = 140;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Logical name";
            this.columnHeader2.Width = 100;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "ico_16_1039.gif");
            this.imageList1.Images.SetKeyName(1, "ico_16_1039_advFind.gif");
            this.imageList1.Images.SetKeyName(2, "ico_16_1039_associated.gif");
            this.imageList1.Images.SetKeyName(3, "ico_16_1039_default.gif");
            this.imageList1.Images.SetKeyName(4, "ico_16_1039_lookup.gif");
            this.imageList1.Images.SetKeyName(5, "ico_16_1039_quickFind.gif");
            this.imageList1.Images.SetKeyName(6, "userquery.png");
            // 
            // tsMain
            // 
            this.tsMain.AutoSize = false;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCloseThisTab,
            this.toolStripSeparator2,
            this.tsbLoadEntities,
            this.toolStripSeparator1,
            this.tsbEditChart,
            this.toolStripSeparator3,
            this.tsbExportCharts,
            this.tsbImportCharts});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.tsMain.Size = new System.Drawing.Size(1216, 38);
            this.tsMain.TabIndex = 85;
            this.tsMain.Text = "toolStrip1";
            // 
            // tsbCloseThisTab
            // 
            this.tsbCloseThisTab.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCloseThisTab.Image = ((System.Drawing.Image)(resources.GetObject("tsbCloseThisTab.Image")));
            this.tsbCloseThisTab.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCloseThisTab.Name = "tsbCloseThisTab";
            this.tsbCloseThisTab.Size = new System.Drawing.Size(28, 35);
            this.tsbCloseThisTab.Text = "Close this tab";
            this.tsbCloseThisTab.Click += new System.EventHandler(this.TsbCloseThisTabClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 38);
            // 
            // tsbLoadEntities
            // 
            this.tsbLoadEntities.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadEntities.Image")));
            this.tsbLoadEntities.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadEntities.Name = "tsbLoadEntities";
            this.tsbLoadEntities.Size = new System.Drawing.Size(140, 35);
            this.tsbLoadEntities.Text = "Load Entities";
            this.tsbLoadEntities.Click += new System.EventHandler(this.TsbLoadEntitiesClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // tsbEditChart
            // 
            this.tsbEditChart.Enabled = false;
            this.tsbEditChart.Image = ((System.Drawing.Image)(resources.GetObject("tsbEditChart.Image")));
            this.tsbEditChart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEditChart.Name = "tsbEditChart";
            this.tsbEditChart.Size = new System.Drawing.Size(117, 35);
            this.tsbEditChart.Text = "Edit Chart";
            this.tsbEditChart.ToolTipText = "Edit selected chart";
            this.tsbEditChart.Click += new System.EventHandler(this.tsbEditChart_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 38);
            // 
            // tsbExportCharts
            // 
            this.tsbExportCharts.Enabled = false;
            this.tsbExportCharts.Image = ((System.Drawing.Image)(resources.GetObject("tsbExportCharts.Image")));
            this.tsbExportCharts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExportCharts.Name = "tsbExportCharts";
            this.tsbExportCharts.Size = new System.Drawing.Size(143, 35);
            this.tsbExportCharts.Text = "Export charts";
            this.tsbExportCharts.ToolTipText = "Export charts that have been checked in charts list";
            this.tsbExportCharts.Click += new System.EventHandler(this.tsbExportCharts_Click);
            // 
            // tsbImportCharts
            // 
            this.tsbImportCharts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importChartsFromFileToolStripMenuItem,
            this.importChartsFromFolderToolStripMenuItem});
            this.tsbImportCharts.Image = ((System.Drawing.Image)(resources.GetObject("tsbImportCharts.Image")));
            this.tsbImportCharts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImportCharts.Name = "tsbImportCharts";
            this.tsbImportCharts.Size = new System.Drawing.Size(161, 35);
            this.tsbImportCharts.Text = "Import charts";
            // 
            // importChartsFromFileToolStripMenuItem
            // 
            this.importChartsFromFileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("importChartsFromFileToolStripMenuItem.Image")));
            this.importChartsFromFileToolStripMenuItem.Name = "importChartsFromFileToolStripMenuItem";
            this.importChartsFromFileToolStripMenuItem.Size = new System.Drawing.Size(211, 30);
            this.importChartsFromFileToolStripMenuItem.Text = "From file";
            this.importChartsFromFileToolStripMenuItem.ToolTipText = "Import one chart from one chart file definition file";
            this.importChartsFromFileToolStripMenuItem.Click += new System.EventHandler(this.importChartsFromFileToolStripMenuItem_Click);
            // 
            // importChartsFromFolderToolStripMenuItem
            // 
            this.importChartsFromFolderToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("importChartsFromFolderToolStripMenuItem.Image")));
            this.importChartsFromFolderToolStripMenuItem.Name = "importChartsFromFolderToolStripMenuItem";
            this.importChartsFromFolderToolStripMenuItem.Size = new System.Drawing.Size(211, 30);
            this.importChartsFromFolderToolStripMenuItem.Text = "From folder";
            this.importChartsFromFolderToolStripMenuItem.ToolTipText = "Import one or multiple charts from chart definition files contained in a folder";
            this.importChartsFromFolderToolStripMenuItem.Click += new System.EventHandler(this.importChartsFromFolderToolStripMenuItem_Click);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "Icon.png");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 38);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gbEntities);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gbCharts);
            this.splitContainer1.Size = new System.Drawing.Size(1216, 756);
            this.splitContainer1.SplitterDistance = 404;
            this.splitContainer1.TabIndex = 90;
            // 
            // gbCharts
            // 
            this.gbCharts.Controls.Add(this.lvCharts);
            this.gbCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbCharts.Location = new System.Drawing.Point(0, 0);
            this.gbCharts.Name = "gbCharts";
            this.gbCharts.Size = new System.Drawing.Size(808, 756);
            this.gbCharts.TabIndex = 0;
            this.gbCharts.TabStop = false;
            this.gbCharts.Text = "Charts";
            // 
            // lvCharts
            // 
            this.lvCharts.CheckBoxes = true;
            this.lvCharts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.lvCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvCharts.FullRowSelect = true;
            this.lvCharts.HoverSelection = true;
            this.lvCharts.Location = new System.Drawing.Point(3, 22);
            this.lvCharts.Name = "lvCharts";
            this.lvCharts.Size = new System.Drawing.Size(802, 731);
            this.lvCharts.TabIndex = 0;
            this.lvCharts.UseCompatibleStateImageBehavior = false;
            this.lvCharts.View = System.Windows.Forms.View.Details;
            this.lvCharts.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListViewColumnClick);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Name";
            this.columnHeader3.Width = 600;
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tsMain);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(1216, 794);
            this.gbEntities.ResumeLayout(false);
            this.gbEntities.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbCharts.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbEntities;
        private System.Windows.Forms.ListView lvEntities;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tsbLoadEntities;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ToolStripButton tsbCloseThisTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TextBox txtSearchEntity;
		private System.Windows.Forms.Label lblSearchEntity;
        private System.Windows.Forms.GroupBox gbCharts;
        private System.Windows.Forms.ListView lvCharts;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbExportCharts;
        private System.Windows.Forms.ToolStripDropDownButton tsbImportCharts;
        private System.Windows.Forms.ToolStripMenuItem importChartsFromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importChartsFromFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbEditChart;
    }
}
