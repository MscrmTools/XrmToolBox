using System;
using System.ComponentModel;
using System.Globalization;
using MsCrmTools.MetadataBrowser.AppCode.AttributeMd;
using MsCrmTools.MetadataBrowser.AppCode.LabelMd;
using MsCrmTools.MetadataBrowser.AppCode.LocalizedLabelMd;
using MsCrmTools.MetadataBrowser.AppCode.ManyToManyRelationship;
using MsCrmTools.MetadataBrowser.AppCode.OneToManyRelationship;
using MsCrmTools.MetadataBrowser.AppCode.OptionMd;
using MsCrmTools.MetadataBrowser.AppCode.OptionSetMd;
using MsCrmTools.MetadataBrowser.AppCode.SecurityPrivilege;

namespace MsCrmTools.MetadataBrowser.AppCode
{
    internal class OneToManyRelationshipMetadataInfoConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destType)
        {
            if (destType == typeof (string) && value is OneToManyRelationshipMetadataInfo)
            {
                // Cast the value to an Employee type
                var emp = (OneToManyRelationshipMetadataInfo) value;


                return string.Format("{0} - {1}", emp.ReferencedEntity, emp.ReferencingEntity);
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

    internal class OneToManyRelationshipCollectionConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destType)
        {
            if (destType == typeof (string) && value is OneToManyRelationshipCollection)
            {
                return "Expand to see items";
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

    internal class ManyToManyRelationshipMetadataInfoConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destType)
        {
            if (destType == typeof (string) && value is ManyToManyRelationshipMetadataInfo)
            {
                // Cast the value to an Employee type
                var emp = (ManyToManyRelationshipMetadataInfo) value;


                return string.Format("{0} - {1}", emp.Entity1LogicalName, emp.Entity2LogicalName);
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

    internal class ManyToManyRelationshipCollectionConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destType)
        {
            if (destType == typeof (string) && value is ManyToManyRelationshipCollection)
            {
                return "Expand to see items";
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

    internal class SecurityPrivilegeInfoConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destType)
        {
            if (destType == typeof (string) && value is SecurityPrivilegeInfo)
            {
                // Cast the value to an Employee type
                var emp = (SecurityPrivilegeInfo) value;


                return emp.Name;
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

    internal class SecurityPrivilegeCollectionConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destType)
        {
            if (destType == typeof (string) && value is SecurityPrivilegeCollection)
            {
                return "Expand to see items";
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

    internal class AttributeMetadataInfoConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destType)
        {
            if (destType == typeof (string) && value is AttributeMetadataInfo)
            {
                // Cast the value to an Employee type
                var emp = (AttributeMetadataInfo) value;


                return emp.AttributeType.ToString();
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

    internal class AttributeMetadataCollectionConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destType)
        {
            if (destType == typeof (string) && value is AttributeMetadataCollection)
            {
                return "Expand to see items";
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

    internal class OptionSetAttributeMetadataInfoConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destType)
        {
            if (destType == typeof (string) && value is OptionSetMetadataInfo)
            {
                // Cast the value to an Employee type
                var emp = (OptionSetMetadataInfo) value;


                return emp.Name;
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

    internal class OptionSetMetadataCollectionConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destType)
        {
            if (destType == typeof (string) && value is OptionSetMetadataCollection)
            {
                return "Expand to see items";
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

    internal class OptionAttributeMetadataInfoConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destType)
        {
            if (destType == typeof (string) && value is OptionMetadataInfo)
            {
                // Cast the value to an Employee type
                var emp = (OptionMetadataInfo) value;


                return emp.Label.UserLocalizedLabel != null
                    ? emp.Label.UserLocalizedLabel.Label
                    : "(label not available for current user's language)";
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

    internal class OptionMetadataCollectionConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destType)
        {
            if (destType == typeof (string) && value is OptionMetadataCollection)
            {
                return "Expand to see items";
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

    internal class LocalizedLabelInfoConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destType)
        {
            if (destType == typeof (string) && value is LocalizedLabelInfo)
            {
                // Cast the value to an Employee type
                var emp = (LocalizedLabelInfo) value;


                return emp.Label;
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

    internal class LocalizedLabelCollectionConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destType)
        {
            if (destType == typeof (string) && value is LocalizedLabelCollection)
            {
                return "Expand to see items";
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

    internal class LabelInfoConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destType)
        {
            if (destType == typeof (string) && value is LabelInfo)
            {
                // Cast the value to an Employee type
                var emp = (LabelInfo) value;


                return emp.UserLocalizedLabel != null
                    ? emp.UserLocalizedLabel.Label
                    : "(label not available for current user's language)";
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

    internal class LabelCollectionConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destType)
        {
            if (destType == typeof (string) && value is LabelCollection)
            {
                return "Expand to see items";
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

    internal class BooleanManagedPropertyInfoConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destType)
        {
            if (destType == typeof(string) && value is BooleanManagedPropertyInfo)
            {
                var property = (BooleanManagedPropertyInfo) value;

                return string.Format("Value: {0} / CanBeChanged: {1}", property.Value, property.CanBeChanged);
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

    internal class AttributeRequiredLevelManagedPropertyInfoConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destType)
        {
            if (destType == typeof(string) && value is AttributeRequiredLevelManagedPropertyInfo)
            {
                var property = (AttributeRequiredLevelManagedPropertyInfo)value;

                return string.Format("Value: {0} / CanBeChanged: {1}", property.Value, property.CanBeChanged);
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

    internal class CascadeConfigurationInfoConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destType)
        {
            if (destType == typeof(string) && value is CascadeConfigurationInfo)
            {
                return "Expand to see items";
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

    internal class AssociatedMenuConfigurationInfoConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destType)
        {
            if (destType == typeof(string) && value is AssociatedMenuConfigurationInfo)
            {
                return "Expand to see items";
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }
}