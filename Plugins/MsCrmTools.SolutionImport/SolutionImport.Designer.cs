namespace MsCrmTools.SolutionImport
{
    partial class SolutionImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolutionImport));
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.tsbCloseThisTab = new System.Windows.Forms.ToolStripButton();
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.chkDownload = new System.Windows.Forms.CheckBox();
            this.chkConvertToManaged = new System.Windows.Forms.CheckBox();
            this.chkOverwriteUnmanagedCustomizations = new System.Windows.Forms.CheckBox();
            this.chkActivate = new System.Windows.Forms.CheckBox();
            this.chkPublish = new System.Windows.Forms.CheckBox();
            this.gbImportByFolder = new System.Windows.Forms.GroupBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.lblFolderPath = new System.Windows.Forms.Label();
            this.gbImportSolution = new System.Windows.Forms.GroupBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStripMain.SuspendLayout();
            this.gbSettings.SuspendLayout();
            this.gbImportByFolder.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMain
            // 
            this.toolStripMain.AutoSize = false;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCloseThisTab});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(900, 25);
            this.toolStripMain.TabIndex = 80;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // tsbCloseThisTab
            // 
            this.tsbCloseThisTab.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCloseThisTab.Image = ((System.Drawing.Image)(resources.GetObject("tsbCloseThisTab.Image")));
            this.tsbCloseThisTab.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCloseThisTab.Name = "tsbCloseThisTab";
            this.tsbCloseThisTab.Size = new System.Drawing.Size(23, 22);
            this.tsbCloseThisTab.Text = "Close this tab";
            this.tsbCloseThisTab.Click += new System.EventHandler(this.TsbCloseThisTabClick);
            // 
            // gbSettings
            // 
            this.gbSettings.Controls.Add(this.chkDownload);
            this.gbSettings.Controls.Add(this.chkConvertToManaged);
            this.gbSettings.Controls.Add(this.chkOverwriteUnmanagedCustomizations);
            this.gbSettings.Controls.Add(this.chkActivate);
            this.gbSettings.Controls.Add(this.chkPublish);
            this.gbSettings.Location = new System.Drawing.Point(3, 28);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Size = new System.Drawing.Size(308, 135);
            this.gbSettings.TabIndex = 79;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Settings";
            // 
            // chkDownload
            // 
            this.chkDownload.AutoSize = true;
            this.chkDownload.Checked = true;
            this.chkDownload.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDownload.Location = new System.Drawing.Point(9, 110);
            this.chkDownload.Name = "chkDownload";
            this.chkDownload.Size = new System.Drawing.Size(193, 17);
            this.chkDownload.TabIndex = 77;
            this.chkDownload.Text = "Download the import log file if exists";
            this.chkDownload.UseVisualStyleBackColor = true;
            // 
            // chkConvertToManaged
            // 
            this.chkConvertToManaged.AutoSize = true;
            this.chkConvertToManaged.Location = new System.Drawing.Point(9, 65);
            this.chkConvertToManaged.Name = "chkConvertToManaged";
            this.chkConvertToManaged.Size = new System.Drawing.Size(168, 17);
            this.chkConvertToManaged.TabIndex = 76;
            this.chkConvertToManaged.Text = "Convert To Managed Solution";
            this.chkConvertToManaged.UseVisualStyleBackColor = true;
            // 
            // chkOverwriteUnmanagedCustomizations
            // 
            this.chkOverwriteUnmanagedCustomizations.AutoSize = true;
            this.chkOverwriteUnmanagedCustomizations.Location = new System.Drawing.Point(9, 42);
            this.chkOverwriteUnmanagedCustomizations.Name = "chkOverwriteUnmanagedCustomizations";
            this.chkOverwriteUnmanagedCustomizations.Size = new System.Drawing.Size(205, 17);
            this.chkOverwriteUnmanagedCustomizations.TabIndex = 75;
            this.chkOverwriteUnmanagedCustomizations.Text = "Overwrite Unmanaged Customizations\r\n";
            this.chkOverwriteUnmanagedCustomizations.UseVisualStyleBackColor = true;
            // 
            // chkActivate
            // 
            this.chkActivate.AutoSize = true;
            this.chkActivate.Checked = true;
            this.chkActivate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActivate.Location = new System.Drawing.Point(9, 19);
            this.chkActivate.Name = "chkActivate";
            this.chkActivate.Size = new System.Drawing.Size(200, 17);
            this.chkActivate.TabIndex = 74;
            this.chkActivate.Text = "Activate plugins steps and workflows";
            this.chkActivate.UseVisualStyleBackColor = true;
            // 
            // chkPublish
            // 
            this.chkPublish.AutoSize = true;
            this.chkPublish.Checked = true;
            this.chkPublish.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPublish.Location = new System.Drawing.Point(9, 87);
            this.chkPublish.Name = "chkPublish";
            this.chkPublish.Size = new System.Drawing.Size(209, 17);
            this.chkPublish.TabIndex = 73;
            this.chkPublish.Text = "Publish solution after having imported it";
            this.chkPublish.UseVisualStyleBackColor = true;
            // 
            // gbImportByFolder
            // 
            this.gbImportByFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbImportByFolder.Controls.Add(this.btnImport);
            this.gbImportByFolder.Controls.Add(this.btnBrowseFolder);
            this.gbImportByFolder.Controls.Add(this.txtFolderPath);
            this.gbImportByFolder.Controls.Add(this.lblFolderPath);
            this.gbImportByFolder.Location = new System.Drawing.Point(3, 169);
            this.gbImportByFolder.Name = "gbImportByFolder";
            this.gbImportByFolder.Size = new System.Drawing.Size(894, 100);
            this.gbImportByFolder.TabIndex = 78;
            this.gbImportByFolder.TabStop = false;
            this.gbImportByFolder.Text = "Import solution from a folder (The folder must be named with the solution archive" +
    " name)";
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Location = new System.Drawing.Point(9, 51);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(879, 43);
            this.btnImport.TabIndex = 3;
            this.btnImport.Text = "Import content of the selected folder as a solution";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.BtnImportClick);
            // 
            // btnBrowseFolder
            // 
            this.btnBrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseFolder.Location = new System.Drawing.Point(858, 18);
            this.btnBrowseFolder.Name = "btnBrowseFolder";
            this.btnBrowseFolder.Size = new System.Drawing.Size(30, 23);
            this.btnBrowseFolder.TabIndex = 2;
            this.btnBrowseFolder.Text = "...";
            this.btnBrowseFolder.UseVisualStyleBackColor = true;
            this.btnBrowseFolder.Click += new System.EventHandler(this.BtnBrowseFolderClick);
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolderPath.Location = new System.Drawing.Point(72, 20);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.Size = new System.Drawing.Size(780, 20);
            this.txtFolderPath.TabIndex = 1;
            // 
            // lblFolderPath
            // 
            this.lblFolderPath.AutoSize = true;
            this.lblFolderPath.Location = new System.Drawing.Point(6, 23);
            this.lblFolderPath.Name = "lblFolderPath";
            this.lblFolderPath.Size = new System.Drawing.Size(60, 13);
            this.lblFolderPath.TabIndex = 0;
            this.lblFolderPath.Text = "Folder path";
            // 
            // gbImportSolution
            // 
            this.gbImportSolution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbImportSolution.Location = new System.Drawing.Point(317, 28);
            this.gbImportSolution.Name = "gbImportSolution";
            this.gbImportSolution.Size = new System.Drawing.Size(580, 135);
            this.gbImportSolution.TabIndex = 77;
            this.gbImportSolution.TabStop = false;
            this.gbImportSolution.Text = "Drop a solution archive here to import it";
            this.gbImportSolution.DragDrop += new System.Windows.Forms.DragEventHandler(this.GbImportSolutionDragDrop);
            this.gbImportSolution.DragEnter += new System.Windows.Forms.DragEventHandler(this.GbImportSolutionDragEnter);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Icon.png");
            // 
            // SolutionImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.gbSettings);
            this.Controls.Add(this.gbImportByFolder);
            this.Controls.Add(this.gbImportSolution);
            this.Name = "SolutionImport";
            this.Size = new System.Drawing.Size(900, 600);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.gbSettings.ResumeLayout(false);
            this.gbSettings.PerformLayout();
            this.gbImportByFolder.ResumeLayout(false);
            this.gbImportByFolder.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.GroupBox gbSettings;
        private System.Windows.Forms.CheckBox chkDownload;
        private System.Windows.Forms.CheckBox chkConvertToManaged;
        private System.Windows.Forms.CheckBox chkOverwriteUnmanagedCustomizations;
        private System.Windows.Forms.CheckBox chkActivate;
        private System.Windows.Forms.CheckBox chkPublish;
        private System.Windows.Forms.GroupBox gbImportByFolder;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnBrowseFolder;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Label lblFolderPath;
        private System.Windows.Forms.GroupBox gbImportSolution;
        private System.Windows.Forms.ToolStripButton tsbCloseThisTab;
        private System.Windows.Forms.ImageList imageList1;
    }
}
