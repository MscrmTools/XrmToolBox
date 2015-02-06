namespace MsCrmTools.WebResourcesManager.Forms.Solutions
{
    partial class SolutionPicker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolutionPicker));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.btnSolutionPickerCancel = new System.Windows.Forms.Button();
            this.btnSolutionPickerValidate = new System.Windows.Forms.Button();
            this.lstSolutions = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.lblHeader);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(750, 92);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 58);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(412, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "Web resources will be added to the selected solution";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(658, 5);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(87, 77);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(4, 14);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(203, 36);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Select a solution";
            // 
            // btnSolutionPickerCancel
            // 
            this.btnSolutionPickerCancel.Location = new System.Drawing.Point(620, 386);
            this.btnSolutionPickerCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSolutionPickerCancel.Name = "btnSolutionPickerCancel";
            this.btnSolutionPickerCancel.Size = new System.Drawing.Size(112, 35);
            this.btnSolutionPickerCancel.TabIndex = 5;
            this.btnSolutionPickerCancel.Text = "Cancel";
            this.btnSolutionPickerCancel.UseVisualStyleBackColor = true;
            this.btnSolutionPickerCancel.Click += new System.EventHandler(this.btnSolutionPickerCancel_Click);
            // 
            // btnSolutionPickerValidate
            // 
            this.btnSolutionPickerValidate.Enabled = false;
            this.btnSolutionPickerValidate.Location = new System.Drawing.Point(498, 386);
            this.btnSolutionPickerValidate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSolutionPickerValidate.Name = "btnSolutionPickerValidate";
            this.btnSolutionPickerValidate.Size = new System.Drawing.Size(112, 35);
            this.btnSolutionPickerValidate.TabIndex = 4;
            this.btnSolutionPickerValidate.Text = "OK";
            this.btnSolutionPickerValidate.UseVisualStyleBackColor = true;
            this.btnSolutionPickerValidate.Click += new System.EventHandler(this.btnSolutionPickerValidate_Click);
            // 
            // lstSolutions
            // 
            this.lstSolutions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lstSolutions.Enabled = false;
            this.lstSolutions.FullRowSelect = true;
            this.lstSolutions.GridLines = true;
            this.lstSolutions.Location = new System.Drawing.Point(10, 102);
            this.lstSolutions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstSolutions.MultiSelect = false;
            this.lstSolutions.Name = "lstSolutions";
            this.lstSolutions.Size = new System.Drawing.Size(720, 273);
            this.lstSolutions.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstSolutions.TabIndex = 6;
            this.lstSolutions.UseCompatibleStateImageBehavior = false;
            this.lstSolutions.View = System.Windows.Forms.View.Details;
            this.lstSolutions.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstSolutions_ColumnClick);
            this.lstSolutions.DoubleClick += new System.EventHandler(this.lstSolutions_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Display Name";
            this.columnHeader1.Width = 125;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Version";
            this.columnHeader2.Width = 125;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Publisher";
            this.columnHeader3.Width = 200;
            // 
            // SolutionPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 435);
            this.Controls.Add(this.lstSolutions);
            this.Controls.Add(this.btnSolutionPickerCancel);
            this.Controls.Add(this.btnSolutionPickerValidate);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SolutionPicker";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Solution Picker";
            this.Load += new System.EventHandler(this.SolutionPicker_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnSolutionPickerCancel;
        private System.Windows.Forms.Button btnSolutionPickerValidate;
        private System.Windows.Forms.ListView lstSolutions;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;

    }
}