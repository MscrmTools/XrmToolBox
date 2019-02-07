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
            this.panel1 = new System.Windows.Forms.Panel();
            this.NotifPanel = new XrmToolBox.Extensibility.UserControls.NotificationArea();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 604);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 28, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1250, 43);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip";
            this.statusStrip1.Visible = false;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(1220, 38);
            this.toolStripStatusLabel.Spring = true;
            this.toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(200, 37);
            this.toolStripProgressBar.Visible = false;
            // 
            // lblEnvInfo
            // 
            this.lblEnvInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEnvInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnvInfo.ForeColor = System.Drawing.Color.White;
            this.lblEnvInfo.Location = new System.Drawing.Point(11, 0);
            this.lblEnvInfo.Name = "lblEnvInfo";
            this.lblEnvInfo.Size = new System.Drawing.Size(1228, 52);
            this.lblEnvInfo.TabIndex = 3;
            this.lblEnvInfo.Text = "!!! PRODUCTION !!!";
            this.lblEnvInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.NotifPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(11, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1228, 584);
            this.panel1.TabIndex = 4;
            // 
            // NotifPanel
            // 
            this.NotifPanel.BackColor = System.Drawing.SystemColors.Info;
            this.NotifPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NotifPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.NotifPanel.Location = new System.Drawing.Point(0, 0);
            this.NotifPanel.Margin = new System.Windows.Forms.Padding(2);
            this.NotifPanel.Name = "NotifPanel";
            this.NotifPanel.Size = new System.Drawing.Size(1228, 58);
            this.NotifPanel.TabIndex = 1;
            this.NotifPanel.Visible = false;
            // 
            // PluginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 646);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblEnvInfo);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PluginForm";
            this.Padding = new System.Windows.Forms.Padding(11, 0, 11, 10);
            this.Text = "Plugin Form";
            this.DockStateChanged += new System.EventHandler(this.PluginForm_DockStateChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PluginForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PluginForm_FormClosed);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.Label lblEnvInfo;
        private System.Windows.Forms.Panel panel1;
        private Extensibility.UserControls.NotificationArea NotifPanel;
    }
}