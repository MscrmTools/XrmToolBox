using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using GemBox.Spreadsheet;
using Microsoft.Crm.Sdk;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;

namespace MsCrmTools.Translator.AppCode
{
    public class ViewTranslation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <example>
        /// viewId;entityLogicalName;viewName;ViewType;Type;LCID1;LCID2;...;LCODX
        /// </example>
        /// <param name="entities"></param>
        /// <param name="languages"></param>
        /// <param name="sheet"></param>
        public void Export(List<EntityMetadata> entities, List<int> languages, ExcelWorksheet sheet, IOrganizationService service)
        {
            var line = 1;

            AddHeader(sheet, languages);

            var qe = new QueryExpression("usersettings");
            qe.ColumnSet = new ColumnSet(new[]{"uilanguageid","localeid"});
            qe.Criteria= new FilterExpression();
            qe.Criteria.AddCondition("systemuserid", ConditionOperator.EqualUserId);
            var settings = service.RetrieveMultiple(qe);
            var userSettingLcid = settings[0].GetAttributeValue<int>("uilanguageid");
            var currentSetting = userSettingLcid;

            var crmViews = new List<CrmView>();

            foreach (var lcid in languages)
            {
                if (userSettingLcid != lcid)
                {
                    settings[0]["localeid"] = lcid;
                    settings[0]["uilanguageid"] = lcid;
                    service.Update(settings[0]);
                    currentSetting = lcid;
                }

                foreach (var entity in entities.OrderBy(e => e.LogicalName))
                {
                    if (!entity.MetadataId.HasValue)
                        continue;

                    var views = RetrieveViews(entity.LogicalName, entity.ObjectTypeCode.Value, service);

                    foreach (var view in views)
                    {
                        var crmView = crmViews.FirstOrDefault(cv => cv.Id == view.Id);
                        if (crmView == null)
                        {
                            crmView = new CrmView
                                          {
                                              Id = view.Id,
                                              Entity = view.GetAttributeValue<string>("returnedtypecode"),
                                              Type = view.GetAttributeValue<int>("querytype"),
                                              Names = new Dictionary<int, string>(),
                                              Descriptions = new Dictionary<int, string>()
                                          };
                            crmViews.Add(crmView);
                        }

                        crmView.Names.Add(lcid, view.GetAttributeValue<string>("name"));
                        crmView.Descriptions.Add(lcid, view.GetAttributeValue<string>("description"));
                    }
                }
            }

            foreach (var crmView in crmViews.OrderBy(cv=>cv.Entity).ThenBy(cv=>cv.Type))
            {
                var cell = 0;
                sheet.Cells[line, cell++].Value = crmView.Id.ToString("B");
                sheet.Cells[line, cell++].Value = crmView.Entity;
                sheet.Cells[line, cell++].Value = crmView.Type;
                sheet.Cells[line, cell++].Value = "Name";

                foreach (var lcid in languages)
                {
                    sheet.Cells[line, cell++].Value = crmView.Names.First(n => n.Key == lcid).Value;
                }

                line++;
                cell = 0;
                sheet.Cells[line, cell++].Value = crmView.Id.ToString("B");
                sheet.Cells[line, cell++].Value = crmView.Entity;
                sheet.Cells[line, cell++].Value = crmView.Type;
                sheet.Cells[line, cell++].Value = "Description";

                foreach (var lcid in languages)
                {
                    sheet.Cells[line, cell++].Value = crmView.Descriptions.First(n => n.Key == lcid).Value;
                }
                line++;
            }

            // Applying style to cells
            for (int i = 0; i < (4 + languages.Count); i++)
            {
                sheet.Cells[0, i].Style.FillPattern.SetSolid(Color.PowderBlue);
                sheet.Cells[0, i].Style.Font.Weight = ExcelFont.BoldWeight;
            }

            for (int i = 1; i < line; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    sheet.Cells[i, j].Style.FillPattern.SetSolid(Color.AliceBlue);
                }
            }

            if (userSettingLcid != currentSetting)
            {
                settings[0]["localeid"] = userSettingLcid;
                settings[0]["uilanguageid"] = userSettingLcid;
                service.Update(settings[0]);
            }
        }

        public void Import(ExcelWorksheet sheet, IOrganizationService service)
        {
            var views = new List<Tuple<int, Entity>>();

            foreach (var row in sheet.Rows.Where(r => r.Index != 0).OrderBy(r => r.Index))
            {
                var currentViewId = new Guid(row.Cells[0].Value.ToString());
                var columnIndex = 4;
                while (row.Cells[columnIndex].Value != null)
                {
                    var currentLcid = int.Parse(sheet.Cells[0, columnIndex].Value.ToString());
                    var viewRecord = views.FirstOrDefault(t => t.Item1 == currentLcid && t.Item2.Id == currentViewId);
                    if (viewRecord == null)
                    {
                        viewRecord = new Tuple<int, Entity>(currentLcid, new Entity("savedquery") { Id = currentViewId });
                        views.Add(viewRecord);
                    }

                    if (row.Cells[3].Value.ToString() == "Name")
                    {
                        viewRecord.Item2["name"] = row.Cells[columnIndex].Value.ToString();
                    }
                    else if (row.Cells[3].Value.ToString() == "Description")
                    {
                        viewRecord.Item2["description"] = row.Cells[columnIndex].Value.ToString();
                    }

                    columnIndex++;
                }
            }

            // Retrieve current user language inviewation
            var qe = new QueryExpression("usersettings");
            qe.ColumnSet = new ColumnSet(new[] { "uilanguageid", "localeid" });
            qe.Criteria = new FilterExpression();
            qe.Criteria.AddCondition("systemuserid", ConditionOperator.EqualUserId);
            var settings = service.RetrieveMultiple(qe);
            var userSettingLcid = settings[0].GetAttributeValue<int>("uilanguageid");
            var currentSetting = userSettingLcid;

            var languages = views.Select(f => f.Item1).Distinct().ToList();
            foreach (var lcid in languages)
            {
                // Define correct user language for update
                if (userSettingLcid != lcid)
                {
                    settings[0]["localeid"] = lcid;
                    settings[0]["uilanguageid"] = lcid;
                    service.Update(settings[0]);
                    currentSetting = lcid;
                }

                foreach (var view in views.Where(f => f.Item1 == lcid))
                {
                    service.Update(view.Item2);
                }
            }

            // Reinit user language
            if (userSettingLcid != currentSetting)
            {
                settings[0]["localeid"] = userSettingLcid;
                settings[0]["uilanguageid"] = userSettingLcid;
                service.Update(settings[0]);
            }
        }


        private void AddHeader(ExcelWorksheet sheet, IEnumerable<int> languages)
        {
            var cell = 0;

            sheet.Cells[0, cell++].Value = "View Id";
            sheet.Cells[0, cell++].Value = "Entity Logical Name";
            sheet.Cells[0, cell++].Value = "ViewType";
            sheet.Cells[0, cell++].Value = "Type";

            foreach (var lcid in languages)
            {
                sheet.Cells[0, cell++].Value = lcid.ToString(CultureInfo.InvariantCulture);
            }
        }

        private List<Entity> RetrieveViews(string entityLogicalName, int objectTypeCode, IOrganizationService service)
        {
            try
            {
                var qba = new QueryByAttribute
                {
                    EntityName = "savedquery",
                    ColumnSet = new ColumnSet(true)
                };

                qba.Attributes.Add("returnedtypecode");
                qba.Values.Add(objectTypeCode);

                EntityCollection views = service.RetrieveMultiple(qba);

                return views.Entities.ToList();
            }
            catch (Exception error)
            {
                throw new Exception("Error while retrieving views: " + error.Message);
            }
        }
    }
}
