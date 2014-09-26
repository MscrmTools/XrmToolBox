namespace MsCrmTools.SiteMapEditor.Forms
{
    partial class SiteMapComponentPicker
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.btnComponentPickerCancel = new System.Windows.Forms.Button();
            this.btnComponentPickerValidate = new System.Windows.Forms.Button();
            this.chkAddChildComponents = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lvCrm2011SiteMap = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvCrm2013SiteMap = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblDescription);
            this.panel1.Controls.Add(this.lblHeader);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(526, 60);
            this.panel1.TabIndex = 2;
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(4, 31);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(429, 18);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Select a site map component to add to the current selected tree node";
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(3, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(166, 22);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "SiteMap component";
            // 
            // btnComponentPickerCancel
            // 
            this.btnComponentPickerCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnComponentPickerCancel.Location = new System.Drawing.Point(427, 364);
            this.btnComponentPickerCancel.Name = "btnComponentPickerCancel";
            this.btnComponentPickerCancel.Size = new System.Drawing.Size(75, 23);
            this.btnComponentPickerCancel.TabIndex = 5;
            this.btnComponentPickerCancel.Text = "Cancel";
            this.btnComponentPickerCancel.UseVisualStyleBackColor = true;
            this.btnComponentPickerCancel.Click += new System.EventHandler(this.btnComponentPickerCancel_Click);
            // 
            // btnComponentPickerValidate
            // 
            this.btnComponentPickerValidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnComponentPickerValidate.Location = new System.Drawing.Point(346, 364);
            this.btnComponentPickerValidate.Name = "btnComponentPickerValidate";
            this.btnComponentPickerValidate.Size = new System.Drawing.Size(75, 23);
            this.btnComponentPickerValidate.TabIndex = 4;
            this.btnComponentPickerValidate.Text = "OK";
            this.btnComponentPickerValidate.UseVisualStyleBackColor = true;
            this.btnComponentPickerValidate.Click += new System.EventHandler(this.btnComponentPickerValidate_Click);
            // 
            // chkAddChildComponents
            // 
            this.chkAddChildComponents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAddChildComponents.AutoSize = true;
            this.chkAddChildComponents.Location = new System.Drawing.Point(12, 368);
            this.chkAddChildComponents.Name = "chkAddChildComponents";
            this.chkAddChildComponents.Size = new System.Drawing.Size(149, 17);
            this.chkAddChildComponents.TabIndex = 7;
            this.chkAddChildComponents.Text = "Add child components too";
            this.chkAddChildComponents.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 66);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(490, 292);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lvCrm2011SiteMap);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(482, 266);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Dynamics CRM 2011 SiteMap";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lvCrm2013SiteMap);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(482, 266);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Dynamics CRM 2013 SiteMap";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lvCrm2011SiteMap
            // 
            this.lvCrm2011SiteMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvCrm2011SiteMap.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvCrm2011SiteMap.FullRowSelect = true;
            this.lvCrm2011SiteMap.GridLines = true;
            this.lvCrm2011SiteMap.Location = new System.Drawing.Point(0, 0);
            this.lvCrm2011SiteMap.MultiSelect = false;
            this.lvCrm2011SiteMap.Name = "lvCrm2011SiteMap";
            this.lvCrm2011SiteMap.Size = new System.Drawing.Size(482, 266);
            this.lvCrm2011SiteMap.TabIndex = 7;
            this.lvCrm2011SiteMap.UseCompatibleStateImageBehavior = false;
            this.lvCrm2011SiteMap.View = System.Windows.Forms.View.Details;
            this.lvCrm2011SiteMap.DoubleClick += new System.EventHandler(this.lstComponents_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Id";
            this.columnHeader1.Width = 125;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Entity";
            this.columnHeader2.Width = 125;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "ResourceId";
            this.columnHeader3.Width = 200;
            // 
            // lvCrm2013SiteMap
            // 
            this.lvCrm2013SiteMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvCrm2013SiteMap.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lvCrm2013SiteMap.FullRowSelect = true;
            this.lvCrm2013SiteMap.GridLines = true;
            this.lvCrm2013SiteMap.Location = new System.Drawing.Point(0, 0);
            this.lvCrm2013SiteMap.MultiSelect = false;
            this.lvCrm2013SiteMap.Name = "lvCrm2013SiteMap";
            this.lvCrm2013SiteMap.Size = new System.Drawing.Size(482, 266);
            this.lvCrm2013SiteMap.TabIndex = 8;
            this.lvCrm2013SiteMap.UseCompatibleStateImageBehavior = false;
            this.lvCrm2013SiteMap.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Id";
            this.columnHeader4.Width = 125;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Entity";
            this.columnHeader5.Width = 125;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "ResourceId";
            this.columnHeader6.Width = 200;
            // 
            // SiteMapComponentPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 399);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.chkAddChildComponents);
            this.Controls.Add(this.btnComponentPickerCancel);
            this.Controls.Add(this.btnComponentPickerValidate);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SiteMapComponentPicker";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "SiteMap Component Picker";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnComponentPickerCancel;
        private System.Windows.Forms.Button btnComponentPickerValidate;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.CheckBox chkAddChildComponents;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView lvCrm2011SiteMap;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView lvCrm2013SiteMap;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;

    }
}