namespace MsCrmTools.SampleTool
{
    partial class SampleTool
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnWhoAmI = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(3, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnCloseClick);
            // 
            // btnWhoAmI
            // 
            this.btnWhoAmI.Location = new System.Drawing.Point(3, 32);
            this.btnWhoAmI.Name = "btnWhoAmI";
            this.btnWhoAmI.Size = new System.Drawing.Size(75, 23);
            this.btnWhoAmI.TabIndex = 1;
            this.btnWhoAmI.Text = "Who Am I";
            this.btnWhoAmI.UseVisualStyleBackColor = true;
            this.btnWhoAmI.Click += new System.EventHandler(this.BtnWhoAmIClick);
            // 
            // SampleTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnWhoAmI);
            this.Controls.Add(this.btnClose);
            this.Name = "SampleTool";
            this.Size = new System.Drawing.Size(300, 300);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnWhoAmI;
    }
}
