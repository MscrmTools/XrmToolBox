namespace XrmToolBox.New
{
    partial class PluginsFilterInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginsFilterInfo));
            this.pnlSecurityInfo = new System.Windows.Forms.Panel();
            this.lblSecurityFilter = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlSecurityInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSecurityInfo
            // 
            this.pnlSecurityInfo.BackColor = System.Drawing.Color.LightBlue;
            this.pnlSecurityInfo.Controls.Add(this.lblSecurityFilter);
            this.pnlSecurityInfo.Controls.Add(this.pictureBox1);
            this.pnlSecurityInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSecurityInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlSecurityInfo.Name = "pnlSecurityInfo";
            this.pnlSecurityInfo.Size = new System.Drawing.Size(800, 40);
            this.pnlSecurityInfo.TabIndex = 5;
            // 
            // lblSecurityFilter
            // 
            this.lblSecurityFilter.AutoSize = true;
            this.lblSecurityFilter.Font = new System.Drawing.Font("Segoe UI Light", 14F);
            this.lblSecurityFilter.Location = new System.Drawing.Point(44, 7);
            this.lblSecurityFilter.Name = "lblSecurityFilter";
            this.lblSecurityFilter.Size = new System.Drawing.Size(670, 25);
            this.lblSecurityFilter.TabIndex = 1;
            this.lblSecurityFilter.Text = "Plugins list filtered by your IT administrator(s). Some plugins may not appear be" +
    "low\r\n";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // PluginsFilterInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlSecurityInfo);
            this.Name = "PluginsFilterInfo";
            this.Size = new System.Drawing.Size(800, 40);
            this.pnlSecurityInfo.ResumeLayout(false);
            this.pnlSecurityInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSecurityInfo;
        private System.Windows.Forms.Label lblSecurityFilter;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
