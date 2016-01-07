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
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(4, 5);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(112, 35);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnCloseClick);
            // 
            // btnWhoAmI
            // 
            this.btnWhoAmI.Location = new System.Drawing.Point(4, 49);
            this.btnWhoAmI.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnWhoAmI.Name = "btnWhoAmI";
            this.btnWhoAmI.Size = new System.Drawing.Size(112, 35);
            this.btnWhoAmI.TabIndex = 1;
            this.btnWhoAmI.Text = "Who Am I";
            this.btnWhoAmI.UseVisualStyleBackColor = true;
            this.btnWhoAmI.Click += new System.EventHandler(this.BtnWhoAmIClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(4, 94);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 35);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SampleTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnWhoAmI);
            this.Controls.Add(this.btnClose);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SampleTool";
            this.Size = new System.Drawing.Size(450, 462);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnWhoAmI;
        private System.Windows.Forms.Button btnCancel;
    }
}
