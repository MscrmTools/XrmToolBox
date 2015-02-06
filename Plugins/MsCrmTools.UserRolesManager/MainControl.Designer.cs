namespace MsCrmTools.UserRolesManager
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLoadCrmItems = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsddbRoleActions = new System.Windows.Forms.ToolStripDropDownButton();
            this.action1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.action2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.action3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.roleSelector1 = new MsCrmTools.UserRolesManager.UserControls.RoleSelector();
            this.principalSelector1 = new MsCrmTools.UserRolesManager.UserControls.PrincipalSelector();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.toolStripSeparator1,
            this.tsbLoadCrmItems,
            this.toolStripSeparator2,
            this.tsddbRoleActions});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1071, 32);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(23, 29);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.TsbCloseClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbLoadCrmItems
            // 
            this.tsbLoadCrmItems.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadCrmItems.Image")));
            this.tsbLoadCrmItems.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadCrmItems.Name = "tsbLoadCrmItems";
            this.tsbLoadCrmItems.Size = new System.Drawing.Size(262, 29);
            this.tsbLoadCrmItems.Text = "Load Roles, Users and Teams";
            this.tsbLoadCrmItems.Click += new System.EventHandler(this.tsbLoadCrmItems_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 32);
            // 
            // tsddbRoleActions
            // 
            this.tsddbRoleActions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.action1ToolStripMenuItem,
            this.action2ToolStripMenuItem,
            this.action3ToolStripMenuItem});
            this.tsddbRoleActions.Image = ((System.Drawing.Image)(resources.GetObject("tsddbRoleActions.Image")));
            this.tsddbRoleActions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbRoleActions.Name = "tsddbRoleActions";
            this.tsddbRoleActions.Size = new System.Drawing.Size(100, 29);
            this.tsddbRoleActions.Text = "Actions";
            // 
            // action1ToolStripMenuItem
            // 
            this.action1ToolStripMenuItem.Name = "action1ToolStripMenuItem";
            this.action1ToolStripMenuItem.Size = new System.Drawing.Size(661, 30);
            this.action1ToolStripMenuItem.Text = "Add selected roles to selected users or teams";
            this.action1ToolStripMenuItem.Click += new System.EventHandler(this.action1ToolStripMenuItem_Click);
            // 
            // action2ToolStripMenuItem
            // 
            this.action2ToolStripMenuItem.Name = "action2ToolStripMenuItem";
            this.action2ToolStripMenuItem.Size = new System.Drawing.Size(661, 30);
            this.action2ToolStripMenuItem.Text = "Remove selected roles from selected users or teams";
            this.action2ToolStripMenuItem.Click += new System.EventHandler(this.action2ToolStripMenuItem_Click);
            // 
            // action3ToolStripMenuItem
            // 
            this.action3ToolStripMenuItem.Name = "action3ToolStripMenuItem";
            this.action3ToolStripMenuItem.Size = new System.Drawing.Size(661, 30);
            this.action3ToolStripMenuItem.Text = "Remove exisiting roles then add selected roles to selected users or teams";
            this.action3ToolStripMenuItem.Click += new System.EventHandler(this.action3ToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 32);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.roleSelector1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.principalSelector1);
            this.splitContainer1.Size = new System.Drawing.Size(1071, 702);
            this.splitContainer1.SplitterDistance = 510;
            this.splitContainer1.TabIndex = 2;
            // 
            // roleSelector1
            // 
            this.roleSelector1.AllRoles = null;
            this.roleSelector1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roleSelector1.Location = new System.Drawing.Point(0, 0);
            this.roleSelector1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.roleSelector1.Name = "roleSelector1";
            this.roleSelector1.Size = new System.Drawing.Size(510, 702);
            this.roleSelector1.TabIndex = 0;
            // 
            // principalSelector1
            // 
            this.principalSelector1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.principalSelector1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.principalSelector1.Location = new System.Drawing.Point(0, 0);
            this.principalSelector1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.principalSelector1.Name = "principalSelector1";
            this.principalSelector1.Size = new System.Drawing.Size(557, 702);
            this.principalSelector1.TabIndex = 1;
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(1071, 734);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbLoadCrmItems;
        private UserControls.PrincipalSelector principalSelector1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private UserControls.RoleSelector roleSelector1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton tsddbRoleActions;
        private System.Windows.Forms.ToolStripMenuItem action1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem action2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem action3ToolStripMenuItem;
    }
}
