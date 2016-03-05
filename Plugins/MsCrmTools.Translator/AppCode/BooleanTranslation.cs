using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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

                    var bAmd = (BooleanAttributeMetadata)attribute;

                    if (bAmd.OptionSet.IsGlobal.Value)
                        continue;

                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = attribute.MetadataId.Value.ToString("B");
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = entity.LogicalName;
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = attribute.LogicalName;
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = bAmd.OptionSet.FalseOption.Value;
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = "Label";

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

                        ZeroBasedSheet.Cell(sheet, line, cell++).Value = label;
                    }

                    line++;
                    cell = 0;

                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = attribute.MetadataId.Value.ToString("B");
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = entity.LogicalName;
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = attribute.LogicalName;
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = bAmd.OptionSet.FalseOption.Value;
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = "Description";

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

                        ZeroBasedSheet.Cell(sheet, line, cell++).Value = label;
                    }

                    line++;
                    cell = 0;

                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = attribute.MetadataId.Value.ToString("B");
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = entity.LogicalName;
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = attribute.LogicalName;
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = bAmd.OptionSet.TrueOption.Value;
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = "Label";

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

                        ZeroBasedSheet.Cell(sheet, line, cell++).Value = label;
                    }

                    line++;
                    cell = 0;

                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = attribute.MetadataId.Value.ToString("B");
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = entity.LogicalName;
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = attribute.LogicalName;
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = bAmd.OptionSet.TrueOption.Value;
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = "Description";

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

                        ZeroBasedSheet.Cell(sheet, line, cell++).Value = label;
                    }

                    line++;
                }
            }

            // Applying style to cells
            for (int i = 0; i < (5 + languages.Count); i++)
            {
                StyleMutator.TitleCell(ZeroBasedSheet.Cell(sheet, 0, i).Style);
            }

            for (int i = 1; i < line; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    StyleMutator.HighlightedCell(ZeroBasedSheet.Cell(sheet, i, j).Style);
                }
            }
        }

        public void Import(ExcelWorksheet sheet, IOrganizationService service)
        {
            var requests = new List<UpdateOptionValueRequest>();

            var rowsCount = sheet.Dimension.Rows;
            var cellsCount = sheet.Dimension.Columns;
            for (var rowI = 1; rowI < rowsCount; rowI++)
            {
                UpdateOptionValueRequest request = requests.FirstOrDefault(r => r.OptionSetName == ZeroBasedSheet.Cell(sheet, rowI, 1).Value.ToString());
                if (request == null)
                {
                    request = new UpdateOptionValueRequest
                    {
                        AttributeLogicalName = ZeroBasedSheet.Cell(sheet, rowI, 2).Value.ToString(),
                        EntityLogicalName = ZeroBasedSheet.Cell(sheet, rowI, 1).Value.ToString(),
                        Value = int.Parse(ZeroBasedSheet.Cell(sheet, rowI, 3).Value.ToString()),
                        Label = new Label(),
                        Description = new Label(),
                        MergeLabels = true
                    };

                    int columnIndex = 5;

                    if (ZeroBasedSheet.Cell(sheet, rowI, 4).Value.ToString() == "Label")
                    {
                        while (columnIndex < cellsCount)
                        {
                            if (ZeroBasedSheet.Cell(sheet, rowI, columnIndex).Value != null)
                            {
                                var lcid = int.Parse(ZeroBasedSheet.Cell(sheet, 0, columnIndex).Value.ToString());
                                var label = ZeroBasedSheet.Cell(sheet, rowI, columnIndex).Value.ToString();
                                request.Label.LocalizedLabels.Add(new LocalizedLabel(label, lcid));
                            }

                            columnIndex++;
                        }
                    }
                    else if (ZeroBasedSheet.Cell(sheet, rowI, 4).Value.ToString() == "Description")
                    {
                        while (columnIndex < cellsCount)
                        {
                            if (ZeroBasedSheet.Cell(sheet, rowI, columnIndex).Value != null)
                            {
                                var lcid = int.Parse(ZeroBasedSheet.Cell(sheet, 0, columnIndex).Value.ToString());
                                var label = ZeroBasedSheet.Cell(sheet, rowI, columnIndex).Value.ToString();
                                request.Description.LocalizedLabels.Add(new LocalizedLabel(label, lcid));
                            }

                            columnIndex++;
                        }
                    }

                    requests.Add(request);
                }
                else
                {
                    int columnIndex = 5;

                    if (ZeroBasedSheet.Cell(sheet, rowI, 4).Value.ToString() == "Label")
                    {
                        while (columnIndex < cellsCount)
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
                    else if (ZeroBasedSheet.Cell(sheet, rowI, 4).Value.ToString() == "Description")
                    {
                        while (columnIndex < cellsCount)
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
            ZeroBasedSheet.Cell(sheet, 0, cell++).Value = "Value";
            ZeroBasedSheet.Cell(sheet, 0, cell++).Value = "Type";

            foreach (var lcid in languages)
            {
                ZeroBasedSheet.Cell(sheet, 0, cell++).Value = lcid.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}