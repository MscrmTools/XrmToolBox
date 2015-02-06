using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk;

namespace MsCrmTools.UserSettingsUtility.AppCode
{
    internal class SettingsCollection
    {
        public EntityCollection TimeZones { get; set; }

        public List<string> Areas { get; set; }

        public List<Tuple<string, string>> SubAreas { get; set; }

        public List<Language> Languages { get; set; }

        public List<Currency> Currencies { get; set; }
    }
}
