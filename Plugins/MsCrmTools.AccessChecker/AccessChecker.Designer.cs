namespace MsCrmTools.AccessChecker
{
    partial class AccessChecker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccessChecker));
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.toolImageList = new System.Windows.Forms.ImageList(this.components);
            this.cBoxEntities = new System.Windows.Forms.ComboBox();
            this.btnRetrieveEntities = new System.Windows.Forms.Button();
            this.lblEntityName = new System.Windows.Forms.Label();
            this.txtObjectId = new System.Windows.Forms.TextBox();
            this.lblObjectId = new System.Windows.Forms.Label();
            this.textBox_PrimaryAttribute = new System.Windows.Forms.TextBox();
            this.lblPrimaryAttr = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.textBox_UserID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRetrieveRights = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlAssign = new System.Windows.Forms.Panel();
            this.lblPrivilegeAssign = new System.Windows.Forms.Label();
            this.lblAssign = new System.Windows.Forms.Label();
            this.pnlAppend = new System.Windows.Forms.Panel();
            this.lblPrivilegeAppend = new System.Windows.Forms.Label();
            this.lblAppend = new System.Windows.Forms.Label();
            this.pnlWrite = new System.Windows.Forms.Panel();
            this.lblPrivilegeWrite = new System.Windows.Forms.Label();
            this.lblWrite = new System.Windows.Forms.Label();
            this.pnlCreate = new System.Windows.Forms.Panel();
            this.lblPrivilegeCreate = new System.Windows.Forms.Label();
            this.lblCreate = new System.Windows.Forms.Label();
            this.pnlShare = new System.Windows.Forms.Panel();
            this.lblPrivilegeShare = new System.Windows.Forms.Label();
            this.lblShare = new System.Windows.Forms.Label();
            this.pnlAppendTo = new System.Windows.Forms.Panel();
            this.lblPrivilegeAppendTo = new System.Windows.Forms.Label();
            this.lblAppenTo = new System.Windows.Forms.Label();
            this.pnlDelete = new System.Windows.Forms.Panel();
            this.lblPrivilegeDelete = new System.Windows.Forms.Label();
            this.lblDelete = new System.Windows.Forms.Label();
            this.pnlRead = new System.Windows.Forms.Panel();
            this.lblPrivilegeRead = new System.Windows.Forms.Label();
            this.lblRead = new System.Windows.Forms.Label();
            this.btnSearchRecordId = new System.Windows.Forms.Button();
            this.toolStripMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlAssign.SuspendLayout();
            this.pnlAppend.SuspendLayout();
            this.pnlWrite.SuspendLayout();
            this.pnlCreate.SuspendLayout();
            this.pnlShare.SuspendLayout();
            this.pnlAppendTo.SuspendLayout();
            this.pnlDelete.SuspendLayout();
            this.pnlRead.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(807, 25);
            this.toolStripMenu.TabIndex = 2;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(102, 22);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.TsbCloseClick);
            // 
            // toolImageList
            // 
            this.toolImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("toolImageList.ImageStream")));
            this.toolImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.toolImageList.Images.SetKeyName(0, "Icon.png");
            // 
            // cBoxEntities
            // 
            this.cBoxEntities.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBoxEntities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxEntities.FormattingEnabled = true;
            this.cBoxEntities.Location = new System.Drawing.Point(110, 48);
            this.cBoxEntities.Name = "cBoxEntities";
            this.cBoxEntities.Size = new System.Drawing.Size(583, 21);
            this.cBoxEntities.Sorted = true;
            this.cBoxEntities.TabIndex = 11;
            this.cBoxEntities.SelectedIndexChanged += new System.EventHandler(this.CBoxEntitiesSelectedIndexChanged);
            // 
            // btnRetrieveEntities
            // 
            this.btnRetrieveEntities.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRetrieveEntities.Location = new System.Drawing.Point(699, 47);
            this.btnRetrieveEntities.Name = "btnRetrieveEntities";
            this.btnRetrieveEntities.Size = new System.Drawing.Size(105, 23);
            this.btnRetrieveEntities.TabIndex = 12;
            this.btnRetrieveEntities.Text = "Retrieve entities";
            this.btnRetrieveEntities.UseVisualStyleBackColor = true;
            this.btnRetrieveEntities.Click += new System.EventHandler(this.BtnRetrieveEntitiesClick);
            // 
            // lblEntityName
            // 
            this.lblEntityName.AutoSize = true;
            this.lblEntityName.Location = new System.Drawing.Point(3, 53);
            this.lblEntityName.Name = "lblEntityName";
            this.lblEntityName.Size = new System.Drawing.Size(64, 13);
            this.lblEntityName.TabIndex = 13;
            this.lblEntityName.Text = "Entity Name";
            // 
            // txtObjectId
            // 
            this.txtObjectId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObjectId.Location = new System.Drawing.Point(110, 75);
            this.txtObjectId.Name = "txtObjectId";
            this.txtObjectId.Size = new System.Drawing.Size(583, 20);
            this.txtObjectId.TabIndex = 14;
            // 
            // lblObjectId
            // 
            this.lblObjectId.AutoSize = true;
            this.lblObjectId.Location = new System.Drawing.Point(3, 78);
            this.lblObjectId.Name = "lblObjectId";
            this.lblObjectId.Size = new System.Drawing.Size(52, 13);
            this.lblObjectId.TabIndex = 15;
            this.lblObjectId.Text = "Object ID";
            // 
            // textBox_PrimaryAttribute
            // 
            this.textBox_PrimaryAttribute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_PrimaryAttribute.Location = new System.Drawing.Point(110, 101);
            this.textBox_PrimaryAttribute.Name = "textBox_PrimaryAttribute";
            this.textBox_PrimaryAttribute.ReadOnly = true;
            this.textBox_PrimaryAttribute.Size = new System.Drawing.Size(694, 20);
            this.textBox_PrimaryAttribute.TabIndex = 81;
            // 
            // lblPrimaryAttr
            // 
            this.lblPrimaryAttr.AutoSize = true;
            this.lblPrimaryAttr.Location = new System.Drawing.Point(3, 104);
            this.lblPrimaryAttr.Name = "lblPrimaryAttr";
            this.lblPrimaryAttr.Size = new System.Drawing.Size(35, 13);
            this.lblPrimaryAttr.TabIndex = 82;
            this.lblPrimaryAttr.Text = "Name";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(699, 126);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(105, 23);
            this.btnBrowse.TabIndex = 77;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowseClick);
            // 
            // textBox_UserID
            // 
            this.textBox_UserID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_UserID.Location = new System.Drawing.Point(110, 127);
            this.textBox_UserID.Name = "textBox_UserID";
            this.textBox_UserID.ReadOnly = true;
            this.textBox_UserID.Size = new System.Drawing.Size(583, 20);
            this.textBox_UserID.TabIndex = 80;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 79;
            this.label3.Text = "User";
            // 
            // btnRetrieveRights
            // 
            this.btnRetrieveRights.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRetrieveRights.Location = new System.Drawing.Point(6, 153);
            this.btnRetrieveRights.Name = "btnRetrieveRights";
            this.btnRetrieveRights.Size = new System.Drawing.Size(798, 23);
            this.btnRetrieveRights.TabIndex = 78;
            this.btnRetrieveRights.Text = "Retrieve rights";
            this.btnRetrieveRights.UseVisualStyleBackColor = true;
            this.btnRetrieveRights.Click += new System.EventHandler(this.BtnRetrieveRightsClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(6, 182);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnlAssign);
            this.splitContainer1.Panel1.Controls.Add(this.pnlAppend);
            this.splitContainer1.Panel1.Controls.Add(this.pnlWrite);
            this.splitContainer1.Panel1.Controls.Add(this.pnlCreate);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlShare);
            this.splitContainer1.Panel2.Controls.Add(this.pnlAppendTo);
            this.splitContainer1.Panel2.Controls.Add(this.pnlDelete);
            this.splitContainer1.Panel2.Controls.Add(this.pnlRead);
            this.splitContainer1.Size = new System.Drawing.Size(798, 352);
            this.splitContainer1.SplitterDistance = 396;
            this.splitContainer1.TabIndex = 83;
            // 
            // pnlAssign
            // 
            this.pnlAssign.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAssign.BackColor = System.Drawing.Color.DarkGray;
            this.pnlAssign.Controls.Add(this.lblPrivilegeAssign);
            this.pnlAssign.Controls.Add(this.lblAssign);
            this.pnlAssign.Location = new System.Drawing.Point(3, 261);
            this.pnlAssign.Name = "pnlAssign";
            this.pnlAssign.Size = new System.Drawing.Size(390, 80);
            this.pnlAssign.TabIndex = 1;
            // 
            // lblPrivilegeAssign
            // 
            this.lblPrivilegeAssign.AutoSize = true;
            this.lblPrivilegeAssign.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrivilegeAssign.Location = new System.Drawing.Point(4, 61);
            this.lblPrivilegeAssign.Name = "lblPrivilegeAssign";
            this.lblPrivilegeAssign.Size = new System.Drawing.Size(107, 19);
            this.lblPrivilegeAssign.TabIndex = 2;
            this.lblPrivilegeAssign.Text = "PrivilegeId : N/A";
            // 
            // lblAssign
            // 
            this.lblAssign.AutoSize = true;
            this.lblAssign.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssign.Location = new System.Drawing.Point(3, 3);
            this.lblAssign.Name = "lblAssign";
            this.lblAssign.Size = new System.Drawing.Size(128, 25);
            this.lblAssign.TabIndex = 1;
            this.lblAssign.Text = "Assign Access";
            // 
            // pnlAppend
            // 
            this.pnlAppend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAppend.BackColor = System.Drawing.Color.DarkGray;
            this.pnlAppend.Controls.Add(this.lblPrivilegeAppend);
            this.pnlAppend.Controls.Add(this.lblAppend);
            this.pnlAppend.Location = new System.Drawing.Point(3, 175);
            this.pnlAppend.Name = "pnlAppend";
            this.pnlAppend.Size = new System.Drawing.Size(390, 80);
            this.pnlAppend.TabIndex = 1;
            // 
            // lblPrivilegeAppend
            // 
            this.lblPrivilegeAppend.AutoSize = true;
            this.lblPrivilegeAppend.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrivilegeAppend.Location = new System.Drawing.Point(4, 61);
            this.lblPrivilegeAppend.Name = "lblPrivilegeAppend";
            this.lblPrivilegeAppend.Size = new System.Drawing.Size(107, 19);
            this.lblPrivilegeAppend.TabIndex = 2;
            this.lblPrivilegeAppend.Text = "PrivilegeId : N/A";
            // 
            // lblAppend
            // 
            this.lblAppend.AutoSize = true;
            this.lblAppend.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppend.Location = new System.Drawing.Point(3, 3);
            this.lblAppend.Name = "lblAppend";
            this.lblAppend.Size = new System.Drawing.Size(139, 25);
            this.lblAppend.TabIndex = 1;
            this.lblAppend.Text = "Append Access";
            // 
            // pnlWrite
            // 
            this.pnlWrite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlWrite.BackColor = System.Drawing.Color.DarkGray;
            this.pnlWrite.Controls.Add(this.lblPrivilegeWrite);
            this.pnlWrite.Controls.Add(this.lblWrite);
            this.pnlWrite.Location = new System.Drawing.Point(3, 89);
            this.pnlWrite.Name = "pnlWrite";
            this.pnlWrite.Size = new System.Drawing.Size(390, 80);
            this.pnlWrite.TabIndex = 1;
            // 
            // lblPrivilegeWrite
            // 
            this.lblPrivilegeWrite.AutoSize = true;
            this.lblPrivilegeWrite.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrivilegeWrite.Location = new System.Drawing.Point(4, 61);
            this.lblPrivilegeWrite.Name = "lblPrivilegeWrite";
            this.lblPrivilegeWrite.Size = new System.Drawing.Size(107, 19);
            this.lblPrivilegeWrite.TabIndex = 2;
            this.lblPrivilegeWrite.Text = "PrivilegeId : N/A";
            // 
            // lblWrite
            // 
            this.lblWrite.AutoSize = true;
            this.lblWrite.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWrite.Location = new System.Drawing.Point(3, 3);
            this.lblWrite.Name = "lblWrite";
            this.lblWrite.Size = new System.Drawing.Size(119, 25);
            this.lblWrite.TabIndex = 1;
            this.lblWrite.Text = "Write Access";
            // 
            // pnlCreate
            // 
            this.pnlCreate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCreate.BackColor = System.Drawing.Color.DarkGray;
            this.pnlCreate.Controls.Add(this.lblPrivilegeCreate);
            this.pnlCreate.Controls.Add(this.lblCreate);
            this.pnlCreate.Location = new System.Drawing.Point(3, 3);
            this.pnlCreate.Name = "pnlCreate";
            this.pnlCreate.Size = new System.Drawing.Size(390, 80);
            this.pnlCreate.TabIndex = 0;
            // 
            // lblPrivilegeCreate
            // 
            this.lblPrivilegeCreate.AutoSize = true;
            this.lblPrivilegeCreate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrivilegeCreate.Location = new System.Drawing.Point(4, 61);
            this.lblPrivilegeCreate.Name = "lblPrivilegeCreate";
            this.lblPrivilegeCreate.Size = new System.Drawing.Size(107, 19);
            this.lblPrivilegeCreate.TabIndex = 1;
            this.lblPrivilegeCreate.Text = "PrivilegeId : N/A";
            // 
            // lblCreate
            // 
            this.lblCreate.AutoSize = true;
            this.lblCreate.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreate.Location = new System.Drawing.Point(3, 3);
            this.lblCreate.Name = "lblCreate";
            this.lblCreate.Size = new System.Drawing.Size(128, 25);
            this.lblCreate.TabIndex = 0;
            this.lblCreate.Text = "Create Access";
            // 
            // pnlShare
            // 
            this.pnlShare.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlShare.BackColor = System.Drawing.Color.DarkGray;
            this.pnlShare.Controls.Add(this.lblPrivilegeShare);
            this.pnlShare.Controls.Add(this.lblShare);
            this.pnlShare.Location = new System.Drawing.Point(3, 261);
            this.pnlShare.Name = "pnlShare";
            this.pnlShare.Size = new System.Drawing.Size(391, 80);
            this.pnlShare.TabIndex = 3;
            // 
            // lblPrivilegeShare
            // 
            this.lblPrivilegeShare.AutoSize = true;
            this.lblPrivilegeShare.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrivilegeShare.Location = new System.Drawing.Point(4, 61);
            this.lblPrivilegeShare.Name = "lblPrivilegeShare";
            this.lblPrivilegeShare.Size = new System.Drawing.Size(107, 19);
            this.lblPrivilegeShare.TabIndex = 4;
            this.lblPrivilegeShare.Text = "PrivilegeId : N/A";
            // 
            // lblShare
            // 
            this.lblShare.AutoSize = true;
            this.lblShare.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShare.Location = new System.Drawing.Point(3, 3);
            this.lblShare.Name = "lblShare";
            this.lblShare.Size = new System.Drawing.Size(121, 25);
            this.lblShare.TabIndex = 3;
            this.lblShare.Text = "Share Access";
            // 
            // pnlAppendTo
            // 
            this.pnlAppendTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAppendTo.BackColor = System.Drawing.Color.DarkGray;
            this.pnlAppendTo.Controls.Add(this.lblPrivilegeAppendTo);
            this.pnlAppendTo.Controls.Add(this.lblAppenTo);
            this.pnlAppendTo.Location = new System.Drawing.Point(3, 175);
            this.pnlAppendTo.Name = "pnlAppendTo";
            this.pnlAppendTo.Size = new System.Drawing.Size(391, 80);
            this.pnlAppendTo.TabIndex = 4;
            // 
            // lblPrivilegeAppendTo
            // 
            this.lblPrivilegeAppendTo.AutoSize = true;
            this.lblPrivilegeAppendTo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrivilegeAppendTo.Location = new System.Drawing.Point(4, 61);
            this.lblPrivilegeAppendTo.Name = "lblPrivilegeAppendTo";
            this.lblPrivilegeAppendTo.Size = new System.Drawing.Size(107, 19);
            this.lblPrivilegeAppendTo.TabIndex = 4;
            this.lblPrivilegeAppendTo.Text = "PrivilegeId : N/A";
            // 
            // lblAppenTo
            // 
            this.lblAppenTo.AutoSize = true;
            this.lblAppenTo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppenTo.Location = new System.Drawing.Point(3, 3);
            this.lblAppenTo.Name = "lblAppenTo";
            this.lblAppenTo.Size = new System.Drawing.Size(160, 25);
            this.lblAppenTo.TabIndex = 3;
            this.lblAppenTo.Text = "AppendTo Access";
            // 
            // pnlDelete
            // 
            this.pnlDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDelete.BackColor = System.Drawing.Color.DarkGray;
            this.pnlDelete.Controls.Add(this.lblPrivilegeDelete);
            this.pnlDelete.Controls.Add(this.lblDelete);
            this.pnlDelete.Location = new System.Drawing.Point(3, 89);
            this.pnlDelete.Name = "pnlDelete";
            this.pnlDelete.Size = new System.Drawing.Size(391, 80);
            this.pnlDelete.TabIndex = 5;
            // 
            // lblPrivilegeDelete
            // 
            this.lblPrivilegeDelete.AutoSize = true;
            this.lblPrivilegeDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrivilegeDelete.Location = new System.Drawing.Point(4, 61);
            this.lblPrivilegeDelete.Name = "lblPrivilegeDelete";
            this.lblPrivilegeDelete.Size = new System.Drawing.Size(107, 19);
            this.lblPrivilegeDelete.TabIndex = 4;
            this.lblPrivilegeDelete.Text = "PrivilegeId : N/A";
            // 
            // lblDelete
            // 
            this.lblDelete.AutoSize = true;
            this.lblDelete.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDelete.Location = new System.Drawing.Point(3, 3);
            this.lblDelete.Name = "lblDelete";
            this.lblDelete.Size = new System.Drawing.Size(127, 25);
            this.lblDelete.TabIndex = 3;
            this.lblDelete.Text = "Delete Access";
            // 
            // pnlRead
            // 
            this.pnlRead.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlRead.BackColor = System.Drawing.Color.DarkGray;
            this.pnlRead.Controls.Add(this.lblPrivilegeRead);
            this.pnlRead.Controls.Add(this.lblRead);
            this.pnlRead.Location = new System.Drawing.Point(3, 3);
            this.pnlRead.Name = "pnlRead";
            this.pnlRead.Size = new System.Drawing.Size(391, 80);
            this.pnlRead.TabIndex = 2;
            // 
            // lblPrivilegeRead
            // 
            this.lblPrivilegeRead.AutoSize = true;
            this.lblPrivilegeRead.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrivilegeRead.Location = new System.Drawing.Point(4, 61);
            this.lblPrivilegeRead.Name = "lblPrivilegeRead";
            this.lblPrivilegeRead.Size = new System.Drawing.Size(107, 19);
            this.lblPrivilegeRead.TabIndex = 3;
            this.lblPrivilegeRead.Text = "PrivilegeId : N/A";
            // 
            // lblRead
            // 
            this.lblRead.AutoSize = true;
            this.lblRead.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRead.Location = new System.Drawing.Point(3, 3);
            this.lblRead.Name = "lblRead";
            this.lblRead.Size = new System.Drawing.Size(115, 25);
            this.lblRead.TabIndex = 2;
            this.lblRead.Text = "Read Access";
            // 
            // btnSearchRecordId
            // 
            this.btnSearchRecordId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchRecordId.Enabled = false;
            this.btnSearchRecordId.Location = new System.Drawing.Point(699, 74);
            this.btnSearchRecordId.Name = "btnSearchRecordId";
            this.btnSearchRecordId.Size = new System.Drawing.Size(105, 23);
            this.btnSearchRecordId.TabIndex = 84;
            this.btnSearchRecordId.Text = "Search";
            this.btnSearchRecordId.UseVisualStyleBackColor = true;
            this.btnSearchRecordId.Click += new System.EventHandler(this.BtnSearchRecordIdClick);
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSearchRecordId);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.textBox_PrimaryAttribute);
            this.Controls.Add(this.lblPrimaryAttr);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.textBox_UserID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRetrieveRights);
            this.Controls.Add(this.txtObjectId);
            this.Controls.Add(this.lblObjectId);
            this.Controls.Add(this.cBoxEntities);
            this.Controls.Add(this.btnRetrieveEntities);
            this.Controls.Add(this.lblEntityName);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(807, 629);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlAssign.ResumeLayout(false);
            this.pnlAssign.PerformLayout();
            this.pnlAppend.ResumeLayout(false);
            this.pnlAppend.PerformLayout();
            this.pnlWrite.ResumeLayout(false);
            this.pnlWrite.PerformLayout();
            this.pnlCreate.ResumeLayout(false);
            this.pnlCreate.PerformLayout();
            this.pnlShare.ResumeLayout(false);
            this.pnlShare.PerformLayout();
            this.pnlAppendTo.ResumeLayout(false);
            this.pnlAppendTo.PerformLayout();
            this.pnlDelete.ResumeLayout(false);
            this.pnlDelete.PerformLayout();
            this.pnlRead.ResumeLayout(false);
            this.pnlRead.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ImageList toolImageList;
        private System.Windows.Forms.ComboBox cBoxEntities;
        private System.Windows.Forms.Button btnRetrieveEntities;
        private System.Windows.Forms.Label lblEntityName;
        private System.Windows.Forms.TextBox txtObjectId;
        private System.Windows.Forms.Label lblObjectId;
        private System.Windows.Forms.TextBox textBox_PrimaryAttribute;
        private System.Windows.Forms.Label lblPrimaryAttr;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox textBox_UserID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRetrieveRights;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnlAssign;
        private System.Windows.Forms.Label lblAssign;
        private System.Windows.Forms.Panel pnlAppend;
        private System.Windows.Forms.Label lblAppend;
        private System.Windows.Forms.Panel pnlWrite;
        private System.Windows.Forms.Label lblWrite;
        private System.Windows.Forms.Panel pnlCreate;
        private System.Windows.Forms.Label lblCreate;
        private System.Windows.Forms.Panel pnlShare;
        private System.Windows.Forms.Label lblShare;
        private System.Windows.Forms.Panel pnlAppendTo;
        private System.Windows.Forms.Label lblAppenTo;
        private System.Windows.Forms.Panel pnlDelete;
        private System.Windows.Forms.Label lblDelete;
        private System.Windows.Forms.Panel pnlRead;
        private System.Windows.Forms.Label lblRead;
        private System.Windows.Forms.Label lblPrivilegeAssign;
        private System.Windows.Forms.Label lblPrivilegeAppend;
        private System.Windows.Forms.Label lblPrivilegeWrite;
        private System.Windows.Forms.Label lblPrivilegeCreate;
        private System.Windows.Forms.Label lblPrivilegeShare;
        private System.Windows.Forms.Label lblPrivilegeAppendTo;
        private System.Windows.Forms.Label lblPrivilegeDelete;
        private System.Windows.Forms.Label lblPrivilegeRead;
        private System.Windows.Forms.Button btnSearchRecordId;
    }
}
