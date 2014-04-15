using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using XrmToolBox;

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
                Stage = workflow.GetAttributeValue<OptionSetValue>("createstage").Value;
                Message = "Create";
            }
            else if (workflow.GetAttributeValue<bool>("triggeronupdate"))
            {
                Stage = workflow.GetAttributeValue<OptionSetValue>("updatestage").Value;
                Message = "Update";
            }
            else if (workflow.GetAttributeValue<bool>("triggerondelete"))
            {
                Stage = workflow.GetAttributeValue<OptionSetValue>("deletestage").Value;
                Message = "Delete";
            }
            else
            {
               // throw new Exception("Unexpected stage data");
            }
        }

        public int Rank
        {
            get { return workflow.GetAttributeValue<int>("rank"); }
            set { workflow["rank"] = value; }
        }

        public string EntityLogicalName
        {
            get { return workflow.GetAttributeValue<string>("primaryentity"); }
        }

        public int Stage { get; private set; }

        public string Message { get; private set; }

        public string Name
        {
            get { return workflow.GetAttributeValue<string>("name"); }
        }

        public string Description
        {
            get { return workflow.GetAttributeValue<string>("description"); }
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

         public static IEnumerable<SynchronousWorkflow> RetrievePluginSteps(IOrganizationService service)
        {
            var qba = new QueryByAttribute("workflow")
            {
                Attributes = {"mode", "type", "category"},
                Values = {1, 1, 0},
                ColumnSet = new ColumnSet(true)
            };

            var steps = service.RetrieveMultiple(qba);

            return steps.Entities.Select(e => new SynchronousWorkflow(e));
        }

         public bool HasChanged
         {
             get { return initialRank != Rank; }
         }

         public string Type { get { return "Workflow"; } }
    }
}
