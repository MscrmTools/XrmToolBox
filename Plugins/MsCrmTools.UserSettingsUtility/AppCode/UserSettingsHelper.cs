using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MsCrmTools.UserSettingsUtility.AppCode
{
    internal class UserSettingsHelper
    {
        private readonly ConnectionDetail detail;
        private readonly IOrganizationService service;

        public UserSettingsHelper(IOrganizationService service, ConnectionDetail detail)
        {
            this.service = service;
            this.detail = detail;
        }

        public List<Language> RetrieveAvailableLanguages()
        {
            var lcidRequest = new RetrieveProvisionedLanguagesRequest();
            var lcidResponse = (RetrieveProvisionedLanguagesResponse)service.Execute(lcidRequest);
            return lcidResponse.RetrieveProvisionedLanguages.Select(lcid => new Language(lcid)).ToList();
        }

        public List<Currency> RetrieveCurrencies()
        {
            return
                service.RetrieveMultiple(new QueryExpression("transactioncurrency") { ColumnSet = new ColumnSet("currencyname") })
                    .Entities.Select(tc => new Currency(tc))
                    .ToList();
        }

        public EntityCollection RetrieveTimeZones()
        {
            var request = new GetAllTimeZonesWithDisplayNameRequest { LocaleId = 1033 };
            var response = (GetAllTimeZonesWithDisplayNameResponse)service.Execute(request);

            return response.EntityCollection;
        }

        public void UpdateSettings(Guid userId, UserSettings settings)
        {
            var currentUserId = detail.ServiceClient.OrganizationServiceProxy.CallerId;
            detail.ServiceClient.OrganizationServiceProxy.CallerId = userId;

            var records = detail.ServiceClient.OrganizationServiceProxy.RetrieveMultiple(new QueryByAttribute("usersettings")
            {
                Attributes = { "systemuserid" },
                Values = { userId },
            });

            var record = records.Entities.First();

            if (settings.AdvancedFindStartupMode >= 1)
                record["advancedfindstartupmode"] = settings.AdvancedFindStartupMode;
            if (settings.AutoCreateContactOnPromote >= 0)
                record["autocreatecontactonpromote"] = settings.AutoCreateContactOnPromote;
            if (settings.DefaultCalendarView >= 0)
                record["defaultcalendarview"] = settings.DefaultCalendarView;
            if (settings.HomePageArea.Length > 0 && settings.HomePageArea != "No change")
                record["homepagearea"] = settings.HomePageArea;
            if (settings.HomePageSubArea.Length > 0 && settings.HomePageSubArea != "No change")
                record["homepagesubarea"] = settings.HomePageSubArea;
            if (settings.IncomingEmailFilteringMethod >= 0)
                record["incomingemailfilteringmethod"] = new OptionSetValue(settings.IncomingEmailFilteringMethod);
            if (settings.PagingLimit.HasValue)
                record["paginglimit"] = settings.PagingLimit.Value;
            if (settings.TimeZoneCode >= 0)
                record["timezonecode"] = settings.TimeZoneCode;
            if (settings.WorkdayStartTime.Length > 0 && settings.WorkdayStartTime != "No change")
                record["workdaystarttime"] = settings.WorkdayStartTime;
            if (settings.WorkdayStopTime.Length > 0 && settings.WorkdayStopTime != "No change")
                record["workdaystoptime"] = settings.WorkdayStopTime;
            if (settings.ReportScriptErrors >= 1)
                record["reportscripterrors"] = new OptionSetValue(settings.ReportScriptErrors);
            if (settings.IsSendAsAllowed.HasValue)
                record["issendasallowed"] = settings.IsSendAsAllowed.Value;
            if (settings.UiLanguage.HasValue)
                record["uilanguageid"] = settings.UiLanguage.Value;
            if (settings.HelpLanguage.HasValue)
                record["helplanguageid"] = settings.HelpLanguage.Value;
            if (settings.Currency != null)
                record["transactioncurrencyid"] = settings.Currency;
            if (settings.StartupPaneEnabled.HasValue)
                record["getstartedpanecontentenabled"] = settings.StartupPaneEnabled.Value;
            if (settings.UseCrmFormForAppointment.HasValue)
                record["usecrmformforappointment"] = settings.UseCrmFormForAppointment.Value;
            if (settings.UseCrmFormForContact.HasValue)
                record["usecrmformforcontact"] = settings.UseCrmFormForContact.Value;
            if (settings.UseCrmFormForEmail.HasValue)
                record["usecrmformforemail"] = settings.UseCrmFormForEmail.Value;
            if (settings.UseCrmFormForTask.HasValue)
                record["usecrmformfortask"] = settings.UseCrmFormForTask.Value;

            service.Update(record);

            detail.ServiceClient.OrganizationServiceProxy.CallerId = currentUserId;
        }
    }
}