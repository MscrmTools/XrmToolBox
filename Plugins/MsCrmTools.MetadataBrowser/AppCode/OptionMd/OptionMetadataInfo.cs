using System.ComponentModel;

using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.MetadataBrowser.AppCode.LabelMd;

namespace MsCrmTools.MetadataBrowser.AppCode.OptionMd
{
    [TypeConverter(typeof (OptionAttributeMetadataInfoConverter))]
    public class OptionMetadataInfo
    {
        private readonly OptionMetadata amd;

        public OptionMetadataInfo(OptionMetadata amd)
        {
            this.amd = amd;
        }

        [TypeConverter(typeof (LabelInfoConverter))]
        public LabelInfo Description
        {
            get { return new LabelInfo(amd.Description); }
        }

        public string ExtensionData
        {
            get { return amd.ExtensionData != null ? amd.ExtensionData.ToString() : ""; }
        }

        public bool HasChanged
        {
            get { return amd.HasChanged.HasValue && amd.HasChanged.Value; }
        }

        public bool IsManaged
        {
            get { return amd.IsManaged.HasValue && amd.IsManaged.Value; }
        }

        [TypeConverter(typeof (LabelInfoConverter))]
        public LabelInfo Label
        {
            get { return new LabelInfo(amd.Label); }
        }

        public string MetadataId
        {
            get { return amd.MetadataId != null ? amd.MetadataId.Value.ToString() : ""; }
        }

        public int Value
        {
            get { return amd.Value.Value; }
        }
    }
}