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
            this.ilWebResourceTypes = new System.Windows.Forms.ImageList(this.components);
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.llCollapseAll = new System.Windows.Forms.LinkLabel();
            this.lblSeparator = new System.Windows.Forms.Label();
            this.llExpandAll = new System.Windows.Forms.LinkLabel();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.tv = new System.Windows.Forms.TreeView();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
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
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.llCollapseAll);
            this.pnlBottom.Controls.Add(this.lblSeparator);
            this.pnlBottom.Controls.Add(this.llExpandAll);
            this.pnlBottom.Controls.Add(this.chkSelectAll);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 653);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(409, 17);
            this.pnlBottom.TabIndex = 85;
            // 
            // llCollapseAll
            // 
            this.llCollapseAll.AutoSize = true;
            this.llCollapseAll.Dock = System.Windows.Forms.DockStyle.Right;
            this.llCollapseAll.Location = new System.Drawing.Point(284, 0);
            this.llCollapseAll.Name = "llCollapseAll";
            this.llCollapseAll.Size = new System.Drawing.Size(60, 13);
            this.llCollapseAll.TabIndex = 87;
            this.llCollapseAll.TabStop = true;
            this.llCollapseAll.Text = "Collapse all";
            this.llCollapseAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llCollapseAll_LinkClicked);
            // 
            // lblSeparator
            // 
            this.lblSeparator.AutoSize = true;
            this.lblSeparator.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblSeparator.Location = new System.Drawing.Point(344, 0);
            this.lblSeparator.Name = "lblSeparator";
            this.lblSeparator.Size = new System.Drawing.Size(9, 13);
            this.lblSeparator.TabIndex = 86;
            this.lblSeparator.Text = "|";
            // 
            // llExpandAll
            // 
            this.llExpandAll.AutoSize = true;
            this.llExpandAll.Dock = System.Windows.Forms.DockStyle.Right;
            this.llExpandAll.Location = new System.Drawing.Point(353, 0);
            this.llExpandAll.Name = "llExpandAll";
            this.llExpandAll.Size = new System.Drawing.Size(56, 13);
            this.llExpandAll.TabIndex = 85;
            this.llExpandAll.TabStop = true;
            this.llExpandAll.Text = "Expand all";
            this.llExpandAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llExpandAll_LinkClicked);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkSelectAll.Location = new System.Drawing.Point(0, 0);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(116, 17);
            this.chkSelectAll.TabIndex = 84;
            this.chkSelectAll.Text = "Select/Unselect all";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.Click += new System.EventHandler(this.chkSelectAll_Click);
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
            this.tv.Size = new System.Drawing.Size(409, 653);
            this.tv.TabIndex = 86;
            this.tv.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterCheck);
            this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
            this.tv.DragDrop += new System.Windows.Forms.DragEventHandler(this.tv_DragDrop);
            this.tv.DragOver += new System.Windows.Forms.DragEventHandler(this.tv_DragOver);
            this.tv.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tv_MouseDown);
            // 
            // WebresourceTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tv);
            this.Controls.Add(this.pnlBottom);
            this.Name = "WebresourceTreeView";
            this.Size = new System.Drawing.Size(409, 670);
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList ilWebResourceTypes;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.LinkLabel llCollapseAll;
        private System.Windows.Forms.Label lblSeparator;
        private System.Windows.Forms.LinkLabel llExpandAll;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.TreeView tv;
    }
}
