using System.ComponentModel;
using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.MetadataBrowser.AppCode.AttributeMd;

namespace MsCrmTools.MetadataBrowser.AppCode.AttributeMd
{
    [TypeConverter(typeof (AttributeMetadataInfoConverter))]
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