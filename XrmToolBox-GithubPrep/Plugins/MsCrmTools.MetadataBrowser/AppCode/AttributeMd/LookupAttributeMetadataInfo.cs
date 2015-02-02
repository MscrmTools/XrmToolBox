using System.ComponentModel;
using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.MetadataBrowser.AppCode.AttributeMd;

namespace MsCrmTools.MetadataBrowser.AppCode.AttributeMd
{
    [TypeConverter(typeof (AttributeMetadataInfoConverter))]
    public class LookupAttributeMetadataInfo : AttributeMetadataInfo
    {
        private readonly LookupAttributeMetadata amd;

        public LookupAttributeMetadataInfo(LookupAttributeMetadata amd)
            : base(amd)
        {
            this.amd = amd;
        }

        public string[] Targets
        {
            get { return amd.Targets; }
        }
    }
}