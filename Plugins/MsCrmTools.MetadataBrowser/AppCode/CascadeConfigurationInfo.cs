using Microsoft.Xrm.Sdk.Metadata;
using System.ComponentModel;

namespace MsCrmTools.MetadataBrowser.AppCode
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class CascadeConfigurationInfo
    {
        private CascadeConfiguration configuration;

        public CascadeConfigurationInfo(CascadeConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public CascadeType Assign
        {
            get { return configuration.Assign.HasValue ? configuration.Assign.Value : CascadeType.NoCascade; }
        }

        public CascadeType Delete
        {
            get { return configuration.Delete.HasValue ? configuration.Delete.Value : CascadeType.NoCascade; }
        }

        public CascadeType Merge
        {
            get { return configuration.Merge.HasValue ? configuration.Merge.Value : CascadeType.NoCascade; }
        }

        public CascadeType Reparent
        {
            get { return configuration.Reparent.HasValue ? configuration.Reparent.Value : CascadeType.NoCascade; }
        }

        public CascadeType Share
        {
            get { return configuration.Share.HasValue ? configuration.Share.Value : CascadeType.NoCascade; }
        }

        public CascadeType Unshare
        {
            get { return configuration.Unshare.HasValue ? configuration.Unshare.Value : CascadeType.NoCascade; }
        }

        public override string ToString()
        {
            return "Expand to see properties";
        }
    }
}