/****************************** Module Header ******************************\
* Module Name:  XMLViewer.cs
* Project:	    CSRichTextBoxSyntaxHighlighting
* Copyright (c) Microsoft Corporation.
*
* This XMLViewer class inherits System.Windows.Forms.RichTextBox and it is used
* to display an Xml in a specified format.
*
* RichTextBox uses the Rtf format to show the test. The XMLViewer will
* convert the Xml to Rtf with some formats specified in the XMLViewerSettings,
* and then set the Rtf property to the value.
*
* This source is subject to the Microsoft Public License.
* See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
* All other rights reserved.
*
* THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
* EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
* WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using System.Text;
using System.Xml;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace XrmToolBox.CustomControls
{
    public class XMLViewerBaseControl : RichTextBox
    {
        private XMLViewerSettings _settings = new XMLViewerSettings();
        private bool _handleKeyPress = true;
        private bool _inProcessing = false;

        /// <summary>
        /// Track the current exception from parsing the Xml
        /// </summary>
        [Description("Track the current exception from parsing the Xml")]
        [Browsable(false)]
        public Exception CurrentParseError { get; set; }

        /// <summary>
        /// Event handler to show processing errors
        /// </summary>
        [Category("XrmToolBox")]
        public event EventHandler<NotificationEventArgs> NotificationMessage;

        /// <summary>
        /// Attempt to format and apply styles to the Xml as you type.  Default to 'true'
        /// </summary>
        [Category("XrmToolBox")]
        [Browsable(true)]
        [Description("Attempt to format and apply styles to the Xml as you type.  Default to 'true'")]
        public bool FormatAsYouType { get; set; } = true;

        /// <summary>
        /// Check to see if the Xml is valid 
        /// </summary>
        [Browsable(false)]
        [Description("Verify that the Xml is well formed in its current state")]
        public bool IsValidXml
        {
            get {
                return Utility.IsValidXml(Text);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public XMLViewerBaseControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // XMLViewer
            // 
            this.TextChanged += new System.EventHandler(this.XmlViewer_TextChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.XmlViewer_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.XmlViewer_KeyPress);

            this.ResumeLayout(false);
        }

        /// <summary>
        /// Handle the control property events so we can pass through to the RTF control 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnSettingsPropertyChange(object sender, PropertyChangedEventArgs args) {
            Process();
        }

        /// <summary>
        /// The format settings.
        /// </summary>
        [Category("XrmToolBox")]
        public XMLViewerSettings Settings
        {
            get
            {
                if (_settings == null)
                {
                    ResetSettings();
                }
                return _settings;
            }
            set
            {
                _settings = value;
                _settings.PropertyChanged += new PropertyChangedEventHandler(this.OnSettingsPropertyChange);
                Process();
            }
        }

        /// <summary>
        /// Used for the designer, allow reset of the Settngs object in the property grid
        /// </summary>
        protected virtual void ResetSettings()
        {
            _settings = new XMLViewerSettings();
        }
        /// <summary>
        /// Property Grid helper methods 
        /// </summary>
        /// <returns></returns>
        protected virtual bool ShoulSettings()
        {
            return true;
        }

        /// <summary>
        /// Get the RTF Format string 
        /// </summary>
        private string RtfFormat
        {
            get {
                return @"{{\rtf1\ansi\ansicpg1252\deff0\deflang1033\deflangfe2052
{{\fonttbl{{\f0\fnil " + Settings.FontName + @";}}}}
{{\colortbl ;{0}}}
\viewkind4\uc1\pard\lang1033\f0\fs" + Math.Round(Settings.FontSize * 2) + @"
{1}}}";
            }
        }

        /// <summary>
        /// Convert the Xml to Rtf with some formats specified in the XMLViewerSettings,
        /// and then set the Rtf property to the value.
        /// </summary>
        public void Process()
        {

            if (string.IsNullOrWhiteSpace(this.Text))
                return;

            _inProcessing = true;
            CurrentParseError = null;

            try
            {
                // maintain the position of the cursor
                var pos = SelectionStart;

                // The Rtf contains 2 parts, header and content. The colortbl is a part of
                // the header, and the {1} will be replaced with the content.

                // Get the XDocument from the Text property.
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(Text);

                StringBuilder xmlRtfContent = new StringBuilder();

                // Get the Rtf of the root element.
                string rootRtfContent = ProcessElement(xmlDoc.DocumentElement, 0);

                xmlRtfContent.Append(rootRtfContent);

                // Construct the completed Rtf, and set the Rtf property to this value.
                Rtf = string.Format(RtfFormat, Settings.ToRtfFormatString(), xmlRtfContent.ToString());

                // put the cursor back in the correct spot 
                SelectionStart = pos;

                // done processing
                NotificationMessage?.Invoke(this,
                    new NotificationEventArgs($@"Input Xml processed: {xmlDoc.DocumentElement.ToString()}",
                    MessageLevel.Information));

                // allow the Text property change to call proces
                _handleKeyPress = true;

            }
            catch (XmlException xmlException)
            {
                NotificationMessage?.Invoke(this,
                    new NotificationEventArgs($@"Please check the input Xml.Error: {xmlException.Message}",
                    MessageLevel.Warning,
                    xmlException));

                CurrentParseError = xmlException;
            }
            catch (Exception ex)
            {
                NotificationMessage?.Invoke(this,
                    new NotificationEventArgs($@"An unknown exception has occurred: {ex.Message}",
                    MessageLevel.Exception,
                    ex));

                CurrentParseError = ex;
            }
            finally
            {
                // done processing!
                _inProcessing = false;
            }
        }

        // Get the Rtf of the xml element.
        private string ProcessElement(XmlNode element, int level)
        {
            string elementRtfFormat = string.Empty;
            StringBuilder childElementsRtfContent = new StringBuilder();
            StringBuilder attributesRtfContent = new StringBuilder();

            // Construct the indent.
            string indent = new string(' ', 2 * level);

            // If the element has child elements or value, then add the element to the
            // Rtf. {{0}} will be replaced with the attributes and {{1}} will be replaced
            // with the child elements or value.
            if (element.ChildNodes.Count > 0 && !(element.ChildNodes.Count == 1 && element.ChildNodes[0] is XmlText))
            {
                elementRtfFormat = string.Format(@"
{0}\cf{1} <\cf{2} {3}{{0}}\cf{1} >\par
{{1}}
{0}\cf{1} </\cf{2} {3}\cf{1} >\par",
                    indent,
                    XMLViewerSettings.TagID,
                    XMLViewerSettings.ElementID,
                    element.Name);

                // Construct the Rtf of child elements.
                foreach (XmlNode childElement in element.ChildNodes)
                {
                    string childElementRtfContent =
                        ProcessElement(childElement, level + 1);
                    childElementsRtfContent.Append(childElementRtfContent);
                }
            }
            else if (element is XmlComment)
            {
                elementRtfFormat = string.Format(@"
{0}\cf{1} <!--
{{1}}
\cf{1} -->\par",
                    indent,
                    XMLViewerSettings.TagID);

                childElementsRtfContent.AppendFormat(@"{0}\cf{1} {2}",
                    new string(' ', 2 * (0 /*level + 1*/)),
                    XMLViewerSettings.CommentID,
                    element.Value.Replace("\r\n", "\n").Replace("\n", "\\line "));
            }

            // If !string.IsNullOrWhiteSpace(element.Value), then construct the Rtf
            // of the value.
            else if (element.ChildNodes.Count == 1 && element.ChildNodes[0] is XmlText)
            {
                elementRtfFormat = string.Format(@"
{0}\cf{1} <\cf{2} {3}{{0}}\cf{1} >
{{1}}
\cf{1} </\cf{2} {3}\cf{1} >\par",
                    indent,
                    XMLViewerSettings.TagID,
                    XMLViewerSettings.ElementID,
                    element.Name);

                childElementsRtfContent.AppendFormat(@"{0}\cf{1} {2}",
                    new string(' ', 2 * (0 /*level + 1*/)),
                    XMLViewerSettings.ValueID,
                    CharacterEncoder.Encode(((XmlText)element.ChildNodes[0]).Value.Trim()));
            }

            // If !string.IsNullOrWhiteSpace(element.Value), then construct the Rtf
            // of the value.
            else if (!string.IsNullOrWhiteSpace(element.Value))
            {
                elementRtfFormat = string.Format(@"
{0}\cf{1} <\cf{2} {3}{{0}}\cf{1} >
{{1}}
\cf{1} </\cf{2} {3}\cf{1} >\par",
                    indent,
                    XMLViewerSettings.TagID,
                    XMLViewerSettings.ElementID,
                    element.Name);

                childElementsRtfContent.AppendFormat(@"{0}\cf{1} {2}",
                    new string(' ', 2 * (0 /*level + 1*/)),
                    XMLViewerSettings.ValueID,
                    CharacterEncoder.Encode(element.Value.Trim()));
            }

            // This element only has attributes. {{0}} will be replaced with the attributes.
            else
            {
                elementRtfFormat =
                    string.Format(@"
{0}\cf{1} <\cf{2} {3}{{0}}\cf{1} />\par",
                    indent,
                    XMLViewerSettings.TagID,
                    XMLViewerSettings.ElementID,
                    element.Name);
            }

            // Construct the Rtf of the attributes.
            if (element.Attributes != null && element.Attributes.Count > 0)
            {
                foreach (XmlAttribute attribute in element.Attributes)
                {
                    string attributeRtfContent = string.Format(
                        @" \cf{0} {3}\cf{1} =\cf0 {5}\cf{2} {4}\cf0 {5}",
                        XMLViewerSettings.AttributeKeyID,
                        XMLViewerSettings.TagID,
                        XMLViewerSettings.AttributeValueID,
                        attribute.Name,
                        CharacterEncoder.Encode(attribute.Value),
                        _settings.QuoteCharacter);
                    attributesRtfContent.Append(attributeRtfContent);
                }
                attributesRtfContent.Append(" ");
            }

            return string.Format(elementRtfFormat, attributesRtfContent,
                childElementsRtfContent);
        }

        #region Control event handlers

        /// <summary>
        /// TODO - make this smarter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XmlViewer_KeyPress(object sender, KeyPressEventArgs e)
        {
            // if an error is being displayed, try to fix with each key press...
            if (CurrentParseError != null)
            {
                _handleKeyPress = true;
            }
            else
            {
                switch (e.KeyChar)
                {
                    // process on Xml specific chars
                    case '>':  // close out element
                    case '\'': // close out attribute
                    case '"':  // close out attribute
                        _handleKeyPress = true;
                        break;
                    default:
                        _handleKeyPress = false;
                        break;
                }
            }
        }

        /// <summary>
        /// Capture some key events, see if we want to apply formatting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XmlViewer_KeyDown(object sender, KeyEventArgs e)
        {
            // handle paste
            _handleKeyPress = false;
            if ((e.KeyCode == Keys.V && e.Control) ||
                (e.KeyCode == Keys.Back))
            {
                _handleKeyPress = true;
            }
            // 'hot key' for format/process
            else if (e.KeyCode == Keys.S && e.Control)
            {
                Process();
            }
        }

        /// <summary>
        /// When the text changes, see if we need to apply formatting and color scheme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XmlViewer_TextChanged(object sender, EventArgs e)
        {
            if ((_handleKeyPress || ReadOnly) && !_inProcessing )
            {
                Process();
            }
            // reset the handle key press so we can process on external Text changed events
            _handleKeyPress = true;
        }
        #endregion
    }
}