using XrmToolBox.Controls;

namespace XrmToolBox.New
{
    partial class PluginsForm2
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

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginsForm2));
            this.addToFavoritesWithCurrentConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFilterOperator = new System.Windows.Forms.Button();
            this.ch1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsOnePlugin = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.lblPluginsNotFoundText = new System.Windows.Forms.Label();
            this.lblToolsNotFound = new System.Windows.Forms.Label();
            this.lblToolsNotFoundExplanation = new System.Windows.Forms.Label();
            this.llResetSearchFilter = new System.Windows.Forms.LinkLabel();
            this.lvTools = new System.Windows.Forms.ListView();
            this.pnlHelp = new System.Windows.Forms.Panel();
            this.pnlLeftBottom = new System.Windows.Forms.Panel();
            this.pnlLeftNavTop = new System.Windows.Forms.Panel();
            this.pnlNavLeft = new System.Windows.Forms.Panel();
            this.pnlNavLeftMain = new System.Windows.Forms.Panel();
            this.pnlNavMain = new System.Windows.Forms.Panel();
            this.pnlToolsList = new System.Windows.Forms.Panel();
            this.pnlNoPluginFound = new System.Windows.Forms.Panel();
            this.pnlTopSearch = new System.Windows.Forms.Panel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pbOpenPluginsStore = new System.Windows.Forms.PictureBox();
            this.tsmiOpenWithNewConection = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenProjectHomePage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShortcutTool = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShortcutToolConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHidePlugin = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUninstallPlugin = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddToFavorites = new System.Windows.Forms.ToolStripMenuItem();
            this.txtSearch = new XrmToolBox.Controls.TextBoxWithPlaceholder();
            this.pbRefreshList = new System.Windows.Forms.PictureBox();
            this.pbClearSearch = new System.Windows.Forms.PictureBox();
            this.btnChangeSize = new System.Windows.Forms.Button();
            this.cmsOnePlugin.SuspendLayout();
            this.pnlHelp.SuspendLayout();
            this.pnlLeftBottom.SuspendLayout();
            this.pnlNavLeftMain.SuspendLayout();
            this.pnlNavMain.SuspendLayout();
            this.pnlToolsList.SuspendLayout();
            this.pnlNoPluginFound.SuspendLayout();
            this.pnlTopSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOpenPluginsStore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRefreshList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClearSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // addToFavoritesWithCurrentConnectionToolStripMenuItem
            // 
            this.addToFavoritesWithCurrentConnectionToolStripMenuItem.Name = "addToFavoritesWithCurrentConnectionToolStripMenuItem";
            this.addToFavoritesWithCurrentConnectionToolStripMenuItem.Size = new System.Drawing.Size(413, 32);
            this.addToFavoritesWithCurrentConnectionToolStripMenuItem.Text = "Add to Favorites with current connection";
            // 
            // btnFilterOperator
            // 
            this.btnFilterOperator.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnFilterOperator.FlatAppearance.BorderSize = 0;
            this.btnFilterOperator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterOperator.Font = new System.Drawing.Font("Segoe UI Variable Text", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilterOperator.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFilterOperator.Location = new System.Drawing.Point(4, -32768);
            this.btnFilterOperator.Name = "btnFilterOperator";
            this.btnFilterOperator.Size = new System.Drawing.Size(208, 22617);
            this.btnFilterOperator.TabIndex = 0;
            this.btnFilterOperator.Text = "Use OR";
            this.btnFilterOperator.UseVisualStyleBackColor = true;
            this.btnFilterOperator.Click += new System.EventHandler(this.btnFilterOperator_Click);
            // 
            // ch1
            // 
            this.ch1.Text = "";
            this.ch1.Width = 400;
            // 
            // cmsOnePlugin
            // 
            this.cmsOnePlugin.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmsOnePlugin.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenWithNewConection,
            this.toolStripSeparator2,
            this.tsmiOpenProjectHomePage,
            this.toolStripSeparator9,
            this.tsmiShortcutTool,
            this.tsmiShortcutToolConnection,
            this.toolStripSeparator7,
            this.tsmiHidePlugin,
            this.tsmiUninstallPlugin,
            this.toolStripSeparator1,
            this.tsmiAddToFavorites,
            this.addToFavoritesWithCurrentConnectionToolStripMenuItem});
            this.cmsOnePlugin.Name = "cmsOnePlugin";
            this.cmsOnePlugin.Size = new System.Drawing.Size(414, 284);
            this.cmsOnePlugin.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmsOnePlugin_ItemClicked);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(410, 6);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(410, 6);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(410, 6);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(410, 6);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 50);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(117)))), ((int)(((byte)(188)))));
            this.label3.Location = new System.Drawing.Point(0, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1718, 110);
            this.label3.TabIndex = 1;
            this.label3.Tag = "";
            this.label3.Text = "Please redefine the criteria, reset it, or download some new tools from our Tools" +
    " Library";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPluginsNotFoundText
            // 
            this.lblPluginsNotFoundText.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPluginsNotFoundText.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPluginsNotFoundText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(117)))), ((int)(((byte)(188)))));
            this.lblPluginsNotFoundText.Location = new System.Drawing.Point(0, 0);
            this.lblPluginsNotFoundText.Name = "lblPluginsNotFoundText";
            this.lblPluginsNotFoundText.Size = new System.Drawing.Size(1718, 80);
            this.lblPluginsNotFoundText.TabIndex = 0;
            this.lblPluginsNotFoundText.Tag = "Searching for \"{0}\"{1} did not match any tools installed";
            this.lblPluginsNotFoundText.Text = "Searching for \"{0}\"{1} did not match any tools installed";
            this.lblPluginsNotFoundText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblToolsNotFound
            // 
            this.lblToolsNotFound.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblToolsNotFound.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToolsNotFound.ForeColor = System.Drawing.Color.Red;
            this.lblToolsNotFound.Location = new System.Drawing.Point(0, 0);
            this.lblToolsNotFound.Name = "lblToolsNotFound";
            this.lblToolsNotFound.Size = new System.Drawing.Size(1718, 51);
            this.lblToolsNotFound.TabIndex = 0;
            this.lblToolsNotFound.Text = "Oups... no tool found!";
            this.lblToolsNotFound.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblToolsNotFoundExplanation
            // 
            this.lblToolsNotFoundExplanation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblToolsNotFoundExplanation.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToolsNotFoundExplanation.Location = new System.Drawing.Point(0, 65);
            this.lblToolsNotFoundExplanation.Name = "lblToolsNotFoundExplanation";
            this.lblToolsNotFoundExplanation.Size = new System.Drawing.Size(1706, 174);
            this.lblToolsNotFoundExplanation.TabIndex = 1;
            this.lblToolsNotFoundExplanation.Text = resources.GetString("lblToolsNotFoundExplanation.Text");
            // 
            // llResetSearchFilter
            // 
            this.llResetSearchFilter.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(117)))), ((int)(((byte)(188)))));
            this.llResetSearchFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.llResetSearchFilter.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(117)))), ((int)(((byte)(188)))));
            this.llResetSearchFilter.Location = new System.Drawing.Point(0, 381);
            this.llResetSearchFilter.Name = "llResetSearchFilter";
            this.llResetSearchFilter.Size = new System.Drawing.Size(1718, 32);
            this.llResetSearchFilter.TabIndex = 3;
            this.llResetSearchFilter.TabStop = true;
            this.llResetSearchFilter.Text = "or reset the search filter";
            this.llResetSearchFilter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.llResetSearchFilter.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llResetSearchFilter_LinkClicked);
            // 
            // lvTools
            // 
            this.lvTools.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvTools.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch1});
            this.lvTools.ContextMenuStrip = this.cmsOnePlugin;
            this.lvTools.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lvTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvTools.Font = new System.Drawing.Font("Segoe UI Variable Text", 14F);
            this.lvTools.FullRowSelect = true;
            this.lvTools.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvTools.HideSelection = false;
            this.lvTools.Location = new System.Drawing.Point(4, 4);
            this.lvTools.MultiSelect = false;
            this.lvTools.Name = "lvTools";
            this.lvTools.OwnerDraw = true;
            this.lvTools.ShowGroups = false;
            this.lvTools.ShowItemToolTips = true;
            this.lvTools.Size = new System.Drawing.Size(1718, 888);
            this.lvTools.SmallImageList = this.imageList1;
            this.lvTools.TabIndex = 0;
            this.lvTools.UseCompatibleStateImageBehavior = false;
            this.lvTools.View = System.Windows.Forms.View.Details;
            this.lvTools.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lvTools_DrawItem);
            this.lvTools.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvTools_KeyDown);
            this.lvTools.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvTools_MouseClick);
            this.lvTools.Resize += new System.EventHandler(this.lvTools_Resize);
            // 
            // pnlHelp
            // 
            this.pnlHelp.BackColor = System.Drawing.Color.White;
            this.pnlHelp.Controls.Add(this.lblToolsNotFoundExplanation);
            this.pnlHelp.Controls.Add(this.lblToolsNotFound);
            this.pnlHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHelp.Location = new System.Drawing.Point(4, 4);
            this.pnlHelp.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.pnlHelp.Name = "pnlHelp";
            this.pnlHelp.Size = new System.Drawing.Size(1718, 888);
            this.pnlHelp.TabIndex = 25;
            this.pnlHelp.Visible = false;
            // 
            // pnlLeftBottom
            // 
            this.pnlLeftBottom.Controls.Add(this.btnFilterOperator);
            this.pnlLeftBottom.Controls.Add(this.btnChangeSize);
            this.pnlLeftBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLeftBottom.Location = new System.Drawing.Point(0, 2075);
            this.pnlLeftBottom.Name = "pnlLeftBottom";
            this.pnlLeftBottom.Padding = new System.Windows.Forms.Padding(4, 10, 4, 10);
            this.pnlLeftBottom.Size = new System.Drawing.Size(216, 2934);
            this.pnlLeftBottom.TabIndex = 2;
            // 
            // pnlLeftNavTop
            // 
            this.pnlLeftNavTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLeftNavTop.Location = new System.Drawing.Point(0, 0);
            this.pnlLeftNavTop.Name = "pnlLeftNavTop";
            this.pnlLeftNavTop.Size = new System.Drawing.Size(216, 405);
            this.pnlLeftNavTop.TabIndex = 0;
            // 
            // pnlNavLeft
            // 
            this.pnlNavLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNavLeft.Font = new System.Drawing.Font("Segoe UI Variable Text", 12F);
            this.pnlNavLeft.Location = new System.Drawing.Point(0, 608);
            this.pnlNavLeft.Name = "pnlNavLeft";
            this.pnlNavLeft.Size = new System.Drawing.Size(216, 0);
            this.pnlNavLeft.TabIndex = 1;
            // 
            // pnlNavLeftMain
            // 
            this.pnlNavLeftMain.AutoScroll = true;
            this.pnlNavLeftMain.Controls.Add(this.pnlNavLeft);
            this.pnlNavLeftMain.Controls.Add(this.pnlLeftBottom);
            this.pnlNavLeftMain.Controls.Add(this.pnlLeftNavTop);
            this.pnlNavLeftMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlNavLeftMain.Location = new System.Drawing.Point(0, 40);
            this.pnlNavLeftMain.Name = "pnlNavLeftMain";
            this.pnlNavLeftMain.Size = new System.Drawing.Size(170, 949);
            this.pnlNavLeftMain.TabIndex = 1;
            this.pnlNavLeftMain.Visible = false;
            // 
            // pnlNavMain
            // 
            this.pnlNavMain.BackColor = System.Drawing.Color.White;
            this.pnlNavMain.Controls.Add(this.pnlToolsList);
            this.pnlNavMain.Controls.Add(this.pnlTopSearch);
            this.pnlNavMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNavMain.Location = new System.Drawing.Point(170, 40);
            this.pnlNavMain.Name = "pnlNavMain";
            this.pnlNavMain.Size = new System.Drawing.Size(1726, 949);
            this.pnlNavMain.TabIndex = 0;
            // 
            // pnlToolsList
            // 
            this.pnlToolsList.Controls.Add(this.pnlNoPluginFound);
            this.pnlToolsList.Controls.Add(this.pnlHelp);
            this.pnlToolsList.Controls.Add(this.lvTools);
            this.pnlToolsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlToolsList.Location = new System.Drawing.Point(0, 53);
            this.pnlToolsList.Name = "pnlToolsList";
            this.pnlToolsList.Padding = new System.Windows.Forms.Padding(4);
            this.pnlToolsList.Size = new System.Drawing.Size(1726, 896);
            this.pnlToolsList.TabIndex = 1;
            // 
            // pnlNoPluginFound
            // 
            this.pnlNoPluginFound.BackColor = System.Drawing.Color.White;
            this.pnlNoPluginFound.Controls.Add(this.llResetSearchFilter);
            this.pnlNoPluginFound.Controls.Add(this.pbOpenPluginsStore);
            this.pnlNoPluginFound.Controls.Add(this.label3);
            this.pnlNoPluginFound.Controls.Add(this.lblPluginsNotFoundText);
            this.pnlNoPluginFound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNoPluginFound.Location = new System.Drawing.Point(4, 4);
            this.pnlNoPluginFound.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.pnlNoPluginFound.Name = "pnlNoPluginFound";
            this.pnlNoPluginFound.Size = new System.Drawing.Size(1718, 888);
            this.pnlNoPluginFound.TabIndex = 26;
            this.pnlNoPluginFound.Visible = false;
            // 
            // pnlTopSearch
            // 
            this.pnlTopSearch.BackColor = System.Drawing.Color.White;
            this.pnlTopSearch.Controls.Add(this.txtSearch);
            this.pnlTopSearch.Controls.Add(this.pbRefreshList);
            this.pnlTopSearch.Controls.Add(this.pbClearSearch);
            this.pnlTopSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlTopSearch.Name = "pnlTopSearch";
            this.pnlTopSearch.Padding = new System.Windows.Forms.Padding(4);
            this.pnlTopSearch.Size = new System.Drawing.Size(1726, 53);
            this.pnlTopSearch.TabIndex = 0;
            // 
            // pnlTop
            // 
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1896, 40);
            this.pnlTop.TabIndex = 4;
            this.pnlTop.Visible = false;
            // 
            // pbOpenPluginsStore
            // 
            this.pbOpenPluginsStore.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbOpenPluginsStore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbOpenPluginsStore.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbOpenPluginsStore.Image = global::XrmToolBox.Properties.Resources.OpenToolsLibrary;
            this.pbOpenPluginsStore.Location = new System.Drawing.Point(0, 190);
            this.pbOpenPluginsStore.Name = "pbOpenPluginsStore";
            this.pbOpenPluginsStore.Size = new System.Drawing.Size(1718, 191);
            this.pbOpenPluginsStore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbOpenPluginsStore.TabIndex = 2;
            this.pbOpenPluginsStore.TabStop = false;
            this.pbOpenPluginsStore.Click += new System.EventHandler(this.pbOpenPluginsStore_Click);
            // 
            // tsmiOpenWithNewConection
            // 
            this.tsmiOpenWithNewConection.Image = global::XrmToolBox.Properties.Resources.Connect24;
            this.tsmiOpenWithNewConection.Name = "tsmiOpenWithNewConection";
            this.tsmiOpenWithNewConection.Size = new System.Drawing.Size(413, 32);
            this.tsmiOpenWithNewConection.Text = "Open Tool with new connection";
            // 
            // tsmiOpenProjectHomePage
            // 
            this.tsmiOpenProjectHomePage.Image = global::XrmToolBox.Properties.Resources.Web24;
            this.tsmiOpenProjectHomePage.Name = "tsmiOpenProjectHomePage";
            this.tsmiOpenProjectHomePage.Size = new System.Drawing.Size(413, 32);
            this.tsmiOpenProjectHomePage.Text = "Open project home page";
            // 
            // tsmiShortcutTool
            // 
            this.tsmiShortcutTool.Image = global::XrmToolBox.Properties.Resources.Shortcut24;
            this.tsmiShortcutTool.Name = "tsmiShortcutTool";
            this.tsmiShortcutTool.Size = new System.Drawing.Size(413, 32);
            this.tsmiShortcutTool.Text = "Create shortcut (Tool)";
            // 
            // tsmiShortcutToolConnection
            // 
            this.tsmiShortcutToolConnection.Image = global::XrmToolBox.Properties.Resources.Shortcut24;
            this.tsmiShortcutToolConnection.Name = "tsmiShortcutToolConnection";
            this.tsmiShortcutToolConnection.Size = new System.Drawing.Size(413, 32);
            this.tsmiShortcutToolConnection.Text = "Create shortcut (Tool and Connection)";
            // 
            // tsmiHidePlugin
            // 
            this.tsmiHidePlugin.Image = global::XrmToolBox.Properties.Resources.Hide24;
            this.tsmiHidePlugin.Name = "tsmiHidePlugin";
            this.tsmiHidePlugin.Size = new System.Drawing.Size(413, 32);
            this.tsmiHidePlugin.Text = "Hide";
            // 
            // tsmiUninstallPlugin
            // 
            this.tsmiUninstallPlugin.Image = global::XrmToolBox.Properties.Resources.Uninstall24;
            this.tsmiUninstallPlugin.Name = "tsmiUninstallPlugin";
            this.tsmiUninstallPlugin.Size = new System.Drawing.Size(413, 32);
            this.tsmiUninstallPlugin.Text = "Uninstall";
            // 
            // tsmiAddToFavorites
            // 
            this.tsmiAddToFavorites.Image = global::XrmToolBox.Properties.Resources.Favorite24;
            this.tsmiAddToFavorites.Name = "tsmiAddToFavorites";
            this.tsmiAddToFavorites.Size = new System.Drawing.Size(413, 32);
            this.tsmiAddToFavorites.Text = "Add to Favorites";
            // 
            // txtSearch
            // 
            this.txtSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI Variable Text", 12F);
            this.txtSearch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtSearch.Location = new System.Drawing.Point(35, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Placeholder = "Search...";
            this.txtSearch.Size = new System.Drawing.Size(1656, 39);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.Tag = true;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // pbRefreshList
            // 
            this.pbRefreshList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbRefreshList.Dock = System.Windows.Forms.DockStyle.Right;
            this.pbRefreshList.Image = global::XrmToolBox.Properties.Resources.Refresh24;
            this.pbRefreshList.Location = new System.Drawing.Point(1691, 4);
            this.pbRefreshList.Name = "pbRefreshList";
            this.pbRefreshList.Size = new System.Drawing.Size(31, 45);
            this.pbRefreshList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbRefreshList.TabIndex = 3;
            this.pbRefreshList.TabStop = false;
            this.pbRefreshList.Click += new System.EventHandler(this.pbRefreshList_Click);
            // 
            // pbClearSearch
            // 
            this.pbClearSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbClearSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbClearSearch.Image = global::XrmToolBox.Properties.Resources.Eraser24;
            this.pbClearSearch.Location = new System.Drawing.Point(4, 4);
            this.pbClearSearch.Name = "pbClearSearch";
            this.pbClearSearch.Size = new System.Drawing.Size(31, 45);
            this.pbClearSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbClearSearch.TabIndex = 1;
            this.pbClearSearch.TabStop = false;
            this.pbClearSearch.Visible = false;
            this.pbClearSearch.Click += new System.EventHandler(this.pbClearSearch_Click);
            // 
            // btnChangeSize
            // 
            this.btnChangeSize.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnChangeSize.FlatAppearance.BorderSize = 0;
            this.btnChangeSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeSize.Image = global::XrmToolBox.Properties.Resources.Expand32;
            this.btnChangeSize.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChangeSize.Location = new System.Drawing.Point(4, -12973);
            this.btnChangeSize.Name = "btnChangeSize";
            this.btnChangeSize.Size = new System.Drawing.Size(208, 15897);
            this.btnChangeSize.TabIndex = 1;
            this.btnChangeSize.Text = "Use big";
            this.btnChangeSize.UseVisualStyleBackColor = true;
            this.btnChangeSize.Click += new System.EventHandler(this.btnChangeSize_Click);
            // 
            // PluginsForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1896, 989);
            this.Controls.Add(this.pnlNavMain);
            this.Controls.Add(this.pnlNavLeftMain);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI Variable Text", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "PluginsForm2";
            this.Text = "Tools";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PluginsForm2_FormClosing);
            this.Load += new System.EventHandler(this.PluginsForm2_Load);
            this.cmsOnePlugin.ResumeLayout(false);
            this.pnlHelp.ResumeLayout(false);
            this.pnlLeftBottom.ResumeLayout(false);
            this.pnlNavLeftMain.ResumeLayout(false);
            this.pnlNavMain.ResumeLayout(false);
            this.pnlToolsList.ResumeLayout(false);
            this.pnlNoPluginFound.ResumeLayout(false);
            this.pnlTopSearch.ResumeLayout(false);
            this.pnlTopSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOpenPluginsStore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRefreshList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClearSearch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlNavMain;
        private System.Windows.Forms.Panel pnlNavLeftMain;
        private System.Windows.Forms.Panel pnlLeftNavTop;
        private System.Windows.Forms.Panel pnlTopSearch;
        private System.Windows.Forms.Panel pnlToolsList;
        private System.Windows.Forms.ListView lvTools;
        private System.Windows.Forms.ColumnHeader ch1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel pnlNavLeft;
        private System.Windows.Forms.Panel pnlLeftBottom;
        private System.Windows.Forms.Button btnChangeSize;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlHelp;
        private System.Windows.Forms.Label lblToolsNotFoundExplanation;
        private System.Windows.Forms.Label lblToolsNotFound;
        private System.Windows.Forms.ContextMenuStrip cmsOnePlugin;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenWithNewConection;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenProjectHomePage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem tsmiShortcutTool;
        private System.Windows.Forms.ToolStripMenuItem tsmiShortcutToolConnection;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem tsmiHidePlugin;
        private System.Windows.Forms.ToolStripMenuItem tsmiUninstallPlugin;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddToFavorites;
        private System.Windows.Forms.ToolStripMenuItem addToFavoritesWithCurrentConnectionToolStripMenuItem;
        private System.Windows.Forms.Panel pnlNoPluginFound;
        private System.Windows.Forms.LinkLabel llResetSearchFilter;
        private System.Windows.Forms.PictureBox pbOpenPluginsStore;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPluginsNotFoundText;
        private System.Windows.Forms.Button btnFilterOperator;
        private TextBoxWithPlaceholder txtSearch;
        private System.Windows.Forms.PictureBox pbClearSearch;
        private System.Windows.Forms.PictureBox pbRefreshList;
    }
}

