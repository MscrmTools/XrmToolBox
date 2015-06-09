using System;
using System.Collections.Generic;

namespace MsCrmTools.PrivDiscover.AppCode
{
    class SecurityRole
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public List<Privilege> Privileges { get; set; } 
    }
}
