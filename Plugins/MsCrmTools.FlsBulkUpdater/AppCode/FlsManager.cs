using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Collections.Generic;
using System.Linq;

namespace MsCrmTools.FlsBulkUpdater.AppCode
{
    internal class FlsManager
    {
        private readonly IOrganizationService service;

        public FlsManager(IOrganizationService service)
        {
            this.service = service;
        }

        public List<SecureFieldInfo> LoadSecureFields()
        {
            var query = new QueryExpression("fieldpermission")
            {
                ColumnSet = new ColumnSet(true)
            };

            var fields = service.RetrieveMultiple(query).Entities.ToList();

            var fieldsInfos = new List<SecureFieldInfo>();

            foreach (var field in fields)
            {
                var fieldInfo =
                    fieldsInfos.FirstOrDefault(fi => fi.Entity == field.GetAttributeValue<string>("entityname")
                                                     && fi.Attribute ==
                                                     field.GetAttributeValue<string>("attributelogicalname"));

                if (fieldInfo == null)
                {
                    fieldInfo = new SecureFieldInfo
                    {
                        Attribute = field.GetAttributeValue<string>("attributelogicalname"),
                        Entity = field.GetAttributeValue<string>("entityname"),
                    };

                    fieldsInfos.Add(fieldInfo);
                }

                fieldInfo.Fields.Add(field);
            }

            return fieldsInfos;
        }

        public List<Entity> LoadSecureProfiles()
        {
            var query = new QueryExpression("fieldsecurityprofile")
            {
                ColumnSet = new ColumnSet(true),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("createdby", ConditionOperator.NotNull)
                    }
                }
            };

            return service.RetrieveMultiple(query).Entities.ToList();
        }

        public void Update(List<Entity> profiles, List<SecureFieldInfo> fields)
        {
        }
    }
}