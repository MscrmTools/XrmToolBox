using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.MetadataBrowser.AppCode.LabelMd;
using System.ComponentModel;

namespace MsCrmTools.MetadataBrowser.AppCode
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class AssociatedMenuConfigurationInfo
    {
        private readonly AssociatedMenuConfiguration configuration;

        public AssociatedMenuConfigurationInfo(AssociatedMenuConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public AssociatedMenuBehavior Behavior
        {
            get { return configuration.Behavior != null ? configuration.Behavior.Value : AssociatedMenuBehavior.UseCollectionName; }
        }

        public AssociatedMenuGroup Group
        {
            get { return configuration.Group != null ? configuration.Group.Value : AssociatedMenuGroup.Details; }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LabelInfo Label
        {
            get { return new LabelInfo(configuration.Label); }
        }

        public int Order
        {
            get { return configuration.Order.HasValue ? configuration.Order.Value : -1; }
        }

        public override string ToString()
        {
            return "Expand to see properties";
        }
    }
}