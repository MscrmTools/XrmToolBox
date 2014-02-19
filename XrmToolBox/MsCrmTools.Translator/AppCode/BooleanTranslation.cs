using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using GemBox.Spreadsheet;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace MsCrmTools.Translator.AppCode
{
    public class BooleanTranslation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <example>
        /// attributeId;entityLogicalName;attributeLogicalName;OptionSetValue;LCID1;LCID2;...;LCODX
        /// </example>
        /// <param name="entities"></param>
        /// <param name="languages"></param>
        /// <param name="sheet"></param>
        public void Export(List<EntityMetadata> entities, List<int> languages, ExcelWorksheet sheet)
        {
            var line = 1;

            AddHeader(sheet, languages);

            foreach (var entity in entities.OrderBy(e => e.LogicalName))
            {
                foreach (var attribute in entity.Attributes.OrderBy(a => a.LogicalName))
                {
                    var cell = 0;

                    if (attribute.AttributeType == null
                        || attribute.AttributeType.Value != AttributeTypeCode.Boolean
                        || !attribute.MetadataId.HasValue)
                        continue;

                    var bAmd = (BooleanAttributeMetadata) attribute;

                    if (bAmd.OptionSet.IsGlobal.Value)
                        continue;

                    sheet.Cells[line, cell++].Value = attribute.MetadataId.Value.ToString("B");
                    sheet.Cells[line, cell++].Value = entity.LogicalName;
                    sheet.Cells[line, cell++].Value = attribute.LogicalName;
                    sheet.Cells[line, cell++].Value = bAmd.OptionSet.FalseOption.Value;
                    sheet.Cells[line, cell++].Value = "Label";

                    foreach (var lcid in languages)
                    {
                        var label = string.Empty;

                        if (bAmd.OptionSet.FalseOption.Label != null)
                        {
                            var optionLabel =
                                bAmd.OptionSet.FalseOption.Label.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == lcid);
                            if (optionLabel != null)
                            {
                                label = optionLabel.Label;
                            }
                        }

                        sheet.Cells[line, cell++].Value = label;
                    }

                    line++;
                    cell = 0;

                    sheet.Cells[line, cell++].Value = attribute.MetadataId.Value.ToString("B");
                    sheet.Cells[line, cell++].Value = entity.LogicalName;
                    sheet.Cells[line, cell++].Value = attribute.LogicalName;
                    sheet.Cells[line, cell++].Value = bAmd.OptionSet.FalseOption.Value;
                    sheet.Cells[line, cell++].Value = "Description";

                    foreach (var lcid in languages)
                    {
                        var label = string.Empty;

                        if (bAmd.OptionSet.FalseOption.Description != null)
                        {
                            var optionLabel =
                                bAmd.OptionSet.FalseOption.Description.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == lcid);
                            if (optionLabel != null)
                            {
                                label = optionLabel.Label;
                            }
                        }

                        sheet.Cells[line, cell++].Value = label;
                    }

                    line++;
                    cell = 0;

                    sheet.Cells[line, cell++].Value = attribute.MetadataId.Value.ToString("B");
                    sheet.Cells[line, cell++].Value = entity.LogicalName;
                    sheet.Cells[line, cell++].Value = attribute.LogicalName;
                    sheet.Cells[line, cell++].Value = bAmd.OptionSet.TrueOption.Value;
                    sheet.Cells[line, cell++].Value = "Label";

                    foreach (var lcid in languages)
                    {
                        var label = string.Empty;

                        if (bAmd.OptionSet.TrueOption.Label != null)
                        {
                            var optionLabel =
                                bAmd.OptionSet.TrueOption.Label.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == lcid);
                            if (optionLabel != null)
                            {
                                label = optionLabel.Label;
                            }
                        }

                        sheet.Cells[line, cell++].Value = label;
                    }

                    line++;
                    cell = 0;

                    sheet.Cells[line, cell++].Value = attribute.MetadataId.Value.ToString("B");
                    sheet.Cells[line, cell++].Value = entity.LogicalName;
                    sheet.Cells[line, cell++].Value = attribute.LogicalName;
                    sheet.Cells[line, cell++].Value = bAmd.OptionSet.TrueOption.Value;
                    sheet.Cells[line, cell++].Value = "Description";

                    foreach (var lcid in languages)
                    {
                        var label = string.Empty;

                        if (bAmd.OptionSet.TrueOption.Description != null)
                        {
                            var optionLabel =
                                bAmd.OptionSet.TrueOption.Description.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == lcid);
                            if (optionLabel != null)
                            {
                                label = optionLabel.Label;
                            }
                        }

                        sheet.Cells[line, cell++].Value = label;
                    }

                    line++;
                }
            }

            // Applying style to cells
            for (int i = 0; i < (5 + languages.Count); i++)
            {
                sheet.Cells[0, i].Style.FillPattern.SetSolid(Color.PowderBlue);
                sheet.Cells[0, i].Style.Font.Weight = ExcelFont.BoldWeight;
            }

            for (int i = 1; i < line; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    sheet.Cells[i, j].Style.FillPattern.SetSolid(Color.AliceBlue);
                }
            }
        }

        public void Import(ExcelWorksheet sheet, IOrganizationService service)
        {
            var requests = new List<UpdateOptionValueRequest>();

            foreach (ExcelRow row in sheet.Rows.Where(r => r.Index != 0).OrderBy(r => r.Index))
            {
                UpdateOptionValueRequest request = requests.FirstOrDefault(r => r.OptionSetName == row.Cells[1].Value.ToString());
                if (request == null)
                {
                    request = new UpdateOptionValueRequest
                                  {
                                      AttributeLogicalName = row.Cells[2].Value.ToString(),
                                      EntityLogicalName = row.Cells[1].Value.ToString(),
                                      Value = int.Parse(row.Cells[3].Value.ToString()),
                                      Label = new Label(),
                                      Description = new Label(),
                                      MergeLabels = true
                                  };

                    int columnIndex = 5;


                    if (row.Cells[4].Value.ToString() == "Label")
                    {
                        while (row.Cells.Count() > columnIndex && row.Cells[columnIndex] != null && row.Cells[columnIndex].Value != null)
                        {
                            var sLcid = sheet.Cells[0, columnIndex].Value.ToString();
                            var sLabel = row.Cells[columnIndex].Value.ToString();

                            if (sLcid.Length > 0 && sLabel.Length > 0)
                            {
                                request.Label.LocalizedLabels.Add(new LocalizedLabel(sLabel, int.Parse(sLcid)));
                            }
                            columnIndex++;
                        }
                    }
                    else if (row.Cells[4].Value.ToString() == "Description")
                    {
                        while (row.Cells.Count() > columnIndex && row.Cells[columnIndex] != null && row.Cells[columnIndex].Value != null)
                        {
                            var sLcid = sheet.Cells[0, columnIndex].Value.ToString();
                            var sLabel = row.Cells[columnIndex].Value.ToString();

                            if (sLcid.Length > 0 && sLabel.Length > 0)
                            {
                                request.Description.LocalizedLabels.Add(new LocalizedLabel(sLabel, int.Parse(sLcid)));
                            }
                            columnIndex++;
                        }
                    }

                    requests.Add(request);
                }
                else
                {
                    int columnIndex = 5;

                    if (row.Cells[4].Value.ToString() == "Label")
                    {
                        while (row.Cells.Count() > columnIndex && row.Cells[columnIndex] != null && row.Cells[columnIndex].Value != null)
                        {
                            var sLcid = sheet.Cells[0, columnIndex].Value.ToString();
                            var sLabel = row.Cells[columnIndex].Value.ToString();

                            if (sLcid.Length > 0 && sLabel.Length > 0)
                            {
                                request.Label.LocalizedLabels.Add(new LocalizedLabel(sLabel, int.Parse(sLcid)));
                            }
                            columnIndex++;
                        }
                    }
                    else if (row.Cells[4].Value.ToString() == "Description")
                    {
                        while (row.Cells.Count() > columnIndex && row.Cells[columnIndex] != null && row.Cells[columnIndex].Value != null)
                        {
                            var sLcid = sheet.Cells[0, columnIndex].Value.ToString();
                            var sLabel = row.Cells[columnIndex].Value.ToString();

                            if (sLcid.Length > 0 && sLabel.Length > 0)
                            {
                                request.Description.LocalizedLabels.Add(new LocalizedLabel(sLabel, int.Parse(sLcid)));
                            }
                            columnIndex++;
                        }
                    }
                }
            }

            foreach (var request in requests)
            {
                service.Execute(request);
            }
        }

        private void AddHeader(ExcelWorksheet sheet, IEnumerable<int> languages)
        {
            var cell = 0;

            sheet.Cells[0, cell++].Value = "Attribute Id";
            sheet.Cells[0, cell++].Value = "Entity Logical Name";
            sheet.Cells[0, cell++].Value = "Attribute Logical Name";
            sheet.Cells[0, cell++].Value = "Value";
            sheet.Cells[0, cell++].Value = "Type";

            foreach (var lcid in languages)
            {
                sheet.Cells[0, cell++].Value = lcid.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}
