using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace MsCrmTools.FetchXmlBuilder.UserControls
{
    public partial class FilterControl : UserControl, IFetchXmlControl
    {
        private readonly XmlNode filterNode;

        public FilterControl(XmlNode node)
        {
            InitializeComponent();

            filterNode = node;
        }

        public XmlNode GetNode()
        {
            if (filterNode.Attributes["type"] == null)
            {
                XmlAttribute type = filterNode.OwnerDocument.CreateAttribute("type");
                filterNode.Attributes.Append(type);
            }

            filterNode.Attributes["type"].Value = cbbFilterOperator.SelectedText;

            return filterNode;
        }

        private void FilterControlLoad(object sender, EventArgs e)
        {
            if (filterNode != null && filterNode.Attributes != null)
            {
                XmlAttribute type = filterNode.Attributes["type"];

                cbbFilterOperator.SelectedText = type != null ? type.Value : "and";
            }
        }
    }
}
