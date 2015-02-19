using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsCrmTools.FlsBulkUpdater.AppCode
{
    class UpdateSettings
    {
        public List<Microsoft.Xrm.Sdk.Entity> Profiles { get; set; }

        public List<SecureFieldInfo> Fields { get; set; }

        public bool? CanCreate { get; set; }

        public bool? CanRead { get; set; }

        public bool? CanUpdate { get; set; }
    }
}
