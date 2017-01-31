namespace XrmToolBox.Extensibility.UserControls
{
    partial class NotificationArea
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
            this.pbNotif = new System.Windows.Forms.PictureBox();
            this.llLearMore = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.llDismiss = new System.Windows.Forms.LinkLabel();
            this.lblMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbNotif)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbNotif
            // 
            this.pbNotif.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbNotif.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbNotif.Location = new System.Drawing.Point(0, 0);
            this.pbNotif.Name = "pbNotif";
            this.pbNotif.Size = new System.Drawing.Size(32, 47);
            this.pbNotif.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbNotif.TabIndex = 1;
            this.pbNotif.TabStop = false;
            // 
            // llLearMore
            // 
            this.llLearMore.AutoSize = true;
            this.llLearMore.Dock = System.Windows.Forms.DockStyle.Top;
            this.llLearMore.Location = new System.Drawing.Point(0, 0);
            this.llLearMore.Name = "llLearMore";
            this.llLearMore.Size = new System.Drawing.Size(84, 20);
            this.llLearMore.TabIndex = 4;
            this.llLearMore.TabStop = true;
            this.llLearMore.Text = "learn more";
            this.llLearMore.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llLearnMore_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.llDismiss);
            this.panel1.Controls.Add(this.llLearMore);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(582, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(89, 47);
            this.panel1.TabIndex = 5;
            // 
            // llDismiss
            // 
            this.llDismiss.AutoSize = true;
            this.llDismiss.Dock = System.Windows.Forms.DockStyle.Top;
            this.llDismiss.Location = new System.Drawing.Point(0, 20);
            this.llDismiss.Name = "llDismiss";
            this.llDismiss.Size = new System.Drawing.Size(61, 20);
            this.llDismiss.TabIndex = 5;
            this.llDismiss.TabStop = true;
            this.llDismiss.Text = "dismiss";
            this.llDismiss.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDismiss_LinkClicked);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMessage.Location = new System.Drawing.Point(32, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(82, 20);
            this.lblMessage.TabIndex = 6;
            this.lblMessage.Text = "[Message]";
            // 
            // NotificationArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pbNotif);
            this.Name = "NotificationArea";
            this.Size = new System.Drawing.Size(671, 47);
            ((System.ComponentModel.ISupportInitialize)(this.pbNotif)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pbNotif;
        private System.Windows.Forms.LinkLabel llLearMore;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel llDismiss;
        private System.Windows.Forms.Label lblMessage;
    }
}
