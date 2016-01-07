namespace MsCrmTools.SiteMapEditor.Controls
{
    partial class GroupControl
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
            chkGroupIsProfile = new System.Windows.Forms.CheckBox();
            txtGroupId = new System.Windows.Forms.TextBox();
            txtGroupUrl = new System.Windows.Forms.TextBox();
            lblGroupId = new System.Windows.Forms.Label();
            lblGroupUrl = new System.Windows.Forms.Label();
            lblGroupIsProfile = new System.Windows.Forms.Label();
            lblGroupDescriptionResourceId = new System.Windows.Forms.Label();
            lblGroupResourceId = new System.Windows.Forms.Label();
            txtGroupDescriptionResourceId = new System.Windows.Forms.TextBox();
            txtGroupResourceId = new System.Windows.Forms.TextBox();
            lblRequired = new System.Windows.Forms.Label();
            panel1 = new System.Windows.Forms.Panel();
            txtGroupDescription = new System.Windows.Forms.TextBox();
            txtGroupTitle = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // chkGroupIsProfile
            // 
            chkGroupIsProfile.AutoSize = true;
            chkGroupIsProfile.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            chkGroupIsProfile.Location = new System.Drawing.Point(210, 58);
            chkGroupIsProfile.Name = "chkGroupIsProfile";
            chkGroupIsProfile.Size = new System.Drawing.Size(15, 14);
            chkGroupIsProfile.TabIndex = 3;
            chkGroupIsProfile.UseVisualStyleBackColor = true;
            // 
            // txtGroupId
            // 
            txtGroupId.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            txtGroupId.Location = new System.Drawing.Point(210, 3);
            txtGroupId.Name = "txtGroupId";
            txtGroupId.Size = new System.Drawing.Size(280, 22);
            txtGroupId.TabIndex = 1;
            // 
            // txtGroupUrl
            // 
            txtGroupUrl.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            txtGroupUrl.Location = new System.Drawing.Point(210, 31);
            txtGroupUrl.Name = "txtGroupUrl";
            txtGroupUrl.Size = new System.Drawing.Size(280, 22);
            txtGroupUrl.TabIndex = 2;
            // 
            // lblGroupId
            // 
            lblGroupId.AutoSize = true;
            lblGroupId.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            lblGroupId.Location = new System.Drawing.Point(3, 6);
            lblGroupId.Name = "lblGroupId";
            lblGroupId.Size = new System.Drawing.Size(17, 13);
            lblGroupId.TabIndex = 10;
            lblGroupId.Text = "Id";
            // 
            // lblGroupUrl
            // 
            lblGroupUrl.AutoSize = true;
            lblGroupUrl.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            lblGroupUrl.Location = new System.Drawing.Point(3, 34);
            lblGroupUrl.Name = "lblGroupUrl";
            lblGroupUrl.Size = new System.Drawing.Size(22, 13);
            lblGroupUrl.TabIndex = 9;
            lblGroupUrl.Text = "Url";
            // 
            // lblGroupIsProfile
            // 
            lblGroupIsProfile.AutoSize = true;
            lblGroupIsProfile.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            lblGroupIsProfile.Location = new System.Drawing.Point(3, 59);
            lblGroupIsProfile.Name = "lblGroupIsProfile";
            lblGroupIsProfile.Size = new System.Drawing.Size(51, 13);
            lblGroupIsProfile.TabIndex = 8;
            lblGroupIsProfile.Text = "Is Profile";
            // 
            // lblGroupDescriptionResourceId
            // 
            lblGroupDescriptionResourceId.AutoSize = true;
            lblGroupDescriptionResourceId.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            lblGroupDescriptionResourceId.Location = new System.Drawing.Point(3, 107);
            lblGroupDescriptionResourceId.Name = "lblGroupDescriptionResourceId";
            lblGroupDescriptionResourceId.Size = new System.Drawing.Size(129, 13);
            lblGroupDescriptionResourceId.TabIndex = 14;
            lblGroupDescriptionResourceId.Text = "Description Resource Id";
            // 
            // lblGroupResourceId
            // 
            lblGroupResourceId.AutoSize = true;
            lblGroupResourceId.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            lblGroupResourceId.Location = new System.Drawing.Point(3, 81);
            lblGroupResourceId.Name = "lblGroupResourceId";
            lblGroupResourceId.Size = new System.Drawing.Size(67, 13);
            lblGroupResourceId.TabIndex = 15;
            lblGroupResourceId.Text = "Resource Id";
            // 
            // txtGroupDescriptionResourceId
            // 
            txtGroupDescriptionResourceId.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            txtGroupDescriptionResourceId.Location = new System.Drawing.Point(210, 104);
            txtGroupDescriptionResourceId.Name = "txtGroupDescriptionResourceId";
            txtGroupDescriptionResourceId.ReadOnly = true;
            txtGroupDescriptionResourceId.Size = new System.Drawing.Size(280, 22);
            txtGroupDescriptionResourceId.TabIndex = 5;
            // 
            // txtGroupResourceId
            // 
            txtGroupResourceId.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            txtGroupResourceId.Location = new System.Drawing.Point(210, 78);
            txtGroupResourceId.Name = "txtGroupResourceId";
            txtGroupResourceId.ReadOnly = true;
            txtGroupResourceId.Size = new System.Drawing.Size(280, 22);
            txtGroupResourceId.TabIndex = 4;
            // 
            // lblRequired
            // 
            lblRequired.AutoSize = true;
            lblRequired.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            lblRequired.ForeColor = System.Drawing.Color.Red;
            lblRequired.Location = new System.Drawing.Point(19, 6);
            lblRequired.Name = "lblRequired";
            lblRequired.Size = new System.Drawing.Size(12, 13);
            lblRequired.TabIndex = 24;
            lblRequired.Text = "*";
            // 
            // panel1
            // 
            panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panel1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            panel1.Location = new System.Drawing.Point(6, 147);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(490, 1);
            panel1.TabIndex = 91;
            // 
            // txtGroupDescription
            // 
            txtGroupDescription.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            txtGroupDescription.Location = new System.Drawing.Point(210, 180);
            txtGroupDescription.Name = "txtGroupDescription";
            txtGroupDescription.Size = new System.Drawing.Size(280, 22);
            txtGroupDescription.TabIndex = 90;
            // 
            // txtGroupTitle
            // 
            txtGroupTitle.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            txtGroupTitle.Location = new System.Drawing.Point(211, 154);
            txtGroupTitle.Name = "txtGroupTitle";
            txtGroupTitle.Size = new System.Drawing.Size(280, 22);
            txtGroupTitle.TabIndex = 89;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            label3.Location = new System.Drawing.Point(3, 183);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(66, 13);
            label3.TabIndex = 88;
            label3.Text = "Description";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            label2.Location = new System.Drawing.Point(3, 157);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(28, 13);
            label2.TabIndex = 87;
            label2.Text = "Title";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            label1.Location = new System.Drawing.Point(3, 131);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(119, 13);
            label1.TabIndex = 86;
            label1.Text = "Deprecated attributes";
            // 
            // GroupControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panel1);
            Controls.Add(txtGroupDescription);
            Controls.Add(txtGroupTitle);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblRequired);
            Controls.Add(txtGroupResourceId);
            Controls.Add(txtGroupDescriptionResourceId);
            Controls.Add(lblGroupResourceId);
            Controls.Add(lblGroupDescriptionResourceId);
            Controls.Add(chkGroupIsProfile);
            Controls.Add(txtGroupId);
            Controls.Add(txtGroupUrl);
            Controls.Add(lblGroupId);
            Controls.Add(lblGroupUrl);
            Controls.Add(lblGroupIsProfile);
            Name = "GroupControl";
            Size = new System.Drawing.Size(500, 400);
            Leave += new System.EventHandler(SiteMapControl_Leave);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkGroupIsProfile;
        private System.Windows.Forms.TextBox txtGroupId;
        private System.Windows.Forms.TextBox txtGroupUrl;
        private System.Windows.Forms.Label lblGroupId;
        private System.Windows.Forms.Label lblGroupUrl;
        private System.Windows.Forms.Label lblGroupIsProfile;
        private System.Windows.Forms.Label lblGroupDescriptionResourceId;
        private System.Windows.Forms.Label lblGroupResourceId;
        private System.Windows.Forms.TextBox txtGroupDescriptionResourceId;
        private System.Windows.Forms.TextBox txtGroupResourceId;
        private System.Windows.Forms.Label lblRequired;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtGroupDescription;
        private System.Windows.Forms.TextBox txtGroupTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;

    }
}
