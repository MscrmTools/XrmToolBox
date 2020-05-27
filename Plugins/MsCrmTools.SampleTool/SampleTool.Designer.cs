namespace MsCrmTools.SampleTool
{
    partial class SampleTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SampleTool));
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.cbMultipleCalls = new System.Windows.Forms.CheckBox();
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbWhoAmI = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDuplicate = new System.Windows.Forms.ToolStripButton();
            this.txtState = new System.Windows.Forms.TextBox();
            this.lblState = new System.Windows.Forms.Label();
            this.gbOptions.SuspendLayout();
            this.toolStripMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.cbMultipleCalls);
            this.gbOptions.Location = new System.Drawing.Point(32, 82);
            this.gbOptions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbOptions.Size = new System.Drawing.Size(324, 95);
            this.gbOptions.TabIndex = 3;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Options";
            // 
            // cbMultipleCalls
            // 
            this.cbMultipleCalls.AutoSize = true;
            this.cbMultipleCalls.Location = new System.Drawing.Point(26, 35);
            this.cbMultipleCalls.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbMultipleCalls.Name = "cbMultipleCalls";
            this.cbMultipleCalls.Size = new System.Drawing.Size(169, 29);
            this.cbMultipleCalls.TabIndex = 0;
            this.cbMultipleCalls.Text = "Multiple calls";
            this.cbMultipleCalls.UseVisualStyleBackColor = true;
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.toolStripSeparator2,
            this.tsbWhoAmI,
            this.toolStripSeparator1,
            this.tsbCancel,
            this.toolStripSeparator3,
            this.tsbDuplicate});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(1420, 42);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(46, 36);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 42);
            // 
            // tsbWhoAmI
            // 
            this.tsbWhoAmI.Image = ((System.Drawing.Image)(resources.GetObject("tsbWhoAmI.Image")));
            this.tsbWhoAmI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbWhoAmI.Name = "tsbWhoAmI";
            this.tsbWhoAmI.Size = new System.Drawing.Size(154, 36);
            this.tsbWhoAmI.Text = "Who am I";
            this.tsbWhoAmI.ToolTipText = "Perfomrs a Who I Am request";
            this.tsbWhoAmI.Click += new System.EventHandler(this.tsbWhoAmI_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 42);
            // 
            // tsbCancel
            // 
            this.tsbCancel.Enabled = false;
            this.tsbCancel.Image = ((System.Drawing.Image)(resources.GetObject("tsbCancel.Image")));
            this.tsbCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCancel.Name = "tsbCancel";
            this.tsbCancel.Size = new System.Drawing.Size(122, 36);
            this.tsbCancel.Text = "Cancel";
            this.tsbCancel.ToolTipText = "Cancel the current request";
            this.tsbCancel.Click += new System.EventHandler(this.tsbCancel_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 42);
            // 
            // tsbDuplicate
            // 
            this.tsbDuplicate.Image = ((System.Drawing.Image)(resources.GetObject("tsbDuplicate.Image")));
            this.tsbDuplicate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDuplicate.Name = "tsbDuplicate";
            this.tsbDuplicate.Size = new System.Drawing.Size(152, 36);
            this.tsbDuplicate.Text = "Duplicate";
            this.tsbDuplicate.Click += new System.EventHandler(this.tsbDuplicate_Click);
            // 
            // txtState
            // 
            this.txtState.Location = new System.Drawing.Point(32, 246);
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(324, 31);
            this.txtState.TabIndex = 5;
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(27, 209);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(62, 25);
            this.lblState.TabIndex = 6;
            this.lblState.Text = "State";
            // 
            // SampleTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.txtState);
            this.Controls.Add(this.toolStripMenu);
            this.Controls.Add(this.gbOptions);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "SampleTool";
            this.PluginIcon = ((System.Drawing.Icon)(resources.GetObject("$this.PluginIcon")));
            this.Size = new System.Drawing.Size(1420, 577);
            this.Load += new System.EventHandler(this.SampleTool_Load);
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.CheckBox cbMultipleCalls;
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbWhoAmI;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbCancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.ToolStripButton tsbDuplicate;
    }
}
