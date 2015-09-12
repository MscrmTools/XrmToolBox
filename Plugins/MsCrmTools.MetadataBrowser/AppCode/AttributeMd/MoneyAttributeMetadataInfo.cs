using Microsoft.Xrm.Sdk.Metadata;

namespace MsCrmTools.MetadataBrowser.AppCode.AttributeMd
{
    public class MoneyAttributeMetadataInfo : AttributeMetadataInfo
    {
        private readonly MoneyAttributeMetadata amd;

        public MoneyAttributeMetadataInfo(MoneyAttributeMetadata amd)
            : base(amd)
        {
            this.amd = amd;
        }

        public string CalculationOf
        {
            get { return amd.CalculationOf; }
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