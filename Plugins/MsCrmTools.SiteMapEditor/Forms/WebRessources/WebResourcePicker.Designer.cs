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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.btnWebResourcePickerCancel = new System.Windows.Forms.Button();
            this.btnWebResourcePickerValidate = new System.Windows.Forms.Button();
            this.lstWebResources = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnNewResource = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(500, 60);
            this.panel1.TabIndex = 2;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(3, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(181, 22);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Select a web resource";
            // 
            // btnWebResourcePickerCancel
            // 
            this.btnWebResourcePickerCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWebResourcePickerCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnWebResourcePickerCancel.Location = new System.Drawing.Point(413, 277);
            this.btnWebResourcePickerCancel.Name = "btnWebResourcePickerCancel";
            this.btnWebResourcePickerCancel.Size = new System.Drawing.Size(75, 23);
            this.btnWebResourcePickerCancel.TabIndex = 5;
            this.btnWebResourcePickerCancel.Text = "Cancel";
            this.btnWebResourcePickerCancel.UseVisualStyleBackColor = true;
            this.btnWebResourcePickerCancel.Click += new System.EventHandler(this.btnWebResourcePickerCancel_Click);
            // 
            // btnWebResourcePickerValidate
            // 
            this.btnWebResourcePickerValidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWebResourcePickerValidate.Location = new System.Drawing.Point(332, 277);
            this.btnWebResourcePickerValidate.Name = "btnWebResourcePickerValidate";
            this.btnWebResourcePickerValidate.Size = new System.Drawing.Size(75, 23);
            this.btnWebResourcePickerValidate.TabIndex = 4;
            this.btnWebResourcePickerValidate.Text = "OK";
            this.btnWebResourcePickerValidate.UseVisualStyleBackColor = true;
            this.btnWebResourcePickerValidate.Click += new System.EventHandler(this.btnWebResourcePickerValidate_Click);
            // 
            // lstWebResources
            // 
            this.lstWebResources.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstWebResources.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lstWebResources.FullRowSelect = true;
            this.lstWebResources.GridLines = true;
            this.lstWebResources.Location = new System.Drawing.Point(7, 66);
            this.lstWebResources.MultiSelect = false;
            this.lstWebResources.Name = "lstWebResources";
            this.lstWebResources.Size = new System.Drawing.Size(481, 179);
            this.lstWebResources.TabIndex = 6;
            this.lstWebResources.UseCompatibleStateImageBehavior = false;
            this.lstWebResources.View = System.Windows.Forms.View.Details;
            this.lstWebResources.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstWebResources_ColumnClick);
            this.lstWebResources.DoubleClick += new System.EventHandler(this.lstWebResources_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Display Name";
            this.columnHeader1.Width = 175;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 250;
            // 
            // btnNewResource
            // 
            this.btnNewResource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNewResource.Location = new System.Drawing.Point(7, 251);
            this.btnNewResource.Name = "btnNewResource";
            this.btnNewResource.Size = new System.Drawing.Size(100, 23);
            this.btnNewResource.TabIndex = 7;
            this.btnNewResource.Text = "New resource";
            this.btnNewResource.UseVisualStyleBackColor = true;
            this.btnNewResource.Click += new System.EventHandler(this.btnNewResource_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Enabled = false;
            this.btnRefresh.Location = new System.Drawing.Point(113, 251);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 23);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "Refresh list";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefreshClick);
            // 
            // WebResourcePicker
            // 
            this.AcceptButton = this.btnWebResourcePickerValidate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnWebResourcePickerCancel;
            this.ClientSize = new System.Drawing.Size(500, 312);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnNewResource);
            this.Controls.Add(this.lstWebResources);
            this.Controls.Add(this.btnWebResourcePickerCancel);
            this.Controls.Add(this.btnWebResourcePickerValidate);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WebResourcePicker";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Web Resource Picker";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

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