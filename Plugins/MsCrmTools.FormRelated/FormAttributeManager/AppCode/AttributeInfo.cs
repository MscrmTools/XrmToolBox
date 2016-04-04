using Microsoft.Xrm.Sdk.Metadata;

namespace MsCrmTools.FormAttributeManager.AppCode
{
    public class AttributeMetadataInfo
    {
        public AttributeMetadata Amd { get; set; }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Amd.LogicalName, Amd.DisplayName.UserLocalizedLabel.Label);
        }
    }
}