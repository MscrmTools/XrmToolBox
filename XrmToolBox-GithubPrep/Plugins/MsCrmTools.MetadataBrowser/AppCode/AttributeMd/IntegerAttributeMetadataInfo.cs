using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.MetadataBrowser.AppCode.AttributeMd;

namespace MsCrmTools.MetadataBrowser.AppCode.AttributeMd
{
    public class IntegerAttributeMetadataInfo : AttributeMetadataInfo
    {
        private readonly IntegerAttributeMetadata amd;

        public IntegerAttributeMetadataInfo(IntegerAttributeMetadata amd)
            : base(amd)
        {
            this.amd = amd;
        }

        public IntegerFormat Format
        {
            get { return amd.Format.Value; }
        }

        public double MaxValue
        {
            get { return amd.MaxValue.Value; }
        }

        public double MinValue
        {
            get { return amd.MinValue.Value; }
        }
    }
}