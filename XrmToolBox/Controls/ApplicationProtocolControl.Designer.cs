
namespace XrmToolBox.Controls
{
    partial class ApplicationProtocolControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationProtocolControl));
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblAppProtocolSummary = new System.Windows.Forms.Label();
            this.pnlAppProtocolPath = new System.Windows.Forms.Panel();
            this.lblAppProtocolStatus = new System.Windows.Forms.Label();
            this.pnlAppProtocolButton = new System.Windows.Forms.Panel();
            this.btnAppProtocol = new System.Windows.Forms.Button();
            this.lblAppProtocolPath = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.llProtocolDoc = new System.Windows.Forms.LinkLabel();
            this.panel4.SuspendLayout();
            this.pnlAppProtocolPath.SuspendLayout();
            this.pnlAppProtocolButton.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(this.pnlAppProtocolButton);
            this.panel4.Controls.Add(this.lblAppProtocolStatus);
            this.panel4.Controls.Add(this.pnlAppProtocolPath);
            this.panel4.Controls.Add(this.lblAppProtocolSummary);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(10);
            this.panel4.Size = new System.Drawing.Size(1312, 1009);
            this.panel4.TabIndex = 20;
            // 
            // lblAppProtocolSummary
            // 
            this.lblAppProtocolSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAppProtocolSummary.Font = new System.Drawing.Font("Segoe UI Variable Text", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppProtocolSummary.Location = new System.Drawing.Point(10, 10);
            this.lblAppProtocolSummary.Name = "lblAppProtocolSummary";
            this.lblAppProtocolSummary.Size = new System.Drawing.Size(1292, 68);
            this.lblAppProtocolSummary.TabIndex = 18;
            this.lblAppProtocolSummary.Text = resources.GetString("lblAppProtocolSummary.Text");
            // 
            // pnlAppProtocolPath
            // 
            this.pnlAppProtocolPath.Controls.Add(this.lblAppProtocolPath);
            this.pnlAppProtocolPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAppProtocolPath.Location = new System.Drawing.Point(10, 78);
            this.pnlAppProtocolPath.Name = "pnlAppProtocolPath";
            this.pnlAppProtocolPath.Padding = new System.Windows.Forms.Padding(27, 4, 27, 4);
            this.pnlAppProtocolPath.Size = new System.Drawing.Size(1292, 44);
            this.pnlAppProtocolPath.TabIndex = 22;
            // 
            // lblAppProtocolStatus
            // 
            this.lblAppProtocolStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAppProtocolStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppProtocolStatus.Location = new System.Drawing.Point(10, 122);
            this.lblAppProtocolStatus.Name = "lblAppProtocolStatus";
            this.lblAppProtocolStatus.Size = new System.Drawing.Size(1292, 77);
            this.lblAppProtocolStatus.TabIndex = 24;
            this.lblAppProtocolStatus.Tag = "Current Status : {0}";
            this.lblAppProtocolStatus.Text = "Current Status";
            this.lblAppProtocolStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlAppProtocolButton
            // 
            this.pnlAppProtocolButton.Controls.Add(this.btnAppProtocol);
            this.pnlAppProtocolButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAppProtocolButton.Location = new System.Drawing.Point(10, 199);
            this.pnlAppProtocolButton.Name = "pnlAppProtocolButton";
            this.pnlAppProtocolButton.Padding = new System.Windows.Forms.Padding(40);
            this.pnlAppProtocolButton.Size = new System.Drawing.Size(1292, 136);
            this.pnlAppProtocolButton.TabIndex = 25;
            // 
            // btnAppProtocol
            // 
            this.btnAppProtocol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAppProtocol.Font = new System.Drawing.Font("Segoe UI Variable Text", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAppProtocol.Location = new System.Drawing.Point(5, 26);
            this.btnAppProtocol.Name = "btnAppProtocol";
            this.btnAppProtocol.Size = new System.Drawing.Size(1284, 84);
            this.btnAppProtocol.TabIndex = 21;
            this.btnAppProtocol.Text = "Enable";
            this.btnAppProtocol.UseVisualStyleBackColor = true;
            this.btnAppProtocol.Click += new System.EventHandler(this.btnAppProtocol_Click);
            // 
            // lblAppProtocolPath
            // 
            this.lblAppProtocolPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAppProtocolPath.Font = new System.Drawing.Font("Segoe UI Variable Text", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppProtocolPath.Location = new System.Drawing.Point(27, 4);
            this.lblAppProtocolPath.Name = "lblAppProtocolPath";
            this.lblAppProtocolPath.Size = new System.Drawing.Size(1238, 36);
            this.lblAppProtocolPath.TabIndex = 2;
            this.lblAppProtocolPath.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.llProtocolDoc);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(10, 958);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1292, 41);
            this.panel1.TabIndex = 26;
            // 
            // llProtocolDoc
            // 
            this.llProtocolDoc.Dock = System.Windows.Forms.DockStyle.Right;
            this.llProtocolDoc.Font = new System.Drawing.Font("Segoe UI Variable Text", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llProtocolDoc.Location = new System.Drawing.Point(228, 0);
            this.llProtocolDoc.Name = "llProtocolDoc";
            this.llProtocolDoc.Size = new System.Drawing.Size(1064, 41);
            this.llProtocolDoc.TabIndex = 1;
            this.llProtocolDoc.TabStop = true;
            this.llProtocolDoc.Text = "Click here to read documentation about how to implement application protocol";
            this.llProtocolDoc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.llProtocolDoc.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llProtocolDoc_LinkClicked);
            // 
            // ApplicationProtocolControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel4);
            this.Name = "ApplicationProtocolControl";
            this.Size = new System.Drawing.Size(1312, 1009);
            this.panel4.ResumeLayout(false);
            this.pnlAppProtocolPath.ResumeLayout(false);
            this.pnlAppProtocolButton.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblAppProtocolSummary;
        private System.Windows.Forms.Panel pnlAppProtocolButton;
        private System.Windows.Forms.Button btnAppProtocol;
        private System.Windows.Forms.Label lblAppProtocolStatus;
        private System.Windows.Forms.Panel pnlAppProtocolPath;
        private System.Windows.Forms.Label lblAppProtocolPath;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel llProtocolDoc;
    }
}
