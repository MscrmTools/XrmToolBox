namespace DamSim.ViewTransferTool
{
    partial class ViewTransferTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewTransferTool));
            this.gbEntities = new System.Windows.Forms.GroupBox();
            this.lvEntities = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbSourceViews = new System.Windows.Forms.GroupBox();
            this.lvSourceViews = new System.Windows.Forms.ListView();
            this.allViewName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.allViewType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbCloseThisTab = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLoadEntities = new System.Windows.Forms.ToolStripButton();
            this.tsbTransferViews = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPublishEntity = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPublishAll = new System.Windows.Forms.ToolStripButton();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.btnSelectTarget = new System.Windows.Forms.Button();
            this.lbTargetValue = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbSourceValue = new System.Windows.Forms.Label();
            this.lbTarget = new System.Windows.Forms.Label();
            this.gbSourceViewLayout = new System.Windows.Forms.GroupBox();
            this.lvSourceViewLayoutPreview = new System.Windows.Forms.ListView();
            this.allViewIsActive = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbEntities.SuspendLayout();
            this.gbSourceViews.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.gbSourceViewLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbEntities
            // 
            this.gbEntities.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbEntities.Controls.Add(this.lvEntities);
            this.gbEntities.Enabled = false;
            this.gbEntities.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.gbEntities.Location = new System.Drawing.Point(3, 85);
            this.gbEntities.Name = "gbEntities";
            this.gbEntities.Size = new System.Drawing.Size(279, 512);
            this.gbEntities.TabIndex = 92;
            this.gbEntities.TabStop = false;
            this.gbEntities.Text = "Entities";
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
            this.lvEntities.Location = new System.Drawing.Point(6, 20);
            this.lvEntities.Name = "lvEntities";
            this.lvEntities.Size = new System.Drawing.Size(267, 486);
            this.lvEntities.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvEntities.TabIndex = 79;
            this.lvEntities.UseCompatibleStateImageBehavior = false;
            this.lvEntities.View = System.Windows.Forms.View.Details;
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
            // gbSourceViews
            // 
            this.gbSourceViews.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSourceViews.Controls.Add(this.lvSourceViews);
            this.gbSourceViews.Enabled = false;
            this.gbSourceViews.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.gbSourceViews.Location = new System.Drawing.Point(292, 85);
            this.gbSourceViews.Name = "gbSourceViews";
            this.gbSourceViews.Size = new System.Drawing.Size(505, 251);
            this.gbSourceViews.TabIndex = 91;
            this.gbSourceViews.TabStop = false;
            this.gbSourceViews.Text = "Source Views";
            // 
            // lvSourceViews
            // 
            this.lvSourceViews.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSourceViews.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.allViewName,
            this.allViewType,
            this.allViewIsActive});
            this.lvSourceViews.FullRowSelect = true;
            this.lvSourceViews.HideSelection = false;
            this.lvSourceViews.Location = new System.Drawing.Point(4, 20);
            this.lvSourceViews.Name = "lvSourceViews";
            this.lvSourceViews.Size = new System.Drawing.Size(495, 225);
            this.lvSourceViews.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvSourceViews.TabIndex = 63;
            this.lvSourceViews.UseCompatibleStateImageBehavior = false;
            this.lvSourceViews.View = System.Windows.Forms.View.Details;
            this.lvSourceViews.SelectedIndexChanged += new System.EventHandler(this.lvSourceViews_SelectedIndexChanged);
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
            // tsMain
            // 
            this.tsMain.AutoSize = false;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCloseThisTab,
            this.toolStripSeparator2,
            this.tsbLoadEntities,
            this.tsbTransferViews,
            this.toolStripSeparator1,
            this.tsbPublishEntity,
            this.toolStripSeparator3,
            this.tsbPublishAll});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(800, 25);
            this.tsMain.TabIndex = 90;
            this.tsMain.Text = "toolStrip1";
            // 
            // tsbCloseThisTab
            // 
            this.tsbCloseThisTab.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCloseThisTab.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tsbCloseThisTab.Image = ((System.Drawing.Image)(resources.GetObject("tsbCloseThisTab.Image")));
            this.tsbCloseThisTab.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCloseThisTab.Name = "tsbCloseThisTab";
            this.tsbCloseThisTab.Size = new System.Drawing.Size(23, 22);
            this.tsbCloseThisTab.Text = "Close this tab";
            this.tsbCloseThisTab.Click += new System.EventHandler(this.tsbCloseThisTab_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbLoadEntities
            // 
            this.tsbLoadEntities.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tsbLoadEntities.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadEntities.Image")));
            this.tsbLoadEntities.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadEntities.Name = "tsbLoadEntities";
            this.tsbLoadEntities.Size = new System.Drawing.Size(93, 22);
            this.tsbLoadEntities.Text = "Load Entities";
            this.tsbLoadEntities.Click += new System.EventHandler(this.tsbLoadEntities_Click);
            // 
            // tsbTransferViews
            // 
            this.tsbTransferViews.Image = ((System.Drawing.Image)(resources.GetObject("tsbTransferViews.Image")));
            this.tsbTransferViews.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTransferViews.Name = "tsbTransferViews";
            this.tsbTransferViews.Size = new System.Drawing.Size(102, 22);
            this.tsbTransferViews.Text = "Transfer views";
            this.tsbTransferViews.Click += new System.EventHandler(this.tsbTransferViews_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbPublishEntity
            // 
            this.tsbPublishEntity.Enabled = false;
            this.tsbPublishEntity.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tsbPublishEntity.Image = ((System.Drawing.Image)(resources.GetObject("tsbPublishEntity.Image")));
            this.tsbPublishEntity.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPublishEntity.Name = "tsbPublishEntity";
            this.tsbPublishEntity.Size = new System.Drawing.Size(97, 22);
            this.tsbPublishEntity.Text = "Publish entity";
            this.tsbPublishEntity.Click += new System.EventHandler(this.tsbPublishEntity_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbPublishAll
            // 
            this.tsbPublishAll.Enabled = false;
            this.tsbPublishAll.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tsbPublishAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbPublishAll.Image")));
            this.tsbPublishAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPublishAll.Name = "tsbPublishAll";
            this.tsbPublishAll.Size = new System.Drawing.Size(80, 22);
            this.tsbPublishAll.Text = "Publish all";
            this.tsbPublishAll.Click += new System.EventHandler(this.tsbPublishAll_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "DamSimIcon.png");
            // 
            // btnSelectTarget
            // 
            this.btnSelectTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectTarget.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.btnSelectTarget.Location = new System.Drawing.Point(716, 50);
            this.btnSelectTarget.Name = "btnSelectTarget";
            this.btnSelectTarget.Size = new System.Drawing.Size(75, 23);
            this.btnSelectTarget.TabIndex = 93;
            this.btnSelectTarget.Text = "Select target";
            this.btnSelectTarget.UseVisualStyleBackColor = true;
            this.btnSelectTarget.Click += new System.EventHandler(this.btnSelectTarget_Click);
            // 
            // lbTargetValue
            // 
            this.lbTargetValue.AutoSize = true;
            this.lbTargetValue.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lbTargetValue.ForeColor = System.Drawing.Color.Red;
            this.lbTargetValue.Location = new System.Drawing.Point(139, 60);
            this.lbTargetValue.Name = "lbTargetValue";
            this.lbTargetValue.Size = new System.Drawing.Size(64, 13);
            this.lbTargetValue.TabIndex = 94;
            this.lbTargetValue.Text = "Unselected";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label1.Location = new System.Drawing.Point(6, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 96;
            this.label1.Text = "Source environnement";
            // 
            // lbSourceValue
            // 
            this.lbSourceValue.AutoSize = true;
            this.lbSourceValue.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lbSourceValue.ForeColor = System.Drawing.Color.Red;
            this.lbSourceValue.Location = new System.Drawing.Point(139, 37);
            this.lbSourceValue.Name = "lbSourceValue";
            this.lbSourceValue.Size = new System.Drawing.Size(64, 13);
            this.lbSourceValue.TabIndex = 95;
            this.lbSourceValue.Text = "Unselected";
            // 
            // lbTarget
            // 
            this.lbTarget.AutoSize = true;
            this.lbTarget.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lbTarget.Location = new System.Drawing.Point(6, 60);
            this.lbTarget.Name = "lbTarget";
            this.lbTarget.Size = new System.Drawing.Size(120, 13);
            this.lbTarget.TabIndex = 97;
            this.lbTarget.Text = "Target environnement";
            // 
            // gbSourceViewLayout
            // 
            this.gbSourceViewLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSourceViewLayout.Controls.Add(this.lvSourceViewLayoutPreview);
            this.gbSourceViewLayout.Enabled = false;
            this.gbSourceViewLayout.Location = new System.Drawing.Point(292, 342);
            this.gbSourceViewLayout.Name = "gbSourceViewLayout";
            this.gbSourceViewLayout.Size = new System.Drawing.Size(505, 249);
            this.gbSourceViewLayout.TabIndex = 98;
            this.gbSourceViewLayout.TabStop = false;
            this.gbSourceViewLayout.Text = "Source view layout";
            // 
            // lvSourceViewLayoutPreview
            // 
            this.lvSourceViewLayoutPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSourceViewLayoutPreview.ForeColor = System.Drawing.Color.Black;
            this.lvSourceViewLayoutPreview.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvSourceViewLayoutPreview.Location = new System.Drawing.Point(6, 19);
            this.lvSourceViewLayoutPreview.Name = "lvSourceViewLayoutPreview";
            this.lvSourceViewLayoutPreview.Size = new System.Drawing.Size(493, 224);
            this.lvSourceViewLayoutPreview.TabIndex = 66;
            this.lvSourceViewLayoutPreview.UseCompatibleStateImageBehavior = false;
            this.lvSourceViewLayoutPreview.View = System.Windows.Forms.View.Details;
            // 
            // allViewIsActive
            // 
            this.allViewIsActive.Text = "View State";
            this.allViewIsActive.Width = 130;
            // 
            // ViewTransferTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbSourceViewLayout);
            this.Controls.Add(this.lbTarget);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbSourceValue);
            this.Controls.Add(this.lbTargetValue);
            this.Controls.Add(this.btnSelectTarget);
            this.Controls.Add(this.gbEntities);
            this.Controls.Add(this.gbSourceViews);
            this.Controls.Add(this.tsMain);
            this.Name = "ViewTransferTool";
            this.Size = new System.Drawing.Size(800, 600);
            this.gbEntities.ResumeLayout(false);
            this.gbSourceViews.ResumeLayout(false);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.gbSourceViewLayout.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbEntities;
        private System.Windows.Forms.ListView lvEntities;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.GroupBox gbSourceViews;
        private System.Windows.Forms.ListView lvSourceViews;
        private System.Windows.Forms.ColumnHeader allViewName;
        private System.Windows.Forms.ColumnHeader allViewType;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tsbCloseThisTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbLoadEntities;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbPublishEntity;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbPublishAll;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Button btnSelectTarget;
        private System.Windows.Forms.Label lbTargetValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbSourceValue;
        private System.Windows.Forms.Label lbTarget;
        private System.Windows.Forms.GroupBox gbSourceViewLayout;
        private System.Windows.Forms.ListView lvSourceViewLayoutPreview;
        private System.Windows.Forms.ToolStripButton tsbTransferViews;
        private System.Windows.Forms.ColumnHeader allViewIsActive;

    }
}
