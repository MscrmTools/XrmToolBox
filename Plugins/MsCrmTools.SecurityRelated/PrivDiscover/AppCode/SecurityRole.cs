using System;
using System.Collections.Generic;

namespace MsCrmTools.PrivDiscover.AppCode
{
    internal class SecurityRole
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Privilege> Privileges { get; set; }
    }
}