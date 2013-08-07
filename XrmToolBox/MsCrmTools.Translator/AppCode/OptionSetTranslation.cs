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
    public class OptionSetTranslation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <example>
        /// attributeId;entityLogicalName;attributeLogicalName;OptionSetValue;LCID1;LCID2;...;LCIDX
        /// </example>
        /// <param name="entities"></param>
        /// <param name="languages"></param>
        /// <param name="sheet"></param>
        public void Export(List<EntityMetadata> entities, List<int> languages, ExcelWorksheet sheet)
        {
            var line = 1;

            AddHeader(sheet, languages);

            foreach (var entity in entities.OrderBy(e=>e.LogicalName))
            {
                foreach (var attribute in entity.Attributes.OrderBy(a=>a.LogicalName))
                {
                    var cell = 0;

                    if(attribute.AttributeType == null
                        || attribute.AttributeType.Value != AttributeTypeCode.Picklist
                        && attribute.AttributeType.Value != AttributeTypeCode.State
                        && attribute.AttributeType.Value != AttributeTypeCode.Status
                        || !attribute.MetadataId.HasValue)
                        continue;

                    OptionSetMetadata omd = null;

                    switch (attribute.AttributeType.Value)
                    {
                        case AttributeTypeCode.Picklist:
                            omd = ((PicklistAttributeMetadata) attribute).OptionSet;
                            break;
                        case AttributeTypeCode.State:
                            omd = ((StateAttributeMetadata)attribute).OptionSet;
                            break;
                        case AttributeTypeCode.Status:
                            omd = ((StatusAttributeMetadata)attribute).OptionSet;
                            break;
                    }

                    if (omd.IsGlobal.Value)
                        continue;
                   
                    foreach (var option in omd.Options.OrderBy(o => o.Value))
                    {
                        sheet.Cells[line, cell++].Value = attribute.MetadataId.Value.ToString("B");
                        sheet.Cells[line, cell++].Value = entity.LogicalName;
                        sheet.Cells[line, cell++].Value = attribute.LogicalName;
                        sheet.Cells[line, cell++].Value = attribute.AttributeType.Value.ToString();
                        sheet.Cells[line, cell++].Value = option.Value;
                        sheet.Cells[line, cell++].Value = "Label";

                        foreach (var lcid in languages)
                        {
                            var label = string.Empty;

                            if (option.Label != null)
                            {
                                var optionLabel = option.Label.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == lcid);
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
                        sheet.Cells[line, cell++].Value = attribute.AttributeType.Value.ToString();
                        sheet.Cells[line, cell++].Value = option.Value;
                        sheet.Cells[line, cell++].Value = "Description";

                        foreach (var lcid in languages)
                        {
                            var label = string.Empty;

                            if (option.Description != null)
                            {
                                var optionLabel = option.Description.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == lcid);
                                if (optionLabel != null)
                                {
                                    label = optionLabel.Label;
                                }
                            }

                            sheet.Cells[line, cell++].Value = label;
                        }

                        line++;
                        cell = 0;
                    }
                }
            }

            // Applying style to cells
            for (int i = 0; i < (6 + languages.Count); i++)
            {
                sheet.Cells[0, i].Style.FillPattern.SetSolid(Color.PowderBlue);
                sheet.Cells[0, i].Style.Font.Weight = ExcelFont.BoldWeight;
            }

            for (int i = 1; i < line; i++)
            {
                for (int j = 0; j < 6; j++)
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
                                      Value = int.Parse(row.Cells[4].Value.ToString()),
                                      Label = new Label(),
                                      MergeLabels = true
                                  };

                    int columnIndex = 6;


                    if (row.Cells[5].ToString() == "Label")
                    {
                        while (row.Cells[columnIndex].Value != null)
                        {
                            request.Label.LocalizedLabels.Add(
                                new LocalizedLabel(row.Cells[columnIndex].Value.ToString(),
                                                   int.Parse(sheet.Cells[0, columnIndex].Value.ToString())));
                            columnIndex++;
                        }
                    }
                    else if (row.Cells[5].ToString() == "Description")
                    {
                        while (row.Cells[columnIndex].Value != null)
                        {
                            request.Description.LocalizedLabels.Add(
                                new LocalizedLabel(row.Cells[columnIndex].Value.ToString(),
                                                   int.Parse(sheet.Cells[0, columnIndex].Value.ToString())));
                            columnIndex++;
                        }
                    }

                    requests.Add(request);
                }
                else
                {
                    int columnIndex = 6;

                    if (row.Cells[5].ToString() == "Label")
                    {
                        while (row.Cells[columnIndex].Value != null)
                        {
                            request.Label.LocalizedLabels.Add(
                                new LocalizedLabel(row.Cells[columnIndex].Value.ToString(),
                                                   int.Parse(sheet.Cells[0, columnIndex].Value.ToString())));
                            columnIndex++;
                        }
                    }
                    else if (row.Cells[5].ToString() == "Description")
                    {
                        while (row.Cells[columnIndex].Value != null)
                        {
                            request.Description.LocalizedLabels.Add(
                                new LocalizedLabel(row.Cells[columnIndex].Value.ToString(),
                                                   int.Parse(sheet.Cells[0, columnIndex].Value.ToString())));
                            columnIndex++;
                        }
                    }
                }
            }
        }

        private void AddHeader(ExcelWorksheet sheet, IEnumerable<int> languages)
        {
            var cell = 0;

            sheet.Cells[0, cell++].Value = "Attribute Id";
            sheet.Cells[0, cell++].Value = "Entity Logical Name";
            sheet.Cells[0, cell++].Value = "Attribute Logical Name";
            sheet.Cells[0, cell++].Value = "Attribute Type";
            sheet.Cells[0, cell++].Value = "Value";
            sheet.Cells[0, cell++].Value = "Type";

            foreach (var lcid in languages)
            {
                sheet.Cells[0, cell++].Value = lcid.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}
