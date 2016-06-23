using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsCrmTools.SynchronousEventOrderEditor.AppCode
{
    public class WorkflowHelper
    {
        public static int UpdateRank(IOrganizationService service, Entity workflow)
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
            return workflow.GetAttributeValue<int>("rank");
        }
    }
}
