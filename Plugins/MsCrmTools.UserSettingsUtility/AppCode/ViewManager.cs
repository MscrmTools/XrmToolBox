using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MsCrmTools.UserSettingsUtility.AppCode
{
    internal class ViewManager
    {
        private readonly IOrganizationService service;

        public ViewManager(IOrganizationService service)
        {
            this.service = service;
        }

        public List<ViewItem> RetrieveViews(string entityLogicalName, BackgroundWorker worker = null)
        {
            var items = new List<ViewItem>();

            if (worker != null && worker.WorkerReportsProgress)
            {
                worker.ReportProgress(0, "Retrieve system views for entity " + entityLogicalName);
            }

            var savedQueries = service.RetrieveMultiple(new QueryExpression("savedquery")
            {
                ColumnSet = new ColumnSet(true),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("returnedtypecode", ConditionOperator.Equal, entityLogicalName),
                        new ConditionExpression("statecode", ConditionOperator.Equal, 0),
                        new ConditionExpression("querytype", ConditionOperator.Equal, 0),
                        new ConditionExpression("fetchxml", ConditionOperator.NotNull),
                    }
                }
            });

            items.AddRange(savedQueries.Entities.Select(e => new ViewItem(e)));

            if (worker != null && worker.WorkerReportsProgress)
            {
                worker.ReportProgress(0, "Retrieve personal views for entity " + entityLogicalName);
            }

            savedQueries = service.RetrieveMultiple(new QueryExpression("userquery")
            {
                ColumnSet = new ColumnSet(true),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("returnedtypecode", ConditionOperator.Equal, entityLogicalName)
                    }
                }
            });

            items.AddRange(savedQueries.Entities.Select(e => new ViewItem(e)));

            return items;
        }
    }
}