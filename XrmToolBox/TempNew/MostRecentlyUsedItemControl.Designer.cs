namespace XrmToolBox.TempNew
{
    partial class MostRecentlyUsedItemControl
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
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.lblPlugin = new System.Windows.Forms.Label();
            this.lblConnectionName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pbLogo
            // 
            this.pbLogo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbLogo.Location = new System.Drawing.Point(10, 10);
            this.pbLogo.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(60, 60);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // lblPlugin
            // 
            this.lblPlugin.AutoEllipsis = true;
            this.lblPlugin.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPlugin.Font = new System.Drawing.Font("Segoe UI Light", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlugin.Location = new System.Drawing.Point(70, 10);
            this.lblPlugin.Name = "lblPlugin";
            this.lblPlugin.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblPlugin.Size = new System.Drawing.Size(411, 34);
            this.lblPlugin.TabIndex = 1;
            this.lblPlugin.Text = "[Plugin Name]";
            // 
            // lblConnectionName
            // 
            this.lblConnectionName.AutoEllipsis = true;
            this.lblConnectionName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblConnectionName.Font = new System.Drawing.Font("Segoe UI Light", 11F);
            this.lblConnectionName.ForeColor = System.Drawing.Color.Gray;
            this.lblConnectionName.Location = new System.Drawing.Point(70, 44);
            this.lblConnectionName.Name = "lblConnectionName";
            this.lblConnectionName.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblConnectionName.Size = new System.Drawing.Size(411, 26);
            this.lblConnectionName.TabIndex = 2;
            this.lblConnectionName.Text = "[Connection Name]";
            // 
            // MostRecentlyUsedItemControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblConnectionName);
            this.Controls.Add(this.lblPlugin);
            this.Controls.Add(this.pbLogo);
            this.Margin = new System.Windows.Forms.Padding(10);
            this.Name = "MostRecentlyUsedItemControl";
            this.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.Size = new System.Drawing.Size(481, 70);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label lblPlugin;
        private System.Windows.Forms.Label lblConnectionName;
    }
}
