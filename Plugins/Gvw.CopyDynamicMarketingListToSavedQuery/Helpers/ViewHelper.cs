using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace Gvw.CopyDynamicMarketingListToSavedQuery.Helpers
{
    internal enum ViewType
    {
        User,
        Saved
    }

    internal class ViewHelper
    {
        internal static string CreateView(IOrganizationService service, string name, string description, string entity, Entity copiedList, ViewType viewType)
        {
            if(String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("The name field cannot be empty.");
            }
            if(copiedList == null)
            {
                throw new ArgumentNullException("No marketing list selected. Please select a marketing list first.");
            }

            Entity view = new Entity();
            view.LogicalName = "userquery";
            if (viewType == ViewType.Saved) view.LogicalName = "savedquery";
            view.Attributes.Add("name", name);
            view.Attributes.Add("description", description);
            view.Attributes.Add("returnedtypecode", entity);
            view.Attributes.Add("fetchxml", copiedList.GetAttributeValue<string>("query"));
            view.Attributes.Add("querytype", 0);

            Guid viewId = service.Create(view);

            return String.Format("A new view with the name {0} and id {1} was created.", name, viewId);
        }
    }
}
