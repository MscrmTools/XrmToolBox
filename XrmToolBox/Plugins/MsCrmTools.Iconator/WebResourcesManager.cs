// PROJECT : MsCrmTools.Iconator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Drawing;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace MsCrmTools.Iconator
{
    public static class WebResourcesManager
    {
        /// <summary>
        /// Class representant l'objet web ressource
        /// </summary>
        public class WebResource
        {
            public string DisplayName { get; set; }
            public Guid WebResourceId { get; set; }
        }

        public class WebResourceAndImage
        {
            public Entity Webresource { get; set; }
            public Image Image { get; set; }
        };

        /// <summary>
        /// Recupere la liste des web resources d'un solution ciblee
        /// </summary>
        /// <param name="service">CRM Service</param>
        /// <returns>Liste des web resources retrouvees</returns>
        public static EntityCollection GetWebResourcesOnSolution(IOrganizationService service)
        {

            var queryWr = new QueryExpression
                              {
                                  EntityName = "webresource",
                                  ColumnSet = new ColumnSet("name", "webresourcetype", "displayname", "content"),
                                  Criteria = new FilterExpression()
                              };

            queryWr.Criteria.AddCondition("webresourcetype", ConditionOperator.In, new object[] { 5, 6, 7 });

            return service.RetrieveMultiple(queryWr);
        }

    }
}
