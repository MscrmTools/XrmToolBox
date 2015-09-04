using Microsoft.Xrm.Sdk;
using System.Collections.Generic;

namespace MsCrmTools.UserSettingsUtility.AppCode
{
    internal class UserSettings
    {
        public int AdvancedFindStartupMode { get; set; }
        public int AutoCreateContactOnPromote { get; set; }
        public EntityReference Currency { get; set; }
        public int DefaultCalendarView { get; set; }
        public int? HelpLanguage { get; set; }
        public string HomePageArea { get; set; }
        public string HomePageSubArea { get; set; }
        public int IncomingEmailFilteringMethod { get; set; }
        public bool? IsSendAsAllowed { get; set; }
        public int? PagingLimit { get; set; }
        public int ReportScriptErrors { get; set; }
        public bool? StartupPaneEnabled { get; set; }
        public int? TimeZoneCode { get; set; }
        public int? UiLanguage { get; set; }
        public bool? UseCrmFormForAppointment { get; set; }
        public bool? UseCrmFormForContact { get; set; }
        public bool? UseCrmFormForEmail { get; set; }
        public bool? UseCrmFormForTask { get; set; }
        public List<Entity> UsersToUpdate { get; set; }
        public string WorkdayStartTime { get; set; }

        public string WorkdayStopTime { get; set; }
    }
}