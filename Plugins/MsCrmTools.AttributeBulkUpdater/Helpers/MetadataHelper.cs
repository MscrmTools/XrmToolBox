﻿// PROJECT : MsCrmTools.AttributeBulkUpdater
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CrmExceptionHelper = XrmToolBox.CrmExceptionHelper;

namespace MsCrmTools.AttributeBulkUpdater.Helpers
{
    /// <summary>
    /// Class for querying Crm Metadata
    /// </summary>
    internal class MetadataHelper
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
        public static XmlDocument RetrieveEntityForms(string logicalName, IOrganizationService oService, ConnectionDetail detail)
        {
            var qe = new QueryExpression("systemform")
            {
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("objecttypecode", ConditionOperator.Equal, logicalName),
                        new ConditionExpression("type", ConditionOperator.In, new[] {2, 7}),
                    }
                },
                ColumnSet = new ColumnSet(true)
            };

            if (detail.OrganizationMajorVersion > 5)
            {
                qe.Criteria.Conditions.Add(new ConditionExpression("formactivationstate", ConditionOperator.Equal, 1));
            }

            EntityCollection ec = oService.RetrieveMultiple(qe);

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