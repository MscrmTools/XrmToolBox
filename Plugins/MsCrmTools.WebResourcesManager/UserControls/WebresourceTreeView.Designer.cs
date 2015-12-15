namespace MsCrmTools.WebResourcesManager.New.UserControls
{
    partial class WebresourceTreeView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebresourceTreeView));
            this.tv = new System.Windows.Forms.TreeView();
            this.ilWebResourceTypes = new System.Windows.Forms.ImageList(this.components);
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tv
            // 
            this.tv.AllowDrop = true;
            this.tv.CheckBoxes = true;
            this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv.HideSelection = false;
            this.tv.ImageIndex = 0;
            this.tv.ImageList = this.ilWebResourceTypes;
            this.tv.Location = new System.Drawing.Point(0, 0);
            this.tv.Name = "tv";
            this.tv.SelectedImageIndex = 0;
            this.tv.Size = new System.Drawing.Size(409, 670);
            this.tv.TabIndex = 0;
            this.tv.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterCheck);
            this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
            this.tv.DragDrop += new System.Windows.Forms.DragEventHandler(this.tv_DragDrop);
            this.tv.DragOver += new System.Windows.Forms.DragEventHandler(this.tv_DragOver);
            this.tv.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tv_MouseDown);
            // 
            // ilWebResourceTypes
            // 
            this.ilWebResourceTypes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilWebResourceTypes.ImageStream")));
            this.ilWebResourceTypes.TransparentColor = System.Drawing.Color.Transparent;
            this.ilWebResourceTypes.Images.SetKeyName(0, "component.png");
            this.ilWebResourceTypes.Images.SetKeyName(1, "folder.png");
            this.ilWebResourceTypes.Images.SetKeyName(2, "html.png");
            this.ilWebResourceTypes.Images.SetKeyName(3, "css.png");
            this.ilWebResourceTypes.Images.SetKeyName(4, "script.png");
            this.ilWebResourceTypes.Images.SetKeyName(5, "database.png");
            this.ilWebResourceTypes.Images.SetKeyName(6, "picture.png");
            this.ilWebResourceTypes.Images.SetKeyName(7, "picture.png");
            this.ilWebResourceTypes.Images.SetKeyName(8, "picture.png");
            this.ilWebResourceTypes.Images.SetKeyName(9, "silverlight.jpg");
            this.ilWebResourceTypes.Images.SetKeyName(10, "xsl.png");
            this.ilWebResourceTypes.Images.SetKeyName(11, "updateicons_16.png");
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chkSelectAll.Location = new System.Drawing.Point(0, 653);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(409, 17);
            this.chkSelectAll.TabIndex = 83;
            this.chkSelectAll.Text = "Select/Unselect all";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.Click += new System.EventHandler(this.chkSelectAll_Click);
            // 
            // WebresourceTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkSelectAll);
            this.Controls.Add(this.tv);
            this.Name = "WebresourceTreeView";
            this.Size = new System.Drawing.Size(409, 670);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.ImageList ilWebResourceTypes;
        private System.Windows.Forms.CheckBox chkSelectAll;
    }
}
