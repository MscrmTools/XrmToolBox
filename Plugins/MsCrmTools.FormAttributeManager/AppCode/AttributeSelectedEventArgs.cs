using System;
using Microsoft.Xrm.Sdk.Metadata;

namespace MsCrmTools.FormAttributeManager.AppCode
{
    public class AttributeSelectedEventArgs : EventArgs
    {
        public AttributeMetadata Metadata { get; set; }
    }
}
