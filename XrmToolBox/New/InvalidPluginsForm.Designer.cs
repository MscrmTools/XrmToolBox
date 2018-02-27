namespace XrmToolBox.New
{
    partial class InvalidPluginsForm
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.gbDescription = new System.Windows.Forms.GroupBox();
            this.txtError = new System.Windows.Forms.TextBox();
            this.pnlShortcut = new System.Windows.Forms.Panel();
            this.lvPlugins = new System.Windows.Forms.ListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.llShortcut = new System.Windows.Forms.LinkLabel();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.gbDescription.SuspendLayout();
            this.pnlShortcut.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.label1);
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(753, 100);
            this.pnlHeader.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 10F);
            this.label1.Location = new System.Drawing.Point(21, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(433, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "These plugins failed to load during plugins analysis";
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI Light", 16F);
            this.lblHeader.Location = new System.Drawing.Point(13, 13);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(271, 45);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Plugins not loaded";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 100);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.lvPlugins);
            this.splitContainer.Panel1.Controls.Add(this.pnlShortcut);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.gbDescription);
            this.splitContainer.Size = new System.Drawing.Size(753, 557);
            this.splitContainer.SplitterDistance = 251;
            this.splitContainer.TabIndex = 1;
            // 
            // gbDescription
            // 
            this.gbDescription.Controls.Add(this.txtError);
            this.gbDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDescription.Location = new System.Drawing.Point(0, 0);
            this.gbDescription.Name = "gbDescription";
            this.gbDescription.Size = new System.Drawing.Size(753, 302);
            this.gbDescription.TabIndex = 0;
            this.gbDescription.TabStop = false;
            this.gbDescription.Text = "Error";
            // 
            // txtError
            // 
            this.txtError.BackColor = System.Drawing.Color.White;
            this.txtError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtError.Location = new System.Drawing.Point(3, 22);
            this.txtError.Multiline = true;
            this.txtError.Name = "txtError";
            this.txtError.ReadOnly = true;
            this.txtError.Size = new System.Drawing.Size(747, 277);
            this.txtError.TabIndex = 0;
            // 
            // pnlShortcut
            // 
            this.pnlShortcut.Controls.Add(this.llShortcut);
            this.pnlShortcut.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlShortcut.Location = new System.Drawing.Point(0, 219);
            this.pnlShortcut.Name = "pnlShortcut";
            this.pnlShortcut.Size = new System.Drawing.Size(753, 32);
            this.pnlShortcut.TabIndex = 1;
            // 
            // lvPlugins
            // 
            this.lvPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName});
            this.lvPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPlugins.FullRowSelect = true;
            this.lvPlugins.HoverSelection = true;
            this.lvPlugins.Location = new System.Drawing.Point(0, 0);
            this.lvPlugins.MultiSelect = false;
            this.lvPlugins.Name = "lvPlugins";
            this.lvPlugins.Size = new System.Drawing.Size(753, 219);
            this.lvPlugins.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvPlugins.TabIndex = 2;
            this.lvPlugins.UseCompatibleStateImageBehavior = false;
            this.lvPlugins.View = System.Windows.Forms.View.Details;
            this.lvPlugins.SelectedIndexChanged += new System.EventHandler(this.lvPlugins_SelectedIndexChanged);
            // 
            // chName
            // 
            this.chName.Text = "Plugin";
            this.chName.Width = 400;
            // 
            // llShortcut
            // 
            this.llShortcut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.llShortcut.Location = new System.Drawing.Point(0, 0);
            this.llShortcut.Name = "llShortcut";
            this.llShortcut.Size = new System.Drawing.Size(753, 32);
            this.llShortcut.TabIndex = 0;
            this.llShortcut.TabStop = true;
            this.llShortcut.Text = "Click here to open Plugins folder so you can delete faulting plugins";
            this.llShortcut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.llShortcut.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShortcut_LinkClicked);
            // 
            // InvalidPluginsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(753, 657);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.pnlHeader);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InvalidPluginsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.InvalidPluginsForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.gbDescription.ResumeLayout(false);
            this.gbDescription.PerformLayout();
            this.pnlShortcut.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.GroupBox gbDescription;
        private System.Windows.Forms.TextBox txtError;
        private System.Windows.Forms.ListView lvPlugins;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.Panel pnlShortcut;
        private System.Windows.Forms.LinkLabel llShortcut;
    }
}