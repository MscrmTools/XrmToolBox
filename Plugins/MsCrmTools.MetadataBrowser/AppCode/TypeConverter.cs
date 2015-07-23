using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
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
   internal class OneToManyRelationshipCollectionConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destType)
        {
            if (destType == typeof (string) && value is OneToManyRelationshipCollection)
            {
                var item = (OneToManyRelationshipCollection)value;
                if (item.Count > 0)
                    return "Expand to see items";

                return "Empty";
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
                var item = (ManyToManyRelationshipCollection)value;
                if (item.Count > 0)
                    return "Expand to see items";

                return "Empty";
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
                var item = (SecurityPrivilegeCollection)value;
                if (item.Count > 0)
                    return "Expand to see items";

                return "Empty";
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
                var item = (AttributeMetadataCollection)value;
                if (item.Count > 0)
                    return "Expand to see items";

                return "Empty";
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
                var item = (OptionSetMetadataCollection)value;
                if (item.Count > 0)
                    return "Expand to see items";

                return "Empty";
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
                var item = (OptionMetadataCollection)value;
                if (item.Count > 0)
                    return "Expand to see items";

                return "Empty";
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
                var item = (LocalizedLabelCollection)value;
                if (item.Count > 0)
                    return "Expand to see items";

                return "Empty";
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
                var item = (LabelCollection) value;
                if (item.Count > 0)
                    return "Expand to see items";

                return "Empty";
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }
}