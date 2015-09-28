// PROJECT : MsCrmTools.WebResourcesManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace MsCrmTools.WebResourcesManager
{
    internal class SolutionManager
    {
        #region Variables

        /// <summary>
        /// Organization service
        /// </summary>
        private readonly IOrganizationService service;

        /// <summary>
        /// Current retrieved solution
        /// </summary>
        private Entity currentSolution;

        #endregion Variables

        #region Constructors

        /// <summary>
        /// Initializes a new instance of class SolutionManager
        /// </summary>
        /// <param name="organizationService">Organization service</param>
        public SolutionManager(IOrganizationService organizationService)
        {
            service = organizationService;
        }

        #endregion Constructors

        #region Properties

        public Entity CurrentSolution
        {
            get { return currentSolution; }
            set { currentSolution = value; }
        }

        #endregion Properties

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