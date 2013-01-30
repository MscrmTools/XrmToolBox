namespace MsCrmTools.FetchXmlBuilder.UserControls
{
    partial class LinkEntityControl
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
            this.txtEntityName = new System.Windows.Forms.TextBox();
            this.txtAlias = new System.Windows.Forms.TextBox();
            this.txtAttributeFrom = new System.Windows.Forms.TextBox();
            this.txtAttributeTo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtEntityName
            // 
            this.txtEntityName.Location = new System.Drawing.Point(226, 64);
            this.txtEntityName.Name = "txtEntityName";
            this.txtEntityName.Size = new System.Drawing.Size(100, 20);
            this.txtEntityName.TabIndex = 0;
            // 
            // txtAlias
            // 
            this.txtAlias.Location = new System.Drawing.Point(226, 90);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.Size = new System.Drawing.Size(100, 20);
            this.txtAlias.TabIndex = 1;
            // 
            // txtAttributeFrom
            // 
            this.txtAttributeFrom.Location = new System.Drawing.Point(226, 116);
            this.txtAttributeFrom.Name = "txtAttributeFrom";
            this.txtAttributeFrom.Size = new System.Drawing.Size(100, 20);
            this.txtAttributeFrom.TabIndex = 3;
            // 
            // txtAttributeTo
            // 
            this.txtAttributeTo.Location = new System.Drawing.Point(226, 142);
            this.txtAttributeTo.Name = "txtAttributeTo";
            this.txtAttributeTo.Size = new System.Drawing.Size(100, 20);
            this.txtAttributeTo.TabIndex = 4;
            // 
            // LinkEntityControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtAttributeTo);
            this.Controls.Add(this.txtAttributeFrom);
            this.Controls.Add(this.txtAlias);
            this.Controls.Add(this.txtEntityName);
            this.Name = "LinkEntityControl";
            this.Size = new System.Drawing.Size(516, 381);
            this.Load += new System.EventHandler(this.LinkEntityControlLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEntityName;
        private System.Windows.Forms.TextBox txtAlias;
        private System.Windows.Forms.TextBox txtAttributeFrom;
        private System.Windows.Forms.TextBox txtAttributeTo;
    }
}
