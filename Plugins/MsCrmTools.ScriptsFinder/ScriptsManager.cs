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

            foreach (var emd in response.EntityMetadata.Where(x =>(x.IsCustomizable.Value || x.IsManaged.Value == false) && x.DisplayName.UserLocalizedLabel != null))
            {
                LoadScripts(emd);
                LoadRibbonCommands(emd);
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
                        script.IsActive = handlerNode.Attributes["enabled"].Value == "true";
                        script.Event = eventName;

                        if (eventName == "onchange")
                        {
                            var amd = emd.Attributes.FirstOrDefault(x => x.LogicalName == eventNode.Attributes["attribute"].Value);

                            if (amd != null)
                            {
                                var displayName = amd.DisplayName != null && amd.DisplayName.UserLocalizedLabel != null
                                    ? amd.DisplayName.UserLocalizedLabel.Label
                                    : "(" + amd.LogicalName + ")";

                                script.Attribute = displayName;
                                script.AttributeLogicalName = amd.LogicalName;
                            }
                            else
                            {
                                script.Attribute = eventNode.Attributes["attribute"].Value;
                                script.AttributeLogicalName = eventNode.Attributes["attribute"].Value;
                                script.HasProblem = true;
                            }
                        }
                        else
                        {
                            script.Attribute = "";
                            script.AttributeLogicalName = "";
                        }
                        script.Name = form["name"].ToString();
                        script.Type = "Form event";

                        Scripts.Add(script);
                    }
                }

                foreach (XmlNode libraryNode in doc.SelectNodes("//Library"))
                {
                    var script = new Script();
                    script.EntityLogicalName = emd.LogicalName;
                    script.EntityName = emd.DisplayName.UserLocalizedLabel.Label;
                    script.ScriptLocation = libraryNode.Attributes["name"].Value;
                    script.MethodCalled = string.Empty;
                    script.Event = string.Empty;
                    script.Attribute = string.Empty;
                    script.AttributeLogicalName = string.Empty;
                    script.Name = form["name"].ToString();
                    script.Type = "Form Library";

                    Scripts.Add(script);
                }
            }
        }

        private void LoadRibbonCommands(EntityMetadata emd)
        {
            var commands = service.RetrieveMultiple(new QueryExpression("ribboncommand")
            {
                ColumnSet = new ColumnSet(true),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("commanddefinition", ConditionOperator.Like, "%Library=\"$webresource:%"),
                        new ConditionExpression("entity", ConditionOperator.Equal, emd.LogicalName)
                    }
                }
            });

            foreach (var command in commands.Entities)
            {
                var commandDoc = new XmlDocument();
                commandDoc.LoadXml(command.GetAttributeValue<string>("commanddefinition"));

                var actionsNode = commandDoc.SelectSingleNode("CommandDefinition/Actions");

                foreach (XmlNode actionNode in actionsNode.ChildNodes)
                {
                    if (actionNode.Attributes == null)
                        continue;

                    var libraryNode = actionNode.Attributes["Library"];
                    if (libraryNode == null)
                    {
                        continue;
                    }

                    var libraryName = libraryNode.Value;

                    if (libraryName.Split(':').Length == 1)
                        continue;

                    var script = new Script();
                    script.EntityLogicalName = emd.LogicalName;
                    script.EntityName = emd.DisplayName.UserLocalizedLabel.Label;
                    script.ScriptLocation = libraryName.Split(':')[1];
                    script.MethodCalled = actionNode.Attributes["FunctionName"].Value;
                    script.Event = "";
                    script.Attribute = "";
                    script.AttributeLogicalName = "";
                    script.Name = string.Empty;
                    script.Type = "Ribbon Command";

                    Scripts.Add(script);
                }
            }
        }
    }
}
