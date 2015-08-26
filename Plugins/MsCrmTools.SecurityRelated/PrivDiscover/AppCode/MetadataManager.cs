using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace MsCrmTools.PrivDiscover.AppCode
{
    internal class MetadataManager
    {
        private readonly IOrganizationService service;

        public MetadataManager(IOrganizationService service)
        {
            this.service = service;
        }

        public List<EntityMetadata> GetEntitiesWithPrivileges()
        {
            var request = new RetrieveAllEntitiesRequest
                              {
                                  EntityFilters =
                                      EntityFilters.Entity | EntityFilters.Privileges
                              };
            var response = (RetrieveAllEntitiesResponse) service.Execute(request);

            return response.EntityMetadata.ToList();
        }
    }
}
