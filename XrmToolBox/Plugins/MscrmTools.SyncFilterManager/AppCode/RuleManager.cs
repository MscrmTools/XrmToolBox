﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using MscrmTools.SyncFilterManager.Forms;

namespace MscrmTools.SyncFilterManager.AppCode
{
    class RuleManager
    {
        private readonly string entityName;

        private readonly IOrganizationService service;

        public RuleManager(string entityName, IOrganizationService service)
        {
            this.entityName = entityName;
            this.service = service;
        }

        public EntityCollection GetRules(int[] ruleTypes, List<Entity> usersToReturn = null, string expectedReturnedType= null, BackgroundWorker worker = null)
        {
            if (entityName == "userquery")
            {
                var rules = new EntityCollection();
                
                if (usersToReturn == null)
                {
                    if (worker != null && worker.WorkerReportsProgress)
                    {
                        worker.ReportProgress(0, "Retrieving active users...");
                    }

                    var userQuery = new QueryExpression("systemuser")
                    {
                        Distinct = true,
                        ColumnSet = new ColumnSet(new []{"fullname","systemuserid"}),
                        Orders =
                        {
                            new OrderExpression("fullname", OrderType.Ascending)
                        },
                        Criteria = new FilterExpression
                        {
                            Conditions =
                            {
                                new ConditionExpression("isdisabled", ConditionOperator.Equal, false)
                            }
                        }
                    };

                    var users = service.RetrieveMultiple(userQuery);
                    usersToReturn = users.Entities.ToList();
                }

                foreach (var user in usersToReturn)
                {
                    if (worker != null && worker.WorkerReportsProgress)
                    {
                        worker.ReportProgress(0, string.Format("Retrieving offline filters for user '{0}'...", user.GetAttributeValue<string>("fullname")));
                    }

                    var qe = new QueryExpression(entityName)
                    {
                        ColumnSet = new ColumnSet(true),
                        Criteria = new FilterExpression
                        {
                            Conditions =
                            {
                                new ConditionExpression("querytype", ConditionOperator.Equal, ruleTypes[0]),
                                new ConditionExpression("ownerid", ConditionOperator.Equal, user.Id)
                            }
                        }
                    };

                    var userRules = service.RetrieveMultiple(qe);
                    rules.Entities.AddRange(userRules.Entities);

                    if (worker != null && worker.WorkerReportsProgress)
                    {
                        worker.ReportProgress(0, string.Format("Retrieving outlook filters for user '{0}'...", user.GetAttributeValue<string>("fullname")));
                    }

                    qe = new QueryExpression(entityName)
                    {
                        ColumnSet = new ColumnSet(true),
                        Criteria = new FilterExpression
                        {
                            Conditions =
                            {
                                new ConditionExpression("querytype", ConditionOperator.Equal, ruleTypes[1]),
                                new ConditionExpression("ownerid", ConditionOperator.Equal, user.Id)
                            }
                        }
                    };

                    userRules = service.RetrieveMultiple(qe);
                    rules.Entities.AddRange(userRules.Entities);
                }

                return rules;
            }
            else
            {
                var qe = new QueryExpression(entityName)
                {
                    ColumnSet = new ColumnSet(true),
                    Criteria = new FilterExpression
                    {
                        Conditions =
                        {
                            new ConditionExpression("querytype", ConditionOperator.In, ruleTypes)
                        }
                    }
                };

                if(!string.IsNullOrEmpty(expectedReturnedType))
                    qe.Criteria.AddCondition("returnedtypecode", ConditionOperator.Equal, expectedReturnedType);

                return service.RetrieveMultiple(qe);
            }
        }

        public void EnableRule(Guid ruleId)
        {
            var setStateRequest = new SetStateRequest
            {
                EntityMoniker = new EntityReference(entityName, ruleId),
                State = new OptionSetValue(0),
                Status = new OptionSetValue(-1)
            };

            service.Execute(setStateRequest);
        }

        public void DisableRule(Guid ruleId)
        {
            var setStateRequest = new SetStateRequest
            {
                EntityMoniker = new EntityReference(entityName, ruleId),
                State = new OptionSetValue(1),
                Status = new OptionSetValue(-1)
            };

            service.Execute(setStateRequest);
        }

        public void DeleteRule(Guid ruleId)
        {
            service.Delete(entityName, ruleId);
        }

        public List<Guid> CreateRuleFromSystemView(List<Entity> systemViews, int templateType, BackgroundWorker worker = null)
        {
            if (systemViews.Count == 0)
                return new List<Guid>();

            var rulesIds = new List<Guid>();

            foreach (var systemView in systemViews)
            {
                var rule = new Entity(entityName);
                rule["returnedtypecode"] = systemView.GetAttributeValue<string>("returnedtypecode");
                rule["name"] = systemView.GetAttributeValue<string>("name");
                rule["description"] = systemView.GetAttributeValue<string>("description");
                rule["querytype"] = templateType;
                rule["isdefault"] = false;
                rule["fetchxml"] = systemView.GetAttributeValue<string>("fetchxml");
                rule["layoutxml"] = systemView.GetAttributeValue<string>("layoutxml");

                rulesIds.Add(service.Create(rule));
            }

            return rulesIds;

           
        }

        public void ApplyRulesToUser(EntityReferenceCollection ec, Guid userId)
        {
            var request = new InstantiateFiltersRequest
            {
                UserId = userId,
                TemplateCollection = ec
            };

            service.Execute(request);
        }

        public void ApplyRuleToActiveUsers(EntityReferenceCollection ec)
        {
            var qba = new QueryByAttribute("systemuser");
            qba.AddAttributeValue("isdisabled", false);
            foreach (var user in service.RetrieveMultiple(qba).Entities)
            {
                var request = new InstantiateFiltersRequest
                {
                    UserId = user.Id,
                    TemplateCollection = ec
                };

                service.Execute(request);
            }
        }

        public void UpdateRuleFromSystemView(Entity systemView, Entity rule, BackgroundWorker worker = null)
        {
            var ruleToUpdate = new Entity("savedquery") {Id = rule.Id};
            ruleToUpdate["fetchxml"] = systemView.GetAttributeValue<string>("fetchxml");

            service.Update(ruleToUpdate);

            if (worker != null && worker.WorkerReportsProgress)
            {
                worker.ReportProgress(0, string.Format("Publishing entity '{0}'...", rule.GetAttributeValue<string>("returnedtypecode")));
            }

            var request = new PublishXmlRequest { ParameterXml = String.Format("<importexportxml><entities><entity>{0}</entity></entities></importexportxml>", rule.GetAttributeValue<string>("returnedtypecode")) };
            service.Execute(request);
        }

        public void ResetUsersRulesFromDefault(List<Entity> users, BackgroundWorker worker = null)
        {
            var currentId = ((OrganizationServiceProxy)(((OrganizationService)service).InnerService)).CallerId;

            foreach (var user in users)
            {
                if (worker != null)
                {
                    worker.ReportProgress(0, "Reseting filters for user \"" + user.GetAttributeValue<string>("fullname") + "\"");
                }

                ((OrganizationServiceProxy)(((OrganizationService)service).InnerService)).CallerId = user.Id;

                var request = new ResetUserFiltersRequest {QueryType = 16};
                service.Execute(request);

                request = new ResetUserFiltersRequest { QueryType = 256 };
                service.Execute(request);
            }

            ((OrganizationServiceProxy)(((OrganizationService)service).InnerService)).CallerId = currentId;
        }

        private void RemoveAllRulesForUser(Guid userId)
        {
            var currentId = ((OrganizationServiceProxy)(((OrganizationService)service).InnerService)).CallerId;
            ((OrganizationServiceProxy)(((OrganizationService)service).InnerService)).CallerId = userId;

            var rules = GetRules(new[] { 16, 256 }, new List<Entity> { new Entity("systemuser") { Id = userId } });

            foreach (var rule in rules.Entities)
            {
                service.Delete(rule.LogicalName, rule.Id);
            }

            ((OrganizationServiceProxy)(((OrganizationService)service).InnerService)).CallerId = currentId;
        }

        public void AddRulesFromUser(Entity sourceUser, List<Entity> users, BackgroundWorker worker = null)
        {
            // Retrieving user filter metadata
            var emd = (RetrieveEntityResponse)
                service.Execute(new RetrieveEntityRequest
                {
                    EntityFilters = EntityFilters.Attributes,
                    LogicalName = "userquery"
                });

            if (worker != null && worker.WorkerReportsProgress)
            {
                worker.ReportProgress(0, "Loading source user synchronization filters...");
            }

            // Retrieve filters for source user
            var rules = GetRules(new[] { 16, 256 }, new List<Entity> { new Entity("systemuser") { Id = sourceUser.Id } });

            foreach (var targetUser in users)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress(0, "Removing filters from user " + targetUser.GetAttributeValue<string>("fullname") + "...");
                }

                // Remove existing rules
                RemoveAllRulesForUser(targetUser.Id);

                //ApplyRulesToUser(new EntityReferenceCollection(rules.Entities.Where(e=>e.GetAttributeValue<EntityReference>("parentqueryid") != null).Select(e=>e.GetAttributeValue<EntityReference>("parentqueryid")).ToList()), targetUserId);


                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress(0, "Adding filters to user " + targetUser.GetAttributeValue<string>("fullname") + "...");
                }

                // Add source rules to target user
                foreach (var rule in rules.Entities)
                {
                    rule.Id = Guid.Empty;
                    rule.Attributes.Remove("userqueryid");
                    rule["ownerid"] = new EntityReference("systemuser", targetUser.Id);
                    foreach (var amd in emd.EntityMetadata.Attributes.Where(a => a.IsValidForCreate.Value == false))
                    {
                        rule.Attributes.Remove(amd.LogicalName);
                    }

                    service.Create(rule);
                }
            }
        }
    }
}
