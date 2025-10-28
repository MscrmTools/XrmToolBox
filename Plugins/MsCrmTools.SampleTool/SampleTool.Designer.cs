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
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.txtState = new System.Windows.Forms.TextBox();
            this.lblState = new System.Windows.Forms.Label();
            this.btnCheckMultiSample = new System.Windows.Forms.Button();
            this.btnDoSomethingWrong = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnOpenNotif = new System.Windows.Forms.Button();
            this.gbToast = new System.Windows.Forms.GroupBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.gbErrorHandling = new System.Windows.Forms.GroupBox();
            this.gbOptions.SuspendLayout();
            this.toolStripMenu.SuspendLayout();
            this.gbToast.SuspendLayout();
            this.gbErrorHandling.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.cbMultipleCalls);
            this.gbOptions.Location = new System.Drawing.Point(21, 53);
            this.gbOptions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbOptions.Size = new System.Drawing.Size(375, 60);
            this.gbOptions.TabIndex = 3;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Options";
            // 
            // cbMultipleCalls
            // 
            this.cbMultipleCalls.AutoSize = true;
            this.cbMultipleCalls.Location = new System.Drawing.Point(18, 22);
            this.cbMultipleCalls.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbMultipleCalls.Name = "cbMultipleCalls";
            this.cbMultipleCalls.Size = new System.Drawing.Size(106, 20);
            this.cbMultipleCalls.TabIndex = 0;
            this.cbMultipleCalls.Text = "Multiple calls";
            this.cbMultipleCalls.UseVisualStyleBackColor = true;
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.toolStripSeparator2,
            this.tsbWhoAmI,
            this.toolStripSeparator1,
            this.tsbCancel,
            this.toolStripSeparator3,
            this.tsbDuplicate,
            this.toolStripSeparator4});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStripMenu.Size = new System.Drawing.Size(1238, 31);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(29, 28);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbWhoAmI
            // 
            this.tsbWhoAmI.Image = ((System.Drawing.Image)(resources.GetObject("tsbWhoAmI.Image")));
            this.tsbWhoAmI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbWhoAmI.Name = "tsbWhoAmI";
            this.tsbWhoAmI.Size = new System.Drawing.Size(101, 28);
            this.tsbWhoAmI.Text = "Who am I";
            this.tsbWhoAmI.ToolTipText = "Perfomrs a Who I Am request";
            this.tsbWhoAmI.Click += new System.EventHandler(this.tsbWhoAmI_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbCancel
            // 
            this.tsbCancel.Enabled = false;
            this.tsbCancel.Image = ((System.Drawing.Image)(resources.GetObject("tsbCancel.Image")));
            this.tsbCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCancel.Name = "tsbCancel";
            this.tsbCancel.Size = new System.Drawing.Size(81, 28);
            this.tsbCancel.Text = "Cancel";
            this.tsbCancel.ToolTipText = "Cancel the current request";
            this.tsbCancel.Click += new System.EventHandler(this.tsbCancel_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbDuplicate
            // 
            this.tsbDuplicate.Image = ((System.Drawing.Image)(resources.GetObject("tsbDuplicate.Image")));
            this.tsbDuplicate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDuplicate.Name = "tsbDuplicate";
            this.tsbDuplicate.Size = new System.Drawing.Size(101, 28);
            this.tsbDuplicate.Text = "Duplicate";
            this.tsbDuplicate.Click += new System.EventHandler(this.tsbDuplicate_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
            // 
            // txtState
            // 
            this.txtState.Location = new System.Drawing.Point(452, 141);
            this.txtState.Margin = new System.Windows.Forms.Padding(2);
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(217, 22);
            this.txtState.TabIndex = 5;
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(449, 117);
            this.lblState.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(38, 16);
            this.lblState.TabIndex = 6;
            this.lblState.Text = "State";
            // 
            // btnCheckMultiSample
            // 
            this.btnCheckMultiSample.Location = new System.Drawing.Point(452, 65);
            this.btnCheckMultiSample.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCheckMultiSample.Name = "btnCheckMultiSample";
            this.btnCheckMultiSample.Size = new System.Drawing.Size(386, 30);
            this.btnCheckMultiSample.TabIndex = 7;
            this.btnCheckMultiSample.Text = "Is Sample Tool for multiconnection installed?";
            this.btnCheckMultiSample.UseVisualStyleBackColor = true;
            this.btnCheckMultiSample.Click += new System.EventHandler(this.btnCheckMultiSample_Click);
            // 
            // btnDoSomethingWrong
            // 
            this.btnDoSomethingWrong.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDoSomethingWrong.Location = new System.Drawing.Point(6, 21);
            this.btnDoSomethingWrong.Margin = new System.Windows.Forms.Padding(4);
            this.btnDoSomethingWrong.Name = "btnDoSomethingWrong";
            this.btnDoSomethingWrong.Size = new System.Drawing.Size(363, 44);
            this.btnDoSomethingWrong.TabIndex = 8;
            this.btnDoSomethingWrong.Text = "Do something wrong";
            this.btnDoSomethingWrong.UseVisualStyleBackColor = true;
            this.btnDoSomethingWrong.Click += new System.EventHandler(this.btnDoSomethingWrong_Click);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.Location = new System.Drawing.Point(6, 65);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(363, 48);
            this.button1.TabIndex = 9;
            this.button1.Text = "Do something wrong without catch";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnOpenNotif
            // 
            this.btnOpenNotif.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnOpenNotif.Location = new System.Drawing.Point(6, 143);
            this.btnOpenNotif.Name = "btnOpenNotif";
            this.btnOpenNotif.Size = new System.Drawing.Size(363, 36);
            this.btnOpenNotif.TabIndex = 10;
            this.btnOpenNotif.Text = "Open Notification";
            this.btnOpenNotif.UseVisualStyleBackColor = true;
            this.btnOpenNotif.Click += new System.EventHandler(this.btnOpenNotif_Click);
            // 
            // gbToast
            // 
            this.gbToast.Controls.Add(this.textBox1);
            this.gbToast.Controls.Add(this.btnOpenNotif);
            this.gbToast.Controls.Add(this.lblMessage);
            this.gbToast.Controls.Add(this.txtHeader);
            this.gbToast.Controls.Add(this.lblHeader);
            this.gbToast.Location = new System.Drawing.Point(21, 246);
            this.gbToast.Name = "gbToast";
            this.gbToast.Padding = new System.Windows.Forms.Padding(6);
            this.gbToast.Size = new System.Drawing.Size(375, 185);
            this.gbToast.TabIndex = 11;
            this.gbToast.TabStop = false;
            this.gbToast.Text = "Toast Notification";
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Location = new System.Drawing.Point(6, 21);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(363, 30);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Header";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtHeader
            // 
            this.txtHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtHeader.Location = new System.Drawing.Point(6, 51);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(363, 22);
            this.txtHeader.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Location = new System.Drawing.Point(6, 103);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(363, 22);
            this.textBox1.TabIndex = 3;
            // 
            // lblMessage
            // 
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMessage.Location = new System.Drawing.Point(6, 73);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(363, 30);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "Message";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbErrorHandling
            // 
            this.gbErrorHandling.Controls.Add(this.button1);
            this.gbErrorHandling.Controls.Add(this.btnDoSomethingWrong);
            this.gbErrorHandling.Location = new System.Drawing.Point(21, 120);
            this.gbErrorHandling.Name = "gbErrorHandling";
            this.gbErrorHandling.Padding = new System.Windows.Forms.Padding(6);
            this.gbErrorHandling.Size = new System.Drawing.Size(375, 123);
            this.gbErrorHandling.TabIndex = 12;
            this.gbErrorHandling.TabStop = false;
            this.gbErrorHandling.Text = "Error management";
            // 
            // SampleTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbErrorHandling);
            this.Controls.Add(this.gbToast);
            this.Controls.Add(this.btnCheckMultiSample);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.txtState);
            this.Controls.Add(this.toolStripMenu);
            this.Controls.Add(this.gbOptions);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SampleTool";
            this.PluginIcon = ((System.Drawing.Icon)(resources.GetObject("$this.PluginIcon")));
            this.Size = new System.Drawing.Size(1238, 584);
            this.Load += new System.EventHandler(this.SampleTool_Load);
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.gbToast.ResumeLayout(false);
            this.gbToast.PerformLayout();
            this.gbErrorHandling.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Button btnCheckMultiSample;
        private System.Windows.Forms.Button btnDoSomethingWrong;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnOpenNotif;
        private System.Windows.Forms.GroupBox gbToast;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.GroupBox gbErrorHandling;
    }
}
