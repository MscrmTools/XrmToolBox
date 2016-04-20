namespace Gvw.CopyDynamicMarketingListToSavedQuery
{
    partial class MainControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainControl));
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.closeButton = new System.Windows.Forms.ToolStripButton();
            this.retrieveListsButton = new System.Windows.Forms.ToolStripButton();
            this.createViewButton = new System.Windows.Forms.ToolStripButton();
            this.listsListView = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEntity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpProperties = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.entityLabel = new System.Windows.Forms.Label();
            this.entityTextBox = new System.Windows.Forms.TextBox();
            this.lblViewType = new System.Windows.Forms.Label();
            this.grpLists = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.cbViewType = new System.Windows.Forms.ComboBox();
            this.toolStrip1.SuspendLayout();
            this.grpProperties.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.grpLists.SuspendLayout();
            this.SuspendLayout();
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(111, 3);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(621, 22);
            this.nameTextBox.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeButton,
            this.retrieveListsButton,
            this.createViewButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1498, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // closeButton
            // 
            this.closeButton.AccessibleDescription = "Close this tool";
            this.closeButton.AccessibleName = "Close";
            this.closeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.closeButton.Image = ((System.Drawing.Image)(resources.GetObject("closeButton.Image")));
            this.closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(24, 24);
            this.closeButton.Text = "Close";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // retrieveListsButton
            // 
            this.retrieveListsButton.AccessibleDescription = "Retrieve Marketing Lists";
            this.retrieveListsButton.AccessibleName = "Retrieve Lists";
            this.retrieveListsButton.Image = ((System.Drawing.Image)(resources.GetObject("retrieveListsButton.Image")));
            this.retrieveListsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.retrieveListsButton.Name = "retrieveListsButton";
            this.retrieveListsButton.Size = new System.Drawing.Size(119, 24);
            this.retrieveListsButton.Text = "Retrieve Lists";
            this.retrieveListsButton.Click += new System.EventHandler(this.retrieveListsButton_Click);
            // 
            // createViewButton
            // 
            this.createViewButton.Enabled = false;
            this.createViewButton.Image = ((System.Drawing.Image)(resources.GetObject("createViewButton.Image")));
            this.createViewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.createViewButton.Name = "createViewButton";
            this.createViewButton.Size = new System.Drawing.Size(98, 24);
            this.createViewButton.Text = "Save view";
            this.createViewButton.Click += new System.EventHandler(this.createViewButton_Click);
            // 
            // listsListView
            // 
            this.listsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colEntity});
            this.listsListView.Location = new System.Drawing.Point(6, 21);
            this.listsListView.MultiSelect = false;
            this.listsListView.Name = "listsListView";
            this.listsListView.Size = new System.Drawing.Size(727, 492);
            this.listsListView.TabIndex = 1;
            this.listsListView.UseCompatibleStateImageBehavior = false;
            this.listsListView.View = System.Windows.Forms.View.Details;
            this.listsListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listsListView_ItemSelectionChanged);
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 620;
            // 
            // colEntity
            // 
            this.colEntity.Text = "Entity";
            this.colEntity.Width = 100;
            // 
            // grpProperties
            // 
            this.grpProperties.Controls.Add(this.tableLayoutPanel1);
            this.grpProperties.Location = new System.Drawing.Point(748, 40);
            this.grpProperties.Name = "grpProperties";
            this.grpProperties.Size = new System.Drawing.Size(747, 216);
            this.grpProperties.TabIndex = 2;
            this.grpProperties.TabStop = false;
            this.grpProperties.Text = "View properties";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.69388F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.30612F));
            this.tableLayoutPanel1.Controls.Add(this.descriptionLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.nameTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.nameLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.descriptionTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.entityLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.entityTextBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblViewType, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cbViewType, 1, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 33);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(735, 177);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(3, 47);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(79, 17);
            this.descriptionLabel.TabIndex = 1;
            this.descriptionLabel.Text = "Description";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(3, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(45, 17);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Name";
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(111, 50);
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(621, 22);
            this.descriptionTextBox.TabIndex = 3;
            // 
            // entityLabel
            // 
            this.entityLabel.AutoSize = true;
            this.entityLabel.Location = new System.Drawing.Point(3, 94);
            this.entityLabel.Name = "entityLabel";
            this.entityLabel.Size = new System.Drawing.Size(43, 17);
            this.entityLabel.TabIndex = 4;
            this.entityLabel.Text = "Entity";
            // 
            // entityTextBox
            // 
            this.entityTextBox.Enabled = false;
            this.entityTextBox.Location = new System.Drawing.Point(111, 97);
            this.entityTextBox.Name = "entityTextBox";
            this.entityTextBox.Size = new System.Drawing.Size(621, 22);
            this.entityTextBox.TabIndex = 5;
            // 
            // lblViewType
            // 
            this.lblViewType.AutoSize = true;
            this.lblViewType.Location = new System.Drawing.Point(3, 139);
            this.lblViewType.Name = "lblViewType";
            this.lblViewType.Size = new System.Drawing.Size(68, 17);
            this.lblViewType.TabIndex = 6;
            this.lblViewType.Text = "View type";
            // 
            // grpLists
            // 
            this.grpLists.Controls.Add(this.listsListView);
            this.grpLists.Location = new System.Drawing.Point(3, 40);
            this.grpLists.Name = "grpLists";
            this.grpLists.Size = new System.Drawing.Size(739, 519);
            this.grpLists.TabIndex = 5;
            this.grpLists.TabStop = false;
            this.grpLists.Text = "Dynamic lists";
            // 
            // cbViewType
            // 
            this.cbViewType.FormattingEnabled = true;
            this.cbViewType.Items.AddRange(new object[] {
            "User",
            "Saved"});
            this.cbViewType.Location = new System.Drawing.Point(111, 142);
            this.cbViewType.Name = "cbViewType";
            this.cbViewType.Size = new System.Drawing.Size(121, 24);
            this.cbViewType.TabIndex = 7;
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpLists);
            this.Controls.Add(this.grpProperties);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(1498, 564);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.grpProperties.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.grpLists.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton closeButton;
        private System.Windows.Forms.ToolStripButton retrieveListsButton;
        private System.Windows.Forms.ListView listsListView;
        private System.Windows.Forms.GroupBox grpProperties;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label entityLabel;
        private System.Windows.Forms.TextBox entityTextBox;
        private System.Windows.Forms.ToolStripButton createViewButton;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.GroupBox grpLists;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colEntity;
        private System.Windows.Forms.Label lblViewType;
        private System.Windows.Forms.ComboBox cbViewType;
    }
}
