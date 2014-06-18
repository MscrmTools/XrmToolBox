// PROJECT : MsCrmTools.ViewLayoutReplicator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using McTools.Xrm.Connection;

namespace MsCrmTools.ViewLayoutReplicator.Helpers
{
    /// <summary>
    /// Helps to interact with Crm views
    /// </summary>
    class ViewHelper
    {
        #region Constants

        public const int VIEW_BASIC = 0;
        public const int VIEW_ADVANCEDFIND = 1;
        public const int VIEW_ASSOCIATED = 2;
        public const int VIEW_QUICKFIND = 4;
        public const int VIEW_SEARCH = 64;

        #endregion

        /// <summary>
        /// Retrieve the list of views for a specific entity
        /// </summary>
        /// <param name="entityDisplayName">Logical name of the entity</param>
        /// <param name="entitiesCache">Entities cache</param>
        /// <param name="service">Organization Service</param>
        /// <returns>List of views</returns>
        public static List<Entity> RetrieveViews(string entityLogicalName, List<EntityMetadata> entitiesCache, IOrganizationService service)
        {
            try
            {
                EntityMetadata currentEmd = entitiesCache.Find(delegate(EntityMetadata emd) { return emd.LogicalName == entityLogicalName; });

                QueryByAttribute qba = new QueryByAttribute
                                           {
                                               EntityName = "savedquery",
                                               ColumnSet = new ColumnSet(true)
                                           };

                qba.Attributes.Add("returnedtypecode");
                qba.Values.Add(currentEmd.ObjectTypeCode.Value);

                EntityCollection views = service.RetrieveMultiple(qba);

                List<Entity> viewsList = new List<Entity>();

                foreach (Entity entity in views.Entities)
                {
                    viewsList.Add(entity);
                }

                return viewsList;
            }
            catch (Exception error)
            {
                string errorMessage = CrmExceptionHelper.GetErrorMessage(error, false);
                throw new Exception("Error while retrieving views: " + errorMessage);
            }
        }

        internal static IEnumerable<Entity> RetrieveUserViews(string entityLogicalName, List<EntityMetadata> entitiesCache, IOrganizationService service)
        {
            try
            {
                EntityMetadata currentEmd = entitiesCache.Find(e => e.LogicalName == entityLogicalName);

                QueryByAttribute qba = new QueryByAttribute
                {
                    EntityName = "userquery",
                    ColumnSet = new ColumnSet(true)
                };

                qba.Attributes.AddRange("returnedtypecode", "querytype");
                qba.Values.AddRange(currentEmd.ObjectTypeCode.Value, 0);

                EntityCollection views = service.RetrieveMultiple(qba);

                List<Entity> viewsList = new List<Entity>();

                foreach (Entity entity in views.Entities)
                {
                    viewsList.Add(entity);
                }

                return viewsList;
            }
            catch (Exception error)
            {
                string errorMessage = CrmExceptionHelper.GetErrorMessage(error, false);
                throw new Exception("Error while retrieving user views: " + errorMessage);
            }
        }

        public static List<Tuple<string, string>> TransferViews(List<Entity> sourceViews, IOrganizationService targetService, EntityMetadata savedQueryMetadata)
        {
            var errors = new List<Tuple<string, string>>();
            try
            {
                foreach (var sourceView in sourceViews)
                {
                    var targetViewQuery = new QueryExpression(sourceView.LogicalName);
                    targetViewQuery.ColumnSet = new ColumnSet { AllColumns = true };
                    targetViewQuery.Criteria.AddCondition("savedqueryid", ConditionOperator.Equal, sourceView.Id);
                    var targetViews = targetService.RetrieveMultiple(targetViewQuery);

                    if (targetViews.Entities.Count > 0)
                    {
                        // We need to update the existing view
                        var targetView = CleanEntityForUpdate(savedQueryMetadata, sourceView);

                        try
                        {
                            targetService.Update(targetView);
                        }
                        catch (Exception error)
                        {
                            errors.Add(new Tuple<string, string>(targetView["name"].ToString(), error.Message));
                        }
                    }
                    else
                    {
                        // We need to create the view
                        var targetView = CleanEntityForCreate(savedQueryMetadata, sourceView);

                        try
                        {
                            targetService.Create(targetView);
                        }
                        catch (Exception error)
                        {
                            errors.Add(new Tuple<string, string>(sourceView["name"].ToString(), error.Message));
                        }
                    }
                }
                return errors;
            }
            catch (Exception error)
            {
                string errorMessage = CrmExceptionHelper.GetErrorMessage(error, false);

                throw new Exception("Error while transfering views: " + errorMessage);
            }
        }

        #region Private methods

        private static Entity CleanEntityForCreate(EntityMetadata metadata, Entity recordToCreate)
        {
            foreach (var attribute in metadata.Attributes)
            {
                if (!attribute.IsValidForCreate.Value && recordToCreate.Contains(attribute.LogicalName))
                {
                    recordToCreate.Attributes.Remove(attribute.LogicalName);
                }
            }
            return recordToCreate;
        }

        private static Entity CleanEntityForUpdate(EntityMetadata metadata, Entity recordToUpdate)
        {
            foreach (var attribute in metadata.Attributes)
            {
                if (!attribute.IsValidForUpdate.Value && recordToUpdate.Contains(attribute.LogicalName))
                {
                    recordToUpdate.Attributes.Remove(attribute.LogicalName);
                }
            }
            return recordToUpdate;
        }

        #endregion
    }
}
