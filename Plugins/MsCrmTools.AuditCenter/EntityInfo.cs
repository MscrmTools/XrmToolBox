using Microsoft.Xrm.Sdk.Metadata;

namespace MsCrmTools.AuditCenter
{
    public class EntityInfo
    {
        public MainControl.ActionState Action { get; set; }
        public EntityMetadata Emd { get; set; }
        public bool InitialState { get; set; }
    }
}