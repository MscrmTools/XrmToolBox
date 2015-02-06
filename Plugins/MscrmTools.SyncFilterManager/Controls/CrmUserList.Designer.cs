namespace MscrmTools.SyncFilterManager.Controls
{
    partial class CrmUserList
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
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.chkSelectUnselectAll = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lvUsers = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(4, 9);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(60, 20);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(75, 5);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(662, 26);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(748, 2);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(112, 35);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // chkSelectUnselectAll
            // 
            this.chkSelectUnselectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSelectUnselectAll.AutoSize = true;
            this.chkSelectUnselectAll.Location = new System.Drawing.Point(878, 8);
            this.chkSelectUnselectAll.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkSelectUnselectAll.Name = "chkSelectUnselectAll";
            this.chkSelectUnselectAll.Size = new System.Drawing.Size(166, 24);
            this.chkSelectUnselectAll.TabIndex = 4;
            this.chkSelectUnselectAll.Text = "Select/Unselect all";
            this.chkSelectUnselectAll.UseVisualStyleBackColor = true;
            this.chkSelectUnselectAll.CheckedChanged += new System.EventHandler(this.chkSelectUnselectAll_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblSearch);
            this.panel1.Controls.Add(this.chkSelectUnselectAll);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1048, 46);
            this.panel1.TabIndex = 5;
            // 
            // lvUsers
            // 
            this.lvUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvUsers.FullRowSelect = true;
            this.lvUsers.HideSelection = false;
            this.lvUsers.Location = new System.Drawing.Point(0, 46);
            this.lvUsers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lvUsers.Name = "lvUsers";
            this.lvUsers.Size = new System.Drawing.Size(1048, 669);
            this.lvUsers.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvUsers.TabIndex = 6;
            this.lvUsers.UseCompatibleStateImageBehavior = false;
            this.lvUsers.View = System.Windows.Forms.View.Details;
            this.lvUsers.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvUsers_ColumnClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Fullname";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Business Unit";
            this.columnHeader2.Width = 150;
            // 
            // CrmUserList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvUsers);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CrmUserList";
            this.Size = new System.Drawing.Size(1048, 715);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.CheckBox chkSelectUnselectAll;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lvUsers;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}
