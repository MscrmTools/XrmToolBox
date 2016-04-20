using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;

namespace Gvw.CopyDynamicMarketingListToSavedQuery.Helpers
{
    internal class ListHelper
    {

        /// <summary>
        /// Retrieve the list of views for a specific entity
        /// </summary>
        /// <param name="entityDisplayName">Logical name of the entity</param>
        /// <param name="entitiesCache">Entities cache</param>
        /// <param name="service">Organization Service</param>
        /// <returns>List of dynamic marketing lists</returns>
        public static List<Entity> RetrieveLists(IOrganizationService service)
        {
            try
            {
                QueryByAttribute qba = new QueryByAttribute
                {
                    EntityName = "list",
                    ColumnSet = new ColumnSet(true)
                };

                qba.Attributes.Add("type");
                qba.Values.Add(true);

                EntityCollection lists = service.RetrieveMultiple(qba);

                List<Entity> results = new List<Entity>();

                foreach (Entity entity in lists.Entities)
                {
                    results.Add(entity);
                }

                return results;
            }
            catch (Exception error)
            {
                string errorMessage = CrmExceptionHelper.GetErrorMessage(error, false);
                throw new Exception("Error while retrieving lists: " + errorMessage);
            }
        }

        public static Entity RetrieveList(IOrganizationService service, Guid listId)
        {
            try
            {
                Entity result = service.Retrieve("list", listId, new ColumnSet(true));

                return result;
            }
            catch (Exception error)
            {
                string errorMessage = CrmExceptionHelper.GetErrorMessage(error, false);
                throw new Exception("Error while retrieving lists: " + errorMessage);
            }
        }
    }
}
