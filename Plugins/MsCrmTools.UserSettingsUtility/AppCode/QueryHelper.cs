using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace MsCrmTools.UserSettingsUtility.AppCode
{
    internal class QueryHelper
    {
        public static EntityCollection GetItems(string fetchXml, IOrganizationService service)
        {
            // Check if businessunitid attribute is contained in attriburtes
            var xDoc = XDocument.Parse(fetchXml);
            AddMissingCrmAttribute(xDoc, "businessunitid");

            var entityElement = xDoc.Descendants("entity").FirstOrDefault();
            if (entityElement == null)
            {
                throw new Exception("Cannot find node 'entity' in FetchXml");
            }

            AddMissingCrmAttribute(xDoc, "firstname");
            AddMissingCrmAttribute(xDoc, "lastname");

            return service.RetrieveMultiple(new FetchExpression(xDoc.ToString()));
        }

        private static void AddMissingCrmAttribute(XDocument xDoc, string attributeName)
        {
            var xBuAttribute = xDoc.XPathSelectElement("fetch/entity/attribute[@name='" + attributeName + "']");
            if (xBuAttribute == null)
            {
                var entityElement = xDoc.Descendants("entity").FirstOrDefault();
                if (entityElement == null)
                {
                    throw new Exception("Cannot find node 'entity' in FetchXml");
                }

                entityElement.Add(new XElement("attribute", new XAttribute("name", attributeName)));
            }
        }
    }
}