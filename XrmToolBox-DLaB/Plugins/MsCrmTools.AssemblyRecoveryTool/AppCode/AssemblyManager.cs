using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;

namespace MsCrmTools.AssemblyRecoveryTool.AppCode
{
    /// <summary>
    /// Class that works with pluginassembly CRM items
    /// </summary>
    public class AssemblyManager
    {
        #region Variables

        /// <summary>
        /// Crm web service
        /// </summary>
        private IOrganizationService service;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class AssemblyManager
        /// </summary>
        /// <param name="service">Details of the connected user</param>
        public AssemblyManager(IOrganizationService service)
        {
            this.service = service;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Retrieve the assemblies for the specified irganization
        /// </summary>
        /// <returns>List of plugin assemblies</returns>
        public List<Entity> RetrieveAssemblies()
        {
            var list = new List<Entity>();

            var qe = new QueryExpression("pluginassembly")
            {
                ColumnSet = new ColumnSet(true),
                Distinct = false,
                LinkEntities =
                {
                    new LinkEntity
                    {
                        LinkFromAttributeName = "pluginassemblyid",
                        LinkFromEntityName = "pluginassembly",
                        LinkToAttributeName = "pluginassemblyid",
                        LinkToEntityName = "plugintype",
                        LinkCriteria = new FilterExpression
                        {
                            Conditions =
                            {
                                new ConditionExpression("typename", ConditionOperator.NotLike, "Microsoft.Crm.%"),
                                new ConditionExpression("typename", ConditionOperator.NotLike, "Compiled.%")
                            }
                        }
                    }
                }
            };

            var response = (RetrieveMultipleResponse) service.Execute(new RetrieveMultipleRequest {Query = qe});

            foreach (Entity pAssembly in response.EntityCollection.Entities.Where(pAssembly => list.Find(x => x["publickeytoken"].ToString() == pAssembly["publickeytoken"].ToString() && x["name"].ToString() == pAssembly["name"].ToString()) == null))
            {
                list.Add(pAssembly);
            }

            return list;
        }

        #endregion
    }
}
