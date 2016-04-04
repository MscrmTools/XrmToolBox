namespace MsCrmTools.PrivDiscover
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainControl));
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLoadRolesAndPrivs = new System.Windows.Forms.ToolStripButton();
            this.toolImageList = new System.Windows.Forms.ImageList(this.components);
            this.gbPrivileges = new System.Windows.Forms.GroupBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lvPrivileges = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvImagesList = new System.Windows.Forms.ImageList(this.components);
            this.gbSelectedPrivileges = new System.Windows.Forms.GroupBox();
            this.btnDisplayRoles = new System.Windows.Forms.Button();
            this.lvSelectedPrivileges = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbResultingRoles = new System.Windows.Forms.GroupBox();
            this.btnOpenSecurityRole = new System.Windows.Forms.Button();
            this.lvRoles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbLevelOrg = new System.Windows.Forms.PictureBox();
            this.pbLevelSubDiv = new System.Windows.Forms.PictureBox();
            this.pbLevelDiv = new System.Windows.Forms.PictureBox();
            this.pbLevelUser = new System.Windows.Forms.PictureBox();
            this.rdbLevelOrg = new System.Windows.Forms.RadioButton();
            this.rdbLevelSubDiv = new System.Windows.Forms.RadioButton();
            this.rdbLevelDiv = new System.Windows.Forms.RadioButton();
            this.rdbLevelUser = new System.Windows.Forms.RadioButton();
            this.lblLevelAny = new System.Windows.Forms.Label();
            this.rdbLevelAny = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rdbLevelNone = new System.Windows.Forms.RadioButton();
            this.toolStripMenu.SuspendLayout();
            this.gbPrivileges.SuspendLayout();
            this.gbSelectedPrivileges.SuspendLayout();
            this.gbResultingRoles.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLevelOrg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLevelSubDiv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLevelDiv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLevelUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.toolStripSeparator1,
            this.tsbLoadRolesAndPrivs});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(850, 25);
            this.toolStripMenu.TabIndex = 2;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(102, 22);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.TsbCloseClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbLoadRolesAndPrivs
            // 
            this.tsbLoadRolesAndPrivs.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadRolesAndPrivs.Image")));
            this.tsbLoadRolesAndPrivs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadRolesAndPrivs.Name = "tsbLoadRolesAndPrivs";
            this.tsbLoadRolesAndPrivs.Size = new System.Drawing.Size(160, 22);
            this.tsbLoadRolesAndPrivs.Text = "Load Roles and Privileges";
            this.tsbLoadRolesAndPrivs.Click += new System.EventHandler(this.TsbLoadRolesAndPrivsClick);
            // 
            // toolImageList
            // 
            this.toolImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("toolImageList.ImageStream")));
            this.toolImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.toolImageList.Images.SetKeyName(0, "Icon.png");
            // 
            // gbPrivileges
            // 
            this.gbPrivileges.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbPrivileges.Controls.Add(this.lblSearch);
            this.gbPrivileges.Controls.Add(this.txtSearch);
            this.gbPrivileges.Controls.Add(this.lvPrivileges);
            this.gbPrivileges.Location = new System.Drawing.Point(3, 28);
            this.gbPrivileges.Name = "gbPrivileges";
            this.gbPrivileges.Size = new System.Drawing.Size(280, 469);
            this.gbPrivileges.TabIndex = 3;
            this.gbPrivileges.TabStop = false;
            this.gbPrivileges.Text = "Privileges";
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(6, 22);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(41, 13);
            this.lblSearch.TabIndex = 4;
            this.lblSearch.Text = "Search";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Enabled = false;
            this.txtSearch.Location = new System.Drawing.Point(53, 19);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(221, 20);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.TxtSearchTextChanged);
            // 
            // lvPrivileges
            // 
            this.lvPrivileges.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPrivileges.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.lvPrivileges.Location = new System.Drawing.Point(6, 48);
            this.lvPrivileges.Name = "lvPrivileges";
            this.lvPrivileges.Size = new System.Drawing.Size(268, 415);
            this.lvPrivileges.SmallImageList = this.lvImagesList;
            this.lvPrivileges.TabIndex = 2;
            this.lvPrivileges.UseCompatibleStateImageBehavior = false;
            this.lvPrivileges.View = System.Windows.Forms.View.Details;
            this.lvPrivileges.SelectedIndexChanged += new System.EventHandler(this.LvPrivilegesSelectedIndexChanged);
            this.lvPrivileges.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LvPrivilegesMouseDoubleClick);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Name";
            this.columnHeader3.Width = 240;
            // 
            // lvImagesList
            // 
            this.lvImagesList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("lvImagesList.ImageStream")));
            this.lvImagesList.TransparentColor = System.Drawing.Color.Transparent;
            this.lvImagesList.Images.SetKeyName(0, "ico_16_1036.gif");
            this.lvImagesList.Images.SetKeyName(1, "key.png");
            // 
            // gbSelectedPrivileges
            // 
            this.gbSelectedPrivileges.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbSelectedPrivileges.Controls.Add(this.btnDisplayRoles);
            this.gbSelectedPrivileges.Controls.Add(this.lvSelectedPrivileges);
            this.gbSelectedPrivileges.Location = new System.Drawing.Point(342, 28);
            this.gbSelectedPrivileges.Name = "gbSelectedPrivileges";
            this.gbSelectedPrivileges.Size = new System.Drawing.Size(280, 469);
            this.gbSelectedPrivileges.TabIndex = 4;
            this.gbSelectedPrivileges.TabStop = false;
            this.gbSelectedPrivileges.Text = "Selected Privileges";
            // 
            // btnDisplayRoles
            // 
            this.btnDisplayRoles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDisplayRoles.Location = new System.Drawing.Point(6, 19);
            this.btnDisplayRoles.Name = "btnDisplayRoles";
            this.btnDisplayRoles.Size = new System.Drawing.Size(268, 23);
            this.btnDisplayRoles.TabIndex = 7;
            this.btnDisplayRoles.Text = "Display roles that match the selection\r\n";
            this.btnDisplayRoles.UseVisualStyleBackColor = true;
            this.btnDisplayRoles.Click += new System.EventHandler(this.BtnDisplayRolesClick);
            // 
            // lvSelectedPrivileges
            // 
            this.lvSelectedPrivileges.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSelectedPrivileges.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader4});
            this.lvSelectedPrivileges.Location = new System.Drawing.Point(6, 48);
            this.lvSelectedPrivileges.Name = "lvSelectedPrivileges";
            this.lvSelectedPrivileges.Size = new System.Drawing.Size(268, 415);
            this.lvSelectedPrivileges.SmallImageList = this.lvImagesList;
            this.lvSelectedPrivileges.TabIndex = 1;
            this.lvSelectedPrivileges.UseCompatibleStateImageBehavior = false;
            this.lvSelectedPrivileges.View = System.Windows.Forms.View.Details;
            this.lvSelectedPrivileges.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LvSelectedPrivilegesMouseDoubleClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Depth";
            this.columnHeader4.Width = 120;
            // 
            // gbResultingRoles
            // 
            this.gbResultingRoles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbResultingRoles.Controls.Add(this.btnOpenSecurityRole);
            this.gbResultingRoles.Controls.Add(this.lvRoles);
            this.gbResultingRoles.Location = new System.Drawing.Point(628, 28);
            this.gbResultingRoles.Name = "gbResultingRoles";
            this.gbResultingRoles.Size = new System.Drawing.Size(219, 472);
            this.gbResultingRoles.TabIndex = 5;
            this.gbResultingRoles.TabStop = false;
            this.gbResultingRoles.Text = "Roles that match selection";
            // 
            // btnOpenSecurityRole
            // 
            this.btnOpenSecurityRole.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenSecurityRole.Location = new System.Drawing.Point(6, 19);
            this.btnOpenSecurityRole.Name = "btnOpenSecurityRole";
            this.btnOpenSecurityRole.Size = new System.Drawing.Size(207, 23);
            this.btnOpenSecurityRole.TabIndex = 8;
            this.btnOpenSecurityRole.Text = "Open security role";
            this.btnOpenSecurityRole.UseVisualStyleBackColor = true;
            this.btnOpenSecurityRole.Click += new System.EventHandler(this.BtnOpenSecurityRoleClick);
            // 
            // lvRoles
            // 
            this.lvRoles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvRoles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvRoles.Location = new System.Drawing.Point(6, 48);
            this.lvRoles.Name = "lvRoles";
            this.lvRoles.Size = new System.Drawing.Size(207, 418);
            this.lvRoles.SmallImageList = this.lvImagesList;
            this.lvRoles.TabIndex = 0;
            this.lvRoles.UseCompatibleStateImageBehavior = false;
            this.lvRoles.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 176;
            // 
            // btnAdd
            // 
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(290, 47);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(46, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = ">>";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAddClick);
            // 
            // btnRemove
            // 
            this.btnRemove.Enabled = false;
            this.btnRemove.Location = new System.Drawing.Point(290, 76);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(46, 23);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "<<";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.BtnRemoveClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.rdbLevelNone);
            this.panel1.Controls.Add(this.pbLevelOrg);
            this.panel1.Controls.Add(this.pbLevelSubDiv);
            this.panel1.Controls.Add(this.pbLevelDiv);
            this.panel1.Controls.Add(this.pbLevelUser);
            this.panel1.Controls.Add(this.rdbLevelOrg);
            this.panel1.Controls.Add(this.rdbLevelSubDiv);
            this.panel1.Controls.Add(this.rdbLevelDiv);
            this.panel1.Controls.Add(this.rdbLevelUser);
            this.panel1.Controls.Add(this.lblLevelAny);
            this.panel1.Controls.Add(this.rdbLevelAny);
            this.panel1.Location = new System.Drawing.Point(290, 106);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(46, 385);
            this.panel1.TabIndex = 8;
            // 
            // pbLevelOrg
            // 
            this.pbLevelOrg.Image = ((System.Drawing.Image)(resources.GetObject("pbLevelOrg.Image")));
            this.pbLevelOrg.Location = new System.Drawing.Point(24, 98);
            this.pbLevelOrg.Name = "pbLevelOrg";
            this.pbLevelOrg.Size = new System.Drawing.Size(16, 16);
            this.pbLevelOrg.TabIndex = 9;
            this.pbLevelOrg.TabStop = false;
            // 
            // pbLevelSubDiv
            // 
            this.pbLevelSubDiv.Image = ((System.Drawing.Image)(resources.GetObject("pbLevelSubDiv.Image")));
            this.pbLevelSubDiv.Location = new System.Drawing.Point(24, 79);
            this.pbLevelSubDiv.Name = "pbLevelSubDiv";
            this.pbLevelSubDiv.Size = new System.Drawing.Size(16, 16);
            this.pbLevelSubDiv.TabIndex = 8;
            this.pbLevelSubDiv.TabStop = false;
            // 
            // pbLevelDiv
            // 
            this.pbLevelDiv.Image = ((System.Drawing.Image)(resources.GetObject("pbLevelDiv.Image")));
            this.pbLevelDiv.Location = new System.Drawing.Point(24, 60);
            this.pbLevelDiv.Name = "pbLevelDiv";
            this.pbLevelDiv.Size = new System.Drawing.Size(16, 16);
            this.pbLevelDiv.TabIndex = 7;
            this.pbLevelDiv.TabStop = false;
            // 
            // pbLevelUser
            // 
            this.pbLevelUser.Image = ((System.Drawing.Image)(resources.GetObject("pbLevelUser.Image")));
            this.pbLevelUser.Location = new System.Drawing.Point(24, 41);
            this.pbLevelUser.Name = "pbLevelUser";
            this.pbLevelUser.Size = new System.Drawing.Size(16, 16);
            this.pbLevelUser.TabIndex = 6;
            this.pbLevelUser.TabStop = false;
            // 
            // rdbLevelOrg
            // 
            this.rdbLevelOrg.AutoSize = true;
            this.rdbLevelOrg.Location = new System.Drawing.Point(4, 100);
            this.rdbLevelOrg.Name = "rdbLevelOrg";
            this.rdbLevelOrg.Size = new System.Drawing.Size(14, 13);
            this.rdbLevelOrg.TabIndex = 5;
            this.rdbLevelOrg.UseVisualStyleBackColor = true;
            // 
            // rdbLevelSubDiv
            // 
            this.rdbLevelSubDiv.AutoSize = true;
            this.rdbLevelSubDiv.Location = new System.Drawing.Point(4, 81);
            this.rdbLevelSubDiv.Name = "rdbLevelSubDiv";
            this.rdbLevelSubDiv.Size = new System.Drawing.Size(14, 13);
            this.rdbLevelSubDiv.TabIndex = 4;
            this.rdbLevelSubDiv.UseVisualStyleBackColor = true;
            // 
            // rdbLevelDiv
            // 
            this.rdbLevelDiv.AutoSize = true;
            this.rdbLevelDiv.Location = new System.Drawing.Point(4, 62);
            this.rdbLevelDiv.Name = "rdbLevelDiv";
            this.rdbLevelDiv.Size = new System.Drawing.Size(14, 13);
            this.rdbLevelDiv.TabIndex = 3;
            this.rdbLevelDiv.UseVisualStyleBackColor = true;
            // 
            // rdbLevelUser
            // 
            this.rdbLevelUser.AutoSize = true;
            this.rdbLevelUser.Location = new System.Drawing.Point(4, 43);
            this.rdbLevelUser.Name = "rdbLevelUser";
            this.rdbLevelUser.Size = new System.Drawing.Size(14, 13);
            this.rdbLevelUser.TabIndex = 2;
            this.rdbLevelUser.UseVisualStyleBackColor = true;
            // 
            // lblLevelAny
            // 
            this.lblLevelAny.AutoSize = true;
            this.lblLevelAny.Location = new System.Drawing.Point(23, 24);
            this.lblLevelAny.Name = "lblLevelAny";
            this.lblLevelAny.Size = new System.Drawing.Size(25, 13);
            this.lblLevelAny.TabIndex = 1;
            this.lblLevelAny.Text = "Any";
            // 
            // rdbLevelAny
            // 
            this.rdbLevelAny.AutoSize = true;
            this.rdbLevelAny.Checked = true;
            this.rdbLevelAny.Location = new System.Drawing.Point(4, 24);
            this.rdbLevelAny.Name = "rdbLevelAny";
            this.rdbLevelAny.Size = new System.Drawing.Size(14, 13);
            this.rdbLevelAny.TabIndex = 0;
            this.rdbLevelAny.TabStop = true;
            this.rdbLevelAny.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(24, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // rdbLevelNone
            // 
            this.rdbLevelNone.AutoSize = true;
            this.rdbLevelNone.Location = new System.Drawing.Point(4, 5);
            this.rdbLevelNone.Name = "rdbLevelNone";
            this.rdbLevelNone.Size = new System.Drawing.Size(14, 13);
            this.rdbLevelNone.TabIndex = 10;
            this.rdbLevelNone.UseVisualStyleBackColor = true;
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.gbResultingRoles);
            this.Controls.Add(this.gbSelectedPrivileges);
            this.Controls.Add(this.gbPrivileges);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(850, 500);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.gbPrivileges.ResumeLayout(false);
            this.gbPrivileges.PerformLayout();
            this.gbSelectedPrivileges.ResumeLayout(false);
            this.gbResultingRoles.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLevelOrg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLevelSubDiv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLevelDiv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLevelUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ImageList toolImageList;
        private System.Windows.Forms.GroupBox gbPrivileges;
        private System.Windows.Forms.GroupBox gbSelectedPrivileges;
        private System.Windows.Forms.GroupBox gbResultingRoles;
        private System.Windows.Forms.ListView lvRoles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView lvPrivileges;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ListView lvSelectedPrivileges;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbLoadRolesAndPrivs;
        private System.Windows.Forms.Button btnDisplayRoles;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ImageList lvImagesList;
        private System.Windows.Forms.Button btnOpenSecurityRole;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbLevelOrg;
        private System.Windows.Forms.PictureBox pbLevelSubDiv;
        private System.Windows.Forms.PictureBox pbLevelDiv;
        private System.Windows.Forms.PictureBox pbLevelUser;
        private System.Windows.Forms.RadioButton rdbLevelOrg;
        private System.Windows.Forms.RadioButton rdbLevelSubDiv;
        private System.Windows.Forms.RadioButton rdbLevelDiv;
        private System.Windows.Forms.RadioButton rdbLevelUser;
        private System.Windows.Forms.Label lblLevelAny;
        private System.Windows.Forms.RadioButton rdbLevelAny;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton rdbLevelNone;
    }
}
