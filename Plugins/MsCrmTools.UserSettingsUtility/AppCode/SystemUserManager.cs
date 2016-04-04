using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;

namespace MsCrmTools.UserSettingsUtility.AppCode
{
    internal class SystemUserManager
    {
        private readonly IOrganizationService service;

        public SystemUserManager(IOrganizationService service)
        {
            this.service = service;
        }

        public Guid GetCurrentUserId()
        {
            return ((WhoAmIResponse)service.Execute(new WhoAmIRequest())).UserId;
        }
    }
}