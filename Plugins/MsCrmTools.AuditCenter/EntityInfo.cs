using Microsoft.Xrm.Sdk.Metadata;

namespace MsCrmTools.AuditCenter
{
    public class EntityInfo
    {
        public EntityMetadata Emd { get; set; }
        public bool InitialState { get; set; }
        public MainControl.ActionState Action { get; set; }
    }
}
