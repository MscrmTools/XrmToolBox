namespace XrmToolBox.ToolLibrary.Forms
{
    partial class ToolLibraryForm
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
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlToolsMain = new System.Windows.Forms.Panel();
            this.pnlLoading = new System.Windows.Forms.Panel();
            this.lblLoading = new System.Windows.Forms.Label();
            this.lvTools = new System.Windows.Forms.ListView();
            this.chCheckbox = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chContent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pnlToolsBottom = new System.Windows.Forms.Panel();
            this.pnlToolProperties = new System.Windows.Forms.Panel();
            this.pnlCustomRepoWarning = new System.Windows.Forms.Panel();
            this.lblCustomRepoWarning = new System.Windows.Forms.Label();
            this.lblToolsSearch = new System.Windows.Forms.Label();
            this.pnlToolsSearch = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblFilterCategory = new System.Windows.Forms.Label();
            this.pnlFilterCategory = new System.Windows.Forms.Panel();
            this.cbbCategories = new System.Windows.Forms.ComboBox();
            this.lblSeparator1 = new System.Windows.Forms.Label();
            this.pnlToolsTop = new System.Windows.Forms.Panel();
            this.llRepoMoreInfo = new System.Windows.Forms.LinkLabel();
            this.pnlFilterRepository = new System.Windows.Forms.Panel();
            this.cbbRepositories = new System.Windows.Forms.ComboBox();
            this.lblFilterRepository = new System.Windows.Forms.Label();
            this.lblSeparator2 = new System.Windows.Forms.Label();
            this.chkIncompatible = new System.Windows.Forms.CheckBox();
            this.chkShowUpdates = new System.Windows.Forms.CheckBox();
            this.chkShowInstalled = new System.Windows.Forms.CheckBox();
            this.chkToInstall = new System.Windows.Forms.CheckBox();
            this.chkFilterNew = new System.Windows.Forms.CheckBox();
            this.chkFilterTopRating = new System.Windows.Forms.CheckBox();
            this.chkFilterMvp = new System.Windows.Forms.CheckBox();
            this.chkFilterOpenSource = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tssRefresh = new System.Windows.Forms.ToolStripSeparator();
            this.tsbBulkInstall = new System.Windows.Forms.ToolStripButton();
            this.tsbBulkDelete = new System.Windows.Forms.ToolStripButton();
            this.tssDelete = new System.Windows.Forms.ToolStripSeparator();
            this.tsddActions = new System.Windows.Forms.ToolStripDropDownButton();
            this.tssReloadImageCache = new System.Windows.Forms.ToolStripMenuItem();
            this.tssClearPackageCache = new System.Windows.Forms.ToolStripMenuItem();
            this.tssSettings = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRestart = new System.Windows.Forms.ToolStripButton();
            this.pnlFilterUpdateInfo = new System.Windows.Forms.Panel();
            this.llApplyUserFilter = new System.Windows.Forms.LinkLabel();
            this.lblFilterUpdateInfo = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlToolsMain.SuspendLayout();
            this.pnlLoading.SuspendLayout();
            this.pnlCustomRepoWarning.SuspendLayout();
            this.pnlToolsSearch.SuspendLayout();
            this.pnlFilterCategory.SuspendLayout();
            this.pnlToolsTop.SuspendLayout();
            this.pnlFilterRepository.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pnlFilterUpdateInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 1336);
            this.pnlBottom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(2375, 86);
            this.pnlBottom.TabIndex = 1;
            this.pnlBottom.Visible = false;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.splitContainer1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 138);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(2375, 1198);
            this.pnlMain.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnlToolsMain);
            this.splitContainer1.Panel1.Controls.Add(this.pnlToolsBottom);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlToolProperties);
            this.splitContainer1.Panel2.Controls.Add(this.pnlCustomRepoWarning);
            this.splitContainer1.Panel2MinSize = 400;
            this.splitContainer1.Size = new System.Drawing.Size(2375, 1198);
            this.splitContainer1.SplitterDistance = 1724;
            this.splitContainer1.SplitterWidth = 12;
            this.splitContainer1.TabIndex = 0;
            // 
            // pnlToolsMain
            // 
            this.pnlToolsMain.AutoScroll = true;
            this.pnlToolsMain.Controls.Add(this.pnlLoading);
            this.pnlToolsMain.Controls.Add(this.lvTools);
            this.pnlToolsMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlToolsMain.Location = new System.Drawing.Point(0, 0);
            this.pnlToolsMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlToolsMain.Name = "pnlToolsMain";
            this.pnlToolsMain.Size = new System.Drawing.Size(1724, 1123);
            this.pnlToolsMain.TabIndex = 2;
            // 
            // pnlLoading
            // 
            this.pnlLoading.Controls.Add(this.lblLoading);
            this.pnlLoading.Location = new System.Drawing.Point(897, 249);
            this.pnlLoading.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlLoading.Name = "pnlLoading";
            this.pnlLoading.Size = new System.Drawing.Size(909, 140);
            this.pnlLoading.TabIndex = 3;
            // 
            // lblLoading
            // 
            this.lblLoading.BackColor = System.Drawing.SystemColors.Info;
            this.lblLoading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLoading.Location = new System.Drawing.Point(0, 0);
            this.lblLoading.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(909, 140);
            this.lblLoading.TabIndex = 0;
            this.lblLoading.Text = "label1";
            this.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lvTools
            // 
            this.lvTools.CheckBoxes = true;
            this.lvTools.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chCheckbox,
            this.chContent});
            this.lvTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvTools.FullRowSelect = true;
            this.lvTools.HideSelection = false;
            this.lvTools.Location = new System.Drawing.Point(0, 0);
            this.lvTools.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lvTools.Name = "lvTools";
            this.lvTools.OwnerDraw = true;
            this.lvTools.Size = new System.Drawing.Size(1806, 1097);
            this.lvTools.SmallImageList = this.imageList1;
            this.lvTools.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvTools.TabIndex = 3;
            this.lvTools.UseCompatibleStateImageBehavior = false;
            this.lvTools.View = System.Windows.Forms.View.Details;
            this.lvTools.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvTools_ColumnClick);
            this.lvTools.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lvTools_DrawColumnHeader);
            this.lvTools.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lvTools_DrawSubItem);
            this.lvTools.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvTools_ItemChecked);
            this.lvTools.SelectedIndexChanged += new System.EventHandler(this.lvTools_SelectedIndexChanged);
            this.lvTools.Resize += new System.EventHandler(this.lvTools_Resize);
            // 
            // chCheckbox
            // 
            this.chCheckbox.Width = 30;
            // 
            // chContent
            // 
            this.chContent.Text = "";
            this.chContent.Width = 400;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(48, 48);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pnlToolsBottom
            // 
            this.pnlToolsBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlToolsBottom.Location = new System.Drawing.Point(0, 1123);
            this.pnlToolsBottom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlToolsBottom.Name = "pnlToolsBottom";
            this.pnlToolsBottom.Size = new System.Drawing.Size(1724, 75);
            this.pnlToolsBottom.TabIndex = 1;
            this.pnlToolsBottom.Visible = false;
            // 
            // pnlToolProperties
            // 
            this.pnlToolProperties.AutoScroll = true;
            this.pnlToolProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlToolProperties.Location = new System.Drawing.Point(0, 0);
            this.pnlToolProperties.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlToolProperties.Name = "pnlToolProperties";
            this.pnlToolProperties.Size = new System.Drawing.Size(639, 1123);
            this.pnlToolProperties.TabIndex = 2;
            // 
            // pnlCustomRepoWarning
            // 
            this.pnlCustomRepoWarning.BackColor = System.Drawing.SystemColors.Info;
            this.pnlCustomRepoWarning.Controls.Add(this.lblCustomRepoWarning);
            this.pnlCustomRepoWarning.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCustomRepoWarning.Location = new System.Drawing.Point(0, 1123);
            this.pnlCustomRepoWarning.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlCustomRepoWarning.Name = "pnlCustomRepoWarning";
            this.pnlCustomRepoWarning.Size = new System.Drawing.Size(639, 75);
            this.pnlCustomRepoWarning.TabIndex = 1;
            this.pnlCustomRepoWarning.Visible = false;
            // 
            // lblCustomRepoWarning
            // 
            this.lblCustomRepoWarning.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCustomRepoWarning.Location = new System.Drawing.Point(0, 0);
            this.lblCustomRepoWarning.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCustomRepoWarning.Name = "lblCustomRepoWarning";
            this.lblCustomRepoWarning.Size = new System.Drawing.Size(639, 75);
            this.lblCustomRepoWarning.TabIndex = 0;
            this.lblCustomRepoWarning.Text = "This tool comes from a custom repository. XrmToolBox cannot validate that the inf" +
    "ormation displayed are accurate.";
            // 
            // lblToolsSearch
            // 
            this.lblToolsSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblToolsSearch.Location = new System.Drawing.Point(0, 0);
            this.lblToolsSearch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblToolsSearch.Name = "lblToolsSearch";
            this.lblToolsSearch.Size = new System.Drawing.Size(70, 55);
            this.lblToolsSearch.TabIndex = 0;
            this.lblToolsSearch.Text = "Search";
            this.lblToolsSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlToolsSearch
            // 
            this.pnlToolsSearch.Controls.Add(this.txtSearch);
            this.pnlToolsSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlToolsSearch.Location = new System.Drawing.Point(70, 0);
            this.pnlToolsSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlToolsSearch.Name = "pnlToolsSearch";
            this.pnlToolsSearch.Size = new System.Drawing.Size(340, 55);
            this.pnlToolsSearch.TabIndex = 2;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(8, 16);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(320, 26);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblFilterCategory
            // 
            this.lblFilterCategory.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFilterCategory.Location = new System.Drawing.Point(410, 0);
            this.lblFilterCategory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFilterCategory.Name = "lblFilterCategory";
            this.lblFilterCategory.Size = new System.Drawing.Size(94, 55);
            this.lblFilterCategory.TabIndex = 7;
            this.lblFilterCategory.Text = "in category";
            this.lblFilterCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlFilterCategory
            // 
            this.pnlFilterCategory.Controls.Add(this.cbbCategories);
            this.pnlFilterCategory.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlFilterCategory.Location = new System.Drawing.Point(504, 0);
            this.pnlFilterCategory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlFilterCategory.Name = "pnlFilterCategory";
            this.pnlFilterCategory.Size = new System.Drawing.Size(250, 55);
            this.pnlFilterCategory.TabIndex = 8;
            // 
            // cbbCategories
            // 
            this.cbbCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCategories.FormattingEnabled = true;
            this.cbbCategories.Location = new System.Drawing.Point(8, 13);
            this.cbbCategories.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbbCategories.Name = "cbbCategories";
            this.cbbCategories.Size = new System.Drawing.Size(230, 28);
            this.cbbCategories.TabIndex = 9;
            this.cbbCategories.SelectedIndexChanged += new System.EventHandler(this.cbbCategories_SelectedIndexChanged);
            // 
            // lblSeparator1
            // 
            this.lblSeparator1.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSeparator1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeparator1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblSeparator1.Location = new System.Drawing.Point(1102, 0);
            this.lblSeparator1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSeparator1.Name = "lblSeparator1";
            this.lblSeparator1.Size = new System.Drawing.Size(24, 55);
            this.lblSeparator1.TabIndex = 9;
            this.lblSeparator1.Text = "| ";
            this.lblSeparator1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlToolsTop
            // 
            this.pnlToolsTop.Controls.Add(this.llRepoMoreInfo);
            this.pnlToolsTop.Controls.Add(this.pnlFilterRepository);
            this.pnlToolsTop.Controls.Add(this.lblFilterRepository);
            this.pnlToolsTop.Controls.Add(this.lblSeparator2);
            this.pnlToolsTop.Controls.Add(this.chkIncompatible);
            this.pnlToolsTop.Controls.Add(this.chkShowUpdates);
            this.pnlToolsTop.Controls.Add(this.chkShowInstalled);
            this.pnlToolsTop.Controls.Add(this.chkToInstall);
            this.pnlToolsTop.Controls.Add(this.lblSeparator1);
            this.pnlToolsTop.Controls.Add(this.chkFilterNew);
            this.pnlToolsTop.Controls.Add(this.chkFilterTopRating);
            this.pnlToolsTop.Controls.Add(this.chkFilterMvp);
            this.pnlToolsTop.Controls.Add(this.chkFilterOpenSource);
            this.pnlToolsTop.Controls.Add(this.pnlFilterCategory);
            this.pnlToolsTop.Controls.Add(this.lblFilterCategory);
            this.pnlToolsTop.Controls.Add(this.pnlToolsSearch);
            this.pnlToolsTop.Controls.Add(this.lblToolsSearch);
            this.pnlToolsTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolsTop.Location = new System.Drawing.Point(0, 83);
            this.pnlToolsTop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlToolsTop.Name = "pnlToolsTop";
            this.pnlToolsTop.Size = new System.Drawing.Size(2375, 55);
            this.pnlToolsTop.TabIndex = 0;
            // 
            // llRepoMoreInfo
            // 
            this.llRepoMoreInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.llRepoMoreInfo.Location = new System.Drawing.Point(1771, 0);
            this.llRepoMoreInfo.Name = "llRepoMoreInfo";
            this.llRepoMoreInfo.Size = new System.Drawing.Size(105, 55);
            this.llRepoMoreInfo.TabIndex = 17;
            this.llRepoMoreInfo.TabStop = true;
            this.llRepoMoreInfo.Text = "More info";
            this.llRepoMoreInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.llRepoMoreInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llRepoMoreInfo_LinkClicked);
            // 
            // pnlFilterRepository
            // 
            this.pnlFilterRepository.Controls.Add(this.cbbRepositories);
            this.pnlFilterRepository.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlFilterRepository.Location = new System.Drawing.Point(1521, 0);
            this.pnlFilterRepository.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlFilterRepository.Name = "pnlFilterRepository";
            this.pnlFilterRepository.Size = new System.Drawing.Size(250, 55);
            this.pnlFilterRepository.TabIndex = 16;
            // 
            // cbbRepositories
            // 
            this.cbbRepositories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbRepositories.FormattingEnabled = true;
            this.cbbRepositories.Location = new System.Drawing.Point(8, 14);
            this.cbbRepositories.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbbRepositories.Name = "cbbRepositories";
            this.cbbRepositories.Size = new System.Drawing.Size(230, 28);
            this.cbbRepositories.TabIndex = 9;
            // 
            // lblFilterRepository
            // 
            this.lblFilterRepository.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFilterRepository.Location = new System.Drawing.Point(1429, 0);
            this.lblFilterRepository.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFilterRepository.Name = "lblFilterRepository";
            this.lblFilterRepository.Size = new System.Drawing.Size(92, 55);
            this.lblFilterRepository.TabIndex = 15;
            this.lblFilterRepository.Text = "Repository";
            this.lblFilterRepository.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSeparator2
            // 
            this.lblSeparator2.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSeparator2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeparator2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblSeparator2.Location = new System.Drawing.Point(1407, 0);
            this.lblSeparator2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSeparator2.Name = "lblSeparator2";
            this.lblSeparator2.Size = new System.Drawing.Size(22, 55);
            this.lblSeparator2.TabIndex = 14;
            this.lblSeparator2.Text = "| ";
            this.lblSeparator2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkIncompatible
            // 
            this.chkIncompatible.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkIncompatible.Image = global::XrmToolBox.ToolLibrary.Resource.Incompatible;
            this.chkIncompatible.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chkIncompatible.Location = new System.Drawing.Point(1339, 0);
            this.chkIncompatible.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkIncompatible.Name = "chkIncompatible";
            this.chkIncompatible.Size = new System.Drawing.Size(68, 55);
            this.chkIncompatible.TabIndex = 12;
            this.chkIncompatible.UseVisualStyleBackColor = true;
            this.chkIncompatible.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // chkShowUpdates
            // 
            this.chkShowUpdates.Checked = true;
            this.chkShowUpdates.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowUpdates.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkShowUpdates.Image = global::XrmToolBox.ToolLibrary.Resource.Update;
            this.chkShowUpdates.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chkShowUpdates.Location = new System.Drawing.Point(1271, 0);
            this.chkShowUpdates.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkShowUpdates.Name = "chkShowUpdates";
            this.chkShowUpdates.Size = new System.Drawing.Size(68, 55);
            this.chkShowUpdates.TabIndex = 11;
            this.chkShowUpdates.UseVisualStyleBackColor = true;
            this.chkShowUpdates.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // chkShowInstalled
            // 
            this.chkShowInstalled.Checked = true;
            this.chkShowInstalled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowInstalled.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkShowInstalled.Image = global::XrmToolBox.ToolLibrary.Resource.Success;
            this.chkShowInstalled.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chkShowInstalled.Location = new System.Drawing.Point(1194, 0);
            this.chkShowInstalled.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkShowInstalled.Name = "chkShowInstalled";
            this.chkShowInstalled.Size = new System.Drawing.Size(77, 55);
            this.chkShowInstalled.TabIndex = 10;
            this.chkShowInstalled.UseVisualStyleBackColor = true;
            this.chkShowInstalled.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // chkToInstall
            // 
            this.chkToInstall.Checked = true;
            this.chkToInstall.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkToInstall.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkToInstall.Image = global::XrmToolBox.ToolLibrary.Resource.NewToInstall;
            this.chkToInstall.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chkToInstall.Location = new System.Drawing.Point(1126, 0);
            this.chkToInstall.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkToInstall.Name = "chkToInstall";
            this.chkToInstall.Size = new System.Drawing.Size(68, 55);
            this.chkToInstall.TabIndex = 13;
            this.chkToInstall.UseVisualStyleBackColor = true;
            this.chkToInstall.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // chkFilterNew
            // 
            this.chkFilterNew.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkFilterNew.Image = global::XrmToolBox.ToolLibrary.Resource.New;
            this.chkFilterNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chkFilterNew.Location = new System.Drawing.Point(1015, 0);
            this.chkFilterNew.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkFilterNew.Name = "chkFilterNew";
            this.chkFilterNew.Size = new System.Drawing.Size(87, 55);
            this.chkFilterNew.TabIndex = 6;
            this.chkFilterNew.UseVisualStyleBackColor = true;
            this.chkFilterNew.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // chkFilterTopRating
            // 
            this.chkFilterTopRating.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkFilterTopRating.Image = global::XrmToolBox.ToolLibrary.Resource.star;
            this.chkFilterTopRating.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chkFilterTopRating.Location = new System.Drawing.Point(928, 0);
            this.chkFilterTopRating.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkFilterTopRating.Name = "chkFilterTopRating";
            this.chkFilterTopRating.Size = new System.Drawing.Size(87, 55);
            this.chkFilterTopRating.TabIndex = 5;
            this.chkFilterTopRating.UseVisualStyleBackColor = true;
            this.chkFilterTopRating.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // chkFilterMvp
            // 
            this.chkFilterMvp.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkFilterMvp.Image = global::XrmToolBox.ToolLibrary.Resource.mvp;
            this.chkFilterMvp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chkFilterMvp.Location = new System.Drawing.Point(841, 0);
            this.chkFilterMvp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkFilterMvp.Name = "chkFilterMvp";
            this.chkFilterMvp.Size = new System.Drawing.Size(87, 55);
            this.chkFilterMvp.TabIndex = 4;
            this.chkFilterMvp.UseVisualStyleBackColor = true;
            this.chkFilterMvp.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // chkFilterOpenSource
            // 
            this.chkFilterOpenSource.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkFilterOpenSource.Image = global::XrmToolBox.ToolLibrary.Resource.github;
            this.chkFilterOpenSource.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chkFilterOpenSource.Location = new System.Drawing.Point(754, 0);
            this.chkFilterOpenSource.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkFilterOpenSource.Name = "chkFilterOpenSource";
            this.chkFilterOpenSource.Size = new System.Drawing.Size(87, 55);
            this.chkFilterOpenSource.TabIndex = 3;
            this.chkFilterOpenSource.UseVisualStyleBackColor = true;
            this.chkFilterOpenSource.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRefresh,
            this.tssRefresh,
            this.tsbBulkInstall,
            this.tsbBulkDelete,
            this.tssDelete,
            this.tsddActions,
            this.tssSettings,
            this.tsbRestart});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip1.Size = new System.Drawing.Size(2375, 34);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "tsMain";
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.Image = global::XrmToolBox.ToolLibrary.Resource.Refresh;
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(98, 29);
            this.tsbRefresh.Text = "Refresh";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // tssRefresh
            // 
            this.tssRefresh.Name = "tssRefresh";
            this.tssRefresh.Size = new System.Drawing.Size(6, 34);
            // 
            // tsbBulkInstall
            // 
            this.tsbBulkInstall.Enabled = false;
            this.tsbBulkInstall.Image = global::XrmToolBox.ToolLibrary.Resource.Install;
            this.tsbBulkInstall.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBulkInstall.Name = "tsbBulkInstall";
            this.tsbBulkInstall.Size = new System.Drawing.Size(86, 29);
            this.tsbBulkInstall.Text = "Install";
            this.tsbBulkInstall.Click += new System.EventHandler(this.tsbBulkInstall_Click);
            // 
            // tsbBulkDelete
            // 
            this.tsbBulkDelete.Enabled = false;
            this.tsbBulkDelete.Image = global::XrmToolBox.ToolLibrary.Resource.Uninstall;
            this.tsbBulkDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBulkDelete.Name = "tsbBulkDelete";
            this.tsbBulkDelete.Size = new System.Drawing.Size(107, 29);
            this.tsbBulkDelete.Text = "Uninstall";
            this.tsbBulkDelete.Click += new System.EventHandler(this.tsbBulkDelete_Click);
            // 
            // tssDelete
            // 
            this.tssDelete.Name = "tssDelete";
            this.tssDelete.Size = new System.Drawing.Size(6, 34);
            // 
            // tsddActions
            // 
            this.tsddActions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssReloadImageCache,
            this.tssClearPackageCache});
            this.tsddActions.Image = global::XrmToolBox.ToolLibrary.Resource.Settings;
            this.tsddActions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddActions.Name = "tsddActions";
            this.tsddActions.Size = new System.Drawing.Size(133, 29);
            this.tsddActions.Text = "Advanced";
            this.tsddActions.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsddActions_DropDownItemClicked);
            // 
            // tssReloadImageCache
            // 
            this.tssReloadImageCache.Image = global::XrmToolBox.ToolLibrary.Resource.RefreshImages;
            this.tssReloadImageCache.Name = "tssReloadImageCache";
            this.tssReloadImageCache.Size = new System.Drawing.Size(279, 34);
            this.tssReloadImageCache.Text = "Reload Image cache";
            // 
            // tssClearPackageCache
            // 
            this.tssClearPackageCache.Image = global::XrmToolBox.ToolLibrary.Resource.ClearCache;
            this.tssClearPackageCache.Name = "tssClearPackageCache";
            this.tssClearPackageCache.Size = new System.Drawing.Size(279, 34);
            this.tssClearPackageCache.Text = "Clear Packages cache";
            // 
            // tssSettings
            // 
            this.tssSettings.Name = "tssSettings";
            this.tssSettings.Size = new System.Drawing.Size(6, 34);
            this.tssSettings.Visible = false;
            // 
            // tsbRestart
            // 
            this.tsbRestart.Image = global::XrmToolBox.ToolLibrary.Resource.Restart;
            this.tsbRestart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRestart.Name = "tsbRestart";
            this.tsbRestart.Size = new System.Drawing.Size(94, 29);
            this.tsbRestart.Text = "Restart";
            this.tsbRestart.Visible = false;
            this.tsbRestart.Click += new System.EventHandler(this.tsbRestart_Click);
            // 
            // pnlFilterUpdateInfo
            // 
            this.pnlFilterUpdateInfo.BackColor = System.Drawing.SystemColors.Info;
            this.pnlFilterUpdateInfo.Controls.Add(this.llApplyUserFilter);
            this.pnlFilterUpdateInfo.Controls.Add(this.lblFilterUpdateInfo);
            this.pnlFilterUpdateInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilterUpdateInfo.Location = new System.Drawing.Point(0, 34);
            this.pnlFilterUpdateInfo.Name = "pnlFilterUpdateInfo";
            this.pnlFilterUpdateInfo.Padding = new System.Windows.Forms.Padding(10);
            this.pnlFilterUpdateInfo.Size = new System.Drawing.Size(2375, 49);
            this.pnlFilterUpdateInfo.TabIndex = 5;
            this.pnlFilterUpdateInfo.Visible = false;
            // 
            // llApplyUserFilter
            // 
            this.llApplyUserFilter.Dock = System.Windows.Forms.DockStyle.Left;
            this.llApplyUserFilter.Location = new System.Drawing.Point(788, 10);
            this.llApplyUserFilter.Name = "llApplyUserFilter";
            this.llApplyUserFilter.Size = new System.Drawing.Size(507, 29);
            this.llApplyUserFilter.TabIndex = 1;
            this.llApplyUserFilter.TabStop = true;
            this.llApplyUserFilter.Text = "Click here to apply your last filters";
            this.llApplyUserFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.llApplyUserFilter.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llApplyUserFilter_LinkClicked);
            // 
            // lblFilterUpdateInfo
            // 
            this.lblFilterUpdateInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFilterUpdateInfo.Location = new System.Drawing.Point(10, 10);
            this.lblFilterUpdateInfo.Name = "lblFilterUpdateInfo";
            this.lblFilterUpdateInfo.Size = new System.Drawing.Size(778, 29);
            this.lblFilterUpdateInfo.TabIndex = 0;
            this.lblFilterUpdateInfo.Text = "Tool library is currently using a specific filter to show you only updates availa" +
    "ble for your tools.";
            this.lblFilterUpdateInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ToolLibraryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2375, 1422);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlToolsTop);
            this.Controls.Add(this.pnlFilterUpdateInfo);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ToolLibraryForm";
            this.Text = "Tool Library";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlMain.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlToolsMain.ResumeLayout(false);
            this.pnlLoading.ResumeLayout(false);
            this.pnlCustomRepoWarning.ResumeLayout(false);
            this.pnlToolsSearch.ResumeLayout(false);
            this.pnlToolsSearch.PerformLayout();
            this.pnlFilterCategory.ResumeLayout(false);
            this.pnlToolsTop.ResumeLayout(false);
            this.pnlFilterRepository.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlFilterUpdateInfo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnlToolsMain;
        private System.Windows.Forms.Panel pnlToolsBottom;
        private System.Windows.Forms.ListView lvTools;
        private System.Windows.Forms.ColumnHeader chContent;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label lblToolsSearch;
        private System.Windows.Forms.Panel pnlToolsSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblFilterCategory;
        private System.Windows.Forms.Panel pnlFilterCategory;
        private System.Windows.Forms.ComboBox cbbCategories;
        private System.Windows.Forms.CheckBox chkFilterOpenSource;
        private System.Windows.Forms.CheckBox chkFilterMvp;
        private System.Windows.Forms.CheckBox chkFilterTopRating;
        private System.Windows.Forms.CheckBox chkFilterNew;
        private System.Windows.Forms.Label lblSeparator1;
        private System.Windows.Forms.Panel pnlToolsTop;
        private System.Windows.Forms.CheckBox chkShowUpdates;
        private System.Windows.Forms.CheckBox chkShowInstalled;
        private System.Windows.Forms.ColumnHeader chCheckbox;
        private System.Windows.Forms.Panel pnlLoading;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.ToolStripSeparator tssRefresh;
        private System.Windows.Forms.ToolStripButton tsbBulkInstall;
        private System.Windows.Forms.ToolStripButton tsbBulkDelete;
        private System.Windows.Forms.CheckBox chkIncompatible;
        private System.Windows.Forms.ToolStripSeparator tssDelete;
        private System.Windows.Forms.ToolStripDropDownButton tsddActions;
        private System.Windows.Forms.ToolStripMenuItem tssReloadImageCache;
        private System.Windows.Forms.ToolStripMenuItem tssClearPackageCache;
        private System.Windows.Forms.CheckBox chkToInstall;
        private System.Windows.Forms.ToolStripSeparator tssSettings;
        private System.Windows.Forms.ToolStripButton tsbRestart;
        private System.Windows.Forms.Panel pnlFilterRepository;
        private System.Windows.Forms.ComboBox cbbRepositories;
        private System.Windows.Forms.Label lblFilterRepository;
        private System.Windows.Forms.Label lblSeparator2;
        private System.Windows.Forms.Panel pnlToolProperties;
        private System.Windows.Forms.Panel pnlCustomRepoWarning;
        private System.Windows.Forms.Label lblCustomRepoWarning;
        private System.Windows.Forms.LinkLabel llRepoMoreInfo;
        private System.Windows.Forms.Panel pnlFilterUpdateInfo;
        private System.Windows.Forms.LinkLabel llApplyUserFilter;
        private System.Windows.Forms.Label lblFilterUpdateInfo;
    }
}

