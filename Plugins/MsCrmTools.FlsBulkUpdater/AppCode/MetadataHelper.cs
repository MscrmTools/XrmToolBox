using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Metadata.Query;
using Microsoft.Xrm.Sdk.Query;

namespace MsCrmTools.FlsBulkUpdater.AppCode
{
    public class MetadataHelper
    {
        public static EntityMetadataCollection LoadMetadata(Dictionary<string, List<string>> attributes, IOrganizationService service)
        {
            try
            {
                var allAttributes = new List<string>();
                foreach (var list in attributes.Values)
                {
                    allAttributes.AddRange(list.ToArray());
                }

                var entityFilter = new MetadataFilterExpression(LogicalOperator.And);
                entityFilter.Conditions.Add(new MetadataConditionExpression("LogicalName",
                    MetadataConditionOperator.In,
                    attributes.Keys.ToArray()));
                    

                var entityQueryExpression = new EntityQueryExpression()
                {
                    Properties = new MetadataPropertiesExpression("LogicalName", "DisplayName", "Attributes"),
                    Criteria = entityFilter,
                    AttributeQuery = new AttributeQueryExpression()
                    {
                        Properties = new MetadataPropertiesExpression("DisplayName", "LogicalName"),
                        Criteria = new MetadataFilterExpression()
                        {
                            Conditions =
                            {
                                new MetadataConditionExpression("LogicalName", MetadataConditionOperator.In, allAttributes.ToArray())
                            }
                        }
                    }
                };
                var retrieveMetadataChangesRequest = new RetrieveMetadataChangesRequest()
                {
                    Query = entityQueryExpression,
                    ClientVersionStamp = null
                };

                return
                    ((RetrieveMetadataChangesResponse) service.Execute(retrieveMetadataChangesRequest)).EntityMetadata;
            }
            catch (FaultException<OrganizationServiceFault> error)
            {
                throw new Exception(error.Detail.Message + error.Detail.TraceText);
            }
        }
    }
}
