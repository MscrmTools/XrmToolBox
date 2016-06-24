using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Collections.Generic;
using System.Linq;

namespace MsCrmTools.SynchronousEventOrderEditor.AppCode
{
    internal class SynchronousWorkflow : ISynchronousEvent
    {
        private readonly Entity workflow;

        private int initialRank;

        public SynchronousWorkflow(Entity workflow)
        {
            this.workflow = workflow;
            initialRank = Rank;

            // Stage
            if (workflow.GetAttributeValue<bool>("triggeroncreate"))
            {
                var stageCode = workflow.GetAttributeValue<OptionSetValue>("createstage");
                Stage = stageCode != null ? stageCode.Value : 40;
                Message = "Create";
            }
            else if (workflow.GetAttributeValue<bool>("triggeronupdate") || 
                !string.IsNullOrEmpty(workflow.GetAttributeValue<string>("triggeronupdateattributelist")))
            {
                var stageCode = workflow.GetAttributeValue<OptionSetValue>("updatestage");
                Stage = stageCode != null ? stageCode.Value : 40;
                Message = "Update";
            }
            else if (workflow.GetAttributeValue<bool>("triggerondelete"))
            {
                var stageCode = workflow.GetAttributeValue<OptionSetValue>("deletestage");
                Stage = stageCode != null ? stageCode.Value : 20;
                Message = "Delete";
            }
        }

        public string Description
        {
            get { return workflow.GetAttributeValue<string>("description"); }
        }

        public string EntityLogicalName
        {
            get { return workflow.GetAttributeValue<string>("primaryentity"); }
        }

        public bool HasChanged
        {
            get { return initialRank != Rank; }
        }

        public string Message { get; private set; }

        public string Name
        {
            get { return workflow.GetAttributeValue<string>("name"); }
        }

        public int Rank
        {
            get { return workflow.GetAttributeValue<int>("rank"); }
            set { workflow["rank"] = value; }
        }

        public int Stage { get; private set; }
        public string Type { get { return "Workflow"; } }

        public static IEnumerable<SynchronousWorkflow> RetrieveTriggeredWorkflows(IOrganizationService service)
        {
            var workflows = service.RetrieveMultiple(new FetchExpression(@"
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
                    <condition attribute='mode' operator='eq' value='1' />
                    <condition attribute='type' operator='eq' value='1' />
                    <condition attribute='category' operator='eq' value='0' />
                    <filter type='or' >
                    <condition attribute='triggeroncreate' operator='eq' value='1' />
                    <condition attribute='triggerondelete' operator='eq' value='1' />
                    <condition attribute='triggeronupdateattributelist' operator='not-null' />
                    </filter>
                </filter>
                </entity>
            </fetch>"));

            return workflows.Entities.Select(e => new SynchronousWorkflow(e));
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