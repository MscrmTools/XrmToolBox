namespace MsCrmTools.SiteMapEditor.Forms.WebRessources
{
    partial class CreateWebResourceDialog
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
            components = new System.ComponentModel.Container();
            panel1 = new System.Windows.Forms.Panel();
            lblHeader = new System.Windows.Forms.Label();
            btnCancel = new System.Windows.Forms.Button();
            btnValidate = new System.Windows.Forms.Button();
            toolTip = new System.Windows.Forms.ToolTip(components);
            lblFile = new System.Windows.Forms.Label();
            txtFile = new System.Windows.Forms.TextBox();
            btnBrowseFile = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            txtName = new System.Windows.Forms.TextBox();
            txtDisplayName = new System.Windows.Forms.TextBox();
            txtDescription = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.White;
            panel1.Controls.Add(lblHeader);
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(430, 60);
            panel1.TabIndex = 5;
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblHeader.Location = new System.Drawing.Point(3, 9);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new System.Drawing.Size(223, 22);
            lblHeader.TabIndex = 1;
            lblHeader.Text = "Create a new web resource";
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(338, 216);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(75, 23);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += new System.EventHandler(btnCancel_Click);
            // 
            // btnValidate
            // 
            btnValidate.Location = new System.Drawing.Point(257, 216);
            btnValidate.Name = "btnValidate";
            btnValidate.Size = new System.Drawing.Size(75, 23);
            btnValidate.TabIndex = 9;
            btnValidate.Text = "OK";
            btnValidate.UseVisualStyleBackColor = true;
            btnValidate.Click += new System.EventHandler(btnValidate_Click);
            // 
            // lblFile
            // 
            lblFile.AutoSize = true;
            lblFile.Location = new System.Drawing.Point(18, 72);
            lblFile.Name = "lblFile";
            lblFile.Size = new System.Drawing.Size(23, 13);
            lblFile.TabIndex = 15;
            lblFile.Text = "File";
            // 
            // txtFile
            // 
            txtFile.Location = new System.Drawing.Point(162, 70);
            txtFile.Name = "txtFile";
            txtFile.Size = new System.Drawing.Size(209, 20);
            txtFile.TabIndex = 16;
            // 
            // btnBrowseFile
            // 
            btnBrowseFile.Location = new System.Drawing.Point(373, 67);
            btnBrowseFile.Name = "btnBrowseFile";
            btnBrowseFile.Size = new System.Drawing.Size(40, 23);
            btnBrowseFile.TabIndex = 17;
            btnBrowseFile.Text = "...";
            btnBrowseFile.UseVisualStyleBackColor = true;
            btnBrowseFile.Click += new System.EventHandler(btnBrowseFile_Click);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(18, 100);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(35, 13);
            label1.TabIndex = 18;
            label1.Text = "Name";
            // 
            // txtName
            // 
            txtName.Location = new System.Drawing.Point(162, 97);
            txtName.Name = "txtName";
            txtName.Size = new System.Drawing.Size(251, 20);
            txtName.TabIndex = 19;
            // 
            // txtDisplayName
            // 
            txtDisplayName.Location = new System.Drawing.Point(162, 123);
            txtDisplayName.Name = "txtDisplayName";
            txtDisplayName.Size = new System.Drawing.Size(251, 20);
            txtDisplayName.TabIndex = 20;
            // 
            // txtDescription
            // 
            txtDescription.Location = new System.Drawing.Point(162, 149);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new System.Drawing.Size(251, 61);
            txtDescription.TabIndex = 21;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(18, 126);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(70, 13);
            label2.TabIndex = 22;
            label2.Text = "Display name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(18, 152);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(60, 13);
            label3.TabIndex = 23;
            label3.Text = "Description";
            // 
            // CreateWebResourceDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(427, 252);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtDescription);
            Controls.Add(txtDisplayName);
            Controls.Add(txtName);
            Controls.Add(label1);
            Controls.Add(btnBrowseFile);
            Controls.Add(txtFile);
            Controls.Add(lblFile);
            Controls.Add(btnCancel);
            Controls.Add(btnValidate);
            Controls.Add(panel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CreateWebResourceDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Create Web Resource Dialog";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnBrowseFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}