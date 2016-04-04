// PROJECT : MsCrmTools.RoleUpdater
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MsCrmTools.RoleUpdater
{
    /// <summary>
    /// This class manages operation against security roles
    /// </summary>
    public class RoleManager
    {
        #region Variables

        /// <summary>
        /// Crm service
        /// </summary>
        private readonly IOrganizationService innerService;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class RoleManager
        /// </summary>
        public RoleManager(IOrganizationService service)
        {
            innerService = service;
        }

        #endregion Constructor

        #region Properties

        public List<Entity> Privileges { get; private set; }
        public List<Entity> Roles { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds list of privileges to each role in the specified list
        /// </summary>
        /// <param name="privileges">List of privileges to add</param>
        /// <param name="role">List of roles to update</param>
        public void AddPrivilegesToRole(List<RolePrivilege> privileges, Entity role)
        {
            bool hasChanged = false;

            Guid roleId = (Guid)role["roleid"];

            // Retrieve the privileges for the current security role
            var request = new RetrieveRolePrivilegesRoleRequest { RoleId = roleId };
            var response =
                (RetrieveRolePrivilegesRoleResponse)innerService.Execute(request);
            List<RolePrivilege> rolePrivileges = response.RolePrivileges.ToList();

            // If the privilege isn't already in the current security role
            // privileges, it is added
            foreach (RolePrivilege privilege in privileges)
            {
                var rp = rolePrivileges.Find(x => x.PrivilegeId == privilege.PrivilegeId);
                if (rp == null)
                {
                    rolePrivileges.Add(privilege);
                    hasChanged = true;
                }
                else if (rp.Depth != privilege.Depth)
                {
                    rolePrivileges[rolePrivileges.IndexOf(rp)] = privilege;
                    hasChanged = true;
                }
            }

            // If some privileges have been added, we can update the security role
            if (hasChanged)
            {
                var apRequest = new ReplacePrivilegesRoleRequest
                                    {
                                        RoleId = roleId,
                                        Privileges = rolePrivileges.ToArray()
                                    };

                innerService.Execute(apRequest);
            }
        }

        public void AddPrivilegeToRole(List<RolePrivilege> privileges, PrivilegeAction pAction)
        {
            var itemToUpdate = privileges.FirstOrDefault(rp => rp.PrivilegeId == pAction.PrivilegeId);

            if (itemToUpdate != null)
            {
                itemToUpdate.Depth = GetDepthFromLevel(pAction.Level);
            }
            else
            {
                itemToUpdate = new RolePrivilege
                                   {
                                       PrivilegeId = pAction.PrivilegeId,
                                       Depth = GetDepthFromLevel(pAction.Level)
                                   };

                privileges.Add(itemToUpdate);
            }
        }

        public void ApplyChanges(List<RolePrivilege> privileges, Guid roleId)
        {
            var apRequest = new ReplacePrivilegesRoleRequest
                                {
                                    RoleId = roleId,
                                    Privileges = privileges.ToArray()
                                };

            innerService.Execute(apRequest);
        }

        public List<RolePrivilege> GetPrivilegesForRole(Guid roleId)
        {
            var request = new RetrieveRolePrivilegesRoleRequest { RoleId = roleId };
            var response = (RetrieveRolePrivilegesRoleResponse)innerService.Execute(request);
            return response.RolePrivileges.ToList();
        }

        /// <summary>
        /// Obtains all privileges
        /// </summary>
        /// <returns></returns>
        public void LoadPrivileges()
        {
            var request = new RetrievePrivilegeSetRequest();
            var response = (RetrievePrivilegeSetResponse)innerService.Execute(request);

            Privileges = response.EntityCollection.Entities.ToList();
        }

        /// <summary>
        /// Obtains all roles that don't have parent role
        /// </summary>
        /// <returns>List of roles</returns>
        public void LoadRootRoles()
        {
            var qe = new QueryExpression("role") { ColumnSet = { AllColumns = true }, Criteria = new FilterExpression() };
            qe.Criteria.AddCondition(new ConditionExpression("parentroleid", ConditionOperator.Null));

            Roles = innerService.RetrieveMultiple(qe).Entities.ToList();
        }

        public void RemovePrivilegeFromRole(List<RolePrivilege> privileges, Guid privilegeToRemoveId)
        {
            var itemToRemove = privileges.FirstOrDefault(rp => rp.PrivilegeId == privilegeToRemoveId);

            if (itemToRemove != null)
            {
                privileges.Remove(itemToRemove);
            }
        }

        /// <summary>
        /// Removes list of privileges to each role in the specified list
        /// </summary>
        /// <param name="privileges">List of privileges to remove</param>
        /// <param name="role">List of roles to update</param>
        public void RemovePrivilegesToRole(List<RolePrivilege> privileges, Entity role)
        {
            Guid roleId = (Guid)role["roleid"];

            // Retrieve the privileges for the current security role
            var request = new RetrieveRolePrivilegesRoleRequest { RoleId = roleId };
            var response =
                (RetrieveRolePrivilegesRoleResponse)innerService.Execute(request);
            List<RolePrivilege> rolePrivileges = response.RolePrivileges.ToList();

            // If the privilege exists in the privileges set of the security role,
            // we remove it
            foreach (RolePrivilege privilege in privileges)
            {
                var request3 = new RemovePrivilegeRoleRequest
                                   {
                                       RoleId = roleId,
                                       PrivilegeId = privilege.PrivilegeId
                                   };

                innerService.Execute(request3);
            }
        }

        private PrivilegeDepth GetDepthFromLevel(PrivilegeLevel privilegeLevel)
        {
            switch (privilegeLevel)
            {
                case PrivilegeLevel.User:
                    return PrivilegeDepth.Basic;

                case PrivilegeLevel.BusinessUnit:
                    return PrivilegeDepth.Local;

                case PrivilegeLevel.ParentChildBusinessUnit:
                    return PrivilegeDepth.Deep;

                case PrivilegeLevel.Organization:
                    return PrivilegeDepth.Global;

                default:
                    throw new Exception("Unexpected Privilege level");
            }
        }

        #endregion Methods
    }
}