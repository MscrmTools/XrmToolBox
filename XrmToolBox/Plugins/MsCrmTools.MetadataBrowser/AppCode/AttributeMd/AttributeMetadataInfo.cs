using System.ComponentModel;
using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.MetadataBrowser.AppCode.LabelMd;

namespace MsCrmTools.MetadataBrowser.AppCode.AttributeMd
{
    [TypeConverter(typeof (AttributeMetadataInfoConverter))]
    public class AttributeMetadataInfo
    {
        private readonly AttributeMetadata amd;

        public AttributeMetadataInfo(AttributeMetadata amd)
        {
            this.amd = amd;
        }

        public string AttributeOf
        {
            get { return amd.AttributeOf; }
        }

        public AttributeTypeCode AttributeType
        {
            get { return amd.AttributeType.Value; }
        }

        public string AttributeTypeName
        {
            get { return amd.AttributeTypeName != null ? amd.AttributeTypeName.Value : ""; }
        }

        public bool CanBeSecuredForCreate
        {
            get { return amd.CanBeSecuredForCreate.HasValue && amd.CanBeSecuredForCreate.Value; }
        }

        public bool CanBeSecuredForRead
        {
            get { return amd.CanBeSecuredForRead.HasValue && amd.CanBeSecuredForRead.Value; }
        }

        public bool CanBeSecuredForUpdate
        {
            get { return amd.CanBeSecuredForUpdate.HasValue && amd.CanBeSecuredForUpdate.Value; }
        }

        [TypeConverter(typeof(BooleanManagedPropertyInfoConverter))]
        public BooleanManagedPropertyInfo CanModifyAdditionalSettings
        {
            get { return new BooleanManagedPropertyInfo(amd.CanModifyAdditionalSettings); }
        }

        public int ColumnNumber
        {
            get { return amd.ColumnNumber.Value; }
        }

        public string DeprecatedVersion
        {
            get { return amd.DeprecatedVersion; }
        }

        [TypeConverter(typeof (LabelInfoConverter))]
        public LabelInfo Description
        {
            get { return new LabelInfo(amd.Description); }
        }

        [TypeConverter(typeof (LabelInfoConverter))]
        public LabelInfo DisplayName
        {
            get { return new LabelInfo(amd.DisplayName); }
        }

        public string EntityLogicalName
        {
            get { return amd.EntityLogicalName; }
        }

        public string ExtensionData
        {
            get { return amd.ExtensionData.ToString(); }
        }

        public bool HasChanged
        {
            get { return amd.HasChanged.HasValue && amd.HasChanged.Value; }
        }

        public string IntroducedVersion

        {
            get { return amd.IntroducedVersion; }
        }

        [TypeConverter(typeof(BooleanManagedPropertyInfoConverter))]
        public BooleanManagedPropertyInfo IsAuditEnabled
        {
            get { return new BooleanManagedPropertyInfo(amd.IsAuditEnabled); }
        }

        public bool IsCustomAttribute
        {
            get { return amd.IsCustomAttribute.HasValue && amd.IsCustomAttribute.Value; }
        }

        [TypeConverter(typeof(BooleanManagedPropertyInfoConverter))]
        public BooleanManagedPropertyInfo IsCustomizable
        {
            get { return new BooleanManagedPropertyInfo(amd.IsCustomizable); }
        }

        public bool IsManaged
        {
            get { return amd.IsManaged.HasValue && amd.IsManaged.Value; }
        }

        public bool IsPrimaryId
        {
            get { return amd.IsPrimaryId.HasValue && amd.IsPrimaryId.Value; }
        }

        public bool IsPrimaryName
        {
            get { return amd.IsPrimaryName.HasValue && amd.IsPrimaryName.Value; }
        }

        [TypeConverter(typeof(BooleanManagedPropertyInfoConverter))]
        public BooleanManagedPropertyInfo IsRenameable
        {
            get { return new BooleanManagedPropertyInfo(amd.IsRenameable); }
        }

        public bool IsSecured
        {
            get { return amd.IsSecured.HasValue && amd.IsSecured.Value; }
        }

        public bool IsValidForCreate
        {
            get { return amd.IsValidForCreate.HasValue && amd.IsValidForCreate.Value; }
        }

        public bool IsValidForRead
        {
            get { return amd.IsValidForRead.HasValue && amd.IsValidForRead.Value; }
        }

        public bool IsValidForUpdate
        {
            get { return amd.IsValidForUpdate.HasValue && amd.IsValidForUpdate.Value; }
        }


        public string LinkedAttributeId
        {
            get { return amd.LinkedAttributeId.HasValue ? amd.LinkedAttributeId.Value.ToString("B") : ""; }
        }

        public string LogicalName
        {
            get { return amd.LogicalName; }
        }

        public string MetadataId
        {
            get { return amd.MetadataId.HasValue ? amd.MetadataId.Value.ToString("B") : ""; }
        }

        public string SchemaName
        {
            get { return amd.SchemaName; }
        }


        [TypeConverter(typeof(AttributeRequiredLevelManagedPropertyInfoConverter))]
        public AttributeRequiredLevelManagedPropertyInfo RequiredLevel
        {
            get { return new AttributeRequiredLevelManagedPropertyInfo(amd.RequiredLevel); }
        }
    }
}