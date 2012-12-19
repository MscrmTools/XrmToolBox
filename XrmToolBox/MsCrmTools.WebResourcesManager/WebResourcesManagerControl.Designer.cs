namespace MsCrmTools.WebResourcesManager
{
    partial class WebResourcesManagerControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebResourcesManagerControl));
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.tvWebResources = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lblResourceName = new System.Windows.Forms.Label();
            this.panelControl = new System.Windows.Forms.Panel();
            this.toolStripScriptContent = new System.Windows.Forms.ToolStrip();
            this.tsbSaveContent = new System.Windows.Forms.ToolStripButton();
            this.tsbUpload = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPublish = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparatorMinifyJS = new System.Windows.Forms.ToolStripSeparator();
            this.tsbMinifyJS = new System.Windows.Forms.ToolStripButton();
            this.tsbPreviewHtml = new System.Windows.Forms.ToolStripButton();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbCloseThisTab = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsddCrmMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.loadWebResourcesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateCheckedWebResourcesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateAndPublishCheckedWebResourcesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updatePublishAndAddToSolutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsddFileMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.loadWebResourcesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCheckedWebResourcesToDiskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllWebResourcesToDiskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbNewRoot = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStripTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewWebResourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewEmptyWebResourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cSSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xSLTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToCRMServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAndPublishToCRMServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePublishAndAddToSolutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStripScriptContent.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.contextMenuStripTreeView.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "Icon.png");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(3, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chkSelectAll);
            this.splitContainer1.Panel1.Controls.Add(this.tvWebResources);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblResourceName);
            this.splitContainer1.Panel2.Controls.Add(this.panelControl);
            this.splitContainer1.Panel2.Controls.Add(this.toolStripScriptContent);
            this.splitContainer1.Size = new System.Drawing.Size(894, 569);
            this.splitContainer1.SplitterDistance = 344;
            this.splitContainer1.TabIndex = 89;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(3, 549);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(116, 17);
            this.chkSelectAll.TabIndex = 82;
            this.chkSelectAll.Text = "Select/Unselect all";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.Click += new System.EventHandler(this.ChkSelectAllCheckedChanged);
            // 
            // tvWebResources
            // 
            this.tvWebResources.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvWebResources.CheckBoxes = true;
            this.tvWebResources.HideSelection = false;
            this.tvWebResources.ImageIndex = 0;
            this.tvWebResources.ImageList = this.imageList1;
            this.tvWebResources.Location = new System.Drawing.Point(3, 3);
            this.tvWebResources.Name = "tvWebResources";
            this.tvWebResources.SelectedImageIndex = 0;
            this.tvWebResources.Size = new System.Drawing.Size(338, 540);
            this.tvWebResources.TabIndex = 83;
            this.tvWebResources.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TvWebResourcesAfterCheck);
            this.tvWebResources.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TvWebResourcesAfterSelect);
            this.tvWebResources.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TvWebResourcesMouseDown);
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
            // lblResourceName
            // 
            this.lblResourceName.AutoSize = true;
            this.lblResourceName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResourceName.Location = new System.Drawing.Point(3, 3);
            this.lblResourceName.Name = "lblResourceName";
            this.lblResourceName.Size = new System.Drawing.Size(0, 25);
            this.lblResourceName.TabIndex = 5;
            // 
            // panelControl
            // 
            this.panelControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl.Location = new System.Drawing.Point(0, 60);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(538, 506);
            this.panelControl.TabIndex = 4;
            // 
            // toolStripScriptContent
            // 
            this.toolStripScriptContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStripScriptContent.AutoSize = false;
            this.toolStripScriptContent.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripScriptContent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSaveContent,
            this.tsbUpload,
            this.toolStripSeparator8,
            this.tsbPublish,
            this.toolStripSeparatorMinifyJS,
            this.tsbMinifyJS,
            this.tsbPreviewHtml});
            this.toolStripScriptContent.Location = new System.Drawing.Point(0, 32);
            this.toolStripScriptContent.Name = "toolStripScriptContent";
            this.toolStripScriptContent.Size = new System.Drawing.Size(538, 25);
            this.toolStripScriptContent.TabIndex = 3;
            this.toolStripScriptContent.Text = "toolStripScriptContent";
            this.toolStripScriptContent.Visible = false;
            // 
            // tsbSaveContent
            // 
            this.tsbSaveContent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveContent.Enabled = false;
            this.tsbSaveContent.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveContent.Image")));
            this.tsbSaveContent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveContent.Name = "tsbSaveContent";
            this.tsbSaveContent.Size = new System.Drawing.Size(23, 22);
            this.tsbSaveContent.Text = "Save Web Resource";
            this.tsbSaveContent.Click += new System.EventHandler(this.TsbSaveContentClick);
            // 
            // tsbUpload
            // 
            this.tsbUpload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbUpload.Enabled = false;
            this.tsbUpload.Image = ((System.Drawing.Image)(resources.GetObject("tsbUpload.Image")));
            this.tsbUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUpload.Name = "tsbUpload";
            this.tsbUpload.Size = new System.Drawing.Size(23, 22);
            this.tsbUpload.Text = "Replace with new file";
            this.tsbUpload.Click += new System.EventHandler(this.TsbUploadClick);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbPublish
            // 
            this.tsbPublish.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPublish.Enabled = false;
            this.tsbPublish.Image = ((System.Drawing.Image)(resources.GetObject("tsbPublish.Image")));
            this.tsbPublish.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPublish.Name = "tsbPublish";
            this.tsbPublish.Size = new System.Drawing.Size(23, 22);
            this.tsbPublish.Text = "Publish this web resource";
            this.tsbPublish.Click += new System.EventHandler(this.TsbPublishClick);
            // 
            // toolStripSeparatorMinifyJS
            // 
            this.toolStripSeparatorMinifyJS.Name = "toolStripSeparatorMinifyJS";
            this.toolStripSeparatorMinifyJS.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparatorMinifyJS.Visible = false;
            // 
            // tsbMinifyJS
            // 
            this.tsbMinifyJS.Image = ((System.Drawing.Image)(resources.GetObject("tsbMinifyJS.Image")));
            this.tsbMinifyJS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMinifyJS.Name = "tsbMinifyJS";
            this.tsbMinifyJS.Size = new System.Drawing.Size(80, 22);
            this.tsbMinifyJS.Text = "Compress";
            this.tsbMinifyJS.ToolTipText = "This feature compress/minify a script web resource. It does not obfuscate the cod" +
    "e, just remove useless formatting.\r\nBe careful when using this feature! There is" +
    " no way to beautify minified JavaScript";
            this.tsbMinifyJS.Visible = false;
            this.tsbMinifyJS.Click += new System.EventHandler(this.TsbMinifyJsClick);
            // 
            // tsbPreviewHtml
            // 
            this.tsbPreviewHtml.Image = ((System.Drawing.Image)(resources.GetObject("tsbPreviewHtml.Image")));
            this.tsbPreviewHtml.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPreviewHtml.Name = "tsbPreviewHtml";
            this.tsbPreviewHtml.Size = new System.Drawing.Size(68, 22);
            this.tsbPreviewHtml.Text = "Preview";
            this.tsbPreviewHtml.ToolTipText = "This feature allows you to preview HTML pages. It does not warn about script erro" +
    "r, so the HTML page could not render or behave as expected";
            this.tsbPreviewHtml.Visible = false;
            this.tsbPreviewHtml.Click += new System.EventHandler(this.TsbPreviewHtmlClick);
            // 
            // tsMain
            // 
            this.tsMain.AutoSize = false;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCloseThisTab,
            this.toolStripSeparator2,
            this.tsddCrmMenu,
            this.tsddFileMenu,
            this.toolStripSeparator6,
            this.tsbNewRoot});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(900, 25);
            this.tsMain.TabIndex = 88;
            this.tsMain.Text = "toolStrip1";
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsddCrmMenu
            // 
            this.tsddCrmMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadWebResourcesToolStripMenuItem,
            this.updateCheckedWebResourcesToolStripMenuItem,
            this.updateAndPublishCheckedWebResourcesToolStripMenuItem,
            this.updatePublishAndAddToSolutionToolStripMenuItem});
            this.tsddCrmMenu.Image = ((System.Drawing.Image)(resources.GetObject("tsddCrmMenu.Image")));
            this.tsddCrmMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddCrmMenu.Name = "tsddCrmMenu";
            this.tsddCrmMenu.Size = new System.Drawing.Size(62, 22);
            this.tsddCrmMenu.Text = "CRM";
            // 
            // loadWebResourcesToolStripMenuItem
            // 
            this.loadWebResourcesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("loadWebResourcesToolStripMenuItem.Image")));
            this.loadWebResourcesToolStripMenuItem.Name = "loadWebResourcesToolStripMenuItem";
            this.loadWebResourcesToolStripMenuItem.Size = new System.Drawing.Size(388, 22);
            this.loadWebResourcesToolStripMenuItem.Text = "Load Web resources";
            this.loadWebResourcesToolStripMenuItem.Click += new System.EventHandler(this.LoadWebResourcesToolStripMenuItemClick);
            // 
            // updateCheckedWebResourcesToolStripMenuItem
            // 
            this.updateCheckedWebResourcesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("updateCheckedWebResourcesToolStripMenuItem.Image")));
            this.updateCheckedWebResourcesToolStripMenuItem.Name = "updateCheckedWebResourcesToolStripMenuItem";
            this.updateCheckedWebResourcesToolStripMenuItem.Size = new System.Drawing.Size(388, 22);
            this.updateCheckedWebResourcesToolStripMenuItem.Text = "Update checked Web resources";
            this.updateCheckedWebResourcesToolStripMenuItem.Click += new System.EventHandler(this.UpdateCheckedWebResourcesToolStripMenuItemClick);
            // 
            // updateAndPublishCheckedWebResourcesToolStripMenuItem
            // 
            this.updateAndPublishCheckedWebResourcesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("updateAndPublishCheckedWebResourcesToolStripMenuItem.Image")));
            this.updateAndPublishCheckedWebResourcesToolStripMenuItem.Name = "updateAndPublishCheckedWebResourcesToolStripMenuItem";
            this.updateAndPublishCheckedWebResourcesToolStripMenuItem.Size = new System.Drawing.Size(388, 22);
            this.updateAndPublishCheckedWebResourcesToolStripMenuItem.Text = "Update and publish checked Web resources";
            this.updateAndPublishCheckedWebResourcesToolStripMenuItem.Click += new System.EventHandler(this.UpdateAndPublishCheckedWebResourcesToolStripMenuItemClick);
            // 
            // updatePublishAndAddToSolutionToolStripMenuItem
            // 
            this.updatePublishAndAddToSolutionToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("updatePublishAndAddToSolutionToolStripMenuItem.Image")));
            this.updatePublishAndAddToSolutionToolStripMenuItem.Name = "updatePublishAndAddToSolutionToolStripMenuItem";
            this.updatePublishAndAddToSolutionToolStripMenuItem.Size = new System.Drawing.Size(388, 22);
            this.updatePublishAndAddToSolutionToolStripMenuItem.Text = "Update, publish and add to solution checked web resources";
            this.updatePublishAndAddToSolutionToolStripMenuItem.Click += new System.EventHandler(this.UpdatePublishAndAddToSolutionToolStripMenuItemClick);
            // 
            // tsddFileMenu
            // 
            this.tsddFileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadWebResourcesToolStripMenuItem1,
            this.saveCheckedWebResourcesToDiskToolStripMenuItem,
            this.saveAllWebResourcesToDiskToolStripMenuItem});
            this.tsddFileMenu.Image = ((System.Drawing.Image)(resources.GetObject("tsddFileMenu.Image")));
            this.tsddFileMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddFileMenu.Name = "tsddFileMenu";
            this.tsddFileMenu.Size = new System.Drawing.Size(54, 22);
            this.tsddFileMenu.Text = "File";
            // 
            // loadWebResourcesToolStripMenuItem1
            // 
            this.loadWebResourcesToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("loadWebResourcesToolStripMenuItem1.Image")));
            this.loadWebResourcesToolStripMenuItem1.Name = "loadWebResourcesToolStripMenuItem1";
            this.loadWebResourcesToolStripMenuItem1.Size = new System.Drawing.Size(263, 22);
            this.loadWebResourcesToolStripMenuItem1.Text = "Load Web resources";
            this.loadWebResourcesToolStripMenuItem1.Click += new System.EventHandler(this.LoadWebResourcesToolStripMenuItem1Click);
            // 
            // saveCheckedWebResourcesToDiskToolStripMenuItem
            // 
            this.saveCheckedWebResourcesToDiskToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveCheckedWebResourcesToDiskToolStripMenuItem.Image")));
            this.saveCheckedWebResourcesToDiskToolStripMenuItem.Name = "saveCheckedWebResourcesToDiskToolStripMenuItem";
            this.saveCheckedWebResourcesToDiskToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.saveCheckedWebResourcesToDiskToolStripMenuItem.Text = "Save checked Web resources to disk";
            this.saveCheckedWebResourcesToDiskToolStripMenuItem.Click += new System.EventHandler(this.SaveCheckedWebResourcesToDiskToolStripMenuItemClick);
            // 
            // saveAllWebResourcesToDiskToolStripMenuItem
            // 
            this.saveAllWebResourcesToDiskToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveAllWebResourcesToDiskToolStripMenuItem.Image")));
            this.saveAllWebResourcesToDiskToolStripMenuItem.Name = "saveAllWebResourcesToDiskToolStripMenuItem";
            this.saveAllWebResourcesToDiskToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.saveAllWebResourcesToDiskToolStripMenuItem.Text = "Save all Web resources to disk";
            this.saveAllWebResourcesToDiskToolStripMenuItem.Click += new System.EventHandler(this.SaveAllWebResourcesToDiskToolStripMenuItemClick);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbNewRoot
            // 
            this.tsbNewRoot.Image = ((System.Drawing.Image)(resources.GetObject("tsbNewRoot.Image")));
            this.tsbNewRoot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNewRoot.Name = "tsbNewRoot";
            this.tsbNewRoot.Size = new System.Drawing.Size(76, 22);
            this.tsbNewRoot.Text = "New root";
            this.tsbNewRoot.Click += new System.EventHandler(this.TsbNewRootClick);
            // 
            // contextMenuStripTreeView
            // 
            this.contextMenuStripTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewWebResourceToolStripMenuItem,
            this.addNewEmptyWebResourceToolStripMenuItem,
            this.addNewFolderToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveToCRMServerToolStripMenuItem,
            this.saveAndPublishToCRMServerToolStripMenuItem,
            this.savePublishAndAddToSolutionToolStripMenuItem,
            this.toolStripSeparator3,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator5,
            this.propertiesToolStripMenuItem});
            this.contextMenuStripTreeView.Name = "contextMenuStripTreeView";
            this.contextMenuStripTreeView.Size = new System.Drawing.Size(274, 220);
            // 
            // addNewWebResourceToolStripMenuItem
            // 
            this.addNewWebResourceToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addNewWebResourceToolStripMenuItem.Image")));
            this.addNewWebResourceToolStripMenuItem.Name = "addNewWebResourceToolStripMenuItem";
            this.addNewWebResourceToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.addNewWebResourceToolStripMenuItem.Text = "Add existing file(s) as Web resource(s)";
            this.addNewWebResourceToolStripMenuItem.Click += new System.EventHandler(this.AddNewWebResourceToolStripMenuItemClick);
            // 
            // addNewEmptyWebResourceToolStripMenuItem
            // 
            this.addNewEmptyWebResourceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hTMLToolStripMenuItem,
            this.cSSToolStripMenuItem,
            this.scriptToolStripMenuItem,
            this.dataToolStripMenuItem,
            this.xSLTToolStripMenuItem});
            this.addNewEmptyWebResourceToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addNewEmptyWebResourceToolStripMenuItem.Image")));
            this.addNewEmptyWebResourceToolStripMenuItem.Name = "addNewEmptyWebResourceToolStripMenuItem";
            this.addNewEmptyWebResourceToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.addNewEmptyWebResourceToolStripMenuItem.Text = "Add new empty web resource";
            // 
            // hTMLToolStripMenuItem
            // 
            this.hTMLToolStripMenuItem.Name = "hTMLToolStripMenuItem";
            this.hTMLToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.hTMLToolStripMenuItem.Text = "Web Page (HTML)";
            this.hTMLToolStripMenuItem.Click += new System.EventHandler(this.AddNewEmptyWebRessource);
            // 
            // cSSToolStripMenuItem
            // 
            this.cSSToolStripMenuItem.Name = "cSSToolStripMenuItem";
            this.cSSToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.cSSToolStripMenuItem.Text = "Style Sheet (CSS)";
            this.cSSToolStripMenuItem.Click += new System.EventHandler(this.AddNewEmptyWebRessource);
            // 
            // scriptToolStripMenuItem
            // 
            this.scriptToolStripMenuItem.Name = "scriptToolStripMenuItem";
            this.scriptToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.scriptToolStripMenuItem.Text = "Script (JScript)";
            this.scriptToolStripMenuItem.Click += new System.EventHandler(this.AddNewEmptyWebRessource);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.dataToolStripMenuItem.Text = "Data (XML)";
            this.dataToolStripMenuItem.Click += new System.EventHandler(this.AddNewEmptyWebRessource);
            // 
            // xSLTToolStripMenuItem
            // 
            this.xSLTToolStripMenuItem.Name = "xSLTToolStripMenuItem";
            this.xSLTToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.xSLTToolStripMenuItem.Text = "Style Sheet (XSL)";
            this.xSLTToolStripMenuItem.Click += new System.EventHandler(this.AddNewEmptyWebRessource);
            // 
            // addNewFolderToolStripMenuItem
            // 
            this.addNewFolderToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addNewFolderToolStripMenuItem.Image")));
            this.addNewFolderToolStripMenuItem.Name = "addNewFolderToolStripMenuItem";
            this.addNewFolderToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.addNewFolderToolStripMenuItem.Text = "Add new folder";
            this.addNewFolderToolStripMenuItem.Click += new System.EventHandler(this.AddNewFolderToolStripMenuItemClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(270, 6);
            // 
            // saveToCRMServerToolStripMenuItem
            // 
            this.saveToCRMServerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToCRMServerToolStripMenuItem.Image")));
            this.saveToCRMServerToolStripMenuItem.Name = "saveToCRMServerToolStripMenuItem";
            this.saveToCRMServerToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.saveToCRMServerToolStripMenuItem.Text = "Save to CRM server";
            this.saveToCRMServerToolStripMenuItem.Click += new System.EventHandler(this.SaveToCrmServerToolStripMenuItemClick);
            // 
            // saveAndPublishToCRMServerToolStripMenuItem
            // 
            this.saveAndPublishToCRMServerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveAndPublishToCRMServerToolStripMenuItem.Image")));
            this.saveAndPublishToCRMServerToolStripMenuItem.Name = "saveAndPublishToCRMServerToolStripMenuItem";
            this.saveAndPublishToCRMServerToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.saveAndPublishToCRMServerToolStripMenuItem.Text = "Save and Publish to CRM server";
            this.saveAndPublishToCRMServerToolStripMenuItem.Click += new System.EventHandler(this.SaveAndPublishToCrmServerToolStripMenuItemClick);
            // 
            // savePublishAndAddToSolutionToolStripMenuItem
            // 
            this.savePublishAndAddToSolutionToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("savePublishAndAddToSolutionToolStripMenuItem.Image")));
            this.savePublishAndAddToSolutionToolStripMenuItem.Name = "savePublishAndAddToSolutionToolStripMenuItem";
            this.savePublishAndAddToSolutionToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.savePublishAndAddToSolutionToolStripMenuItem.Text = "Save, publish and add to solution";
            this.savePublishAndAddToSolutionToolStripMenuItem.Click += new System.EventHandler(this.SavePublishAndAddToSolutionToolStripMenuItemClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(270, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItem.Image")));
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItemClick);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(270, 6);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("propertiesToolStripMenuItem.Image")));
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.propertiesToolStripMenuItem.Text = "Properties";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.PropertiesToolStripMenuItemClick);
            // 
            // WebResourcesManagerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tsMain);
            this.Name = "WebResourcesManagerControl";
            this.Size = new System.Drawing.Size(900, 600);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStripScriptContent.ResumeLayout(false);
            this.toolStripScriptContent.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.contextMenuStripTreeView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.TreeView tvWebResources;
        private System.Windows.Forms.Label lblResourceName;
        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.ToolStrip toolStripScriptContent;
        private System.Windows.Forms.ToolStripButton tsbSaveContent;
        private System.Windows.Forms.ToolStripButton tsbUpload;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton tsbPublish;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorMinifyJS;
        private System.Windows.Forms.ToolStripButton tsbMinifyJS;
        private System.Windows.Forms.ToolStripButton tsbPreviewHtml;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripDropDownButton tsddCrmMenu;
        private System.Windows.Forms.ToolStripMenuItem loadWebResourcesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateCheckedWebResourcesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateAndPublishCheckedWebResourcesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updatePublishAndAddToSolutionToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton tsddFileMenu;
        private System.Windows.Forms.ToolStripMenuItem loadWebResourcesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveCheckedWebResourcesToDiskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAllWebResourcesToDiskToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsbNewRoot;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTreeView;
        private System.Windows.Forms.ToolStripMenuItem addNewWebResourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewEmptyWebResourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hTMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cSSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xSLTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveToCRMServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAndPublishToCRMServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savePublishAndAddToSolutionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbCloseThisTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ImageList imageList1;
    }
}
