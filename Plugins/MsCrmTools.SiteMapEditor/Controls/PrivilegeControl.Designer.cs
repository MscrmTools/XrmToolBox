namespace MsCrmTools.SiteMapEditor.Controls
{
    partial class PrivilegeControl
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
            txtPrivilegeEntity = new System.Windows.Forms.TextBox();
            lblPrivilegeEntity = new System.Windows.Forms.Label();
            chkPrivilegeWrite = new System.Windows.Forms.CheckBox();
            chkPrivilegeAppend = new System.Windows.Forms.CheckBox();
            chkPrivilegeAppendTo = new System.Windows.Forms.CheckBox();
            chkPrivilegeCreate = new System.Windows.Forms.CheckBox();
            chkPrivilegeDelete = new System.Windows.Forms.CheckBox();
            chkPrivilegeShare = new System.Windows.Forms.CheckBox();
            chkPrivilegeAssign = new System.Windows.Forms.CheckBox();
            chkPrivilegeAll = new System.Windows.Forms.CheckBox();
            chkPrivilegeAllowQuickCampaign = new System.Windows.Forms.CheckBox();
            chkPrivilegeUseInternetMarketing = new System.Windows.Forms.CheckBox();
            lblPrivilegePrivileges = new System.Windows.Forms.Label();
            chkPrivilegeRead = new System.Windows.Forms.CheckBox();
            SuspendLayout();
            // 
            // txtPrivilegeEntity
            // 
            txtPrivilegeEntity.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txtPrivilegeEntity.Location = new System.Drawing.Point(210, 3);
            txtPrivilegeEntity.Name = "txtPrivilegeEntity";
            txtPrivilegeEntity.Size = new System.Drawing.Size(280, 22);
            txtPrivilegeEntity.TabIndex = 1;
            // 
            // lblPrivilegeEntity
            // 
            lblPrivilegeEntity.AutoSize = true;
            lblPrivilegeEntity.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblPrivilegeEntity.Location = new System.Drawing.Point(3, 6);
            lblPrivilegeEntity.Name = "lblPrivilegeEntity";
            lblPrivilegeEntity.Size = new System.Drawing.Size(36, 13);
            lblPrivilegeEntity.TabIndex = 7;
            lblPrivilegeEntity.Text = "Entity";
            // 
            // chkPrivilegeWrite
            // 
            chkPrivilegeWrite.AutoSize = true;
            chkPrivilegeWrite.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chkPrivilegeWrite.Location = new System.Drawing.Point(210, 75);
            chkPrivilegeWrite.Name = "chkPrivilegeWrite";
            chkPrivilegeWrite.Size = new System.Drawing.Size(54, 17);
            chkPrivilegeWrite.TabIndex = 4;
            chkPrivilegeWrite.Text = "Write";
            chkPrivilegeWrite.UseVisualStyleBackColor = true;
            // 
            // chkPrivilegeAppend
            // 
            chkPrivilegeAppend.AutoSize = true;
            chkPrivilegeAppend.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chkPrivilegeAppend.Location = new System.Drawing.Point(209, 121);
            chkPrivilegeAppend.Name = "chkPrivilegeAppend";
            chkPrivilegeAppend.Size = new System.Drawing.Size(67, 17);
            chkPrivilegeAppend.TabIndex = 6;
            chkPrivilegeAppend.Text = "Append";
            chkPrivilegeAppend.UseVisualStyleBackColor = true;
            // 
            // chkPrivilegeAppendTo
            // 
            chkPrivilegeAppendTo.AutoSize = true;
            chkPrivilegeAppendTo.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chkPrivilegeAppendTo.Location = new System.Drawing.Point(209, 144);
            chkPrivilegeAppendTo.Name = "chkPrivilegeAppendTo";
            chkPrivilegeAppendTo.Size = new System.Drawing.Size(81, 17);
            chkPrivilegeAppendTo.TabIndex = 7;
            chkPrivilegeAppendTo.Text = "Append to";
            chkPrivilegeAppendTo.UseVisualStyleBackColor = true;
            // 
            // chkPrivilegeCreate
            // 
            chkPrivilegeCreate.AutoSize = true;
            chkPrivilegeCreate.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chkPrivilegeCreate.Location = new System.Drawing.Point(210, 29);
            chkPrivilegeCreate.Name = "chkPrivilegeCreate";
            chkPrivilegeCreate.Size = new System.Drawing.Size(59, 17);
            chkPrivilegeCreate.TabIndex = 2;
            chkPrivilegeCreate.Text = "Create";
            chkPrivilegeCreate.UseVisualStyleBackColor = true;
            // 
            // chkPrivilegeDelete
            // 
            chkPrivilegeDelete.AutoSize = true;
            chkPrivilegeDelete.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chkPrivilegeDelete.Location = new System.Drawing.Point(210, 98);
            chkPrivilegeDelete.Name = "chkPrivilegeDelete";
            chkPrivilegeDelete.Size = new System.Drawing.Size(59, 17);
            chkPrivilegeDelete.TabIndex = 5;
            chkPrivilegeDelete.Text = "Delete";
            chkPrivilegeDelete.UseVisualStyleBackColor = true;
            // 
            // chkPrivilegeShare
            // 
            chkPrivilegeShare.AutoSize = true;
            chkPrivilegeShare.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chkPrivilegeShare.Location = new System.Drawing.Point(310, 29);
            chkPrivilegeShare.Name = "chkPrivilegeShare";
            chkPrivilegeShare.Size = new System.Drawing.Size(55, 17);
            chkPrivilegeShare.TabIndex = 8;
            chkPrivilegeShare.Text = "Share";
            chkPrivilegeShare.UseVisualStyleBackColor = true;
            // 
            // chkPrivilegeAssign
            // 
            chkPrivilegeAssign.AutoSize = true;
            chkPrivilegeAssign.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chkPrivilegeAssign.Location = new System.Drawing.Point(310, 52);
            chkPrivilegeAssign.Name = "chkPrivilegeAssign";
            chkPrivilegeAssign.Size = new System.Drawing.Size(60, 17);
            chkPrivilegeAssign.TabIndex = 9;
            chkPrivilegeAssign.Text = "Assign";
            chkPrivilegeAssign.UseVisualStyleBackColor = true;
            // 
            // chkPrivilegeAll
            // 
            chkPrivilegeAll.AutoSize = true;
            chkPrivilegeAll.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chkPrivilegeAll.Location = new System.Drawing.Point(310, 75);
            chkPrivilegeAll.Name = "chkPrivilegeAll";
            chkPrivilegeAll.Size = new System.Drawing.Size(39, 17);
            chkPrivilegeAll.TabIndex = 10;
            chkPrivilegeAll.Text = "All";
            chkPrivilegeAll.UseVisualStyleBackColor = true;
            // 
            // chkPrivilegeAllowQuickCampaign
            // 
            chkPrivilegeAllowQuickCampaign.AutoSize = true;
            chkPrivilegeAllowQuickCampaign.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chkPrivilegeAllowQuickCampaign.Location = new System.Drawing.Point(310, 98);
            chkPrivilegeAllowQuickCampaign.Name = "chkPrivilegeAllowQuickCampaign";
            chkPrivilegeAllowQuickCampaign.Size = new System.Drawing.Size(143, 17);
            chkPrivilegeAllowQuickCampaign.TabIndex = 11;
            chkPrivilegeAllowQuickCampaign.Text = "Allow Quick Campaign";
            chkPrivilegeAllowQuickCampaign.UseVisualStyleBackColor = true;
            // 
            // chkPrivilegeUseInternetMarketing
            // 
            chkPrivilegeUseInternetMarketing.AutoSize = true;
            chkPrivilegeUseInternetMarketing.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chkPrivilegeUseInternetMarketing.Location = new System.Drawing.Point(310, 121);
            chkPrivilegeUseInternetMarketing.Name = "chkPrivilegeUseInternetMarketing";
            chkPrivilegeUseInternetMarketing.Size = new System.Drawing.Size(145, 17);
            chkPrivilegeUseInternetMarketing.TabIndex = 12;
            chkPrivilegeUseInternetMarketing.Text = "Use Internet Marketing";
            chkPrivilegeUseInternetMarketing.UseVisualStyleBackColor = true;
            // 
            // lblPrivilegePrivileges
            // 
            lblPrivilegePrivileges.AutoSize = true;
            lblPrivilegePrivileges.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblPrivilegePrivileges.Location = new System.Drawing.Point(3, 30);
            lblPrivilegePrivileges.Name = "lblPrivilegePrivileges";
            lblPrivilegePrivileges.Size = new System.Drawing.Size(55, 13);
            lblPrivilegePrivileges.TabIndex = 20;
            lblPrivilegePrivileges.Text = "Privileges";
            // 
            // chkPrivilegeRead
            // 
            chkPrivilegeRead.AutoSize = true;
            chkPrivilegeRead.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chkPrivilegeRead.Location = new System.Drawing.Point(210, 52);
            chkPrivilegeRead.Name = "chkPrivilegeRead";
            chkPrivilegeRead.Size = new System.Drawing.Size(52, 17);
            chkPrivilegeRead.TabIndex = 3;
            chkPrivilegeRead.Text = "Read";
            chkPrivilegeRead.UseVisualStyleBackColor = true;
            // 
            // PrivilegeControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(chkPrivilegeWrite);
            Controls.Add(chkPrivilegeAppend);
            Controls.Add(chkPrivilegeAppendTo);
            Controls.Add(chkPrivilegeCreate);
            Controls.Add(chkPrivilegeDelete);
            Controls.Add(chkPrivilegeShare);
            Controls.Add(chkPrivilegeAssign);
            Controls.Add(chkPrivilegeAll);
            Controls.Add(chkPrivilegeAllowQuickCampaign);
            Controls.Add(chkPrivilegeUseInternetMarketing);
            Controls.Add(lblPrivilegePrivileges);
            Controls.Add(chkPrivilegeRead);
            Controls.Add(txtPrivilegeEntity);
            Controls.Add(lblPrivilegeEntity);
            Name = "PrivilegeControl";
            Size = new System.Drawing.Size(500, 400);
            Leave += new System.EventHandler(SiteMapControl_Leave);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPrivilegeEntity;
        private System.Windows.Forms.Label lblPrivilegeEntity;
        private System.Windows.Forms.CheckBox chkPrivilegeWrite;
        private System.Windows.Forms.CheckBox chkPrivilegeAppend;
        private System.Windows.Forms.CheckBox chkPrivilegeAppendTo;
        private System.Windows.Forms.CheckBox chkPrivilegeCreate;
        private System.Windows.Forms.CheckBox chkPrivilegeDelete;
        private System.Windows.Forms.CheckBox chkPrivilegeShare;
        private System.Windows.Forms.CheckBox chkPrivilegeAssign;
        private System.Windows.Forms.CheckBox chkPrivilegeAll;
        private System.Windows.Forms.CheckBox chkPrivilegeAllowQuickCampaign;
        private System.Windows.Forms.CheckBox chkPrivilegeUseInternetMarketing;
        private System.Windows.Forms.Label lblPrivilegePrivileges;
        private System.Windows.Forms.CheckBox chkPrivilegeRead;
    }
}
