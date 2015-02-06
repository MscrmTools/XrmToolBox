namespace MsCrmTools.RoleUpdater
{
    partial class RoleUpdater
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoleUpdater));
            this.pnlSteps = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbCloseThisTab = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbBtnLoadRolesAndPrivileges = new System.Windows.Forms.ToolStripButton();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSteps
            // 
            this.pnlSteps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSteps.BackColor = System.Drawing.Color.Transparent;
            this.pnlSteps.Location = new System.Drawing.Point(0, 25);
            this.pnlSteps.Name = "pnlSteps";
            this.pnlSteps.Size = new System.Drawing.Size(900, 539);
            this.pnlSteps.TabIndex = 104;
            this.pnlSteps.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCloseThisTab,
            this.toolStripSeparator1,
            this.tsbBtnLoadRolesAndPrivileges});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(900, 25);
            this.toolStrip1.TabIndex = 105;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbCloseThisTab
            // 
            this.tsbCloseThisTab.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCloseThisTab.Image = ((System.Drawing.Image)(resources.GetObject("tsbCloseThisTab.Image")));
            this.tsbCloseThisTab.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCloseThisTab.Name = "tsbCloseThisTab";
            this.tsbCloseThisTab.Size = new System.Drawing.Size(23, 22);
            this.tsbCloseThisTab.Text = "Close this tab";
            this.tsbCloseThisTab.Click += new System.EventHandler(this.TsbCloseThisTabClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbBtnLoadRolesAndPrivileges
            // 
            this.tsbBtnLoadRolesAndPrivileges.Image = ((System.Drawing.Image)(resources.GetObject("tsbBtnLoadRolesAndPrivileges.Image")));
            this.tsbBtnLoadRolesAndPrivileges.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBtnLoadRolesAndPrivileges.Name = "tsbBtnLoadRolesAndPrivileges";
            this.tsbBtnLoadRolesAndPrivileges.Size = new System.Drawing.Size(160, 22);
            this.tsbBtnLoadRolesAndPrivileges.Text = "Load Roles and Privileges";
            this.tsbBtnLoadRolesAndPrivileges.Click += new System.EventHandler(this.BtnConnectClick);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Location = new System.Drawing.Point(822, 570);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 106;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Visible = false;
            this.btnNext.Click += new System.EventHandler(this.BtnNextClick);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevious.Location = new System.Drawing.Point(741, 570);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 23);
            this.btnPrevious.TabIndex = 107;
            this.btnPrevious.Text = "Back";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Visible = false;
            this.btnPrevious.Click += new System.EventHandler(this.BtnPreviousClick);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(660, 570);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 108;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Visible = false;
            this.btnReset.Click += new System.EventHandler(this.BtnResetClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Icon.png");
            // 
            // RoleUpdater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.pnlSteps);
            this.Name = "RoleUpdater";
            this.Size = new System.Drawing.Size(900, 600);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlSteps;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.ToolStripButton tsbCloseThisTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbBtnLoadRolesAndPrivileges;
        private System.Windows.Forms.ImageList imageList1;
    }
}
