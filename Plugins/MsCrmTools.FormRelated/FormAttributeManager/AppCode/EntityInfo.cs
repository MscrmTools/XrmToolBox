using Microsoft.Xrm.Sdk.Metadata;

namespace MsCrmTools.FormAttributeManager.AppCode
{
    internal class EntityInfo
    {
        public EntityMetadata Metadata { get; set; }

        public override string ToString()
        {
            return string.Format("{0} ({1})",
                Metadata.DisplayName != null && Metadata.DisplayName.UserLocalizedLabel != null
                ? Metadata.DisplayName.UserLocalizedLabel.Label : "N/A",
                Metadata.LogicalName);
        }
    }
}