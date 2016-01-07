using Microsoft.Xrm.Sdk.Metadata;
using System;

namespace MsCrmTools.FormAttributeManager.AppCode
{
    public class AttributeSelectedEventArgs : EventArgs
    {
        public AttributeMetadata Metadata { get; set; }
    }
}