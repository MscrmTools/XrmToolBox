using Microsoft.Xrm.Sdk.Metadata;

namespace MsCrmTools.Translator.AppCode
{
    public class MasterAttribute
    {
        public AttributeMetadata Amd { get; set; }
        public string EntityLogicalName { get; set; }
    }
}