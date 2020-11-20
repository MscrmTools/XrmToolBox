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
            this.linkClose = new System.Windows.Forms.LinkLabel();
            this.lblWorkingState = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.lblXrmToolBox = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlSupport = new System.Windows.Forms.Panel();
            this.lblSupport = new System.Windows.Forms.Label();
            this.pnlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.pnlSupport.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblVersion
            // 
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI Light", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.White;
            this.lblVersion.Location = new System.Drawing.Point(577, 0);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(306, 57);
            this.lblVersion.TabIndex = 0;
            this.lblVersion.Text = "[Version]";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // linkClose
            // 
            this.linkClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkClose.AutoSize = true;
            this.linkClose.BackColor = System.Drawing.Color.Transparent;
            this.linkClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkClose.DisabledLinkColor = System.Drawing.Color.White;
            this.linkClose.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkClose.ForeColor = System.Drawing.Color.White;
            this.linkClose.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.linkClose.LinkColor = System.Drawing.Color.White;
            this.linkClose.Location = new System.Drawing.Point(824, 9);
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
            this.lblWorkingState.BackColor = System.Drawing.Color.Transparent;
            this.lblWorkingState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWorkingState.Font = new System.Drawing.Font("Segoe UI Light", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWorkingState.ForeColor = System.Drawing.Color.White;
            this.lblWorkingState.Location = new System.Drawing.Point(0, 0);
            this.lblWorkingState.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWorkingState.Name = "lblWorkingState";
            this.lblWorkingState.Size = new System.Drawing.Size(577, 57);
            this.lblWorkingState.TabIndex = 9;
            this.lblWorkingState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.Transparent;
            this.pnlFooter.Controls.Add(this.lblWorkingState);
            this.pnlFooter.Controls.Add(this.lblVersion);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 1006);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(883, 57);
            this.pnlFooter.TabIndex = 10;
            // 
            // pbLogo
            // 
            this.pbLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
            this.pbLogo.Location = new System.Drawing.Point(0, 0);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(883, 189);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbLogo.TabIndex = 11;
            this.pbLogo.TabStop = false;
            // 
            // lblXrmToolBox
            // 
            this.lblXrmToolBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblXrmToolBox.Font = new System.Drawing.Font("Segoe UI Light", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXrmToolBox.ForeColor = System.Drawing.Color.White;
            this.lblXrmToolBox.Location = new System.Drawing.Point(0, 189);
            this.lblXrmToolBox.Name = "lblXrmToolBox";
            this.lblXrmToolBox.Size = new System.Drawing.Size(883, 208);
            this.lblXrmToolBox.TabIndex = 12;
            this.lblXrmToolBox.Text = "XrmToolBox";
            this.lblXrmToolBox.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 14F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 397);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(883, 355);
            this.label1.TabIndex = 13;
            this.label1.Text = "The ultimate set of tools for Microsoft Dataverse \r\nand Microsoft Dynamics 365\r\n\r\nEmpowering Business analysts, Developers, Administrators a" +
    "nd Users since 2012";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pnlSupport
            // 
            this.pnlSupport.BackColor = System.Drawing.Color.Transparent;
            this.pnlSupport.Controls.Add(this.lblSupport);
            this.pnlSupport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSupport.Location = new System.Drawing.Point(0, 752);
            this.pnlSupport.Margin = new System.Windows.Forms.Padding(4);
            this.pnlSupport.Name = "pnlSupport";
            this.pnlSupport.Padding = new System.Windows.Forms.Padding(10);
            this.pnlSupport.Size = new System.Drawing.Size(883, 254);
            this.pnlSupport.TabIndex = 14;
            this.pnlSupport.Visible = false;
            // 
            // lblSupport
            // 
            this.lblSupport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSupport.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.lblSupport.ForeColor = System.Drawing.Color.White;
            this.lblSupport.Location = new System.Drawing.Point(10, 10);
            this.lblSupport.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSupport.Name = "lblSupport";
            this.lblSupport.Size = new System.Drawing.Size(863, 234);
            this.lblSupport.TabIndex = 2;
            this.lblSupport.Text = "Sponsored by you, {0}{1}!\r\nThank you very much for your support!";
            // 
            // WelcomeDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.linkClose;
            this.ClientSize = new System.Drawing.Size(883, 1063);
            this.ControlBox = false;
            this.Controls.Add(this.linkClose);
            this.Controls.Add(this.pnlSupport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblXrmToolBox);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.pnlFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "WelcomeDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WelcomeDialog";
            this.pnlFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.pnlSupport.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.LinkLabel linkClose;
        private System.Windows.Forms.Label lblWorkingState;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label lblXrmToolBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlSupport;
        private System.Windows.Forms.Label lblSupport;
    }
}