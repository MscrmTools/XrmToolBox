using Microsoft.Xrm.Sdk.Metadata;

namespace MsCrmTools.AuditCenter
{
    public class AttributeInfo
    {
        public MainControl.ActionState Action { get; set; }
        public AttributeMetadata Amd { get; set; }
        public bool InitialState { get; set; }
    }
}