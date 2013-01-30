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
    public partial class EntityControl : UserControl, IFetchXmlControl
    {
        private readonly XmlNode entityNode;

        public EntityControl(XmlNode node)
        {
            InitializeComponent();

            entityNode = node;
        }

        public XmlNode GetNode()
        {
            if (txtEntityName.Text.Length != 0)
            {
                if (entityNode.Attributes["name"] == null)
                {
                    XmlAttribute page = entityNode.OwnerDocument.CreateAttribute("name");
                    entityNode.Attributes.Append(page);
                }

                entityNode.Attributes["name"].Value = txtEntityName.Text;
            }

            return entityNode;
        }

        private void EntityControlLoad(object sender, EventArgs e)
        {
            if (entityNode != null && entityNode.Attributes != null)
            {
                txtEntityName.Text = entityNode.Attributes["name"] != null ? entityNode.Attributes["name"].Value : "";
            }
        }
    }
}
