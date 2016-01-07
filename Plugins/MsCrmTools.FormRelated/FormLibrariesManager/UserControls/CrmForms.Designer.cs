namespace MsCrmTools.FormLibrariesManager.UserControls
{
    partial class CrmForms
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
            this.lvForms = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rightClickFormMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.manageOnLoadEventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageOnSaveEventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewLibrariesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightClickFormMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvForms
            // 
            this.lvForms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvForms.CheckBoxes = true;
            this.lvForms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader1});
            this.lvForms.FullRowSelect = true;
            this.lvForms.HideSelection = false;
            this.lvForms.Location = new System.Drawing.Point(0, 0);
            this.lvForms.Name = "lvForms";
            this.lvForms.Size = new System.Drawing.Size(459, 571);
            this.lvForms.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvForms.TabIndex = 1;
            this.lvForms.UseCompatibleStateImageBehavior = false;
            this.lvForms.View = System.Windows.Forms.View.Details;
            this.lvForms.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvForms_ColumnClick);
            this.lvForms.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvForms_MouseClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Entity";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 200;
            // 
            // rightClickFormMenu
            // 
            this.rightClickFormMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageOnLoadEventsToolStripMenuItem,
            this.manageOnSaveEventsToolStripMenuItem,
            this.viewLibrariesToolStripMenuItem});
            this.rightClickFormMenu.Name = "rightClickFormMenu";
            this.rightClickFormMenu.Size = new System.Drawing.Size(200, 92);
            // 
            // manageOnLoadEventsToolStripMenuItem
            // 
            this.manageOnLoadEventsToolStripMenuItem.Name = "manageOnLoadEventsToolStripMenuItem";
            this.manageOnLoadEventsToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.manageOnLoadEventsToolStripMenuItem.Text = "Manage OnLoad Events";
            this.manageOnLoadEventsToolStripMenuItem.Click += new System.EventHandler(this.manageOnLoadEventsToolStripMenuItem_Click);
            // 
            // manageOnSaveEventsToolStripMenuItem
            // 
            this.manageOnSaveEventsToolStripMenuItem.Name = "manageOnSaveEventsToolStripMenuItem";
            this.manageOnSaveEventsToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.manageOnSaveEventsToolStripMenuItem.Text = "Manage OnSave Events";
            this.manageOnSaveEventsToolStripMenuItem.Click += new System.EventHandler(this.manageOnSaveEventsToolStripMenuItem_Click);
            // 
            // viewLibrariesToolStripMenuItem
            // 
            this.viewLibrariesToolStripMenuItem.Name = "viewLibrariesToolStripMenuItem";
            this.viewLibrariesToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.viewLibrariesToolStripMenuItem.Text = "View Libraries";
            this.viewLibrariesToolStripMenuItem.Click += new System.EventHandler(this.viewLibrariesToolStripMenuItem_Click);
            // 
            // CrmForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvForms);
            this.Name = "CrmForms";
            this.Size = new System.Drawing.Size(462, 574);
            this.rightClickFormMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvForms;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ContextMenuStrip rightClickFormMenu;
        private System.Windows.Forms.ToolStripMenuItem manageOnLoadEventsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageOnSaveEventsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewLibrariesToolStripMenuItem;
    }
}
