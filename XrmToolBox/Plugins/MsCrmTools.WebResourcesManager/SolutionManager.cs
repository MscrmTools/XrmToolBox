// PROJECT : MsCrmTools.WebResourcesManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace MsCrmTools.WebResourcesManager
{
    class SolutionManager
    {
        #region Variables

        /// <summary>
        /// Organization service
        /// </summary>
        readonly IOrganizationService service;

        /// <summary>
        /// Current retrieved solution
        /// </summary>
        Entity currentSolution;

       #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of class SolutionManager
        /// </summary>
        /// <param name="organizationService">Organization service</param>
        public SolutionManager(IOrganizationService organizationService)
        {
            service = organizationService;
        }

        #endregion

        #region Properties

        public Entity CurrentSolution
        {
            get { return currentSolution; }
            set { currentSolution = value; }
        }

        #endregion

        public void AddComponent(int type, Guid id)
        {
            try
            {
                // Adding Component to server solution
                var request = new AddSolutionComponentRequest
                {
                    ComponentType = type,
                    SolutionUniqueName = currentSolution["uniquename"].ToString(),
                    ComponentId = id
                };

                service.Execute(request);
            }
            catch (Exception error)
            {
                throw new Exception("Error while adding solution component: " + error.Message);
            }
        }

        public string GetSolutionPrefix(Guid publisherId)
        {
            Entity publisher = service.Retrieve("publisher", publisherId, new ColumnSet(true));

            return publisher["customizationprefix"].ToString();
        }
    }
}
