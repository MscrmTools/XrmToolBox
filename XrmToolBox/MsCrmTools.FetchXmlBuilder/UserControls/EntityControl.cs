using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Xrm.Sdk.Metadata;

namespace MsCrmTools.FetchXmlBuilder.UserControls
{
    public partial class EntityControl : UserControl, IFetchXmlControl
    {
        private readonly XmlNode entityNode;

        private readonly Dictionary<string, EntityMetadata> cache;

        public EntityControl(XmlNode node, Dictionary<string, EntityMetadata> cache)
        {
            InitializeComponent();

            entityNode = node;
            this.cache = cache;
        }

        public XmlNode GetNode()
        {
            if (cbbEntities.SelectedItem != null)
            {
                if (entityNode.Attributes["name"] == null)
                {
                    XmlAttribute page = entityNode.OwnerDocument.CreateAttribute("name");
                    entityNode.Attributes.Append(page);
                }

                entityNode.Attributes["name"].Value = cbbEntities.SelectedItem.ToString().StartsWith("N/A") ?
                    cbbEntities.SelectedItem.ToString().Split('(')[1].Split(')')[0]
                    : cbbEntities.SelectedItem.ToString();
            }

            return entityNode;
        }

        private void EntityControlLoad(object sender, EventArgs e)
        {
            // Loads entity picklist
            foreach (var emdKey in cache.Keys)
            {
                var displayName = cache[emdKey].DisplayName != null && cache[emdKey].DisplayName.UserLocalizedLabel != null
                                      ? cache[emdKey].DisplayName.UserLocalizedLabel.Label
                                      : string.Format("N/A ({0})", emdKey);

                cbbEntities.Items.Add(displayName);
            }


            if (entityNode != null && entityNode.Attributes != null)
            {
                var currentEntityLogicalName = entityNode.Attributes["name"] != null
                                                   ? entityNode.Attributes["name"].Value
                                                   : "";

                if (currentEntityLogicalName.Length > 0)
                {
                    var emd = cache.FirstOrDefault(x => x.Key == currentEntityLogicalName).Value;
                    if (emd != null)
                    {
                        var displayName = emd.DisplayName != null
                            ? emd.DisplayName.UserLocalizedLabel.Label
                            : string.Format("N/A ({0})", currentEntityLogicalName);

                        txtEntityName.Text = currentEntityLogicalName;

                        cbbEntities.SelectedItem = displayName;
                    }
                }
            }
        }
    }
}
