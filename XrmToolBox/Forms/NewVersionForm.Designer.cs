namespace XrmToolBox.Forms
{
    partial class NewVersionForm
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
            this.btnClose = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCurrentVersion = new System.Windows.Forms.Label();
            this.lblNewVersion = new System.Windows.Forms.Label();
            this.lblHeaderDesc = new System.Windows.Forms.Label();
            this.btnDownload = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(576, 728);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(112, 35);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(12, 234);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(676, 485);
            this.webBrowser1.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(712, 100);
            this.panel1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(319, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "A new version is available";
            // 
            // lblCurrentVersion
            // 
            this.lblCurrentVersion.AutoSize = true;
            this.lblCurrentVersion.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentVersion.Location = new System.Drawing.Point(14, 103);
            this.lblCurrentVersion.Name = "lblCurrentVersion";
            this.lblCurrentVersion.Size = new System.Drawing.Size(271, 32);
            this.lblCurrentVersion.TabIndex = 8;
            this.lblCurrentVersion.Text = "Your current version is {0}";
            // 
            // lblNewVersion
            // 
            this.lblNewVersion.AutoSize = true;
            this.lblNewVersion.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewVersion.Location = new System.Drawing.Point(14, 135);
            this.lblNewVersion.Name = "lblNewVersion";
            this.lblNewVersion.Size = new System.Drawing.Size(192, 32);
            this.lblNewVersion.TabIndex = 9;
            this.lblNewVersion.Text = "New version is {0}";
            // 
            // lblHeaderDesc
            // 
            this.lblHeaderDesc.AutoSize = true;
            this.lblHeaderDesc.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderDesc.Location = new System.Drawing.Point(14, 198);
            this.lblHeaderDesc.Name = "lblHeaderDesc";
            this.lblHeaderDesc.Size = new System.Drawing.Size(139, 32);
            this.lblHeaderDesc.TabIndex = 10;
            this.lblHeaderDesc.Text = "What\'s new";
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.Location = new System.Drawing.Point(426, 106);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(262, 75);
            this.btnDownload.TabIndex = 11;
            this.btnDownload.Text = "Update now!";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // NewVersionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(700, 775);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.lblHeaderDesc);
            this.Controls.Add(this.lblNewVersion);
            this.Controls.Add(this.lblCurrentVersion);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.btnClose);
            this.Name = "NewVersionForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCurrentVersion;
        private System.Windows.Forms.Label lblNewVersion;
        private System.Windows.Forms.Label lblHeaderDesc;
        private System.Windows.Forms.Button btnDownload;

    }
}