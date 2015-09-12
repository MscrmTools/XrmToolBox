using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.MetadataBrowser.AppCode.LabelMd;
using System.ComponentModel;

namespace MsCrmTools.MetadataBrowser.AppCode.OptionMd
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class OptionMetadataInfo
    {
        private readonly OptionMetadata amd;

        public OptionMetadataInfo(OptionMetadata amd)
        {
            this.amd = amd;
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
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

        [TypeConverter(typeof(ExpandableObjectConverter))]
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

        public override string ToString()
        {
            return amd.Label.UserLocalizedLabel != null ? amd.Label.UserLocalizedLabel.Label : "N/A";
        }
    }
}