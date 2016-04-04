using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MsCrmTools.FlsBulkUpdater.AppCode
{
    internal class SecureFieldInfo
    {
        public SecureFieldInfo()
        {
            Fields = new List<Entity>();
        }

        public string Attribute { get; set; }
        public bool? CanCreate { get; set; }
        public bool? CanRead { get; set; }
        public bool? CanUpdate { get; set; }

        public string Entity { get; set; }
        public List<Entity> Fields { get; set; }

        public void Update(IOrganizationService service, List<Entity> profiles)
        {
            foreach (var profile in profiles)
            {
                var field = Fields.FirstOrDefault(
                        f => f.GetAttributeValue<EntityReference>("fieldsecurityprofileid").Id == profile.Id);

                if (field == null)
                {
                    field = new Entity("fieldpermission");
                    field["fieldsecurityprofileid"] = profile.ToEntityReference();
                    field["canread"] = new OptionSetValue(0);
                    field["cancreate"] = new OptionSetValue(0);
                    field["canupdate"] = new OptionSetValue(0);
                    field["entityname"] = Entity;
                    field["attributelogicalname"] = Attribute;
                }

                if (CanRead.HasValue)
                {
                    field["canread"] = new OptionSetValue(CanRead.Value ? 4 : 0);
                }

                if (CanCreate.HasValue)
                {
                    field["cancreate"] = new OptionSetValue(CanCreate.Value ? 4 : 0);
                }

                if (CanUpdate.HasValue)
                {
                    field["canupdate"] = new OptionSetValue(CanUpdate.Value ? 4 : 0);
                }

                if (field.Id == Guid.Empty)
                {
                    field.Id = service.Create(field);
                    Fields.Add(field);
                }
                else
                {
                    service.Update(field);
                }
            }
        }
    }
}