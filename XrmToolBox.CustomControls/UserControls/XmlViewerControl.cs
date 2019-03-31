using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace XrmToolBox.CustomControls
{
    /// <summary>
    /// Wraps the XmlViewer Control functionality with some helper UI elements
    /// </summary>
    public partial class XmlViewerControl : UserControl
    {
        /// <summary>
        /// Event handler to show processing errors
        /// </summary>
        [Category("XrmToolBox")]
        public event EventHandler<NotificationEventArgs> NotificationMessage;

        private bool _displayParseErrors;
        private XMLViewerSettings _settings = new XMLViewerSettings();

        /// <summary>
        /// Constructor!
        /// </summary>
        public XmlViewerControl()
        {
            InitializeComponent();
        }

        #region Public properties for the RichText control
        /// <summary>
        /// The text only content of the RichText Control
        /// </summary>
        [Category("Code Editor")]
        [Browsable(true)]
        public string Text
        {
            get => XmlViewer.Text;
            set { XmlViewer.Text = value; }
        }

        /// <summary>
        /// ScrollBars RichText control property
        /// </summary>
        [Category("Code Editor")]
        [Browsable(true)]
        public RichTextBoxScrollBars ScrollBars { get => XmlViewer.ScrollBars; set => XmlViewer.ScrollBars = value; }

        /// <summary>
        /// AcceptsTab RichText control property
        /// </summary>
        [Category("Code Editor")]
        [Browsable(true)]
        public bool AcceptsTab { get => XmlViewer.AcceptsTab; set => XmlViewer.AcceptsTab = value; }

        /// <summary>
        /// AutoWordSelection RichText control property
        /// </summary>
        [Category("Code Editor")]
        [Browsable(true)]
        public bool AutoWordSelection { get => XmlViewer.AutoWordSelection; set => XmlViewer.AutoWordSelection = value; }

        /// <summary>
        /// RichText control property
        /// </summary>
        [Category("Code Editor")]
        [Browsable(true)]
        public int BulletIndent { get => XmlViewer.BulletIndent; set => XmlViewer.BulletIndent = value; }

        /// <summary>
        /// DetectUrls RichText control property
        /// </summary>
        [Category("Code Editor")]
        [Browsable(true)]
        public bool DetectUrls { get => XmlViewer.DetectUrls; set => XmlViewer.DetectUrls = value; }

        /// <summary>
        /// EnableAutoDragDrop RichText control property
        /// </summary>
        [Category("Code Editor")]
        [Browsable(true)]
        public bool EnableAutoDragDrop { get => XmlViewer.EnableAutoDragDrop; set => XmlViewer.EnableAutoDragDrop = value; }

        /// <summary>
        /// HideSelection RichText control property
        /// </summary>
        [Category("Code Editor")]
        [Browsable(true)]
        public bool HideSelection { get => XmlViewer.HideSelection; set => XmlViewer.HideSelection = value; }

        /// <summary>
        /// MaxLength RichText control property
        /// </summary>
        [Category("Code Editor")]
        [Browsable(true)]
        public int MaxLength { get => XmlViewer.MaxLength; set => XmlViewer.MaxLength = value; }

        /// <summary>
        /// Multiline RichText control property
        /// </summary>
        [Category("Code Editor")]
        [Browsable(true)]
        public bool Multiline { get => XmlViewer.Multiline; set => XmlViewer.Multiline = value; }

        /// <summary>
        /// ReadOnly RichText control property
        /// </summary>
        [Category("Code Editor")]
        [Browsable(true)]
        public bool ReadOnly { get => XmlViewer.ReadOnly; set => XmlViewer.ReadOnly = value; }

        /// <summary>
        /// RightMargin RichText control property
        /// </summary>
        [Category("Code Editor")]
        [Browsable(true)]
        public int RightMargin { get => XmlViewer.RightMargin; set => XmlViewer.RightMargin = value; }

        /// <summary>
        /// ShortcutsEnabled RichText control property
        /// </summary>
        [Category("Code Editor")]
        [Browsable(true)]
        public bool ShortcutsEnabled { get => XmlViewer.ShortcutsEnabled; set => XmlViewer.ShortcutsEnabled = value; }

        /// <summary>
        /// ShowSelectionMargin RichText control property
        /// </summary>
        [Category("Code Editor")]
        [Browsable(true)]
        public bool ShowSelectionMargin { get => XmlViewer.ShowSelectionMargin; set => XmlViewer.ShowSelectionMargin = value; }

        /// <summary>
        /// WordWrap RichText control property
        /// </summary>
        [Category("Code Editor")]
        [Browsable(true)]
        public bool WordWrap { get => XmlViewer.WordWrap; set => XmlViewer.WordWrap = value; }

        /// <summary>
        /// ZoomFactor RichText control property
        /// </summary>
        [Category("Code Editor")]
        [Browsable(true)]
        public float ZoomFactor { get => XmlViewer.ZoomFactor; set => XmlViewer.ZoomFactor = value; }

        #endregion

        #region Additional Settings
        /// <summary>
        /// Display Xml parsing errors in the status area. Default to 'true'
        /// </summary>
        [Category("XrmToolBox")]
        [Browsable(true)]
        [Description("Display Xml parsing errors in the status area. Default to 'true'")]
        public bool DisplayParseErrors
        {
            get => _displayParseErrors;
            set
            {
                _displayParseErrors = value;
                SetStatusMessage();
                Process();
            }
        }

        /// <summary>
        /// Attempt to format and apply styles to the Xml as you type.  Default to 'true'
        /// </summary>
        [Category("XrmToolBox")]
        [Browsable(true)]
        [Description("Attempt to format and apply styles to the Xml as you type.  Default to 'true'")]
        public bool FormatAsYouType { get => XmlViewer.FormatAsYouType; set => XmlViewer.FormatAsYouType = value; }

        /// <summary>
        /// Display the toolbar on on the control.  Default to 'true'
        /// </summary>
        [Category("XrmToolBox")]
        [Browsable(true)]
        [Description("Display the toolbar on on the control.  Default to 'true'")]
        public bool DisplayToolbar
        {
            get => ToolStripTools.Visible;
            set => ToolStripTools.Visible = value;
        }

        /// <summary>
        /// The format settings.
        /// </summary>
        [Category("XrmToolBox")]
        [Browsable(true)]
        public XMLViewerSettings Settings
        {
            get => XmlViewer.Settings;
            set => XmlViewer.Settings = value;
        }


        /// <summary>
        /// Used for the designer, allow reset of the Settngs object in the property grid
        /// </summary>
        protected virtual void ResetSettings()
        {
            XmlViewer.Settings = new XMLViewerSettings();
        }
        /// <summary>
        /// Property Grid helper methods 
        /// </summary>
        /// <returns></returns>
        protected virtual bool ShoulSettings()
        {
            return true;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Process the current text and apply Xml formatting and styles
        /// </summary>
        public void Process()
        {
            XmlViewer.Process();
        }

        /// <summary>
        /// Helper method for displaying the Stauts message
        /// </summary>
        /// <param name="message"></param>
        private void SetStatusMessage(string message = null)
        {
            var err = (message != null);
            if (err)
            {
                message = $"Parse Error: {message}";
            }
            ToolStripLabelErrorMessage.Text = message;
            ToolStripLabelErrorMessage.ToolTipText = message;
            StatusStripErrors.Visible = err;
        }
        #endregion

        #region Control event handlers

        /// <summary>
        /// Handle the error/info notification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xmlViewer_NotificationMessage(object sender, NotificationEventArgs e)
        {
            if (DisplayParseErrors)
            {
                if (e.Level == MessageLevel.Information)
                {
                    SetStatusMessage(null);
                }
                else
                {
                    SetStatusMessage(e.Message);
                }
            }
            // bubble up the notification message
            NotificationMessage?.Invoke(this, e);
        }

        /// <summary>
        /// Manually apply the format and theme  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolButtonFormat_Click(object sender, EventArgs e)
        {
            Process();
        }

        /// <summary>
        /// On load of the main control 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XmlViewerControl_Load(object sender, EventArgs e)
        {
            // on load, clone the settings that are loaded from the designer res
            // this will allow us to reset the settings if needed

            // TODO - this can be overridden from the parent control if they want to save settings
            _settings = Settings.Copy();
        }

        /// <summary>
        /// Handle some of these property change events and pass the properties on to the XmlView control 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XmlViewerControl_PropertyChanged(object sender, EventArgs e)
        {
            XmlViewer.Enabled = Enabled;
            ToolButtonFormat.Enabled = Enabled;
            ToolButtonSettings.Enabled = Enabled;

            XmlViewer.Cursor = Cursor;
            XmlViewer.Font = Font;
            XmlViewer.ForeColor = ForeColor;
            XmlViewer.RightToLeft = RightToLeft;
            XmlViewer.BackColor = BackColor;
        }
        #region Settings 

        /// <summary>
        /// Settings OK button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSettingsOk_Click(object sender, EventArgs e)
        {
            PanelSettingsModal.SendToBack();
            PanelSettingsModal.Visible = false;
        }

        /// <summary>
        /// Settings Cancel Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSettingsReset_Click(object sender, EventArgs e)
        {
            // reset the settings back to the initial load
            Settings = _settings.Copy();
            PropertyGridSettings.SelectedObject = Settings;
        }

        /// <summary>
        /// Display and position the settings control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolButtonSettings_Click(object sender, EventArgs e)
        {
            PanelSettingsModal.Top = ToolStripTools.Height + 10;
            PanelSettingsModal.Left = ClientRectangle.Width / 2 - PanelSettingsModal.Width / 2;
            PropertyGridSettings.SelectedObject = Settings;
            PanelSettingsModal.Visible = true;
            PanelSettingsModal.BringToFront();

        }
        #endregion

        #endregion

    }
}
