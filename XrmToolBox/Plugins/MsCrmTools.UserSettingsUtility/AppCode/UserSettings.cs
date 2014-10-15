using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk;

namespace MsCrmTools.UserSettingsUtility.AppCode
{
    class UserSettings
    {
        public List<Entity> UsersToUpdate { get; set; }
        public string HomePageArea { get; set; }
        public string HomePageSubArea { get; set; }
        public int AdvancedFindStartupMode { get; set; }

        public int AutoCreateContactOnPromote { get; set; }

        public int DefaultCalendarView { get; set; }

        public int IncomingEmailFilteringMethod { get; set; }

        public int? PagingLimit { get; set; }

        public int? TimeZoneCode { get; set; }

        public string WorkdayStartTime { get; set; }

        public string WorkdayStopTime { get; set; }

        public int ReportScriptErrors { get; set; }

        public bool? IsSendAsAllowed { get; set; }

        public int? HelpLanguage { get; set; }

        public int? UiLanguage { get; set; }

        public EntityReference Currency { get; set; }
        public bool? StartupPaneEnabled { get; set; }
    }
}
