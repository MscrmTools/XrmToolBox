using Microsoft.Xrm.Sdk.Metadata;

namespace MsCrmTools.MetadataBrowser.AppCode.AttributeMd
{
    public class ImageAttributeMetadataInfo : AttributeMetadataInfo
    {
        private readonly ImageAttributeMetadata amd;

        public ImageAttributeMetadataInfo(ImageAttributeMetadata amd)
            : base(amd)
        {
            this.amd = amd;
        }

        public bool IsPrimaryImage
        {
            get { return amd.IsPrimaryImage.HasValue && amd.IsPrimaryImage.Value; }
        }

        public int MaxHeight
        {
            get { return amd.MaxHeight.HasValue ? amd.MaxHeight.Value : -1; }
        }

        public int MaxWidth
        {
            get { return amd.MaxWidth.HasValue ? amd.MaxWidth.Value : -1; }
        }
    }
}