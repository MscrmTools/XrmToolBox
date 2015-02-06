namespace MscrmTools.SyncFilterManager
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainControl));
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.toolImageList = new System.Windows.Forms.ImageList(this.components);
            this.tabCtrl = new System.Windows.Forms.TabControl();
            this.tabPageSystemFilters = new System.Windows.Forms.TabPage();
            this.systemRulesListView = new MscrmTools.SyncFilterManager.Controls.CrmSystemViewList();
            this.toolStrip5 = new System.Windows.Forms.ToolStrip();
            this.tsbLoadSystemSynchronizationFilter = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSystemFilterProperties = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSystemRuleDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbShowFetchXmlSystemRules = new System.Windows.Forms.ToolStripButton();
            this.tabPageDefaultRules = new System.Windows.Forms.TabPage();
            this.defaultLocalDataRulesView = new MscrmTools.SyncFilterManager.Controls.CrmSystemViewList();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.tsbLoadDefaultLocalDataRules = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbTemplateFilterProperties = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDefaultDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbDefineAsDefault = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbApplyToUsers = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbShowFetchXmlDefault = new System.Windows.Forms.ToolStripButton();
            this.tabPageUsersRules = new System.Windows.Forms.TabPage();
            this.usersLocalDataRulesView = new MscrmTools.SyncFilterManager.Controls.CrmSystemViewList();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkDisplayOfflineFilters = new System.Windows.Forms.CheckBox();
            this.chkDisplayOutlookFilters = new System.Windows.Forms.CheckBox();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.tsddbLoadUsersLocalDataRules = new System.Windows.Forms.ToolStripDropDownButton();
            this.forSpecificUsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.loadAllUsersLocalDataRulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbUserRuleEnable = new System.Windows.Forms.ToolStripButton();
            this.tsbUserRuleDisable = new System.Windows.Forms.ToolStripButton();
            this.tsbDeleteUserRule = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbShowFetchXmlUser = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsddbGroupBy = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiGroupByRule = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGroupByReturnedType = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGroupByUser = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageSystemViews = new System.Windows.Forms.TabPage();
            this.crmSystemViewsList = new MscrmTools.SyncFilterManager.Controls.CrmSystemViewList();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbLoadSystemViews = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbShowFetchXmlView = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiCreateSystemFilterFromView = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCreateFilterTemplateFromView = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiUpdateSystemFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpdateFilterTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageUsers = new System.Windows.Forms.TabPage();
            this.crmUserList1 = new MscrmTools.SyncFilterManager.Controls.CrmUserList();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbResetUsersFiltersToDefault = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCopyUserFiltersToUser = new System.Windows.Forms.ToolStripButton();
            this.toolStripMenu.SuspendLayout();
            this.tabCtrl.SuspendLayout();
            this.tabPageSystemFilters.SuspendLayout();
            this.toolStrip5.SuspendLayout();
            this.tabPageDefaultRules.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            this.tabPageUsersRules.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.tabPageSystemViews.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tabPageUsers.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripMenu.Size = new System.Drawing.Size(1366, 25);
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
            // toolImageList
            // 
            this.toolImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("toolImageList.ImageStream")));
            this.toolImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.toolImageList.Images.SetKeyName(0, "nologo.png");
            // 
            // tabCtrl
            // 
            this.tabCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCtrl.Controls.Add(this.tabPageSystemFilters);
            this.tabCtrl.Controls.Add(this.tabPageDefaultRules);
            this.tabCtrl.Controls.Add(this.tabPageUsersRules);
            this.tabCtrl.Controls.Add(this.tabPageSystemViews);
            this.tabCtrl.Controls.Add(this.tabPageUsers);
            this.tabCtrl.Location = new System.Drawing.Point(4, 43);
            this.tabCtrl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabCtrl.Name = "tabCtrl";
            this.tabCtrl.SelectedIndex = 0;
            this.tabCtrl.Size = new System.Drawing.Size(1358, 875);
            this.tabCtrl.TabIndex = 3;
            // 
            // tabPageSystemFilters
            // 
            this.tabPageSystemFilters.Controls.Add(this.systemRulesListView);
            this.tabPageSystemFilters.Controls.Add(this.toolStrip5);
            this.tabPageSystemFilters.Location = new System.Drawing.Point(4, 29);
            this.tabPageSystemFilters.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageSystemFilters.Name = "tabPageSystemFilters";
            this.tabPageSystemFilters.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageSystemFilters.Size = new System.Drawing.Size(1350, 842);
            this.tabPageSystemFilters.TabIndex = 4;
            this.tabPageSystemFilters.Text = "System Synchronization Filters";
            this.tabPageSystemFilters.UseVisualStyleBackColor = true;
            // 
            // systemRulesListView
            // 
            this.systemRulesListView.DisplayOfflineFilter = true;
            this.systemRulesListView.DisplayOutlookFilter = true;
            this.systemRulesListView.DisplayRulesTemplate = false;
            this.systemRulesListView.DisplaySystemRules = true;
            this.systemRulesListView.DisplaySystemView = false;
            this.systemRulesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.systemRulesListView.EntityName = "savedquery";
            this.systemRulesListView.Location = new System.Drawing.Point(4, 37);
            this.systemRulesListView.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.systemRulesListView.Name = "systemRulesListView";
            this.systemRulesListView.Size = new System.Drawing.Size(1342, 800);
            this.systemRulesListView.TabIndex = 2;
            // 
            // toolStrip5
            // 
            this.toolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbLoadSystemSynchronizationFilter,
            this.toolStripSeparator11,
            this.tsbSystemFilterProperties,
            this.toolStripSeparator12,
            this.tsbSystemRuleDelete,
            this.toolStripSeparator10,
            this.tsbShowFetchXmlSystemRules});
            this.toolStrip5.Location = new System.Drawing.Point(4, 5);
            this.toolStrip5.Name = "toolStrip5";
            this.toolStrip5.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip5.Size = new System.Drawing.Size(1342, 32);
            this.toolStrip5.TabIndex = 0;
            this.toolStrip5.Text = "toolStrip5";
            // 
            // tsbLoadSystemSynchronizationFilter
            // 
            this.tsbLoadSystemSynchronizationFilter.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadSystemSynchronizationFilter.Image")));
            this.tsbLoadSystemSynchronizationFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadSystemSynchronizationFilter.Name = "tsbLoadSystemSynchronizationFilter";
            this.tsbLoadSystemSynchronizationFilter.Size = new System.Drawing.Size(316, 29);
            this.tsbLoadSystemSynchronizationFilter.Text = "Load System Synchronization Filters";
            this.tsbLoadSystemSynchronizationFilter.ToolTipText = "Load templates used to synchronize Outlook and Offline items";
            this.tsbLoadSystemSynchronizationFilter.Click += new System.EventHandler(this.tsbLoadSystemSynchronizationFilter_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbSystemFilterProperties
            // 
            this.tsbSystemFilterProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSystemFilterProperties.Image = ((System.Drawing.Image)(resources.GetObject("tsbSystemFilterProperties.Image")));
            this.tsbSystemFilterProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSystemFilterProperties.Name = "tsbSystemFilterProperties";
            this.tsbSystemFilterProperties.Size = new System.Drawing.Size(23, 29);
            this.tsbSystemFilterProperties.Text = "Properties";
            this.tsbSystemFilterProperties.Click += new System.EventHandler(this.tsbSystemFilterProperties_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbSystemRuleDelete
            // 
            this.tsbSystemRuleDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbSystemRuleDelete.Image")));
            this.tsbSystemRuleDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSystemRuleDelete.Name = "tsbSystemRuleDelete";
            this.tsbSystemRuleDelete.Size = new System.Drawing.Size(82, 29);
            this.tsbSystemRuleDelete.Text = "Delete";
            this.tsbSystemRuleDelete.ToolTipText = "Delete selected default local data rule(s)";
            this.tsbSystemRuleDelete.Click += new System.EventHandler(this.tsbSystemRuleDelete_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbShowFetchXmlSystemRules
            // 
            this.tsbShowFetchXmlSystemRules.Image = ((System.Drawing.Image)(resources.GetObject("tsbShowFetchXmlSystemRules.Image")));
            this.tsbShowFetchXmlSystemRules.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowFetchXmlSystemRules.Name = "tsbShowFetchXmlSystemRules";
            this.tsbShowFetchXmlSystemRules.Size = new System.Drawing.Size(154, 29);
            this.tsbShowFetchXmlSystemRules.Text = "Show FetchXml";
            this.tsbShowFetchXmlSystemRules.ToolTipText = "Display FetchXml query used for the selected rule";
            this.tsbShowFetchXmlSystemRules.Click += new System.EventHandler(this.tsbShowFetchXmlSystemRules_Click);
            // 
            // tabPageDefaultRules
            // 
            this.tabPageDefaultRules.Controls.Add(this.defaultLocalDataRulesView);
            this.tabPageDefaultRules.Controls.Add(this.toolStrip4);
            this.tabPageDefaultRules.Location = new System.Drawing.Point(4, 29);
            this.tabPageDefaultRules.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageDefaultRules.Name = "tabPageDefaultRules";
            this.tabPageDefaultRules.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageDefaultRules.Size = new System.Drawing.Size(1350, 842);
            this.tabPageDefaultRules.TabIndex = 0;
            this.tabPageDefaultRules.Text = "Synchronization Filters Templates";
            this.tabPageDefaultRules.UseVisualStyleBackColor = true;
            // 
            // defaultLocalDataRulesView
            // 
            this.defaultLocalDataRulesView.DisplayOfflineFilter = false;
            this.defaultLocalDataRulesView.DisplayOutlookFilter = false;
            this.defaultLocalDataRulesView.DisplayRulesTemplate = true;
            this.defaultLocalDataRulesView.DisplaySystemRules = false;
            this.defaultLocalDataRulesView.DisplaySystemView = false;
            this.defaultLocalDataRulesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.defaultLocalDataRulesView.EntityName = "savedquery";
            this.defaultLocalDataRulesView.Location = new System.Drawing.Point(4, 37);
            this.defaultLocalDataRulesView.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.defaultLocalDataRulesView.Name = "defaultLocalDataRulesView";
            this.defaultLocalDataRulesView.Size = new System.Drawing.Size(1342, 800);
            this.defaultLocalDataRulesView.TabIndex = 7;
            // 
            // toolStrip4
            // 
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbLoadDefaultLocalDataRules,
            this.toolStripSeparator3,
            this.tsbTemplateFilterProperties,
            this.toolStripSeparator13,
            this.tsbDefaultDelete,
            this.tsbDefineAsDefault,
            this.toolStripSeparator5,
            this.tsbApplyToUsers,
            this.toolStripSeparator7,
            this.tsbShowFetchXmlDefault});
            this.toolStrip4.Location = new System.Drawing.Point(4, 5);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip4.Size = new System.Drawing.Size(1342, 32);
            this.toolStrip4.TabIndex = 6;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // tsbLoadDefaultLocalDataRules
            // 
            this.tsbLoadDefaultLocalDataRules.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadDefaultLocalDataRules.Image")));
            this.tsbLoadDefaultLocalDataRules.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadDefaultLocalDataRules.Name = "tsbLoadDefaultLocalDataRules";
            this.tsbLoadDefaultLocalDataRules.Size = new System.Drawing.Size(331, 29);
            this.tsbLoadDefaultLocalDataRules.Text = "Load Synchronization Filter Templates";
            this.tsbLoadDefaultLocalDataRules.ToolTipText = "Load templates used to synchronize Outlook and Offline items";
            this.tsbLoadDefaultLocalDataRules.Click += new System.EventHandler(this.tsbLoadDefaultLocalDataRules_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbTemplateFilterProperties
            // 
            this.tsbTemplateFilterProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTemplateFilterProperties.Image = ((System.Drawing.Image)(resources.GetObject("tsbTemplateFilterProperties.Image")));
            this.tsbTemplateFilterProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTemplateFilterProperties.Name = "tsbTemplateFilterProperties";
            this.tsbTemplateFilterProperties.Size = new System.Drawing.Size(23, 29);
            this.tsbTemplateFilterProperties.Text = "Properties";
            this.tsbTemplateFilterProperties.Click += new System.EventHandler(this.tsbTemplateFilterProperties_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbDefaultDelete
            // 
            this.tsbDefaultDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbDefaultDelete.Image")));
            this.tsbDefaultDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDefaultDelete.Name = "tsbDefaultDelete";
            this.tsbDefaultDelete.Size = new System.Drawing.Size(82, 29);
            this.tsbDefaultDelete.Text = "Delete";
            this.tsbDefaultDelete.ToolTipText = "Delete selected default local data rule(s)";
            this.tsbDefaultDelete.Click += new System.EventHandler(this.tsbDefaultDelete_Click);
            // 
            // tsbDefineAsDefault
            // 
            this.tsbDefineAsDefault.Image = ((System.Drawing.Image)(resources.GetObject("tsbDefineAsDefault.Image")));
            this.tsbDefineAsDefault.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDefineAsDefault.Name = "tsbDefineAsDefault";
            this.tsbDefineAsDefault.Size = new System.Drawing.Size(165, 29);
            this.tsbDefineAsDefault.Text = "Define as default";
            this.tsbDefineAsDefault.Click += new System.EventHandler(this.tsbDefineAsDefault_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbApplyToUsers
            // 
            this.tsbApplyToUsers.Image = ((System.Drawing.Image)(resources.GetObject("tsbApplyToUsers.Image")));
            this.tsbApplyToUsers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbApplyToUsers.Name = "tsbApplyToUsers";
            this.tsbApplyToUsers.Size = new System.Drawing.Size(147, 29);
            this.tsbApplyToUsers.Text = "Apply to users";
            this.tsbApplyToUsers.ToolTipText = "Select one or more users. Individual synchronization rules of these users will be" +
    " created or overriden by selected rules";
            this.tsbApplyToUsers.Click += new System.EventHandler(this.tsbApplyToUsers_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbShowFetchXmlDefault
            // 
            this.tsbShowFetchXmlDefault.Image = ((System.Drawing.Image)(resources.GetObject("tsbShowFetchXmlDefault.Image")));
            this.tsbShowFetchXmlDefault.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowFetchXmlDefault.Name = "tsbShowFetchXmlDefault";
            this.tsbShowFetchXmlDefault.Size = new System.Drawing.Size(154, 29);
            this.tsbShowFetchXmlDefault.Text = "Show FetchXml";
            this.tsbShowFetchXmlDefault.ToolTipText = "Display FetchXml query used for the selected rule";
            this.tsbShowFetchXmlDefault.Click += new System.EventHandler(this.tsbShowFetchXmlDefault_Click);
            // 
            // tabPageUsersRules
            // 
            this.tabPageUsersRules.Controls.Add(this.usersLocalDataRulesView);
            this.tabPageUsersRules.Controls.Add(this.panel1);
            this.tabPageUsersRules.Controls.Add(this.toolStrip3);
            this.tabPageUsersRules.Location = new System.Drawing.Point(4, 29);
            this.tabPageUsersRules.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageUsersRules.Name = "tabPageUsersRules";
            this.tabPageUsersRules.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageUsersRules.Size = new System.Drawing.Size(1350, 842);
            this.tabPageUsersRules.TabIndex = 1;
            this.tabPageUsersRules.Text = "Users Synchronization Filters";
            this.tabPageUsersRules.UseVisualStyleBackColor = true;
            // 
            // usersLocalDataRulesView
            // 
            this.usersLocalDataRulesView.DisplayOfflineFilter = false;
            this.usersLocalDataRulesView.DisplayOutlookFilter = false;
            this.usersLocalDataRulesView.DisplayRulesTemplate = false;
            this.usersLocalDataRulesView.DisplaySystemRules = false;
            this.usersLocalDataRulesView.DisplaySystemView = false;
            this.usersLocalDataRulesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usersLocalDataRulesView.EntityName = "userquery";
            this.usersLocalDataRulesView.Location = new System.Drawing.Point(4, 73);
            this.usersLocalDataRulesView.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.usersLocalDataRulesView.Name = "usersLocalDataRulesView";
            this.usersLocalDataRulesView.Size = new System.Drawing.Size(1342, 764);
            this.usersLocalDataRulesView.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkDisplayOfflineFilters);
            this.panel1.Controls.Add(this.chkDisplayOutlookFilters);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 37);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1342, 36);
            this.panel1.TabIndex = 11;
            // 
            // chkDisplayOfflineFilters
            // 
            this.chkDisplayOfflineFilters.AutoSize = true;
            this.chkDisplayOfflineFilters.Checked = true;
            this.chkDisplayOfflineFilters.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisplayOfflineFilters.Location = new System.Drawing.Point(206, 5);
            this.chkDisplayOfflineFilters.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkDisplayOfflineFilters.Name = "chkDisplayOfflineFilters";
            this.chkDisplayOfflineFilters.Size = new System.Drawing.Size(183, 24);
            this.chkDisplayOfflineFilters.TabIndex = 8;
            this.chkDisplayOfflineFilters.Text = "Display Offline Filters";
            this.chkDisplayOfflineFilters.UseVisualStyleBackColor = true;
            // 
            // chkDisplayOutlookFilters
            // 
            this.chkDisplayOutlookFilters.AutoSize = true;
            this.chkDisplayOutlookFilters.Checked = true;
            this.chkDisplayOutlookFilters.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisplayOutlookFilters.Location = new System.Drawing.Point(4, 5);
            this.chkDisplayOutlookFilters.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkDisplayOutlookFilters.Name = "chkDisplayOutlookFilters";
            this.chkDisplayOutlookFilters.Size = new System.Drawing.Size(192, 24);
            this.chkDisplayOutlookFilters.TabIndex = 7;
            this.chkDisplayOutlookFilters.Text = "Display Outlook Filters";
            this.chkDisplayOutlookFilters.UseVisualStyleBackColor = true;
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsddbLoadUsersLocalDataRules,
            this.toolStripSeparator4,
            this.tsbUserRuleEnable,
            this.tsbUserRuleDisable,
            this.tsbDeleteUserRule,
            this.toolStripSeparator8,
            this.tsbShowFetchXmlUser,
            this.toolStripSeparator6,
            this.tsddbGroupBy});
            this.toolStrip3.Location = new System.Drawing.Point(4, 5);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip3.Size = new System.Drawing.Size(1342, 32);
            this.toolStrip3.TabIndex = 4;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // tsddbLoadUsersLocalDataRules
            // 
            this.tsddbLoadUsersLocalDataRules.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.forSpecificUsersToolStripMenuItem,
            this.toolStripSeparator2,
            this.loadAllUsersLocalDataRulesToolStripMenuItem});
            this.tsddbLoadUsersLocalDataRules.Image = ((System.Drawing.Image)(resources.GetObject("tsddbLoadUsersLocalDataRules.Image")));
            this.tsddbLoadUsersLocalDataRules.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbLoadUsersLocalDataRules.Name = "tsddbLoadUsersLocalDataRules";
            this.tsddbLoadUsersLocalDataRules.Size = new System.Drawing.Size(310, 29);
            this.tsddbLoadUsersLocalDataRules.Text = "Load Users Synchronization Filters";
            // 
            // forSpecificUsersToolStripMenuItem
            // 
            this.forSpecificUsersToolStripMenuItem.Name = "forSpecificUsersToolStripMenuItem";
            this.forSpecificUsersToolStripMenuItem.Size = new System.Drawing.Size(219, 30);
            this.forSpecificUsersToolStripMenuItem.Text = "For specific users";
            this.forSpecificUsersToolStripMenuItem.Click += new System.EventHandler(this.forSpecificUsersToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(216, 6);
            // 
            // loadAllUsersLocalDataRulesToolStripMenuItem
            // 
            this.loadAllUsersLocalDataRulesToolStripMenuItem.Name = "loadAllUsersLocalDataRulesToolStripMenuItem";
            this.loadAllUsersLocalDataRulesToolStripMenuItem.Size = new System.Drawing.Size(219, 30);
            this.loadAllUsersLocalDataRulesToolStripMenuItem.Text = "For all users";
            this.loadAllUsersLocalDataRulesToolStripMenuItem.Click += new System.EventHandler(this.tsbLoadUsersLocalDataRules_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbUserRuleEnable
            // 
            this.tsbUserRuleEnable.Image = ((System.Drawing.Image)(resources.GetObject("tsbUserRuleEnable.Image")));
            this.tsbUserRuleEnable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUserRuleEnable.Name = "tsbUserRuleEnable";
            this.tsbUserRuleEnable.Size = new System.Drawing.Size(84, 29);
            this.tsbUserRuleEnable.Text = "Enable";
            this.tsbUserRuleEnable.ToolTipText = "Enable selected rule(s)";
            this.tsbUserRuleEnable.Click += new System.EventHandler(this.tsbUserRuleEnable_Click);
            // 
            // tsbUserRuleDisable
            // 
            this.tsbUserRuleDisable.Image = ((System.Drawing.Image)(resources.GetObject("tsbUserRuleDisable.Image")));
            this.tsbUserRuleDisable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUserRuleDisable.Name = "tsbUserRuleDisable";
            this.tsbUserRuleDisable.Size = new System.Drawing.Size(90, 29);
            this.tsbUserRuleDisable.Text = "Disable";
            this.tsbUserRuleDisable.ToolTipText = "Disable selected rule(s)";
            this.tsbUserRuleDisable.Click += new System.EventHandler(this.tsbUserRuleDisable_Click);
            // 
            // tsbDeleteUserRule
            // 
            this.tsbDeleteUserRule.Image = ((System.Drawing.Image)(resources.GetObject("tsbDeleteUserRule.Image")));
            this.tsbDeleteUserRule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeleteUserRule.Name = "tsbDeleteUserRule";
            this.tsbDeleteUserRule.Size = new System.Drawing.Size(82, 29);
            this.tsbDeleteUserRule.Text = "Delete";
            this.tsbDeleteUserRule.ToolTipText = "Delete selected user local data rule(s)";
            this.tsbDeleteUserRule.Click += new System.EventHandler(this.tsbDeleteUserRule_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbShowFetchXmlUser
            // 
            this.tsbShowFetchXmlUser.Image = ((System.Drawing.Image)(resources.GetObject("tsbShowFetchXmlUser.Image")));
            this.tsbShowFetchXmlUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowFetchXmlUser.Name = "tsbShowFetchXmlUser";
            this.tsbShowFetchXmlUser.Size = new System.Drawing.Size(154, 29);
            this.tsbShowFetchXmlUser.Text = "Show FetchXml";
            this.tsbShowFetchXmlUser.ToolTipText = "Display FetchXml query used for the selected rule";
            this.tsbShowFetchXmlUser.Click += new System.EventHandler(this.tsbShowFetchXmlUser_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 32);
            // 
            // tsddbGroupBy
            // 
            this.tsddbGroupBy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsddbGroupBy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiGroupByRule,
            this.tsmiGroupByReturnedType,
            this.tsmiGroupByUser});
            this.tsddbGroupBy.Image = ((System.Drawing.Image)(resources.GetObject("tsddbGroupBy.Image")));
            this.tsddbGroupBy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbGroupBy.Name = "tsddbGroupBy";
            this.tsddbGroupBy.Size = new System.Drawing.Size(100, 29);
            this.tsddbGroupBy.Text = "Group by";
            // 
            // tsmiGroupByRule
            // 
            this.tsmiGroupByRule.Image = ((System.Drawing.Image)(resources.GetObject("tsmiGroupByRule.Image")));
            this.tsmiGroupByRule.Name = "tsmiGroupByRule";
            this.tsmiGroupByRule.Size = new System.Drawing.Size(199, 30);
            this.tsmiGroupByRule.Text = "Rule";
            this.tsmiGroupByRule.Click += new System.EventHandler(this.tsmiGroupByRule_Click);
            // 
            // tsmiGroupByReturnedType
            // 
            this.tsmiGroupByReturnedType.Image = ((System.Drawing.Image)(resources.GetObject("tsmiGroupByReturnedType.Image")));
            this.tsmiGroupByReturnedType.Name = "tsmiGroupByReturnedType";
            this.tsmiGroupByReturnedType.Size = new System.Drawing.Size(199, 30);
            this.tsmiGroupByReturnedType.Text = "Returned Type";
            this.tsmiGroupByReturnedType.Click += new System.EventHandler(this.tsmiGroupByReturnedType_Click);
            // 
            // tsmiGroupByUser
            // 
            this.tsmiGroupByUser.Image = ((System.Drawing.Image)(resources.GetObject("tsmiGroupByUser.Image")));
            this.tsmiGroupByUser.Name = "tsmiGroupByUser";
            this.tsmiGroupByUser.Size = new System.Drawing.Size(199, 30);
            this.tsmiGroupByUser.Text = "User";
            this.tsmiGroupByUser.Click += new System.EventHandler(this.tsmiGroupByUser_Click);
            // 
            // tabPageSystemViews
            // 
            this.tabPageSystemViews.Controls.Add(this.crmSystemViewsList);
            this.tabPageSystemViews.Controls.Add(this.toolStrip2);
            this.tabPageSystemViews.Location = new System.Drawing.Point(4, 29);
            this.tabPageSystemViews.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageSystemViews.Name = "tabPageSystemViews";
            this.tabPageSystemViews.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageSystemViews.Size = new System.Drawing.Size(1350, 842);
            this.tabPageSystemViews.TabIndex = 2;
            this.tabPageSystemViews.Text = "System Views";
            this.tabPageSystemViews.UseVisualStyleBackColor = true;
            // 
            // crmSystemViewsList
            // 
            this.crmSystemViewsList.DisplayOfflineFilter = false;
            this.crmSystemViewsList.DisplayOutlookFilter = false;
            this.crmSystemViewsList.DisplayRulesTemplate = false;
            this.crmSystemViewsList.DisplaySystemRules = false;
            this.crmSystemViewsList.DisplaySystemView = true;
            this.crmSystemViewsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crmSystemViewsList.EntityName = "savedquery";
            this.crmSystemViewsList.Location = new System.Drawing.Point(4, 37);
            this.crmSystemViewsList.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.crmSystemViewsList.Name = "crmSystemViewsList";
            this.crmSystemViewsList.Size = new System.Drawing.Size(1342, 800);
            this.crmSystemViewsList.TabIndex = 4;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbLoadSystemViews,
            this.toolStripSeparator1,
            this.tsbShowFetchXmlView,
            this.toolStripSeparator9,
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton2});
            this.toolStrip2.Location = new System.Drawing.Point(4, 5);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip2.Size = new System.Drawing.Size(1342, 32);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsbLoadSystemViews
            // 
            this.tsbLoadSystemViews.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadSystemViews.Image")));
            this.tsbLoadSystemViews.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadSystemViews.Name = "tsbLoadSystemViews";
            this.tsbLoadSystemViews.Size = new System.Drawing.Size(184, 29);
            this.tsbLoadSystemViews.Text = "Load System Views";
            this.tsbLoadSystemViews.Click += new System.EventHandler(this.tsbLoadSystemViews_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbShowFetchXmlView
            // 
            this.tsbShowFetchXmlView.Image = ((System.Drawing.Image)(resources.GetObject("tsbShowFetchXmlView.Image")));
            this.tsbShowFetchXmlView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowFetchXmlView.Name = "tsbShowFetchXmlView";
            this.tsbShowFetchXmlView.Size = new System.Drawing.Size(154, 29);
            this.tsbShowFetchXmlView.Text = "Show FetchXml";
            this.tsbShowFetchXmlView.ToolTipText = "Display FetchXml query used for the selected rule";
            this.tsbShowFetchXmlView.Click += new System.EventHandler(this.tsbShowFetchXmlView_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 32);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCreateSystemFilterFromView,
            this.tsmiCreateFilterTemplateFromView});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(91, 29);
            this.toolStripDropDownButton1.Text = "Create";
            // 
            // tsmiCreateSystemFilterFromView
            // 
            this.tsmiCreateSystemFilterFromView.Name = "tsmiCreateSystemFilterFromView";
            this.tsmiCreateSystemFilterFromView.Size = new System.Drawing.Size(510, 30);
            this.tsmiCreateSystemFilterFromView.Text = "System Synchronization Filter from selected view(s)";
            this.tsmiCreateSystemFilterFromView.Click += new System.EventHandler(this.tsmiCreateSystemFilterFromView_Click);
            // 
            // tsmiCreateFilterTemplateFromView
            // 
            this.tsmiCreateFilterTemplateFromView.Name = "tsmiCreateFilterTemplateFromView";
            this.tsmiCreateFilterTemplateFromView.Size = new System.Drawing.Size(510, 30);
            this.tsmiCreateFilterTemplateFromView.Text = "Synchronization Filter Template from selected views(s)";
            this.tsmiCreateFilterTemplateFromView.Click += new System.EventHandler(this.tsmiCreateFilterTemplateFromView_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiUpdateSystemFilter,
            this.tsmiUpdateFilterTemplate});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(99, 29);
            this.toolStripDropDownButton2.Text = "Update";
            // 
            // tsmiUpdateSystemFilter
            // 
            this.tsmiUpdateSystemFilter.Name = "tsmiUpdateSystemFilter";
            this.tsmiUpdateSystemFilter.Size = new System.Drawing.Size(484, 30);
            this.tsmiUpdateSystemFilter.Text = "System Synchronization Filter from selected view";
            this.tsmiUpdateSystemFilter.Click += new System.EventHandler(this.tsmiUpdateSystemFilter_Click);
            // 
            // tsmiUpdateFilterTemplate
            // 
            this.tsmiUpdateFilterTemplate.Name = "tsmiUpdateFilterTemplate";
            this.tsmiUpdateFilterTemplate.Size = new System.Drawing.Size(484, 30);
            this.tsmiUpdateFilterTemplate.Text = "Synchronization Filter Template from selected view";
            this.tsmiUpdateFilterTemplate.Click += new System.EventHandler(this.tsmiUpdateFilterTemplate_Click);
            // 
            // tabPageUsers
            // 
            this.tabPageUsers.Controls.Add(this.crmUserList1);
            this.tabPageUsers.Controls.Add(this.toolStrip1);
            this.tabPageUsers.Location = new System.Drawing.Point(4, 29);
            this.tabPageUsers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageUsers.Name = "tabPageUsers";
            this.tabPageUsers.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageUsers.Size = new System.Drawing.Size(1350, 842);
            this.tabPageUsers.TabIndex = 3;
            this.tabPageUsers.Text = "Users";
            this.tabPageUsers.UseVisualStyleBackColor = true;
            // 
            // crmUserList1
            // 
            this.crmUserList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crmUserList1.Location = new System.Drawing.Point(4, 37);
            this.crmUserList1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.crmUserList1.Name = "crmUserList1";
            this.crmUserList1.SelectMultipleUsers = true;
            this.crmUserList1.Size = new System.Drawing.Size(1342, 800);
            this.crmUserList1.TabIndex = 2;
            this.crmUserList1.OnRequestConnection += new System.EventHandler(this.crmUserList1_OnRequestConnection);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbResetUsersFiltersToDefault,
            this.toolStripSeparator14,
            this.tsbCopyUserFiltersToUser});
            this.toolStrip1.Location = new System.Drawing.Point(4, 5);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1342, 32);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbResetUsersFiltersToDefault
            // 
            this.tsbResetUsersFiltersToDefault.Image = ((System.Drawing.Image)(resources.GetObject("tsbResetUsersFiltersToDefault.Image")));
            this.tsbResetUsersFiltersToDefault.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbResetUsersFiltersToDefault.Name = "tsbResetUsersFiltersToDefault";
            this.tsbResetUsersFiltersToDefault.Size = new System.Drawing.Size(321, 29);
            this.tsbResetUsersFiltersToDefault.Text = "Reset to the default Local Data Rules";
            this.tsbResetUsersFiltersToDefault.ToolTipText = "All synchronization rules of the selected users will be overriden with the defaul" +
    "t templates";
            this.tsbResetUsersFiltersToDefault.Click += new System.EventHandler(this.tsbResetUsersFiltersToDefault_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbCopyUserFiltersToUser
            // 
            this.tsbCopyUserFiltersToUser.Image = ((System.Drawing.Image)(resources.GetObject("tsbCopyUserFiltersToUser.Image")));
            this.tsbCopyUserFiltersToUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopyUserFiltersToUser.Name = "tsbCopyUserFiltersToUser";
            this.tsbCopyUserFiltersToUser.Size = new System.Drawing.Size(441, 29);
            this.tsbCopyUserFiltersToUser.Text = "Apply selected user synchronization filters to user(s)";
            this.tsbCopyUserFiltersToUser.Click += new System.EventHandler(this.tsbCopyUserFiltersToUser_Click);
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabCtrl);
            this.Controls.Add(this.toolStripMenu);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(1366, 923);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.tabCtrl.ResumeLayout(false);
            this.tabPageSystemFilters.ResumeLayout(false);
            this.tabPageSystemFilters.PerformLayout();
            this.toolStrip5.ResumeLayout(false);
            this.toolStrip5.PerformLayout();
            this.tabPageDefaultRules.ResumeLayout(false);
            this.tabPageDefaultRules.PerformLayout();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.tabPageUsersRules.ResumeLayout(false);
            this.tabPageUsersRules.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.tabPageSystemViews.ResumeLayout(false);
            this.tabPageSystemViews.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tabPageUsers.ResumeLayout(false);
            this.tabPageUsers.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ImageList toolImageList;
        private System.Windows.Forms.TabControl tabCtrl;
        private System.Windows.Forms.TabPage tabPageDefaultRules;
        private System.Windows.Forms.TabPage tabPageUsersRules;
        private System.Windows.Forms.TabPage tabPageSystemViews;
        private System.Windows.Forms.TabPage tabPageUsers;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbResetUsersFiltersToDefault;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbLoadSystemViews;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton tsbLoadDefaultLocalDataRules;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbDefaultDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbUserRuleEnable;
        private System.Windows.Forms.ToolStripButton tsbUserRuleDisable;
        private System.Windows.Forms.ToolStripButton tsbDeleteUserRule;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbApplyToUsers;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripDropDownButton tsddbGroupBy;
        private System.Windows.Forms.ToolStripMenuItem tsmiGroupByRule;
        private System.Windows.Forms.ToolStripMenuItem tsmiGroupByReturnedType;
        private System.Windows.Forms.ToolStripMenuItem tsmiGroupByUser;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tsbShowFetchXmlDefault;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton tsbShowFetchXmlUser;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton tsbShowFetchXmlView;
        private System.Windows.Forms.ToolStripButton tsbDefineAsDefault;
        private System.Windows.Forms.ToolStripDropDownButton tsddbLoadUsersLocalDataRules;
        private System.Windows.Forms.ToolStripMenuItem loadAllUsersLocalDataRulesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem forSpecificUsersToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageSystemFilters;
        private System.Windows.Forms.ToolStrip toolStrip5;
        private System.Windows.Forms.ToolStripButton tsbLoadSystemSynchronizationFilter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton tsbShowFetchXmlSystemRules;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripButton tsbSystemRuleDelete;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem tsmiCreateSystemFilterFromView;
        private System.Windows.Forms.ToolStripMenuItem tsmiCreateFilterTemplateFromView;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpdateSystemFilter;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpdateFilterTemplate;
        private System.Windows.Forms.ToolStripButton tsbSystemFilterProperties;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripButton tsbTemplateFilterProperties;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripButton tsbCopyUserFiltersToUser;
        private Controls.CrmUserList crmUserList1;
        private Controls.CrmSystemViewList crmSystemViewsList;
        private Controls.CrmSystemViewList systemRulesListView;
        private Controls.CrmSystemViewList defaultLocalDataRulesView;
        private Controls.CrmSystemViewList usersLocalDataRulesView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkDisplayOfflineFilters;
        private System.Windows.Forms.CheckBox chkDisplayOutlookFilters;
    }
}
