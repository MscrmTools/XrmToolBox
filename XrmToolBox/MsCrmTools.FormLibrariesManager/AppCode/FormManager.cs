using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace MsCrmTools.FormLibrariesManager.AppCode
{
    internal class FormManager
    {
        public FormManager(IOrganizationService service)
        {
            Service = service;
        }

        public IOrganizationService Service { get; set; }

        public List<Entity> GetAllForms()
        {
            var qe = new QueryByAttribute("systemform");
            qe.ColumnSet = new ColumnSet(new[] { "name", "formxml", "objecttypecode" });
            qe.AddAttributeValue("type", 2);
            qe.AddAttributeValue("iscustomizable", true);
            qe.AddAttributeValue("formactivationstate", 1);

            try
            {
                return Service.RetrieveMultiple(qe).Entities.ToList();
            }
            catch
            {
                qe.Attributes.RemoveAt(qe.Attributes.Count - 1);
                qe.Values.RemoveAt(qe.Values.Count - 1);
                return Service.RetrieveMultiple(qe).Entities.ToList();
            }
        }

        public void AddLibrary(Entity form, string libraryName, bool addFirst)
        {
            // Read the form xml content
            var formXml = form.GetAttributeValue<string>("formxml");
            var formDoc = new XmlDocument();
            formDoc.LoadXml(formXml);

            var formNode = formDoc.SelectSingleNode("form");
            if (formNode == null)
            {
                throw new Exception("Expected node \"formNode\" was not found");
            }

            var formLibrariesNode = formNode.SelectSingleNode("formLibraries");
            if (formLibrariesNode == null)
            {
                formLibrariesNode = formDoc.CreateElement("formLibraries");
                formNode.AppendChild(formLibrariesNode);
            }

            // Search for existing library
            var libraryNode = formLibrariesNode.SelectSingleNode(string.Format("Library[@name='{0}']", libraryName));
            if (libraryNode != null)
            {
                throw new Exception("This library is already included in this form");
            }

            // Create the new library node
            var nameAttribute = formDoc.CreateAttribute("name");
            var libraryUniqueIdAttribute = formDoc.CreateAttribute("libraryUniqueId");
            nameAttribute.Value = libraryName;
            libraryUniqueIdAttribute.Value = Guid.NewGuid().ToString("B");
            libraryNode = formDoc.CreateElement("Library");
            if (libraryNode.Attributes != null)
            {
                libraryNode.Attributes.Append(nameAttribute);
                libraryNode.Attributes.Append(libraryUniqueIdAttribute);
            }

            formLibrariesNode.InsertBefore(libraryNode, formLibrariesNode.FirstChild);

            // Update the form xml content
            form["formxml"] = formDoc.OuterXml;
        }

        public bool RemoveLibrary(Entity form, string libraryName, Form parentForm)
        {
            // Read the form xml content
            var formXml = form.GetAttributeValue<string>("formxml");
            var formDoc = new XmlDocument();
            formDoc.LoadXml(formXml);

           
            var formNode = formDoc.SelectSingleNode("form");
            if (formNode == null)
            {
                throw new Exception("Expected node \"formNode\" was not found");
            }

            // Retrieve events that use the library
            var eventNodes = formNode.SelectNodes(string.Format("events/event/Handlers/Handler[@libraryName='{0}']", libraryName));
            if (eventNodes == null || eventNodes.Count > 0)
            {
                var result =
                    MessageBox.Show(parentForm,
                        string.Format(
                            "The library '{2}' is used by {0} event{1}. If you remove the library, {0} event{1} will be removed too\r\n\r\nDo you want to continue?",
                            eventNodes.Count, eventNodes.Count > 1 ? "s" : "", libraryName), "Question", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    return false;
                }
            }

            var formLibrariesNode = formNode.SelectSingleNode("formLibraries");
            if (formLibrariesNode == null)
            {
                throw new Exception("Expected node \"formLibraries\" was not found");
            }

            // Search for existing library
            var libraryNode = formLibrariesNode.SelectSingleNode(string.Format("Library[@name='{0}']", libraryName));
            if (libraryNode == null)
            {
                throw new Exception("This library is noy included in this form");
            }

            // Remove library
            formLibrariesNode.RemoveChild(libraryNode);

            if (formLibrariesNode.ChildNodes.Count == 0)
            {
                formLibrariesNode.ParentNode.RemoveChild(formLibrariesNode);
            }

            // Remove events that was using the library
            foreach (XmlNode eventNode in eventNodes)
            {
                if (eventNode.ParentNode.ChildNodes.Count == 1)
                {
                    formNode.SelectSingleNode("events").RemoveChild(eventNode.ParentNode.ParentNode);
                }
                else
                {
                    eventNode.ParentNode.RemoveChild(eventNode);
                }
            }

            // Update the form xml content
            form["formxml"] = formDoc.OuterXml;

            return true;
        }

        public void UpdateForm(Entity form)
        {
            Service.Update(form);
        }

        public void PublishForm(string entityName)
        {
            var request = new PublishXmlRequest { ParameterXml = String.Format("<importexportxml><entities><entity>{0}</entity></entities></importexportxml>", entityName) };
            Service.Execute(request);
        }
    }
}
