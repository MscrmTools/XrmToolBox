using Microsoft.Crm.Sdk.Messages;
using System;

namespace MsCrmTools.PrivDiscover.AppCode
{
    internal class Privilege
    {
        public PrivilegeDepth Depth { get; set; }
        public Guid Id { get; set; }
        public bool IsAnyDepth { get; set; }
        public bool IsNoDepth { get; set; }
        public string Name { get; set; }
    }
}