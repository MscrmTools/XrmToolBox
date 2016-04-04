namespace MsCrmTools.MetadataBrowser.UserControls
{
    partial class EntityPropertiesControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntityPropertiesControl));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.entityPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.entityToolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbHideEntityPanel = new System.Windows.Forms.ToolStripButton();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.attributesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.attributeListView = new System.Windows.Forms.ListView();
            this.attributePropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.attributeToolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbHideAttributePanel = new System.Windows.Forms.ToolStripButton();
            this.tsbAttributeColumns = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslSearchAttr = new System.Windows.Forms.ToolStripLabel();
            this.tstxtSearchContact = new System.Windows.Forms.ToolStripTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.oneToManySplitContainer = new System.Windows.Forms.SplitContainer();
            this.OneToManyListView = new System.Windows.Forms.ListView();
            this.OneToManyPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.manyToOneToolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbHideOneToManyPanel = new System.Windows.Forms.ToolStripButton();
            this.tsbOneToManyColumns = new System.Windows.Forms.ToolStripButton();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.manyToOneSplitContainer = new System.Windows.Forms.SplitContainer();
            this.manyToOneListView = new System.Windows.Forms.ListView();
            this.manyToOnePropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbHideManyToOnePanel = new System.Windows.Forms.ToolStripButton();
            this.tsbManyToOneColumns = new System.Windows.Forms.ToolStripButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.manyToManySplitContainer = new System.Windows.Forms.SplitContainer();
            this.manyToManyListView = new System.Windows.Forms.ListView();
            this.manyToManyPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.manyToManyToolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbHideManyToManyPanel = new System.Windows.Forms.ToolStripButton();
            this.tsbManyToManyColumns = new System.Windows.Forms.ToolStripButton();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.privilegeSplitContainer = new System.Windows.Forms.SplitContainer();
            this.privilegeListView = new System.Windows.Forms.ListView();
            this.privilegePropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.privilegeToolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbHidePrivilegePanel = new System.Windows.Forms.ToolStripButton();
            this.tsbPrivilegeColumns = new System.Windows.Forms.ToolStripButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.entityToolStrip.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.attributesSplitContainer)).BeginInit();
            this.attributesSplitContainer.Panel1.SuspendLayout();
            this.attributesSplitContainer.Panel2.SuspendLayout();
            this.attributesSplitContainer.SuspendLayout();
            this.attributeToolStrip.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.oneToManySplitContainer)).BeginInit();
            this.oneToManySplitContainer.Panel1.SuspendLayout();
            this.oneToManySplitContainer.Panel2.SuspendLayout();
            this.oneToManySplitContainer.SuspendLayout();
            this.manyToOneToolStrip.SuspendLayout();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.manyToOneSplitContainer)).BeginInit();
            this.manyToOneSplitContainer.Panel1.SuspendLayout();
            this.manyToOneSplitContainer.Panel2.SuspendLayout();
            this.manyToOneSplitContainer.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.manyToManySplitContainer)).BeginInit();
            this.manyToManySplitContainer.Panel1.SuspendLayout();
            this.manyToManySplitContainer.Panel2.SuspendLayout();
            this.manyToManySplitContainer.SuspendLayout();
            this.manyToManyToolStrip.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.privilegeSplitContainer)).BeginInit();
            this.privilegeSplitContainer.Panel1.SuspendLayout();
            this.privilegeSplitContainer.Panel2.SuspendLayout();
            this.privilegeSplitContainer.SuspendLayout();
            this.privilegeToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(632, 525);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.entityPropertyGrid);
            this.tabPage1.Controls.Add(this.entityToolStrip);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(624, 499);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Entity";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // entityPropertyGrid
            // 
            this.entityPropertyGrid.CommandsDisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            this.entityPropertyGrid.CommandsForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            this.entityPropertyGrid.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            this.entityPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entityPropertyGrid.Location = new System.Drawing.Point(2, 27);
            this.entityPropertyGrid.Margin = new System.Windows.Forms.Padding(2);
            this.entityPropertyGrid.Name = "entityPropertyGrid";
            this.entityPropertyGrid.Size = new System.Drawing.Size(620, 470);
            this.entityPropertyGrid.TabIndex = 5;
            this.entityPropertyGrid.ViewForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            // 
            // entityToolStrip
            // 
            this.entityToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbHideEntityPanel});
            this.entityToolStrip.Location = new System.Drawing.Point(2, 2);
            this.entityToolStrip.Name = "entityToolStrip";
            this.entityToolStrip.Size = new System.Drawing.Size(620, 25);
            this.entityToolStrip.TabIndex = 4;
            this.entityToolStrip.Text = "toolStrip2";
            // 
            // tsbHideEntityPanel
            // 
            this.tsbHideEntityPanel.Image = ((System.Drawing.Image)(resources.GetObject("tsbHideEntityPanel.Image")));
            this.tsbHideEntityPanel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHideEntityPanel.Name = "tsbHideEntityPanel";
            this.tsbHideEntityPanel.Size = new System.Drawing.Size(56, 22);
            this.tsbHideEntityPanel.Text = "Close";
            this.tsbHideEntityPanel.Click += new System.EventHandler(this.tsbHidePanel_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.attributesSplitContainer);
            this.tabPage4.Controls.Add(this.attributeToolStrip);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage4.Size = new System.Drawing.Size(624, 499);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Attributes";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // attributesSplitContainer
            // 
            this.attributesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.attributesSplitContainer.Location = new System.Drawing.Point(2, 27);
            this.attributesSplitContainer.Margin = new System.Windows.Forms.Padding(2);
            this.attributesSplitContainer.Name = "attributesSplitContainer";
            // 
            // attributesSplitContainer.Panel1
            // 
            this.attributesSplitContainer.Panel1.Controls.Add(this.attributeListView);
            // 
            // attributesSplitContainer.Panel2
            // 
            this.attributesSplitContainer.Panel2.Controls.Add(this.attributePropertyGrid);
            this.attributesSplitContainer.Size = new System.Drawing.Size(620, 470);
            this.attributesSplitContainer.SplitterDistance = 205;
            this.attributesSplitContainer.SplitterWidth = 3;
            this.attributesSplitContainer.TabIndex = 3;
            // 
            // attributeListView
            // 
            this.attributeListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.attributeListView.FullRowSelect = true;
            this.attributeListView.GridLines = true;
            this.attributeListView.HideSelection = false;
            this.attributeListView.Location = new System.Drawing.Point(0, 0);
            this.attributeListView.Margin = new System.Windows.Forms.Padding(2);
            this.attributeListView.Name = "attributeListView";
            this.attributeListView.Size = new System.Drawing.Size(205, 470);
            this.attributeListView.TabIndex = 1;
            this.attributeListView.UseCompatibleStateImageBehavior = false;
            this.attributeListView.View = System.Windows.Forms.View.Details;
            this.attributeListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            this.attributeListView.DoubleClick += new System.EventHandler(this.attributeListView_DoubleClick);
            // 
            // attributePropertyGrid
            // 
            this.attributePropertyGrid.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            this.attributePropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.attributePropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.attributePropertyGrid.Margin = new System.Windows.Forms.Padding(2);
            this.attributePropertyGrid.Name = "attributePropertyGrid";
            this.attributePropertyGrid.Size = new System.Drawing.Size(412, 470);
            this.attributePropertyGrid.TabIndex = 0;
            this.attributePropertyGrid.ViewForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            // 
            // attributeToolStrip
            // 
            this.attributeToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbHideAttributePanel,
            this.tsbAttributeColumns,
            this.toolStripSeparator1,
            this.tslSearchAttr,
            this.tstxtSearchContact});
            this.attributeToolStrip.Location = new System.Drawing.Point(2, 2);
            this.attributeToolStrip.Name = "attributeToolStrip";
            this.attributeToolStrip.Size = new System.Drawing.Size(620, 25);
            this.attributeToolStrip.TabIndex = 2;
            this.attributeToolStrip.Text = "toolStrip3";
            // 
            // tsbHideAttributePanel
            // 
            this.tsbHideAttributePanel.Image = ((System.Drawing.Image)(resources.GetObject("tsbHideAttributePanel.Image")));
            this.tsbHideAttributePanel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHideAttributePanel.Name = "tsbHideAttributePanel";
            this.tsbHideAttributePanel.Size = new System.Drawing.Size(84, 22);
            this.tsbHideAttributePanel.Text = "Hide panel";
            this.tsbHideAttributePanel.Visible = false;
            this.tsbHideAttributePanel.Click += new System.EventHandler(this.tsbHidePanel_Click);
            // 
            // tsbAttributeColumns
            // 
            this.tsbAttributeColumns.Image = ((System.Drawing.Image)(resources.GetObject("tsbAttributeColumns.Image")));
            this.tsbAttributeColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAttributeColumns.Name = "tsbAttributeColumns";
            this.tsbAttributeColumns.Size = new System.Drawing.Size(84, 22);
            this.tsbAttributeColumns.Text = "Columns...";
            this.tsbAttributeColumns.Click += new System.EventHandler(this.tsbColumns_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tslSearchAttr
            // 
            this.tslSearchAttr.Name = "tslSearchAttr";
            this.tslSearchAttr.Size = new System.Drawing.Size(42, 22);
            this.tslSearchAttr.Text = "Search";
            // 
            // tstxtSearchContact
            // 
            this.tstxtSearchContact.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.tstxtSearchContact.Name = "tstxtSearchContact";
            this.tstxtSearchContact.Size = new System.Drawing.Size(200, 25);
            this.tstxtSearchContact.Text = "by logical and displayname";
            this.tstxtSearchContact.Enter += new System.EventHandler(this.tstxtSearch_Enter);
            this.tstxtSearchContact.TextChanged += new System.EventHandler(this.tstxtSearch_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.oneToManySplitContainer);
            this.tabPage2.Controls.Add(this.manyToOneToolStrip);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(624, 499);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "OneToManyRelationships";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // oneToManySplitContainer
            // 
            this.oneToManySplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneToManySplitContainer.Location = new System.Drawing.Point(2, 27);
            this.oneToManySplitContainer.Margin = new System.Windows.Forms.Padding(2);
            this.oneToManySplitContainer.Name = "oneToManySplitContainer";
            // 
            // oneToManySplitContainer.Panel1
            // 
            this.oneToManySplitContainer.Panel1.Controls.Add(this.OneToManyListView);
            // 
            // oneToManySplitContainer.Panel2
            // 
            this.oneToManySplitContainer.Panel2.Controls.Add(this.OneToManyPropertyGrid);
            this.oneToManySplitContainer.Size = new System.Drawing.Size(620, 470);
            this.oneToManySplitContainer.SplitterDistance = 205;
            this.oneToManySplitContainer.SplitterWidth = 3;
            this.oneToManySplitContainer.TabIndex = 4;
            // 
            // OneToManyListView
            // 
            this.OneToManyListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OneToManyListView.FullRowSelect = true;
            this.OneToManyListView.GridLines = true;
            this.OneToManyListView.HideSelection = false;
            this.OneToManyListView.Location = new System.Drawing.Point(0, 0);
            this.OneToManyListView.Margin = new System.Windows.Forms.Padding(2);
            this.OneToManyListView.Name = "OneToManyListView";
            this.OneToManyListView.Size = new System.Drawing.Size(205, 470);
            this.OneToManyListView.TabIndex = 1;
            this.OneToManyListView.UseCompatibleStateImageBehavior = false;
            this.OneToManyListView.View = System.Windows.Forms.View.Details;
            this.OneToManyListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            this.OneToManyListView.DoubleClick += new System.EventHandler(this.OneToManyListView_DoubleClick);
            // 
            // OneToManyPropertyGrid
            // 
            this.OneToManyPropertyGrid.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            this.OneToManyPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OneToManyPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.OneToManyPropertyGrid.Margin = new System.Windows.Forms.Padding(2);
            this.OneToManyPropertyGrid.Name = "OneToManyPropertyGrid";
            this.OneToManyPropertyGrid.Size = new System.Drawing.Size(412, 470);
            this.OneToManyPropertyGrid.TabIndex = 0;
            this.OneToManyPropertyGrid.ViewForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            // 
            // manyToOneToolStrip
            // 
            this.manyToOneToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbHideOneToManyPanel,
            this.tsbOneToManyColumns});
            this.manyToOneToolStrip.Location = new System.Drawing.Point(2, 2);
            this.manyToOneToolStrip.Name = "manyToOneToolStrip";
            this.manyToOneToolStrip.Size = new System.Drawing.Size(620, 25);
            this.manyToOneToolStrip.TabIndex = 3;
            this.manyToOneToolStrip.Text = "toolStrip3";
            // 
            // tsbHideOneToManyPanel
            // 
            this.tsbHideOneToManyPanel.Image = ((System.Drawing.Image)(resources.GetObject("tsbHideOneToManyPanel.Image")));
            this.tsbHideOneToManyPanel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHideOneToManyPanel.Name = "tsbHideOneToManyPanel";
            this.tsbHideOneToManyPanel.Size = new System.Drawing.Size(84, 22);
            this.tsbHideOneToManyPanel.Text = "Hide panel";
            this.tsbHideOneToManyPanel.Visible = false;
            this.tsbHideOneToManyPanel.Click += new System.EventHandler(this.tsbHidePanel_Click);
            // 
            // tsbOneToManyColumns
            // 
            this.tsbOneToManyColumns.Image = ((System.Drawing.Image)(resources.GetObject("tsbOneToManyColumns.Image")));
            this.tsbOneToManyColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOneToManyColumns.Name = "tsbOneToManyColumns";
            this.tsbOneToManyColumns.Size = new System.Drawing.Size(84, 22);
            this.tsbOneToManyColumns.Text = "Columns...";
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.manyToOneSplitContainer);
            this.tabPage6.Controls.Add(this.toolStrip2);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage6.Size = new System.Drawing.Size(624, 499);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "ManyToOneRelationships";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // manyToOneSplitContainer
            // 
            this.manyToOneSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.manyToOneSplitContainer.Location = new System.Drawing.Point(2, 27);
            this.manyToOneSplitContainer.Margin = new System.Windows.Forms.Padding(2);
            this.manyToOneSplitContainer.Name = "manyToOneSplitContainer";
            // 
            // manyToOneSplitContainer.Panel1
            // 
            this.manyToOneSplitContainer.Panel1.Controls.Add(this.manyToOneListView);
            // 
            // manyToOneSplitContainer.Panel2
            // 
            this.manyToOneSplitContainer.Panel2.Controls.Add(this.manyToOnePropertyGrid);
            this.manyToOneSplitContainer.Size = new System.Drawing.Size(620, 470);
            this.manyToOneSplitContainer.SplitterDistance = 205;
            this.manyToOneSplitContainer.SplitterWidth = 3;
            this.manyToOneSplitContainer.TabIndex = 6;
            // 
            // manyToOneListView
            // 
            this.manyToOneListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.manyToOneListView.FullRowSelect = true;
            this.manyToOneListView.GridLines = true;
            this.manyToOneListView.HideSelection = false;
            this.manyToOneListView.Location = new System.Drawing.Point(0, 0);
            this.manyToOneListView.Margin = new System.Windows.Forms.Padding(2);
            this.manyToOneListView.Name = "manyToOneListView";
            this.manyToOneListView.Size = new System.Drawing.Size(205, 470);
            this.manyToOneListView.TabIndex = 1;
            this.manyToOneListView.UseCompatibleStateImageBehavior = false;
            this.manyToOneListView.View = System.Windows.Forms.View.Details;
            this.manyToOneListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            this.manyToOneListView.DoubleClick += new System.EventHandler(this.manyToOneListView_DoubleClick);
            // 
            // manyToOnePropertyGrid
            // 
            this.manyToOnePropertyGrid.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            this.manyToOnePropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.manyToOnePropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.manyToOnePropertyGrid.Margin = new System.Windows.Forms.Padding(2);
            this.manyToOnePropertyGrid.Name = "manyToOnePropertyGrid";
            this.manyToOnePropertyGrid.Size = new System.Drawing.Size(412, 470);
            this.manyToOnePropertyGrid.TabIndex = 0;
            this.manyToOnePropertyGrid.ViewForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbHideManyToOnePanel,
            this.tsbManyToOneColumns});
            this.toolStrip2.Location = new System.Drawing.Point(2, 2);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(620, 25);
            this.toolStrip2.TabIndex = 5;
            this.toolStrip2.Text = "toolStrip3";
            // 
            // tsbHideManyToOnePanel
            // 
            this.tsbHideManyToOnePanel.Image = ((System.Drawing.Image)(resources.GetObject("tsbHideManyToOnePanel.Image")));
            this.tsbHideManyToOnePanel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHideManyToOnePanel.Name = "tsbHideManyToOnePanel";
            this.tsbHideManyToOnePanel.Size = new System.Drawing.Size(84, 22);
            this.tsbHideManyToOnePanel.Text = "Hide panel";
            this.tsbHideManyToOnePanel.Visible = false;
            this.tsbHideManyToOnePanel.Click += new System.EventHandler(this.tsbHidePanel_Click);
            // 
            // tsbManyToOneColumns
            // 
            this.tsbManyToOneColumns.Image = ((System.Drawing.Image)(resources.GetObject("tsbManyToOneColumns.Image")));
            this.tsbManyToOneColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbManyToOneColumns.Name = "tsbManyToOneColumns";
            this.tsbManyToOneColumns.Size = new System.Drawing.Size(84, 22);
            this.tsbManyToOneColumns.Text = "Columns...";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.manyToManySplitContainer);
            this.tabPage3.Controls.Add(this.manyToManyToolStrip);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage3.Size = new System.Drawing.Size(624, 499);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "ManyToManyRelationships";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // manyToManySplitContainer
            // 
            this.manyToManySplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.manyToManySplitContainer.Location = new System.Drawing.Point(2, 27);
            this.manyToManySplitContainer.Margin = new System.Windows.Forms.Padding(2);
            this.manyToManySplitContainer.Name = "manyToManySplitContainer";
            // 
            // manyToManySplitContainer.Panel1
            // 
            this.manyToManySplitContainer.Panel1.Controls.Add(this.manyToManyListView);
            // 
            // manyToManySplitContainer.Panel2
            // 
            this.manyToManySplitContainer.Panel2.Controls.Add(this.manyToManyPropertyGrid);
            this.manyToManySplitContainer.Size = new System.Drawing.Size(620, 470);
            this.manyToManySplitContainer.SplitterDistance = 205;
            this.manyToManySplitContainer.SplitterWidth = 3;
            this.manyToManySplitContainer.TabIndex = 4;
            // 
            // manyToManyListView
            // 
            this.manyToManyListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.manyToManyListView.FullRowSelect = true;
            this.manyToManyListView.GridLines = true;
            this.manyToManyListView.HideSelection = false;
            this.manyToManyListView.Location = new System.Drawing.Point(0, 0);
            this.manyToManyListView.Margin = new System.Windows.Forms.Padding(2);
            this.manyToManyListView.Name = "manyToManyListView";
            this.manyToManyListView.Size = new System.Drawing.Size(205, 470);
            this.manyToManyListView.TabIndex = 1;
            this.manyToManyListView.UseCompatibleStateImageBehavior = false;
            this.manyToManyListView.View = System.Windows.Forms.View.Details;
            this.manyToManyListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            this.manyToManyListView.DoubleClick += new System.EventHandler(this.manyToManyListView_DoubleClick);
            // 
            // manyToManyPropertyGrid
            // 
            this.manyToManyPropertyGrid.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            this.manyToManyPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.manyToManyPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.manyToManyPropertyGrid.Margin = new System.Windows.Forms.Padding(2);
            this.manyToManyPropertyGrid.Name = "manyToManyPropertyGrid";
            this.manyToManyPropertyGrid.Size = new System.Drawing.Size(412, 470);
            this.manyToManyPropertyGrid.TabIndex = 0;
            this.manyToManyPropertyGrid.ViewForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            // 
            // manyToManyToolStrip
            // 
            this.manyToManyToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbHideManyToManyPanel,
            this.tsbManyToManyColumns});
            this.manyToManyToolStrip.Location = new System.Drawing.Point(2, 2);
            this.manyToManyToolStrip.Name = "manyToManyToolStrip";
            this.manyToManyToolStrip.Size = new System.Drawing.Size(620, 25);
            this.manyToManyToolStrip.TabIndex = 3;
            this.manyToManyToolStrip.Text = "toolStrip3";
            // 
            // tsbHideManyToManyPanel
            // 
            this.tsbHideManyToManyPanel.Image = ((System.Drawing.Image)(resources.GetObject("tsbHideManyToManyPanel.Image")));
            this.tsbHideManyToManyPanel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHideManyToManyPanel.Name = "tsbHideManyToManyPanel";
            this.tsbHideManyToManyPanel.Size = new System.Drawing.Size(84, 22);
            this.tsbHideManyToManyPanel.Text = "Hide panel";
            this.tsbHideManyToManyPanel.Visible = false;
            this.tsbHideManyToManyPanel.Click += new System.EventHandler(this.tsbHidePanel_Click);
            // 
            // tsbManyToManyColumns
            // 
            this.tsbManyToManyColumns.Image = ((System.Drawing.Image)(resources.GetObject("tsbManyToManyColumns.Image")));
            this.tsbManyToManyColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbManyToManyColumns.Name = "tsbManyToManyColumns";
            this.tsbManyToManyColumns.Size = new System.Drawing.Size(84, 22);
            this.tsbManyToManyColumns.Text = "Columns...";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.privilegeSplitContainer);
            this.tabPage5.Controls.Add(this.privilegeToolStrip);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage5.Size = new System.Drawing.Size(624, 499);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Privileges";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // privilegeSplitContainer
            // 
            this.privilegeSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.privilegeSplitContainer.Location = new System.Drawing.Point(2, 27);
            this.privilegeSplitContainer.Margin = new System.Windows.Forms.Padding(2);
            this.privilegeSplitContainer.Name = "privilegeSplitContainer";
            // 
            // privilegeSplitContainer.Panel1
            // 
            this.privilegeSplitContainer.Panel1.Controls.Add(this.privilegeListView);
            // 
            // privilegeSplitContainer.Panel2
            // 
            this.privilegeSplitContainer.Panel2.Controls.Add(this.privilegePropertyGrid);
            this.privilegeSplitContainer.Size = new System.Drawing.Size(620, 470);
            this.privilegeSplitContainer.SplitterDistance = 205;
            this.privilegeSplitContainer.SplitterWidth = 3;
            this.privilegeSplitContainer.TabIndex = 4;
            // 
            // privilegeListView
            // 
            this.privilegeListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.privilegeListView.FullRowSelect = true;
            this.privilegeListView.GridLines = true;
            this.privilegeListView.HideSelection = false;
            this.privilegeListView.Location = new System.Drawing.Point(0, 0);
            this.privilegeListView.Margin = new System.Windows.Forms.Padding(2);
            this.privilegeListView.Name = "privilegeListView";
            this.privilegeListView.Size = new System.Drawing.Size(205, 470);
            this.privilegeListView.TabIndex = 1;
            this.privilegeListView.UseCompatibleStateImageBehavior = false;
            this.privilegeListView.View = System.Windows.Forms.View.Details;
            this.privilegeListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            this.privilegeListView.DoubleClick += new System.EventHandler(this.privilegeListView_DoubleClick);
            // 
            // privilegePropertyGrid
            // 
            this.privilegePropertyGrid.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            this.privilegePropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.privilegePropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.privilegePropertyGrid.Margin = new System.Windows.Forms.Padding(2);
            this.privilegePropertyGrid.Name = "privilegePropertyGrid";
            this.privilegePropertyGrid.Size = new System.Drawing.Size(412, 470);
            this.privilegePropertyGrid.TabIndex = 0;
            this.privilegePropertyGrid.ViewForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            // 
            // privilegeToolStrip
            // 
            this.privilegeToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbHidePrivilegePanel,
            this.tsbPrivilegeColumns});
            this.privilegeToolStrip.Location = new System.Drawing.Point(2, 2);
            this.privilegeToolStrip.Name = "privilegeToolStrip";
            this.privilegeToolStrip.Size = new System.Drawing.Size(620, 25);
            this.privilegeToolStrip.TabIndex = 3;
            this.privilegeToolStrip.Text = "toolStrip3";
            // 
            // tsbHidePrivilegePanel
            // 
            this.tsbHidePrivilegePanel.Image = ((System.Drawing.Image)(resources.GetObject("tsbHidePrivilegePanel.Image")));
            this.tsbHidePrivilegePanel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHidePrivilegePanel.Name = "tsbHidePrivilegePanel";
            this.tsbHidePrivilegePanel.Size = new System.Drawing.Size(84, 22);
            this.tsbHidePrivilegePanel.Text = "Hide panel";
            this.tsbHidePrivilegePanel.Visible = false;
            this.tsbHidePrivilegePanel.Click += new System.EventHandler(this.tsbHidePanel_Click);
            // 
            // tsbPrivilegeColumns
            // 
            this.tsbPrivilegeColumns.Image = ((System.Drawing.Image)(resources.GetObject("tsbPrivilegeColumns.Image")));
            this.tsbPrivilegeColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrivilegeColumns.Name = "tsbPrivilegeColumns";
            this.tsbPrivilegeColumns.Size = new System.Drawing.Size(84, 22);
            this.tsbPrivilegeColumns.Text = "Columns...";
            // 
            // EntityPropertiesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "EntityPropertiesControl";
            this.Size = new System.Drawing.Size(632, 525);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.entityToolStrip.ResumeLayout(false);
            this.entityToolStrip.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.attributesSplitContainer.Panel1.ResumeLayout(false);
            this.attributesSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.attributesSplitContainer)).EndInit();
            this.attributesSplitContainer.ResumeLayout(false);
            this.attributeToolStrip.ResumeLayout(false);
            this.attributeToolStrip.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.oneToManySplitContainer.Panel1.ResumeLayout(false);
            this.oneToManySplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.oneToManySplitContainer)).EndInit();
            this.oneToManySplitContainer.ResumeLayout(false);
            this.manyToOneToolStrip.ResumeLayout(false);
            this.manyToOneToolStrip.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.manyToOneSplitContainer.Panel1.ResumeLayout(false);
            this.manyToOneSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.manyToOneSplitContainer)).EndInit();
            this.manyToOneSplitContainer.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.manyToManySplitContainer.Panel1.ResumeLayout(false);
            this.manyToManySplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.manyToManySplitContainer)).EndInit();
            this.manyToManySplitContainer.ResumeLayout(false);
            this.manyToManyToolStrip.ResumeLayout(false);
            this.manyToManyToolStrip.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.privilegeSplitContainer.Panel1.ResumeLayout(false);
            this.privilegeSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.privilegeSplitContainer)).EndInit();
            this.privilegeSplitContainer.ResumeLayout(false);
            this.privilegeToolStrip.ResumeLayout(false);
            this.privilegeToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.PropertyGrid entityPropertyGrid;
        private System.Windows.Forms.ToolStrip entityToolStrip;
        private System.Windows.Forms.ToolStripButton tsbHideEntityPanel;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.SplitContainer attributesSplitContainer;
        private System.Windows.Forms.ListView attributeListView;
        private System.Windows.Forms.PropertyGrid attributePropertyGrid;
        private System.Windows.Forms.ToolStrip attributeToolStrip;
        private System.Windows.Forms.ToolStripButton tsbHideAttributePanel;
        private System.Windows.Forms.ToolStripButton tsbAttributeColumns;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer oneToManySplitContainer;
        private System.Windows.Forms.ListView OneToManyListView;
        private System.Windows.Forms.PropertyGrid OneToManyPropertyGrid;
        private System.Windows.Forms.ToolStrip manyToOneToolStrip;
        private System.Windows.Forms.ToolStripButton tsbHideOneToManyPanel;
        private System.Windows.Forms.ToolStripButton tsbOneToManyColumns;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.SplitContainer manyToOneSplitContainer;
        private System.Windows.Forms.ListView manyToOneListView;
        private System.Windows.Forms.PropertyGrid manyToOnePropertyGrid;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbHideManyToOnePanel;
        private System.Windows.Forms.ToolStripButton tsbManyToOneColumns;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.SplitContainer manyToManySplitContainer;
        private System.Windows.Forms.ListView manyToManyListView;
        private System.Windows.Forms.PropertyGrid manyToManyPropertyGrid;
        private System.Windows.Forms.ToolStrip manyToManyToolStrip;
        private System.Windows.Forms.ToolStripButton tsbHideManyToManyPanel;
        private System.Windows.Forms.ToolStripButton tsbManyToManyColumns;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.SplitContainer privilegeSplitContainer;
        private System.Windows.Forms.ListView privilegeListView;
        private System.Windows.Forms.PropertyGrid privilegePropertyGrid;
        private System.Windows.Forms.ToolStrip privilegeToolStrip;
        private System.Windows.Forms.ToolStripButton tsbHidePrivilegePanel;
        private System.Windows.Forms.ToolStripButton tsbPrivilegeColumns;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tslSearchAttr;
        private System.Windows.Forms.ToolStripTextBox tstxtSearchContact;
    }
}
