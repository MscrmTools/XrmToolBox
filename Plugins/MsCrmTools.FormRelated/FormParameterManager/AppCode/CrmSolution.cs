using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Collections.Generic;
using System.Linq;

namespace MsCrmTools.FormParameterManager.AppCode
{
    internal class CrmSolution
    {
        public CrmSolution(string name, string prefix)
        {
            Name = name;
            Prefix = prefix;
        }

        public string Name { get; private set; }

        public string Prefix { get; private set; }

        public static List<CrmSolution> GetUnmanagedSolutions(IOrganizationService service)
        {
            var qba = new QueryByAttribute("solution") { ColumnSet = new ColumnSet("friendlyname", "publisherid") };
            qba.Attributes.AddRange("ismanaged", "isvisible");
            qba.Values.AddRange(false, true);

            var solutions = service.RetrieveMultiple(qba);

            return (from solution in solutions.Entities
                    let publisher =
                        service.Retrieve("publisher", solution.GetAttributeValue<EntityReference>("publisherid").Id,
                            new ColumnSet("customizationprefix"))
                    select
                        new CrmSolution(solution.GetAttributeValue<string>("friendlyname"),
                            publisher.GetAttributeValue<string>("customizationprefix"))).ToList();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}