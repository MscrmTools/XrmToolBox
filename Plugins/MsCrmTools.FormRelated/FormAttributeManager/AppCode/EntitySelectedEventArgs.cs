using System;
using Microsoft.Xrm.Sdk.Metadata;

namespace MsCrmTools.FormAttributeManager.AppCode
{
    public class EntitySelectedEventArgs : EventArgs
    {
        public EntityMetadata Metadata { get; set; }
    }
}
