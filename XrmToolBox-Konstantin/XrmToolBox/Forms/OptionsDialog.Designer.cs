namespace XrmToolBox.Forms
{
    partial class OptionsDialog
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rdbToolsListSmall = new System.Windows.Forms.RadioButton();
            this.rdbToolsListLarge = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnResetMuList = new System.Windows.Forms.Button();
            this.chkDisplayMuFirst = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rdbToolsListSmall);
            this.groupBox1.Controls.Add(this.rdbToolsListLarge);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(445, 52);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Display";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tools list";
            // 
            // rdbToolsListSmall
            // 
            this.rdbToolsListSmall.AutoSize = true;
            this.rdbToolsListSmall.Location = new System.Drawing.Point(148, 18);
            this.rdbToolsListSmall.Name = "rdbToolsListSmall";
            this.rdbToolsListSmall.Size = new System.Drawing.Size(78, 17);
            this.rdbToolsListSmall.TabIndex = 1;
            this.rdbToolsListSmall.Text = "Small icons";
            this.rdbToolsListSmall.UseVisualStyleBackColor = true;
            // 
            // rdbToolsListLarge
            // 
            this.rdbToolsListLarge.AutoSize = true;
            this.rdbToolsListLarge.Checked = true;
            this.rdbToolsListLarge.Location = new System.Drawing.Point(61, 18);
            this.rdbToolsListLarge.Name = "rdbToolsListLarge";
            this.rdbToolsListLarge.Size = new System.Drawing.Size(80, 17);
            this.rdbToolsListLarge.TabIndex = 0;
            this.rdbToolsListLarge.TabStop = true;
            this.rdbToolsListLarge.Text = "Large icons";
            this.rdbToolsListLarge.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnResetMuList);
            this.groupBox2.Controls.Add(this.chkDisplayMuFirst);
            this.groupBox2.Location = new System.Drawing.Point(12, 70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(445, 81);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Most used tools";
            // 
            // btnResetMuList
            // 
            this.btnResetMuList.Location = new System.Drawing.Point(10, 42);
            this.btnResetMuList.Name = "btnResetMuList";
            this.btnResetMuList.Size = new System.Drawing.Size(155, 23);
            this.btnResetMuList.TabIndex = 1;
            this.btnResetMuList.Text = "Reset Most used tools list";
            this.btnResetMuList.UseVisualStyleBackColor = true;
            this.btnResetMuList.Click += new System.EventHandler(this.BtnResetMuListClick);
            // 
            // chkDisplayMuFirst
            // 
            this.chkDisplayMuFirst.AutoSize = true;
            this.chkDisplayMuFirst.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDisplayMuFirst.Location = new System.Drawing.Point(10, 19);
            this.chkDisplayMuFirst.Name = "chkDisplayMuFirst";
            this.chkDisplayMuFirst.Size = new System.Drawing.Size(155, 17);
            this.chkDisplayMuFirst.TabIndex = 0;
            this.chkDisplayMuFirst.Text = "Display most used tools first";
            this.chkDisplayMuFirst.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(382, 157);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(301, 157);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOkClick);
            // 
            // OptionsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 191);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdbToolsListSmall;
        private System.Windows.Forms.RadioButton rdbToolsListLarge;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnResetMuList;
        private System.Windows.Forms.CheckBox chkDisplayMuFirst;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
    }
}