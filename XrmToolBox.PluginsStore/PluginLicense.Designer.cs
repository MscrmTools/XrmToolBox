namespace XrmToolBox.PluginsStore
{
    partial class PluginLicense
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblPluginName = new System.Windows.Forms.Label();
            this.lblAuthors = new System.Windows.Forms.Label();
            this.llLicense = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblAuthors);
            this.panel1.Controls.Add(this.lblPluginName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 13);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.llLicense);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 13);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(300, 17);
            this.panel2.TabIndex = 1;
            // 
            // lblPluginName
            // 
            this.lblPluginName.AutoSize = true;
            this.lblPluginName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPluginName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPluginName.Location = new System.Drawing.Point(0, 0);
            this.lblPluginName.Name = "lblPluginName";
            this.lblPluginName.Size = new System.Drawing.Size(83, 13);
            this.lblPluginName.TabIndex = 0;
            this.lblPluginName.Text = "[plugin name]";
            this.lblPluginName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAuthors
            // 
            this.lblAuthors.AutoSize = true;
            this.lblAuthors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAuthors.Location = new System.Drawing.Point(83, 0);
            this.lblAuthors.Name = "lblAuthors";
            this.lblAuthors.Size = new System.Drawing.Size(69, 13);
            this.lblAuthors.TabIndex = 1;
            this.lblAuthors.Tag = "Author(s): {0}";
            this.lblAuthors.Text = "Author(s): {0}";
            // 
            // llLicense
            // 
            this.llLicense.AutoSize = true;
            this.llLicense.Dock = System.Windows.Forms.DockStyle.Left;
            this.llLicense.Location = new System.Drawing.Point(0, 0);
            this.llLicense.Name = "llLicense";
            this.llLicense.Size = new System.Drawing.Size(66, 13);
            this.llLicense.TabIndex = 0;
            this.llLicense.TabStop = true;
            this.llLicense.Text = "View license";
            this.llLicense.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llLicense_LinkClicked);
            // 
            // PluginLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "PluginLicense";
            this.Size = new System.Drawing.Size(300, 30);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblAuthors;
        private System.Windows.Forms.Label lblPluginName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.LinkLabel llLicense;
    }
}
