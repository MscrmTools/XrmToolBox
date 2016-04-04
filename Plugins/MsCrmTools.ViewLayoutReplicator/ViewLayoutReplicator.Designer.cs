namespace MsCrmTools.ViewLayoutReplicator
{
    partial class ViewLayoutReplicator
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewLayoutReplicator));
			this.gbEntities = new System.Windows.Forms.GroupBox();
			this.txtSearchEntity = new System.Windows.Forms.TextBox();
			this.lvEntities = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.gbTargetViews = new System.Windows.Forms.GroupBox();
			this.lvTargetViews = new System.Windows.Forms.ListView();
			this.viewName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.viewType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.tsMain = new System.Windows.Forms.ToolStrip();
			this.tsbCloseThisTab = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbLoadEntities = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbSaveViews = new System.Windows.Forms.ToolStripButton();
			this.tsbPublishEntity = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbPublishAll = new System.Windows.Forms.ToolStripButton();
			this.imageList2 = new System.Windows.Forms.ImageList(this.components);
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.gbSourceViews = new System.Windows.Forms.GroupBox();
			this.lvSourceViews = new System.Windows.Forms.ListView();
			this.allViewName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.allViewType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lvSourceViewLayoutPreview = new System.Windows.Forms.ListView();
			this.lblSearchEntity = new System.Windows.Forms.Label();
			this.gbEntities.SuspendLayout();
			this.gbTargetViews.SuspendLayout();
			this.tsMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.gbSourceViews.SuspendLayout();
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
			this.gbEntities.Name = "gbEntities";
			this.gbEntities.Size = new System.Drawing.Size(270, 491);
			this.gbEntities.TabIndex = 89;
			this.gbEntities.TabStop = false;
			this.gbEntities.Text = "Entities";
			// 
			// txtSearchEntity
			// 
			this.txtSearchEntity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSearchEntity.Location = new System.Drawing.Point(56, 16);
			this.txtSearchEntity.Name = "txtSearchEntity";
			this.txtSearchEntity.Size = new System.Drawing.Size(208, 20);
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
			this.lvEntities.Location = new System.Drawing.Point(6, 42);
			this.lvEntities.Name = "lvEntities";
			this.lvEntities.Size = new System.Drawing.Size(258, 446);
			this.lvEntities.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvEntities.TabIndex = 79;
			this.lvEntities.UseCompatibleStateImageBehavior = false;
			this.lvEntities.View = System.Windows.Forms.View.Details;
			this.lvEntities.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LvEntitiesColumnClick);
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
			// gbTargetViews
			// 
			this.gbTargetViews.Controls.Add(this.lvTargetViews);
			this.gbTargetViews.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbTargetViews.Enabled = false;
			this.gbTargetViews.Location = new System.Drawing.Point(0, 0);
			this.gbTargetViews.Name = "gbTargetViews";
			this.gbTargetViews.Size = new System.Drawing.Size(538, 264);
			this.gbTargetViews.TabIndex = 87;
			this.gbTargetViews.TabStop = false;
			this.gbTargetViews.Text = "Target Views";
			// 
			// lvTargetViews
			// 
			this.lvTargetViews.CheckBoxes = true;
			this.lvTargetViews.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.viewName,
            this.viewType});
			this.lvTargetViews.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvTargetViews.Location = new System.Drawing.Point(3, 16);
			this.lvTargetViews.Name = "lvTargetViews";
			this.lvTargetViews.Size = new System.Drawing.Size(532, 245);
			this.lvTargetViews.SmallImageList = this.imageList1;
			this.lvTargetViews.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvTargetViews.TabIndex = 78;
			this.lvTargetViews.UseCompatibleStateImageBehavior = false;
			this.lvTargetViews.View = System.Windows.Forms.View.Details;
			this.lvTargetViews.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.LvTargetViewsItemChecked);
			// 
			// viewName
			// 
			this.viewName.Text = "View Name";
			this.viewName.Width = 350;
			// 
			// viewType
			// 
			this.viewType.Text = "View Type";
			this.viewType.Width = 130;
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
            this.tsbSaveViews,
            this.tsbPublishEntity,
            this.toolStripSeparator3,
            this.tsbPublishAll});
			this.tsMain.Location = new System.Drawing.Point(0, 0);
			this.tsMain.Name = "tsMain";
			this.tsMain.Size = new System.Drawing.Size(811, 25);
			this.tsMain.TabIndex = 85;
			this.tsMain.Text = "toolStrip1";
			// 
			// tsbCloseThisTab
			// 
			this.tsbCloseThisTab.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbCloseThisTab.Image = ((System.Drawing.Image)(resources.GetObject("tsbCloseThisTab.Image")));
			this.tsbCloseThisTab.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbCloseThisTab.Name = "tsbCloseThisTab";
			this.tsbCloseThisTab.Size = new System.Drawing.Size(23, 22);
			this.tsbCloseThisTab.Text = "Close this tab";
			this.tsbCloseThisTab.Click += new System.EventHandler(this.TsbCloseThisTabClick);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbLoadEntities
			// 
			this.tsbLoadEntities.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadEntities.Image")));
			this.tsbLoadEntities.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbLoadEntities.Name = "tsbLoadEntities";
			this.tsbLoadEntities.Size = new System.Drawing.Size(94, 22);
			this.tsbLoadEntities.Text = "Load Entities";
			this.tsbLoadEntities.Click += new System.EventHandler(this.TsbLoadEntitiesClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbSaveViews
			// 
			this.tsbSaveViews.Enabled = false;
			this.tsbSaveViews.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveViews.Image")));
			this.tsbSaveViews.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSaveViews.Name = "tsbSaveViews";
			this.tsbSaveViews.Size = new System.Drawing.Size(83, 22);
			this.tsbSaveViews.Text = "Save views";
			this.tsbSaveViews.Click += new System.EventHandler(this.TsbSaveViewsClick);
			// 
			// tsbPublishEntity
			// 
			this.tsbPublishEntity.Enabled = false;
			this.tsbPublishEntity.Image = ((System.Drawing.Image)(resources.GetObject("tsbPublishEntity.Image")));
			this.tsbPublishEntity.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbPublishEntity.Name = "tsbPublishEntity";
			this.tsbPublishEntity.Size = new System.Drawing.Size(99, 22);
			this.tsbPublishEntity.Text = "Publish entity";
			this.tsbPublishEntity.Click += new System.EventHandler(this.TsbPublishEntityClick);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbPublishAll
			// 
			this.tsbPublishAll.Enabled = false;
			this.tsbPublishAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbPublishAll.Image")));
			this.tsbPublishAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbPublishAll.Name = "tsbPublishAll";
			this.tsbPublishAll.Size = new System.Drawing.Size(81, 22);
			this.tsbPublishAll.Text = "Publish all";
			this.tsbPublishAll.Click += new System.EventHandler(this.TsbPublishAllClick);
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
			this.splitContainer1.Location = new System.Drawing.Point(0, 25);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.gbEntities);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(811, 491);
			this.splitContainer1.SplitterDistance = 270;
			this.splitContainer1.SplitterWidth = 3;
			this.splitContainer1.TabIndex = 90;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Margin = new System.Windows.Forms.Padding(2);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.gbSourceViews);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.gbTargetViews);
			this.splitContainer2.Size = new System.Drawing.Size(538, 491);
			this.splitContainer2.SplitterDistance = 224;
			this.splitContainer2.SplitterWidth = 3;
			this.splitContainer2.TabIndex = 0;
			// 
			// gbSourceViews
			// 
			this.gbSourceViews.Controls.Add(this.lvSourceViews);
			this.gbSourceViews.Controls.Add(this.lvSourceViewLayoutPreview);
			this.gbSourceViews.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbSourceViews.Enabled = false;
			this.gbSourceViews.Location = new System.Drawing.Point(0, 0);
			this.gbSourceViews.Name = "gbSourceViews";
			this.gbSourceViews.Size = new System.Drawing.Size(538, 224);
			this.gbSourceViews.TabIndex = 89;
			this.gbSourceViews.TabStop = false;
			this.gbSourceViews.Text = "Source Views";
			// 
			// lvSourceViews
			// 
			this.lvSourceViews.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.allViewName,
            this.allViewType});
			this.lvSourceViews.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvSourceViews.FullRowSelect = true;
			this.lvSourceViews.HideSelection = false;
			this.lvSourceViews.Location = new System.Drawing.Point(3, 16);
			this.lvSourceViews.Name = "lvSourceViews";
			this.lvSourceViews.Size = new System.Drawing.Size(532, 138);
			this.lvSourceViews.SmallImageList = this.imageList1;
			this.lvSourceViews.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvSourceViews.TabIndex = 68;
			this.lvSourceViews.UseCompatibleStateImageBehavior = false;
			this.lvSourceViews.View = System.Windows.Forms.View.Details;
			this.lvSourceViews.SelectedIndexChanged += new System.EventHandler(this.LvSourceViewsSelectedIndexChanged);
			this.lvSourceViews.DoubleClick += new System.EventHandler(this.LvSourceViewsDoubleClick);
			// 
			// allViewName
			// 
			this.allViewName.Text = "View Name";
			this.allViewName.Width = 350;
			// 
			// allViewType
			// 
			this.allViewType.Text = "View Type";
			this.allViewType.Width = 130;
			// 
			// lvSourceViewLayoutPreview
			// 
			this.lvSourceViewLayoutPreview.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lvSourceViewLayoutPreview.ForeColor = System.Drawing.Color.Black;
			this.lvSourceViewLayoutPreview.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvSourceViewLayoutPreview.Location = new System.Drawing.Point(3, 154);
			this.lvSourceViewLayoutPreview.Name = "lvSourceViewLayoutPreview";
			this.lvSourceViewLayoutPreview.Size = new System.Drawing.Size(532, 67);
			this.lvSourceViewLayoutPreview.TabIndex = 67;
			this.lvSourceViewLayoutPreview.UseCompatibleStateImageBehavior = false;
			this.lvSourceViewLayoutPreview.View = System.Windows.Forms.View.Details;
			// 
			// lblSearchEntity
			// 
			this.lblSearchEntity.AutoSize = true;
			this.lblSearchEntity.Location = new System.Drawing.Point(6, 19);
			this.lblSearchEntity.Name = "lblSearchEntity";
			this.lblSearchEntity.Size = new System.Drawing.Size(44, 13);
			this.lblSearchEntity.TabIndex = 81;
			this.lblSearchEntity.Text = "Search:";
			// 
			// ViewLayoutReplicator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.tsMain);
			this.Name = "ViewLayoutReplicator";
			this.Size = new System.Drawing.Size(811, 516);
			this.gbEntities.ResumeLayout(false);
			this.gbEntities.PerformLayout();
			this.gbTargetViews.ResumeLayout(false);
			this.tsMain.ResumeLayout(false);
			this.tsMain.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.gbSourceViews.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbEntities;
        private System.Windows.Forms.ListView lvEntities;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.GroupBox gbTargetViews;
        private System.Windows.Forms.ListView lvTargetViews;
        private System.Windows.Forms.ColumnHeader viewName;
        private System.Windows.Forms.ColumnHeader viewType;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tsbLoadEntities;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbSaveViews;
        private System.Windows.Forms.ToolStripButton tsbPublishEntity;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ToolStripButton tsbCloseThisTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbPublishAll;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox gbSourceViews;
        private System.Windows.Forms.ListView lvSourceViews;
        private System.Windows.Forms.ColumnHeader allViewName;
        private System.Windows.Forms.ColumnHeader allViewType;
        private System.Windows.Forms.ListView lvSourceViewLayoutPreview;
		private System.Windows.Forms.TextBox txtSearchEntity;
		private System.Windows.Forms.Label lblSearchEntity;
    }
}
