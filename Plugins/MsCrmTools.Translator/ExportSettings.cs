using System.Collections.Generic;

namespace MsCrmTools.Translator
{
    public class ExportSettings
    {
        public IEnumerable<string> Entities { get; set; }

        public bool ExportAttributes { get; set; }
        public bool ExportBooleans { get; set; }
        public bool ExportCharts { get; internal set; }
        public bool ExportCustomizedRelationships { get; set; }
        public bool ExportDashboards { get; set; }
        public bool ExportEntities { get; set; }
        public bool ExportFormFields { get; set; }
        public bool ExportForms { get; set; }
        public bool ExportFormSections { get; set; }
        public bool ExportFormTabs { get; set; }
        public bool ExportGlobalOptionSet { get; set; }
        public bool ExportOptionSet { get; set; }
        public bool ExportSiteMap { get; set; }
        public bool ExportViews { get; set; }
        public string FilePath { get; set; }
    }
}