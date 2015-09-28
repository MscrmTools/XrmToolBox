using Microsoft.Xrm.Sdk.Metadata;
using System.Collections.Generic;

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