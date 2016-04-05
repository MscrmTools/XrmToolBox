namespace SiteMapEditor.Controls
{
    partial class TitleControl
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
            txtTitleLCID = new System.Windows.Forms.TextBox();
            txtTitleTitle = new System.Windows.Forms.TextBox();
            lblTitleLCID = new System.Windows.Forms.Label();
            lblTitleTitle = new System.Windows.Forms.Label();
            lblRequired = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // txtTitleLCID
            // 
            txtTitleLCID.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txtTitleLCID.Location = new System.Drawing.Point(210, 3);
            txtTitleLCID.Name = "txtTitleLCID";
            txtTitleLCID.Size = new System.Drawing.Size(280, 22);
            txtTitleLCID.TabIndex = 1;
            // 
            // txtTitleTitle
            // 
            txtTitleTitle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txtTitleTitle.Location = new System.Drawing.Point(210, 29);
            txtTitleTitle.Name = "txtTitleTitle";
            txtTitleTitle.Size = new System.Drawing.Size(280, 22);
            txtTitleTitle.TabIndex = 2;
            // 
            // lblTitleLCID
            // 
            lblTitleLCID.AutoSize = true;
            lblTitleLCID.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblTitleLCID.Location = new System.Drawing.Point(3, 6);
            lblTitleLCID.Name = "lblTitleLCID";
            lblTitleLCID.Size = new System.Drawing.Size(30, 13);
            lblTitleLCID.TabIndex = 8;
            lblTitleLCID.Text = "LCID";
            // 
            // lblTitleTitle
            // 
            lblTitleTitle.AutoSize = true;
            lblTitleTitle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblTitleTitle.Location = new System.Drawing.Point(3, 32);
            lblTitleTitle.Name = "lblTitleTitle";
            lblTitleTitle.Size = new System.Drawing.Size(28, 13);
            lblTitleTitle.TabIndex = 7;
            lblTitleTitle.Text = "Title";
            // 
            // lblRequired
            // 
            lblRequired.AutoSize = true;
            lblRequired.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblRequired.ForeColor = System.Drawing.Color.Red;
            lblRequired.Location = new System.Drawing.Point(34, 6);
            lblRequired.Name = "lblRequired";
            lblRequired.Size = new System.Drawing.Size(12, 13);
            lblRequired.TabIndex = 75;
            lblRequired.Text = "*";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.ForeColor = System.Drawing.Color.Red;
            label1.Location = new System.Drawing.Point(30, 32);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(12, 13);
            label1.TabIndex = 76;
            label1.Text = "*";
            // 
            // TitleControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(label1);
            Controls.Add(lblRequired);
            Controls.Add(txtTitleLCID);
            Controls.Add(txtTitleTitle);
            Controls.Add(lblTitleLCID);
            Controls.Add(lblTitleTitle);
            Name = "TitleControl";
            Size = new System.Drawing.Size(500, 400);
            Leave += new System.EventHandler(SiteMapControl_Leave);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTitleLCID;
        private System.Windows.Forms.TextBox txtTitleTitle;
        private System.Windows.Forms.Label lblTitleLCID;
        private System.Windows.Forms.Label lblTitleTitle;
        private System.Windows.Forms.Label lblRequired;
        private System.Windows.Forms.Label label1;



    }
}
