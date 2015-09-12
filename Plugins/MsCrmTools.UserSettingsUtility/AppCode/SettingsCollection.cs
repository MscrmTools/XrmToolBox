using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;

namespace MsCrmTools.UserSettingsUtility.AppCode
{
    internal class SettingsCollection
    {
        public List<string> Areas { get; set; }
        public List<Currency> Currencies { get; set; }
        public List<Language> Languages { get; set; }
        public List<Tuple<string, string>> SubAreas { get; set; }
        public EntityCollection TimeZones { get; set; }
    }
}