namespace XrmToolBox.CustomControls
{
    partial class XmlViewerControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XmlViewerControl));
            XrmToolBox.CustomControls.XMLViewerSettings xmlViewerSettings1 = new XrmToolBox.CustomControls.XMLViewerSettings();
            this.ToolStripTools = new System.Windows.Forms.ToolStrip();
            this.ToolButtonFormat = new System.Windows.Forms.ToolStripButton();
            this.ToolButtonSettings = new System.Windows.Forms.ToolStripButton();
            this.PanelSettingsModal = new System.Windows.Forms.Panel();
            this.PropertyGridSettings = new System.Windows.Forms.PropertyGrid();
            this.PanelSettingsButtons = new System.Windows.Forms.Panel();
            this.ButtonSettingsReset = new System.Windows.Forms.Button();
            this.ButtonSettingsOk = new System.Windows.Forms.Button();
            this.StatusStripErrors = new System.Windows.Forms.StatusStrip();
            this.ToolStripLabelErrorMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.XmlViewer = new XrmToolBox.CustomControls.XMLViewerBaseControl();
            this.ToolStripTools.SuspendLayout();
            this.PanelSettingsModal.SuspendLayout();
            this.PanelSettingsButtons.SuspendLayout();
            this.StatusStripErrors.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolStripTools
            // 
            this.ToolStripTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolButtonFormat,
            this.ToolButtonSettings});
            this.ToolStripTools.Location = new System.Drawing.Point(0, 0);
            this.ToolStripTools.Name = "ToolStripTools";
            this.ToolStripTools.Size = new System.Drawing.Size(786, 37);
            this.ToolStripTools.TabIndex = 3;
            // 
            // ToolButtonFormat
            // 
            this.ToolButtonFormat.Image = ((System.Drawing.Image)(resources.GetObject("ToolButtonFormat.Image")));
            this.ToolButtonFormat.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolButtonFormat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolButtonFormat.Name = "ToolButtonFormat";
            this.ToolButtonFormat.Size = new System.Drawing.Size(139, 34);
            this.ToolButtonFormat.Text = "Format Xml";
            this.ToolButtonFormat.ToolTipText = "Format Xml (Ctrl+S)";
            this.ToolButtonFormat.Click += new System.EventHandler(this.ToolButtonFormat_Click);
            // 
            // ToolButtonSettings
            // 
            this.ToolButtonSettings.Image = ((System.Drawing.Image)(resources.GetObject("ToolButtonSettings.Image")));
            this.ToolButtonSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolButtonSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolButtonSettings.Name = "ToolButtonSettings";
            this.ToolButtonSettings.Size = new System.Drawing.Size(109, 34);
            this.ToolButtonSettings.Text = "Settings";
            this.ToolButtonSettings.Click += new System.EventHandler(this.ToolButtonSettings_Click);
            // 
            // PanelSettingsModal
            // 
            this.PanelSettingsModal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelSettingsModal.Controls.Add(this.PropertyGridSettings);
            this.PanelSettingsModal.Controls.Add(this.PanelSettingsButtons);
            this.PanelSettingsModal.Location = new System.Drawing.Point(3, 42);
            this.PanelSettingsModal.Name = "PanelSettingsModal";
            this.PanelSettingsModal.Padding = new System.Windows.Forms.Padding(10);
            this.PanelSettingsModal.Size = new System.Drawing.Size(523, 674);
            this.PanelSettingsModal.TabIndex = 5;
            this.PanelSettingsModal.Visible = false;
            // 
            // PropertyGridSettings
            // 
            this.PropertyGridSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PropertyGridSettings.Location = new System.Drawing.Point(10, 10);
            this.PropertyGridSettings.Name = "PropertyGridSettings";
            this.PropertyGridSettings.Size = new System.Drawing.Size(501, 592);
            this.PropertyGridSettings.TabIndex = 0;
            // 
            // PanelSettingsButtons
            // 
            this.PanelSettingsButtons.Controls.Add(this.ButtonSettingsReset);
            this.PanelSettingsButtons.Controls.Add(this.ButtonSettingsOk);
            this.PanelSettingsButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelSettingsButtons.Location = new System.Drawing.Point(10, 602);
            this.PanelSettingsButtons.Name = "PanelSettingsButtons";
            this.PanelSettingsButtons.Padding = new System.Windows.Forms.Padding(5);
            this.PanelSettingsButtons.Size = new System.Drawing.Size(501, 60);
            this.PanelSettingsButtons.TabIndex = 1;
            // 
            // ButtonSettingsReset
            // 
            this.ButtonSettingsReset.Location = new System.Drawing.Point(292, 5);
            this.ButtonSettingsReset.Margin = new System.Windows.Forms.Padding(3, 3, 15, 3);
            this.ButtonSettingsReset.Name = "ButtonSettingsReset";
            this.ButtonSettingsReset.Size = new System.Drawing.Size(98, 53);
            this.ButtonSettingsReset.TabIndex = 1;
            this.ButtonSettingsReset.Text = "Reset";
            this.ButtonSettingsReset.UseVisualStyleBackColor = true;
            this.ButtonSettingsReset.Click += new System.EventHandler(this.ButtonSettingsReset_Click);
            // 
            // ButtonSettingsOk
            // 
            this.ButtonSettingsOk.Location = new System.Drawing.Point(400, 5);
            this.ButtonSettingsOk.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.ButtonSettingsOk.Name = "ButtonSettingsOk";
            this.ButtonSettingsOk.Size = new System.Drawing.Size(98, 53);
            this.ButtonSettingsOk.TabIndex = 0;
            this.ButtonSettingsOk.Text = "OK";
            this.ButtonSettingsOk.UseVisualStyleBackColor = true;
            this.ButtonSettingsOk.Click += new System.EventHandler(this.ButtonSettingsOk_Click);
            // 
            // StatusStripErrors
            // 
            this.StatusStripErrors.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.StatusStripErrors.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripLabelErrorMessage});
            this.StatusStripErrors.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.StatusStripErrors.Location = new System.Drawing.Point(0, 629);
            this.StatusStripErrors.Name = "StatusStripErrors";
            this.StatusStripErrors.ShowItemToolTips = true;
            this.StatusStripErrors.Size = new System.Drawing.Size(786, 35);
            this.StatusStripErrors.TabIndex = 6;
            // 
            // ToolStripLabelErrorMessage
            // 
            this.ToolStripLabelErrorMessage.AutoToolTip = true;
            this.ToolStripLabelErrorMessage.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripLabelErrorMessage.Image")));
            this.ToolStripLabelErrorMessage.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ToolStripLabelErrorMessage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ToolStripLabelErrorMessage.Name = "ToolStripLabelErrorMessage";
            this.ToolStripLabelErrorMessage.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.ToolStripLabelErrorMessage.Size = new System.Drawing.Size(141, 30);
            this.ToolStripLabelErrorMessage.Text = "Parse Error: ";
            this.ToolStripLabelErrorMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // XmlViewer
            // 
            this.XmlViewer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.XmlViewer.CurrentParseError = null;
            this.XmlViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.XmlViewer.FormatAsYouType = true;
            this.XmlViewer.Location = new System.Drawing.Point(0, 37);
            this.XmlViewer.Name = "XmlViewer";
            xmlViewerSettings1.AttributeKey = System.Drawing.Color.Blue;
            xmlViewerSettings1.AttributeValue = System.Drawing.Color.DarkRed;
            xmlViewerSettings1.Comment = System.Drawing.Color.Gray;
            xmlViewerSettings1.Element = System.Drawing.Color.DarkGreen;
            xmlViewerSettings1.FontName = "Consolas";
            xmlViewerSettings1.FontSize = 9F;
            xmlViewerSettings1.QuoteCharacter = '\"';
            xmlViewerSettings1.Tag = System.Drawing.Color.ForestGreen;
            xmlViewerSettings1.Value = System.Drawing.Color.Black;
            this.XmlViewer.Settings = xmlViewerSettings1;
            this.XmlViewer.Size = new System.Drawing.Size(786, 592);
            this.XmlViewer.TabIndex = 2;
            this.XmlViewer.Text = "";
            this.XmlViewer.NotificationMessage += new System.EventHandler<XrmToolBox.CustomControls.NotificationEventArgs>(this.xmlViewer_NotificationMessage);
            // 
            // XmlViewerControl
            // 
            this.Controls.Add(this.XmlViewer);
            this.Controls.Add(this.StatusStripErrors);
            this.Controls.Add(this.PanelSettingsModal);
            this.Controls.Add(this.ToolStripTools);
            this.Name = "XmlViewerControl";
            this.Size = new System.Drawing.Size(786, 664);
            this.Load += new System.EventHandler(this.XmlViewerControl_Load);
            this.BackColorChanged += new System.EventHandler(this.XmlViewerControl_PropertyChanged);
            this.CursorChanged += new System.EventHandler(this.XmlViewerControl_PropertyChanged);
            this.EnabledChanged += new System.EventHandler(this.XmlViewerControl_PropertyChanged);
            this.FontChanged += new System.EventHandler(this.XmlViewerControl_PropertyChanged);
            this.ForeColorChanged += new System.EventHandler(this.XmlViewerControl_PropertyChanged);
            this.RightToLeftChanged += new System.EventHandler(this.XmlViewerControl_PropertyChanged);
            this.ToolStripTools.ResumeLayout(false);
            this.ToolStripTools.PerformLayout();
            this.PanelSettingsModal.ResumeLayout(false);
            this.PanelSettingsButtons.ResumeLayout(false);
            this.StatusStripErrors.ResumeLayout(false);
            this.StatusStripErrors.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private XMLViewerBaseControl XmlViewer;
        private System.Windows.Forms.ToolStrip ToolStripTools;
        private System.Windows.Forms.ToolStripButton ToolButtonFormat;
        private System.Windows.Forms.ToolStripButton ToolButtonSettings;
        private System.Windows.Forms.Panel PanelSettingsModal;
        private System.Windows.Forms.Panel PanelSettingsButtons;
        private System.Windows.Forms.Button ButtonSettingsReset;
        private System.Windows.Forms.Button ButtonSettingsOk;
        private System.Windows.Forms.PropertyGrid PropertyGridSettings;
        private System.Windows.Forms.StatusStrip StatusStripErrors;
        private System.Windows.Forms.ToolStripStatusLabel ToolStripLabelErrorMessage;
    }
}
