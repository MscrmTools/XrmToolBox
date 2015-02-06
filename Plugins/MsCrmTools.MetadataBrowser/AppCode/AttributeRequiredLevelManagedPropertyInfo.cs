using System.ComponentModel;
using Microsoft.Xrm.Sdk.Metadata;

namespace MsCrmTools.MetadataBrowser.AppCode
{
    [TypeConverter(typeof(AttributeRequiredLevelManagedPropertyInfoConverter))]
    public class AttributeRequiredLevelManagedPropertyInfo
    {
        private readonly AttributeRequiredLevelManagedProperty property;

        public AttributeRequiredLevelManagedPropertyInfo(AttributeRequiredLevelManagedProperty property)
        {
            this.property = property;
        }

        public bool CanBeChanged
        {
            get { return property.CanBeChanged; }
        }

        public AttributeRequiredLevel Value
        {
            get { return property.Value; }
        }

        public string ManagedPropertyLogicalName
        {
            get { return property.ManagedPropertyLogicalName; }
        }
    }
}
