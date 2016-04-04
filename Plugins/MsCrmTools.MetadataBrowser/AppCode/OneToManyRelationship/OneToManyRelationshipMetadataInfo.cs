using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.ComponentModel;

namespace MsCrmTools.MetadataBrowser.AppCode.OneToManyRelationship
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class OneToManyRelationshipMetadataInfo
    {
        private readonly OneToManyRelationshipMetadata otmmd;

        public OneToManyRelationshipMetadataInfo(OneToManyRelationshipMetadata otmmd)
        {
            this.otmmd = otmmd;
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public AssociatedMenuConfigurationInfo AssociatedMenuConfiguration
        {
            get
            {
                return new AssociatedMenuConfigurationInfo(otmmd.AssociatedMenuConfiguration);
            }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public CascadeConfigurationInfo CascadeConfiguration
        {
            get { return new CascadeConfigurationInfo(otmmd.CascadeConfiguration); }
        }

        public string ExtensionData
        {
            get { return otmmd.ExtensionData != null ? otmmd.ExtensionData.ToString() : ""; }
        }

        public bool HasChanged
        {
            get { return otmmd.HasChanged.HasValue && otmmd.HasChanged.Value; }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BooleanManagedPropertyInfo IsCustomizable
        {
            get { return new BooleanManagedPropertyInfo(otmmd.IsCustomizable); }
        }

        public bool IsCustomRelationship
        {
            get { return otmmd.IsCustomRelationship.HasValue && otmmd.IsCustomRelationship.Value; }
        }

        public bool IsManaged
        {
            get { return otmmd.IsManaged.HasValue && otmmd.IsManaged.Value; }
        }

        public bool IsValidForAdvancedFind
        {
            get { return otmmd.IsValidForAdvancedFind.HasValue && otmmd.IsValidForAdvancedFind.Value; }
        }

        public Guid MetadataId
        {
            get { return otmmd.MetadataId.Value; }
        }

        public string ReferencedAttribute
        {
            get { return otmmd.ReferencedAttribute; }
        }

        public string ReferencedEntity
        {
            get { return otmmd.ReferencedEntity; }
        }

        public string ReferencingAttribute
        {
            get { return otmmd.ReferencingAttribute; }
        }

        public string ReferencingEntity
        {
            get { return otmmd.ReferencingEntity; }
        }

        public RelationshipType RelationshipType
        {
            get { return otmmd.RelationshipType; }
        }

        public string SchemaName
        {
            get { return otmmd.SchemaName; }
        }

        public SecurityTypes SecurityTypes
        {
            get { return otmmd.SecurityTypes.Value; }
        }

        public override string ToString()
        {
            return otmmd.SchemaName;
        }
    }
}