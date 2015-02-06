namespace XrmToolBox.Forms
{
    partial class WelcomeDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomeDialog));
            this.lblVersion = new System.Windows.Forms.Label();
            this.pbThanks = new System.Windows.Forms.PictureBox();
            this.pnlSupport = new System.Windows.Forms.Panel();
            this.lblSupport = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbThanks)).BeginInit();
            this.pnlSupport.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI Light", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.White;
            this.lblVersion.Location = new System.Drawing.Point(659, 423);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(229, 28);
            this.lblVersion.TabIndex = 0;
            this.lblVersion.Text = "[Version]";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pbThanks
            // 
            this.pbThanks.BackColor = System.Drawing.Color.Transparent;
            this.pbThanks.Image = ((System.Drawing.Image)(resources.GetObject("pbThanks.Image")));
            this.pbThanks.InitialImage = null;
            this.pbThanks.Location = new System.Drawing.Point(3, 3);
            this.pbThanks.Name = "pbThanks";
            this.pbThanks.Size = new System.Drawing.Size(133, 127);
            this.pbThanks.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbThanks.TabIndex = 1;
            this.pbThanks.TabStop = false;
            // 
            // pnlSupport
            // 
            this.pnlSupport.BackColor = System.Drawing.Color.Transparent;
            this.pnlSupport.Controls.Add(this.lblSupport);
            this.pnlSupport.Controls.Add(this.pbThanks);
            this.pnlSupport.Location = new System.Drawing.Point(40, 270);
            this.pnlSupport.Name = "pnlSupport";
            this.pnlSupport.Size = new System.Drawing.Size(848, 154);
            this.pnlSupport.TabIndex = 2;
            this.pnlSupport.Visible = false;
            // 
            // lblSupport
            // 
            this.lblSupport.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupport.ForeColor = System.Drawing.Color.White;
            this.lblSupport.Location = new System.Drawing.Point(153, 35);
            this.lblSupport.Name = "lblSupport";
            this.lblSupport.Size = new System.Drawing.Size(692, 119);
            this.lblSupport.TabIndex = 2;
            this.lblSupport.Text = "Sponsored by you, {0}{1}!\r\nThank you very much for your support!";
            // 
            // WelcomeDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(900, 460);
            this.ControlBox = false;
            this.Controls.Add(this.pnlSupport);
            this.Controls.Add(this.lblVersion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WelcomeDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "WelcomeDialog";
            ((System.ComponentModel.ISupportInitialize)(this.pbThanks)).EndInit();
            this.pnlSupport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.PictureBox pbThanks;
        private System.Windows.Forms.Panel pnlSupport;
        private System.Windows.Forms.Label lblSupport;
    }
}