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
    public partial class FetchControl : UserControl, IFetchXmlControl
    {
        private readonly XmlNode fetchNode;

        public FetchControl(XmlNode node)
        {
            InitializeComponent();

            fetchNode = node;
        }

        private void FetchControlLoad(object sender, EventArgs e)
        {
            if (fetchNode != null && fetchNode.Attributes != null)
            {
                XmlAttribute page = fetchNode.Attributes["page"];
                XmlAttribute count = fetchNode.Attributes["count"];
                XmlAttribute top = fetchNode.Attributes["top"];
                XmlAttribute aggregate = fetchNode.Attributes["aggregate"];
                XmlAttribute distinct = fetchNode.Attributes["distinct"];

                nudPage.Value = page != null ? int.Parse(page.Value) : 0;
                nudCount.Value = count != null ? int.Parse(count.Value) : 0;
                nudTop.Value = top != null ? int.Parse(top.Value) : 0;
                chkAggregate.Checked = aggregate != null && aggregate.Value == "1";
                chkDistinct.Checked = aggregate != null && distinct.Value == "1";
            }
        }

        public XmlNode GetNode()
        {
            if (nudPage.Value != 0)
            {
                if (fetchNode.Attributes["page"] == null)
                {
                    XmlAttribute page = fetchNode.OwnerDocument.CreateAttribute("page");
                    fetchNode.Attributes.Append(page);
                }

                fetchNode.Attributes["page"].Value = nudPage.Value.ToString();
            }

            if (nudCount.Value != 0)
            {
                if (fetchNode.Attributes["count"] == null)
                {
                    XmlAttribute count = fetchNode.OwnerDocument.CreateAttribute("count");
                    fetchNode.Attributes.Append(count);
                }

                fetchNode.Attributes["count"].Value = nudCount.Value.ToString();
            }

            if (nudTop.Value != 0)
            {
                if (fetchNode.Attributes["top"] == null)
                {
                    XmlAttribute top = fetchNode.OwnerDocument.CreateAttribute("top");
                    fetchNode.Attributes.Append(top);
                }

                fetchNode.Attributes["top"].Value = nudTop.Value.ToString();
            }

            if (fetchNode.Attributes["aggregate"] == null)
            {
                XmlAttribute aggregate = fetchNode.OwnerDocument.CreateAttribute("aggregate");
                fetchNode.Attributes.Append(aggregate);
            }
            fetchNode.Attributes["aggregate"].Value = chkAggregate.Checked.ToString().ToLower();

            if (fetchNode.Attributes["distinct"] == null)
            {
                XmlAttribute distinct = fetchNode.OwnerDocument.CreateAttribute("distinct");
                fetchNode.Attributes.Append(distinct);
            }
            fetchNode.Attributes["distinct"].Value = chkDistinct.Checked.ToString().ToLower();

            return fetchNode;
        }
    }
}
