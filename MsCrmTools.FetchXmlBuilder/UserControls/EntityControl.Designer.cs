namespace MsCrmTools.FetchXmlBuilder.UserControls
{
    partial class EntityControl
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
            this.cbbEntities = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txtEntityName
            // 
            this.txtEntityName.Location = new System.Drawing.Point(133, 15);
            this.txtEntityName.Name = "txtEntityName";
            this.txtEntityName.Size = new System.Drawing.Size(172, 20);
            this.txtEntityName.TabIndex = 0;
            // 
            // cbbEntities
            // 
            this.cbbEntities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbEntities.FormattingEnabled = true;
            this.cbbEntities.Location = new System.Drawing.Point(133, 41);
            this.cbbEntities.Name = "cbbEntities";
            this.cbbEntities.Size = new System.Drawing.Size(172, 21);
            this.cbbEntities.TabIndex = 1;
            // 
            // EntityControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbbEntities);
            this.Controls.Add(this.txtEntityName);
            this.Name = "EntityControl";
            this.Size = new System.Drawing.Size(571, 484);
            this.Load += new System.EventHandler(this.EntityControlLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEntityName;
        private System.Windows.Forms.ComboBox cbbEntities;
    }
}
