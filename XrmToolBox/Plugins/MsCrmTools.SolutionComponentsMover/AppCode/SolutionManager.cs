using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace MsCrmTools.SolutionComponentsMover.AppCode
{
    internal class SolutionManager
    {
        private readonly IOrganizationService service;

        public SolutionManager(IOrganizationService service)
        {
            this.service = service;
        }

        public IEnumerable<Entity> RetrieveSolutions()
        {
            var qe = new QueryExpression
            {
                EntityName = "solution",
                ColumnSet = new ColumnSet(new[]{
                                            "publisherid", "installedon", "version",
                                            "uniquename", "friendlyname", "description"
                                        }),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("ismanaged", ConditionOperator.Equal, false),
                        new ConditionExpression("isvisible", ConditionOperator.Equal, true),
                        new ConditionExpression("uniquename", ConditionOperator.NotEqual, "Default")
                    }
                }
            };

            return service.RetrieveMultiple(qe).Entities;
        }

        internal List<Entity> RetrieveComponentsFromSolutions(List<Guid> solutionsIds, List<int> componentsTypes )
        {
            var qe = new QueryExpression("solutioncomponent")
            {
                ColumnSet = new ColumnSet(true),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("solutionid", ConditionOperator.In, solutionsIds.ToArray()),
                        new ConditionExpression("componenttype", ConditionOperator.In, componentsTypes.ToArray())
                    }
                }
            };

            return service.RetrieveMultiple(qe).Entities.ToList();
        } 

        internal void CopyComponents(CopySettings settings, BackgroundWorker backgroundWorker)
        {
            backgroundWorker.ReportProgress(0,"Retrieving source solution(s) components...");

            var components = RetrieveComponentsFromSolutions(settings.SourceSolutions.Select(s => s.Id).ToList(), settings.ComponentsTypes);
            
            foreach (var target in settings.TargetSolutions)
            {
                backgroundWorker.ReportProgress(0, string.Format("Adding {0} components to solution '{1}'", components.Count, target.GetAttributeValue<string>("friendlyname")));

                foreach (var component in components)
                {
                    var request = new AddSolutionComponentRequest
                    {
                        AddRequiredComponents = false,
                        ComponentId =component.GetAttributeValue<Guid>("objectid"),
                        ComponentType = component.GetAttributeValue<OptionSetValue>("componenttype").Value,
                        SolutionUniqueName = target.GetAttributeValue<string>("uniquename")
                    };

                    service.Execute(request);
                }
            }
        }
    }
}
