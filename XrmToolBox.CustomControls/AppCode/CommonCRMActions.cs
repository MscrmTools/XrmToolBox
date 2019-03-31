using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

using Microsoft.Xrm.Sdk.Query;

namespace XrmToolBox.CustomControls
{
    /// <summary>
    /// Various helper methods for working with CRM queries, objects, etc.
    /// </summary>
    public class CommonCRMActions
    {
        #region Entities
        /// <summary>
        /// Rerieve all entities with the given filter conditions
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entityFilters"></param>
        /// <param name="retrieveAsIfPublished"></param>
        /// <returns></returns>
        public static List<EntityMetadata> RetrieveAllEntities(IOrganizationService service, List<EntityFilters> entityFilters = null, bool retrieveAsIfPublished = true)
        {
            if (entityFilters == null) {
                entityFilters = new List<EntityFilters>() { EntityFilters.Default };
            }

            // build the bitwise or list of the entity filters
            var filters = entityFilters.Aggregate<EntityFilters, EntityFilters>(0, (current, item) => current | item);

            var req = new RetrieveAllEntitiesRequest() {
                EntityFilters = filters,
                RetrieveAsIfPublished = retrieveAsIfPublished
            };
            var resp = (RetrieveAllEntitiesResponse)service.Execute(req);

            // set the itemsource of the itembox equal to entity metadata that is customizable (takes out systemjobs and stuff like that)
            var entities = resp.EntityMetadata.Where(x => x.IsCustomizable.Value == true).ToList<EntityMetadata>();

            return entities;
        }

        /// <summary>
        /// Rerieve all entities with the given filter conditions
        /// </summary>
        /// <param name="service"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static List<EntityMetadata> RetrieveAllEntities(IOrganizationService service, ConfigurationInfo config)
        {
            return RetrieveAllEntities(service, config.EntityRequestFilters, config.RetrieveAsIfPublished);
        }

        /// <summary>
        /// Retrieve an Entity Metadata and include the default entity Entity details
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entityLogicalName"></param>
        /// <param name="retrieveAsIfPublished"></param>
        /// <returns></returns>
        public static EntityMetadata RetrieveEntity(IOrganizationService service, string entityLogicalName, bool retrieveAsIfPublished)
        {
            return RetrieveEntity(service, entityLogicalName, retrieveAsIfPublished, new List<EntityFilters> { EntityFilters.Default });
        }

        /// <summary>
        /// Retrieve the Entity Metadata and include the details as provided by the entityFilter argument
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entityLogicalName"></param>
        /// <param name="retrieveAsIfPublished"></param>
        /// <param name="entityFilter"></param>
        /// <returns></returns>
        public static EntityMetadata RetrieveEntity(IOrganizationService service, string entityLogicalName, bool retrieveAsIfPublished, EntityFilters entityFilter)
        {
            return RetrieveEntity(service, entityLogicalName, retrieveAsIfPublished, new List<EntityFilters> { entityFilter });
        }
        /// <summary>
        /// Retrieve an Entity Metadata and include Entity details as specified in the EntityFilters provided 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entityLogicalName"></param>
        /// <param name="retrieveAsIfPublished"></param>
        /// <param name="entityFilters"></param>
        /// <returns></returns>
        public static EntityMetadata RetrieveEntity(IOrganizationService service, string entityLogicalName, bool retrieveAsIfPublished, List<EntityFilters> entityFilters)
        {
            var filters = entityFilters.Aggregate<EntityFilters, EntityFilters>(0, (current, item) => current | item);

            var req = new RetrieveEntityRequest() {
                RetrieveAsIfPublished = retrieveAsIfPublished,
                EntityFilters = filters,
                LogicalName = entityLogicalName
            };

            var resp = (RetrieveEntityResponse)service.Execute(req);

            return resp.EntityMetadata;
        }

        /// <summary>
        /// Retrieve entities in a list of Entity logical names
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entityLogcialNames"></param>
        /// <param name="retrieveAsIfPublished"></param>
        /// <param name="entityFilters"></param>
        /// <returns></returns>
        public static List<EntityMetadata> RetrieveEntity(IOrganizationService service, List<string> entityLogcialNames, bool retrieveAsIfPublished = true, List<EntityFilters> entityFilters = null) {

            EntityFilters filters = EntityFilters.Default;
            if (entityFilters != null) {
                filters = entityFilters.Aggregate<EntityFilters, EntityFilters>(0, (current, item) => current | item);
            }
            var entities = new List<EntityMetadata>();
            // Create an ExecuteMultipleRequest object.
            var batchRequest = new ExecuteMultipleRequest() {
                Settings = new ExecuteMultipleSettings() {
                    ContinueOnError = false,
                    ReturnResponses = true
                },
                Requests = new OrganizationRequestCollection()
            };

            foreach (var entityLogicalName in entityLogcialNames) {
                batchRequest.Requests.Add(
                    new RetrieveEntityRequest() {
                        RetrieveAsIfPublished = retrieveAsIfPublished,
                        EntityFilters = filters,
                        LogicalName = entityLogicalName
                    });
            }

            var responses = (ExecuteMultipleResponse)service.Execute(batchRequest);

            foreach (var resp in responses.Responses) {
                entities.Add(((RetrieveEntityResponse)resp.Response).EntityMetadata);
            }

            return entities;
        }

        #endregion

        #region Entity Views

        /// <summary>
        /// Helper method for retrieving system and user saved queries for an entity
        /// </summary>
        /// <param name="service"></param>
        /// <param name="objectTypeCode"></param>
        /// <param name="includePersonal"></param>
        /// <returns></returns>
        public static List<Entity> RetrieveEntityViews(IOrganizationService service, int objectTypeCode, bool includePersonal) {

            var query = new QueryExpression("savedquery")
            {
                ColumnSet = new ColumnSet(true),
                /*ColumnSet = new ColumnSet("savedqueryid", "returnedtypecode", "name", "description", "statuscode", "statecode", 
                "savedqueryidunique", "conditionalformatting", "solutionid", "organizationid", "querytype",
                "columnsetxml", "fetchxml", "layoutjson", "layoutxml",
                "canbedeleted", "iscustom", "isdefault", "isuserdefined", "iscustomizable", "ismanaged", "isprivate", "isquickfindquery",
                "createdon", "createdby", "modifiedby", "modifiedon", "componentstate", "introducedversion", "advancedgroupby",
                "overwritetime", "queryapi", "organizationtaborder", "queryappusage", "versionnumber"),*/
                Criteria = new FilterExpression() {
                    Conditions = { new ConditionExpression("returnedtypecode", ConditionOperator.Equal, objectTypeCode) }
                }
            };

            var systemViews = service.RetrieveMultiple(query);
            var allViews = new List<Entity>(systemViews.Entities.ToList());

            if (includePersonal) {

                query = new QueryExpression("userquery") {
                    ColumnSet = new ColumnSet(true),
                    //ColumnSet = new ColumnSet("userqueryid", "returnedtypecode", "statecode", "description", "name",
                    //    "layoutxml", "columnsetxml", "fetchxml", "querytype", "statuscode",
                    //    "owninguser", "owningbusinessunit", "owningteam", "parentqueryid", "advancedgroupby", "conditionalformatting",
                    //    "modifiedby", "modifiedon", "createdby", "createdon", "ownerid", "versionnumber"),
                    Criteria = new FilterExpression() {
                        Conditions = { new ConditionExpression("returnedtypecode", ConditionOperator.Equal, objectTypeCode) }
                    }
                };

                var personalViews = service.RetrieveMultiple(query);

                if (personalViews.Entities.Count > 0) {
                    allViews.AddRange(personalViews.Entities.ToList());
                }
            }

            return allViews;
        }
        #endregion

        #region Entity Keys

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entityLogicalName"></param>
        /// <param name="logicalName"></param>
        /// <param name="keyDisplayName"></param>
        /// <param name="keyAttributes"></param>
        public static string CreateEntityKey(IOrganizationService service, string entityLogicalName, string logicalName, string keyDisplayName, List< string > keyAttributes)
        {
            try
            {
                var entKeyMeta = new EntityKeyMetadata() {
                    KeyAttributes = keyAttributes.ToArray(),
                    LogicalName = logicalName,
                    DisplayName = new Label(keyDisplayName, 1033),
                    SchemaName = logicalName
                };
                var req = new CreateEntityKeyRequest() {
                    EntityKey = entKeyMeta,
                    EntityName = entityLogicalName
                };

                var resp = service.Execute(req) as CreateEntityKeyResponse;

                return null;
            }
            catch (Exception ex) {
                return $"Error createing the new Alternate Key: {keyDisplayName} ({logicalName}): \n{ex.Message}";
            }
        }

        /// <summary>
        /// Reactivate an Entity Key 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<string> ReactivateEntityKey(IOrganizationService service, EntityKeyMetadata key) {

            try {
                // call the Activate action for the selected key 
                var req = new ReactivateEntityKeyRequest() {
                    EntityKeyLogicalName = key.SchemaName,
                    EntityLogicalName = key.EntityLogicalName
                };

                var resp = (ReactivateEntityKeyResponse)service.Execute(req);

                return null;
            }
            catch(Exception ex)
            {
                return new List<string>() {$"Error reactivating key {key.SchemaName}: \n{ex.Message}" };
            }
        }

        /// <summary>
        /// Reactivate a batch of Entity Alternate Keys
        /// </summary>
        /// <param name="service"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static List<string> ReactivateEntityKey(IOrganizationService service, List<EntityKeyMetadata> keys)
        {
            // Create an ExecuteMultipleRequest object.
            var batchRequest = new ExecuteMultipleRequest() {
                Settings = new ExecuteMultipleSettings() {
                    ContinueOnError = false,
                    ReturnResponses = true
                },
                Requests = new OrganizationRequestCollection()
            };

            // add the requests
            foreach (var key in keys) 
            {
                // call the Activate action for the selected key 
                batchRequest.Requests.Add(new ReactivateEntityKeyRequest() {
                    EntityKeyLogicalName = key.SchemaName,
                    EntityLogicalName = key.EntityLogicalName
                });
            }

            var responses = (ExecuteMultipleResponse)service.Execute(batchRequest);

            if (responses.IsFaulted) {

                var faultList = new List<string>();

                foreach (var resp in responses.Responses) {
                    var respDetail = resp.Response as ReactivateEntityKeyResponse;
                    var name = (respDetail != null) ? respDetail.ResponseName : $"Error code: {resp.Fault.ErrorCode.ToString()}";
                    faultList.Add($"Error reactivating key {name}\n{resp.Fault.Message}");
                }
                return faultList;
            }
            else {
                return null;
            }            
        }

        /// <summary>
        /// Retrieve an Entity Key Metadata record 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="config"></param>
        /// <param name="entity"></param>
        /// <param name="retrieveAsIfPublished"></param>
        /// <returns></returns>
        public static EntityKeyMetadata RetrieveEntityKey(IOrganizationService service, ConfigurationInfo config, EntityMetadata entity, bool retrieveAsIfPublished)
        {
            var req = new RetrieveEntityKeyRequest() {
                EntityLogicalName = entity.LogicalName,
                RetrieveAsIfPublished = true
            };

            var resp = service.Execute(req) as RetrieveEntityKeyResponse;
            return resp.EntityKeyMetadata;
        }

        /// <summary>
        /// Delete a selected Entity Alternate Key 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="key"></param>
        public static void DeleteEntityKey(IOrganizationService service, EntityKeyMetadata key)
        {
            var req = new DeleteEntityKeyRequest() {
                EntityLogicalName = key.EntityLogicalName,
                Name = key.LogicalName
            };

            var resp = service.Execute(req) as DeleteEntityKeyResponse;
        }

        /// <summary>
        /// Delete a list of Alternate Keys
        /// </summary>
        /// <param name="service"></param>
        /// <param name="keys"></param>
        public static void DeleteEntityKey(IOrganizationService service, List<EntityKeyMetadata> keys)
        {
            var batchRequest = new ExecuteMultipleRequest() {
                Settings = new ExecuteMultipleSettings() {
                    ContinueOnError = false,
                    ReturnResponses = true
                },
                Requests = new OrganizationRequestCollection()
            };

            // add the requests
            foreach (var key in keys) 
            {
                batchRequest.Requests.Add(new DeleteEntityKeyRequest() {
                    EntityLogicalName = key.EntityLogicalName,
                    Name = key.LogicalName
                });
            }
            var resp = service.Execute(batchRequest) as DeleteEntityKeyResponse;
        }
        #endregion

        #region Attributes
        /// <summary>
        /// Retrieve all attributes for an Entity
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entityLogicalName"></param>
        /// <param name="retrieveAsIfPublished"></param>
        /// <returns></returns>
        public static List<AttributeMetadata> RetrieveEntityAttributes(IOrganizationService service, string entityLogicalName, bool retrieveAsIfPublished)
        {
            // Retrieve the attribute metadata
            var req = new RetrieveEntityRequest() {
                LogicalName = entityLogicalName,
                EntityFilters = EntityFilters.Attributes,
                RetrieveAsIfPublished = retrieveAsIfPublished
            };
            var resp = (RetrieveEntityResponse)service.Execute(req);

            return resp.EntityMetadata.Attributes.ToList();
        }

        /// <summary>
        /// Retrieve all attributes for an Entity
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entityLogicalName"></param>
        /// <returns></returns>
        public static List<AttributeMetadata> RetrieveEntityAttributes(IOrganizationService service, string entityLogicalName) {

            return RetrieveEntityAttributes(service, entityLogicalName, true);
        }
        #endregion

        #region Additional Metadata helper methods 
        /// <summary>
        /// Rtrieve a list of Publishers with some common attributes
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static List<Entity> RetrievePublishers(IOrganizationService service)
        {
            var query = new QueryExpression("publisher") {
                ColumnSet = new ColumnSet("uniquename", "friendlyname", "isreadonly", "description", "customizationprefix", "publisherid", "organizationid")
            };
            var resp = service.RetrieveMultiple(query);

            return resp.Entities.ToList();
        }

        /// <summary>
        /// Retrieve a list of Solutions with some common attributes
        /// </summary>
        /// <param name="service"></param>
        /// <param name="publisherFilters"></param>
        /// <returns></returns>
        public static List<Entity> RetrieveSolutions(IOrganizationService service, List<string> publisherFilters = null)
        {
            var query = new QueryExpression("solution") {
                ColumnSet = new ColumnSet("solutionid", "uniquename", "friendlyname", "description", "publisherid", "isvisible", "ismanaged",
                                            "version", "versionnumber", "installedon", "createdon", "modifiedon",
                                            "solutionpackageversion", "solutiontype", "parentsolutionid",  
                                            "createdonbehalfby", "configurationpageid", "organizationid")
            };

            // Add link-entity QEsolution_publisher
            var publink = query.AddLink("publisher", "publisherid", "publisherid", JoinOperator.Inner);
            publink.EntityAlias = "pub";
            publink.Columns.AddColumns("customizationprefix", "friendlyname", "publisherid", "uniquename");

            if (publisherFilters != null)
            {
                foreach (var publisher in publisherFilters)
                {
                    // filter on either the publisher name or prefix
                    var filter = new FilterExpression() {
                        FilterOperator = LogicalOperator.Or,
                        Conditions = {
                            new ConditionExpression("customizationprefix", ConditionOperator.Equal, publisher),
                            new ConditionExpression("friendlyname", ConditionOperator.Equal, publisher),
                            new ConditionExpression("uniquename", ConditionOperator.Equal, publisher)}
                    };
                    publink.LinkCriteria.AddFilter(filter);
                }
            }
            var resp = service.RetrieveMultiple(query);

            return resp.Entities.ToList();
        }


        /// <summary>
        /// https://simonetagliaro.wordpress.com/2012/10/02/retrieve-all-the-entities-within-a-solution-crm-2011/
        /// </summary>
        /// <param name="service"></param>
        /// <param name="solutionName"></param>
        /// <param name="retrieveAsIfPublished"></param>
        /// <returns></returns>
        public static List<EntityMetadata> RetrieveEntitiesForSolution(IOrganizationService service, string solutionName, bool retrieveAsIfPublished = true)
        {
            // get solution components for solution
            var query = new QueryExpression
            {
                EntityName = "solutioncomponent",
                ColumnSet = new ColumnSet("objectid"),
                Criteria = new FilterExpression() {
                    Conditions = { new ConditionExpression("componenttype", ConditionOperator.Equal, 1) }
                },
                LinkEntities = {
                    new LinkEntity("solutioncomponent", "solution", "solutionid", "solutionid", JoinOperator.Inner) {
                        LinkCriteria = new FilterExpression() {
                            Conditions = { new ConditionExpression("uniquename", ConditionOperator.Equal, solutionName) }
                        }
                    }
                }
            };
            var components = service.RetrieveMultiple(query);
            
            //Get all entities
            var entRequest = new RetrieveAllEntitiesRequest() {
                EntityFilters = EntityFilters.Entity,
                RetrieveAsIfPublished = true
            };

            var entities = (RetrieveAllEntitiesResponse)service.Execute(entRequest);
            //Join entities Id and solution Components Id 
            return entities.EntityMetadata
                .Join(
                components.Entities.Select(x => x.Attributes["objectid"]), 
                    x => x.MetadataId, y => y, 
                    (x, y) => x)
                .ToList();
        }
        #endregion

        #region OptionSets
        public static List<OptionSetMetadataBase > RetrieveGlobalOptionSets(IOrganizationService service, bool retrieveAsIfPublished = true, string solutionName = null) {
            var request = new RetrieveAllOptionSetsRequest() {
                RetrieveAsIfPublished = retrieveAsIfPublished
            };

            // Execute the request
            var response = (RetrieveAllOptionSetsResponse)service.Execute(request);

            return response.OptionSetMetadata.ToList();
        }
        #endregion

        /// <summary>
        /// Helper method to get the first label in the list of LocalizedLabels 
        /// </summary>
        /// <param name="label">Label object containing Localized Labels </param>
        /// <param name="valueIfNull">If the Localizd Labels are null, use this value instead</param>
        /// <param name="languagCode"></param>
        /// <returns></returns>
        public static string GetLocalizedLabel(Label label, string valueIfNull, int languagCode = 1033) {
            var labels = label.LocalizedLabels;
            var locLabelValue = valueIfNull;

            if (labels.Count > 0)
            {
                var locLabel = labels.Where(lbl => lbl.LanguageCode == languagCode).FirstOrDefault();

                locLabelValue = (locLabel != null) ? locLabel.Label : valueIfNull;
            }

            return locLabelValue;
        }
    }
}
