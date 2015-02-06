using Microsoft.Xrm.Sdk.Metadata;

namespace MsCrmTools.AuditCenter
{
    public class AttributeInfo
    {
        public AttributeMetadata Amd { get; set; }
        public bool InitialState { get; set; }
        public MainControl.ActionState Action { get; set; }
    }
}
