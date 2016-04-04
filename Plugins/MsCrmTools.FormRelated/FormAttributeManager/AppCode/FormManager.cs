using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MsCrmTools.FormAttributeManager.AppCode
{
    internal class FormManager
    {
        public FormManager(IOrganizationService service)
        {
            Service = service;
        }

        public IOrganizationService Service { get; set; }

        public List<Entity> GetAllFormsByTypeCode(int objectTypeCode, ConnectionDetail detail)
        {
            var qe = new QueryExpression("systemform")
            {
                ColumnSet = new ColumnSet(new[] { "name", "formxml" }),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("objecttypecode", ConditionOperator.Equal, objectTypeCode),
                        new ConditionExpression("type", ConditionOperator.In, new[] {2,7}),
                        new ConditionExpression("iscustomizable", ConditionOperator.Equal, true)
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

        public void PublishForm(string entityName)
        {
            var request = new PublishXmlRequest { ParameterXml = String.Format("<importexportxml><entities><entity>{0}</entity></entities></importexportxml>", entityName) };
            Service.Execute(request);
        }

        public void UpdateForm(Entity form)
        {
            Service.Update(form);
        }
    }
}