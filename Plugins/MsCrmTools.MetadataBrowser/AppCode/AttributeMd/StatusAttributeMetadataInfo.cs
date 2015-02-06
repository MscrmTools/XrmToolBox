using System.ComponentModel;
using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.MetadataBrowser.AppCode.OptionSetMd;

namespace MsCrmTools.MetadataBrowser.AppCode.AttributeMd
{
    public class StatusAttributeMetadataInfo : AttributeMetadataInfo
    {
        private readonly StatusAttributeMetadata amd;

        public StatusAttributeMetadataInfo(StatusAttributeMetadata amd)
            : base(amd)
        {
            this.amd = amd;
        }

        public int DefaultFormValue
        {
            get { return amd.DefaultFormValue.Value; }
        }

        [TypeConverter(typeof(OptionSetAttributeMetadataInfoConverter))]
        public OptionSetMetadataInfo OptionSet
        {
            get { return new OptionSetMetadataInfo(amd.OptionSet); }
        }
    }
}