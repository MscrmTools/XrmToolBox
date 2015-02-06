using System.ComponentModel;
using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.MetadataBrowser.AppCode.BooleanOptionSetMd;

namespace MsCrmTools.MetadataBrowser.AppCode.AttributeMd
{
    [TypeConverter(typeof (AttributeMetadataInfoConverter))]
    public class BooleanAttributeMetadataInfo : AttributeMetadataInfo
    {
        private readonly BooleanAttributeMetadata amd;

        public BooleanAttributeMetadataInfo(BooleanAttributeMetadata amd)
            : base(amd)
        {
            this.amd = amd;
        }

        public bool DefaultValue
        {
            get { return amd.DefaultValue.HasValue && amd.DefaultValue.Value; }
        }

        public BooleanOptionSetMetadataInfo OptionSet
        {
            get { return new BooleanOptionSetMetadataInfo(amd.OptionSet); }
        }
    }
}