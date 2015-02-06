using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.MetadataBrowser.AppCode.LabelMd;

namespace MsCrmTools.MetadataBrowser.AppCode
{
    [TypeConverter(typeof(AssociatedMenuConfigurationInfoConverter))]
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

        [TypeConverter(typeof(LabelInfoConverter))]
        public LabelInfo Label
        {
            get { return new LabelInfo(configuration.Label); }
        }

        public int Order
        {
            get { return configuration.Order.HasValue ? configuration.Order.Value : -1; }
        }
    }
}
