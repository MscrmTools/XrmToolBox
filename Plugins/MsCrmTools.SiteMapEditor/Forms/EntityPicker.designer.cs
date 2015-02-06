namespace MsCrmTools.SiteMapEditor.Forms
{
    partial class EntityPicker
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
            lvSelectedEntities = new System.Windows.Forms.ListView();
            columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            btnValidate = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            label1 = new System.Windows.Forms.Label();
            lblHeader = new System.Windows.Forms.Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lvSelectedEntities
            // 
            lvSelectedEntities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            lvSelectedEntities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1});
            lvSelectedEntities.Enabled = false;
            lvSelectedEntities.FullRowSelect = true;
            lvSelectedEntities.Location = new System.Drawing.Point(12, 66);
            lvSelectedEntities.Name = "lvSelectedEntities";
            lvSelectedEntities.Size = new System.Drawing.Size(332, 291);
            lvSelectedEntities.Sorting = System.Windows.Forms.SortOrder.Ascending;
            lvSelectedEntities.TabIndex = 2;
            lvSelectedEntities.UseCompatibleStateImageBehavior = false;
            lvSelectedEntities.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "DisplayName";
            columnHeader1.Width = 301;
            // 
            // btnValidate
            // 
            btnValidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnValidate.Enabled = false;
            btnValidate.Location = new System.Drawing.Point(188, 363);
            btnValidate.Name = "btnValidate";
            btnValidate.Size = new System.Drawing.Size(75, 23);
            btnValidate.TabIndex = 3;
            btnValidate.Text = "OK";
            btnValidate.UseVisualStyleBackColor = true;
            btnValidate.Click += new System.EventHandler(BtnValidateClick);
            // 
            // btnCancel
            // 
            btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnCancel.Enabled = false;
            btnCancel.Location = new System.Drawing.Point(269, 363);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(75, 23);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += new System.EventHandler(BtnCancelClick);
            // 
            // panel1
            // 
            panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            panel1.BackColor = System.Drawing.Color.White;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lblHeader);
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(356, 60);
            panel1.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(4, 35);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(189, 13);
            label1.TabIndex = 2;
            label1.Text = "Select the entity for the SubArea item";
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblHeader.Location = new System.Drawing.Point(3, 9);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new System.Drawing.Size(107, 22);
            lblHeader.TabIndex = 1;
            lblHeader.Text = "Select Entity";
            // 
            // EntityPicker
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(356, 398);
            Controls.Add(panel1);
            Controls.Add(btnCancel);
            Controls.Add(btnValidate);
            Controls.Add(lvSelectedEntities);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EntityPicker";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Entity Picker";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvSelectedEntities;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblHeader;
    }
}