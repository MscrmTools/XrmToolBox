using System.ComponentModel;
using Microsoft.Xrm.Sdk;

namespace MsCrmTools.MetadataBrowser.AppCode
{
    [TypeConverter(typeof(BooleanManagedPropertyInfoConverter))]
    public class BooleanManagedPropertyInfo
    {
        private readonly BooleanManagedProperty property;

        public BooleanManagedPropertyInfo(BooleanManagedProperty property)
        {
            this.property = property;
        }

        public bool CanBeChanged
        {
            get { return property.CanBeChanged; }
        }

        public bool Value
        {
            get { return property.Value; }
        }

        public string ManagedPropertyLogicalName
        {
            get { return property.ManagedPropertyLogicalName; }
        }
    }
}
