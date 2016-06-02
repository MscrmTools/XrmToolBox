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
        public string Type { get { return "Business Rule"; } }

        public static IEnumerable<BusinessRules> RetrieveBusinessRules(IOrganizationService service)
        {
            var qba = new QueryByAttribute("workflow")
            {
                Attributes = { "mode", "type", "category" },
                Values = { 1, 1, 2 },
                ColumnSet = new ColumnSet(true)
            };

            var steps = service.RetrieveMultiple(qba);
            var q = from e in steps.Entities
                    orderby e.LogicalName, e.GetAttributeValue<DateTime>("modifiedon") descending
                    select new BusinessRules(e);
            return q;
        }

        public void UpdateRank(IOrganizationService service)
        {
            if (HasChanged)
            {
                var wf = service.Retrieve("workflow", workflow.Id, new ColumnSet("statecode"));
                if (wf.GetAttributeValue<OptionSetValue>("statecode").Value != 0)
                {
                    service.Execute(new SetStateRequest
                    {
                        EntityMoniker = wf.ToEntityReference(),
                        State = new OptionSetValue(0),
                        Status = new OptionSetValue(-1)
                    });
                }

                workflow.Attributes.Remove("statecode");
                workflow.Attributes.Remove("statuscode");
                service.Update(workflow);

                if (wf.GetAttributeValue<OptionSetValue>("statecode").Value != 0)
                {
                    service.Execute(new SetStateRequest
                    {
                        EntityMoniker = wf.ToEntityReference(),
                        State = new OptionSetValue(1),
                        Status = new OptionSetValue(-1)
                    });
                }
                initialRank = workflow.GetAttributeValue<int>("rank");
            }
        }
    }
}