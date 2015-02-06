using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.MetadataBrowser.AppCode.AttributeMd;
using MsCrmTools.MetadataBrowser.AppCode.LabelMd;
using MsCrmTools.MetadataBrowser.AppCode.ManyToManyRelationship;
using MsCrmTools.MetadataBrowser.AppCode.OneToManyRelationship;
using MsCrmTools.MetadataBrowser.AppCode.SecurityPrivilege;

namespace MsCrmTools.MetadataBrowser.AppCode
{
    public class EntityMetadataInfo
    {
        private readonly EntityMetadata emd;

        public EntityMetadataInfo(EntityMetadata emd)
        {
            this.emd = emd;
        }

        public int ActivityTypeMask
        {
            get { return emd.ActivityTypeMask.Value; }
        }

        [Editor(typeof (CustomCollectionEditor), typeof (UITypeEditor))]
        [TypeConverter(typeof (AttributeMetadataCollectionConverter))]
        public AttributeMetadataCollection Attributes
        {
            get
            {
                var collection = new AttributeMetadataCollection();
                if (emd.Attributes == null)
                {
                    return collection;
                }
                
                foreach (AttributeMetadata rmd in emd.Attributes.OrderBy(r => r.SchemaName))
                {
                    switch (rmd.AttributeType.Value)
                    {
                        case AttributeTypeCode.Boolean:
                        {
                            collection.Add(new BooleanAttributeMetadataInfo((BooleanAttributeMetadata) rmd));
                        }
                            break;
                        case AttributeTypeCode.BigInt:
                        {
                            collection.Add(new BigIntAttributeMetadataInfo((BigIntAttributeMetadata) rmd));
                        }
                            break;
                        case AttributeTypeCode.Customer:
                        case AttributeTypeCode.Lookup:
                        case AttributeTypeCode.Owner:
                        {
                            collection.Add(new LookupAttributeMetadataInfo((LookupAttributeMetadata) rmd));
                        }
                            break;
                        case AttributeTypeCode.DateTime:
                        {
                            collection.Add(new DateTimeAttributeMetadataInfo((DateTimeAttributeMetadata) rmd));
                        }
                            break;
                        case AttributeTypeCode.Decimal:
                        {
                            collection.Add(new DecimalAttributeMetadataInfo((DecimalAttributeMetadata) rmd));
                        }
                            break;
                        case AttributeTypeCode.Double:
                        {
                            collection.Add(new DoubleAttributeMetadataInfo((DoubleAttributeMetadata) rmd));
                        }
                            break;
                        case AttributeTypeCode.EntityName:
                        {
                            collection.Add(new AttributeMetadataInfo(rmd));
                        }
                            break;
                        case AttributeTypeCode.Integer:
                        {
                            collection.Add(new IntegerAttributeMetadataInfo((IntegerAttributeMetadata) rmd));
                        }
                            break;
                        case AttributeTypeCode.ManagedProperty:
                        {
                            collection.Add(
                                new ManagedPropertyAttributeMetadataInfo((ManagedPropertyAttributeMetadata) rmd));
                        }
                            break;
                        case AttributeTypeCode.Memo:
                        {
                            collection.Add(new MemoAttributeMetadataInfo((MemoAttributeMetadata) rmd));
                        }
                            break;
                        case AttributeTypeCode.Money:
                        {
                            collection.Add(new MoneyAttributeMetadataInfo((MoneyAttributeMetadata) rmd));
                        }
                            break;
                        case AttributeTypeCode.Picklist:
                        {
                            collection.Add(new PicklistAttributeMetadataInfo((PicklistAttributeMetadata) rmd));
                        }
                            break;
                        case AttributeTypeCode.State:
                        {
                            collection.Add(new StateAttributeMetadataInfo((StateAttributeMetadata) rmd));
                        }
                            break;
                        case AttributeTypeCode.Status:
                        {
                            collection.Add(new StatusAttributeMetadataInfo((StatusAttributeMetadata) rmd));
                        }
                            break;
                        case AttributeTypeCode.String:
                        {
                            collection.Add(new StringAttributeMetadataInfo((StringAttributeMetadata) rmd));
                        }
                            break;
                        default:
                        {
                            collection.Add(new AttributeMetadataInfo(rmd));
                        }
                            break;
                    }
                }

                return collection;
            }
        }

        public bool AutoRouteToOwnerQueue
        {
            get { return emd.AutoRouteToOwnerQueue.HasValue && emd.AutoRouteToOwnerQueue.Value; }
        }

        public bool CanBeInManyToMany
        {
            get { return emd.CanBeInManyToMany.Value; }
        }

        public bool CanBePrimaryEntityInRelationship
        {
            get { return emd.CanBePrimaryEntityInRelationship.Value; }
        }

        public bool CanBeRelatedEntityInRelationship
        {
            get { return emd.CanBeRelatedEntityInRelationship.Value; }
        }

        public bool CanCreateAttributes
        {
            get { return emd.CanCreateAttributes.Value; }
        }

        public bool CanCreateCharts
        {
            get { return emd.CanCreateCharts.Value; }
        }

        public bool CanCreateForms
        {
            get { return emd.CanCreateForms.Value; }
        }

        public bool CanCreateViews
        {
            get { return emd.CanCreateViews.Value; }
        }

        public bool CanModifyAdditionalSettings
        {
            get { return emd.CanModifyAdditionalSettings.Value; }
        }

        public bool CanTriggerWorkflow
        {
            get { return emd.CanTriggerWorkflow.HasValue && emd.CanTriggerWorkflow.Value; }
        }

        [TypeConverter(typeof (LabelInfoConverter))]
        public LabelInfo Description
        {
            get { return new LabelInfo(emd.Description); }
        }

        [TypeConverter(typeof (LabelInfoConverter))]
        public LabelInfo DisplayCollectionName
        {
            get { return new LabelInfo(emd.DisplayCollectionName); }
        }

        [TypeConverter(typeof (LabelInfoConverter))]
        public LabelInfo DisplayName
        {
            get { return new LabelInfo(emd.DisplayName); }
        }

        public string ExtensionData
        {
            get { return emd.ExtensionData == null ? "" : emd.ExtensionData.ToString(); }
        }

        public bool HasChanged
        {
            get { return emd.HasChanged.HasValue && emd.HasChanged.Value; }
        }

        public string IconLargeName
        {
            get { return emd.IconLargeName; }
        }

        public string IconMediumName
        {
            get { return emd.IconMediumName; }
        }

        public string IconSmallName
        {
            get { return emd.IconSmallName; }
        }

        public bool IsActivity
        {
            get { return emd.IsActivity.HasValue && emd.IsActivity.Value; }
        }

        public bool IsActivityParty
        {
            get { return emd.IsActivityParty.HasValue && emd.IsActivityParty.Value; }
        }

        public bool IsAuditEnabled
        {
            get { return emd.IsAuditEnabled.Value; }
        }

        public bool IsAvailableOffline
        {
            get { return emd.IsAvailableOffline.HasValue && emd.IsAvailableOffline.Value; }
        }

        public bool IsChildEntity
        {
            get { return emd.IsChildEntity.HasValue && emd.IsChildEntity.Value; }
        }

        public bool IsConnectionsEnabled
        {
            get { return emd.IsConnectionsEnabled.Value; }
        }

        public bool IsCustomEntity
        {
            get { return emd.IsCustomEntity.HasValue && emd.IsCustomEntity.Value; }
        }

        public bool IsCustomizable
        {
            get { return emd.IsCustomizable.Value; }
        }

        public bool IsDocumentManagementEnabled
        {
            get { return emd.IsDocumentManagementEnabled.HasValue && emd.IsDocumentManagementEnabled.Value; }
        }

        public bool IsDuplicateDetectionEnabled
        {
            get { return emd.IsDuplicateDetectionEnabled.Value; }
        }

        public bool IsEnabledForCharts
        {
            get { return emd.IsEnabledForCharts.HasValue && emd.IsEnabledForCharts.Value; }
        }

        public bool IsImportable
        {
            get { return emd.IsImportable.HasValue && emd.IsImportable.Value; }
        }

        public bool IsIntersect
        {
            get { return emd.IsIntersect.HasValue && emd.IsIntersect.Value; }
        }

        public bool IsMailMergeEnabled
        {
            get { return emd.IsMailMergeEnabled.Value; }
        }

        public bool IsManaged
        {
            get { return emd.IsManaged.HasValue && emd.IsManaged.Value; }
        }

        public bool IsMappable
        {
            get { return emd.IsMappable.Value; }
        }

        public bool IsReadingPaneEnabled
        {
            get { return emd.IsReadingPaneEnabled.HasValue && emd.IsReadingPaneEnabled.Value; }
        }

        public bool IsRenameable
        {
            get { return emd.IsRenameable.Value; }
        }

        public bool IsValidForAdvancedFind
        {
            get { return emd.IsValidForAdvancedFind.HasValue && emd.IsValidForAdvancedFind.Value; }
        }

        public bool IsValidForQueue
        {
            get { return emd.IsValidForQueue.Value; }
        }

        public bool IsVisibleInMobile
        {
            get { return emd.IsVisibleInMobile.Value; }
        }

        public string LogicalName
        {
            get { return emd.LogicalName; }
        }

        [Editor(typeof (CustomCollectionEditor), typeof (UITypeEditor))]
        [TypeConverter(typeof (ManyToManyRelationshipCollectionConverter))]
        public ManyToManyRelationshipCollection ManyToManyRelationships
        {
            get
            {
                var collection = new ManyToManyRelationshipCollection();
                if (emd.ManyToManyRelationships == null)
                {
                    return collection;
                }
                
                foreach (ManyToManyRelationshipMetadata rmd in emd.ManyToManyRelationships.OrderBy(r => r.SchemaName))
                {
                    collection.Add(new ManyToManyRelationshipMetadataInfo(rmd));
                }

                return collection;
            }
        }

        [Editor(typeof (CustomCollectionEditor), typeof (UITypeEditor))]
        [TypeConverter(typeof (OneToManyRelationshipCollectionConverter))]
        public OneToManyRelationshipCollection ManyToOneRelationships
        {
            get
            {
                var collection = new OneToManyRelationshipCollection();
                if (emd.ManyToOneRelationships == null)
                {
                    return collection;
                } 
                
                foreach (OneToManyRelationshipMetadata rmd in emd.ManyToOneRelationships.OrderBy(r => r.SchemaName))
                {
                    collection.Add(new OneToManyRelationshipMetadataInfo(rmd));
                }

                return collection;
            }
        }

        public Guid MetadataId
        {
            get { return emd.MetadataId.Value; }
        }

        public int ObjectTypeCode
        {
            get { return emd.ObjectTypeCode.Value; }
        }

        [Editor(typeof (CustomCollectionEditor), typeof (UITypeEditor))]
        [TypeConverter(typeof (OneToManyRelationshipCollectionConverter))]
        public OneToManyRelationshipCollection OneToManyRelationships
        {
            get
            {
                var collection = new OneToManyRelationshipCollection();
                if (emd.OneToManyRelationships == null)
                {
                    return collection;
                }
                
                foreach (OneToManyRelationshipMetadata rmd in emd.OneToManyRelationships.OrderBy(r => r.SchemaName))
                {
                    collection.Add(new OneToManyRelationshipMetadataInfo(rmd));
                }

                return collection;
            }
        }

        public OwnershipTypes OwnershipType
        {
            get { return emd.OwnershipType.Value; }
        }

        public string PrimaryIdAttribute
        {
            get { return emd.PrimaryIdAttribute; }
        }

        public string PrimaryNameAttribute
        {
            get { return emd.PrimaryNameAttribute; }
        }

        [Editor(typeof (CustomCollectionEditor), typeof (UITypeEditor))]
        [TypeConverter(typeof (SecurityPrivilegeCollectionConverter))]
        public SecurityPrivilegeCollection Privileges
        {
            get
            {
                var collection = new SecurityPrivilegeCollection();
                if (emd.Privileges == null)
                {
                    return collection;
                }
                
                foreach (SecurityPrivilegeMetadata rmd in emd.Privileges.OrderBy(r => r.Name))
                {
                    collection.Add(new SecurityPrivilegeInfo(rmd));
                }

                return collection;
            }
        }

        public string RecurrenceBaseEntityLogicalName
        {
            get { return emd.RecurrenceBaseEntityLogicalName; }
        }

        public string ReportViewName
        {
            get { return emd.ReportViewName; }
        }

        public string SchemaName
        {
            get { return emd.SchemaName; }
        }
    }
}