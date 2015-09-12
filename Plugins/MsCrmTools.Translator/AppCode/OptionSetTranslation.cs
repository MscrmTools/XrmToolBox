using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Label = Microsoft.Xrm.Sdk.Label;

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

            foreach (var entity in entities.OrderBy(e => e.LogicalName))
            {
                foreach (var attribute in entity.Attributes.OrderBy(a => a.LogicalName))
                {
                    var cell = 0;

                    if (attribute.AttributeType == null
                        || attribute.AttributeType.Value != AttributeTypeCode.Picklist
                        && attribute.AttributeType.Value != AttributeTypeCode.State
                        && attribute.AttributeType.Value != AttributeTypeCode.Status
                        || !attribute.MetadataId.HasValue)
                        continue;

                    OptionSetMetadata omd = null;

                    switch (attribute.AttributeType.Value)
                    {
                        case AttributeTypeCode.Picklist:
                            omd = ((PicklistAttributeMetadata)attribute).OptionSet;
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
                        ZeroBasedSheet.Cell(sheet, line, cell++).Value = attribute.MetadataId.Value.ToString("B");
                        ZeroBasedSheet.Cell(sheet, line, cell++).Value = entity.LogicalName;
                        ZeroBasedSheet.Cell(sheet, line, cell++).Value = attribute.LogicalName;
                        ZeroBasedSheet.Cell(sheet, line, cell++).Value = attribute.AttributeType.Value.ToString();
                        ZeroBasedSheet.Cell(sheet, line, cell++).Value = option.Value;
                        ZeroBasedSheet.Cell(sheet, line, cell++).Value = "Label";

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

                            ZeroBasedSheet.Cell(sheet, line, cell++).Value = label;
                        }

                        line++;
                        cell = 0;

                        ZeroBasedSheet.Cell(sheet, line, cell++).Value = attribute.MetadataId.Value.ToString("B");
                        ZeroBasedSheet.Cell(sheet, line, cell++).Value = entity.LogicalName;
                        ZeroBasedSheet.Cell(sheet, line, cell++).Value = attribute.LogicalName;
                        ZeroBasedSheet.Cell(sheet, line, cell++).Value = attribute.AttributeType.Value.ToString();
                        ZeroBasedSheet.Cell(sheet, line, cell++).Value = option.Value;
                        ZeroBasedSheet.Cell(sheet, line, cell++).Value = "Description";

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

                            ZeroBasedSheet.Cell(sheet, line, cell++).Value = label;
                        }

                        line++;
                        cell = 0;
                    }
                }
            }

            // Applying style to cells
            for (int i = 0; i < (6 + languages.Count); i++)
            {
                StyleMutator.TitleCell(ZeroBasedSheet.Cell(sheet, 0, i).Style);
            }

            for (int i = 1; i < line; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    StyleMutator.HighlightedCell(ZeroBasedSheet.Cell(sheet, 0, i).Style);
                }
            }
        }

        public void Import(ExcelWorksheet sheet, IOrganizationService service)
        {
            var requests = new List<UpdateOptionValueRequest>();

            var rowsCount = sheet.Dimension.Rows;

            for (var rowI = 1; rowI < rowsCount; rowI++)
            {
                UpdateOptionValueRequest request =
                    requests
                    .FirstOrDefault(
                        r => r.OptionSetName == ZeroBasedSheet.Cell(sheet, rowI, 1).Value.ToString() &&
                        r.Value == int.Parse(ZeroBasedSheet.Cell(sheet, rowI, 4).Value.ToString()));

                if (request == null)
                {
                    request = new UpdateOptionValueRequest
                    {
                        AttributeLogicalName = ZeroBasedSheet.Cell(sheet, rowI, 2).Value.ToString(),
                        EntityLogicalName = ZeroBasedSheet.Cell(sheet, rowI, 1).Value.ToString(),
                        Value = int.Parse(ZeroBasedSheet.Cell(sheet, rowI, 4).Value.ToString()),
                        Label = new Label(),
                        Description = new Label(),
                        MergeLabels = true
                    };

                    int columnIndex = 6;

                    if (ZeroBasedSheet.Cell(sheet, rowI, 5).Value.ToString() == "Label")
                    {
                        // WTF: QUESTIONABLE DELETION: row.Cells.Count() > columnIndex &&
                        while (ZeroBasedSheet.Cell(sheet, rowI, columnIndex) != null && ZeroBasedSheet.Cell(sheet, rowI, columnIndex).Value != null)
                        {
                            var sLcid = ZeroBasedSheet.Cell(sheet, 0, columnIndex).Value.ToString();
                            var sLabel = ZeroBasedSheet.Cell(sheet, rowI, columnIndex).Value.ToString();

                            if (sLcid.Length > 0 && sLabel.Length > 0)
                            {
                                request.Label.LocalizedLabels.Add(new LocalizedLabel(sLabel, int.Parse(sLcid)));
                            }
                            columnIndex++;
                        }
                    }
                    else if (ZeroBasedSheet.Cell(sheet, rowI, 5).Value.ToString() == "Description")
                    {
                        // WTF: QUESTIONABLE DELETION: row.Cells.Count() > columnIndex &&
                        while (ZeroBasedSheet.Cell(sheet, rowI, columnIndex) != null && ZeroBasedSheet.Cell(sheet, rowI, columnIndex).Value != null)
                        {
                            var sLcid = ZeroBasedSheet.Cell(sheet, 0, columnIndex).Value.ToString();
                            var sLabel = ZeroBasedSheet.Cell(sheet, rowI, columnIndex).Value.ToString();

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

                    if (ZeroBasedSheet.Cell(sheet, rowI, 5).Value.ToString() == "Label")
                    {
                        // WTF: QUESTIONABLE DELETION: row.Cells.Count() > columnIndex &&
                        while (ZeroBasedSheet.Cell(sheet, rowI, columnIndex) != null && ZeroBasedSheet.Cell(sheet, rowI, columnIndex).Value != null)
                        {
                            var sLcid = ZeroBasedSheet.Cell(sheet, 0, columnIndex).Value.ToString();
                            var sLabel = ZeroBasedSheet.Cell(sheet, rowI, columnIndex).Value.ToString();

                            if (sLcid.Length > 0 && sLabel.Length > 0)
                            {
                                request.Label.LocalizedLabels.Add(new LocalizedLabel(sLabel, int.Parse(sLcid)));
                            }
                            columnIndex++;
                        }
                    }
                    else if (ZeroBasedSheet.Cell(sheet, rowI, 5).Value.ToString() == "Description")
                    {
                        // WTF: QUESTIONABLE DELETION: row.Cells.Count() > columnIndex &&
                        while (ZeroBasedSheet.Cell(sheet, rowI, columnIndex) != null && ZeroBasedSheet.Cell(sheet, rowI, columnIndex).Value != null)
                        {
                            var sLcid = ZeroBasedSheet.Cell(sheet, 0, columnIndex).Value.ToString();
                            var sLabel = ZeroBasedSheet.Cell(sheet, rowI, columnIndex).Value.ToString();

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

            ZeroBasedSheet.Cell(sheet, 0, cell++).Value = "Attribute Id";
            ZeroBasedSheet.Cell(sheet, 0, cell++).Value = "Entity Logical Name";
            ZeroBasedSheet.Cell(sheet, 0, cell++).Value = "Attribute Logical Name";
            ZeroBasedSheet.Cell(sheet, 0, cell++).Value = "Attribute Type";
            ZeroBasedSheet.Cell(sheet, 0, cell++).Value = "Value";
            ZeroBasedSheet.Cell(sheet, 0, cell++).Value = "Type";

            foreach (var lcid in languages)
            {
                ZeroBasedSheet.Cell(sheet, 0, cell++).Value = lcid.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}