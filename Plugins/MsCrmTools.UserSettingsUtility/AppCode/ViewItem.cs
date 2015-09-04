using Microsoft.Xrm.Sdk;

namespace MsCrmTools.UserSettingsUtility.AppCode
{
    internal class ViewItem
    {
        private readonly Entity view;

        public ViewItem(Entity view)
        {
            this.view = view;
        }

        public string FetchXml
        {
            get { return view.GetAttributeValue<string>("fetchxml"); }
        }

        public override string ToString()
        {
            return view.GetAttributeValue<string>("name");
        }
    }
}