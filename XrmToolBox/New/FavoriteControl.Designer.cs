namespace XrmToolBox.New
{
    partial class FavoriteControl
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
            this.lblPlugin = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.cmsMru = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.cmsMru.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPlugin
            // 
            this.lblPlugin.AutoEllipsis = true;
            this.lblPlugin.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPlugin.Font = new System.Drawing.Font("Segoe UI Light", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlugin.Location = new System.Drawing.Point(128, 9);
            this.lblPlugin.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblPlugin.Name = "lblPlugin";
            this.lblPlugin.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.lblPlugin.Size = new System.Drawing.Size(754, 62);
            this.lblPlugin.TabIndex = 1;
            this.lblPlugin.Text = "[Plugin Name]";
            // 
            // toolTip
            // 
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.ToolTipTitle = "Plugin used";
            // 
            // cmsMru
            // 
            this.cmsMru.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRemove});
            this.cmsMru.Name = "cmsMru";
            this.cmsMru.Size = new System.Drawing.Size(282, 76);
            this.cmsMru.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmsMru_ItemClicked);
            // 
            // tsmiRemove
            // 
            this.tsmiRemove.Image = global::XrmToolBox.Properties.Resources.close;
            this.tsmiRemove.Name = "tsmiRemove";
            this.tsmiRemove.Size = new System.Drawing.Size(293, 34);
            this.tsmiRemove.Text = "Remove from this list";
            this.tsmiRemove.ToolTipText = "Remove this item from the Most Recently Used items";
            // 
            // pbLogo
            // 
            this.pbLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbLogo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbLogo.Location = new System.Drawing.Point(18, 9);
            this.pbLogo.Margin = new System.Windows.Forms.Padding(5, 14, 5, 14);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(110, 112);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // FavoriteControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ContextMenuStrip = this.cmsMru;
            this.Controls.Add(this.lblPlugin);
            this.Controls.Add(this.pbLogo);
            this.Margin = new System.Windows.Forms.Padding(18);
            this.Name = "FavoriteControl";
            this.Padding = new System.Windows.Forms.Padding(18, 9, 0, 9);
            this.Size = new System.Drawing.Size(882, 130);
            this.Load += new System.EventHandler(this.MostRecentlyUsedItemControl_Load);
            this.cmsMru.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label lblPlugin;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ContextMenuStrip cmsMru;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemove;
    }
}
