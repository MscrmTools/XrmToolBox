namespace MsCrmTools.UserSettingsUtility.UserControls
{
    partial class UserSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserSelector));
            this.cbbViews = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ilUserAndTeams = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lvUsers = new System.Windows.Forms.ListView();
            this.llCheckAll = new System.Windows.Forms.LinkLabel();
            this.llCheckNone = new System.Windows.Forms.LinkLabel();
            this.llInvertSelection = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbbViews
            // 
            this.cbbViews.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbViews.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbViews.FormattingEnabled = true;
            this.cbbViews.Location = new System.Drawing.Point(66, 4);
            this.cbbViews.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbbViews.Name = "cbbViews";
            this.cbbViews.Size = new System.Drawing.Size(492, 28);
            this.cbbViews.TabIndex = 2;
            this.cbbViews.SelectedIndexChanged += new System.EventHandler(this.cbbViews_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "View";
            // 
            // ilUserAndTeams
            // 
            this.ilUserAndTeams.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilUserAndTeams.ImageStream")));
            this.ilUserAndTeams.TransparentColor = System.Drawing.Color.Transparent;
            this.ilUserAndTeams.Images.SetKeyName(0, "ico_16_8.gif");
            this.ilUserAndTeams.Images.SetKeyName(1, "ico_16_9.gif");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbbViews);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(561, 38);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.llInvertSelection);
            this.panel2.Controls.Add(this.llCheckNone);
            this.panel2.Controls.Add(this.llCheckAll);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(561, 32);
            this.panel2.TabIndex = 7;
            // 
            // lvUsers
            // 
            this.lvUsers.CheckBoxes = true;
            this.lvUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvUsers.FullRowSelect = true;
            this.lvUsers.HideSelection = false;
            this.lvUsers.Location = new System.Drawing.Point(0, 70);
            this.lvUsers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lvUsers.Name = "lvUsers";
            this.lvUsers.Size = new System.Drawing.Size(561, 561);
            this.lvUsers.SmallImageList = this.ilUserAndTeams;
            this.lvUsers.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvUsers.TabIndex = 8;
            this.lvUsers.UseCompatibleStateImageBehavior = false;
            this.lvUsers.View = System.Windows.Forms.View.Details;
            this.lvUsers.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvUsers_ColumnClick);
            // 
            // llCheckAll
            // 
            this.llCheckAll.AutoSize = true;
            this.llCheckAll.Location = new System.Drawing.Point(3, 3);
            this.llCheckAll.Name = "llCheckAll";
            this.llCheckAll.Size = new System.Drawing.Size(73, 20);
            this.llCheckAll.TabIndex = 0;
            this.llCheckAll.TabStop = true;
            this.llCheckAll.Text = "Check all";
            this.llCheckAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llCheckAll_LinkClicked);
            // 
            // llCheckNone
            // 
            this.llCheckNone.AutoSize = true;
            this.llCheckNone.Location = new System.Drawing.Point(82, 3);
            this.llCheckNone.Name = "llCheckNone";
            this.llCheckNone.Size = new System.Drawing.Size(94, 20);
            this.llCheckNone.TabIndex = 1;
            this.llCheckNone.TabStop = true;
            this.llCheckNone.Text = "Check none";
            this.llCheckNone.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llCheckNone_LinkClicked);
            // 
            // llInvertSelection
            // 
            this.llInvertSelection.AutoSize = true;
            this.llInvertSelection.Location = new System.Drawing.Point(182, 3);
            this.llInvertSelection.Name = "llInvertSelection";
            this.llInvertSelection.Size = new System.Drawing.Size(116, 20);
            this.llInvertSelection.TabIndex = 2;
            this.llInvertSelection.TabStop = true;
            this.llInvertSelection.Text = "Invert selection";
            this.llInvertSelection.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llInvertSelection_LinkClicked);
            // 
            // UserSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvUsers);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UserSelector";
            this.Size = new System.Drawing.Size(561, 631);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbViews;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ImageList ilUserAndTeams;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView lvUsers;
        private System.Windows.Forms.LinkLabel llInvertSelection;
        private System.Windows.Forms.LinkLabel llCheckNone;
        private System.Windows.Forms.LinkLabel llCheckAll;
    }
}
