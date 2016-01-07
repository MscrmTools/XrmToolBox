using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.ComponentModel;

namespace MsCrmTools.MetadataBrowser.AppCode.ManyToManyRelationship
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ManyToManyRelationshipMetadataInfo
    {
        private readonly ManyToManyRelationshipMetadata mtmmd;

        public ManyToManyRelationshipMetadataInfo(ManyToManyRelationshipMetadata mtmmd)
        {
            this.mtmmd = mtmmd;
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public AssociatedMenuConfigurationInfo Entity1AssociatedMenuConfiguration
        {
            get
            {
                return new AssociatedMenuConfigurationInfo(mtmmd.Entity1AssociatedMenuConfiguration);
            }
        }

        public string Entity1IntersectAttribute
        {
            get { return mtmmd.Entity1IntersectAttribute; }
        }

        public string Entity1LogicalName
        {
            get { return mtmmd.Entity1LogicalName; }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public AssociatedMenuConfigurationInfo Entity2AssociatedMenuConfiguration
        {
            get
            {
                return new AssociatedMenuConfigurationInfo(mtmmd.Entity2AssociatedMenuConfiguration);
            }
        }

        public string Entity2IntersectAttribute
        {
            get { return mtmmd.Entity2IntersectAttribute; }
        }

        public string Entity2LogicalName
        {
            get { return mtmmd.Entity2LogicalName; }
        }

        public string ExtensionData
        {
            get { return mtmmd.ExtensionData != null ? mtmmd.ExtensionData.ToString() : ""; }
        }

        public bool HasChanged
        {
            get { return mtmmd.IsValidForAdvancedFind.HasValue && mtmmd.IsValidForAdvancedFind.Value; }
        }

        public string IntersectEntityName
        {
            get { return mtmmd.IntersectEntityName; }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BooleanManagedPropertyInfo IsCustomizable
        {
            get { return new BooleanManagedPropertyInfo(mtmmd.IsCustomizable); }
        }

        public bool IsCustomRelationship
        {
            get { return mtmmd.IsCustomRelationship.HasValue && mtmmd.IsCustomRelationship.Value; }
        }

        public bool IsManaged
        {
            get { return mtmmd.IsManaged.HasValue && mtmmd.IsManaged.Value; }
        }

        public bool IsValidForAdvancedFind
        {
            get { return mtmmd.IsValidForAdvancedFind.HasValue && mtmmd.IsValidForAdvancedFind.Value; }
        }

        public Guid MetadataId
        {
            get { return mtmmd.MetadataId.Value; }
        }

        public RelationshipType RelationshipType
        {
            get { return mtmmd.RelationshipType; }
        }

        public string SchemaName
        {
            get { return mtmmd.SchemaName; }
        }

        public SecurityTypes SecurityTypes
        {
            get { return mtmmd.SecurityTypes.Value; }
        }

        public override string ToString()
        {
            return mtmmd.SchemaName;
        }
    }
}