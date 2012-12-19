namespace MsCrmTools.SiteMapEditor.Forms.WebRessources
{
    partial class WebResourcePicker
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
            lblHeader = new System.Windows.Forms.Label();
            btnWebResourcePickerCancel = new System.Windows.Forms.Button();
            btnWebResourcePickerValidate = new System.Windows.Forms.Button();
            lstWebResources = new System.Windows.Forms.ListView();
            columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            btnNewResource = new System.Windows.Forms.Button();
            btnRefresh = new System.Windows.Forms.Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.White;
            panel1.Controls.Add(lblHeader);
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(500, 60);
            panel1.TabIndex = 2;
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblHeader.Location = new System.Drawing.Point(3, 9);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new System.Drawing.Size(181, 22);
            lblHeader.TabIndex = 1;
            lblHeader.Text = "Select a web resource";
            // 
            // btnWebResourcePickerCancel
            // 
            btnWebResourcePickerCancel.Location = new System.Drawing.Point(413, 277);
            btnWebResourcePickerCancel.Name = "btnWebResourcePickerCancel";
            btnWebResourcePickerCancel.Size = new System.Drawing.Size(75, 23);
            btnWebResourcePickerCancel.TabIndex = 5;
            btnWebResourcePickerCancel.Text = "Cancel";
            btnWebResourcePickerCancel.UseVisualStyleBackColor = true;
            btnWebResourcePickerCancel.Click += new System.EventHandler(btnWebResourcePickerCancel_Click);
            // 
            // btnWebResourcePickerValidate
            // 
            btnWebResourcePickerValidate.Location = new System.Drawing.Point(332, 277);
            btnWebResourcePickerValidate.Name = "btnWebResourcePickerValidate";
            btnWebResourcePickerValidate.Size = new System.Drawing.Size(75, 23);
            btnWebResourcePickerValidate.TabIndex = 4;
            btnWebResourcePickerValidate.Text = "OK";
            btnWebResourcePickerValidate.UseVisualStyleBackColor = true;
            btnWebResourcePickerValidate.Click += new System.EventHandler(btnWebResourcePickerValidate_Click);
            // 
            // lstWebResources
            // 
            lstWebResources.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1,
            columnHeader2});
            lstWebResources.FullRowSelect = true;
            lstWebResources.GridLines = true;
            lstWebResources.Location = new System.Drawing.Point(7, 66);
            lstWebResources.MultiSelect = false;
            lstWebResources.Name = "lstWebResources";
            lstWebResources.Size = new System.Drawing.Size(481, 179);
            lstWebResources.TabIndex = 6;
            lstWebResources.UseCompatibleStateImageBehavior = false;
            lstWebResources.View = System.Windows.Forms.View.Details;
            lstWebResources.DoubleClick += new System.EventHandler(lstWebResources_DoubleClick);
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Display Name";
            columnHeader1.Width = 175;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Name";
            columnHeader2.Width = 250;
            // 
            // btnNewResource
            // 
            btnNewResource.Location = new System.Drawing.Point(7, 251);
            btnNewResource.Name = "btnNewResource";
            btnNewResource.Size = new System.Drawing.Size(100, 23);
            btnNewResource.TabIndex = 7;
            btnNewResource.Text = "New resource";
            btnNewResource.UseVisualStyleBackColor = true;
            btnNewResource.Click += new System.EventHandler(btnNewResource_Click);
            // 
            // btnRefresh
            // 
            btnRefresh.Enabled = false;
            btnRefresh.Location = new System.Drawing.Point(113, 251);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new System.Drawing.Size(100, 23);
            btnRefresh.TabIndex = 8;
            btnRefresh.Text = "Refresh list";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += new System.EventHandler(BtnRefreshClick);
            // 
            // WebResourcePicker
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(500, 312);
            Controls.Add(btnRefresh);
            Controls.Add(btnNewResource);
            Controls.Add(lstWebResources);
            Controls.Add(btnWebResourcePickerCancel);
            Controls.Add(btnWebResourcePickerValidate);
            Controls.Add(panel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "WebResourcePicker";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Web Resource Picker";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnWebResourcePickerCancel;
        private System.Windows.Forms.Button btnWebResourcePickerValidate;
        private System.Windows.Forms.ListView lstWebResources;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnNewResource;
        private System.Windows.Forms.Button btnRefresh;

    }
}