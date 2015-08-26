namespace MsCrmTools.FormLibrariesManager.UserControls
{
    partial class CrmScriptsTreeView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CrmScriptsTreeView));
            this.ScriptsTreeView = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // ScriptsTreeView
            // 
            this.ScriptsTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScriptsTreeView.CheckBoxes = true;
            this.ScriptsTreeView.ImageIndex = 0;
            this.ScriptsTreeView.ImageList = this.imageList1;
            this.ScriptsTreeView.Location = new System.Drawing.Point(3, 3);
            this.ScriptsTreeView.Name = "ScriptsTreeView";
            this.ScriptsTreeView.SelectedImageIndex = 0;
            this.ScriptsTreeView.Size = new System.Drawing.Size(452, 616);
            this.ScriptsTreeView.TabIndex = 0;
            this.ScriptsTreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.ScriptsTreeView_AfterCheck);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "component.png");
            this.imageList1.Images.SetKeyName(1, "folder.png");
            this.imageList1.Images.SetKeyName(2, "html.png");
            this.imageList1.Images.SetKeyName(3, "css.png");
            this.imageList1.Images.SetKeyName(4, "script.png");
            this.imageList1.Images.SetKeyName(5, "database.png");
            this.imageList1.Images.SetKeyName(6, "picture.png");
            this.imageList1.Images.SetKeyName(7, "picture.png");
            this.imageList1.Images.SetKeyName(8, "picture.png");
            this.imageList1.Images.SetKeyName(9, "silverlight.jpg");
            this.imageList1.Images.SetKeyName(10, "xsl.png");
            this.imageList1.Images.SetKeyName(11, "updateicons_16.png");
            // 
            // CrmScriptsTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ScriptsTreeView);
            this.Name = "CrmScriptsTreeView";
            this.Size = new System.Drawing.Size(458, 622);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView ScriptsTreeView;
        private System.Windows.Forms.ImageList imageList1;
    }
}
