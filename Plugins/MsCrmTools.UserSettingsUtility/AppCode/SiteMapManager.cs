using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace MsCrmTools.UserSettingsUtility.AppCode
{
    internal class SiteMapManager
    {
        private readonly XmlDocument siteMapDoc;

        public SiteMapManager(IOrganizationService service)
        {
            var qe = new QueryExpression("sitemap") { ColumnSet = new ColumnSet(true) };

            EntityCollection ec = service.RetrieveMultiple(qe);

            siteMapDoc = new XmlDocument();
            siteMapDoc.LoadXml(ec[0]["sitemapxml"].ToString());
        }

        public List<string> GetAreaList()
        {
            var areaNodes = siteMapDoc.SelectNodes("//Area");
            return (from XmlNode node in areaNodes select node.Attributes["Id"].Value).ToList();
        }

        public List<Tuple<string, string>> GetSubAreaList()
        {
            var areaNodes = siteMapDoc.SelectNodes("//SubArea");
            return (from XmlNode node in areaNodes select new Tuple<string, string>(node.Attributes["Id"].Value, node.ParentNode.ParentNode.Attributes["Id"].Value)).ToList();
        }
    }
}