// PROJECT : MsCrmTools.RoleUpdater
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System.Collections.Generic;
using System.Linq;

namespace MsCrmTools.RoleUpdater
{
    /// <summary>
    /// This class helps to retrieve metadata
    /// </summary>
    internal class MetadataHelper
    {
        /// <summary>
        /// Gets all entities metadata with specified informations
        /// </summary>
        /// <param name="service">Organization service</param>
        /// <param name="filters">Information to retrieve</param>
        /// <returns>List of entities metadata</returns>
        public static List<EntityMetadata> GetEntitiesMetadata(IOrganizationService service, EntityFilters filters)
        {
            var request = new RetrieveAllEntitiesRequest
            {
                EntityFilters = filters
            };

            var response = (RetrieveAllEntitiesResponse)service.Execute(request);

            return response.EntityMetadata.ToList();
        }
    }
}