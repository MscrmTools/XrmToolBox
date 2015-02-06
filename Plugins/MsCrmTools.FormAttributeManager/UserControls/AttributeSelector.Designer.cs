namespace MsCrmTools.FormAttributeManager.UserControls
{
    partial class AttributeSelector
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
            this.cbbEntities = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lvAttributes = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.llSelectAll = new System.Windows.Forms.LinkLabel();
            this.llSelectNone = new System.Windows.Forms.LinkLabel();
            this.llSelectOnForm = new System.Windows.Forms.LinkLabel();
            this.llSelectNotOnForm = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbbEntities
            // 
            this.cbbEntities.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbEntities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbEntities.FormattingEnabled = true;
            this.cbbEntities.Location = new System.Drawing.Point(0, 0);
            this.cbbEntities.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbbEntities.Name = "cbbEntities";
            this.cbbEntities.Size = new System.Drawing.Size(495, 28);
            this.cbbEntities.Sorted = true;
            this.cbbEntities.TabIndex = 6;
            this.cbbEntities.SelectedIndexChanged += new System.EventHandler(this.cbbEntities_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.llSelectNotOnForm);
            this.panel1.Controls.Add(this.llSelectOnForm);
            this.panel1.Controls.Add(this.llSelectNone);
            this.panel1.Controls.Add(this.llSelectAll);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(495, 36);
            this.panel1.TabIndex = 10;
            // 
            // lvAttributes
            // 
            this.lvAttributes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvAttributes.FullRowSelect = true;
            this.lvAttributes.HideSelection = false;
            this.lvAttributes.Location = new System.Drawing.Point(0, 64);
            this.lvAttributes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lvAttributes.Name = "lvAttributes";
            this.lvAttributes.Size = new System.Drawing.Size(495, 877);
            this.lvAttributes.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvAttributes.TabIndex = 11;
            this.lvAttributes.UseCompatibleStateImageBehavior = false;
            this.lvAttributes.View = System.Windows.Forms.View.Details;
            this.lvAttributes.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            this.lvAttributes.SelectedIndexChanged += new System.EventHandler(this.lvAttributes_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Display name";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Logical name";
            this.columnHeader2.Width = 200;
            // 
            // llSelectAll
            // 
            this.llSelectAll.AutoSize = true;
            this.llSelectAll.Location = new System.Drawing.Point(3, 4);
            this.llSelectAll.Name = "llSelectAll";
            this.llSelectAll.Size = new System.Drawing.Size(73, 20);
            this.llSelectAll.TabIndex = 0;
            this.llSelectAll.TabStop = true;
            this.llSelectAll.Text = "Select all";
            this.llSelectAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSelectAll_LinkClicked);
            // 
            // llSelectNone
            // 
            this.llSelectNone.AutoSize = true;
            this.llSelectNone.Location = new System.Drawing.Point(82, 4);
            this.llSelectNone.Name = "llSelectNone";
            this.llSelectNone.Size = new System.Drawing.Size(94, 20);
            this.llSelectNone.TabIndex = 1;
            this.llSelectNone.TabStop = true;
            this.llSelectNone.Text = "Select none";
            this.llSelectNone.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSelectNone_LinkClicked);
            // 
            // llSelectOnForm
            // 
            this.llSelectOnForm.AutoSize = true;
            this.llSelectOnForm.Location = new System.Drawing.Point(182, 4);
            this.llSelectOnForm.Name = "llSelectOnForm";
            this.llSelectOnForm.Size = new System.Drawing.Size(139, 20);
            this.llSelectOnForm.TabIndex = 2;
            this.llSelectOnForm.TabStop = true;
            this.llSelectOnForm.Text = "Select on all forms";
            this.llSelectOnForm.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSelectOnForm_LinkClicked);
            // 
            // llSelectNotOnForm
            // 
            this.llSelectNotOnForm.AutoSize = true;
            this.llSelectNotOnForm.Location = new System.Drawing.Point(327, 4);
            this.llSelectNotOnForm.Name = "llSelectNotOnForm";
            this.llSelectNotOnForm.Size = new System.Drawing.Size(139, 20);
            this.llSelectNotOnForm.TabIndex = 3;
            this.llSelectNotOnForm.TabStop = true;
            this.llSelectNotOnForm.Text = "Select not on form";
            this.llSelectNotOnForm.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSelectNotOnForm_LinkClicked);
            // 
            // AttributeSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvAttributes);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbbEntities);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AttributeSelector";
            this.Size = new System.Drawing.Size(495, 941);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbEntities;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lvAttributes;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.LinkLabel llSelectNotOnForm;
        private System.Windows.Forms.LinkLabel llSelectOnForm;
        private System.Windows.Forms.LinkLabel llSelectNone;
        private System.Windows.Forms.LinkLabel llSelectAll;
    }
}
