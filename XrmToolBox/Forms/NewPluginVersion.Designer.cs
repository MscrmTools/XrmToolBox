namespace XrmToolBox.Forms
{
    partial class NewPluginVersion
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.rtbReleaseNotes = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblPluginTitle = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.cbbReminder = new System.Windows.Forms.ComboBox();
            this.llDoNotUpdate = new System.Windows.Forms.LinkLabel();
            this.llUpdateNow = new System.Windows.Forms.LinkLabel();
            this.lblNewVersion = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rtbReleaseNotes);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pnlFooter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(25);
            this.panel1.Size = new System.Drawing.Size(640, 529);
            this.panel1.TabIndex = 0;
            // 
            // rtbReleaseNotes
            // 
            this.rtbReleaseNotes.BackColor = System.Drawing.Color.White;
            this.rtbReleaseNotes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbReleaseNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbReleaseNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rtbReleaseNotes.Location = new System.Drawing.Point(25, 178);
            this.rtbReleaseNotes.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rtbReleaseNotes.Name = "rtbReleaseNotes";
            this.rtbReleaseNotes.ReadOnly = true;
            this.rtbReleaseNotes.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbReleaseNotes.Size = new System.Drawing.Size(590, 265);
            this.rtbReleaseNotes.TabIndex = 15;
            this.rtbReleaseNotes.Text = "";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 120);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.label1.Size = new System.Drawing.Size(590, 58);
            this.label1.TabIndex = 14;
            this.label1.Tag = "A new version of the plugin {0} is available in the Plugins Store.";
            this.label1.Text = "What\'s new";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblPluginTitle);
            this.panel2.Controls.Add(this.lblNewVersion);
            this.panel2.Controls.Add(this.pbLogo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(25, 25);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.panel2.Size = new System.Drawing.Size(590, 95);
            this.panel2.TabIndex = 8;
            // 
            // lblPluginTitle
            // 
            this.lblPluginTitle.AutoEllipsis = true;
            this.lblPluginTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPluginTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPluginTitle.Location = new System.Drawing.Point(94, 1);
            this.lblPluginTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPluginTitle.Name = "lblPluginTitle";
            this.lblPluginTitle.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblPluginTitle.Size = new System.Drawing.Size(496, 60);
            this.lblPluginTitle.TabIndex = 1;
            this.lblPluginTitle.Text = "[Plugin Title]";
            this.lblPluginTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbLogo
            // 
            this.pbLogo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbLogo.Location = new System.Drawing.Point(0, 1);
            this.pbLogo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(94, 94);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.cbbReminder);
            this.pnlFooter.Controls.Add(this.llDoNotUpdate);
            this.pnlFooter.Controls.Add(this.llUpdateNow);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(25, 443);
            this.pnlFooter.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(590, 61);
            this.pnlFooter.TabIndex = 10;
            // 
            // cbbReminder
            // 
            this.cbbReminder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbReminder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbReminder.FormattingEnabled = true;
            this.cbbReminder.Items.AddRange(new object[] {
            "Don\'t remind me for 1 day",
            "Don\'t remind me for 2 days",
            "Don\'t remind me for 3 days",
            "Don\'t remind me for this version"});
            this.cbbReminder.Location = new System.Drawing.Point(401, 29);
            this.cbbReminder.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbbReminder.Name = "cbbReminder";
            this.cbbReminder.Size = new System.Drawing.Size(171, 21);
            this.cbbReminder.TabIndex = 2;
            // 
            // llDoNotUpdate
            // 
            this.llDoNotUpdate.ActiveLinkColor = System.Drawing.Color.Red;
            this.llDoNotUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llDoNotUpdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.llDoNotUpdate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llDoNotUpdate.ForeColor = System.Drawing.Color.Red;
            this.llDoNotUpdate.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llDoNotUpdate.LinkColor = System.Drawing.Color.Red;
            this.llDoNotUpdate.Location = new System.Drawing.Point(285, 18);
            this.llDoNotUpdate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.llDoNotUpdate.Name = "llDoNotUpdate";
            this.llDoNotUpdate.Size = new System.Drawing.Size(97, 38);
            this.llDoNotUpdate.TabIndex = 1;
            this.llDoNotUpdate.TabStop = true;
            this.llDoNotUpdate.Text = "No thanks";
            this.llDoNotUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.llDoNotUpdate.VisitedLinkColor = System.Drawing.Color.Red;
            this.llDoNotUpdate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDoNotUpdate_LinkClicked);
            // 
            // llUpdateNow
            // 
            this.llUpdateNow.ActiveLinkColor = System.Drawing.Color.Green;
            this.llUpdateNow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.llUpdateNow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.llUpdateNow.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llUpdateNow.ForeColor = System.Drawing.Color.Green;
            this.llUpdateNow.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llUpdateNow.LinkColor = System.Drawing.Color.Green;
            this.llUpdateNow.Location = new System.Drawing.Point(16, 18);
            this.llUpdateNow.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.llUpdateNow.MinimumSize = new System.Drawing.Size(210, 0);
            this.llUpdateNow.Name = "llUpdateNow";
            this.llUpdateNow.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.llUpdateNow.Size = new System.Drawing.Size(210, 38);
            this.llUpdateNow.TabIndex = 0;
            this.llUpdateNow.TabStop = true;
            this.llUpdateNow.Text = "Update Now";
            this.llUpdateNow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.llUpdateNow.VisitedLinkColor = System.Drawing.Color.Green;
            this.llUpdateNow.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llUpdateNow_LinkClicked);
            // 
            // lblNewVersion
            // 
            this.lblNewVersion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblNewVersion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewVersion.Location = new System.Drawing.Point(94, 61);
            this.lblNewVersion.Name = "lblNewVersion";
            this.lblNewVersion.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblNewVersion.Size = new System.Drawing.Size(496, 34);
            this.lblNewVersion.TabIndex = 2;
            this.lblNewVersion.Text = "[new version]";
            // 
            // NewPluginVersion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(640, 529);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(604, 387);
            this.Name = "NewPluginVersion";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Update available!";
            this.Resize += new System.EventHandler(this.NewPluginVersion_Resize);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblPluginTitle;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.LinkLabel llDoNotUpdate;
        private System.Windows.Forms.LinkLabel llUpdateNow;
        private System.Windows.Forms.RichTextBox rtbReleaseNotes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbReminder;
        private System.Windows.Forms.Label lblNewVersion;
    }
}