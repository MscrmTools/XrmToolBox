namespace MsCrmTools.SiteMapEditor.Forms
{
    partial class XmlContentDisplayDialog
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
            panel1 = new System.Windows.Forms.Panel();
            lblHeader = new System.Windows.Forms.Label();
            txtContent = new System.Windows.Forms.TextBox();
            lblDescription = new System.Windows.Forms.Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            panel1.BackColor = System.Drawing.Color.White;
            panel1.Controls.Add(lblDescription);
            panel1.Controls.Add(lblHeader);
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(500, 60);
            panel1.TabIndex = 4;
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblHeader.Location = new System.Drawing.Point(3, 9);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new System.Drawing.Size(104, 22);
            lblHeader.TabIndex = 1;
            lblHeader.Text = "Xml content";
            // 
            // txtContent
            // 
            txtContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            txtContent.Location = new System.Drawing.Point(12, 66);
            txtContent.Multiline = true;
            txtContent.Name = "txtContent";
            txtContent.ReadOnly = true;
            txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            txtContent.Size = new System.Drawing.Size(476, 235);
            txtContent.TabIndex = 5;
            // 
            // lblDescription
            // 
            lblDescription.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblDescription.Location = new System.Drawing.Point(4, 31);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new System.Drawing.Size(429, 18);
            lblDescription.TabIndex = 3;
            lblDescription.Text = "This dialog shows the Xml corresponding to the selected SiteMap component";
            // 
            // XmlContentDisplayDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(500, 313);
            Controls.Add(txtContent);
            Controls.Add(panel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Name = "XmlContentDisplayDialog";
            ShowIcon = false;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Label lblDescription;
    }
}