using Microsoft.Xrm.Sdk.Metadata;
using System.ComponentModel;

namespace MsCrmTools.MetadataBrowser.AppCode
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
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

        public string ManagedPropertyLogicalName
        {
            get { return property.ManagedPropertyLogicalName; }
        }

        public AttributeRequiredLevel Value
        {
            get { return property.Value; }
        }

        public override string ToString()
        {
            return string.Format("Value: {0} / Can be changed: {1}", Value, CanBeChanged);
        }
    }
}