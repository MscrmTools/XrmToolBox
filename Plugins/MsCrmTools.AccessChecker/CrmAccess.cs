using System;
using System.Collections.Generic;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;

namespace MsCrmTools.AccessChecker
{
    /// <summary>
    /// This class provides methods to query CRM Server
    /// </summary>
    public class CrmAccess
    {
        #region Variables

        /// <summary>
        /// CRM proxy data service
        /// </summary>
        readonly IOrganizationService service;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class CrmAccess
        /// </summary>
        /// <param name="service">Organization service</param>
        public CrmAccess(IOrganizationService service)
        {
            this.service = service;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Retrieve the list of all CRM entities
        /// </summary>
        /// <returns>List of all CRM entities</returns>
        public RetrieveAllEntitiesResponse RetrieveEntitiesList()
        {
            var request = new RetrieveAllEntitiesRequest { EntityFilters = EntityFilters.Entity };
            return (RetrieveAllEntitiesResponse)service.Execute(request);
        }

        /// <summary>
        /// Obtains all users corresponding to the search filter
        /// </summary>
        /// <param name="value">Search filter</param>
        /// <returns>List of users matching the search filter</returns>
        public List<Entity> GetUsers(string value)
        {
            var users = new List<Entity>();

            var ceStatus = new ConditionExpression("isdisabled", ConditionOperator.Equal, false);

            var feStatus = new FilterExpression();
            feStatus.AddCondition(ceStatus);

            var qe = new QueryExpression("systemuser");
            qe.ColumnSet = new ColumnSet("systemuserid", "fullname", "lastname", "firstname", "domainname");
            qe.AddOrder("lastname", OrderType.Ascending);
            qe.Criteria = new FilterExpression();
            qe.Criteria.Filters.Add(new FilterExpression());
            qe.Criteria.Filters[0].FilterOperator = LogicalOperator.And;
            qe.Criteria.Filters[0].Filters.Add(feStatus);
            qe.Criteria.Filters[0].Filters.Add(new FilterExpression());
            qe.Criteria.Filters[0].Filters[1].FilterOperator = LogicalOperator.Or;

            if (value.Length > 0)
            {
                bool isGuid = false;

                try
                {
                    Guid g = new Guid(value);
                    isGuid = true;
                }
                catch
                { }
                
                if (isGuid)
                {
                    var ce = new ConditionExpression("systemuserid", ConditionOperator.Equal, value);
                    qe.Criteria.Filters[0].Filters[1].AddCondition(ce);
                }
                else
                {
                    var ce = new ConditionExpression("fullname", ConditionOperator.Like, value.Replace("*", "%"));
                    var ce2 = new ConditionExpression("firstname", ConditionOperator.Like, value.Replace("*", "%"));
                    var ce3 = new ConditionExpression("lastname", ConditionOperator.Like, value.Replace("*", "%"));

                    qe.Criteria.Filters[0].Filters[1].AddCondition(ce);
                    qe.Criteria.Filters[0].Filters[1].AddCondition(ce2);
                    qe.Criteria.Filters[0].Filters[1].AddCondition(ce3);
                }
            }

            foreach (var record in service.RetrieveMultiple(qe).Entities)
            {
                users.Add(record);
            }

            return users;
        }

        /// <summary>
        /// Retrieve the access rights for the specified user against the specified object
        /// </summary>
        /// <param name="userId">Unique identifier of the user</param>
        /// <param name="objectId">Unique identifier of the object</param>
        /// <param name="entityName">Logical name of the object entity</param>
        /// <returns>List of access rigths</returns>
        public RetrievePrincipalAccessResponse RetrieveRights(Guid userId, Guid objectId, string entityName)
        {
            try
            {
                // Requête d'accès
                var request = new RetrievePrincipalAccessRequest();
                request.Principal = new EntityReference("systemuser", userId);
                request.Target = new EntityReference(entityName, objectId);

                return (RetrievePrincipalAccessResponse)service.Execute(request);
            }
            catch (Exception error)
            {
                throw new Exception("Error while checking rigths: " + error.Message);
            }
        }

        /// <summary>
        /// Retrieve all privileges definition for the specified entity
        /// </summary>
        /// <param name="entityName">Entity logical name</param>
        /// <returns>List of privileges</returns>
        public Dictionary<string, Guid> RetrievePrivileges(string entityName)
        {
            var request = new RetrieveEntityRequest {LogicalName = entityName, EntityFilters = EntityFilters.Privileges};
            var response = (RetrieveEntityResponse)service.Execute(request);

            var privileges = new Dictionary<string, Guid>();

            foreach (SecurityPrivilegeMetadata spmd in response.EntityMetadata.Privileges)
            {
                privileges.Add(spmd.Name.ToLower(), spmd.PrivilegeId);
            }

            return privileges;
        }
    
        /// <summary>
        /// Retrieve the primary attribute value of the specified object
        /// </summary>
        /// <param name="recordId">Unique identifier of the object</param>
        /// <param name="entityName">Object entity logical name</param>
        /// <param name="primaryAttribute">Entity primary attribute logical name</param>
        /// <returns>Dynamic Entity containing the primary attribute value</returns>
        public Entity RetrieveDynamicWithPrimaryAttr(Guid recordId, string entityName, string primaryAttribute)
        {
            return service.Retrieve(entityName, recordId, new ColumnSet(primaryAttribute));
        }

        #endregion
    }
}
