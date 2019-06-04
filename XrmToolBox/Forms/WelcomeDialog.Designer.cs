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
            this.linkClose = new System.Windows.Forms.LinkLabel();
            this.lblWorkingState = new System.Windows.Forms.Label();
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
            this.lblVersion.Location = new System.Drawing.Point(878, 531);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(306, 35);
            this.lblVersion.TabIndex = 0;
            this.lblVersion.Text = "[Version]";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pbThanks
            // 
            this.pbThanks.BackColor = System.Drawing.Color.Transparent;
            this.pbThanks.Image = ((System.Drawing.Image)(resources.GetObject("pbThanks.Image")));
            this.pbThanks.InitialImage = null;
            this.pbThanks.Location = new System.Drawing.Point(4, 4);
            this.pbThanks.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbThanks.Name = "pbThanks";
            this.pbThanks.Size = new System.Drawing.Size(178, 160);
            this.pbThanks.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbThanks.TabIndex = 1;
            this.pbThanks.TabStop = false;
            // 
            // pnlSupport
            // 
            this.pnlSupport.BackColor = System.Drawing.Color.Transparent;
            this.pnlSupport.Controls.Add(this.lblSupport);
            this.pnlSupport.Controls.Add(this.pbThanks);
            this.pnlSupport.Location = new System.Drawing.Point(54, 337);
            this.pnlSupport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlSupport.Name = "pnlSupport";
            this.pnlSupport.Size = new System.Drawing.Size(1130, 192);
            this.pnlSupport.TabIndex = 2;
            this.pnlSupport.Visible = false;
            // 
            // lblSupport
            // 
            this.lblSupport.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupport.ForeColor = System.Drawing.Color.White;
            this.lblSupport.Location = new System.Drawing.Point(204, 44);
            this.lblSupport.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSupport.Name = "lblSupport";
            this.lblSupport.Size = new System.Drawing.Size(922, 148);
            this.lblSupport.TabIndex = 2;
            this.lblSupport.Text = "Sponsored by you, {0}{1}!\r\nThank you very much for your support!";
            // 
            // linkClose
            // 
            this.linkClose.AutoSize = true;
            this.linkClose.BackColor = System.Drawing.Color.Transparent;
            this.linkClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkClose.DisabledLinkColor = System.Drawing.Color.White;
            this.linkClose.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkClose.ForeColor = System.Drawing.Color.White;
            this.linkClose.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.linkClose.LinkColor = System.Drawing.Color.White;
            this.linkClose.Location = new System.Drawing.Point(1130, 17);
            this.linkClose.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.linkClose.Name = "linkClose";
            this.linkClose.Size = new System.Drawing.Size(44, 51);
            this.linkClose.TabIndex = 8;
            this.linkClose.Text = "X";
            this.linkClose.Visible = false;
            this.linkClose.VisitedLinkColor = System.Drawing.Color.White;
            this.linkClose.Click += new System.EventHandler(this.linkClose_Click);
            // 
            // lblWorkingState
            // 
            this.lblWorkingState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWorkingState.BackColor = System.Drawing.Color.Transparent;
            this.lblWorkingState.Font = new System.Drawing.Font("Segoe UI Light", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWorkingState.ForeColor = System.Drawing.Color.White;
            this.lblWorkingState.Location = new System.Drawing.Point(22, 533);
            this.lblWorkingState.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWorkingState.Name = "lblWorkingState";
            this.lblWorkingState.Size = new System.Drawing.Size(754, 35);
            this.lblWorkingState.TabIndex = 9;
            // 
            // WelcomeDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.linkClose;
            this.ClientSize = new System.Drawing.Size(1200, 577);
            this.ControlBox = false;
            this.Controls.Add(this.lblWorkingState);
            this.Controls.Add(this.linkClose);
            this.Controls.Add(this.pnlSupport);
            this.Controls.Add(this.lblVersion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "WelcomeDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WelcomeDialog";
            ((System.ComponentModel.ISupportInitialize)(this.pbThanks)).EndInit();
            this.pnlSupport.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.PictureBox pbThanks;
        private System.Windows.Forms.Panel pnlSupport;
        private System.Windows.Forms.Label lblSupport;
        private System.Windows.Forms.LinkLabel linkClose;
        private System.Windows.Forms.Label lblWorkingState;
    }
}