namespace XrmToolBox.Announcement
{
    partial class AnnouncementDialog
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
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.llMoreInfo = new System.Windows.Forms.LinkLabel();
            this.llForget = new System.Windows.Forms.LinkLabel();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.pnlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.Controls.Add(this.llForget);
            this.pnlFooter.Controls.Add(this.llMoreInfo);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 410);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(800, 40);
            this.pnlFooter.TabIndex = 0;
            // 
            // llMoreInfo
            // 
            this.llMoreInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.llMoreInfo.Location = new System.Drawing.Point(694, 0);
            this.llMoreInfo.Name = "llMoreInfo";
            this.llMoreInfo.Size = new System.Drawing.Size(106, 40);
            this.llMoreInfo.TabIndex = 0;
            this.llMoreInfo.TabStop = true;
            this.llMoreInfo.Text = "More info";
            this.llMoreInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.llMoreInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llMoreInfo_LinkClicked);
            // 
            // llForget
            // 
            this.llForget.Dock = System.Windows.Forms.DockStyle.Right;
            this.llForget.Location = new System.Drawing.Point(511, 0);
            this.llForget.Name = "llForget";
            this.llForget.Size = new System.Drawing.Size(183, 40);
            this.llForget.TabIndex = 1;
            this.llForget.TabStop = true;
            this.llForget.Text = "Do not show again";
            this.llForget.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.llForget.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llForget_LinkClicked);
            // 
            // pbImage
            // 
            this.pbImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbImage.Location = new System.Drawing.Point(0, 0);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(800, 410);
            this.pbImage.TabIndex = 1;
            this.pbImage.TabStop = false;
            this.pbImage.Click += new System.EventHandler(this.pbImage_Click);
            // 
            // AnnouncementDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.pnlFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AnnouncementDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Announcement";
            this.TopMost = true;
            this.pnlFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.LinkLabel llForget;
        private System.Windows.Forms.LinkLabel llMoreInfo;
        private System.Windows.Forms.PictureBox pbImage;
    }
}