namespace MsCrmTools.RoleUpdater.Controls
{
    partial class PrivilegeLevelSelectionControl
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
            this.AddNoneLevel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRemove = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAddOrganizationLevel = new System.Windows.Forms.Button();
            this.btnAddParentChildLevel = new System.Windows.Forms.Button();
            this.btnAddBusinessUnitLevel = new System.Windows.Forms.Button();
            this.btnAddUserLevel = new System.Windows.Forms.Button();
            this.lvPrivileges = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.lblAlreadyAdded = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AddNoneLevel
            // 
            this.AddNoneLevel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddNoneLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.AddNoneLevel.Location = new System.Drawing.Point(269, 89);
            this.AddNoneLevel.Name = "AddNoneLevel";
            this.AddNoneLevel.Size = new System.Drawing.Size(200, 34);
            this.AddNoneLevel.TabIndex = 122;
            this.AddNoneLevel.Text = "None";
            this.AddNoneLevel.UseVisualStyleBackColor = true;
            this.AddNoneLevel.Click += new System.EventHandler(this.AddNoneLevelClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 121;
            this.label1.Text = "Search";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.textBox1.Location = new System.Drawing.Point(66, 56);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(197, 20);
            this.textBox1.TabIndex = 120;
            this.textBox1.TextChanged += new System.EventHandler(this.TextBox1TextChanged);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5});
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(475, 89);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(422, 400);
            this.listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView1.TabIndex = 119;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListView1MouseDoubleClick);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Name";
            this.columnHeader4.Width = 200;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Level";
            this.columnHeader5.Width = 200;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnRemove.Location = new System.Drawing.Point(475, 495);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(108, 34);
            this.btnRemove.TabIndex = 118;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.BtnRemoveClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(269, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 13);
            this.label4.TabIndex = 116;
            this.label4.Text = "Add privilege with level :";
            // 
            // btnAddOrganizationLevel
            // 
            this.btnAddOrganizationLevel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddOrganizationLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnAddOrganizationLevel.Location = new System.Drawing.Point(269, 257);
            this.btnAddOrganizationLevel.Name = "btnAddOrganizationLevel";
            this.btnAddOrganizationLevel.Size = new System.Drawing.Size(200, 34);
            this.btnAddOrganizationLevel.TabIndex = 115;
            this.btnAddOrganizationLevel.Text = "organization";
            this.btnAddOrganizationLevel.UseVisualStyleBackColor = true;
            this.btnAddOrganizationLevel.Click += new System.EventHandler(this.BtnAddOrganizationLevelClick);
            // 
            // btnAddParentChildLevel
            // 
            this.btnAddParentChildLevel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddParentChildLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnAddParentChildLevel.Location = new System.Drawing.Point(269, 215);
            this.btnAddParentChildLevel.Name = "btnAddParentChildLevel";
            this.btnAddParentChildLevel.Size = new System.Drawing.Size(200, 34);
            this.btnAddParentChildLevel.TabIndex = 114;
            this.btnAddParentChildLevel.Text = "Parent : child business unit";
            this.btnAddParentChildLevel.UseVisualStyleBackColor = true;
            this.btnAddParentChildLevel.Click += new System.EventHandler(this.BtnAddParentChildLevelClick);
            // 
            // btnAddBusinessUnitLevel
            // 
            this.btnAddBusinessUnitLevel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddBusinessUnitLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnAddBusinessUnitLevel.Location = new System.Drawing.Point(269, 173);
            this.btnAddBusinessUnitLevel.Name = "btnAddBusinessUnitLevel";
            this.btnAddBusinessUnitLevel.Size = new System.Drawing.Size(200, 34);
            this.btnAddBusinessUnitLevel.TabIndex = 113;
            this.btnAddBusinessUnitLevel.Text = "Business unit";
            this.btnAddBusinessUnitLevel.UseVisualStyleBackColor = true;
            this.btnAddBusinessUnitLevel.Click += new System.EventHandler(this.BtnAddBusinessUnitLevelClick);
            // 
            // btnAddUserLevel
            // 
            this.btnAddUserLevel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddUserLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnAddUserLevel.Location = new System.Drawing.Point(269, 131);
            this.btnAddUserLevel.Name = "btnAddUserLevel";
            this.btnAddUserLevel.Size = new System.Drawing.Size(200, 34);
            this.btnAddUserLevel.TabIndex = 112;
            this.btnAddUserLevel.Text = "User";
            this.btnAddUserLevel.UseVisualStyleBackColor = true;
            this.btnAddUserLevel.Click += new System.EventHandler(this.BtnAddUserLevelClick);
            // 
            // lvPrivileges
            // 
            this.lvPrivileges.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvPrivileges.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvPrivileges.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvPrivileges.FullRowSelect = true;
            this.lvPrivileges.HideSelection = false;
            this.lvPrivileges.Location = new System.Drawing.Point(4, 89);
            this.lvPrivileges.Name = "lvPrivileges";
            this.lvPrivileges.Size = new System.Drawing.Size(259, 440);
            this.lvPrivileges.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvPrivileges.TabIndex = 111;
            this.lvPrivileges.UseCompatibleStateImageBehavior = false;
            this.lvPrivileges.View = System.Windows.Forms.View.Details;
            this.lvPrivileges.SelectedIndexChanged += new System.EventHandler(this.LvPrivilegesSelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 200;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(441, 24);
            this.label3.TabIndex = 123;
            this.label3.Text = "Select privileges and levels that need to be updated";
            // 
            // lblAlreadyAdded
            // 
            this.lblAlreadyAdded.AutoSize = true;
            this.lblAlreadyAdded.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlreadyAdded.ForeColor = System.Drawing.Color.Red;
            this.lblAlreadyAdded.Location = new System.Drawing.Point(312, 311);
            this.lblAlreadyAdded.Name = "lblAlreadyAdded";
            this.lblAlreadyAdded.Size = new System.Drawing.Size(117, 13);
            this.lblAlreadyAdded.TabIndex = 124;
            this.lblAlreadyAdded.Text = "Privilege already added";
            this.lblAlreadyAdded.Visible = false;
            // 
            // PrivilegeLevelSelectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblAlreadyAdded);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.AddNoneLevel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnAddOrganizationLevel);
            this.Controls.Add(this.btnAddParentChildLevel);
            this.Controls.Add(this.btnAddBusinessUnitLevel);
            this.Controls.Add(this.btnAddUserLevel);
            this.Controls.Add(this.lvPrivileges);
            this.Name = "PrivilegeLevelSelectionControl";
            this.Size = new System.Drawing.Size(900, 550);
            this.Load += new System.EventHandler(this.PrivilegeLevelSelectionControlLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddNoneLevel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAddOrganizationLevel;
        private System.Windows.Forms.Button btnAddParentChildLevel;
        private System.Windows.Forms.Button btnAddBusinessUnitLevel;
        private System.Windows.Forms.Button btnAddUserLevel;
        private System.Windows.Forms.ListView lvPrivileges;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblAlreadyAdded;
    }
}
