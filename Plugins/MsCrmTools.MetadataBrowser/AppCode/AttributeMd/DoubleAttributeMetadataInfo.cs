using Microsoft.Xrm.Sdk.Metadata;

namespace MsCrmTools.MetadataBrowser.AppCode.AttributeMd
{
    public class DoubleAttributeMetadataInfo : AttributeMetadataInfo
    {
        private readonly DoubleAttributeMetadata amd;

        public DoubleAttributeMetadataInfo(DoubleAttributeMetadata amd)
            : base(amd)
        {
            this.amd = amd;
        }

        public ImeMode ImeMode
        {
            get { return amd.ImeMode.Value; }
        }

        public double MaxValue
        {
            get { return amd.MaxValue.Value; }
        }

        public double MinValue
        {
            get { return amd.MinValue.Value; }
        }

        public decimal Precision
        {
            get { return amd.Precision.Value; }
        }
    }
}