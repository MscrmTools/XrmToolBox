using System.Collections.Generic;

namespace MsCrmTools.FlsBulkUpdater.AppCode
{
    internal class UpdateSettings
    {
        public bool? CanCreate { get; set; }
        public bool? CanRead { get; set; }
        public bool? CanUpdate { get; set; }
        public List<SecureFieldInfo> Fields { get; set; }
        public List<Microsoft.Xrm.Sdk.Entity> Profiles { get; set; }
    }
}