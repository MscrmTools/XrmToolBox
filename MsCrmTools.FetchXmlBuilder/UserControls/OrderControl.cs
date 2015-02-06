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
    public partial class OrderControl : UserControl, IFetchXmlControl
    {
        private readonly XmlNode orderNode;

        public OrderControl(XmlNode node)
        {
            InitializeComponent();

            orderNode = node;
        }

        private void OrderControlLoad(object sender, EventArgs e)
        {
            if (orderNode != null && orderNode.Attributes != null)
            {
                txtAttribute.Text = orderNode.Attributes["attribute"] != null ? orderNode.Attributes["attribute"].Value : "";
                cbbOrder.SelectedText = orderNode.Attributes["descending"] != null && orderNode.Attributes["descending"].Value == "true" ? "descending"  : "ascending";
            }
        }

        public XmlNode GetNode()
        {
            if (txtAttribute.Text.Length != 0)
            {
                if (orderNode.Attributes["attribute"] == null)
                {
                    XmlAttribute attribute = orderNode.OwnerDocument.CreateAttribute("attribute");
                    orderNode.Attributes.Append(attribute);
                }

                orderNode.Attributes["attribute"].Value = txtAttribute.Text;
            }

            if (orderNode.Attributes["descending"] == null)
            {
                XmlAttribute descending = orderNode.OwnerDocument.CreateAttribute("descending");
                orderNode.Attributes.Append(descending);
            }

            orderNode.Attributes["descending"].Value = (cbbOrder.SelectedText == "descending").ToString().ToLower();

            return orderNode;
        }
    }
}
