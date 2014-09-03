using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk.Metadata;

namespace MsCrmTools.FormAttributeManager.AppCode
{
    public class EntitySelectedEventArgs : EventArgs
    {
        public EntityMetadata Metadata { get; set; }
    }
}
