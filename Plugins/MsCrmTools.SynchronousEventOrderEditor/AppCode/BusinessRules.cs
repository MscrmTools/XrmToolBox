using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MsCrmTools.SynchronousEventOrderEditor.AppCode
{
    internal class BusinessRules : ISynchronousEvent
    {
        private readonly Entity workflow;

        private int initialRank;

        public BusinessRules(Entity workflow)
        {
            this.workflow = workflow;
            initialRank = Rank;
            Message = "Create/Update";
            Stage = 40;
        }

        public string Description => workflow.GetAttributeValue<string>("description");

        public string EntityLogicalName => workflow.GetAttributeValue<string>("primaryentity");

        public bool HasChanged => initialRank != Rank;

        public string Message { get; private set; }

        public string Name => workflow.GetAttributeValue<string>("name");

        public int Rank
        {
            get { return workflow.GetAttributeValue<int>("rank"); }
            set { workflow["rank"] = value; }
        }

        public int Stage { get; private set; }

        public string Type => "Business Rule";

        public static IEnumerable<BusinessRules> RetrieveBusinessRules(IOrganizationService service)
        {
            var businessRules = service.RetrieveMultiple(new FetchExpression(@"
            <fetch>
                <entity name='workflow' >
                <attribute name='triggeroncreate' />
                <attribute name='createdon' />
                <attribute name='primaryentity' />
                <attribute name='triggerondelete' />
                <attribute name='triggeronupdateattributelist' />
                <attribute name='processorder' />
                <attribute name='modifiedon' />
                <attribute name='name' />
                <filter>
                    <condition attribute='type' operator='eq' value='1' />
                    <condition attribute='category' operator='eq' value='2' />
                </filter>
                </entity>
            </fetch>"));

            var q = from e in businessRules.Entities
                    orderby e.GetAttributeValue<string>("primaryentity"), e.GetAttributeValue<DateTime>("modifiedon")
                    select new BusinessRules(e);
            return q;
        }

        public void UpdateRank(IOrganizationService service)
        {
            if (HasChanged)
            {
                initialRank = WorkflowHelper.UpdateRank(service, workflow);
            }
        }
    }
}