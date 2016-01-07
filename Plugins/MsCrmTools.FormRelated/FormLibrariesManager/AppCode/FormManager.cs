using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using MsCrmTools.FormRelated.FormLibrariesManager.POCO;

namespace MsCrmTools.FormLibrariesManager.AppCode
{
    internal class FormManager
    {
        public FormManager(IOrganizationService service)
        {
            Service = service;
        }

        public IOrganizationService Service { get; set; }

        public void AddLibrary(Entity form, string libraryName, bool addFirst)
        {
            // Read the form xml content
            var formLibrariesNode = GetFormLibraries(form);

            // Search for existing library
            var libraryNode = formLibrariesNode.SelectSingleNode(string.Format("Library[@name='{0}']", libraryName));
            if (libraryNode != null)
            {
                throw new Exception("This library is already included in this form");
            }

            // Create the new library node
            libraryNode = formLibrariesNode.OwnerDocument.CreateElement("Library");
            AddAttribute(libraryNode, "name", libraryName);
            AddAttribute(libraryNode, "libraryUniqueId", Guid.NewGuid().ToString("B"));

            if (formLibrariesNode.ChildNodes.Count > 0 && addFirst)
            {
                formLibrariesNode.InsertBefore(libraryNode, formLibrariesNode.FirstChild);
            }
            else
            {
                formLibrariesNode.AppendChild(libraryNode);
            }

            // Update the form xml content
            form["formxml"] = formLibrariesNode.OwnerDocument.OuterXml;
        }

        public static XmlNode GetFormLibraries(Entity form)
        {
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
            return formLibrariesNode;
        }

        public List<Entity> GetAllForms(ConnectionDetail detail)
        {
            var qe = new QueryExpression("systemform")
            {
                ColumnSet = new ColumnSet(new[] { "name", "formxml", "objecttypecode" }),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("type", ConditionOperator.In, new[] {2,7}),
                        new ConditionExpression("iscustomizable", ConditionOperator.Equal, true),
                    }
                }
            };

            if (detail.OrganizationMajorVersion > 5)
            {
                qe.Criteria.Conditions.Add(new ConditionExpression("formactivationstate", ConditionOperator.Equal, 1));
            }

            try
            {
                return Service.RetrieveMultiple(qe).Entities.ToList();
            }
            catch
            {
                qe.Criteria.Conditions.RemoveAt(qe.Criteria.Conditions.Count - 1);
                return Service.RetrieveMultiple(qe).Entities.ToList();
            }
        }

        public void PublishEntity(string entityName)
        {
            var request = new PublishXmlRequest { ParameterXml = String.Format("<importexportxml><entities><entity>{0}</entity></entities></importexportxml>", entityName) };
            Service.Execute(request);
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
            var handlerEventNodes = formNode.SelectNodes(string.Format("events/event/Handlers/Handler[@libraryName='{0}']", libraryName));
            if (handlerEventNodes != null && handlerEventNodes.Count > 0)
            {
                var result = DialogResult.None;
                parentForm.Invoke((MethodInvoker)delegate {
                    result = MessageBox.Show(parentForm, string.Format(
                        "The library '{2}' is used by {0} event{1}. If you remove the library, {0} event{1} will be removed too\r\n\r\nDo you want to continue?",
                        handlerEventNodes.Count, handlerEventNodes.Count > 1 ? "s" : "", libraryName)
                        , "Question", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                });

                if (result == DialogResult.No)
                {
                    return false;
                }

                // Remove events that were using the library
                foreach (XmlNode handlerNode in handlerEventNodes)
                {
                    // Events> <Event> <Handlers><Handler>
                    if (handlerNode.ParentNode.ChildNodes.Count == 1)
                    {
                        var events = formNode.SelectSingleNode("events");
                        if (events.ChildNodes.Count == 1)
                        {
                            // Remove Events since it only has one Event and the Event only has on handler, and the Handler is getting removed
                            events.ParentNode.RemoveChild(events);
                        }
                        else
                        {
                            // Remove Event since it only has one handler and it is being removed, but there are other events
                            events.RemoveChild(handlerNode.ParentNode.ParentNode);
                        }

                    }
                    else
                    {
                        // Remove only handler since there are other handlers 
                        handlerNode.ParentNode.RemoveChild(handlerNode);
                    }
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
                throw new Exception("This library is not included in this form");
            }

            // Remove library
            formLibrariesNode.RemoveChild(libraryNode);

            if (formLibrariesNode.ChildNodes.Count == 0)
            {
                formLibrariesNode.ParentNode.RemoveChild(formLibrariesNode);
            }

            // Update the form xml content
            form["formxml"] = formDoc.OuterXml;

            return true;
        }

        public void UpdateForm(Entity form)
        {
            Service.Update(form);
        }

        public void UpdateFormEventHandlers(Entity form, string eventName, List<FormEvent> formEvents)
        {
            var formXml = form.GetAttributeValue<string>("formxml");
            var formDoc = new XmlDocument();
            formDoc.LoadXml(formXml);

            var formNode = formDoc.SelectSingleNode("form");
            if (formNode == null)
            {
                throw new Exception("Expected node \"formNode\" was not found");
            }

            // Remove all Nodes for Event
            RemoveAllNodesForEvent(formNode, eventName);

            // Add All Nodes
            AddFormEvents(formNode, eventName, formEvents);

            form["formxml"] = formDoc.OuterXml;
            UpdateForm(form);
        }

        private static void AddFormEvents(XmlNode formNode, string eventName, List<FormEvent> formEvents)
        {
            if (formEvents.Count == 0)
            {
                return;
            }
            var eventsNode = formNode.SelectSingleNode("events");
            if (eventsNode == null)
            {
                eventsNode = formNode.OwnerDocument.CreateElement("events");
                formNode.AppendChild(eventsNode);
            }
            var eventNode = eventsNode.OwnerDocument.CreateElement("event");
            AddAttribute(eventNode, "name", eventName);
            AddAttribute(eventNode, "application", "false");
            AddAttribute(eventNode, "active", "false");
            eventsNode.AppendChild(eventNode);
            var handlers = eventNode.OwnerDocument.CreateElement("Handlers");
            eventNode.AppendChild(handlers);
            foreach (var formEvent in formEvents)
            {
                var handler = eventNode.OwnerDocument.CreateElement("Handler");
                AddAttribute(handler, "functionName", formEvent.Function);
                AddAttribute(handler, "libraryName", formEvent.Script);
                AddAttribute(handler, "handlerUniqueId", Guid.NewGuid().ToString("B"));
                AddAttribute(handler, "enabled", formEvent.Enabled.ToString().ToLower());
                AddAttribute(handler, "parameters", formEvent.Parameters);
                AddAttribute(handler, "passExecutionContext", formEvent.PassExecutionContext.ToString().ToLower());
                handlers.AppendChild(handler);
            }
        }

        private static void AddAttribute(XmlNode eventNode, string name, string value)
        {
            var attribute = eventNode.OwnerDocument.CreateAttribute(name);
            attribute.Value = value;
            eventNode.Attributes.Append(attribute);
        }

        private static void RemoveAllNodesForEvent(XmlNode formNode, string eventName)
        {
            var eventNode = formNode.SelectSingleNode(string.Format("events/event[@name='{0}']", eventName));
            if (eventNode != null)
            {
                var eventsNode = eventNode.ParentNode;
                if (eventsNode.ChildNodes.Count == 1)
                {
                    // Removing event nod, and no more events, remove Events
                    eventsNode.ParentNode.RemoveChild(eventsNode);
                }
                else
                {
                    eventsNode.RemoveChild(eventNode);
                }
            }
        }
    }
}