using Microsoft.Xrm.Sdk;
using MsCrmTools.MetadataBrowser.AppCode.LocalizedLabelMd;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using LocalizedLabelCollection = MsCrmTools.MetadataBrowser.AppCode.LocalizedLabelMd.LocalizedLabelCollection;

namespace MsCrmTools.MetadataBrowser.AppCode.LabelMd
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class LabelInfo
    {
        private readonly Label amd;

        public LabelInfo(Label amd)
        {
            this.amd = amd;
        }

        public string ExtensionData
        {
            get { return amd.ExtensionData != null ? amd.ExtensionData.ToString() : ""; }
        }

        [Editor(typeof(CustomCollectionEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(LocalizedLabelCollectionConverter))]
        public LocalizedLabelCollection LocalizedLabels
        {
            get
            {
                var collection = new LocalizedLabelCollection();
                foreach (LocalizedLabel rmd in amd.LocalizedLabels.ToList().OrderBy(r => r.LanguageCode))
                {
                    collection.Add(new LocalizedLabelInfo(rmd));
                }

                return collection;
            }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LocalizedLabelInfo UserLocalizedLabel
        {
            get { return new LocalizedLabelInfo(amd.UserLocalizedLabel); }
        }

        public override string ToString()
        {
            return amd.UserLocalizedLabel != null ? amd.UserLocalizedLabel.Label : "N/A";
        }
    }
}