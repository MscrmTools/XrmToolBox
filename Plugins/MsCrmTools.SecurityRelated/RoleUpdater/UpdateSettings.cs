// PROJECT : MsCrmTools.RoleUpdater
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;

namespace MsCrmTools.RoleUpdater
{
    public class UpdateSettings
    {
        public UpdateSettings()
        {
            SelectedRoles = new List<Entity>();
            Actions = new List<PrivilegeAction>();
        }

        public List<PrivilegeAction> Actions { get; set; }
        public List<Entity> SelectedRoles { get; set; }

        public List<RolePrivilege> ActionsToRolePrivilegeList()
        {
            var list = new List<RolePrivilege>();

            foreach (PrivilegeAction pAction in Actions)
            {
                var rp = new RolePrivilege { PrivilegeId = pAction.PrivilegeId };

                switch (pAction.Level)
                {
                    case PrivilegeLevel.User:
                        {
                            rp.Depth = PrivilegeDepth.Local;
                        }
                        break;

                    case PrivilegeLevel.BusinessUnit:
                        {
                            rp.Depth = PrivilegeDepth.Basic;
                        }
                        break;

                    case PrivilegeLevel.ParentChildBusinessUnit:
                        {
                            rp.Depth = PrivilegeDepth.Deep;
                        }
                        break;

                    case PrivilegeLevel.Organization:
                        {
                            rp.Depth = PrivilegeDepth.Global;
                        }
                        break;
                }

                list.Add(rp);
            }

            return list;
        }
    }
}