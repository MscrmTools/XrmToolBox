using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.MetadataBrowser.AppCode.AttributeMd;

namespace MsCrmTools.MetadataBrowser.AppCode.AttributeMd
{
    // [TypeConverter(typeof(AttributeMetadataInfoConverter))]
    public class DateTimeAttributeMetadataInfo : AttributeMetadataInfo
    {
        private readonly DateTimeAttributeMetadata amd;

        public DateTimeAttributeMetadataInfo(DateTimeAttributeMetadata amd)
            : base(amd)
        {
            this.amd = amd;
        }

        public DateTimeFormat Format
        {
            get { return amd.Format.Value; }
        }

        public ImeMode ImeMode
        {
            get { return amd.ImeMode.Value; }
        }
    }
}