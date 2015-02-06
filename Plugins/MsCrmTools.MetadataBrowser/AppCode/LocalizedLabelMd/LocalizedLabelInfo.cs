using System.ComponentModel;
using Microsoft.Xrm.Sdk;

namespace MsCrmTools.MetadataBrowser.AppCode.LocalizedLabelMd
{
    [TypeConverter(typeof (LocalizedLabelInfoConverter))]
    public class LocalizedLabelInfo
    {
        private readonly LocalizedLabel amd;

        public LocalizedLabelInfo(LocalizedLabel amd)
        {
            this.amd = amd;
        }

        public string ExtensionData
        {
            get { return amd != null && amd.ExtensionData != null ? amd.ExtensionData.ToString() : ""; }
        }

        public bool HasChanged
        {
            get { return amd.HasChanged.HasValue && amd.HasChanged.Value; }
        }

        public bool IsManaged
        {
            get { return amd.IsManaged.HasValue && amd.IsManaged.Value; }
        }

        public string Label
        {
            get { return amd != null ? amd.Label : "N/A"; }
        }

        public int LanguageCode
        {
            get { return amd != null ? amd.LanguageCode : -1; }
        }
    }
}