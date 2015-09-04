// PROJECT : MsCrmTools.ViewLayoutReplicator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace MsCrmTools.ViewLayoutReplicator.Helpers
{
    /// <summary>
    /// Class for querying Crm Metadata
    /// </summary>
    internal class MetadataHelper
    {
        public static string RetrieveAttributeDisplayName(EntityMetadata emd, string attributeName, string fetchXml, IOrganizationService oService)
        {
            string rAttributeName = attributeName;
            string rEntityName = string.Empty;

            if (attributeName.Contains("."))
            {
                string[] data = attributeName.ToLower().Split('.');

                if (!string.IsNullOrEmpty(fetchXml))
                {
                    XmlDocument fetchDoc = new XmlDocument();
                    fetchDoc.LoadXml(fetchXml);

                    XmlNode aliasNode = fetchDoc.SelectSingleNode("//link-entity[@alias='" + data[0] + "']");
                    if (aliasNode != null)
                    {
                        data[0] = string.Format("{0}{1}{2}{3}",
                                                emd.LogicalName,
                                                aliasNode.Attributes["to"].Value,
                                                aliasNode.Attributes["name"].Value,
                                                aliasNode.Attributes["from"].Value);
                    }
                }

                foreach (OneToManyRelationshipMetadata otmmd in emd.ManyToOneRelationships)
                {
                    string referencing = otmmd.ReferencingEntity;
                    string attrreferencing = otmmd.ReferencingAttribute;
                    string referenced = otmmd.ReferencedEntity;
                    string attrreferenced = otmmd.ReferencedAttribute;

                    string name = referencing + attrreferencing + referenced + attrreferenced;

                    if (name == data[0])
                    {
                        rAttributeName = data[1];
                        rEntityName = referenced;
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(rEntityName) && !string.IsNullOrEmpty(rAttributeName))
                {
                    EntityMetadata relatedEmd = RetrieveEntity(rEntityName, oService);

                    AttributeMetadata relatedamd = (from attr in relatedEmd.Attributes
                                                    where attr.LogicalName == rAttributeName
                                                    select attr).First<AttributeMetadata>();

                    return relatedamd.DisplayName.UserLocalizedLabel.Label;
                }

                return string.Empty;
            }
            else
            {
                AttributeMetadata attribute = (from attr in emd.Attributes
                                               where attr.LogicalName == attributeName
                                               select attr).First<AttributeMetadata>();

                return attribute.DisplayName.UserLocalizedLabel.Label;
            }
        }

        /// <summary>
        /// Retrieve list of entities
        /// </summary>
        /// <returns></returns>
        public static List<EntityMetadata> RetrieveEntities(IOrganizationService oService)
        {
            List<EntityMetadata> entities = new List<EntityMetadata>();

            RetrieveAllEntitiesRequest request = new RetrieveAllEntitiesRequest
                                                     {
                                                         RetrieveAsIfPublished = true,
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

        public static EntityMetadata RetrieveEntity(string logicalName, IOrganizationService oService)
        {
            try
            {
                RetrieveEntityRequest request = new RetrieveEntityRequest
                                                    {
                                                        LogicalName = logicalName,
                                                        EntityFilters = EntityFilters.Attributes | EntityFilters.Relationships
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
    }
}