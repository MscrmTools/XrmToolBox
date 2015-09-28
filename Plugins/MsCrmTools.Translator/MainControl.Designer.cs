namespace MsCrmTools.Translator
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
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLoadEntities = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExportTranslations = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbImportTranslations = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCheckAll = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.gbEntitiesOptions = new System.Windows.Forms.GroupBox();
            this.chkExportCustomizedRelationships = new System.Windows.Forms.CheckBox();
            this.chkExportEntity = new System.Windows.Forms.CheckBox();
            this.chkExportAttributes = new System.Windows.Forms.CheckBox();
            this.chkExportFormsFields = new System.Windows.Forms.CheckBox();
            this.chkExportPicklists = new System.Windows.Forms.CheckBox();
            this.chkExportFormsSections = new System.Windows.Forms.CheckBox();
            this.chkExportBooleans = new System.Windows.Forms.CheckBox();
            this.chkExportFormsTabs = new System.Windows.Forms.CheckBox();
            this.chkExportViews = new System.Windows.Forms.CheckBox();
            this.chkExportForms = new System.Windows.Forms.CheckBox();
            this.gbGlobalOptions = new System.Windows.Forms.GroupBox();
            this.chkExportSiteMap = new System.Windows.Forms.CheckBox();
            this.chkExportGlobalOptSet = new System.Windows.Forms.CheckBox();
            this.lvEntities = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnBrowseImportFile = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkExportDashboards = new System.Windows.Forms.CheckBox();
            this.toolStripMenu.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbEntitiesOptions.SuspendLayout();
            this.gbGlobalOptions.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.toolStripSeparator1,
            this.tsbLoadEntities,
            this.toolStripSeparator2,
            this.tsbExportTranslations,
            this.toolStripSeparator3,
            this.tsbImportTranslations});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripMenu.Size = new System.Drawing.Size(1366, 32);
            this.toolStripMenu.TabIndex = 2;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(28, 29);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.TsbCloseClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbLoadEntities
            // 
            this.tsbLoadEntities.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadEntities.Image")));
            this.tsbLoadEntities.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadEntities.Name = "tsbLoadEntities";
            this.tsbLoadEntities.Size = new System.Drawing.Size(140, 29);
            this.tsbLoadEntities.Text = "Load entities";
            this.tsbLoadEntities.Click += new System.EventHandler(this.TsbLoadEntitiesClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbExportTranslations
            // 
            this.tsbExportTranslations.Image = ((System.Drawing.Image)(resources.GetObject("tsbExportTranslations.Image")));
            this.tsbExportTranslations.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExportTranslations.Name = "tsbExportTranslations";
            this.tsbExportTranslations.Size = new System.Drawing.Size(187, 29);
            this.tsbExportTranslations.Text = "Export translations";
            this.tsbExportTranslations.Click += new System.EventHandler(this.BtnExportTranslationsClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbImportTranslations
            // 
            this.tsbImportTranslations.Image = ((System.Drawing.Image)(resources.GetObject("tsbImportTranslations.Image")));
            this.tsbImportTranslations.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImportTranslations.Name = "tsbImportTranslations";
            this.tsbImportTranslations.Size = new System.Drawing.Size(191, 29);
            this.tsbImportTranslations.Text = "Import translations";
            this.tsbImportTranslations.Click += new System.EventHandler(this.BtnImportTranslationsClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(4, 43);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1358, 875);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Size = new System.Drawing.Size(1350, 842);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Export translations";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnCheckAll);
            this.groupBox2.Controls.Add(this.btnClearAll);
            this.groupBox2.Controls.Add(this.gbEntitiesOptions);
            this.groupBox2.Controls.Add(this.gbGlobalOptions);
            this.groupBox2.Controls.Add(this.lvEntities);
            this.groupBox2.Location = new System.Drawing.Point(4, 9);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(1332, 817);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Entities options";
            // 
            // btnCheckAll
            // 
            this.btnCheckAll.Location = new System.Drawing.Point(248, 29);
            this.btnCheckAll.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.Size = new System.Drawing.Size(112, 35);
            this.btnCheckAll.TabIndex = 95;
            this.btnCheckAll.Text = "Check all";
            this.btnCheckAll.UseVisualStyleBackColor = true;
            this.btnCheckAll.Click += new System.EventHandler(this.BtnCheckAllClick);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(369, 29);
            this.btnClearAll.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(112, 35);
            this.btnClearAll.TabIndex = 94;
            this.btnClearAll.Text = "Clear all";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.BtnClearAllClick);
            // 
            // gbEntitiesOptions
            // 
            this.gbEntitiesOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbEntitiesOptions.Controls.Add(this.chkExportCustomizedRelationships);
            this.gbEntitiesOptions.Controls.Add(this.chkExportEntity);
            this.gbEntitiesOptions.Controls.Add(this.chkExportAttributes);
            this.gbEntitiesOptions.Controls.Add(this.chkExportFormsFields);
            this.gbEntitiesOptions.Controls.Add(this.chkExportPicklists);
            this.gbEntitiesOptions.Controls.Add(this.chkExportFormsSections);
            this.gbEntitiesOptions.Controls.Add(this.chkExportBooleans);
            this.gbEntitiesOptions.Controls.Add(this.chkExportFormsTabs);
            this.gbEntitiesOptions.Controls.Add(this.chkExportViews);
            this.gbEntitiesOptions.Controls.Add(this.chkExportForms);
            this.gbEntitiesOptions.Location = new System.Drawing.Point(490, 189);
            this.gbEntitiesOptions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbEntitiesOptions.MinimumSize = new System.Drawing.Size(376, 382);
            this.gbEntitiesOptions.Name = "gbEntitiesOptions";
            this.gbEntitiesOptions.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbEntitiesOptions.Size = new System.Drawing.Size(832, 382);
            this.gbEntitiesOptions.TabIndex = 93;
            this.gbEntitiesOptions.TabStop = false;
            this.gbEntitiesOptions.Text = "Entity related options";
            // 
            // chkExportCustomizedRelationships
            // 
            this.chkExportCustomizedRelationships.AutoSize = true;
            this.chkExportCustomizedRelationships.Checked = true;
            this.chkExportCustomizedRelationships.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportCustomizedRelationships.Location = new System.Drawing.Point(9, 346);
            this.chkExportCustomizedRelationships.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkExportCustomizedRelationships.Name = "chkExportCustomizedRelationships";
            this.chkExportCustomizedRelationships.Size = new System.Drawing.Size(383, 24);
            this.chkExportCustomizedRelationships.TabIndex = 90;
            this.chkExportCustomizedRelationships.Text = "Export Relationships that are using custom labels";
            this.chkExportCustomizedRelationships.UseVisualStyleBackColor = true;
            // 
            // chkExportEntity
            // 
            this.chkExportEntity.AutoSize = true;
            this.chkExportEntity.Checked = true;
            this.chkExportEntity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportEntity.Location = new System.Drawing.Point(9, 29);
            this.chkExportEntity.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkExportEntity.Name = "chkExportEntity";
            this.chkExportEntity.Size = new System.Drawing.Size(207, 24);
            this.chkExportEntity.TabIndex = 81;
            this.chkExportEntity.Text = "Export Entity Translation";
            this.chkExportEntity.UseVisualStyleBackColor = true;
            // 
            // chkExportAttributes
            // 
            this.chkExportAttributes.AutoSize = true;
            this.chkExportAttributes.Checked = true;
            this.chkExportAttributes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportAttributes.Location = new System.Drawing.Point(9, 65);
            this.chkExportAttributes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkExportAttributes.Name = "chkExportAttributes";
            this.chkExportAttributes.Size = new System.Drawing.Size(236, 24);
            this.chkExportAttributes.TabIndex = 82;
            this.chkExportAttributes.Text = "Export Attributes Translation";
            this.chkExportAttributes.UseVisualStyleBackColor = true;
            // 
            // chkExportFormsFields
            // 
            this.chkExportFormsFields.AutoSize = true;
            this.chkExportFormsFields.Checked = true;
            this.chkExportFormsFields.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportFormsFields.Location = new System.Drawing.Point(9, 312);
            this.chkExportFormsFields.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkExportFormsFields.Name = "chkExportFormsFields";
            this.chkExportFormsFields.Size = new System.Drawing.Size(258, 24);
            this.chkExportFormsFields.TabIndex = 89;
            this.chkExportFormsFields.Text = "Export Forms Fields Translation";
            this.chkExportFormsFields.UseVisualStyleBackColor = true;
            // 
            // chkExportPicklists
            // 
            this.chkExportPicklists.AutoSize = true;
            this.chkExportPicklists.Checked = true;
            this.chkExportPicklists.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportPicklists.Location = new System.Drawing.Point(9, 100);
            this.chkExportPicklists.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkExportPicklists.Name = "chkExportPicklists";
            this.chkExportPicklists.Size = new System.Drawing.Size(325, 24);
            this.chkExportPicklists.TabIndex = 83;
            this.chkExportPicklists.Text = "Export Picklists Option Labels Translation";
            this.chkExportPicklists.UseVisualStyleBackColor = true;
            // 
            // chkExportFormsSections
            // 
            this.chkExportFormsSections.AutoSize = true;
            this.chkExportFormsSections.Checked = true;
            this.chkExportFormsSections.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportFormsSections.Location = new System.Drawing.Point(9, 277);
            this.chkExportFormsSections.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkExportFormsSections.Name = "chkExportFormsSections";
            this.chkExportFormsSections.Size = new System.Drawing.Size(278, 24);
            this.chkExportFormsSections.TabIndex = 88;
            this.chkExportFormsSections.Text = "Export Forms Sections Translation";
            this.chkExportFormsSections.UseVisualStyleBackColor = true;
            // 
            // chkExportBooleans
            // 
            this.chkExportBooleans.AutoSize = true;
            this.chkExportBooleans.Checked = true;
            this.chkExportBooleans.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportBooleans.Location = new System.Drawing.Point(9, 135);
            this.chkExportBooleans.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkExportBooleans.Name = "chkExportBooleans";
            this.chkExportBooleans.Size = new System.Drawing.Size(336, 24);
            this.chkExportBooleans.TabIndex = 84;
            this.chkExportBooleans.Text = "Export Booleans Option Labels Translation";
            this.chkExportBooleans.UseVisualStyleBackColor = true;
            // 
            // chkExportFormsTabs
            // 
            this.chkExportFormsTabs.AutoSize = true;
            this.chkExportFormsTabs.Checked = true;
            this.chkExportFormsTabs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportFormsTabs.Location = new System.Drawing.Point(9, 242);
            this.chkExportFormsTabs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkExportFormsTabs.Name = "chkExportFormsTabs";
            this.chkExportFormsTabs.Size = new System.Drawing.Size(251, 24);
            this.chkExportFormsTabs.TabIndex = 87;
            this.chkExportFormsTabs.Text = "Export Forms Tabs Translation";
            this.chkExportFormsTabs.UseVisualStyleBackColor = true;
            // 
            // chkExportViews
            // 
            this.chkExportViews.AutoSize = true;
            this.chkExportViews.Checked = true;
            this.chkExportViews.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportViews.Location = new System.Drawing.Point(9, 171);
            this.chkExportViews.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkExportViews.Name = "chkExportViews";
            this.chkExportViews.Size = new System.Drawing.Size(209, 24);
            this.chkExportViews.TabIndex = 85;
            this.chkExportViews.Text = "Export Views Translation";
            this.chkExportViews.UseVisualStyleBackColor = true;
            // 
            // chkExportForms
            // 
            this.chkExportForms.AutoSize = true;
            this.chkExportForms.Checked = true;
            this.chkExportForms.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportForms.Location = new System.Drawing.Point(9, 206);
            this.chkExportForms.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkExportForms.Name = "chkExportForms";
            this.chkExportForms.Size = new System.Drawing.Size(212, 24);
            this.chkExportForms.TabIndex = 86;
            this.chkExportForms.Text = "Export Forms Translation";
            this.chkExportForms.UseVisualStyleBackColor = true;
            // 
            // gbGlobalOptions
            // 
            this.gbGlobalOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGlobalOptions.Controls.Add(this.chkExportDashboards);
            this.gbGlobalOptions.Controls.Add(this.chkExportSiteMap);
            this.gbGlobalOptions.Controls.Add(this.chkExportGlobalOptSet);
            this.gbGlobalOptions.Location = new System.Drawing.Point(490, 29);
            this.gbGlobalOptions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbGlobalOptions.MinimumSize = new System.Drawing.Size(376, 108);
            this.gbGlobalOptions.Name = "gbGlobalOptions";
            this.gbGlobalOptions.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbGlobalOptions.Size = new System.Drawing.Size(832, 151);
            this.gbGlobalOptions.TabIndex = 92;
            this.gbGlobalOptions.TabStop = false;
            this.gbGlobalOptions.Text = "Global Options";
            // 
            // chkExportSiteMap
            // 
            this.chkExportSiteMap.AutoSize = true;
            this.chkExportSiteMap.Checked = true;
            this.chkExportSiteMap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportSiteMap.Location = new System.Drawing.Point(8, 63);
            this.chkExportSiteMap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkExportSiteMap.Name = "chkExportSiteMap";
            this.chkExportSiteMap.Size = new System.Drawing.Size(327, 24);
            this.chkExportSiteMap.TabIndex = 92;
            this.chkExportSiteMap.Text = "Export SiteMap custom labels Translation";
            this.chkExportSiteMap.UseVisualStyleBackColor = true;
            // 
            // chkExportGlobalOptSet
            // 
            this.chkExportGlobalOptSet.AutoSize = true;
            this.chkExportGlobalOptSet.Checked = true;
            this.chkExportGlobalOptSet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportGlobalOptSet.Location = new System.Drawing.Point(9, 29);
            this.chkExportGlobalOptSet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkExportGlobalOptSet.Name = "chkExportGlobalOptSet";
            this.chkExportGlobalOptSet.Size = new System.Drawing.Size(348, 24);
            this.chkExportGlobalOptSet.TabIndex = 91;
            this.chkExportGlobalOptSet.Text = "Export Global OptionSets Labels Translation";
            this.chkExportGlobalOptSet.UseVisualStyleBackColor = true;
            // 
            // lvEntities
            // 
            this.lvEntities.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvEntities.CheckBoxes = true;
            this.lvEntities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader7});
            this.lvEntities.FullRowSelect = true;
            this.lvEntities.HideSelection = false;
            this.lvEntities.Location = new System.Drawing.Point(10, 74);
            this.lvEntities.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lvEntities.Name = "lvEntities";
            this.lvEntities.Size = new System.Drawing.Size(469, 732);
            this.lvEntities.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvEntities.TabIndex = 80;
            this.lvEntities.UseCompatibleStateImageBehavior = false;
            this.lvEntities.View = System.Windows.Forms.View.Details;
            this.lvEntities.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LvEntitiesColumnClick);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Display name";
            this.columnHeader4.Width = 157;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Schema name";
            this.columnHeader7.Width = 130;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.btnBrowseImportFile);
            this.tabPage2.Controls.Add(this.txtFilePath);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Size = new System.Drawing.Size(1350, 842);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Import translations";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.LightYellow;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Location = new System.Drawing.Point(14, 275);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1322, 102);
            this.panel2.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(62, 18);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.MinimumSize = new System.Drawing.Size(834, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(834, 82);
            this.label3.TabIndex = 1;
            this.label3.Text = "Please be sure you have a backup of your customizations prior to using this tool." +
    "\r\nIn case of problem, importing this backup solution will restore previous trans" +
    "lations";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(4, 18);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 49);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.LightYellow;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(14, 52);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1322, 213);
            this.panel1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(62, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.MinimumSize = new System.Drawing.Size(735, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(735, 188);
            this.label2.TabIndex = 1;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(4, 18);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 49);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnBrowseImportFile
            // 
            this.btnBrowseImportFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseImportFile.Location = new System.Drawing.Point(1224, 9);
            this.btnBrowseImportFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBrowseImportFile.Name = "btnBrowseImportFile";
            this.btnBrowseImportFile.Size = new System.Drawing.Size(112, 35);
            this.btnBrowseImportFile.TabIndex = 2;
            this.btnBrowseImportFile.Text = "...";
            this.btnBrowseImportFile.UseVisualStyleBackColor = true;
            this.btnBrowseImportFile.Click += new System.EventHandler(this.BtnBrowseImportFileClick);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.Location = new System.Drawing.Point(170, 12);
            this.txtFilePath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(1044, 26);
            this.txtFilePath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Translation file";
            // 
            // chkExportDashboards
            // 
            this.chkExportDashboards.AutoSize = true;
            this.chkExportDashboards.Checked = true;
            this.chkExportDashboards.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportDashboards.Location = new System.Drawing.Point(9, 97);
            this.chkExportDashboards.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkExportDashboards.Name = "chkExportDashboards";
            this.chkExportDashboards.Size = new System.Drawing.Size(355, 24);
            this.chkExportDashboards.TabIndex = 93;
            this.chkExportDashboards.Text = "Export Dashboards custom labels Translation";
            this.chkExportDashboards.UseVisualStyleBackColor = true;
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStripMenu);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(1366, 923);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.gbEntitiesOptions.ResumeLayout(false);
            this.gbEntitiesOptions.PerformLayout();
            this.gbGlobalOptions.ResumeLayout(false);
            this.gbGlobalOptions.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView lvEntities;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.CheckBox chkExportFormsFields;
        private System.Windows.Forms.CheckBox chkExportFormsSections;
        private System.Windows.Forms.CheckBox chkExportFormsTabs;
        private System.Windows.Forms.CheckBox chkExportForms;
        private System.Windows.Forms.CheckBox chkExportViews;
        private System.Windows.Forms.CheckBox chkExportBooleans;
        private System.Windows.Forms.CheckBox chkExportPicklists;
        private System.Windows.Forms.CheckBox chkExportAttributes;
        private System.Windows.Forms.CheckBox chkExportEntity;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbLoadEntities;
        private System.Windows.Forms.Button btnBrowseImportFile;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkExportGlobalOptSet;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbExportTranslations;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbImportTranslations;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox gbEntitiesOptions;
        private System.Windows.Forms.GroupBox gbGlobalOptions;
        private System.Windows.Forms.Button btnCheckAll;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.CheckBox chkExportSiteMap;
        private System.Windows.Forms.CheckBox chkExportCustomizedRelationships;
        private System.Windows.Forms.CheckBox chkExportDashboards;
    }
}
