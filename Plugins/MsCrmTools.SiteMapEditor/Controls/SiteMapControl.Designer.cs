namespace MsCrmTools.SiteMapEditor.Controls
{
    partial class SiteMapControl
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

            tip.Dispose();

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtSiteMapUrl = new System.Windows.Forms.TextBox();
            lblSiteMapUrl = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // txtSiteMapUrl
            // 
            txtSiteMapUrl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txtSiteMapUrl.Location = new System.Drawing.Point(210, 3);
            txtSiteMapUrl.Name = "txtSiteMapUrl";
            txtSiteMapUrl.Size = new System.Drawing.Size(280, 22);
            txtSiteMapUrl.TabIndex = 8;
            // 
            // lblSiteMapUrl
            // 
            lblSiteMapUrl.AutoSize = true;
            lblSiteMapUrl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblSiteMapUrl.Location = new System.Drawing.Point(3, 6);
            lblSiteMapUrl.Name = "lblSiteMapUrl";
            lblSiteMapUrl.Size = new System.Drawing.Size(22, 13);
            lblSiteMapUrl.TabIndex = 7;
            lblSiteMapUrl.Text = "Url";
            // 
            // SiteMapControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(txtSiteMapUrl);
            Controls.Add(lblSiteMapUrl);
            Name = "SiteMapControl";
            Size = new System.Drawing.Size(500, 400);
            Leave += new System.EventHandler(SiteMapControl_Leave);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSiteMapUrl;
        private System.Windows.Forms.Label lblSiteMapUrl;
    }
}
