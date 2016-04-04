using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.Translator.AppCode;
using OfficeOpenXml;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MsCrmTools.Translator
{
    public class Engine
    {
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
                if (settings.ExportAttributes || settings.ExportOptionSet || settings.ExportBooleans)
                {
                    filters = filters | EntityFilters.Attributes;
                }

                var request = new RetrieveEntityRequest { LogicalName = entityLogicalName, EntityFilters = filters };
                var response = (RetrieveEntityResponse)service.Execute(request);
                emds.Add(response.EntityMetadata);
            }

            var file = new ExcelPackage();
            file.File = new FileInfo(settings.FilePath);

            if (settings.ExportEntities && emds.Count > 0)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress(0, "Exporting entities translations...");
                }

                var sheet = file.Workbook.Worksheets.Add("Entities");
                var et = new EntityTranslation();
                et.Export(emds, lcids, sheet);
                StyleMutator.FontDefaults(sheet);
            }

            if (settings.ExportAttributes && emds.Count > 0)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress(0, "Exporting attributes translations...");
                }

                var sheet = file.Workbook.Worksheets.Add("Attributes");
                var at = new AttributeTranslation();
                at.Export(emds, lcids, sheet);
                StyleMutator.FontDefaults(sheet);
            }

            if (settings.ExportCustomizedRelationships && emds.Count > 0)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress(0, "Exporting relationships with custom labels translations...");
                }

                var sheet = file.Workbook.Worksheets.Add("Relationships");
                var rt = new RelationshipTranslation();
                rt.Export(emds, lcids, sheet);
                StyleMutator.FontDefaults(sheet);

                var sheetNn = file.Workbook.Worksheets.Add("RelationshipsNN");
                var rtNn = new RelationshipNnTranslation();
                rtNn.Export(emds, lcids, sheetNn);
                StyleMutator.FontDefaults(sheetNn);
            }

            if (settings.ExportGlobalOptionSet)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress(0, "Exporting global optionsets translations...");
                }

                var sheet = file.Workbook.Worksheets.Add("Global OptionSets");
                var ot = new GlobalOptionSetTranslation();
                ot.Export(lcids, sheet, service);
                StyleMutator.FontDefaults(sheet);
            }

            if (settings.ExportOptionSet && emds.Count > 0)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress(0, "Exporting optionset translations...");
                }

                var sheet = file.Workbook.Worksheets.Add("OptionSets");
                var ot = new OptionSetTranslation();
                ot.Export(emds, lcids, sheet);
                StyleMutator.FontDefaults(sheet);
            }

            if (settings.ExportBooleans && emds.Count > 0)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress(0, "Exporting booleans translations...");
                }

                var sheet = file.Workbook.Worksheets.Add("Booleans");

                var bt = new BooleanTranslation();
                bt.Export(emds, lcids, sheet);
                StyleMutator.FontDefaults(sheet);
            }

            if (settings.ExportViews && emds.Count > 0)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress(0, "Exporting views translations...");
                }

                var sheet = file.Workbook.Worksheets.Add("Views");
                var vt = new ViewTranslation();
                vt.Export(emds, lcids, sheet, service);
                StyleMutator.FontDefaults(sheet);
            }

            if (settings.ExportCharts && emds.Count > 0)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress(0, "Exporting Charts translations...");
                }

                var sheet = file.Workbook.Worksheets.Add("Charts");
                var vt = new VisualizationTranslation();
                vt.Export(emds, lcids, sheet, service);
                StyleMutator.FontDefaults(sheet);
            }

            if ((settings.ExportForms || settings.ExportFormTabs || settings.ExportFormSections || settings.ExportFormFields) && emds.Count > 0)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress(0, "Exporting forms translations...");
                }

                var ft = new FormTranslation();

                ft.Export(emds, lcids, file.Workbook, service,
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

                st.Export(lcids, file.Workbook, service);
            }

            if (settings.ExportDashboards)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    worker.ReportProgress(0, "Exporting Dashboards custom labels translations...");
                }

                var st = new DashboardTranslation();

                st.Export(lcids, file.Workbook, service);
            }

            file.Save();

            if (DialogResult.Yes == MessageBox.Show("Do you want to open generated document?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                Process.Start(settings.FilePath);
            }
        }

        public void Import(string filePath, IOrganizationService service, BackgroundWorker worker = null)
        {
            var stream = File.OpenRead(filePath);
            var file = new ExcelPackage(stream);

            var emds = new List<EntityMetadata>();

            var forms = new List<Entity>();
            var ft = new FormTranslation();
            var st = new SiteMapTranslation();
            var db = new DashboardTranslation();
            bool hasFormContent = false;
            bool hasDashboardContent = false;
            bool hasSiteMapContent = false;

            foreach (var sheet in file.Workbook.Worksheets)
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

                    case "Charts":
                        if (worker != null && worker.WorkerReportsProgress)
                        {
                            worker.ReportProgress(0, "Importing charts translations...");
                        }

                        var vt2 = new VisualizationTranslation();
                        vt2.Import(sheet, service);
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

                    case "Dashboards":
                        if (worker != null && worker.WorkerReportsProgress)
                        {
                            worker.ReportProgress(0, "Importing dashboard translations...");
                        }

                        db.ImportFormName(sheet, service);
                        break;

                    case "Dashboards Tabs":
                        db.PrepareFormTabs(sheet, service, forms);
                        hasDashboardContent = true;
                        break;

                    case "Dashboards Sections":
                        db.PrepareFormSections(sheet, service, forms);
                        hasDashboardContent = true;
                        break;

                    case "Dashboards Fields":
                        db.PrepareFormLabels(sheet, service, forms);
                        hasDashboardContent = true;
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

                if (hasDashboardContent)
                {
                    if (worker != null && worker.WorkerReportsProgress)
                    {
                        worker.ReportProgress(0, "Importing dashboard content translations...");
                    }

                    db.ImportFormsContent(service, forms);
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