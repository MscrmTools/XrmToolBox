using System;
using System.ComponentModel;
using Microsoft.Xrm.Sdk.Metadata;

namespace MsCrmTools.MetadataBrowser.AppCode.SecurityPrivilege
{
    [TypeConverter(typeof (SecurityPrivilegeInfoConverter))]
    public class SecurityPrivilegeInfo
    {
        private readonly SecurityPrivilegeMetadata p;

        public SecurityPrivilegeInfo(SecurityPrivilegeMetadata p)
        {
            this.p = p;
        }

        public bool CanBeBasic
        {
            get { return p.CanBeBasic; }
        }

        public bool CanBeDeep
        {
            get { return p.CanBeDeep; }
        }

        public bool CanBeGlobal
        {
            get { return p.CanBeGlobal; }
        }

        public bool CanBeLocal
        {
            get { return p.CanBeLocal; }
        }

        public string Name
        {
            get { return p.Name; }
        }

        public Guid PrivilegeId
        {
            get { return p.PrivilegeId; }
        }

        public PrivilegeType PrivilegeType
        {
            get { return p.PrivilegeType; }
        }

        public string ExtensionData
        {
            get { return p.ExtensionData != null ? p.ExtensionData.ToString() : ""; }
        }
    }
}