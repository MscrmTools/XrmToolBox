namespace MsCrmTools.Iconator
{
    partial class ImageCreationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageCreationForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lvFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAddFiles = new System.Windows.Forms.Button();
            this.chkAddToDefaultSolution = new System.Windows.Forms.CheckBox();
            this.cbbSolutions = new System.Windows.Forms.ComboBox();
            this.btnCreateWebResources = new System.Windows.Forms.Button();
            this.txtPrefixToUse = new System.Windows.Forms.TextBox();
            this.chkUseSolutionPrefix = new System.Windows.Forms.CheckBox();
            this.panelWaiting = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panelWaiting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblHeader);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(424, 60);
            this.panel1.TabIndex = 6;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(3, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(280, 25);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Adding new image web resources";
            // 
            // lvFiles
            // 
            this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvFiles.Location = new System.Drawing.Point(3, 3);
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.Size = new System.Drawing.Size(398, 174);
            this.lvFiles.TabIndex = 7;
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "File name";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "File path";
            this.columnHeader2.Width = 200;
            // 
            // btnAddFiles
            // 
            this.btnAddFiles.Location = new System.Drawing.Point(3, 183);
            this.btnAddFiles.Name = "btnAddFiles";
            this.btnAddFiles.Size = new System.Drawing.Size(398, 23);
            this.btnAddFiles.TabIndex = 8;
            this.btnAddFiles.Text = "Add image(s)";
            this.btnAddFiles.UseVisualStyleBackColor = true;
            this.btnAddFiles.Click += new System.EventHandler(this.BtnAddFilesClick);
            // 
            // chkAddToDefaultSolution
            // 
            this.chkAddToDefaultSolution.AutoSize = true;
            this.chkAddToDefaultSolution.Checked = true;
            this.chkAddToDefaultSolution.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAddToDefaultSolution.Location = new System.Drawing.Point(4, 213);
            this.chkAddToDefaultSolution.Name = "chkAddToDefaultSolution";
            this.chkAddToDefaultSolution.Size = new System.Drawing.Size(131, 17);
            this.chkAddToDefaultSolution.TabIndex = 9;
            this.chkAddToDefaultSolution.Text = "Add to default solution";
            this.chkAddToDefaultSolution.UseVisualStyleBackColor = true;
            this.chkAddToDefaultSolution.CheckedChanged += new System.EventHandler(this.ChkAddToDefaultSolutionCheckedChanged);
            // 
            // cbbSolutions
            // 
            this.cbbSolutions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSolutions.Enabled = false;
            this.cbbSolutions.FormattingEnabled = true;
            this.cbbSolutions.Location = new System.Drawing.Point(141, 211);
            this.cbbSolutions.Name = "cbbSolutions";
            this.cbbSolutions.Size = new System.Drawing.Size(259, 21);
            this.cbbSolutions.TabIndex = 10;
            this.cbbSolutions.SelectedIndexChanged += new System.EventHandler(this.CbbFilesSelectedIndexChanged);
            // 
            // btnCreateWebResources
            // 
            this.btnCreateWebResources.Location = new System.Drawing.Point(2, 288);
            this.btnCreateWebResources.Name = "btnCreateWebResources";
            this.btnCreateWebResources.Size = new System.Drawing.Size(398, 23);
            this.btnCreateWebResources.TabIndex = 11;
            this.btnCreateWebResources.Text = "Create web resources with selected images";
            this.btnCreateWebResources.UseVisualStyleBackColor = true;
            this.btnCreateWebResources.Click += new System.EventHandler(this.BtnCreateWebResourcesClick);
            // 
            // txtPrefixToUse
            // 
            this.txtPrefixToUse.Enabled = false;
            this.txtPrefixToUse.Location = new System.Drawing.Point(141, 239);
            this.txtPrefixToUse.Name = "txtPrefixToUse";
            this.txtPrefixToUse.Size = new System.Drawing.Size(259, 20);
            this.txtPrefixToUse.TabIndex = 12;
            this.txtPrefixToUse.Text = "new_";
            // 
            // chkUseSolutionPrefix
            // 
            this.chkUseSolutionPrefix.AutoSize = true;
            this.chkUseSolutionPrefix.Checked = true;
            this.chkUseSolutionPrefix.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseSolutionPrefix.Location = new System.Drawing.Point(3, 239);
            this.chkUseSolutionPrefix.Name = "chkUseSolutionPrefix";
            this.chkUseSolutionPrefix.Size = new System.Drawing.Size(112, 17);
            this.chkUseSolutionPrefix.TabIndex = 13;
            this.chkUseSolutionPrefix.Text = "Use solution prefix";
            this.chkUseSolutionPrefix.UseVisualStyleBackColor = true;
            this.chkUseSolutionPrefix.CheckedChanged += new System.EventHandler(this.ChkUseSolutionPrefixCheckedChanged);
            // 
            // panelWaiting
            // 
            this.panelWaiting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panelWaiting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelWaiting.Controls.Add(this.pictureBox1);
            this.panelWaiting.Controls.Add(this.label1);
            this.panelWaiting.Location = new System.Drawing.Point(60, 150);
            this.panelWaiting.Name = "panelWaiting";
            this.panelWaiting.Size = new System.Drawing.Size(300, 100);
            this.panelWaiting.TabIndex = 14;
            this.panelWaiting.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(134, 54);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-1, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(300, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Creating web resources. Please wait...";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lvFiles);
            this.panel3.Controls.Add(this.btnAddFiles);
            this.panel3.Controls.Add(this.chkUseSolutionPrefix);
            this.panel3.Controls.Add(this.chkAddToDefaultSolution);
            this.panel3.Controls.Add(this.txtPrefixToUse);
            this.panel3.Controls.Add(this.cbbSolutions);
            this.panel3.Controls.Add(this.btnCreateWebResources);
            this.panel3.Location = new System.Drawing.Point(6, 60);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(410, 314);
            this.panel3.TabIndex = 15;
            // 
            // ImageCreationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 378);
            this.Controls.Add(this.panelWaiting);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImageCreationForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelWaiting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.ListView lvFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnAddFiles;
        private System.Windows.Forms.CheckBox chkAddToDefaultSolution;
        private System.Windows.Forms.ComboBox cbbSolutions;
        private System.Windows.Forms.Button btnCreateWebResources;
        private System.Windows.Forms.TextBox txtPrefixToUse;
        private System.Windows.Forms.CheckBox chkUseSolutionPrefix;
        private System.Windows.Forms.Panel panelWaiting;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}