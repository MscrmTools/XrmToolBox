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
    public partial class ConditionControl : UserControl, IFetchXmlControl
    {
        private readonly XmlNode conditionNode;

        public ConditionControl(XmlNode node)
        {
            InitializeComponent();

            conditionNode = node;
        }

        private void ConditionControlLoad(object sender, EventArgs e)
        {
            if (conditionNode != null && conditionNode.Attributes != null)
            {
                txtAttribute.Text = conditionNode.Attributes["attribute"] != null
                                        ? conditionNode.Attributes["attribute"].Value
                                        : "";
                txtOperator.Text = conditionNode.Attributes["operator"] != null
                                       ? conditionNode.Attributes["operator"].Value
                                       : "";
                txtValue.Text = conditionNode.Attributes["value"] != null ? conditionNode.Attributes["value"].Value : "";
            }
        }

        public XmlNode GetNode()
        {
            if (txtAttribute.Text.Length != 0)
            {
                if (conditionNode.Attributes["attribute"] == null)
                {
                    XmlAttribute attribute = conditionNode.OwnerDocument.CreateAttribute("attribute");
                    conditionNode.Attributes.Append(attribute);
                }

                conditionNode.Attributes["attribute"].Value = txtAttribute.Text;
            }


            if (txtOperator.Text.Length != 0)
            {
                if (conditionNode.Attributes["operator"] == null)
                {
                    XmlAttribute operatorAttr = conditionNode.OwnerDocument.CreateAttribute("operator");
                    conditionNode.Attributes.Append(operatorAttr);
                }

                conditionNode.Attributes["operator"].Value = txtOperator.Text;
            }

            if (txtValue.Text.Length != 0)
            {
                if (conditionNode.Attributes["value"] == null)
                {
                    XmlAttribute value = conditionNode.OwnerDocument.CreateAttribute("value");
                    conditionNode.Attributes.Append(value);
                }

                conditionNode.Attributes["value"].Value = txtValue.Text;
            }

            return conditionNode;
        }
    }
}