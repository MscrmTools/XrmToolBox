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
    public partial class LinkEntityControl : UserControl, IFetchXmlControl
    {
        private readonly XmlNode linkNode;

        public LinkEntityControl(XmlNode node)
        {
            InitializeComponent();

            linkNode = node;
        }

        public XmlNode GetNode()
        {
            if (txtEntityName.Text.Length != 0)
            {
                if (linkNode.Attributes["name"] == null)
                {
                    XmlAttribute page = linkNode.OwnerDocument.CreateAttribute("name");
                    linkNode.Attributes.Append(page);
                }

                linkNode.Attributes["name"].Value = txtEntityName.Text;
            }

            if (txtAttributeFrom.Text.Length != 0)
            {
                if (linkNode.Attributes["from"] == null)
                {
                    XmlAttribute from = linkNode.OwnerDocument.CreateAttribute("from");
                    linkNode.Attributes.Append(from);
                }

                linkNode.Attributes["from"].Value = txtAttributeFrom.Text;
            }

            if (txtAttributeTo.Text.Length != 0)
            {
                if (linkNode.Attributes["to"] == null)
                {
                    XmlAttribute to = linkNode.OwnerDocument.CreateAttribute("to");
                    linkNode.Attributes.Append(to);
                }

                linkNode.Attributes["to"].Value = txtAttributeTo.Text;
            }

            if (txtAlias.Text.Length != 0)
            {
                if (linkNode.Attributes["alias"] == null)
                {
                    XmlAttribute alias = linkNode.OwnerDocument.CreateAttribute("alias");
                    linkNode.Attributes.Append(alias);
                }

                linkNode.Attributes["name"].Value = txtAlias.Text;
            }

            return linkNode;
        }

        private void LinkEntityControlLoad(object sender, EventArgs e)
        {
            if (linkNode != null && linkNode.Attributes != null)
            {
                txtEntityName.Text = linkNode.Attributes["name"] != null ? linkNode.Attributes["name"].Value : "";
                txtAlias.Text = linkNode.Attributes["alias"] != null? linkNode.Attributes["alias"].Value : "";
                txtAttributeFrom.Text = linkNode.Attributes["from"] != null ? linkNode.Attributes["from"].Value : "";
                txtAttributeTo.Text = linkNode.Attributes["to"] != null ? linkNode.Attributes["to"].Value : "";
            }
        }
    }
}
