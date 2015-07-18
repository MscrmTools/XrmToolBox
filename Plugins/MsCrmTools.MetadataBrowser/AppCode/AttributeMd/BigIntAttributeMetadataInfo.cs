using Microsoft.Xrm.Sdk.Metadata;

namespace MsCrmTools.MetadataBrowser.AppCode.AttributeMd
{
    public class BigIntAttributeMetadataInfo : AttributeMetadataInfo
    {
        private readonly BigIntAttributeMetadata amd;

        public BigIntAttributeMetadataInfo(BigIntAttributeMetadata amd)
            : base(amd)
        {
            this.amd = amd;
        }

        public long MaxValue
        {
            get { return amd.MaxValue.Value; }
        }

        public long MinValue
        {
            get { return amd.MinValue.Value; }
        }
    }
}