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
            panel1 = new System.Windows.Forms.Panel();
            lblDescription = new System.Windows.Forms.Label();
            lblHeader = new System.Windows.Forms.Label();
            btnComponentPickerCancel = new System.Windows.Forms.Button();
            btnComponentPickerValidate = new System.Windows.Forms.Button();
            lstComponents = new System.Windows.Forms.ListView();
            columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            chkAddChildComponents = new System.Windows.Forms.CheckBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.White;
            panel1.Controls.Add(lblDescription);
            panel1.Controls.Add(lblHeader);
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(500, 60);
            panel1.TabIndex = 2;
            // 
            // lblDescription
            // 
            lblDescription.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblDescription.Location = new System.Drawing.Point(4, 31);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new System.Drawing.Size(429, 18);
            lblDescription.TabIndex = 2;
            lblDescription.Text = "Select a site map component to add to the current selected tree node";
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblHeader.Location = new System.Drawing.Point(3, 9);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new System.Drawing.Size(166, 22);
            lblHeader.TabIndex = 1;
            lblHeader.Text = "SiteMap component";
            // 
            // btnComponentPickerCancel
            // 
            btnComponentPickerCancel.Location = new System.Drawing.Point(413, 277);
            btnComponentPickerCancel.Name = "btnComponentPickerCancel";
            btnComponentPickerCancel.Size = new System.Drawing.Size(75, 23);
            btnComponentPickerCancel.TabIndex = 5;
            btnComponentPickerCancel.Text = "Cancel";
            btnComponentPickerCancel.UseVisualStyleBackColor = true;
            btnComponentPickerCancel.Click += new System.EventHandler(btnComponentPickerCancel_Click);
            // 
            // btnComponentPickerValidate
            // 
            btnComponentPickerValidate.Location = new System.Drawing.Point(332, 277);
            btnComponentPickerValidate.Name = "btnComponentPickerValidate";
            btnComponentPickerValidate.Size = new System.Drawing.Size(75, 23);
            btnComponentPickerValidate.TabIndex = 4;
            btnComponentPickerValidate.Text = "OK";
            btnComponentPickerValidate.UseVisualStyleBackColor = true;
            btnComponentPickerValidate.Click += new System.EventHandler(btnComponentPickerValidate_Click);
            // 
            // lstComponents
            // 
            lstComponents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1,
            columnHeader2,
            columnHeader3});
            lstComponents.FullRowSelect = true;
            lstComponents.GridLines = true;
            lstComponents.Location = new System.Drawing.Point(7, 66);
            lstComponents.MultiSelect = false;
            lstComponents.Name = "lstComponents";
            lstComponents.Size = new System.Drawing.Size(481, 205);
            lstComponents.TabIndex = 6;
            lstComponents.UseCompatibleStateImageBehavior = false;
            lstComponents.View = System.Windows.Forms.View.Details;
            lstComponents.DoubleClick += new System.EventHandler(lstComponents_DoubleClick);
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Id";
            columnHeader1.Width = 125;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Entity";
            columnHeader2.Width = 125;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "ResourceId";
            columnHeader3.Width = 200;
            // 
            // chkAddChildComponents
            // 
            chkAddChildComponents.AutoSize = true;
            chkAddChildComponents.Location = new System.Drawing.Point(12, 281);
            chkAddChildComponents.Name = "chkAddChildComponents";
            chkAddChildComponents.Size = new System.Drawing.Size(149, 17);
            chkAddChildComponents.TabIndex = 7;
            chkAddChildComponents.Text = "Add child components too";
            chkAddChildComponents.UseVisualStyleBackColor = true;
            // 
            // SiteMapComponentPicker
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(500, 312);
            Controls.Add(chkAddChildComponents);
            Controls.Add(lstComponents);
            Controls.Add(btnComponentPickerCancel);
            Controls.Add(btnComponentPickerValidate);
            Controls.Add(panel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SiteMapComponentPicker";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "SiteMap Component Picker";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnComponentPickerCancel;
        private System.Windows.Forms.Button btnComponentPickerValidate;
        private System.Windows.Forms.ListView lstComponents;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.CheckBox chkAddChildComponents;

    }
}