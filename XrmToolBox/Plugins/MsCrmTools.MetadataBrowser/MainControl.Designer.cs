namespace MsCrmTools.MetadataBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainControl));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLoadEntities = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEntityColumns = new System.Windows.Forms.ToolStripButton();
            this.entityListView = new System.Windows.Forms.ListView();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.tbEntitiesList = new System.Windows.Forms.TabPage();
            this.toolStrip1.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.tbEntitiesList.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.toolStripSeparator1,
            this.tsbLoadEntities,
            this.toolStripSeparator2,
            this.tsbEntityColumns});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1162, 32);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(23, 29);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbLoadEntities
            // 
            this.tsbLoadEntities.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadEntities.Image")));
            this.tsbLoadEntities.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadEntities.Name = "tsbLoadEntities";
            this.tsbLoadEntities.Size = new System.Drawing.Size(132, 29);
            this.tsbLoadEntities.Text = "Load Entities";
            this.tsbLoadEntities.Click += new System.EventHandler(this.tsbLoadEntities_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbEntityColumns
            // 
            this.tsbEntityColumns.Image = ((System.Drawing.Image)(resources.GetObject("tsbEntityColumns.Image")));
            this.tsbEntityColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEntityColumns.Name = "tsbEntityColumns";
            this.tsbEntityColumns.Size = new System.Drawing.Size(114, 29);
            this.tsbEntityColumns.Text = "Columns...";
            this.tsbEntityColumns.Click += new System.EventHandler(this.tsbColumns_Click);
            // 
            // entityListView
            // 
            this.entityListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entityListView.FullRowSelect = true;
            this.entityListView.HideSelection = false;
            this.entityListView.Location = new System.Drawing.Point(3, 3);
            this.entityListView.Name = "entityListView";
            this.entityListView.Size = new System.Drawing.Size(1148, 674);
            this.entityListView.TabIndex = 0;
            this.entityListView.UseCompatibleStateImageBehavior = false;
            this.entityListView.View = System.Windows.Forms.View.Details;
            this.entityListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            this.entityListView.DoubleClick += new System.EventHandler(this.entityListView_DoubleClick);
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.tbEntitiesList);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 32);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(1162, 713);
            this.mainTabControl.TabIndex = 7;
            // 
            // tbEntitiesList
            // 
            this.tbEntitiesList.Controls.Add(this.entityListView);
            this.tbEntitiesList.Location = new System.Drawing.Point(4, 29);
            this.tbEntitiesList.Name = "tbEntitiesList";
            this.tbEntitiesList.Padding = new System.Windows.Forms.Padding(3);
            this.tbEntitiesList.Size = new System.Drawing.Size(1154, 680);
            this.tbEntitiesList.TabIndex = 0;
            this.tbEntitiesList.Text = "Entities";
            this.tbEntitiesList.UseVisualStyleBackColor = true;
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(1162, 745);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.tbEntitiesList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ListView entityListView;
        private System.Windows.Forms.ToolStripButton tsbLoadEntities;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbEntityColumns;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage tbEntitiesList;
    }
}
