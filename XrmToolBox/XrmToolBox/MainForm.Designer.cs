﻿using System.Drawing;

namespace XrmToolBox
{
    sealed partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbConnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbManageConnections = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbManageTabs = new System.Windows.Forms.ToolStripDropDownButton();
            this.closeCurrentTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllTabsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllTabsExceptActiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbOptions = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAbout = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCodePlex = new System.Windows.Forms.ToolStripDropDownButton();
            this.reportABugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startADiscussionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rateThisToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CodePlexXrmToolBoxMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CodePlexPluginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportABugPluginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startADiscussionPluginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rateThisToolPluginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsddbDonate = new System.Windows.Forms.ToolStripDropDownButton();
            this.donateInUSDollarsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.donateInEuroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.donateInGBPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.HomePageTab = new System.Windows.Forms.TabPage();
            this.pnlHelp = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.HomePageTab.SuspendLayout();
            this.pnlHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "access.png");
            this.imageList1.Images.SetKeyName(1, "acroread.png");
            this.imageList1.Images.SetKeyName(2, "agent.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbConnect,
            this.toolStripSeparator2,
            this.tsbManageConnections,
            this.toolStripSeparator5,
            this.tsbManageTabs,
            this.tsbOptions,
            this.toolStripSeparator1,
            this.tsbAbout,
            this.toolStripSeparator3,
            this.tsbCodePlex,
            this.toolStripSeparator4,
            this.tsddbDonate});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1326, 32);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbConnect
            // 
            this.tsbConnect.Image = ((System.Drawing.Image)(resources.GetObject("tsbConnect.Image")));
            this.tsbConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbConnect.Name = "tsbConnect";
            this.tsbConnect.Size = new System.Drawing.Size(170, 29);
            this.tsbConnect.Text = "Connect to CRM";
            this.tsbConnect.Click += new System.EventHandler(this.TsbConnectClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbManageConnections
            // 
            this.tsbManageConnections.Image = ((System.Drawing.Image)(resources.GetObject("tsbManageConnections.Image")));
            this.tsbManageConnections.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbManageConnections.Name = "tsbManageConnections";
            this.tsbManageConnections.Size = new System.Drawing.Size(204, 29);
            this.tsbManageConnections.Text = "Manage connections";
            this.tsbManageConnections.Click += new System.EventHandler(this.tsbManageConnections_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbManageTabs
            // 
            this.tsbManageTabs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeCurrentTabToolStripMenuItem,
            this.closeAllTabsToolStripMenuItem,
            this.closeAllTabsExceptActiveToolStripMenuItem});
            this.tsbManageTabs.Image = ((System.Drawing.Image)(resources.GetObject("tsbManageTabs.Image")));
            this.tsbManageTabs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbManageTabs.Name = "tsbManageTabs";
            this.tsbManageTabs.Size = new System.Drawing.Size(91, 29);
            this.tsbManageTabs.Text = "Tabs";
            // 
            // closeCurrentTabToolStripMenuItem
            // 
            this.closeCurrentTabToolStripMenuItem.Name = "closeCurrentTabToolStripMenuItem";
            this.closeCurrentTabToolStripMenuItem.Size = new System.Drawing.Size(286, 30);
            this.closeCurrentTabToolStripMenuItem.Text = "Close current tab";
            this.closeCurrentTabToolStripMenuItem.Click += new System.EventHandler(this.closeCurrentTabToolStripMenuItem_Click);
            // 
            // closeAllTabsToolStripMenuItem
            // 
            this.closeAllTabsToolStripMenuItem.Name = "closeAllTabsToolStripMenuItem";
            this.closeAllTabsToolStripMenuItem.Size = new System.Drawing.Size(286, 30);
            this.closeAllTabsToolStripMenuItem.Text = "Close all";
            this.closeAllTabsToolStripMenuItem.Click += new System.EventHandler(this.CloseAllTabsToolStripMenuItemClick);
            // 
            // closeAllTabsExceptActiveToolStripMenuItem
            // 
            this.closeAllTabsExceptActiveToolStripMenuItem.Name = "closeAllTabsExceptActiveToolStripMenuItem";
            this.closeAllTabsExceptActiveToolStripMenuItem.Size = new System.Drawing.Size(286, 30);
            this.closeAllTabsExceptActiveToolStripMenuItem.Text = "Close all except active tab";
            this.closeAllTabsExceptActiveToolStripMenuItem.Click += new System.EventHandler(this.CloseAllTabsExceptActiveToolStripMenuItemClick);
            // 
            // tsbOptions
            // 
            this.tsbOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOptions.Image = ((System.Drawing.Image)(resources.GetObject("tsbOptions.Image")));
            this.tsbOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOptions.Name = "tsbOptions";
            this.tsbOptions.Size = new System.Drawing.Size(28, 29);
            this.tsbOptions.Text = "Settings";
            this.tsbOptions.Click += new System.EventHandler(this.TsbOptionsClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbAbout
            // 
            this.tsbAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAbout.Image = ((System.Drawing.Image)(resources.GetObject("tsbAbout.Image")));
            this.tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAbout.Name = "tsbAbout";
            this.tsbAbout.Size = new System.Drawing.Size(28, 29);
            this.tsbAbout.Text = "About";
            this.tsbAbout.Click += new System.EventHandler(this.TsbAboutClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 32);
            // 
            // tsbCodePlex
            // 
            this.tsbCodePlex.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reportABugToolStripMenuItem,
            this.startADiscussionToolStripMenuItem,
            this.rateThisToolToolStripMenuItem,
            this.CodePlexXrmToolBoxMenuItem,
            this.CodePlexPluginMenuItem});
            this.tsbCodePlex.Image = ((System.Drawing.Image)(resources.GetObject("tsbCodePlex.Image")));
            this.tsbCodePlex.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCodePlex.Name = "tsbCodePlex";
            this.tsbCodePlex.Size = new System.Drawing.Size(127, 29);
            this.tsbCodePlex.Text = "CodePlex";
            // 
            // reportABugToolStripMenuItem
            // 
            this.reportABugToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("reportABugToolStripMenuItem.Image")));
            this.reportABugToolStripMenuItem.Name = "reportABugToolStripMenuItem";
            this.reportABugToolStripMenuItem.Size = new System.Drawing.Size(385, 30);
            this.reportABugToolStripMenuItem.Text = "Report a bug";
            this.reportABugToolStripMenuItem.Click += new System.EventHandler(this.TsbReportBugClick);
            // 
            // startADiscussionToolStripMenuItem
            // 
            this.startADiscussionToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("startADiscussionToolStripMenuItem.Image")));
            this.startADiscussionToolStripMenuItem.Name = "startADiscussionToolStripMenuItem";
            this.startADiscussionToolStripMenuItem.Size = new System.Drawing.Size(385, 30);
            this.startADiscussionToolStripMenuItem.Text = "Start a discussion / Request a feature";
            this.startADiscussionToolStripMenuItem.Click += new System.EventHandler(this.TsbDiscussClick);
            // 
            // rateThisToolToolStripMenuItem
            // 
            this.rateThisToolToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("rateThisToolToolStripMenuItem.Image")));
            this.rateThisToolToolStripMenuItem.Name = "rateThisToolToolStripMenuItem";
            this.rateThisToolToolStripMenuItem.Size = new System.Drawing.Size(385, 30);
            this.rateThisToolToolStripMenuItem.Text = "Rate this tool";
            this.rateThisToolToolStripMenuItem.Click += new System.EventHandler(this.TsbRateClick);
            // 
            // CodePlexXrmToolBoxMenuItem
            // 
            this.CodePlexXrmToolBoxMenuItem.Name = "CodePlexXrmToolBoxMenuItem";
            this.CodePlexXrmToolBoxMenuItem.Size = new System.Drawing.Size(385, 30);
            this.CodePlexXrmToolBoxMenuItem.Text = "XrmToolbox";
            // 
            // CodePlexPluginMenuItem
            // 
            this.CodePlexPluginMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reportABugPluginToolStripMenuItem,
            this.startADiscussionPluginToolStripMenuItem,
            this.rateThisToolPluginToolStripMenuItem});
            this.CodePlexPluginMenuItem.Name = "CodePlexPluginMenuItem";
            this.CodePlexPluginMenuItem.Size = new System.Drawing.Size(385, 30);
            this.CodePlexPluginMenuItem.Text = "Selected Plugin";
            // 
            // reportABugPluginToolStripMenuItem
            // 
            this.reportABugPluginToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("reportABugPluginToolStripMenuItem.Image")));
            this.reportABugPluginToolStripMenuItem.Name = "reportABugPluginToolStripMenuItem";
            this.reportABugPluginToolStripMenuItem.Size = new System.Drawing.Size(385, 30);
            this.reportABugPluginToolStripMenuItem.Text = "Report a bug";
            this.reportABugPluginToolStripMenuItem.Click += new System.EventHandler(this.TsbReportBugPluginClick);
            // 
            // startADiscussionPluginToolStripMenuItem
            // 
            this.startADiscussionPluginToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("startADiscussionPluginToolStripMenuItem.Image")));
            this.startADiscussionPluginToolStripMenuItem.Name = "startADiscussionPluginToolStripMenuItem";
            this.startADiscussionPluginToolStripMenuItem.Size = new System.Drawing.Size(385, 30);
            this.startADiscussionPluginToolStripMenuItem.Text = "Start a discussion / Request a feature";
            this.startADiscussionPluginToolStripMenuItem.Click += new System.EventHandler(this.TsbDiscussPluginClick);
            // 
            // rateThisToolPluginToolStripMenuItem
            // 
            this.rateThisToolPluginToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("rateThisToolPluginToolStripMenuItem.Image")));
            this.rateThisToolPluginToolStripMenuItem.Name = "rateThisToolPluginToolStripMenuItem";
            this.rateThisToolPluginToolStripMenuItem.Size = new System.Drawing.Size(385, 30);
            this.rateThisToolPluginToolStripMenuItem.Text = "Rate this tool";
            this.rateThisToolPluginToolStripMenuItem.Click += new System.EventHandler(this.TsbRatePluginClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 32);
            // 
            // tsddbDonate
            // 
            this.tsddbDonate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.donateInUSDollarsToolStripMenuItem,
            this.donateInEuroToolStripMenuItem,
            this.donateInGBPToolStripMenuItem});
            this.tsddbDonate.Image = ((System.Drawing.Image)(resources.GetObject("tsddbDonate.Image")));
            this.tsddbDonate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbDonate.Name = "tsddbDonate";
            this.tsddbDonate.Size = new System.Drawing.Size(112, 29);
            this.tsddbDonate.Text = "Donate";
            // 
            // donateInUSDollarsToolStripMenuItem
            // 
            this.donateInUSDollarsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("donateInUSDollarsToolStripMenuItem.Image")));
            this.donateInUSDollarsToolStripMenuItem.Name = "donateInUSDollarsToolStripMenuItem";
            this.donateInUSDollarsToolStripMenuItem.Size = new System.Drawing.Size(256, 30);
            this.donateInUSDollarsToolStripMenuItem.Text = "Donate in US Dollars";
            this.donateInUSDollarsToolStripMenuItem.Click += new System.EventHandler(this.donateInUSDollarsToolStripMenuItem_Click);
            // 
            // donateInEuroToolStripMenuItem
            // 
            this.donateInEuroToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("donateInEuroToolStripMenuItem.Image")));
            this.donateInEuroToolStripMenuItem.Name = "donateInEuroToolStripMenuItem";
            this.donateInEuroToolStripMenuItem.Size = new System.Drawing.Size(256, 30);
            this.donateInEuroToolStripMenuItem.Text = "Donate in Euro";
            this.donateInEuroToolStripMenuItem.Click += new System.EventHandler(this.donateInEuroToolStripMenuItem_Click);
            // 
            // donateInGBPToolStripMenuItem
            // 
            this.donateInGBPToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("donateInGBPToolStripMenuItem.Image")));
            this.donateInGBPToolStripMenuItem.Name = "donateInGBPToolStripMenuItem";
            this.donateInGBPToolStripMenuItem.Size = new System.Drawing.Size(256, 30);
            this.donateInGBPToolStripMenuItem.Text = "Donate in GBP";
            this.donateInGBPToolStripMenuItem.Click += new System.EventHandler(this.donateInGBPToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.HomePageTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 32);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1326, 910);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseClick);
            // 
            // HomePageTab
            // 
            this.HomePageTab.AutoScroll = true;
            this.HomePageTab.Controls.Add(this.pnlHelp);
            this.HomePageTab.Location = new System.Drawing.Point(4, 32);
            this.HomePageTab.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.HomePageTab.Name = "HomePageTab";
            this.HomePageTab.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.HomePageTab.Size = new System.Drawing.Size(1318, 874);
            this.HomePageTab.TabIndex = 0;
            this.HomePageTab.Text = "Home";
            this.HomePageTab.UseVisualStyleBackColor = true;
            // 
            // pnlHelp
            // 
            this.pnlHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHelp.Controls.Add(this.label2);
            this.pnlHelp.Controls.Add(this.label1);
            this.pnlHelp.Location = new System.Drawing.Point(4, 9);
            this.pnlHelp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlHelp.Name = "pnlHelp";
            this.pnlHelp.Size = new System.Drawing.Size(1298, 853);
            this.pnlHelp.TabIndex = 0;
            this.pnlHelp.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 80);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1286, 178);
            this.label2.TabIndex = 1;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(8, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1286, 52);
            this.label1.TabIndex = 0;
            this.label1.Text = "Oups... no plugin found!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1326, 942);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XrmToolBox for Microsoft Dynamics CRM 2011/2013/2015";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.HomePageTab.ResumeLayout(false);
            this.pnlHelp.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbConnect;
        private System.Windows.Forms.ToolStripButton tsbAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripDropDownButton tsbManageTabs;
        private System.Windows.Forms.ToolStripMenuItem closeAllTabsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllTabsExceptActiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripDropDownButton tsbCodePlex;
        private System.Windows.Forms.ToolStripMenuItem reportABugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startADiscussionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rateThisToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbOptions;
        private System.Windows.Forms.ToolStripDropDownButton tsddbDonate;
        private System.Windows.Forms.ToolStripMenuItem donateInUSDollarsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem donateInEuroToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbManageConnections;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem donateInGBPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeCurrentTabToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage HomePageTab;
        private System.Windows.Forms.Panel pnlHelp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem CodePlexXrmToolBoxMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CodePlexPluginMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportABugPluginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startADiscussionPluginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rateThisToolPluginToolStripMenuItem;
    }
}

