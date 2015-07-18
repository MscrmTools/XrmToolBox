using System.ComponentModel;
using Microsoft.Xrm.Sdk;

namespace MsCrmTools.MetadataBrowser.AppCode
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
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

        public override string ToString()
        {
            return string.Format("Value: {0} / Can be changed: {1}", Value, CanBeChanged);
        }
    }
}
