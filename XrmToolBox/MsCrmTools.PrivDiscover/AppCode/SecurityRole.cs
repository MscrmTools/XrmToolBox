using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MsCrmTools.RolePrivilegeAnalyzer.AppCode
{
    class SecurityRole
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public List<Privilege> Privileges { get; set; } 
    }
}
