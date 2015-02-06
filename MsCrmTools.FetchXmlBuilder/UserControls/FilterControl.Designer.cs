namespace MsCrmTools.FetchXmlBuilder.UserControls
{
    partial class FilterControl
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
            this.cbbFilterOperator = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbbFilterOperator
            // 
            this.cbbFilterOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbFilterOperator.FormattingEnabled = true;
            this.cbbFilterOperator.Items.AddRange(new object[] {
            "and",
            "or"});
            this.cbbFilterOperator.Location = new System.Drawing.Point(145, 77);
            this.cbbFilterOperator.Name = "cbbFilterOperator";
            this.cbbFilterOperator.Size = new System.Drawing.Size(121, 21);
            this.cbbFilterOperator.TabIndex = 0;
            // 
            // FilterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbbFilterOperator);
            this.Name = "FilterControl";
            this.Size = new System.Drawing.Size(465, 420);
            this.Load += new System.EventHandler(this.FilterControlLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbFilterOperator;
    }
}
