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
    public class GlobalOptionSetTranslation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <example>
        /// OptionSet Id;OptionSet Name;OptionSetValue;Type;LCID1;LCID2;...;LCIDX
        /// </example>
        /// <param name="languages"></param>
        /// <param name="sheet"></param>
        /// <param name="service"></param>
        public void Export(List<int> languages, ExcelWorksheet sheet, IOrganizationService service)
        {
            var line = 1;

            AddHeader(sheet, languages);

            var request = new RetrieveAllOptionSetsRequest();
            var response = (RetrieveAllOptionSetsResponse) service.Execute(request);

            foreach (var omd in response.OptionSetMetadata)
            {
                if (omd is OptionSetMetadata)
                {
                    var oomd = (OptionSetMetadata) omd;
                    foreach (var option in oomd.Options.OrderBy(o => o.Value))
                    {
                        var cell = 0;
                        sheet.Cells[line, cell++].Value = omd.MetadataId.Value.ToString("B");
                        sheet.Cells[line, cell++].Value = omd.Name;
                        sheet.Cells[line, cell++].Value = option.Value;
                        sheet.Cells[line, cell++].Value = "Label";

                        foreach (var lcid in languages)
                        {
                            var label = string.Empty;

                            var optionLabel = option.Label.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == lcid);
                            if (optionLabel != null)
                            {
                                label = optionLabel.Label;
                            }

                            sheet.Cells[line, cell++].Value = label;
                        }

                        line++;
                        cell = 0;
                        sheet.Cells[line, cell++].Value = omd.MetadataId.Value.ToString("B");
                        sheet.Cells[line, cell++].Value = omd.Name;
                        sheet.Cells[line, cell++].Value = option.Value;
                        sheet.Cells[line, cell++].Value = "Description";

                        foreach (var lcid in languages)
                        {
                            var label = string.Empty;

                            var optionDescription = option.Description.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == lcid);
                            if (optionDescription != null)
                            {
                                label = optionDescription.Label;
                            }

                            sheet.Cells[line, cell++].Value = label;
                        }

                        line++;
                    }
                }
                else if (omd is BooleanOptionSetMetadata)
                {
                    var bomd = (BooleanOptionSetMetadata)omd;

                    var cell = 0;
                    sheet.Cells[line, cell++].Value = omd.MetadataId.Value.ToString("B");
                    sheet.Cells[line, cell++].Value = omd.Name;
                    sheet.Cells[line, cell++].Value = bomd.FalseOption.Value;
                    sheet.Cells[line, cell++].Value = "Label";

                    foreach (var lcid in languages)
                    {
                        var label = string.Empty;

                        if (bomd.FalseOption.Label != null)
                        {
                            var optionLabel =
                                bomd.FalseOption.Label.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == lcid);
                            if (optionLabel != null)
                            {
                                label = optionLabel.Label;
                            }
                        }

                        sheet.Cells[line, cell++].Value = label;
                    }

                    line++;
                    cell = 0;

                    sheet.Cells[line, cell++].Value = omd.MetadataId.Value.ToString("B");
                    sheet.Cells[line, cell++].Value = omd.Name;
                    sheet.Cells[line, cell++].Value = bomd.FalseOption.Value;
                    sheet.Cells[line, cell++].Value = "Description";

                    foreach (var lcid in languages)
                    {
                        var label = string.Empty;

                        if (bomd.FalseOption.Description != null)
                        {
                            var optionLabel =
                                bomd.FalseOption.Description.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == lcid);
                            if (optionLabel != null)
                            {
                                label = optionLabel.Label;
                            }
                        }

                        sheet.Cells[line, cell++].Value = label;
                    }

                    line++;
                    cell = 0;

                    sheet.Cells[line, cell++].Value = omd.MetadataId.Value.ToString("B");
                    sheet.Cells[line, cell++].Value = omd.Name;
                    sheet.Cells[line, cell++].Value = bomd.TrueOption.Value;
                    sheet.Cells[line, cell++].Value = "Label";

                    foreach (var lcid in languages)
                    {
                        var label = string.Empty;

                        if (bomd.TrueOption.Label != null)
                        {
                            var optionLabel =
                                bomd.TrueOption.Label.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == lcid);
                            if (optionLabel != null)
                            {
                                label = optionLabel.Label;
                            }
                        }

                        sheet.Cells[line, cell++].Value = label;
                    }

                    line++;
                    cell = 0;

                    sheet.Cells[line, cell++].Value = omd.MetadataId.Value.ToString("B");
                    sheet.Cells[line, cell++].Value = omd.Name;
                    sheet.Cells[line, cell++].Value = bomd.TrueOption.Value;
                    sheet.Cells[line, cell++].Value = "Description";

                    foreach (var lcid in languages)
                    {
                        var label = string.Empty;

                        if (bomd.TrueOption.Description != null)
                        {
                            var optionLabel =
                                bomd.TrueOption.Description.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == lcid);
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
                                      OptionSetName = row.Cells[1].Value.ToString(),
                                      Value = int.Parse(row.Cells[2].Value.ToString()),
                                      Label = new Label(),
                                      Description = new Label(),
                                      MergeLabels = true
                                  };

                    int columnIndex = 4;

                    if (row.Cells[3].ToString() == "Label")
                    {
                        while (row.Cells[columnIndex].Value != null)
                        {
                            request.Label.LocalizedLabels.Add(new LocalizedLabel(row.Cells[columnIndex].Value.ToString(),int.Parse(sheet.Cells[0, columnIndex].Value.ToString())));
                            columnIndex++;
                        }
                    }
                    else if (row.Cells[3].ToString() == "Description")
                    {
                        while (row.Cells[columnIndex].Value != null)
                        {
                            request.Description.LocalizedLabels.Add(new LocalizedLabel(row.Cells[columnIndex].Value.ToString(),int.Parse(sheet.Cells[0, columnIndex].Value.ToString())));
                            columnIndex++;
                        }
                    }

                    requests.Add(request);
                }
                else
                {
                    int columnIndex = 4;

                    if (row.Cells[3].ToString() == "Label")
                    {
                        while (row.Cells[columnIndex].Value != null)
                        {
                            request.Label.LocalizedLabels.Add(new LocalizedLabel(row.Cells[columnIndex].Value.ToString(),int.Parse(sheet.Cells[0, columnIndex].Value.ToString())));
                            columnIndex++;
                        }
                    }
                    else if (row.Cells[3].ToString() == "Description")
                    {
                        while (row.Cells[columnIndex].Value != null)
                        {
                            request.Description.LocalizedLabels.Add(new LocalizedLabel(row.Cells[columnIndex].Value.ToString(),int.Parse(sheet.Cells[0, columnIndex].Value.ToString())));
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

            sheet.Cells[0, cell++].Value = "OptionSet Id";
            sheet.Cells[0, cell++].Value = "OptionSet Name";
            sheet.Cells[0, cell++].Value = "Value";
            sheet.Cells[0, cell++].Value = "Type";

            foreach (var lcid in languages)
            {
                sheet.Cells[0, cell++].Value = lcid.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}
