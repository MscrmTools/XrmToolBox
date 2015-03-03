namespace MsCrmTools.FlsBulkUpdater
{
    partial class MainControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainControl));
            this.MainToolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.TsbLoadFls = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbUpdate = new System.Windows.Forms.ToolStripButton();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.lvFlsRoles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.llSelectAllProfiles = new System.Windows.Forms.LinkLabel();
            this.LvSecuredAttributes = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel2 = new System.Windows.Forms.Panel();
            this.llSecureAttrCheckAll = new System.Windows.Forms.LinkLabel();
            this.CbbUpdate = new System.Windows.Forms.ComboBox();
            this.LblUpdate = new System.Windows.Forms.Label();
            this.CbbCreate = new System.Windows.Forms.ComboBox();
            this.LblCreate = new System.Windows.Forms.Label();
            this.CbbRead = new System.Windows.Forms.ComboBox();
            this.LblRead = new System.Windows.Forms.Label();
            this.MainToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainToolStrip
            // 
            this.MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.toolStripSeparator2,
            this.TsbLoadFls,
            this.toolStripSeparator1,
            this.tsbUpdate});
            this.MainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.MainToolStrip.Name = "MainToolStrip";
            this.MainToolStrip.Size = new System.Drawing.Size(1086, 32);
            this.MainToolStrip.TabIndex = 0;
            this.MainToolStrip.Text = "tsMain";
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(28, 29);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 32);
            // 
            // TsbLoadFls
            // 
            this.TsbLoadFls.Image = ((System.Drawing.Image)(resources.GetObject("TsbLoadFls.Image")));
            this.TsbLoadFls.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbLoadFls.Name = "TsbLoadFls";
            this.TsbLoadFls.Size = new System.Drawing.Size(111, 29);
            this.TsbLoadFls.Text = "Load FLS";
            this.TsbLoadFls.Click += new System.EventHandler(this.TsbLoadFls_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbUpdate
            // 
            this.tsbUpdate.Image = ((System.Drawing.Image)(resources.GetObject("tsbUpdate.Image")));
            this.tsbUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUpdate.Name = "tsbUpdate";
            this.tsbUpdate.Size = new System.Drawing.Size(130, 29);
            this.tsbUpdate.Text = "Update FLS";
            this.tsbUpdate.ToolTipText = "Updates checked Secure field(s) for checked Field Security profile(s)";
            this.tsbUpdate.Click += new System.EventHandler(this.tsbUpdate_Click);
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 32);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.lvFlsRoles);
            this.scMain.Panel1.Controls.Add(this.panel1);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.LvSecuredAttributes);
            this.scMain.Panel2.Controls.Add(this.panel2);
            this.scMain.Size = new System.Drawing.Size(1086, 595);
            this.scMain.SplitterDistance = 252;
            this.scMain.TabIndex = 1;
            // 
            // lvFlsRoles
            // 
            this.lvFlsRoles.CheckBoxes = true;
            this.lvFlsRoles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvFlsRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFlsRoles.FullRowSelect = true;
            this.lvFlsRoles.HideSelection = false;
            this.lvFlsRoles.Location = new System.Drawing.Point(0, 40);
            this.lvFlsRoles.MultiSelect = false;
            this.lvFlsRoles.Name = "lvFlsRoles";
            this.lvFlsRoles.Size = new System.Drawing.Size(252, 555);
            this.lvFlsRoles.TabIndex = 2;
            this.lvFlsRoles.UseCompatibleStateImageBehavior = false;
            this.lvFlsRoles.View = System.Windows.Forms.View.Details;
            this.lvFlsRoles.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView_ColumnClick);
            this.lvFlsRoles.SelectedIndexChanged += new System.EventHandler(this.lvFlsRoles_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 180;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.llSelectAllProfiles);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(252, 40);
            this.panel1.TabIndex = 1;
            // 
            // llSelectAllProfiles
            // 
            this.llSelectAllProfiles.AutoSize = true;
            this.llSelectAllProfiles.Location = new System.Drawing.Point(3, 10);
            this.llSelectAllProfiles.Name = "llSelectAllProfiles";
            this.llSelectAllProfiles.Size = new System.Drawing.Size(73, 20);
            this.llSelectAllProfiles.TabIndex = 7;
            this.llSelectAllProfiles.TabStop = true;
            this.llSelectAllProfiles.Text = "Select all";
            this.llSelectAllProfiles.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSecureAttrCheckAll_LinkClicked);
            // 
            // LvSecuredAttributes
            // 
            this.LvSecuredAttributes.CheckBoxes = true;
            this.LvSecuredAttributes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.LvSecuredAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LvSecuredAttributes.FullRowSelect = true;
            this.LvSecuredAttributes.HideSelection = false;
            this.LvSecuredAttributes.Location = new System.Drawing.Point(0, 40);
            this.LvSecuredAttributes.Name = "LvSecuredAttributes";
            this.LvSecuredAttributes.Size = new System.Drawing.Size(830, 555);
            this.LvSecuredAttributes.TabIndex = 3;
            this.LvSecuredAttributes.UseCompatibleStateImageBehavior = false;
            this.LvSecuredAttributes.View = System.Windows.Forms.View.Details;
            this.LvSecuredAttributes.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView_ColumnClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Attribute";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Attribute Logical Name";
            this.columnHeader4.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Entity";
            this.columnHeader3.Width = 170;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Entity Logical Name";
            this.columnHeader5.Width = 170;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Read";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Create";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Update";
            this.columnHeader8.Width = 70;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.llSecureAttrCheckAll);
            this.panel2.Controls.Add(this.CbbUpdate);
            this.panel2.Controls.Add(this.LblUpdate);
            this.panel2.Controls.Add(this.CbbCreate);
            this.panel2.Controls.Add(this.LblCreate);
            this.panel2.Controls.Add(this.CbbRead);
            this.panel2.Controls.Add(this.LblRead);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(830, 40);
            this.panel2.TabIndex = 2;
            // 
            // llSecureAttrCheckAll
            // 
            this.llSecureAttrCheckAll.AutoSize = true;
            this.llSecureAttrCheckAll.Location = new System.Drawing.Point(2, 9);
            this.llSecureAttrCheckAll.Name = "llSecureAttrCheckAll";
            this.llSecureAttrCheckAll.Size = new System.Drawing.Size(73, 20);
            this.llSecureAttrCheckAll.TabIndex = 6;
            this.llSecureAttrCheckAll.TabStop = true;
            this.llSecureAttrCheckAll.Text = "Select all";
            this.llSecureAttrCheckAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSecureAttrCheckAll_LinkClicked);
            // 
            // CbbUpdate
            // 
            this.CbbUpdate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbbUpdate.FormattingEnabled = true;
            this.CbbUpdate.Items.AddRange(new object[] {
            "Don\'t change",
            "No",
            "Yes"});
            this.CbbUpdate.Location = new System.Drawing.Point(667, 6);
            this.CbbUpdate.Name = "CbbUpdate";
            this.CbbUpdate.Size = new System.Drawing.Size(160, 28);
            this.CbbUpdate.TabIndex = 5;
            // 
            // LblUpdate
            // 
            this.LblUpdate.AutoSize = true;
            this.LblUpdate.Location = new System.Drawing.Point(599, 10);
            this.LblUpdate.Name = "LblUpdate";
            this.LblUpdate.Size = new System.Drawing.Size(62, 20);
            this.LblUpdate.TabIndex = 4;
            this.LblUpdate.Text = "Update";
            // 
            // CbbCreate
            // 
            this.CbbCreate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbbCreate.FormattingEnabled = true;
            this.CbbCreate.Items.AddRange(new object[] {
            "Don\'t change",
            "No",
            "Yes"});
            this.CbbCreate.Location = new System.Drawing.Point(403, 6);
            this.CbbCreate.Name = "CbbCreate";
            this.CbbCreate.Size = new System.Drawing.Size(160, 28);
            this.CbbCreate.TabIndex = 3;
            // 
            // LblCreate
            // 
            this.LblCreate.AutoSize = true;
            this.LblCreate.Location = new System.Drawing.Point(340, 10);
            this.LblCreate.Name = "LblCreate";
            this.LblCreate.Size = new System.Drawing.Size(57, 20);
            this.LblCreate.TabIndex = 2;
            this.LblCreate.Text = "Create";
            // 
            // CbbRead
            // 
            this.CbbRead.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbbRead.FormattingEnabled = true;
            this.CbbRead.Items.AddRange(new object[] {
            "Don\'t change",
            "No",
            "Yes"});
            this.CbbRead.Location = new System.Drawing.Point(162, 6);
            this.CbbRead.Name = "CbbRead";
            this.CbbRead.Size = new System.Drawing.Size(160, 28);
            this.CbbRead.TabIndex = 1;
            // 
            // LblRead
            // 
            this.LblRead.AutoSize = true;
            this.LblRead.Location = new System.Drawing.Point(108, 10);
            this.LblRead.Name = "LblRead";
            this.LblRead.Size = new System.Drawing.Size(48, 20);
            this.LblRead.TabIndex = 0;
            this.LblRead.Text = "Read";
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scMain);
            this.Controls.Add(this.MainToolStrip);
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(1086, 627);
            this.MainToolStrip.ResumeLayout(false);
            this.MainToolStrip.PerformLayout();
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip MainToolStrip;
        private System.Windows.Forms.ToolStripButton TsbLoadFls;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.ListView lvFlsRoles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView LvSecuredAttributes;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox CbbUpdate;
        private System.Windows.Forms.Label LblUpdate;
        private System.Windows.Forms.ComboBox CbbCreate;
        private System.Windows.Forms.Label LblCreate;
        private System.Windows.Forms.ComboBox CbbRead;
        private System.Windows.Forms.Label LblRead;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.LinkLabel llSecureAttrCheckAll;
        private System.Windows.Forms.ToolStripButton tsbUpdate;
        private System.Windows.Forms.LinkLabel llSelectAllProfiles;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
    }
}
