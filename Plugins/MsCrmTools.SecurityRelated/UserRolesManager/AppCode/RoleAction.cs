using Microsoft.Xrm.Sdk;
using System.Collections.Generic;

namespace MsCrmTools.UserRolesManager.AppCode
{
    public class RoleAction
    {
        public int ActionType { get; set; }
        public List<Entity> Principals { get; set; }
        public List<Entity> Roles { get; set; }
    }
}