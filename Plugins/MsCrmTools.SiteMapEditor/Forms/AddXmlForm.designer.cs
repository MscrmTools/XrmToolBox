namespace MsCrmTools.SiteMapEditor.Forms
{
    partial class AddXmlForm
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
            textBox1 = new System.Windows.Forms.TextBox();
            btnCancel = new System.Windows.Forms.Button();
            btnOK = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            lblDescription = new System.Windows.Forms.Label();
            lblHeader = new System.Windows.Forms.Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            textBox1.Location = new System.Drawing.Point(13, 74);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(489, 202);
            textBox1.TabIndex = 0;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnCancel.Location = new System.Drawing.Point(426, 283);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(75, 23);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += new System.EventHandler(btnCancel_Click);
            // 
            // btnOK
            // 
            btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnOK.Location = new System.Drawing.Point(345, 282);
            btnOK.Name = "btnOK";
            btnOK.Size = new System.Drawing.Size(75, 23);
            btnOK.TabIndex = 2;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += new System.EventHandler(btnOK_Click);
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
            panel1.Size = new System.Drawing.Size(516, 60);
            panel1.TabIndex = 5;
            // 
            // lblDescription
            // 
            lblDescription.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblDescription.Location = new System.Drawing.Point(4, 31);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new System.Drawing.Size(429, 18);
            lblDescription.TabIndex = 3;
            lblDescription.Text = "You can add SiteMap components to the selected one by pasting Xml in the textbox " +
                "below";
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblHeader.Location = new System.Drawing.Point(3, 9);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new System.Drawing.Size(141, 22);
            lblHeader.TabIndex = 1;
            lblHeader.Text = "Add Xml content";
            // 
            // AddXmlForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(514, 318);
            Controls.Add(panel1);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            Controls.Add(textBox1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Name = "AddXmlForm";
            ShowIcon = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblHeader;
    }
}