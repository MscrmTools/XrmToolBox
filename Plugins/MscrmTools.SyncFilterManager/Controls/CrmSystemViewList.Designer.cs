namespace MscrmTools.SyncFilterManager.Controls
{
    partial class CrmSystemViewList
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
            this.lvViews = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvViews
            // 
            this.lvViews.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader2,
            this.columnHeader3});
            this.lvViews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvViews.FullRowSelect = true;
            this.lvViews.HideSelection = false;
            this.lvViews.Location = new System.Drawing.Point(0, 0);
            this.lvViews.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lvViews.Name = "lvViews";
            this.lvViews.Size = new System.Drawing.Size(1048, 715);
            this.lvViews.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvViews.TabIndex = 3;
            this.lvViews.UseCompatibleStateImageBehavior = false;
            this.lvViews.View = System.Windows.Forms.View.Details;
            this.lvViews.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvViews_ColumnClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Type";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Returned Type";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Description";
            this.columnHeader3.Width = 150;
            // 
            // CrmSystemViewList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvViews);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CrmSystemViewList";
            this.Size = new System.Drawing.Size(1048, 715);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvViews;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}
