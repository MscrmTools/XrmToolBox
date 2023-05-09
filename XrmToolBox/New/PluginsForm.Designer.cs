﻿namespace XrmToolBox.New
{
    partial class PluginsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginsForm));
            this.cmsOnePlugin = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiOpenWithNewConection = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiOpenProjectHomePage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiShortcutTool = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShortcutToolConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiHidePlugin = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUninstallPlugin = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAddToFavorites = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.pbClear = new System.Windows.Forms.PictureBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.pnlCategory = new System.Windows.Forms.Panel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlNoPluginFound = new System.Windows.Forms.Panel();
            this.llResetSearchFilter = new System.Windows.Forms.LinkLabel();
            this.pbOpenPluginsStore = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPluginsNotFoundText = new System.Windows.Forms.Label();
            this.pnlPlugins = new System.Windows.Forms.Panel();
            this.pnlHelp = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.addToFavoritesWithCurrentConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsOnePlugin.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbClear)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.pnlNoPluginFound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOpenPluginsStore)).BeginInit();
            this.pnlHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsOnePlugin
            // 
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
            this.cmsOnePlugin.Size = new System.Drawing.Size(609, 476);
            this.cmsOnePlugin.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmsOnePlugin_ItemClicked);
            // 
            // tsmiOpenWithNewConection
            // 
            this.tsmiOpenWithNewConection.Image = global::XrmToolBox.Properties.Resources.lightning;
            this.tsmiOpenWithNewConection.Name = "tsmiOpenWithNewConection";
            this.tsmiOpenWithNewConection.Size = new System.Drawing.Size(405, 32);
            this.tsmiOpenWithNewConection.Text = "Open Tool with new connection";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(605, 6);
            // 
            // tsmiOpenProjectHomePage
            // 
            this.tsmiOpenProjectHomePage.Image = global::XrmToolBox.Properties.Resources.www_page;
            this.tsmiOpenProjectHomePage.Name = "tsmiOpenProjectHomePage";
            this.tsmiOpenProjectHomePage.Size = new System.Drawing.Size(405, 32);
            this.tsmiOpenProjectHomePage.Text = "Open project home page";
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(605, 6);
            // 
            // tsmiShortcutTool
            // 
            this.tsmiShortcutTool.Image = global::XrmToolBox.Properties.Resources.shortcut;
            this.tsmiShortcutTool.Name = "tsmiShortcutTool";
            this.tsmiShortcutTool.Size = new System.Drawing.Size(405, 32);
            this.tsmiShortcutTool.Text = "Create shortcut (Tool)";
            // 
            // tsmiShortcutToolConnection
            // 
            this.tsmiShortcutToolConnection.Image = global::XrmToolBox.Properties.Resources.shortcut;
            this.tsmiShortcutToolConnection.Name = "tsmiShortcutToolConnection";
            this.tsmiShortcutToolConnection.Size = new System.Drawing.Size(405, 32);
            this.tsmiShortcutToolConnection.Text = "Create shortcut (Tool and Connection)";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(605, 6);
            // 
            // tsmiHidePlugin
            // 
            this.tsmiHidePlugin.Image = global::XrmToolBox.Properties.Resources.hide_detail;
            this.tsmiHidePlugin.Name = "tsmiHidePlugin";
            this.tsmiHidePlugin.Size = new System.Drawing.Size(405, 32);
            this.tsmiHidePlugin.Text = "Hide";
            // 
            // tsmiUninstallPlugin
            // 
            this.tsmiUninstallPlugin.Image = global::XrmToolBox.Properties.Resources.Clear_16;
            this.tsmiUninstallPlugin.Name = "tsmiUninstallPlugin";
            this.tsmiUninstallPlugin.Size = new System.Drawing.Size(405, 32);
            this.tsmiUninstallPlugin.Text = "Uninstall";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(605, 6);
            // 
            // tsmiAddToFavorites
            // 
            this.tsmiAddToFavorites.Image = global::XrmToolBox.Properties.Resources.star;
            this.tsmiAddToFavorites.Name = "tsmiAddToFavorites";
            this.tsmiAddToFavorites.Size = new System.Drawing.Size(405, 32);
            this.tsmiAddToFavorites.Text = "Add to Favorites";
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.White;
            this.pnlSearch.Controls.Add(this.pbClear);
            this.pnlSearch.Controls.Add(this.txtSearch);
            this.pnlSearch.Controls.Add(this.lblSearch);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlSearch.Margin = new System.Windows.Forms.Padding(5);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(1071, 47);
            this.pnlSearch.TabIndex = 11;
            // 
            // pbClear
            // 
            this.pbClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbClear.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbClear.Image = global::XrmToolBox.Properties.Resources.Clear_16;
            this.pbClear.Location = new System.Drawing.Point(75, 0);
            this.pbClear.Name = "pbClear";
            this.pbClear.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.pbClear.Size = new System.Drawing.Size(34, 47);
            this.pbClear.TabIndex = 2;
            this.pbClear.TabStop = false;
            this.pbClear.Click += new System.EventHandler(this.pbClear_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtSearch.Location = new System.Drawing.Point(117, 7);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(5);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(948, 26);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // lblSearch
            // 
            this.lblSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSearch.Location = new System.Drawing.Point(0, 0);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(75, 47);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Filter";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlCategory
            // 
            this.pnlCategory.AutoScroll = true;
            this.pnlCategory.BackColor = System.Drawing.Color.White;
            this.pnlCategory.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCategory.Location = new System.Drawing.Point(0, 47);
            this.pnlCategory.Margin = new System.Windows.Forms.Padding(5);
            this.pnlCategory.Name = "pnlCategory";
            this.pnlCategory.Size = new System.Drawing.Size(1071, 35);
            this.pnlCategory.TabIndex = 12;
            this.pnlCategory.SizeChanged += new System.EventHandler(this.pnlCategory_SizeChanged);
            // 
            // pnlTop
            // 
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 82);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1071, 60);
            this.pnlTop.TabIndex = 16;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlNoPluginFound);
            this.pnlMain.Controls.Add(this.pnlPlugins);
            this.pnlMain.Controls.Add(this.pnlHelp);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 142);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(5);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1071, 620);
            this.pnlMain.TabIndex = 20;
            // 
            // pnlNoPluginFound
            // 
            this.pnlNoPluginFound.BackColor = System.Drawing.Color.White;
            this.pnlNoPluginFound.Controls.Add(this.llResetSearchFilter);
            this.pnlNoPluginFound.Controls.Add(this.pbOpenPluginsStore);
            this.pnlNoPluginFound.Controls.Add(this.label3);
            this.pnlNoPluginFound.Controls.Add(this.lblPluginsNotFoundText);
            this.pnlNoPluginFound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNoPluginFound.Location = new System.Drawing.Point(0, 0);
            this.pnlNoPluginFound.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.pnlNoPluginFound.Name = "pnlNoPluginFound";
            this.pnlNoPluginFound.Size = new System.Drawing.Size(1071, 620);
            this.pnlNoPluginFound.TabIndex = 21;
            this.pnlNoPluginFound.Visible = false;
            this.pnlNoPluginFound.Resize += new System.EventHandler(this.pnlNoPluginFound_Resize);
            // 
            // llResetSearchFilter
            // 
            this.llResetSearchFilter.AutoSize = true;
            this.llResetSearchFilter.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(117)))), ((int)(((byte)(188)))));
            this.llResetSearchFilter.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(117)))), ((int)(((byte)(188)))));
            this.llResetSearchFilter.Location = new System.Drawing.Point(430, 407);
            this.llResetSearchFilter.Name = "llResetSearchFilter";
            this.llResetSearchFilter.Size = new System.Drawing.Size(176, 20);
            this.llResetSearchFilter.TabIndex = 3;
            this.llResetSearchFilter.TabStop = true;
            this.llResetSearchFilter.Text = "or reset the search filter";
            this.llResetSearchFilter.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llResetSearchFilter_LinkClicked);
            // 
            // pbOpenPluginsStore
            // 
            this.pbOpenPluginsStore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbOpenPluginsStore.Image = global::XrmToolBox.Properties.Resources.OpenToolsLibrary;
            this.pbOpenPluginsStore.Location = new System.Drawing.Point(218, 240);
            this.pbOpenPluginsStore.Name = "pbOpenPluginsStore";
            this.pbOpenPluginsStore.Size = new System.Drawing.Size(600, 127);
            this.pbOpenPluginsStore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbOpenPluginsStore.TabIndex = 2;
            this.pbOpenPluginsStore.TabStop = false;
            this.pbOpenPluginsStore.Click += new System.EventHandler(this.pbOpenPluginsStore_Click);
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(117)))), ((int)(((byte)(188)))));
            this.label3.Location = new System.Drawing.Point(0, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1071, 110);
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
            this.lblPluginsNotFoundText.Size = new System.Drawing.Size(1071, 80);
            this.lblPluginsNotFoundText.TabIndex = 0;
            this.lblPluginsNotFoundText.Tag = "Searching for \"{0}\"{1} did not match any tools installed";
            this.lblPluginsNotFoundText.Text = "Searching for \"{0}\"{1} did not match any tools installed";
            this.lblPluginsNotFoundText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlPlugins
            // 
            this.pnlPlugins.AutoScroll = true;
            this.pnlPlugins.BackColor = System.Drawing.Color.White;
            this.pnlPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPlugins.Location = new System.Drawing.Point(0, 0);
            this.pnlPlugins.Name = "pnlPlugins";
            this.pnlPlugins.Size = new System.Drawing.Size(1071, 620);
            this.pnlPlugins.TabIndex = 20;
            // 
            // pnlHelp
            // 
            this.pnlHelp.BackColor = System.Drawing.Color.White;
            this.pnlHelp.Controls.Add(this.label2);
            this.pnlHelp.Controls.Add(this.label1);
            this.pnlHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHelp.Location = new System.Drawing.Point(0, 0);
            this.pnlHelp.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.pnlHelp.Name = "pnlHelp";
            this.pnlHelp.Size = new System.Drawing.Size(1071, 620);
            this.pnlHelp.TabIndex = 22;
            this.pnlHelp.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1059, 174);
            this.label2.TabIndex = 1;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1071, 51);
            this.label1.TabIndex = 0;
            this.label1.Text = "Oups... no tool found!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // addToFavoritesWithCurrentConnectionToolStripMenuItem
            // 
            this.addToFavoritesWithCurrentConnectionToolStripMenuItem.Name = "addToFavoritesWithCurrentConnectionToolStripMenuItem";
            this.addToFavoritesWithCurrentConnectionToolStripMenuItem.Size = new System.Drawing.Size(405, 32);
            this.addToFavoritesWithCurrentConnectionToolStripMenuItem.Text = "Add to Favorites with current connection";
            // 
            // PluginsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1071, 762);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlCategory);
            this.Controls.Add(this.pnlSearch);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PluginsForm";
            this.Text = "Tools";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PluginsForm_FormClosing);
            this.Load += new System.EventHandler(this.PluginsForm_Load);
            this.cmsOnePlugin.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbClear)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlNoPluginFound.ResumeLayout(false);
            this.pnlNoPluginFound.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOpenPluginsStore)).EndInit();
            this.pnlHelp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip cmsOnePlugin;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenProjectHomePage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem tsmiShortcutTool;
        private System.Windows.Forms.ToolStripMenuItem tsmiShortcutToolConnection;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem tsmiHidePlugin;
        private System.Windows.Forms.ToolStripMenuItem tsmiUninstallPlugin;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Panel pnlCategory;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlHelp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlNoPluginFound;
        private System.Windows.Forms.LinkLabel llResetSearchFilter;
        private System.Windows.Forms.PictureBox pbOpenPluginsStore;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPluginsNotFoundText;
        private System.Windows.Forms.Panel pnlPlugins;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddToFavorites;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenWithNewConection;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.PictureBox pbClear;
        private System.Windows.Forms.ToolStripMenuItem addToFavoritesWithCurrentConnectionToolStripMenuItem;
    }
}