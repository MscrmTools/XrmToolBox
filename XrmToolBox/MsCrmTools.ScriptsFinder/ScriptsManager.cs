// PROJECT : MsCrmTools.ScriptsFinder
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;

namespace MsCrmTools.ScriptsFinder
{
    public class ScriptsManager
    {
        private readonly IOrganizationService service;
        public List<Script> Scripts { get; private set; }

        public ScriptsManager(IOrganizationService service)
        {
            this.service = service;
            Scripts = new List<Script>();
        }

        public void Find()
        {
            var request = new RetrieveAllEntitiesRequest {EntityFilters = EntityFilters.Entity | EntityFilters.Attributes};
            var response = (RetrieveAllEntitiesResponse) service.Execute(request);

            foreach (var emd in response.EntityMetadata.Where(x => x.IsCustomizable.Value && x.DisplayName.UserLocalizedLabel != null))
            {
                LoadScripts(emd);
            }
        }

        private void LoadScripts(EntityMetadata emd)
        {
            var qba = new QueryByAttribute("systemform");
            qba.Attributes.Add("objecttypecode");
            qba.Values.Add(emd.ObjectTypeCode.Value);
            qba.ColumnSet = new ColumnSet(true);

            foreach(var form in service.RetrieveMultiple(qba).Entities)
            {
                var doc = new XmlDocument();
                doc.LoadXml(form["formxml"].ToString());

                foreach (XmlNode eventNode in doc.SelectNodes("//event[@application='false']"))
                {

                    string eventName = eventNode.Attributes["name"].Value;

                    foreach (XmlNode handlerNode in eventNode.SelectNodes("Handlers/Handler"))
                    {
                        var script = new Script();
                        script.EntityLogicalName = emd.LogicalName;
                        script.EntityName = emd.DisplayName.UserLocalizedLabel.Label;
                        script.ScriptLocation = handlerNode.Attributes["libraryName"].Value;
                        script.MethodCalled = handlerNode.Attributes["functionName"].Value;
                        script.Event = eventName;
                        script.Attribute = eventName == "onchange" ? emd.Attributes.First(x=>x.LogicalName == eventNode.Attributes["attribute"].Value).DisplayName.UserLocalizedLabel.Label : "";
                        script.AttributeLogicalName = eventName == "onchange" ? eventNode.Attributes["attribute"].Value : "";
                        script.Form = form["name"].ToString();

                        Scripts.Add(script);
                    }
                }
            }
        }
    }
}
