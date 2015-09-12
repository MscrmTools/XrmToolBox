using Microsoft.Xrm.Sdk.Metadata;
using System;

namespace MsCrmTools.FormAttributeManager.AppCode
{
    public class EntitySelectedEventArgs : EventArgs
    {
        public EntityMetadata Metadata { get; set; }
    }
}