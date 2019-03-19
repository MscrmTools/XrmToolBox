namespace XrmToolBox.CustomControls.Dialogs
{
    partial class SolutionPickerDialog
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnValidate = new System.Windows.Forms.Button();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsllFilter = new System.Windows.Forms.ToolStripLabel();
            this.tstFilter = new System.Windows.Forms.ToolStripTextBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lvSolutions = new System.Windows.Forms.ListView();
            this.chDisplayName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPublisher = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTYpe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnlFooter.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(634, 5);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 35);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnValidate
            // 
            this.btnValidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValidate.Enabled = false;
            this.btnValidate.Location = new System.Drawing.Point(514, 5);
            this.btnValidate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(112, 35);
            this.btnValidate.TabIndex = 3;
            this.btnValidate.Text = "OK";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.btnValidate);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 389);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(750, 46);
            this.pnlFooter.TabIndex = 6;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(750, 77);
            this.pnlHeader.TabIndex = 9;
            this.pnlHeader.Visible = false;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoEllipsis = true;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(750, 77);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Please select a solution";
            // 
            // tsMain
            // 
            this.tsMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsllFilter,
            this.tstFilter});
            this.tsMain.Location = new System.Drawing.Point(0, 77);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(750, 31);
            this.tsMain.TabIndex = 11;
            this.tsMain.Text = "toolStrip1";
            // 
            // tsllFilter
            // 
            this.tsllFilter.Name = "tsllFilter";
            this.tsllFilter.Size = new System.Drawing.Size(50, 28);
            this.tsllFilter.Text = "Filter";
            // 
            // tstFilter
            // 
            this.tstFilter.Name = "tstFilter";
            this.tstFilter.Size = new System.Drawing.Size(400, 31);
            this.tstFilter.ToolTipText = "Filter by solution unique or display name";
            this.tstFilter.TextChanged += new System.EventHandler(this.tstFilter_TextChanged);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.lvSolutions);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 108);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(750, 281);
            this.pnlMain.TabIndex = 12;
            // 
            // lvSolutions
            // 
            this.lvSolutions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDisplayName,
            this.chVersion,
            this.chPublisher,
            this.chTYpe});
            this.lvSolutions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSolutions.Enabled = false;
            this.lvSolutions.FullRowSelect = true;
            this.lvSolutions.GridLines = true;
            this.lvSolutions.HideSelection = false;
            this.lvSolutions.Location = new System.Drawing.Point(0, 0);
            this.lvSolutions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lvSolutions.MultiSelect = false;
            this.lvSolutions.Name = "lvSolutions";
            this.lvSolutions.Size = new System.Drawing.Size(750, 281);
            this.lvSolutions.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvSolutions.TabIndex = 4;
            this.lvSolutions.UseCompatibleStateImageBehavior = false;
            this.lvSolutions.View = System.Windows.Forms.View.Details;
            this.lvSolutions.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstSolutions_ColumnClick);
            this.lvSolutions.DoubleClick += new System.EventHandler(this.lstSolutions_DoubleClick);
            // 
            // chDisplayName
            // 
            this.chDisplayName.Text = "Display Name";
            this.chDisplayName.Width = 250;
            // 
            // chVersion
            // 
            this.chVersion.Text = "Version";
            this.chVersion.Width = 125;
            // 
            // chPublisher
            // 
            this.chPublisher.Text = "Publisher";
            this.chPublisher.Width = 200;
            // 
            // chTYpe
            // 
            this.chTYpe.Text = "Type";
            this.chTYpe.Width = 100;
            // 
            // SolutionPickerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 435);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlFooter);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SolutionPickerDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Solution Picker";
            this.Load += new System.EventHandler(this.SolutionPicker_Load);
            this.pnlFooter.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripLabel tsllFilter;
        private System.Windows.Forms.ToolStripTextBox tstFilter;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.ListView lvSolutions;
        private System.Windows.Forms.ColumnHeader chDisplayName;
        private System.Windows.Forms.ColumnHeader chVersion;
        private System.Windows.Forms.ColumnHeader chPublisher;
        private System.Windows.Forms.ColumnHeader chTYpe;
    }
}