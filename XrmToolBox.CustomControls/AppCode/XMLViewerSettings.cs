/****************************** Module Header ******************************\
* Module Name:  XMLViewerSettings.cs
* Project:	    CSRichTextBoxSyntaxHighlighting
* Copyright (c) Microsoft Corporation.
*
* This XMLViewerSettings class defines the colors used in the XmlViewer, and some
* constants that specify the color order in the RTF.
*
* This source is subject to the Microsoft Public License.
* See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
* All other rights reserved.
*
* THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
* EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
* WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace XrmToolBox.CustomControls
{
    /// <summary>
    /// Applys formatting and color coding to an Xml Document using RTF 
    /// </summary>
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class XMLViewerSettings : INotifyPropertyChanged
    {

        public const int ElementID = 1;
        public const int ValueID = 2;
        public const int AttributeKeyID = 3;
        public const int AttributeValueID = 4;
        public const int TagID = 5;
        public const int CommentID = 6;
        private Color _element = Color.DarkGreen;
        private Color _value = Color.Black;
        private Color _attributeKey = Color.Blue;
        private Color _attributeValue = Color.DarkRed;
        private Color _comment = Color.Gray;
        private Color _tag = Color.ForestGreen;
        private char _quoteCharacter = '"';
        private string _fontName = "Consolas";
        private float _fontSize = 9;

        /// <summary>
        /// Allow us to notify when property values change
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The color of an Xml element name.
        /// </summary>
        [Description("The color of an Xml element name.")]
        public Color Element
        {
            get => _element;
            set {
                _element = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Element"));
            }
        }
        /// <summary>
        /// The color of an Xml element value.
        /// </summary>
        [Description("The color of an Xml element value.")]
        public Color Value
        {
            get => _value;
            set {
                _value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
            }
        }
        /// <summary>
        /// The color of an Attribute Key in Xml element.
        /// </summary>
        [Description("The color of an Attribute Key in Xml element.")]
        public Color AttributeKey
        {
            get => _attributeKey;
            set {
                _attributeKey = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AttributeKey"));
            }
        }
        /// <summary>
        /// The color of an Attribute Value in Xml element.
        /// </summary>
        [Description("The color of an Attribute Value in Xml element.")]
        public Color AttributeValue { get => _attributeValue;
            set {
                _attributeValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AttributeValue"));
            }
        }

        /// <summary>
        /// The color of comment content
        /// </summary>
        [Description("The color of comment content.")]
        public Color Comment { get => _comment;
            set {
                _comment = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Comment"));
            }
        }

        /// <summary>
        /// The color of the tags and operators
        /// </summary>
        [Description("The color of the tags and operators like \"<,/> and = \".")]
        public Color Tag { get => _tag;
            set {
                _tag = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Tag"));
            }
        }

        /// <summary>
        /// Defines quote character in xml. Default double quotes "
        /// </summary>
        [Description("Defines quote character in xml. Default double quotes '\"'.")]
        public char QuoteCharacter { get => _quoteCharacter;
            set {
                _quoteCharacter = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("QuoteCharacter"));
            }
        }

        /// <summary>
        /// Font name for the Rich Text editor.
        /// </summary>
        [Description("Font name for the Rich Text editor.")]
        public string FontName { get => _fontName;
            set {
                _fontName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AttributeKey"));
            }
        }

        /// <summary>
        /// Font size for the Rich Text editor
        /// </summary>
        [Description("Font size for the Rich Text editor.")]
        public float FontSize { get => _fontSize;
            set {
                _fontSize = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FontSize"));
            }
        }

        /// <summary>
        /// Convert the settings to Rtf color definitions.
        /// </summary>
        public string ToRtfFormatString()
        {
            // The Rtf color definition format.
            string format = @"\red{0}\green{1}\blue{2};";

            StringBuilder rtfFormatString = new StringBuilder();

            rtfFormatString.AppendFormat(format, Element.R, Element.G, Element.B);
            rtfFormatString.AppendFormat(format, Value.R, Value.G, Value.B);
            rtfFormatString.AppendFormat(format, AttributeKey.R, AttributeKey.G, AttributeKey.B);
            rtfFormatString.AppendFormat(format, AttributeValue.R, AttributeValue.G, AttributeValue.B);
            rtfFormatString.AppendFormat(format, Tag.R, Tag.G, Tag.B);
            rtfFormatString.AppendFormat(format, Comment.R, Comment.G, Comment.B);

            return rtfFormatString.ToString();
        }
        /// <summary>
        /// Helper method to copy the settings for reset
        /// </summary>
        /// <returns></returns>
        public XMLViewerSettings Copy()
        {
            return (XMLViewerSettings)MemberwiseClone();
        }
    }
}