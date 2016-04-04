using Microsoft.Xrm.Sdk;
using System;
using System.Globalization;
using System.Xml;

namespace MsCrmTools.FormAttributeManager.AppCode
{
    public class FormInfo
    {
        private readonly Entity form;

        private readonly XmlDocument formXml;

        public FormInfo(Entity form)
        {
            if (form.LogicalName != "systemform")
            {
                throw new ArgumentException("Only systemform entity can be used in FormInfo", "form");
            }

            this.form = form;

            formXml = new XmlDocument();
            formXml.LoadXml(form.GetAttributeValue<string>("formxml"));
        }

        public Entity Form
        {
            get
            {
                form["formxml"] = formXml.OuterXml;
                return form;
            }
        }

        public bool HasChanged { get; private set; }

        public void AddAttribute(string attributeLogicalName, string classId, Label displayNames, string sourceAttributeLogicalName)
        {
            var referenceCellNode = GetCellNodeByAttributeLogicalName(sourceAttributeLogicalName);

            // Check if there is an empty cell after the reference cell
            if (referenceCellNode.NextSibling != null
                && referenceCellNode.NextSibling.SelectSingleNode("control") == null)
            {
                AddAttributeNode(referenceCellNode.NextSibling, attributeLogicalName, classId, displayNames);
            }
            else
            {
                // If not, we add a new row to store the new cell
                int cellCount = referenceCellNode.ParentNode.ChildNodes.Count;
                var currentRowNode = referenceCellNode.ParentNode;
                var rowsNode = currentRowNode.ParentNode;

                var newRowNode = rowsNode.OwnerDocument.CreateElement("row");

                for (int i = 0; i < cellCount; i++)
                {
                    var newCellNode = rowsNode.OwnerDocument.CreateElement("cell");

                    var idAttr = rowsNode.OwnerDocument.CreateAttribute("id");
                    idAttr.Value = Guid.NewGuid().ToString("B");
                    newCellNode.Attributes.Append(idAttr);

                    var labelsNode = rowsNode.OwnerDocument.CreateElement("labels");
                    newCellNode.AppendChild(labelsNode);

                    newRowNode.AppendChild(newCellNode);

                    if (i == 0)
                    {
                        AddAttributeNode(newCellNode, attributeLogicalName, classId, displayNames);
                    }
                }

                rowsNode.InsertAfter(newRowNode, currentRowNode);
            }
        }

        public void ChangeLabel(Guid cellId, int lcid, string text)
        {
            var cellNode = GetCellNode(cellId);
            var labelNode = cellNode.SelectSingleNode(string.Format("Labels/Label[@languagecode='{0}']", lcid));
            if (labelNode == null)
            {
                throw new Exception("Unable to find label with language code " + lcid);
            }

            if (labelNode.Attributes == null)
            {
                throw new NullReferenceException();
            }

            labelNode.Attributes["description"].Value = text;
        }

        public void ChangeLabel(string attributeLogicalName, int lcid, string text)
        {
            var cellNode = GetCellNodeByAttributeLogicalName(attributeLogicalName);
            var labelNode = cellNode.SelectSingleNode(string.Format("Labels/Label[@languagecode='{0}']", lcid));
            if (labelNode == null)
            {
                throw new Exception("Unable to find label with language code " + lcid);
            }

            if (labelNode.Attributes == null)
            {
                throw new NullReferenceException();
            }

            labelNode.Attributes["description"].Value = text;
        }

        public bool HasAttribute(string attributeLogicalName)
        {
            try
            {
                GetCellNodeByAttributeLogicalName(attributeLogicalName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void RemoveAttribute(string attributeLogicalName)
        {
            var cellNode = GetCellNodeByAttributeLogicalName(attributeLogicalName);

            while (cellNode != null)
            {
                var controlNode = GetCellControlNode(cellNode);
                cellNode.RemoveChild(controlNode);

                var labelNodes = cellNode.SelectNodes("Labels/Label");

                if (labelNodes == null)
                {
                    throw new NullReferenceException();
                }

                foreach (XmlNode labelNode in labelNodes)
                {
                    if (labelNode.Attributes == null)
                    {
                        throw new NullReferenceException();
                    }

                    labelNode.Attributes["description"].Value = string.Empty;
                }

                // Check if there is still controls in the same row, if not remove the row
                if (cellNode.ParentNode.SelectNodes("cell/control").Count == 0)
                {
                    cellNode.ParentNode.ParentNode.RemoveChild(cellNode.ParentNode);
                }

                try
                {
                    cellNode = GetCellNodeByAttributeLogicalName(attributeLogicalName);
                }
                catch
                {
                    cellNode = null;
                }
            }
        }

        public void SetAttributeLabelDisplayMode(Guid cellId, bool isLabelDisplayed)
        {
            var attribute = GetAttribute(GetCellNode(cellId), "showlabel");
            attribute.Value = isLabelDisplayed ? "true" : "false";

            HasChanged = true;
        }

        public void SetAttributeLabelDisplayMode(string attributeLogicalName, bool isLabelDisplayed)
        {
            var attribute = GetAttribute(GetCellNodeByAttributeLogicalName(attributeLogicalName), "showlabel");
            attribute.Value = isLabelDisplayed ? "true" : "false";

            HasChanged = true;
        }

        public void SetAttributeLockMode(Guid cellId, bool isLocked)
        {
            var attribute = GetAttribute(GetCellNode(cellId), "locklevel");
            attribute.Value = isLocked ? "1" : "0";

            HasChanged = true;
        }

        public void SetAttributeLockMode(string attributeLogicalName, bool isLocked)
        {
            var attribute = GetAttribute(GetCellNodeByAttributeLogicalName(attributeLogicalName), "locklevel");
            attribute.Value = isLocked ? "1" : "0";

            HasChanged = true;
        }

        public void SetAttributeReadOnlyMode(Guid cellId, bool isReadOnly)
        {
            var attribute = GetAttribute(GetCellControlNode(GetCellNode(cellId)), "disabled");
            attribute.Value = isReadOnly ? "true" : "false";

            HasChanged = true;
        }

        public void SetAttributeReadOnlyMode(string attributeLogicalName, bool isReadOnly)
        {
            var attribute = GetAttribute(GetCellControlNode(GetCellNodeByAttributeLogicalName(attributeLogicalName)), "disabled");
            attribute.Value = isReadOnly ? "true" : "false";

            HasChanged = true;
        }

        public void SetAttributeVisibilityMode(Guid cellId, bool isVisible)
        {
            var attribute = GetAttribute(GetCellNode(cellId), "visible");
            attribute.Value = isVisible ? "true" : "false";

            HasChanged = true;
        }

        public void SetAttributeVisibilityMode(string attributeLogicalName, bool isVisible)
        {
            var attribute = GetAttribute(GetCellNodeByAttributeLogicalName(attributeLogicalName), "visible");
            attribute.Value = isVisible ? "true" : "false";

            HasChanged = true;
        }

        public override string ToString()
        {
            return form.GetAttributeValue<string>("name");
        }

        public void Update(IOrganizationService service)
        {
            form["formxml"] = formXml.OuterXml;

            service.Update(form);
        }

        private void AddAttributeNode(XmlNode referenceNode, string attributeLogicalName, string classId, Label displayNames)
        {
            // Add labels information
            referenceNode.SelectSingleNode("labels").RemoveAll();

            foreach (LocalizedLabel l in displayNames.LocalizedLabels)
            {
                XmlNode labelNode = referenceNode.OwnerDocument.CreateElement("label");
                XmlAttribute languageCodeAttr = referenceNode.OwnerDocument.CreateAttribute("languagecode");
                languageCodeAttr.Value = l.LanguageCode.ToString(CultureInfo.InvariantCulture);
                XmlAttribute descriptionAttr = referenceNode.OwnerDocument.CreateAttribute("description");
                descriptionAttr.Value = l.Label;
                labelNode.Attributes.Append(languageCodeAttr);
                labelNode.Attributes.Append(descriptionAttr);

                referenceNode.SelectSingleNode("labels").AppendChild(labelNode);
            }

            // Add control information
            XmlNode newControlNode = referenceNode.OwnerDocument.CreateElement("control");
            XmlAttribute idAttr = referenceNode.OwnerDocument.CreateAttribute("id");
            idAttr.Value = attributeLogicalName;
            XmlAttribute classIdAttr = referenceNode.OwnerDocument.CreateAttribute("classid");
            classIdAttr.Value = classId;
            XmlAttribute dataFieldNameAttr = referenceNode.OwnerDocument.CreateAttribute("datafieldname");
            dataFieldNameAttr.Value = attributeLogicalName;
            newControlNode.Attributes.Append(idAttr);
            newControlNode.Attributes.Append(classIdAttr);
            newControlNode.Attributes.Append(dataFieldNameAttr);

            referenceNode.AppendChild(newControlNode);
        }

        private XmlAttribute GetAttribute(XmlNode node, string xmlAttributeName)
        {
            if (node.Attributes == null)
            {
                throw new Exception("Attribute collection expected for the current node!");
            }

            var attribute = node.Attributes[xmlAttributeName];
            if (attribute == null)
            {
                attribute = formXml.CreateAttribute(xmlAttributeName);
                node.Attributes.Append(attribute);
            }

            return attribute;
        }

        private XmlNode GetCellControlNode(XmlNode cellNode)
        {
            return cellNode.SelectSingleNode("control");
        }

        private XmlNode GetCellNode(Guid cellId)
        {
            var node = formXml.SelectSingleNode(string.Format("//cell[@id='{0}']", cellId.ToString("B").ToLower()));
            if (node == null)
            {
                throw new Exception(string.Format("Cannot find cell node with id '{0}' in form '{1}",
                    cellId,
                    form.GetAttributeValue<string>("name")));
            }

            return node;
        }

        private XmlNode GetCellNodeByAttributeLogicalName(string attributeLogicalName)
        {
            var node = formXml.SelectSingleNode(string.Format("//cell[control/@datafieldname='{0}']", attributeLogicalName));
            if (node == null)
            {
                throw new Exception(string.Format("Cannot find cell node for attribute '{0}' in form '{1}",
                    attributeLogicalName,
                    form.GetAttributeValue<string>("name")));
            }

            return node;
        }
    }
}