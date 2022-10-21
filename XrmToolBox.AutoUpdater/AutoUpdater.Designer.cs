namespace XrmToolBox.AutoUpdater
{
    partial class AutoUpdater
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

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoUpdater));
            this.pbDownloadFile = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblEndInstructions = new System.Windows.Forms.Label();
            this.btnLaunchXrmToolBox = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pbDownloadFile
            // 
            this.pbDownloadFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbDownloadFile.Location = new System.Drawing.Point(0, 0);
            this.pbDownloadFile.Name = "pbDownloadFile";
            this.pbDownloadFile.Size = new System.Drawing.Size(578, 25);
            this.pbDownloadFile.TabIndex = 0;
            // 
            // lblProgress
            // 
            this.lblProgress.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProgress.Location = new System.Drawing.Point(0, 25);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(578, 25);
            this.lblProgress.TabIndex = 1;
            this.lblProgress.Text = "[Info]";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblEndInstructions
            // 
            this.lblEndInstructions.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEndInstructions.Location = new System.Drawing.Point(0, 50);
            this.lblEndInstructions.Name = "lblEndInstructions";
            this.lblEndInstructions.Size = new System.Drawing.Size(578, 69);
            this.lblEndInstructions.TabIndex = 2;
            this.lblEndInstructions.Tag = "Your existing installation has been backed up in the following directory: {0}";
            this.lblEndInstructions.Text = "Your existing installation has been backed up in the following directory: {0}";
            // 
            // btnLaunchXrmToolBox
            // 
            this.btnLaunchXrmToolBox.Location = new System.Drawing.Point(374, 123);
            this.btnLaunchXrmToolBox.Name = "btnLaunchXrmToolBox";
            this.btnLaunchXrmToolBox.Size = new System.Drawing.Size(200, 35);
            this.btnLaunchXrmToolBox.TabIndex = 3;
            this.btnLaunchXrmToolBox.Text = "Open XrmToolBox";
            this.btnLaunchXrmToolBox.UseVisualStyleBackColor = true;
            this.btnLaunchXrmToolBox.Click += new System.EventHandler(this.btnLaunchXrmToolBox_Click);
            // 
            // AutoUpdater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 51);
            this.Controls.Add(this.btnLaunchXrmToolBox);
            this.Controls.Add(this.lblEndInstructions);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.pbDownloadFile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AutoUpdater";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XrmToolBox AutoUpdater";
            this.Load += new System.EventHandler(this.AutoUpdater_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbDownloadFile;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblEndInstructions;
        private System.Windows.Forms.Button btnLaunchXrmToolBox;
    }
}

