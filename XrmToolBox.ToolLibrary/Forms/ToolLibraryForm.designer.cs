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
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 872);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1624, 56);
            this.pnlBottom.TabIndex = 1;
            this.pnlBottom.Visible = false;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.splitContainer1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 81);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1624, 791);
            this.pnlMain.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
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
            this.splitContainer1.Size = new System.Drawing.Size(1624, 791);
            this.splitContainer1.SplitterDistance = 1180;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 0;
            // 
            // pnlToolsMain
            // 
            this.pnlToolsMain.AutoScroll = true;
            this.pnlToolsMain.Controls.Add(this.pnlLoading);
            this.pnlToolsMain.Controls.Add(this.lvTools);
            this.pnlToolsMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlToolsMain.Location = new System.Drawing.Point(0, 0);
            this.pnlToolsMain.Name = "pnlToolsMain";
            this.pnlToolsMain.Size = new System.Drawing.Size(1180, 742);
            this.pnlToolsMain.TabIndex = 2;
            // 
            // pnlLoading
            // 
            this.pnlLoading.Controls.Add(this.lblLoading);
            this.pnlLoading.Location = new System.Drawing.Point(598, 162);
            this.pnlLoading.Name = "pnlLoading";
            this.pnlLoading.Size = new System.Drawing.Size(606, 91);
            this.pnlLoading.TabIndex = 3;
            // 
            // lblLoading
            // 
            this.lblLoading.BackColor = System.Drawing.SystemColors.Info;
            this.lblLoading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLoading.Location = new System.Drawing.Point(0, 0);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(606, 91);
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
            this.lvTools.Name = "lvTools";
            this.lvTools.OwnerDraw = true;
            this.lvTools.Size = new System.Drawing.Size(1204, 725);
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
            this.pnlToolsBottom.Location = new System.Drawing.Point(0, 742);
            this.pnlToolsBottom.Name = "pnlToolsBottom";
            this.pnlToolsBottom.Size = new System.Drawing.Size(1180, 49);
            this.pnlToolsBottom.TabIndex = 1;
            this.pnlToolsBottom.Visible = false;
            // 
            // pnlToolProperties
            // 
            this.pnlToolProperties.AutoScroll = true;
            this.pnlToolProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlToolProperties.Location = new System.Drawing.Point(0, 0);
            this.pnlToolProperties.Name = "pnlToolProperties";
            this.pnlToolProperties.Size = new System.Drawing.Size(436, 742);
            this.pnlToolProperties.TabIndex = 2;
            // 
            // pnlCustomRepoWarning
            // 
            this.pnlCustomRepoWarning.BackColor = System.Drawing.SystemColors.Info;
            this.pnlCustomRepoWarning.Controls.Add(this.lblCustomRepoWarning);
            this.pnlCustomRepoWarning.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCustomRepoWarning.Location = new System.Drawing.Point(0, 742);
            this.pnlCustomRepoWarning.Name = "pnlCustomRepoWarning";
            this.pnlCustomRepoWarning.Size = new System.Drawing.Size(436, 49);
            this.pnlCustomRepoWarning.TabIndex = 1;
            this.pnlCustomRepoWarning.Visible = false;
            // 
            // lblCustomRepoWarning
            // 
            this.lblCustomRepoWarning.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCustomRepoWarning.Location = new System.Drawing.Point(0, 0);
            this.lblCustomRepoWarning.Name = "lblCustomRepoWarning";
            this.lblCustomRepoWarning.Size = new System.Drawing.Size(436, 49);
            this.lblCustomRepoWarning.TabIndex = 0;
            this.lblCustomRepoWarning.Text = "This tool comes from a custom repository. XrmToolBox cannot validate that the inf" +
    "ormation displayed are accurate.";
            // 
            // lblToolsSearch
            // 
            this.lblToolsSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblToolsSearch.Location = new System.Drawing.Point(0, 0);
            this.lblToolsSearch.Name = "lblToolsSearch";
            this.lblToolsSearch.Size = new System.Drawing.Size(47, 42);
            this.lblToolsSearch.TabIndex = 0;
            this.lblToolsSearch.Text = "Search";
            this.lblToolsSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlToolsSearch
            // 
            this.pnlToolsSearch.Controls.Add(this.txtSearch);
            this.pnlToolsSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlToolsSearch.Location = new System.Drawing.Point(47, 0);
            this.pnlToolsSearch.Name = "pnlToolsSearch";
            this.pnlToolsSearch.Size = new System.Drawing.Size(227, 42);
            this.pnlToolsSearch.TabIndex = 2;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(6, 10);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(215, 20);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblFilterCategory
            // 
            this.lblFilterCategory.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFilterCategory.Location = new System.Drawing.Point(274, 0);
            this.lblFilterCategory.Name = "lblFilterCategory";
            this.lblFilterCategory.Size = new System.Drawing.Size(63, 42);
            this.lblFilterCategory.TabIndex = 7;
            this.lblFilterCategory.Text = "in category";
            this.lblFilterCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlFilterCategory
            // 
            this.pnlFilterCategory.Controls.Add(this.cbbCategories);
            this.pnlFilterCategory.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlFilterCategory.Location = new System.Drawing.Point(337, 0);
            this.pnlFilterCategory.Name = "pnlFilterCategory";
            this.pnlFilterCategory.Size = new System.Drawing.Size(167, 42);
            this.pnlFilterCategory.TabIndex = 8;
            // 
            // cbbCategories
            // 
            this.cbbCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCategories.FormattingEnabled = true;
            this.cbbCategories.Location = new System.Drawing.Point(6, 9);
            this.cbbCategories.Name = "cbbCategories";
            this.cbbCategories.Size = new System.Drawing.Size(155, 21);
            this.cbbCategories.TabIndex = 9;
            this.cbbCategories.SelectedIndexChanged += new System.EventHandler(this.cbbCategories_SelectedIndexChanged);
            // 
            // lblSeparator1
            // 
            this.lblSeparator1.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSeparator1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeparator1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblSeparator1.Location = new System.Drawing.Point(764, 0);
            this.lblSeparator1.Name = "lblSeparator1";
            this.lblSeparator1.Size = new System.Drawing.Size(16, 42);
            this.lblSeparator1.TabIndex = 9;
            this.lblSeparator1.Text = "| ";
            // 
            // pnlToolsTop
            // 
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
            this.pnlToolsTop.Location = new System.Drawing.Point(0, 39);
            this.pnlToolsTop.Name = "pnlToolsTop";
            this.pnlToolsTop.Size = new System.Drawing.Size(1624, 42);
            this.pnlToolsTop.TabIndex = 0;
            // 
            // pnlFilterRepository
            // 
            this.pnlFilterRepository.Controls.Add(this.cbbRepositories);
            this.pnlFilterRepository.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlFilterRepository.Location = new System.Drawing.Point(1112, 0);
            this.pnlFilterRepository.Name = "pnlFilterRepository";
            this.pnlFilterRepository.Size = new System.Drawing.Size(167, 42);
            this.pnlFilterRepository.TabIndex = 16;
            // 
            // cbbRepositories
            // 
            this.cbbRepositories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbRepositories.FormattingEnabled = true;
            this.cbbRepositories.Location = new System.Drawing.Point(6, 9);
            this.cbbRepositories.Name = "cbbRepositories";
            this.cbbRepositories.Size = new System.Drawing.Size(155, 21);
            this.cbbRepositories.TabIndex = 9;
            // 
            // lblFilterRepository
            // 
            this.lblFilterRepository.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFilterRepository.Location = new System.Drawing.Point(1051, 0);
            this.lblFilterRepository.Name = "lblFilterRepository";
            this.lblFilterRepository.Size = new System.Drawing.Size(61, 42);
            this.lblFilterRepository.TabIndex = 15;
            this.lblFilterRepository.Text = "Repository";
            this.lblFilterRepository.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSeparator2
            // 
            this.lblSeparator2.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSeparator2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeparator2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblSeparator2.Location = new System.Drawing.Point(1036, 0);
            this.lblSeparator2.Name = "lblSeparator2";
            this.lblSeparator2.Size = new System.Drawing.Size(15, 42);
            this.lblSeparator2.TabIndex = 14;
            this.lblSeparator2.Text = "| ";
            // 
            // chkIncompatible
            // 
            this.chkIncompatible.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkIncompatible.Image = global::XrmToolBox.ToolLibrary.Resource.Incompatible;
            this.chkIncompatible.Location = new System.Drawing.Point(972, 0);
            this.chkIncompatible.Name = "chkIncompatible";
            this.chkIncompatible.Size = new System.Drawing.Size(64, 42);
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
            this.chkShowUpdates.Location = new System.Drawing.Point(908, 0);
            this.chkShowUpdates.Name = "chkShowUpdates";
            this.chkShowUpdates.Size = new System.Drawing.Size(64, 42);
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
            this.chkShowInstalled.Location = new System.Drawing.Point(844, 0);
            this.chkShowInstalled.Name = "chkShowInstalled";
            this.chkShowInstalled.Size = new System.Drawing.Size(64, 42);
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
            this.chkToInstall.Location = new System.Drawing.Point(780, 0);
            this.chkToInstall.Name = "chkToInstall";
            this.chkToInstall.Size = new System.Drawing.Size(64, 42);
            this.chkToInstall.TabIndex = 13;
            this.chkToInstall.UseVisualStyleBackColor = true;
            this.chkToInstall.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // chkFilterNew
            // 
            this.chkFilterNew.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkFilterNew.Image = global::XrmToolBox.ToolLibrary.Resource.New;
            this.chkFilterNew.Location = new System.Drawing.Point(700, 0);
            this.chkFilterNew.Name = "chkFilterNew";
            this.chkFilterNew.Size = new System.Drawing.Size(64, 42);
            this.chkFilterNew.TabIndex = 6;
            this.chkFilterNew.UseVisualStyleBackColor = true;
            this.chkFilterNew.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // chkFilterTopRating
            // 
            this.chkFilterTopRating.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkFilterTopRating.Image = global::XrmToolBox.ToolLibrary.Resource.star;
            this.chkFilterTopRating.Location = new System.Drawing.Point(636, 0);
            this.chkFilterTopRating.Name = "chkFilterTopRating";
            this.chkFilterTopRating.Size = new System.Drawing.Size(64, 42);
            this.chkFilterTopRating.TabIndex = 5;
            this.chkFilterTopRating.UseVisualStyleBackColor = true;
            this.chkFilterTopRating.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // chkFilterMvp
            // 
            this.chkFilterMvp.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkFilterMvp.Image = global::XrmToolBox.ToolLibrary.Resource.mvp;
            this.chkFilterMvp.Location = new System.Drawing.Point(572, 0);
            this.chkFilterMvp.Name = "chkFilterMvp";
            this.chkFilterMvp.Size = new System.Drawing.Size(64, 42);
            this.chkFilterMvp.TabIndex = 4;
            this.chkFilterMvp.UseVisualStyleBackColor = true;
            this.chkFilterMvp.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // chkFilterOpenSource
            // 
            this.chkFilterOpenSource.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkFilterOpenSource.Image = global::XrmToolBox.ToolLibrary.Resource.github;
            this.chkFilterOpenSource.Location = new System.Drawing.Point(504, 0);
            this.chkFilterOpenSource.Name = "chkFilterOpenSource";
            this.chkFilterOpenSource.Size = new System.Drawing.Size(68, 42);
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
            this.toolStrip1.Size = new System.Drawing.Size(1624, 39);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "tsMain";
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.Image = global::XrmToolBox.ToolLibrary.Resource.Refresh;
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(82, 36);
            this.tsbRefresh.Text = "Refresh";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // tssRefresh
            // 
            this.tssRefresh.Name = "tssRefresh";
            this.tssRefresh.Size = new System.Drawing.Size(6, 39);
            // 
            // tsbBulkInstall
            // 
            this.tsbBulkInstall.Enabled = false;
            this.tsbBulkInstall.Image = global::XrmToolBox.ToolLibrary.Resource.Install;
            this.tsbBulkInstall.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBulkInstall.Name = "tsbBulkInstall";
            this.tsbBulkInstall.Size = new System.Drawing.Size(74, 36);
            this.tsbBulkInstall.Text = "Install";
            this.tsbBulkInstall.Click += new System.EventHandler(this.tsbBulkInstall_Click);
            // 
            // tsbBulkDelete
            // 
            this.tsbBulkDelete.Enabled = false;
            this.tsbBulkDelete.Image = global::XrmToolBox.ToolLibrary.Resource.Uninstall;
            this.tsbBulkDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBulkDelete.Name = "tsbBulkDelete";
            this.tsbBulkDelete.Size = new System.Drawing.Size(76, 36);
            this.tsbBulkDelete.Text = "Delete";
            this.tsbBulkDelete.Click += new System.EventHandler(this.tsbBulkDelete_Click);
            // 
            // tssDelete
            // 
            this.tssDelete.Name = "tssDelete";
            this.tssDelete.Size = new System.Drawing.Size(6, 39);
            // 
            // tsddActions
            // 
            this.tsddActions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssReloadImageCache,
            this.tssClearPackageCache});
            this.tsddActions.Image = global::XrmToolBox.ToolLibrary.Resource.Settings;
            this.tsddActions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddActions.Name = "tsddActions";
            this.tsddActions.Size = new System.Drawing.Size(94, 36);
            this.tsddActions.Text = "Settings";
            this.tsddActions.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsddActions_DropDownItemClicked);
            // 
            // tssReloadImageCache
            // 
            this.tssReloadImageCache.Image = global::XrmToolBox.ToolLibrary.Resource.RefreshImages;
            this.tssReloadImageCache.Name = "tssReloadImageCache";
            this.tssReloadImageCache.Size = new System.Drawing.Size(187, 22);
            this.tssReloadImageCache.Text = "Reload Image cache";
            // 
            // tssClearPackageCache
            // 
            this.tssClearPackageCache.Image = global::XrmToolBox.ToolLibrary.Resource.ClearCache;
            this.tssClearPackageCache.Name = "tssClearPackageCache";
            this.tssClearPackageCache.Size = new System.Drawing.Size(187, 22);
            this.tssClearPackageCache.Text = "Clear Packages cache";
            // 
            // tssSettings
            // 
            this.tssSettings.Name = "tssSettings";
            this.tssSettings.Size = new System.Drawing.Size(6, 39);
            this.tssSettings.Visible = false;
            // 
            // tsbRestart
            // 
            this.tsbRestart.Image = global::XrmToolBox.ToolLibrary.Resource.Restart;
            this.tsbRestart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRestart.Name = "tsbRestart";
            this.tsbRestart.Size = new System.Drawing.Size(71, 36);
            this.tsbRestart.Text = "Restart";
            this.tsbRestart.Visible = false;
            this.tsbRestart.Click += new System.EventHandler(this.tsbRestart_Click);
            // 
            // ToolLibraryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1624, 928);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlToolsTop);
            this.Controls.Add(this.toolStrip1);
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
    }
}

