namespace MsCrmTools.WebResourcesManager.Forms
{
    partial class WebPreviewDialog
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
            this.wBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // wBrowser
            // 
            this.wBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wBrowser.Location = new System.Drawing.Point(0, 0);
            this.wBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.wBrowser.Name = "wBrowser";
            this.wBrowser.Size = new System.Drawing.Size(676, 535);
            this.wBrowser.TabIndex = 0;
            this.wBrowser.TabStop = false;
            // 
            // WebPreviewDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 535);
            this.Controls.Add(this.wBrowser);
            this.Name = "WebPreviewDialog";
            this.ShowIcon = false;
            this.Text = "HTML Preview";
            this.Load += new System.EventHandler(this.WebPreviewDialogLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser wBrowser;
    }
}