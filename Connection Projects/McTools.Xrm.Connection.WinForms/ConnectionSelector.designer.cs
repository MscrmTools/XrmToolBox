namespace McTools.Xrm.Connection.WinForms
{
    partial class ConnectionSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionSelector));
            this.bValidate = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.lvConnections = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menu = new System.Windows.Forms.ToolStrip();
            this.tsbNewConnection = new System.Windows.Forms.ToolStripButton();
            this.tsbUpdateConnection = new System.Windows.Forms.ToolStripButton();
            this.tsbDeleteConnection = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_UseMru = new System.Windows.Forms.ToolStripButton();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // bValidate
            // 
            this.bValidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bValidate.Location = new System.Drawing.Point(387, 375);
            this.bValidate.Name = "bValidate";
            this.bValidate.Size = new System.Drawing.Size(75, 23);
            this.bValidate.TabIndex = 3;
            this.bValidate.Text = "OK";
            this.bValidate.UseVisualStyleBackColor = true;
            this.bValidate.Click += new System.EventHandler(this.BValidateClick);
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(468, 375);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 4;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.BCancelClick);
            // 
            // lvConnections
            // 
            this.lvConnections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvConnections.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader2,
            this.columnHeader4});
            this.lvConnections.FullRowSelect = true;
            this.lvConnections.GridLines = true;
            this.lvConnections.Location = new System.Drawing.Point(12, 28);
            this.lvConnections.Name = "lvConnections";
            this.lvConnections.Size = new System.Drawing.Size(531, 341);
            this.lvConnections.TabIndex = 2;
            this.lvConnections.UseCompatibleStateImageBehavior = false;
            this.lvConnections.View = System.Windows.Forms.View.Details;
            this.lvConnections.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LvConnectionsColumnClick);
            this.lvConnections.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvConnections_KeyDown);
            this.lvConnections.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LvConnectionsMouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 180;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Server";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Organization";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Version";
            this.columnHeader4.Width = 100;
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNewConnection,
            this.tsbUpdateConnection,
            this.tsbDeleteConnection,
            this.toolStripSeparator1,
            this.tsb_UseMru});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(555, 25);
            this.menu.TabIndex = 1;
            this.menu.Text = "toolStrip1";
            // 
            // tsbNewConnection
            // 
            this.tsbNewConnection.Image = ((System.Drawing.Image)(resources.GetObject("tsbNewConnection.Image")));
            this.tsbNewConnection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNewConnection.Name = "tsbNewConnection";
            this.tsbNewConnection.Size = new System.Drawing.Size(114, 22);
            this.tsbNewConnection.Text = "New connection";
            this.tsbNewConnection.Click += new System.EventHandler(this.tsbNewConnection_Click);
            // 
            // tsbUpdateConnection
            // 
            this.tsbUpdateConnection.Image = ((System.Drawing.Image)(resources.GetObject("tsbUpdateConnection.Image")));
            this.tsbUpdateConnection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUpdateConnection.Name = "tsbUpdateConnection";
            this.tsbUpdateConnection.Size = new System.Drawing.Size(128, 22);
            this.tsbUpdateConnection.Text = "Update connection";
            this.tsbUpdateConnection.Click += new System.EventHandler(this.tsbUpdateConnection_Click);
            // 
            // tsbDeleteConnection
            // 
            this.tsbDeleteConnection.Image = ((System.Drawing.Image)(resources.GetObject("tsbDeleteConnection.Image")));
            this.tsbDeleteConnection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeleteConnection.Name = "tsbDeleteConnection";
            this.tsbDeleteConnection.Size = new System.Drawing.Size(123, 22);
            this.tsbDeleteConnection.Text = "Delete connection";
            this.tsbDeleteConnection.Click += new System.EventHandler(this.tsbDeleteConnection_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsb_UseMru
            // 
            this.tsb_UseMru.CheckOnClick = true;
            this.tsb_UseMru.Image = ((System.Drawing.Image)(resources.GetObject("tsb_UseMru.Image")));
            this.tsb_UseMru.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_UseMru.Name = "tsb_UseMru";
            this.tsb_UseMru.Size = new System.Drawing.Size(117, 22);
            this.tsb_UseMru.Text = "Display MRU first";
            this.tsb_UseMru.ToolTipText = "Display Most Recently Used connections first";
            // 
            // ConnectionSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(555, 410);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.bValidate);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.lvConnections);
            this.Name = "ConnectionSelector";
            this.ShowIcon = false;
            this.Text = "Select connection";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConnectionSelector_KeyDown);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bValidate;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.ListView lvConnections;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ToolStrip menu;
        private System.Windows.Forms.ToolStripButton tsbNewConnection;
        private System.Windows.Forms.ToolStripButton tsbUpdateConnection;
        private System.Windows.Forms.ToolStripButton tsbDeleteConnection;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsb_UseMru;

    }
}