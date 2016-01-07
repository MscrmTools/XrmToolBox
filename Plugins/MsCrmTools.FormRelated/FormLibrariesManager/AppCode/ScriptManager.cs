using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Collections.Generic;
using System.Linq;

namespace MsCrmTools.FormLibrariesManager.AppCode
{
    internal class ScriptManager
    {
        public ScriptManager(IOrganizationService service)
        {
            Service = service;
        }

        public IOrganizationService Service { get; set; }

        public List<Entity> GetAllScripts()
        {
            var qba = new QueryByAttribute("webresource");
            qba.ColumnSet = new ColumnSet(true);
            qba.AddAttributeValue("webresourcetype", 3);
            qba.AddAttributeValue("ishidden", false);

            return Service.RetrieveMultiple(qba).Entities.ToList();
        }
    }
}