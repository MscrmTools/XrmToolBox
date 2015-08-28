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
            //var key = new GemBox.LicenseKey.Key();
           // string excelKey = key.ExcelKey37;
            SpreadsheetInfo.SetLicense("E43Y-5VC8-CTZJ-7XN0");
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
                var filters = EntityFilters.Default;
                if (settings.ExportEntities)
                {
                    filters = filters | EntityFilters.Entity;
                }
                if (settings.ExportCustomizedRelationships)
                {
                    filters = filters | EntityFilters.Relationships;
                }
                if (settings.ExportAttributes)
                {
                    filters = filters | EntityFilters.Attributes;
                }

                var request = new RetrieveEntityRequest { LogicalName = entityLogicalName, EntityFilters = filters };
                var response = (RetrieveEntityResponse)service.Execute(request);
                emds.Add(response.EntityMetadata);
            }
#if NO_GEMBOX
            var file = new ExcelPackage(new FileInfo(settings.FilePath));
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
                var et = new EntityTranslation();
                et.Export(emds, lcids, sheet);
                StyleMutator.FontDefaults(sheet);
#else
                var sheet = file.Worksheets.Add("Entities");
                var et = new EntityTranslation();
                et.Export(emds, lcids, sheet);
#endif
            }

            if (settings.ExportAttributes && emds.Count > 0)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress(0, "Exporting attributes translations...");
                }
#if NO_GEMBOX
                var sheet = file.Workbook.Worksheets.Add("Attributes");
                var at = new AttributeTranslation();
                at.Export(emds, lcids, sheet);
                StyleMutator.FontDefaults(sheet);
#else
                var sheet = file.Worksheets.Add("Attributes");
                var at = new AttributeTranslation();
                at.Export(emds, lcids, sheet);
#endif
            }


            if (settings.ExportCustomizedRelationships && emds.Count > 0)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress(0, "Exporting relationships with custom labels translations...");
                }

                #if NO_GEMBOX
                var sheet = file.Workbook.Worksheets.Add("Relationships");
                var rt = new RelationshipTranslation();
                rt.Export(emds, lcids, sheet);
                StyleMutator.FontDefaults(sheet);

                var sheetNn =  file.Workbook.Worksheets.Add("RelationshipsNN");
                var rtNn = new RelationshipNnTranslation();
                rtNn.Export(emds, lcids, sheetNn);
                StyleMutator.FontDefaults(sheetNn);
#else
                var sheet = file.Worksheets.Add("Relationships");
                var rt = new RelationshipTranslation();
                rt.Export(emds, lcids, sheet);

                var sheetNn = file.Worksheets.Add("RelationshipsNN");
                var rtNn = new RelationshipNnTranslation();
                rtNn.Export(emds, lcids, sheetNn);
#endif
            }

            if (settings.ExportGlobalOptionSet)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress(0, "Exporting global optionsets translations...");
                }
#if NO_GEMBOX
                var sheet = file.Workbook.Worksheets.Add("Global OptionSets");
                var ot = new GlobalOptionSetTranslation();
                ot.Export(lcids, sheet, service);
                StyleMutator.FontDefaults(sheet);
#else
                var sheet = file.Worksheets.Add("Global OptionSets");
                var ot = new GlobalOptionSetTranslation();
                ot.Export(lcids, sheet, service);
#endif
            }

            if (settings.ExportOptionSet && emds.Count > 0)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress(0, "Exporting optionset translations...");
                }

#if NO_GEMBOX
                var sheet = file.Workbook.Worksheets.Add("OptionSets");
                var ot = new OptionSetTranslation();
                ot.Export(emds, lcids, sheet);
                StyleMutator.FontDefaults(sheet);
#else
                var sheet = file.Worksheets.Add("OptionSets");
                var ot = new OptionSetTranslation();
                ot.Export(emds, lcids, sheet);
#endif
            }

            if (settings.ExportBooleans && emds.Count > 0)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress(0, "Exporting booleans translations...");
                }

#if NO_GEMBOX
                var sheet = file.Workbook.Worksheets.Add("Booleans");

                var bt = new BooleanTranslation();
                bt.Export(emds, lcids, sheet);
                StyleMutator.FontDefaults(sheet);
#else
                var sheet = file.Worksheets.Add("Booleans");
                
                var bt = new BooleanTranslation();
                bt.Export(emds, lcids, sheet);
#endif
            }

            if (settings.ExportViews && emds.Count > 0)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress(0, "Exporting views translations...");
                }

#if NO_GEMBOX
                var sheet = file.Workbook.Worksheets.Add("Views");
                var vt = new ViewTranslation();
                vt.Export(emds, lcids, sheet, service);
                StyleMutator.FontDefaults(sheet);
#else
                var sheet = file.Worksheets.Add("Views");
                var vt = new ViewTranslation();
                vt.Export(emds, lcids, sheet, service);
#endif
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
            file.Save();
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
                    case "Relationships":
                    {
                        if (worker != null && worker.WorkerReportsProgress)
                        {
                            worker.ReportProgress(0, "Importing Relationships with custom label translations...");
                        }

                        var rt = new RelationshipTranslation();
                        rt.Import(sheet, emds, service);
                        break;
                    }
                    case "RelationshipsNN":
                    {
                        if (worker != null && worker.WorkerReportsProgress)
                        {
                            worker.ReportProgress(0, "Importing NN Relationships with custom label translations...");
                        }

                        var rtNn = new RelationshipNnTranslation();
                        rtNn.Import(sheet, emds, service);
                        break;
                    }
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
