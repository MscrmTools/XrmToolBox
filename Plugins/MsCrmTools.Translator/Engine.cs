using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.Translator.AppCode;

#if NO_GEMBOX
using OfficeOpenXml;
#else
using GemBox.Spreadsheet;
#endif

namespace MsCrmTools.Translator
{
    public  class Engine
    {
#if NO_GEMBOX
#else
        public Engine()
        {
            // The license key to use Gembox.Spreadsheet is not included in 
            // this source code. To obtain a license key, visit Gembox website
            var key = new GemBox.LicenseKey.Key();
            string excelKey = key.ExcelKey37;
            SpreadsheetInfo.SetLicense(excelKey);
        }
#endif

        public void Export(ExportSettings settings, IOrganizationService service, BackgroundWorker worker = null)
        {
            // Loading available languages
            if (worker != null && worker.WorkerReportsProgress)
            {
                worker.ReportProgress(0, "Loading provisioned languages...");
            }
            var lcidRequest = new RetrieveProvisionedLanguagesRequest();
            var lcidResponse = (RetrieveProvisionedLanguagesResponse)service.Execute(lcidRequest);
            var lcids = lcidResponse.RetrieveProvisionedLanguages.Select(lcid => lcid).ToList();

            // Loading entities
            var emds = new List<EntityMetadata>();

            if (worker != null && worker.WorkerReportsProgress)
            {
                worker.ReportProgress(0, "Loading selected entities...");
            }
            foreach (string entityLogicalName in settings.Entities)
            {
                var request = new RetrieveEntityRequest { LogicalName = entityLogicalName, EntityFilters = EntityFilters.Attributes };
                var response = (RetrieveEntityResponse)service.Execute(request);
                emds.Add(response.EntityMetadata);
            }
#if NO_GEMBOX
            var file = new ExcelPackage();
#else
            var file = new ExcelFile();
#endif
            if (settings.ExportEntities && emds.Count > 0)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress(0, "Exporting entities translations...");
                }

#if NO_GEMBOX
                var sheet = file.Workbook.Worksheets.Add("Entities");
#else
                var sheet = file.Worksheets.Add("Entities");
#endif
                var et = new EntityTranslation();
                et.Export(emds, lcids, sheet);
            }

            if (settings.ExportAttributes && emds.Count > 0)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress(0, "Exporting attributes translations...");
                }
#if NO_GEMBOX
                var sheet = file.Workbook.Worksheets.Add("Attributes");
#else
                var sheet = file.Worksheets.Add("Attributes");
#endif
                var at = new AttributeTranslation();
                at.Export(emds, lcids, sheet);
            }

            if (settings.ExportGlobalOptionSet)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress(0, "Exporting global optionsets translations...");
                }
#if NO_GEMBOX
                var sheet = file.Workbook.Worksheets.Add("Global OptionSets");
#else
                var sheet = file.Worksheets.Add("Global OptionSets");
#endif

                var ot = new GlobalOptionSetTranslation();
                ot.Export(lcids, sheet, service);
            }

            if (settings.ExportOptionSet && emds.Count > 0)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress(0, "Exporting optionset translations...");
                }

#if NO_GEMBOX
                var sheet = file.Workbook.Worksheets.Add("OptionSets");
#else
                var sheet = file.Worksheets.Add("OptionSets");
#endif
                var ot = new OptionSetTranslation();
                ot.Export(emds, lcids, sheet);
            }

            if (settings.ExportBooleans && emds.Count > 0)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress(0, "Exporting booleans translations...");
                }

#if NO_GEMBOX
                var sheet = file.Workbook.Worksheets.Add("Booleans");
#else
                var sheet = file.Worksheets.Add("Booleans");
#endif

                var bt = new BooleanTranslation();
                bt.Export(emds, lcids, sheet);
            }

            if (settings.ExportViews && emds.Count > 0)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress(0, "Exporting views translations...");
                }

#if NO_GEMBOX
                var sheet = file.Workbook.Worksheets.Add("Views");
#else
                var sheet = file.Add("Views");
#endif
                var vt = new ViewTranslation();
                vt.Export(emds, lcids, sheet, service);
            }

            if ((settings.ExportForms || settings.ExportFormTabs || settings.ExportFormSections || settings.ExportFormFields) && emds.Count > 0)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress(0, "Exporting forms translations...");
                }
                
                var ft = new FormTranslation();
#if NO_GEMBOX
                ft.Export(emds, lcids, file.Workbook, service,
#else
                ft.Export(emds, lcids, file, service,
#endif
                new FormExportOption
                              {
                                  ExportForms = settings.ExportForms,
                                  ExportFormTabs = settings.ExportFormTabs,
                                  ExportFormSections = settings.ExportFormSections,
                                  ExportFormFields = settings.ExportFormFields
                              });
            }

            if (settings.ExportSiteMap)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress(0, "Exporting SiteMap custom labels translations...");
                }

                var st = new SiteMapTranslation();

#if NO_GEMBOX
                st.Export(lcids, file.Workbook, service);
#else
                st.Export(lcids, file, service);
#endif
            }

#if NO_GEMBOX
            file.Save(settings.FilePath);
#else
            file.Save(settings.FilePath, SaveOptions.XlsxDefault);
#endif
        }

        public void Import(string filePath, IOrganizationService service, BackgroundWorker worker = null)
        {
#if NO_GEMBOX
            var stream = File.OpenRead(filePath);
            var file = new ExcelPackage(stream);
#else
            var file = ExcelFile.Load(filePath);
#endif

            var emds = new List<EntityMetadata>();
                
            var forms = new List<Entity>();
            var ft = new FormTranslation();
            var st = new SiteMapTranslation();
            bool hasFormContent = false;
            bool hasSiteMapContent = false;

#if NO_GEMBOX
            foreach (var sheet in file.Workbook.Worksheets)
#else
            foreach (var sheet in file.Worksheets)
#endif
            {
                switch (sheet.Name)
                {
                    case "Entities":
                        if (worker != null && worker.WorkerReportsProgress)
                        {
                            worker.ReportProgress(0, "Importing entities translations...");
                        }

                        var et = new EntityTranslation();
                        et.Import(sheet, emds, service);
                        break;
                    case "Attributes":
                        if (worker != null && worker.WorkerReportsProgress)
                        {
                            worker.ReportProgress(0, "Importing attributes translations...");
                        }

                        var at = new AttributeTranslation();
                        at.Import(sheet, emds, service);
                        break;
                    case "Global OptionSets":
                        if (worker != null && worker.WorkerReportsProgress)
                        {
                            worker.ReportProgress(0, "Importing global optionsets translations...");
                        }

                        var got = new GlobalOptionSetTranslation();
                        got.Import(sheet, service);
                        break;
                    case "OptionSets":
                        if (worker != null && worker.WorkerReportsProgress)
                        {
                            worker.ReportProgress(0, "Importing optionsets translations...");
                        }

                        var ot = new OptionSetTranslation();
                        ot.Import(sheet, service);
                        break;
                    case "Booleans":
                        if (worker != null && worker.WorkerReportsProgress)
                        {
                            worker.ReportProgress(0, "Importing booleans translations...");
                        }

                        var bt = new BooleanTranslation();
                        bt.Import(sheet, service);
                        break;
                    case "Views":
                        if (worker != null && worker.WorkerReportsProgress)
                        {
                            worker.ReportProgress(0, "Importing views translations...");
                        }

                        var vt = new ViewTranslation();
                        vt.Import(sheet, service);
                        break;
                    case "Forms":
                        if (worker != null && worker.WorkerReportsProgress)
                        {
                            worker.ReportProgress(0, "Importing forms translations...");
                        }

                        ft.ImportFormName(sheet, service);
                        break;
                    case "Forms Tabs":
                        ft.PrepareFormTabs(sheet, service, forms);
                        hasFormContent = true;
                        break;
                    case "Forms Sections":
                        ft.PrepareFormSections(sheet, service, forms);
                        hasFormContent = true;
                        break;
                    case "Forms Fields":
                        ft.PrepareFormLabels(sheet, service, forms);
                        hasFormContent = true;
                        break;
                    case "SiteMap Areas":
                        st.PrepareAreas(sheet, service);
                        hasSiteMapContent = true;
                        break;
                    case "SiteMap Groups":
                        st.PrepareGroups(sheet, service);
                        hasSiteMapContent = true;
                        break;
                    case "SiteMap SubAreas":
                        st.PrepareSubAreas(sheet, service);
                        hasSiteMapContent = true;
                        break;
                }

                if (hasFormContent)
                {
                    if (worker != null && worker.WorkerReportsProgress)
                    {
                        worker.ReportProgress(0, "Importing form content translations...");
                    }

                    ft.ImportFormsContent(service, forms);
                }

                if (hasSiteMapContent)
                {
                    if (worker != null && worker.WorkerReportsProgress)
                    {
                        worker.ReportProgress(0, "Importing SiteMap translations...");
                    }

                    st.Import(service);
                }
            }

            if (worker != null && worker.WorkerReportsProgress)
            {
                worker.ReportProgress(0, "Publishing customizations...");
            }

            var paxRequest = new PublishAllXmlRequest();
            service.Execute(paxRequest);
        }
    }
}
