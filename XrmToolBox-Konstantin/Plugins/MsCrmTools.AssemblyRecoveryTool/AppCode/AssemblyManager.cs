using System.Collections.Generic;
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
            List<Entity> list = new List<Entity>();

            LinkEntity le = new LinkEntity();
            le.LinkFromAttributeName = "pluginassemblyid";
            le.LinkFromEntityName = "pluginassembly";
            le.LinkToAttributeName = "pluginassemblyid";
            le.LinkToEntityName = "plugintype";
            le.LinkCriteria = new FilterExpression();
            le.LinkCriteria.Conditions.AddRange(
                new ConditionExpression("typename", ConditionOperator.NotLike, "Microsoft.Crm.%"),
                new ConditionExpression("typename", ConditionOperator.NotLike, "Compiled.%"));

            QueryExpression qe = new QueryExpression();
            qe.EntityName = "pluginassembly";
            qe.ColumnSet = new ColumnSet(true);
            qe.LinkEntities.Add(le);
            qe.Distinct = false;

            RetrieveMultipleRequest request = new RetrieveMultipleRequest();
            request.Query = qe;

            RetrieveMultipleResponse response = (RetrieveMultipleResponse)service.Execute(request);

            EntityCollection bec = response.EntityCollection;// service.RetrieveMultiple(qe);

            foreach (Entity pAssembly in bec.Entities)
            {
                if (list.Find(x => x["publickeytoken"].ToString() == pAssembly["publickeytoken"].ToString() && x["name"].ToString() == pAssembly["name"].ToString()) == null)
                {
                    list.Add(pAssembly);
                }
            }

            return list;
        }

        #endregion
    }
}
