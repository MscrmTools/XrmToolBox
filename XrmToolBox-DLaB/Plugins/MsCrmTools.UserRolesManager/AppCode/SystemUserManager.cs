using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace MsCrmTools.UserRolesManager.AppCode
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
            return ((WhoAmIResponse) service.Execute(new WhoAmIRequest())).UserId;
        }
    }
}
