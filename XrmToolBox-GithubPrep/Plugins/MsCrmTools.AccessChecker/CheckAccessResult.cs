using System;

namespace MsCrmTools.AccessChecker
{
    internal class CheckAccessResult
    {
        public string RecordName { get; set; }

        public bool HasAppendAccess { get; set; }

        public Guid AppendPrivilegeId { get; set; }

        public bool HasAppendToAccess { get; set; }

        public Guid AppendToPrivilegeId { get; set; }

        public bool HasAssignToAccess { get; set; }

        public bool HasAssignAccess { get; set; }

        public Guid AssignPrivilegeId { get; set; }

        public bool HasCreateAccess { get; set; }

        public Guid CreatePrivilegeId { get; set; }

        public bool HasDeleteAccess { get; set; }

        public Guid DeletePrivilegeId { get; set; }

        public bool HasReadAccess { get; set; }

        public Guid ReadPrivilegeId { get; set; }

        public bool HasShareAccess { get; set; }

        public Guid SharePrivilegeId { get; set; }

        public bool HasWriteAccess { get; set; }

        public Guid WritePrivilegeId { get; set; }
    }
}
