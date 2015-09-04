using MsCrmTools.MetadataBrowser.AppCode.AttributeMd;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace MsCrmTools.MetadataBrowser.AppCode
{
    public class CustomCollectionEditor : CollectionEditor
    {
        public CustomCollectionEditor(Type type)
            : base(type)
        {
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.None;
        }

        protected override string GetDisplayText(object value)
        {
            var ami = value as AttributeMetadataInfo;
            if (ami != null)
            {
                return base.GetDisplayText(ami.SchemaName);
            }

            return base.GetDisplayText(value);
        }
    }
}