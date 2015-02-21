﻿using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

#if NO_GEMBOX
using OfficeOpenXml;
#else
using GemBox.Spreadsheet;
#endif

namespace MsCrmTools.Translator.AppCode
{
    public class EntityTranslation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <example>
        /// entityId;entityLogicalName;Type;LCID1;LCID2;...;LCODX
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
                if(!entity.MetadataId.HasValue)
                    continue;
                

                    var cell = 0;

                   ZeroBasedSheet.Cell(sheet, line, cell++).Value = entity.MetadataId.Value.ToString("B");
                   ZeroBasedSheet.Cell(sheet, line, cell++).Value = entity.LogicalName;

                    // DisplayName
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = "DisplayName";

                    foreach (var lcid in languages)
                    {
                        var displayName = string.Empty;

                        if (entity.DisplayName != null)
                        {
                            var displayNameLabel = entity.DisplayName.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == lcid);
                            if (displayNameLabel != null)
                            {
                                displayName = displayNameLabel.Label;
                            }
                        }

                        ZeroBasedSheet.Cell(sheet, line, cell++).Value = displayName;
                    }

                    // Plural Name
                    line++;
                    cell = 0;
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = entity.MetadataId.Value.ToString("B");
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = entity.LogicalName;
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = "DisplayCollectionName";

                    foreach (var lcid in languages)
                    {
                        var collectionName = string.Empty;

                        if (entity.DisplayCollectionName != null)
                        {
                            var collectionNameLabel = entity.DisplayCollectionName.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == lcid);
                            if (collectionNameLabel != null)
                            {
                                collectionName = collectionNameLabel.Label;
                            }
                        }

                        ZeroBasedSheet.Cell(sheet, line, cell++).Value = collectionName;
                    }
                    
                    // Description
                    line++;
                    cell = 0;
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = entity.MetadataId.Value.ToString("B");
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = entity.LogicalName;
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = "Description";

                    foreach (var lcid in languages)
                    {
                        var description = string.Empty;

                        if (entity.Description != null)
                        {
                            var descriptionLabel = entity.Description.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == lcid);
                            if (descriptionLabel != null)
                            {
                                description = descriptionLabel.Label;
                            }
                        }

                        ZeroBasedSheet.Cell(sheet, line, cell++).Value = description;
                    }

                    line++;
            }

            // Applying style to cells
            for (int i = 0; i < (3 + languages.Count); i++)
            {
                StyleMutator.TitleCell(ZeroBasedSheet.Cell(sheet, 0, i).Style);
            }

            for (int i = 1; i < line; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    StyleMutator.HighlightedCell(ZeroBasedSheet.Cell(sheet, i, j).Style);
                }
            }
        }

#if NO_GEMBOX
        public void Import(ExcelWorksheet sheet, List<EntityMetadata> emds, IOrganizationService service)
        {
            var rowsCount = sheet.Dimension.Rows;

            for (var rowI = 1; rowI < rowsCount; rowI++)
            {
                {
                    var emd = emds.FirstOrDefault(e => e.LogicalName == ZeroBasedSheet.Cell(sheet, rowI, 1).Value.ToString());
                    if (emd == null)
                    {
                        var request = new RetrieveEntityRequest
                        {
                            LogicalName = ZeroBasedSheet.Cell(sheet, rowI, 1).Value.ToString(),
                            EntityFilters = EntityFilters.Entity | EntityFilters.Attributes
                        };

                        var response = ((RetrieveEntityResponse) service.Execute(request));
                        emd = response.EntityMetadata;

                        emds.Add(emd);
                    }

                    if (ZeroBasedSheet.Cell(sheet, rowI, 2).Value.ToString() == "DisplayName")
                    {
                        emd.DisplayName = new Label();
                        int columnIndex = 3;

                        while (ZeroBasedSheet.Cell(sheet, rowI, columnIndex).Value != null)
                        {
                            emd.DisplayName.LocalizedLabels.Add(
                                new LocalizedLabel(ZeroBasedSheet.Cell(sheet, rowI, columnIndex).Value.ToString(),
                                    int.Parse(ZeroBasedSheet.Cell(sheet, 0, columnIndex).Value.ToString())));

                            columnIndex++;
                        }
                    }
                    else if (ZeroBasedSheet.Cell(sheet, rowI, 2).Value.ToString() == "DisplayCollectionName")
                    {
                        emd.DisplayCollectionName = new Label();
                        int columnIndex = 3;

                        while (ZeroBasedSheet.Cell(sheet, rowI, columnIndex).Value != null)
                        {
                            emd.DisplayCollectionName.LocalizedLabels.Add(
                                new LocalizedLabel(ZeroBasedSheet.Cell(sheet, rowI, columnIndex).Value.ToString(),
                                    int.Parse(ZeroBasedSheet.Cell(sheet, 0, columnIndex).Value.ToString())));

                            columnIndex++;
                        }
                    }
                    else if (ZeroBasedSheet.Cell(sheet, rowI, 2).Value.ToString() == "Description")
                    {
                        emd.Description = new Label();
                        int columnIndex = 3;

                        while (ZeroBasedSheet.Cell(sheet, rowI, columnIndex).Value != null)
                        {
                            emd.Description.LocalizedLabels.Add(
                                new LocalizedLabel(ZeroBasedSheet.Cell(sheet, rowI, columnIndex).Value.ToString(),
                                    int.Parse(ZeroBasedSheet.Cell(sheet, 0, columnIndex).Value.ToString())));

                            columnIndex++;
                        }
                    }
                }

                foreach (var emd in emds.Where(e => e.IsRenameable.Value))
                {
                    var request = new UpdateEntityRequest {Entity = emd};
                    service.Execute(request);
                }
            }
        }
#else
        public void Import(ExcelWorksheet sheet, List<EntityMetadata> emds, IOrganizationService service)
        {
            foreach (ExcelRow row in sheet.Rows.Where(r => r.Index != 0).OrderBy(r => r.Index))
            {
                var emd = emds.FirstOrDefault(e => e.LogicalName == row.Cells[1].Value.ToString());
                if (emd == null)
                {
                    var request = new RetrieveEntityRequest
                                  {
                                      LogicalName = row.Cells[1].Value.ToString(),
                                      EntityFilters = EntityFilters.Entity | EntityFilters.Attributes
                                  };

                    var response = ((RetrieveEntityResponse) service.Execute(request));
                    emd = response.EntityMetadata;

                    emds.Add(emd);
                }

                if (row.Cells[2].Value.ToString() == "DisplayName")
                {
                    emd.DisplayName = new Label();
                    int columnIndex = 3;

                    while (row.Cells[columnIndex].Value != null)
                    {
                        emd.DisplayName.LocalizedLabels.Add(new LocalizedLabel(row.Cells[columnIndex].Value.ToString(), int.Parse(sheet.Cells[0, columnIndex].Value.ToString())));

                        columnIndex++;
                    }
                }
                else if (row.Cells[2].Value.ToString() == "DisplayCollectionName")
                {
                    emd.DisplayCollectionName = new Label();
                    int columnIndex = 3;

                    while (row.Cells[columnIndex].Value != null)
                    {
                        emd.DisplayCollectionName.LocalizedLabels.Add(new LocalizedLabel(row.Cells[columnIndex].Value.ToString(), int.Parse(sheet.Cells[0, columnIndex].Value.ToString())));

                        columnIndex++;
                    }
                }
                else if (row.Cells[2].Value.ToString() == "Description")
                {
                    emd.Description = new Label();
                    int columnIndex = 3;

                    while (row.Cells[columnIndex].Value != null)
                    {
                        emd.Description.LocalizedLabels.Add(new LocalizedLabel(row.Cells[columnIndex].Value.ToString(), int.Parse(sheet.Cells[0, columnIndex].Value.ToString())));

                        columnIndex++;
                    }
                }
            }

            foreach (var emd in emds.Where(e=>e.IsRenameable.Value))
            {
                var request = new UpdateEntityRequest {Entity = emd};
                service.Execute(request);
            }
        }
#endif
        private void AddHeader(ExcelWorksheet sheet, IEnumerable<int> languages)
        {
            var cell = 0;

            ZeroBasedSheet.Cell(sheet, 0, cell++).Value = "Entity Id";
            ZeroBasedSheet.Cell(sheet, 0, cell++).Value = "Entity Logical Name";
            ZeroBasedSheet.Cell(sheet, 0, cell++).Value = "Type";

            foreach (var lcid in languages)
            {
                ZeroBasedSheet.Cell(sheet, 0, cell++).Value = lcid.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}
