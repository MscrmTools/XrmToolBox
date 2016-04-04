using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace MsCrmTools.UserRolesManager.AppCode
{
    internal class RoleManager
    {
        private readonly IOrganizationService service;

        public RoleManager(IOrganizationService service)
        {
            this.service = service;
        }

        public List<Entity> GetRoles()
        {
            return service.RetrieveMultiple(new QueryExpression("role") { ColumnSet = new ColumnSet(true) }).Entities.ToList();
        }

        public Guid GetRootBusinessUnitId()
        {
            return service.RetrieveMultiple(new QueryExpression("businessunit")
            {
                Criteria = new FilterExpression
                    {
                        Conditions =
                {
                    new ConditionExpression("parentbusinessunitid", ConditionOperator.Null)
                }
                    }
            }).Entities.First().Id;
        }

        public void AddRolesToPrincipals(List<Entity> roles, List<Entity> principals, List<Entity> allRoles, BackgroundWorker worker = null)
        {
            int total = principals.Count * roles.Count;
            int current = 0;

            foreach (var principal in principals)
            {
                foreach (var role in roles)
                {
                    if (worker != null && worker.WorkerReportsProgress)
                    {
                        worker.ReportProgress(current * 100 / total, "Adding roles to principals ({0} %)...");
                    }

                    var roleToUse = role;
                    if (allRoles != null)
                    {
                        var currentPrincipalBuId = principal.GetAttributeValue<EntityReference>("businessunitid").Id;
                        if (role.GetAttributeValue<EntityReference>("businessunitid").Id != currentPrincipalBuId)
                        {
                            roleToUse = GetRoleRecursive(currentPrincipalBuId, new List<Entity> { role }, allRoles);
                        }
                    }

                    try
                    {
                        service.Associate(
                             principal.LogicalName,
                             principal.Id,
                             new Relationship(principal.LogicalName + "roles_association"),
                             new EntityReferenceCollection { roleToUse.ToEntityReference() });
                    }
                    catch (Exception error)
                    {

                    }

                    current++;
                }
                //service.Associate(
                //    principal.LogicalName,
                //    principal.Id,
                //    new Relationship(principal.LogicalName + "roles_association"),
                //    new EntityReferenceCollection(roles.Select(r => r.ToEntityReference()).ToList()));
            }
        }

        public void RemoveRolesFromPrincipals(List<Entity> roles, List<Entity> principals, List<Entity> allRoles, BackgroundWorker worker = null)
        {
            int total = principals.Count * roles.Count;
            int current = 0;

            foreach (var principal in principals)
            {
                foreach (var role in roles)
                {
                    if (worker != null && worker.WorkerReportsProgress)
                    {
                        worker.ReportProgress(current * 100 / total, "Removing roles from principals ({0} %)...");
                    }

                    var roleToUse = role;

                    if (allRoles != null)
                    {
                        var currentPrincipalBuId = principal.GetAttributeValue<EntityReference>("businessunitid").Id;
                        if (role.GetAttributeValue<EntityReference>("businessunitid").Id != currentPrincipalBuId)
                        {
                            roleToUse = GetRoleRecursive(currentPrincipalBuId, new List<Entity> { role }, allRoles);
                        }
                    }

                    try
                    {
                        service.Disassociate(
                            principal.LogicalName,
                            principal.Id,
                            new Relationship(principal.LogicalName + "roles_association"),
                            new EntityReferenceCollection { roleToUse.ToEntityReference() });
                    }
                    catch (Exception error)
                    {

                    }

                    current++;
                }
                //service.Disassociate(
                //    principal.LogicalName,
                //    principal.Id,
                //    new Relationship(principal.LogicalName + "roles_association"),
                //    new EntityReferenceCollection(roles.Select(r => r.ToEntityReference()).ToList()));
            }
        }

        public void RemoveExistingRolesFromPrincipals(List<Entity> principals, BackgroundWorker worker = null)
        {
            if (principals.First().LogicalName == "systemuser")
            {
                var links = service.RetrieveMultiple(new QueryExpression("systemuserroles")
                {
                    ColumnSet = new ColumnSet(true),
                    Criteria = new FilterExpression
                    {
                        Conditions =
                        {
                            new ConditionExpression("systemuserid", ConditionOperator.In, principals.Select(p => p.Id).ToArray())
                        }
                    },
                });

                int total = links.Entities.Count;
                int current = 0;

                foreach (var link in links.Entities)
                {
                    if (worker != null && worker.WorkerReportsProgress)
                    {
                        worker.ReportProgress(current * 100 / total, "Removing roles from principals ({0} %)...");
                    }

                    RemoveRolesFromPrincipals(new List<Entity>
                    {
                        new Entity("role") {Id = link.GetAttributeValue<Guid>("roleid")}
                    },
                        new List<Entity>
                        {
                            new Entity("systemuser") {Id = link.GetAttributeValue<Guid>("systemuserid")}
                        },
                        null,
                        worker);

                    current++;
                }
            }
            else
            {
                var links = service.RetrieveMultiple(new QueryExpression("teamroles")
                {
                    ColumnSet = new ColumnSet(true),
                    Criteria = new FilterExpression
                    {
                        Conditions =
                        {
                            new ConditionExpression("teamid", ConditionOperator.In, principals.Select(p => p.Id).ToArray())
                        }
                    }
                });

                int total = links.Entities.Count;
                int current = 0;

                foreach (var link in links.Entities)
                {
                    if (worker != null && worker.WorkerReportsProgress)
                    {
                        worker.ReportProgress(current * 100 / total, "Removing roles from principals ({0} %)...");
                    }

                    RemoveRolesFromPrincipals(new List<Entity>
                    {
                        new Entity("role") {Id = link.GetAttributeValue<Guid>("roleid")}
                    },
                        new List<Entity>
                        {
                            new Entity("team") {Id = link.GetAttributeValue<Guid>("teamid")}
                        },
                        null,
                        worker);

                    current++;
                }
            }
        }

        Entity GetRoleRecursive(Guid buId, List<Entity> roles, List<Entity> allRoles)
        {
            if (roles == null || allRoles == null)
            {
                return null;
            }

            foreach (var role in roles)
            {
                if (role.GetAttributeValue<EntityReference>("businessunitid").Id == buId)
                {
                    return role;
                }

                var childRole = GetRoleRecursive(buId,
                       allRoles.Where(a => a.GetAttributeValue<EntityReference>("parentroleid") != null && a.GetAttributeValue<EntityReference>("parentroleid").Id == role.Id).ToList(),
                       allRoles);

                if (childRole != null)
                {
                    return childRole;
                }
            }

            return null;
        }
    }
}
