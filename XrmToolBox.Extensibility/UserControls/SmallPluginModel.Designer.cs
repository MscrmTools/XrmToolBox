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
            this.pnlDonate = new System.Windows.Forms.Panel();
            this.pbDonate = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picture = new System.Windows.Forms.PictureBox();
            this.lblTitle = new XrmToolBox.Extensibility.UserControls.SmallPluginLabel();
            this.pnlDonate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDonate)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCount
            // 
            this.lblCount.AutoEllipsis = true;
            this.lblCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.Location = new System.Drawing.Point(550, 0);
            this.lblCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(75, 75);
            this.lblCount.TabIndex = 6;
            this.lblCount.Text = "[NB]";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblCount.Paint += new System.Windows.Forms.PaintEventHandler(this.SmallPluginModel_Paint);
            this.lblCount.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MouseClick);
            // 
            // pnlDonate
            // 
            this.pnlDonate.Controls.Add(this.pbDonate);
            this.pnlDonate.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlDonate.Location = new System.Drawing.Point(475, 0);
            this.pnlDonate.Name = "pnlDonate";
            this.pnlDonate.Padding = new System.Windows.Forms.Padding(20);
            this.pnlDonate.Size = new System.Drawing.Size(75, 75);
            this.pnlDonate.TabIndex = 7;
            this.pnlDonate.Visible = false;
            this.pnlDonate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MouseClick);
            // 
            // pbDonate
            // 
            this.pbDonate.BackgroundImage = global::XrmToolBox.Extensibility.Properties.Resources.paypal;
            this.pbDonate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbDonate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbDonate.Location = new System.Drawing.Point(20, 20);
            this.pbDonate.Margin = new System.Windows.Forms.Padding(4);
            this.pbDonate.Name = "pbDonate";
            this.pbDonate.Size = new System.Drawing.Size(35, 35);
            this.pbDonate.TabIndex = 8;
            this.pbDonate.TabStop = false;
            this.pbDonate.Click += new System.EventHandler(this.PbDonate_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.picture);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(75, 75);
            this.panel1.TabIndex = 8;
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MouseClick);
            // 
            // picture
            // 
            this.picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picture.Location = new System.Drawing.Point(10, 10);
            this.picture.Margin = new System.Windows.Forms.Padding(4);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(55, 55);
            this.picture.TabIndex = 1;
            this.picture.TabStop = false;
            this.picture.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MouseClick);
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.Location = new System.Drawing.Point(75, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(400, 75);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "[Label]";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MouseClick);
            // 
            // SmallPluginModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Lavender;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlDonate);
            this.Controls.Add(this.lblCount);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SmallPluginModel";
            this.Size = new System.Drawing.Size(625, 75);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SmallPluginModel_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseClick);
            this.Resize += new System.EventHandler(this.SmallPluginModel_Resize);
            this.pnlDonate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDonate)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            this.ResumeLayout(false);

        }

     



        #endregion
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Panel pnlDonate;
        private System.Windows.Forms.PictureBox pbDonate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picture;
        private SmallPluginLabel lblTitle;
    }
}
