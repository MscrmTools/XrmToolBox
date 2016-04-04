using Microsoft.Xrm.Sdk.Metadata;

namespace MsCrmTools.MetadataBrowser.AppCode.AttributeMd
{
    public class ManagedPropertyAttributeMetadataInfo : AttributeMetadataInfo
    {
        private readonly ManagedPropertyAttributeMetadata amd;

        public ManagedPropertyAttributeMetadataInfo(ManagedPropertyAttributeMetadata amd)
            : base(amd)
        {
            this.amd = amd;
        }

        public string ManagedPropertyLogicalName
        {
            get { return amd.ManagedPropertyLogicalName; }
        }

        public string ParentAttributeName
        {
            get { return amd.ParentAttributeName; }
        }

        public int ParentComponentType
        {
            get { return amd.ParentComponentType.Value; }
        }
    }
}