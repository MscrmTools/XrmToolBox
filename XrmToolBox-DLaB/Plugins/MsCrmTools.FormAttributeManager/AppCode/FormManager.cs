using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace MsCrmTools.FormAttributeManager.AppCode
{
    internal class FormManager
    {
        public FormManager(IOrganizationService service)
        {
            Service = service;
        }

        public IOrganizationService Service { get; set; }

        public List<Entity> GetAllFormsByTypeCode(int objectTypeCode)
        {
            var qe = new QueryByAttribute("systemform");
            qe.ColumnSet = new ColumnSet(new[] { "name", "formxml" });
            qe.AddAttributeValue("objecttypecode", objectTypeCode);
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
