using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Collections.Generic;
using System.Linq;

namespace MsCrmTools.SynchronousEventOrderEditor.AppCode
{
    internal class PluginStep : ISynchronousEvent
    {
        private readonly Entity pluginStep;
        private int initialRank;

        public PluginStep(Entity pluginStep, IEnumerable<Entity> sdkMessageFilers, IEnumerable<Entity> sdkMessages)
        {
            this.pluginStep = pluginStep;
            initialRank = Rank;

            // EntityLogicalName
            var messageFilter = sdkMessageFilers.FirstOrDefault(
                    s => pluginStep.GetAttributeValue<EntityReference>("sdkmessagefilterid") != null &&
            s.Id == pluginStep.GetAttributeValue<EntityReference>("sdkmessagefilterid").Id);
            if (messageFilter != null)
            {
                EntityLogicalName = messageFilter.GetAttributeValue<string>("primaryobjecttypecode");

                if (EntityLogicalName.Length == 0)
                {
                    EntityLogicalName = "None";
                }

                var message = sdkMessages.FirstOrDefault(
                    m => m.Id == messageFilter.GetAttributeValue<EntityReference>("sdkmessageid").Id);
                if (message != null)
                {
                    Message = message.GetAttributeValue<string>("name");
                }
            }
            else
            {
                EntityLogicalName = "(none)";

                var message = sdkMessages.FirstOrDefault(
                  m => m.Id == pluginStep.GetAttributeValue<EntityReference>("sdkmessageid").Id);
                if (message != null)
                {
                    Message = message.GetAttributeValue<string>("name");
                }
            }
        }

        public string Description
        {
            get { return pluginStep.GetAttributeValue<string>("description"); }
        }

        public string EntityLogicalName { get; private set; }

        public bool HasChanged
        {
            get { return initialRank != Rank; }
        }

        public string Message { get; private set; }

        public string Name
        {
            get { return pluginStep.GetAttributeValue<string>("name"); }
        }

        public int Rank
        {
            get { return pluginStep.GetAttributeValue<int>("rank"); }
            set { pluginStep["rank"] = value; }
        }

        public int Stage
        {
            get { return pluginStep.GetAttributeValue<OptionSetValue>("stage").Value; }
        }

        public string Type { get { return "Plugin step"; } }

        public static IEnumerable<PluginStep> RetrievePluginSteps(IOrganizationService service, IEnumerable<Entity> sdkMessageFilers, IEnumerable<Entity> sdkMessages)
        {
            var qe = new QueryExpression("sdkmessageprocessingstep")
            {
                ColumnSet = new ColumnSet(true),
                Criteria =
                {
                    Conditions =
                    {
                         new ConditionExpression("mode", ConditionOperator.Equal, 0)
                    }
                },
                LinkEntities =
                {
                    new LinkEntity
                    {
                        LinkFromEntityName = "sdkmessageprocessingstep",
                        LinkFromAttributeName = "plugintypeid",
                        LinkToAttributeName = "plugintypeid",
                        LinkToEntityName = "plugintype",
                        LinkCriteria =
                        {
                            Conditions =
                            {
                                new ConditionExpression("typename", ConditionOperator.NotLike, "Microsoft.Crm.%"),
                                new ConditionExpression("typename", ConditionOperator.NotLike, "Compiled.%")
                            }
                        }
                    }
                }
            };

            var steps = service.RetrieveMultiple(qe);

            return steps.Entities.Select(e => new PluginStep(e, sdkMessageFilers, sdkMessages));
        }

        public void UpdateRank(IOrganizationService service)
        {
            if (HasChanged)
            {
                service.Update(pluginStep);
                initialRank = pluginStep.GetAttributeValue<int>("rank");
            }
        }
    }
}