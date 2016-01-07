namespace MsCrmTools.RoleUpdater.Controls
{
    partial class RoleSelectionControl
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
            this.label3 = new System.Windows.Forms.Label();
            this.lvRoles = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(371, 24);
            this.label3.TabIndex = 83;
            this.label3.Text = "Select the security roles you want to update";
            // 
            // lvRoles
            // 
            this.lvRoles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvRoles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3});
            this.lvRoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lvRoles.FullRowSelect = true;
            this.lvRoles.HideSelection = false;
            this.lvRoles.Location = new System.Drawing.Point(8, 28);
            this.lvRoles.Name = "lvRoles";
            this.lvRoles.Size = new System.Drawing.Size(889, 522);
            this.lvRoles.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvRoles.TabIndex = 82;
            this.lvRoles.UseCompatibleStateImageBehavior = false;
            this.lvRoles.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 400;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Root business unit";
            this.columnHeader3.Width = 200;
            // 
            // RoleSelectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lvRoles);
            this.Name = "RoleSelectionControl";
            this.Size = new System.Drawing.Size(900, 550);
            this.Load += new System.EventHandler(this.RoleSelectionControlLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lvRoles;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}
