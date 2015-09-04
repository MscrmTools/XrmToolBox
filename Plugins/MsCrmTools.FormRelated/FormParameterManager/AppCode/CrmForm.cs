using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml;

namespace MsCrmTools.FormParameterManager.AppCode
{
    public class CrmForm
    {
        #region Variables

        private const string ParametersListXPath = "form/formparameters";
        private static EntityMetadata[] entitiesMetadatas;
        private readonly Entity form;

        #endregion Variables

        #region Constructor

        public CrmForm(Entity form)
        {
            var emd = entitiesMetadatas.ToList().First(e => e.LogicalName == form.GetAttributeValue<string>("objecttypecode"));

            this.form = form;

            Name = form.GetAttributeValue<string>("name");
            EntityLogicalName = form.GetAttributeValue<string>("objecttypecode");
            EntityDisplayName = emd.DisplayName != null ? emd.DisplayName.UserLocalizedLabel.Label : "N/A";
            Id = form.Id;

            ListParameters();
        }

        #endregion Constructor

        #region Properties

        public string EntityDisplayName { get; private set; }
        public string EntityLogicalName { get; private set; }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public List<FormParameter> Parameters { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Get forms
        /// </summary>
        /// <param name="service">Microsoft Dynamics CRM organization service</param>
        /// <param name="worker"></param>
        /// <returns>List of forms</returns>
        public static IEnumerable<CrmForm> GetForms(IOrganizationService service, BackgroundWorker worker = null)
        {
            if (worker != null && worker.WorkerReportsProgress)
            {
                worker.ReportProgress(0, "Retrieving entities metadata...");
            }

            var request = new RetrieveAllEntitiesRequest { EntityFilters = EntityFilters.Entity };
            var response = (RetrieveAllEntitiesResponse)service.Execute(request);
            entitiesMetadatas = response.EntityMetadata;

            if (worker != null && worker.WorkerReportsProgress)
            {
                worker.ReportProgress(0, "Retrieving forms...");
            }

            var qe = new QueryExpression("systemform")
            {
                ColumnSet = new ColumnSet(new[] { "name", "formxml", "objecttypecode" }),
                Criteria = new FilterExpression
                {
                    Filters =
                    {
                        new FilterExpression
                        {
                            Conditions =
                            {
                                new ConditionExpression("type", ConditionOperator.Equal, 2)
                            }
                        },
                        new FilterExpression
                        {
                            FilterOperator = LogicalOperator.Or,
                            Conditions =
                            {
                                new ConditionExpression("iscustomizable", ConditionOperator.Equal, true),
                                new ConditionExpression("ismanaged", ConditionOperator.Equal, false)
                            }
                        }
                    }
                }
            };

            return service.RetrieveMultiple(qe).Entities.Select(e => new CrmForm(e));
        }

        public static void PublishForms(IOrganizationService service, IEnumerable<string> entities)
        {
            var entitiesToPublish = "";
            entitiesToPublish = entities.Aggregate(entitiesToPublish,
                (current, e) => current + ("<entity>" + e + "</entity>"));
            var request = new PublishXmlRequest
            {
                ParameterXml =
                    string.Format("<importexportxml><entities>{0}</entities></importexportxml>",
                        entitiesToPublish)
            };

            service.Execute(request);
        }

        public void AddParameter(FormParameter parameter)
        {
            var formXml = form.GetAttributeValue<string>("formxml");
            var doc = new XmlDocument();
            doc.LoadXml(formXml);

            var parametersListNode = doc.SelectSingleNode(ParametersListXPath);
            if (parametersListNode == null)
            {
                parametersListNode = doc.CreateElement("formparameters");
                doc.SelectSingleNode("form").AppendChild(parametersListNode);
            }

            var parameterNode = parametersListNode.SelectSingleNode(string.Format("querystringparameter[@name='{0}']", parameter.Name));
            if (parameterNode != null)
            {
                throw new XmlException(string.Format("A parameter with the name '{0}' already exists in this form",
                    parameter.Name));
            }

            // Create Xml Node
            parameterNode = doc.CreateElement("querystringparameter");
            var nameAttribute = doc.CreateAttribute("name");
            nameAttribute.Value = parameter.Name;
            var typeAttribute = doc.CreateAttribute("type");
            typeAttribute.Value = parameter.Type.ToString();
            parameterNode.Attributes.Append(nameAttribute);
            parameterNode.Attributes.Append(typeAttribute);

            parametersListNode.AppendChild(parameterNode);

            form["formxml"] = doc.OuterXml;

            var parameterToAdd = (FormParameter)parameter.Clone();
            parameterToAdd.ParentForm = this;
            Parameters.Add(parameterToAdd);
        }

        public void PublishForm(IOrganizationService service)
        {
            var request = new PublishXmlRequest
            {
                ParameterXml =
                    string.Format("<importexportxml><entities><entity>{0}</entity></entities></importexportxml>",
                        EntityLogicalName)
            };

            service.Execute(request);
        }

        public void RemoveParameter(FormParameter parameter)
        {
            var formXml = form.GetAttributeValue<string>("formxml");
            var doc = new XmlDocument();
            doc.LoadXml(formXml);

            var parametersListNode = doc.SelectSingleNode(ParametersListXPath);
            if (parametersListNode == null)
            {
                throw new XmlException("Unable to find node 'formparameters' in the form");
            }

            var parameterNode = parametersListNode.SelectSingleNode(string.Format("querystringparameter[@name='{0}']", parameter.Name));
            if (parameterNode == null)
            {
                throw new XmlException(string.Format("Unable to find parameter with the name '{0}'",
                    parameter.Name));
            }

            parametersListNode.RemoveChild(parameterNode);

            form["formxml"] = doc.OuterXml;

            Parameters.Remove(parameter);
        }

        public void UpdateForm(IOrganizationService service)
        {
            service.Update(form);
        }

        private void ListParameters()
        {
            Parameters = new List<FormParameter>();

            var formXml = form.GetAttributeValue<string>("formxml");
            var doc = new XmlDocument();
            doc.LoadXml(formXml);

            var parametersListNode = doc.SelectSingleNode(ParametersListXPath);
            if (parametersListNode == null)
            {
                return;
            }

            foreach (XmlNode parameterNode in parametersListNode.ChildNodes)
            {
                if (parameterNode.NodeType == XmlNodeType.Comment)
                {
                    continue;
                }

                Parameters.Add(new FormParameter
                {
                    Name = parameterNode.Attributes["name"].Value,
                    Type = (FormParameterType)Enum.Parse(typeof(FormParameterType), parameterNode.Attributes["type"].Value),
                    ParentForm = this
                });
            }
        }

        #endregion Methods
    }
}