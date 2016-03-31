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
            this.chkExportCharts = new System.Windows.Forms.CheckBox();
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
            this.chkExportDashboards = new System.Windows.Forms.CheckBox();
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
            this.toolStripMenu.Size = new System.Drawing.Size(911, 25);
            this.toolStripMenu.TabIndex = 2;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(23, 22);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.TsbCloseClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbLoadEntities
            // 
            this.tsbLoadEntities.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadEntities.Image")));
            this.tsbLoadEntities.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadEntities.Name = "tsbLoadEntities";
            this.tsbLoadEntities.Size = new System.Drawing.Size(94, 22);
            this.tsbLoadEntities.Text = "Load entities";
            this.tsbLoadEntities.Click += new System.EventHandler(this.TsbLoadEntitiesClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbExportTranslations
            // 
            this.tsbExportTranslations.Image = ((System.Drawing.Image)(resources.GetObject("tsbExportTranslations.Image")));
            this.tsbExportTranslations.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExportTranslations.Name = "tsbExportTranslations";
            this.tsbExportTranslations.Size = new System.Drawing.Size(124, 22);
            this.tsbExportTranslations.Text = "Export translations";
            this.tsbExportTranslations.Click += new System.EventHandler(this.BtnExportTranslationsClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbImportTranslations
            // 
            this.tsbImportTranslations.Image = ((System.Drawing.Image)(resources.GetObject("tsbImportTranslations.Image")));
            this.tsbImportTranslations.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImportTranslations.Name = "tsbImportTranslations";
            this.tsbImportTranslations.Size = new System.Drawing.Size(127, 22);
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
            this.tabControl1.Location = new System.Drawing.Point(3, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(905, 569);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(897, 543);
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
            this.groupBox2.Location = new System.Drawing.Point(3, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(888, 531);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Entities options";
            // 
            // btnCheckAll
            // 
            this.btnCheckAll.Location = new System.Drawing.Point(165, 19);
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.Size = new System.Drawing.Size(75, 23);
            this.btnCheckAll.TabIndex = 95;
            this.btnCheckAll.Text = "Check all";
            this.btnCheckAll.UseVisualStyleBackColor = true;
            this.btnCheckAll.Click += new System.EventHandler(this.BtnCheckAllClick);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(246, 19);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(75, 23);
            this.btnClearAll.TabIndex = 94;
            this.btnClearAll.Text = "Clear all";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.BtnClearAllClick);
            // 
            // gbEntitiesOptions
            // 
            this.gbEntitiesOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbEntitiesOptions.Controls.Add(this.chkExportCharts);
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
            this.gbEntitiesOptions.Location = new System.Drawing.Point(327, 123);
            this.gbEntitiesOptions.MinimumSize = new System.Drawing.Size(251, 248);
            this.gbEntitiesOptions.Name = "gbEntitiesOptions";
            this.gbEntitiesOptions.Size = new System.Drawing.Size(555, 314);
            this.gbEntitiesOptions.TabIndex = 93;
            this.gbEntitiesOptions.TabStop = false;
            this.gbEntitiesOptions.Text = "Entity related options";
            // 
            // chkExportCharts
            // 
            this.chkExportCharts.AutoSize = true;
            this.chkExportCharts.Checked = true;
            this.chkExportCharts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportCharts.Location = new System.Drawing.Point(6, 134);
            this.chkExportCharts.Name = "chkExportCharts";
            this.chkExportCharts.Size = new System.Drawing.Size(144, 17);
            this.chkExportCharts.TabIndex = 91;
            this.chkExportCharts.Text = "Export Charts Translation";
            this.chkExportCharts.UseVisualStyleBackColor = true;
            // 
            // chkExportCustomizedRelationships
            // 
            this.chkExportCustomizedRelationships.AutoSize = true;
            this.chkExportCustomizedRelationships.Checked = true;
            this.chkExportCustomizedRelationships.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportCustomizedRelationships.Location = new System.Drawing.Point(6, 248);
            this.chkExportCustomizedRelationships.Name = "chkExportCustomizedRelationships";
            this.chkExportCustomizedRelationships.Size = new System.Drawing.Size(256, 17);
            this.chkExportCustomizedRelationships.TabIndex = 90;
            this.chkExportCustomizedRelationships.Text = "Export Relationships that are using custom labels";
            this.chkExportCustomizedRelationships.UseVisualStyleBackColor = true;
            // 
            // chkExportEntity
            // 
            this.chkExportEntity.AutoSize = true;
            this.chkExportEntity.Checked = true;
            this.chkExportEntity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportEntity.Location = new System.Drawing.Point(6, 19);
            this.chkExportEntity.Name = "chkExportEntity";
            this.chkExportEntity.Size = new System.Drawing.Size(140, 17);
            this.chkExportEntity.TabIndex = 81;
            this.chkExportEntity.Text = "Export Entity Translation";
            this.chkExportEntity.UseVisualStyleBackColor = true;
            // 
            // chkExportAttributes
            // 
            this.chkExportAttributes.AutoSize = true;
            this.chkExportAttributes.Checked = true;
            this.chkExportAttributes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportAttributes.Location = new System.Drawing.Point(6, 42);
            this.chkExportAttributes.Name = "chkExportAttributes";
            this.chkExportAttributes.Size = new System.Drawing.Size(158, 17);
            this.chkExportAttributes.TabIndex = 82;
            this.chkExportAttributes.Text = "Export Attributes Translation";
            this.chkExportAttributes.UseVisualStyleBackColor = true;
            // 
            // chkExportFormsFields
            // 
            this.chkExportFormsFields.AutoSize = true;
            this.chkExportFormsFields.Checked = true;
            this.chkExportFormsFields.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportFormsFields.Location = new System.Drawing.Point(6, 226);
            this.chkExportFormsFields.Name = "chkExportFormsFields";
            this.chkExportFormsFields.Size = new System.Drawing.Size(172, 17);
            this.chkExportFormsFields.TabIndex = 89;
            this.chkExportFormsFields.Text = "Export Forms Fields Translation";
            this.chkExportFormsFields.UseVisualStyleBackColor = true;
            // 
            // chkExportPicklists
            // 
            this.chkExportPicklists.AutoSize = true;
            this.chkExportPicklists.Checked = true;
            this.chkExportPicklists.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportPicklists.Location = new System.Drawing.Point(6, 65);
            this.chkExportPicklists.Name = "chkExportPicklists";
            this.chkExportPicklists.Size = new System.Drawing.Size(220, 17);
            this.chkExportPicklists.TabIndex = 83;
            this.chkExportPicklists.Text = "Export Picklists Option Labels Translation";
            this.chkExportPicklists.UseVisualStyleBackColor = true;
            // 
            // chkExportFormsSections
            // 
            this.chkExportFormsSections.AutoSize = true;
            this.chkExportFormsSections.Checked = true;
            this.chkExportFormsSections.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportFormsSections.Location = new System.Drawing.Point(6, 203);
            this.chkExportFormsSections.Name = "chkExportFormsSections";
            this.chkExportFormsSections.Size = new System.Drawing.Size(186, 17);
            this.chkExportFormsSections.TabIndex = 88;
            this.chkExportFormsSections.Text = "Export Forms Sections Translation";
            this.chkExportFormsSections.UseVisualStyleBackColor = true;
            // 
            // chkExportBooleans
            // 
            this.chkExportBooleans.AutoSize = true;
            this.chkExportBooleans.Checked = true;
            this.chkExportBooleans.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportBooleans.Location = new System.Drawing.Point(6, 88);
            this.chkExportBooleans.Name = "chkExportBooleans";
            this.chkExportBooleans.Size = new System.Drawing.Size(226, 17);
            this.chkExportBooleans.TabIndex = 84;
            this.chkExportBooleans.Text = "Export Booleans Option Labels Translation";
            this.chkExportBooleans.UseVisualStyleBackColor = true;
            // 
            // chkExportFormsTabs
            // 
            this.chkExportFormsTabs.AutoSize = true;
            this.chkExportFormsTabs.Checked = true;
            this.chkExportFormsTabs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportFormsTabs.Location = new System.Drawing.Point(6, 180);
            this.chkExportFormsTabs.Name = "chkExportFormsTabs";
            this.chkExportFormsTabs.Size = new System.Drawing.Size(169, 17);
            this.chkExportFormsTabs.TabIndex = 87;
            this.chkExportFormsTabs.Text = "Export Forms Tabs Translation";
            this.chkExportFormsTabs.UseVisualStyleBackColor = true;
            // 
            // chkExportViews
            // 
            this.chkExportViews.AutoSize = true;
            this.chkExportViews.Checked = true;
            this.chkExportViews.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportViews.Location = new System.Drawing.Point(6, 111);
            this.chkExportViews.Name = "chkExportViews";
            this.chkExportViews.Size = new System.Drawing.Size(142, 17);
            this.chkExportViews.TabIndex = 85;
            this.chkExportViews.Text = "Export Views Translation";
            this.chkExportViews.UseVisualStyleBackColor = true;
            // 
            // chkExportForms
            // 
            this.chkExportForms.AutoSize = true;
            this.chkExportForms.Checked = true;
            this.chkExportForms.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportForms.Location = new System.Drawing.Point(6, 157);
            this.chkExportForms.Name = "chkExportForms";
            this.chkExportForms.Size = new System.Drawing.Size(142, 17);
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
            this.gbGlobalOptions.Location = new System.Drawing.Point(327, 19);
            this.gbGlobalOptions.MinimumSize = new System.Drawing.Size(251, 70);
            this.gbGlobalOptions.Name = "gbGlobalOptions";
            this.gbGlobalOptions.Size = new System.Drawing.Size(555, 98);
            this.gbGlobalOptions.TabIndex = 92;
            this.gbGlobalOptions.TabStop = false;
            this.gbGlobalOptions.Text = "Global Options";
            // 
            // chkExportDashboards
            // 
            this.chkExportDashboards.AutoSize = true;
            this.chkExportDashboards.Checked = true;
            this.chkExportDashboards.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportDashboards.Location = new System.Drawing.Point(6, 63);
            this.chkExportDashboards.Name = "chkExportDashboards";
            this.chkExportDashboards.Size = new System.Drawing.Size(238, 17);
            this.chkExportDashboards.TabIndex = 93;
            this.chkExportDashboards.Text = "Export Dashboards custom labels Translation";
            this.chkExportDashboards.UseVisualStyleBackColor = true;
            // 
            // chkExportSiteMap
            // 
            this.chkExportSiteMap.AutoSize = true;
            this.chkExportSiteMap.Checked = true;
            this.chkExportSiteMap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportSiteMap.Location = new System.Drawing.Point(5, 41);
            this.chkExportSiteMap.Name = "chkExportSiteMap";
            this.chkExportSiteMap.Size = new System.Drawing.Size(220, 17);
            this.chkExportSiteMap.TabIndex = 92;
            this.chkExportSiteMap.Text = "Export SiteMap custom labels Translation";
            this.chkExportSiteMap.UseVisualStyleBackColor = true;
            // 
            // chkExportGlobalOptSet
            // 
            this.chkExportGlobalOptSet.AutoSize = true;
            this.chkExportGlobalOptSet.Checked = true;
            this.chkExportGlobalOptSet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportGlobalOptSet.Location = new System.Drawing.Point(6, 19);
            this.chkExportGlobalOptSet.Name = "chkExportGlobalOptSet";
            this.chkExportGlobalOptSet.Size = new System.Drawing.Size(233, 17);
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
            this.lvEntities.Location = new System.Drawing.Point(7, 48);
            this.lvEntities.Name = "lvEntities";
            this.lvEntities.Size = new System.Drawing.Size(314, 477);
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
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(897, 543);
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
            this.panel2.Location = new System.Drawing.Point(9, 179);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(882, 67);
            this.panel2.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(41, 12);
            this.label3.MinimumSize = new System.Drawing.Size(556, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(556, 53);
            this.label3.TabIndex = 1;
            this.label3.Text = "Please be sure you have a backup of your customizations prior to using this tool." +
    "\r\nIn case of problem, importing this backup solution will restore previous trans" +
    "lations";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(3, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 32);
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
            this.panel1.Location = new System.Drawing.Point(9, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(882, 139);
            this.panel1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(41, 12);
            this.label2.MinimumSize = new System.Drawing.Size(490, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(490, 122);
            this.label2.TabIndex = 1;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnBrowseImportFile
            // 
            this.btnBrowseImportFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseImportFile.Location = new System.Drawing.Point(816, 6);
            this.btnBrowseImportFile.Name = "btnBrowseImportFile";
            this.btnBrowseImportFile.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseImportFile.TabIndex = 2;
            this.btnBrowseImportFile.Text = "...";
            this.btnBrowseImportFile.UseVisualStyleBackColor = true;
            this.btnBrowseImportFile.Click += new System.EventHandler(this.BtnBrowseImportFileClick);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.Location = new System.Drawing.Point(113, 8);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(697, 20);
            this.txtFilePath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Translation file";
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(911, 600);
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
        private System.Windows.Forms.CheckBox chkExportCharts;
    }
}
