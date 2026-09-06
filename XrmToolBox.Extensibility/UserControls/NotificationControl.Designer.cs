namespace XrmToolBox.Extensibility.UserControls
{
    partial class NotificationControl
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
            this.lblClose = new System.Windows.Forms.Label();
            this.btnAction = new System.Windows.Forms.Button();
            this.lLabel = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lblClose
            // 
            this.lblClose.BackColor = System.Drawing.Color.Transparent;
            this.lblClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblClose.Location = new System.Drawing.Point(607, 4);
            this.lblClose.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(29, 28);
            this.lblClose.TabIndex = 0;
            this.lblClose.Text = "X";
            this.lblClose.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblClose.Click += new System.EventHandler(this.lblClose_Click);
            // 
            // btnAction
            // 
            this.btnAction.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAction.Location = new System.Drawing.Point(478, 4);
            this.btnAction.Name = "btnAction";
            this.btnAction.Size = new System.Drawing.Size(129, 28);
            this.btnAction.TabIndex = 1;
            this.btnAction.Text = "button1";
            this.btnAction.UseVisualStyleBackColor = true;
            this.btnAction.Click += new System.EventHandler(this.btnAction_Click);
            // 
            // lLabel
            // 
            this.lLabel.BackColor = System.Drawing.Color.Transparent;
            this.lLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.lLabel.Location = new System.Drawing.Point(354, 4);
            this.lLabel.Name = "lLabel";
            this.lLabel.Size = new System.Drawing.Size(124, 28);
            this.lLabel.TabIndex = 2;
            this.lLabel.TabStop = true;
            this.lLabel.Text = "linkLabel1";
            this.lLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lLabel_LinkClicked);
            // 
            // NotificationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lLabel);
            this.Controls.Add(this.btnAction);
            this.Controls.Add(this.lblClose);
            this.Name = "NotificationControl";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Size = new System.Drawing.Size(640, 36);
            this.Load += new System.EventHandler(this.NotificationControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.NotificationControl_Paint);
            this.Resize += new System.EventHandler(this.NotificationControl_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.Button btnAction;
        private System.Windows.Forms.LinkLabel lLabel;
    }
}
