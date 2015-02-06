// PROJECT : MsCrmTools.AttributeBulkUpdater
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using XrmToolBox;

namespace MsCrmTools.AttributeBulkUpdater.Helpers
{
    /// <summary>
    /// Class for querying Crm Metadata 
    /// </summary>
    class MetadataHelper
    {
        /// <summary>
        /// Gets the list of entities metadata (only Entity Items)
        /// </summary>
        /// <returns>List of entities metadata</returns>
        public static List<EntityMetadata> RetrieveEntities(IOrganizationService oService)
        {
            List<EntityMetadata> entities = new List<EntityMetadata>();

            RetrieveAllEntitiesRequest request = new RetrieveAllEntitiesRequest
                                                     {
                EntityFilters = EntityFilters.Entity
            };

            RetrieveAllEntitiesResponse response = (RetrieveAllEntitiesResponse)oService.Execute(request);

            foreach (EntityMetadata emd in response.EntityMetadata)
            {
                if (emd.DisplayName.UserLocalizedLabel != null && (emd.IsCustomizable.Value || emd.IsManaged.Value == false))
                {
                    entities.Add(emd);
                }
            }

            return entities;
        }

        /// <summary>
        /// Gets specified entity metadata (include attributes)
        /// </summary>
        /// <param name="logicalName">Logical name of the entity</param>
        /// <param name="oService">Crm organization service</param>
        /// <returns>Entity metadata</returns>
        public static EntityMetadata RetrieveEntity(string logicalName, IOrganizationService oService)
        {
            try
            {
                RetrieveEntityRequest request = new RetrieveEntityRequest
                                                    {
                    LogicalName = logicalName,
                    EntityFilters = EntityFilters.Attributes,
                    RetrieveAsIfPublished = true
                };

                RetrieveEntityResponse response = (RetrieveEntityResponse)oService.Execute(request);

                return response.EntityMetadata;
            }
            catch (Exception error)
            {
                string errorMessage = CrmExceptionHelper.GetErrorMessage(error, false);
                throw new Exception("Error while retrieving entity: " + errorMessage);
            }
        }

        /// <summary>
        /// Retrieves main forms for the specified entity
        /// </summary>
        /// <param name="logicalName">Entity logical name</param>
        /// <param name="oService">Crm organization service</param>
        /// <returns>Document containing all forms definition</returns>
        public static XmlDocument RetrieveEntityForms(string logicalName, IOrganizationService oService)
        {
            QueryByAttribute qba = new QueryByAttribute("systemform");
            qba.Attributes.AddRange("objecttypecode", "type");
            qba.Values.AddRange(logicalName, 2);
            qba.ColumnSet = new ColumnSet(true);

            EntityCollection ec = oService.RetrieveMultiple(qba);

            StringBuilder allFormsXml = new StringBuilder();
            allFormsXml.Append("<root>");

            foreach (Entity form in ec.Entities)
            {
                allFormsXml.Append(form["formxml"]);
            }

            allFormsXml.Append("</root>");

            XmlDocument docAllForms = new XmlDocument();
            docAllForms.LoadXml(allFormsXml.ToString());

            return docAllForms;
        }
    }
}