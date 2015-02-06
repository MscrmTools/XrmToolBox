using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.MetadataBrowser.AppCode.AttributeMd;

namespace MsCrmTools.MetadataBrowser.AppCode.AttributeMd
{
    public class MemoAttributeMetadataInfo : AttributeMetadataInfo
    {
        private readonly MemoAttributeMetadata amd;

        public MemoAttributeMetadataInfo(MemoAttributeMetadata amd)
            : base(amd)
        {
            this.amd = amd;
        }

        public StringFormat Format
        {
            get { return amd.Format.Value; }
        }

        public ImeMode ImeMode
        {
            get { return amd.ImeMode.Value; }
        }

        public int MaxLength
        {
            get { return amd.MaxLength.Value; }
        }
    }
}