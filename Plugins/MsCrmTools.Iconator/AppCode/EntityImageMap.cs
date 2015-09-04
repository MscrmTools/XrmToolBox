// PROJECT : MsCrmTools.Iconator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Xrm.Sdk.Metadata;

namespace MsCrmTools.Iconator.AppCode
{
    public class EntityImageMap
    {
        public EntityMetadata Entity { get; set; }
        public int ImageSize { get; set; }
        public string WebResourceName { get; set; }
    }
}