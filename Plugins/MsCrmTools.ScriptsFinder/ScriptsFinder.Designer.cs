namespace MsCrmTools.ScriptsFinder
{
    partial class ScriptsFinder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptsFinder));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbCloseThisTab = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbMainFindScripts = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExportToCsv = new System.Windows.Forms.ToolStripButton();
            this.lvScripts = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCloseThisTab,
            this.toolStripSeparator2,
            this.tsbMainFindScripts,
            this.toolStripSeparator1,
            this.tsbExportToCsv});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1215, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbCloseThisTab
            // 
            this.tsbCloseThisTab.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCloseThisTab.Image = ((System.Drawing.Image)(resources.GetObject("tsbCloseThisTab.Image")));
            this.tsbCloseThisTab.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCloseThisTab.Name = "tsbCloseThisTab";
            this.tsbCloseThisTab.Size = new System.Drawing.Size(23, 24);
            this.tsbCloseThisTab.Text = "Close this tab";
            this.tsbCloseThisTab.Click += new System.EventHandler(this.TsbCloseThisTabClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbMainFindScripts
            // 
            this.tsbMainFindScripts.Image = ((System.Drawing.Image)(resources.GetObject("tsbMainFindScripts.Image")));
            this.tsbMainFindScripts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMainFindScripts.Name = "tsbMainFindScripts";
            this.tsbMainFindScripts.Size = new System.Drawing.Size(103, 24);
            this.tsbMainFindScripts.Text = "Find scripts";
            this.tsbMainFindScripts.ToolTipText = "Loads the list of custom registered scripts";
            this.tsbMainFindScripts.Click += new System.EventHandler(this.TsbMainFindScriptsClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbExportToCsv
            // 
            this.tsbExportToCsv.Image = ((System.Drawing.Image)(resources.GetObject("tsbExportToCsv.Image")));
            this.tsbExportToCsv.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExportToCsv.Name = "tsbExportToCsv";
            this.tsbExportToCsv.Size = new System.Drawing.Size(114, 24);
            this.tsbExportToCsv.Text = "Export to csv";
            this.tsbExportToCsv.Click += new System.EventHandler(this.TsbExportToCsvClick);
            // 
            // lvScripts
            // 
            this.lvScripts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvScripts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader1,
            this.columnHeader7,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader8,
            this.columnHeader5,
            this.columnHeader6});
            this.lvScripts.FullRowSelect = true;
            this.lvScripts.HideSelection = false;
            this.lvScripts.Location = new System.Drawing.Point(4, 34);
            this.lvScripts.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lvScripts.Name = "lvScripts";
            this.lvScripts.Size = new System.Drawing.Size(1205, 699);
            this.lvScripts.TabIndex = 1;
            this.lvScripts.UseCompatibleStateImageBehavior = false;
            this.lvScripts.View = System.Windows.Forms.View.Details;
            this.lvScripts.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LvScriptsColumnClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Entity";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Entity Logical name";
            this.columnHeader7.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Form name";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Event";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Attribute";
            this.columnHeader4.Width = 150;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Attribute Logical name";
            this.columnHeader8.Width = 150;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "File";
            this.columnHeader5.Width = 150;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Method";
            this.columnHeader6.Width = 150;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Icon.png");
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Type";
            this.columnHeader9.Width = 100;
            // 
            // ScriptsFinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvScripts);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ScriptsFinder";
            this.Size = new System.Drawing.Size(1215, 738);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbCloseThisTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        internal System.Windows.Forms.ToolStripButton tsbMainFindScripts;
        private System.Windows.Forms.ListView lvScripts;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbExportToCsv;
        private System.Windows.Forms.ColumnHeader columnHeader9;
    }
}
