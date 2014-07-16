using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk;

namespace MsCrmTools.UserRolesManager.AppCode
{
    public class RoleAction
    {
        public List<Entity> Roles { get; set; }
        public List<Entity> Principals { get; set; }

        public int ActionType { get; set; }
    }
}
