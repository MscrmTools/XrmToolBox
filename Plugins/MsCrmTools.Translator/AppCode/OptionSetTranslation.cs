using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Label = Microsoft.Xrm.Sdk.Label;

#if NO_GEMBOX
using OfficeOpenXml;
#else
using GemBox.Spreadsheet;
#endif

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
                StyleMutator.SetCellColorAndFontWeight(sheet.Cells[0, i].Style, Color.PowderBlue, isBold:true);
            }

            for (int i = 1; i < line; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    StyleMutator.SetCellColorAndFontWeight(sheet.Cells[0, i].Style, Color.AliceBlue);
                }
            }
        }

#if NO_GEMBOX
        public void Import(ExcelWorksheet sheet, IOrganizationService service)
        {
            var requests = new List<UpdateOptionValueRequest>();

            var rowsCount = sheet.Dimension.Rows;

            for (var rowI = 1; rowI < rowsCount; rowI++)
            {
                UpdateOptionValueRequest request =
                    requests
                    .FirstOrDefault(
                        r => r.OptionSetName == sheet.Cells[rowI, 1].Value.ToString() &&
                        r.Value == int.Parse(sheet.Cells[rowI, 4].Value.ToString()));

                if (request == null)
                {
                    request = new UpdateOptionValueRequest
                    {
                        AttributeLogicalName = sheet.Cells[rowI, 2].Value.ToString(),
                        EntityLogicalName = sheet.Cells[rowI, 1].Value.ToString(),
                        Value = int.Parse(sheet.Cells[rowI, 4].Value.ToString()),
                        Label = new Label(),
                        Description = new Label(),
                        MergeLabels = true
                    };

                    int columnIndex = 6;

                    if (sheet.Cells[rowI, 5].Value.ToString() == "Label")
                    {
                        // WTF: QUESTIONABLE DELETION: row.Cells.Count() > columnIndex && 
                        while (sheet.Cells[rowI, columnIndex] != null && sheet.Cells[rowI, columnIndex].Value != null)
                        {
                            var sLcid = sheet.Cells[0, columnIndex].Value.ToString();
                            var sLabel = sheet.Cells[rowI, columnIndex].Value.ToString();

                            if (sLcid.Length > 0 && sLabel.Length > 0)
                            {
                                request.Label.LocalizedLabels.Add(new LocalizedLabel(sLabel, int.Parse(sLcid)));
                            }
                            columnIndex++;
                        }
                    }
                    else if (sheet.Cells[rowI, 5].Value.ToString() == "Description")
                    {
                        // WTF: QUESTIONABLE DELETION: row.Cells.Count() > columnIndex && 
                        while (sheet.Cells[rowI, columnIndex] != null && sheet.Cells[rowI, columnIndex].Value != null)
                        {
                            var sLcid = sheet.Cells[0, columnIndex].Value.ToString();
                            var sLabel = sheet.Cells[rowI, columnIndex].Value.ToString();

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
                    int columnIndex = 6;

                    if (sheet.Cells[rowI, 5].Value.ToString() == "Label")
                    {
                        // WTF: QUESTIONABLE DELETION: row.Cells.Count() > columnIndex && 
                        while (sheet.Cells[rowI, columnIndex] != null && sheet.Cells[rowI, columnIndex].Value != null)
                        {
                            var sLcid = sheet.Cells[0, columnIndex].Value.ToString();
                            var sLabel = sheet.Cells[rowI, columnIndex].Value.ToString();

                            if (sLcid.Length > 0 && sLabel.Length > 0)
                            {
                                request.Label.LocalizedLabels.Add(new LocalizedLabel(sLabel, int.Parse(sLcid)));
                            }
                            columnIndex++;
                        }
                    }
                    else if (sheet.Cells[rowI, 5].Value.ToString() == "Description")
                    {
                        // WTF: QUESTIONABLE DELETION: row.Cells.Count() > columnIndex && 
                        while (sheet.Cells[rowI, columnIndex] != null && sheet.Cells[rowI, columnIndex].Value != null)
                        {
                            var sLcid = sheet.Cells[0, columnIndex].Value.ToString();
                            var sLabel = sheet.Cells[rowI, columnIndex].Value.ToString();

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
#else
        public void Import(ExcelWorksheet sheet, IOrganizationService service)
        {
            var requests = new List<UpdateOptionValueRequest>();
            
            foreach (ExcelRow row in sheet.Rows.Where(r => r.Index != 0).OrderBy(r => r.Index))
            {
                UpdateOptionValueRequest request = requests.FirstOrDefault(r => r.OptionSetName == row.Cells[1].Value.ToString() && r.Value == int.Parse(row.Cells[4].Value.ToString()));
                if (request == null)
                {
                    request = new UpdateOptionValueRequest
                                  {
                                      AttributeLogicalName = row.Cells[2].Value.ToString(),
                                      EntityLogicalName = row.Cells[1].Value.ToString(),
                                      Value = int.Parse(row.Cells[4].Value.ToString()),
                                      Label = new Label(),
                                      Description = new Label(),
                                      MergeLabels = true
                                  };

                    int columnIndex = 6;
                    
                    if (row.Cells[5].Value.ToString() == "Label")
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
                    else if (row.Cells[5].Value.ToString() == "Description")
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
                    int columnIndex = 6;

                    if (row.Cells[5].Value.ToString() == "Label")
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
                    else if (row.Cells[5].Value.ToString() == "Description")
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
#endif
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
