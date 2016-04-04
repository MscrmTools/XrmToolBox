using Microsoft.Xrm.Sdk.Metadata;
using System.Collections.Generic;

namespace GapConsulting.PowerBIOptionSetAssistant.AppCode
{
    internal class Settings
    {
        public EntityMetadataCollection AllMetadata { get; internal set; }
        public bool CreateEntity { get; internal set; }
        public List<AttributeMetadata> OptionSets { get; internal set; }
    }
}