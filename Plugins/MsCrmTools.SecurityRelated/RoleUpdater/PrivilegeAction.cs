// PROJECT : MsCrmTools.RoleUpdater
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;

namespace MsCrmTools.RoleUpdater
{
    [Flags]
    public enum PrivilegeLevel
    {
        None,
        User,
        BusinessUnit,
        ParentChildBusinessUnit,
        Organization
    }

    public class PrivilegeAction
    {
        private PrivilegeLevel _level;

        public PrivilegeLevel Level
        {
            get { return _level; }
            set
            {
                _level = value;

                switch (value)
                {
                    case PrivilegeLevel.None:
                        LevelName = "None";
                        break;

                    case PrivilegeLevel.User:
                        LevelName = "User";
                        break;

                    case PrivilegeLevel.BusinessUnit:
                        LevelName = "Business Unit";
                        break;

                    case PrivilegeLevel.ParentChildBusinessUnit:
                        LevelName = "Parent : Child Business Unit";
                        break;

                    case PrivilegeLevel.Organization:
                        LevelName = "Organization";
                        break;
                }
            }
        }

        public string LevelName { get; set; }
        public Guid PrivilegeId { get; set; }
        public string PrivilegeName { get; set; }
    }
}