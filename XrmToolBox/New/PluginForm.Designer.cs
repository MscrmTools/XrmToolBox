using System.Windows.Forms;

namespace XrmToolBox.New
{
    partial class PluginForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.lblEnvInfo = new System.Windows.Forms.Label();
            this.NotifPanel = new XrmToolBox.Extensibility.UserControls.NotificationArea();
            this.pnlImpersonate = new System.Windows.Forms.Panel();
            this.lblImpersonation = new System.Windows.Forms.Label();
            this.btnResetImpersonate = new System.Windows.Forms.Button();
            this.pbImpersonate = new System.Windows.Forms.PictureBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tlpHighlight = new System.Windows.Forms.TableLayoutPanel();
            this.lblTargetConnections = new System.Windows.Forms.Label();
            this.lblSourceConnection = new System.Windows.Forms.Label();
            this.pnlHighlight = new System.Windows.Forms.Panel();
            this.lblEnvName = new System.Windows.Forms.Label();
            this.pbEnvLogo = new System.Windows.Forms.PictureBox();
            this.statusStrip1.SuspendLayout();
            this.pnlImpersonate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImpersonate)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tlpHighlight.SuspendLayout();
            this.pnlHighlight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbEnvLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 483);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip1.Size = new System.Drawing.Size(938, 34);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip";
            this.statusStrip1.Visible = false;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(915, 27);
            this.toolStripStatusLabel.Spring = true;
            this.toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(150, 26);
            this.toolStripProgressBar.Visible = false;
            // 
            // lblEnvInfo
            // 
            this.lblEnvInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEnvInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnvInfo.ForeColor = System.Drawing.Color.White;
            this.lblEnvInfo.Location = new System.Drawing.Point(8, 42);
            this.lblEnvInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEnvInfo.Name = "lblEnvInfo";
            this.lblEnvInfo.Size = new System.Drawing.Size(972, 42);
            this.lblEnvInfo.TabIndex = 3;
            this.lblEnvInfo.Text = "!!! PRODUCTION !!!";
            this.lblEnvInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NotifPanel
            // 
            this.NotifPanel.BackColor = System.Drawing.SystemColors.Info;
            this.NotifPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NotifPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.NotifPanel.Location = new System.Drawing.Point(0, 0);
            this.NotifPanel.Margin = new System.Windows.Forms.Padding(2);
            this.NotifPanel.Name = "NotifPanel";
            this.NotifPanel.Size = new System.Drawing.Size(972, 47);
            this.NotifPanel.TabIndex = 1;
            this.NotifPanel.Visible = false;
            // 
            // pnlImpersonate
            // 
            this.pnlImpersonate.BackColor = System.Drawing.SystemColors.Info;
            this.pnlImpersonate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlImpersonate.Controls.Add(this.lblImpersonation);
            this.pnlImpersonate.Controls.Add(this.btnResetImpersonate);
            this.pnlImpersonate.Controls.Add(this.pbImpersonate);
            this.pnlImpersonate.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlImpersonate.Location = new System.Drawing.Point(0, 47);
            this.pnlImpersonate.Margin = new System.Windows.Forms.Padding(2);
            this.pnlImpersonate.Name = "pnlImpersonate";
            this.pnlImpersonate.Padding = new System.Windows.Forms.Padding(4);
            this.pnlImpersonate.Size = new System.Drawing.Size(972, 47);
            this.pnlImpersonate.TabIndex = 2;
            this.pnlImpersonate.Visible = false;
            // 
            // lblImpersonation
            // 
            this.lblImpersonation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblImpersonation.Location = new System.Drawing.Point(38, 4);
            this.lblImpersonation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblImpersonation.Name = "lblImpersonation";
            this.lblImpersonation.Size = new System.Drawing.Size(733, 37);
            this.lblImpersonation.TabIndex = 2;
            this.lblImpersonation.Tag = "You are currently impersonated as user {0} ({1})";
            this.lblImpersonation.Text = "You are currently impersonated as user {0} ({1})";
            this.lblImpersonation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnResetImpersonate
            // 
            this.btnResetImpersonate.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnResetImpersonate.Location = new System.Drawing.Point(771, 4);
            this.btnResetImpersonate.Margin = new System.Windows.Forms.Padding(2);
            this.btnResetImpersonate.Name = "btnResetImpersonate";
            this.btnResetImpersonate.Size = new System.Drawing.Size(195, 37);
            this.btnResetImpersonate.TabIndex = 1;
            this.btnResetImpersonate.Text = "Cancel Impersonation";
            this.btnResetImpersonate.UseVisualStyleBackColor = true;
            this.btnResetImpersonate.Click += new System.EventHandler(this.btnResetImpersonate_Click);
            // 
            // pbImpersonate
            // 
            this.pbImpersonate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbImpersonate.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbImpersonate.Image = global::XrmToolBox.Properties.Resources.mask;
            this.pbImpersonate.Location = new System.Drawing.Point(4, 4);
            this.pbImpersonate.Margin = new System.Windows.Forms.Padding(2);
            this.pbImpersonate.Name = "pbImpersonate";
            this.pbImpersonate.Size = new System.Drawing.Size(34, 37);
            this.pbImpersonate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbImpersonate.TabIndex = 0;
            this.pbImpersonate.TabStop = false;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlMain.Controls.Add(this.pnlImpersonate);
            this.pnlMain.Controls.Add(this.NotifPanel);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(8, 143);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(2);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(972, 366);
            this.pnlMain.TabIndex = 4;
            // 
            // tlpHighlight
            // 
            this.tlpHighlight.ColumnCount = 2;
            this.tlpHighlight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpHighlight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpHighlight.Controls.Add(this.lblTargetConnections, 1, 0);
            this.tlpHighlight.Controls.Add(this.lblSourceConnection, 0, 0);
            this.tlpHighlight.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpHighlight.Location = new System.Drawing.Point(8, 0);
            this.tlpHighlight.Name = "tlpHighlight";
            this.tlpHighlight.RowCount = 1;
            this.tlpHighlight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpHighlight.Size = new System.Drawing.Size(972, 42);
            this.tlpHighlight.TabIndex = 6;
            this.tlpHighlight.Visible = false;
            // 
            // lblTargetConnections
            // 
            this.lblTargetConnections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTargetConnections.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTargetConnections.Location = new System.Drawing.Point(489, 0);
            this.lblTargetConnections.Name = "lblTargetConnections";
            this.lblTargetConnections.Size = new System.Drawing.Size(480, 42);
            this.lblTargetConnections.TabIndex = 0;
            this.lblTargetConnections.Text = "Target connections";
            this.lblTargetConnections.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSourceConnection
            // 
            this.lblSourceConnection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSourceConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSourceConnection.Location = new System.Drawing.Point(3, 0);
            this.lblSourceConnection.Name = "lblSourceConnection";
            this.lblSourceConnection.Size = new System.Drawing.Size(480, 42);
            this.lblSourceConnection.TabIndex = 1;
            this.lblSourceConnection.Text = "Source connection";
            this.lblSourceConnection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlHighlight
            // 
            this.pnlHighlight.Controls.Add(this.lblEnvName);
            this.pnlHighlight.Controls.Add(this.pbEnvLogo);
            this.pnlHighlight.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHighlight.Location = new System.Drawing.Point(8, 84);
            this.pnlHighlight.Name = "pnlHighlight";
            this.pnlHighlight.Size = new System.Drawing.Size(972, 59);
            this.pnlHighlight.TabIndex = 7;
            this.pnlHighlight.Visible = false;
            // 
            // lblEnvName
            // 
            this.lblEnvName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEnvName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnvName.Location = new System.Drawing.Point(62, 0);
            this.lblEnvName.Name = "lblEnvName";
            this.lblEnvName.Size = new System.Drawing.Size(910, 59);
            this.lblEnvName.TabIndex = 1;
            this.lblEnvName.Text = "Env";
            this.lblEnvName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbEnvLogo
            // 
            this.pbEnvLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbEnvLogo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbEnvLogo.Location = new System.Drawing.Point(0, 0);
            this.pbEnvLogo.Name = "pbEnvLogo";
            this.pbEnvLogo.Size = new System.Drawing.Size(62, 59);
            this.pbEnvLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbEnvLogo.TabIndex = 0;
            this.pbEnvLogo.TabStop = false;
            // 
            // PluginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 517);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlHighlight);
            this.Controls.Add(this.lblEnvInfo);
            this.Controls.Add(this.tlpHighlight);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PluginForm";
            this.Padding = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this.Text = "Tool Form";
            this.DockStateChanged += new System.EventHandler(this.PluginForm_DockStateChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PluginForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PluginForm_FormClosed);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.pnlImpersonate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImpersonate)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tlpHighlight.ResumeLayout(false);
            this.pnlHighlight.ResumeLayout(false);
            this.pnlHighlight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbEnvLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.Label lblEnvInfo;
        private Extensibility.UserControls.NotificationArea NotifPanel;
        private Panel pnlImpersonate;
        private Label lblImpersonation;
        private Button btnResetImpersonate;
        private PictureBox pbImpersonate;
        private Panel pnlMain;
        private TableLayoutPanel tlpHighlight;
        private Label lblTargetConnections;
        private Label lblSourceConnection;
        private Panel pnlHighlight;
        private Label lblEnvName;
        private PictureBox pbEnvLogo;
    }
}