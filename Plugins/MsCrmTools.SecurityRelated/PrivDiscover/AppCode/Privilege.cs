using System;
using Microsoft.Crm.Sdk.Messages;

namespace MsCrmTools.PrivDiscover.AppCode
{
    class Privilege
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public PrivilegeDepth Depth { get; set; }
        public bool IsAnyDepth { get; set; }
        public bool IsNoDepth { get; set; }
    }
}
