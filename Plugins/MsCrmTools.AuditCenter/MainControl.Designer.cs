namespace MsCrmTools.AuditCenter
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
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbConnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbChangeSystemAuditStatus = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbApplyChanges = new System.Windows.Forms.ToolStripButton();
            this.toolImageList = new System.Windows.Forms.ImageList(this.components);
            this.gbGlobalSettings = new System.Windows.Forms.GroupBox();
            this.lblStatusStatus = new System.Windows.Forms.Label();
            this.lblStatusLabel = new System.Windows.Forms.Label();
            this.gbEntitiesAndAttributes = new System.Windows.Forms.GroupBox();
            this.gbAttributes = new System.Windows.Forms.GroupBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnAddAttribute = new System.Windows.Forms.ToolStripButton();
            this.btnRemoveAttribute = new System.Windows.Forms.ToolStripButton();
            this.lvAttributes = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbEntities = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAddEntity = new System.Windows.Forms.ToolStripButton();
            this.btnRemoveEntity = new System.Windows.Forms.ToolStripButton();
            this.lvEntities = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusImageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStripMenu.SuspendLayout();
            this.gbGlobalSettings.SuspendLayout();
            this.gbEntitiesAndAttributes.SuspendLayout();
            this.gbAttributes.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.gbEntities.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.toolStripSeparator2,
            this.tsbConnect,
            this.toolStripSeparator1,
            this.tsbChangeSystemAuditStatus,
            this.toolStripSeparator3,
            this.tsbApplyChanges});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(911, 25);
            this.toolStripMenu.TabIndex = 2;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(23, 22);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.TsbCloseClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbConnect
            // 
            this.tsbConnect.Image = ((System.Drawing.Image)(resources.GetObject("tsbConnect.Image")));
            this.tsbConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbConnect.Name = "tsbConnect";
            this.tsbConnect.Size = new System.Drawing.Size(79, 22);
            this.tsbConnect.Text = "Load data";
            this.tsbConnect.ToolTipText = "Load:\r\n- Entities metadata\r\n- Attributes metadata\r\n- System audit status\r\n\r\n(This" +
    " operation can take some times as it retrieves all entities and all attributes m" +
    "etadata)";
            this.tsbConnect.Click += new System.EventHandler(this.TsbConnectClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbChangeSystemAuditStatus
            // 
            this.tsbChangeSystemAuditStatus.Enabled = false;
            this.tsbChangeSystemAuditStatus.Image = ((System.Drawing.Image)(resources.GetObject("tsbChangeSystemAuditStatus.Image")));
            this.tsbChangeSystemAuditStatus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbChangeSystemAuditStatus.Name = "tsbChangeSystemAuditStatus";
            this.tsbChangeSystemAuditStatus.Size = new System.Drawing.Size(132, 22);
            this.tsbChangeSystemAuditStatus.Text = "Change audit status";
            this.tsbChangeSystemAuditStatus.Click += new System.EventHandler(this.TsbChangeSystemAuditStatusClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbApplyChanges
            // 
            this.tsbApplyChanges.Enabled = false;
            this.tsbApplyChanges.Image = ((System.Drawing.Image)(resources.GetObject("tsbApplyChanges.Image")));
            this.tsbApplyChanges.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbApplyChanges.Name = "tsbApplyChanges";
            this.tsbApplyChanges.Size = new System.Drawing.Size(105, 22);
            this.tsbApplyChanges.Text = "Apply changes";
            this.tsbApplyChanges.ToolTipText = "Apply changes made to entities and attributes.\r\n\r\nThis command does not change sy" +
    "stem audit status";
            this.tsbApplyChanges.Click += new System.EventHandler(this.TsbApplyChangesClick);
            // 
            // toolImageList
            // 
            this.toolImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("toolImageList.ImageStream")));
            this.toolImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.toolImageList.Images.SetKeyName(0, "nologo.png");
            // 
            // gbGlobalSettings
            // 
            this.gbGlobalSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGlobalSettings.Controls.Add(this.lblStatusStatus);
            this.gbGlobalSettings.Controls.Add(this.lblStatusLabel);
            this.gbGlobalSettings.Location = new System.Drawing.Point(3, 28);
            this.gbGlobalSettings.Name = "gbGlobalSettings";
            this.gbGlobalSettings.Size = new System.Drawing.Size(905, 52);
            this.gbGlobalSettings.TabIndex = 3;
            this.gbGlobalSettings.TabStop = false;
            this.gbGlobalSettings.Text = "Global audit settings";
            // 
            // lblStatusStatus
            // 
            this.lblStatusStatus.AutoSize = true;
            this.lblStatusStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusStatus.ForeColor = System.Drawing.Color.Black;
            this.lblStatusStatus.Location = new System.Drawing.Point(223, 16);
            this.lblStatusStatus.Name = "lblStatusStatus";
            this.lblStatusStatus.Size = new System.Drawing.Size(42, 24);
            this.lblStatusStatus.TabIndex = 1;
            this.lblStatusStatus.Text = "N/A";
            // 
            // lblStatusLabel
            // 
            this.lblStatusLabel.AutoSize = true;
            this.lblStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusLabel.Location = new System.Drawing.Point(6, 16);
            this.lblStatusLabel.Name = "lblStatusLabel";
            this.lblStatusLabel.Size = new System.Drawing.Size(211, 24);
            this.lblStatusLabel.TabIndex = 0;
            this.lblStatusLabel.Text = "System audit is currently";
            // 
            // gbEntitiesAndAttributes
            // 
            this.gbEntitiesAndAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbEntitiesAndAttributes.Controls.Add(this.gbAttributes);
            this.gbEntitiesAndAttributes.Controls.Add(this.gbEntities);
            this.gbEntitiesAndAttributes.Location = new System.Drawing.Point(3, 86);
            this.gbEntitiesAndAttributes.Name = "gbEntitiesAndAttributes";
            this.gbEntitiesAndAttributes.Size = new System.Drawing.Size(905, 511);
            this.gbEntitiesAndAttributes.TabIndex = 4;
            this.gbEntitiesAndAttributes.TabStop = false;
            this.gbEntitiesAndAttributes.Text = "Entities and attributes to audit";
            // 
            // gbAttributes
            // 
            this.gbAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbAttributes.Controls.Add(this.toolStrip2);
            this.gbAttributes.Controls.Add(this.lvAttributes);
            this.gbAttributes.Enabled = false;
            this.gbAttributes.Location = new System.Drawing.Point(342, 19);
            this.gbAttributes.Name = "gbAttributes";
            this.gbAttributes.Size = new System.Drawing.Size(557, 486);
            this.gbAttributes.TabIndex = 90;
            this.gbAttributes.TabStop = false;
            this.gbAttributes.Text = "Attributes";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddAttribute,
            this.btnRemoveAttribute});
            this.toolStrip2.Location = new System.Drawing.Point(3, 16);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(551, 25);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnAddAttribute
            // 
            this.btnAddAttribute.Image = ((System.Drawing.Image)(resources.GetObject("btnAddAttribute.Image")));
            this.btnAddAttribute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddAttribute.Name = "btnAddAttribute";
            this.btnAddAttribute.Size = new System.Drawing.Size(97, 22);
            this.btnAddAttribute.Text = "Add attribute";
            this.btnAddAttribute.Click += new System.EventHandler(this.PbAddAttributeClick);
            // 
            // btnRemoveAttribute
            // 
            this.btnRemoveAttribute.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveAttribute.Image")));
            this.btnRemoveAttribute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoveAttribute.Name = "btnRemoveAttribute";
            this.btnRemoveAttribute.Size = new System.Drawing.Size(177, 22);
            this.btnRemoveAttribute.Text = "Remove selected attribute(s)";
            this.btnRemoveAttribute.Click += new System.EventHandler(this.PbRemoveAttributeClick);
            // 
            // lvAttributes
            // 
            this.lvAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvAttributes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader1});
            this.lvAttributes.FullRowSelect = true;
            this.lvAttributes.Location = new System.Drawing.Point(6, 44);
            this.lvAttributes.Name = "lvAttributes";
            this.lvAttributes.Size = new System.Drawing.Size(545, 436);
            this.lvAttributes.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvAttributes.TabIndex = 2;
            this.lvAttributes.UseCompatibleStateImageBehavior = false;
            this.lvAttributes.View = System.Windows.Forms.View.Details;
            this.lvAttributes.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListViewColumnClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Attribute Display Name";
            this.columnHeader2.Width = 200;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Attribute Logical Name";
            this.columnHeader1.Width = 200;
            // 
            // gbEntities
            // 
            this.gbEntities.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbEntities.Controls.Add(this.toolStrip1);
            this.gbEntities.Controls.Add(this.lvEntities);
            this.gbEntities.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gbEntities.Enabled = false;
            this.gbEntities.Location = new System.Drawing.Point(10, 19);
            this.gbEntities.Name = "gbEntities";
            this.gbEntities.Size = new System.Drawing.Size(326, 486);
            this.gbEntities.TabIndex = 89;
            this.gbEntities.TabStop = false;
            this.gbEntities.Text = "Entities";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddEntity,
            this.btnRemoveEntity});
            this.toolStrip1.Location = new System.Drawing.Point(3, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(320, 25);
            this.toolStrip1.TabIndex = 83;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAddEntity
            // 
            this.btnAddEntity.Image = ((System.Drawing.Image)(resources.GetObject("btnAddEntity.Image")));
            this.btnAddEntity.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddEntity.Name = "btnAddEntity";
            this.btnAddEntity.Size = new System.Drawing.Size(82, 22);
            this.btnAddEntity.Text = "Add entity";
            this.btnAddEntity.Click += new System.EventHandler(this.PbAddEntityClick);
            // 
            // btnRemoveEntity
            // 
            this.btnRemoveEntity.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveEntity.Image")));
            this.btnRemoveEntity.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoveEntity.Name = "btnRemoveEntity";
            this.btnRemoveEntity.Size = new System.Drawing.Size(157, 22);
            this.btnRemoveEntity.Text = "Remove selected entities";
            this.btnRemoveEntity.Click += new System.EventHandler(this.PbRemoveEntityClick);
            // 
            // lvEntities
            // 
            this.lvEntities.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvEntities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader7});
            this.lvEntities.FullRowSelect = true;
            this.lvEntities.HideSelection = false;
            this.lvEntities.Location = new System.Drawing.Point(6, 44);
            this.lvEntities.Name = "lvEntities";
            this.lvEntities.Size = new System.Drawing.Size(314, 436);
            this.lvEntities.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvEntities.TabIndex = 79;
            this.lvEntities.UseCompatibleStateImageBehavior = false;
            this.lvEntities.View = System.Windows.Forms.View.Details;
            this.lvEntities.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListViewColumnClick);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Display name";
            this.columnHeader4.Width = 156;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Schema name";
            this.columnHeader7.Width = 130;
            // 
            // statusImageList
            // 
            this.statusImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("statusImageList.ImageStream")));
            this.statusImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.statusImageList.Images.SetKeyName(0, "16_publish.gif");
            this.statusImageList.Images.SetKeyName(1, "16_unpublish.gif");
            this.statusImageList.Images.SetKeyName(2, "17_help.png");
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbEntitiesAndAttributes);
            this.Controls.Add(this.gbGlobalSettings);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(911, 600);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.gbGlobalSettings.ResumeLayout(false);
            this.gbGlobalSettings.PerformLayout();
            this.gbEntitiesAndAttributes.ResumeLayout(false);
            this.gbAttributes.ResumeLayout(false);
            this.gbAttributes.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.gbEntities.ResumeLayout(false);
            this.gbEntities.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ImageList toolImageList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbChangeSystemAuditStatus;
        private System.Windows.Forms.GroupBox gbGlobalSettings;
        private System.Windows.Forms.Label lblStatusStatus;
        private System.Windows.Forms.Label lblStatusLabel;
        private System.Windows.Forms.GroupBox gbEntitiesAndAttributes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbConnect;
        private System.Windows.Forms.GroupBox gbEntities;
        private System.Windows.Forms.ListView lvEntities;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.GroupBox gbAttributes;
        private System.Windows.Forms.ListView lvAttributes;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnAddAttribute;
        private System.Windows.Forms.ToolStripButton btnRemoveAttribute;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAddEntity;
        private System.Windows.Forms.ToolStripButton btnRemoveEntity;
        private System.Windows.Forms.ImageList statusImageList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbApplyChanges;
    }
}
