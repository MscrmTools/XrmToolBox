namespace MsCrmTools.SiteMapEditor.Controls
{
    partial class DescriptionControl
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
            txtDescriptionLCID = new System.Windows.Forms.TextBox();
            txtDescriptionDescription = new System.Windows.Forms.TextBox();
            lblDescriptionLCID = new System.Windows.Forms.Label();
            lblDescriptionDescription = new System.Windows.Forms.Label();
            lblRequired = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // txtDescriptionLCID
            // 
            txtDescriptionLCID.Location = new System.Drawing.Point(210, 3);
            txtDescriptionLCID.Name = "txtDescriptionLCID";
            txtDescriptionLCID.Size = new System.Drawing.Size(280, 20);
            txtDescriptionLCID.TabIndex = 1;
            // 
            // txtDescriptionDescription
            // 
            txtDescriptionDescription.Location = new System.Drawing.Point(210, 29);
            txtDescriptionDescription.Name = "txtDescriptionDescription";
            txtDescriptionDescription.Size = new System.Drawing.Size(280, 20);
            txtDescriptionDescription.TabIndex = 2;
            // 
            // lblDescriptionLCID
            // 
            lblDescriptionLCID.AutoSize = true;
            lblDescriptionLCID.Location = new System.Drawing.Point(3, 6);
            lblDescriptionLCID.Name = "lblDescriptionLCID";
            lblDescriptionLCID.Size = new System.Drawing.Size(31, 13);
            lblDescriptionLCID.TabIndex = 8;
            lblDescriptionLCID.Text = "LCID";
            // 
            // lblDescriptionDescription
            // 
            lblDescriptionDescription.AutoSize = true;
            lblDescriptionDescription.Location = new System.Drawing.Point(3, 32);
            lblDescriptionDescription.Name = "lblDescriptionDescription";
            lblDescriptionDescription.Size = new System.Drawing.Size(60, 13);
            lblDescriptionDescription.TabIndex = 7;
            lblDescriptionDescription.Text = "Description";
            // 
            // lblRequired
            // 
            lblRequired.AutoSize = true;
            lblRequired.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblRequired.ForeColor = System.Drawing.Color.Red;
            lblRequired.Location = new System.Drawing.Point(34, 6);
            lblRequired.Name = "lblRequired";
            lblRequired.Size = new System.Drawing.Size(11, 13);
            lblRequired.TabIndex = 23;
            lblRequired.Text = "*";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.ForeColor = System.Drawing.Color.Red;
            label1.Location = new System.Drawing.Point(63, 32);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(11, 13);
            label1.TabIndex = 24;
            label1.Text = "*";
            // 
            // DescriptionControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(label1);
            Controls.Add(lblRequired);
            Controls.Add(txtDescriptionLCID);
            Controls.Add(txtDescriptionDescription);
            Controls.Add(lblDescriptionLCID);
            Controls.Add(lblDescriptionDescription);
            Name = "DescriptionControl";
            Size = new System.Drawing.Size(500, 400);
            Leave += new System.EventHandler(SiteMapControl_Leave);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDescriptionLCID;
        private System.Windows.Forms.TextBox txtDescriptionDescription;
        private System.Windows.Forms.Label lblDescriptionLCID;
        private System.Windows.Forms.Label lblDescriptionDescription;
        private System.Windows.Forms.Label lblRequired;
        private System.Windows.Forms.Label label1;


    }
}
