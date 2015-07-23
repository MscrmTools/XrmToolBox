using System.Collections.Generic;
using Microsoft.Xrm.Sdk.Metadata;

namespace MsCrmTools.MetadataDocumentGenerator.Helper
{
    internal class AttributeMetadataComparer : IEqualityComparer<AttributeMetadata>
    {
        public bool Equals(AttributeMetadata x, AttributeMetadata y)
        {
            return x.EntityLogicalName == y.EntityLogicalName && x.LogicalName == y.LogicalName;
        }

        public int GetHashCode(AttributeMetadata obj)
        {
            return obj.GetHashCode();
        }
    }
}
