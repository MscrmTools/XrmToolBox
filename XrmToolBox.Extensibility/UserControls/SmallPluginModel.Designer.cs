namespace XrmToolBox.Extensibility.UserControls
{
    partial class SmallPluginModel
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
            this.lblCount = new System.Windows.Forms.Label();
            this.lblTitle = new XrmToolBox.Extensibility.UserControls.SmallPluginLabel();
            this.picture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCount
            // 
            this.lblCount.AutoEllipsis = true;
            this.lblCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.Location = new System.Drawing.Point(968, 0);
            this.lblCount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(100, 100);
            this.lblCount.TabIndex = 6;
            this.lblCount.Text = "[NB]";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblCount.Paint += new System.Windows.Forms.PaintEventHandler(this.SmallPluginModel_Paint);
            this.lblCount.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MouseClick);
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblTitle.Location = new System.Drawing.Point(100, 18);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(846, 84);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "[Label]";
            this.lblTitle.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MouseClick);
            // 
            // picture
            // 
            this.picture.Location = new System.Drawing.Point(20, 16);
            this.picture.Margin = new System.Windows.Forms.Padding(6);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(66, 66);
            this.picture.TabIndex = 0;
            this.picture.TabStop = false;
            this.picture.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MouseClick);
            // 
            // SmallPluginModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Lavender;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.picture);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "SmallPluginModel";
            this.Size = new System.Drawing.Size(1068, 100);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SmallPluginModel_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseClick);
            this.Resize += new System.EventHandler(this.SmallPluginModel_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picture;
        private SmallPluginLabel lblTitle;
        private System.Windows.Forms.Label lblCount;
    }
}
