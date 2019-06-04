namespace XrmToolBox.Controls
{
    partial class ConnectingCdsControl
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
            this.lblConnecting = new System.Windows.Forms.Label();
            this.pbConnectionLoading = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbConnectionLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // lblConnecting
            // 
            this.lblConnecting.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblConnecting.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnecting.ForeColor = System.Drawing.Color.White;
            this.lblConnecting.Location = new System.Drawing.Point(-1, 162);
            this.lblConnecting.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblConnecting.Name = "lblConnecting";
            this.lblConnecting.Size = new System.Drawing.Size(1067, 111);
            this.lblConnecting.TabIndex = 2;
            this.lblConnecting.Tag = "Please wait while XrmToolBox is connecting to {0}";
            this.lblConnecting.Text = "Please wait while XrmToolBox is connecting to your \r\nCommon Data Service environm" +
    "ent";
            this.lblConnecting.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbConnectionLoading
            // 
            this.pbConnectionLoading.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbConnectionLoading.Image = global::XrmToolBox.Properties.Resources.connectingCds;
            this.pbConnectionLoading.Location = new System.Drawing.Point(265, 31);
            this.pbConnectionLoading.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.pbConnectionLoading.Name = "pbConnectionLoading";
            this.pbConnectionLoading.Size = new System.Drawing.Size(533, 125);
            this.pbConnectionLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbConnectionLoading.TabIndex = 1;
            this.pbConnectionLoading.TabStop = false;
            // 
            // ConnectingCdsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Purple;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblConnecting);
            this.Controls.Add(this.pbConnectionLoading);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ConnectingCdsControl";
            this.Size = new System.Drawing.Size(1064, 272);
            ((System.ComponentModel.ISupportInitialize)(this.pbConnectionLoading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblConnecting;
        private System.Windows.Forms.PictureBox pbConnectionLoading;
    }
}
