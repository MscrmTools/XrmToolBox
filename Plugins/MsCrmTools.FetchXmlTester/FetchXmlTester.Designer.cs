namespace MsCrmTools.FetchXmlTester
{
    partial class FetchXmlTester
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FetchXmlTester));
            CSRichTextBoxSyntaxHighlighting.XMLViewerSettings xmlViewerSettings5 = new CSRichTextBoxSyntaxHighlighting.XMLViewerSettings();
            CSRichTextBoxSyntaxHighlighting.XMLViewerSettings xmlViewerSettings6 = new CSRichTextBoxSyntaxHighlighting.XMLViewerSettings();
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExecute = new System.Windows.Forms.ToolStripButton();
            this.toolImageList = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtResponse = new CSRichTextBoxSyntaxHighlighting.XMLViewer();
            this.txtRequest = new CSRichTextBoxSyntaxHighlighting.XMLViewer();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripMenu.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.toolStripSeparator1,
            this.tsbExecute,
            this.toolStripSeparator2,
            this.toolStripButton1});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(911, 25);
            this.toolStripMenu.TabIndex = 2;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(102, 22);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.TsbCloseClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbExecute
            // 
            this.tsbExecute.Image = ((System.Drawing.Image)(resources.GetObject("tsbExecute.Image")));
            this.tsbExecute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExecute.Name = "tsbExecute";
            this.tsbExecute.Size = new System.Drawing.Size(67, 22);
            this.tsbExecute.Text = "Execute";
            this.tsbExecute.Click += new System.EventHandler(this.TsbExecuteClick);
            // 
            // toolImageList
            // 
            this.toolImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("toolImageList.ImageStream")));
            this.toolImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.toolImageList.Images.SetKeyName(0, "Icon.png");
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(905, 569);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtRequest);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(897, 543);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Request";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtResponse);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(897, 543);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Response";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtResponse
            // 
            this.txtResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResponse.Location = new System.Drawing.Point(6, 6);
            this.txtResponse.Name = "txtResponse";
            xmlViewerSettings5.AttributeKey = System.Drawing.Color.Red;
            xmlViewerSettings5.AttributeValue = System.Drawing.Color.Blue;
            xmlViewerSettings5.Element = System.Drawing.Color.DarkRed;
            xmlViewerSettings5.Tag = System.Drawing.Color.Blue;
            xmlViewerSettings5.Value = System.Drawing.Color.Black;
            this.txtResponse.Settings = xmlViewerSettings5;
            this.txtResponse.Size = new System.Drawing.Size(885, 531);
            this.txtResponse.TabIndex = 0;
            this.txtResponse.Text = "";
            // 
            // txtRequest
            // 
            this.txtRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRequest.Location = new System.Drawing.Point(6, 6);
            this.txtRequest.Name = "txtRequest";
            xmlViewerSettings6.AttributeKey = System.Drawing.Color.Red;
            xmlViewerSettings6.AttributeValue = System.Drawing.Color.Blue;
            xmlViewerSettings6.Element = System.Drawing.Color.DarkRed;
            xmlViewerSettings6.Tag = System.Drawing.Color.Blue;
            xmlViewerSettings6.Value = System.Drawing.Color.Black;
            this.txtRequest.Settings = xmlViewerSettings6;
            this.txtRequest.Size = new System.Drawing.Size(885, 531);
            this.txtRequest.TabIndex = 1;
            this.txtRequest.Text = "";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(89, 22);
            this.toolStripButton1.Text = "Format Xml";
            this.toolStripButton1.Click += new System.EventHandler(this.ToolStripButton1Click);
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(911, 600);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ImageList toolImageList;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbExecute;
        private CSRichTextBoxSyntaxHighlighting.XMLViewer txtResponse;
        private CSRichTextBoxSyntaxHighlighting.XMLViewer txtRequest;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}
