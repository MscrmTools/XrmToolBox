using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MsCrmTools.Translator.AppCode
{
    internal class RelationshipNnTranslation
    {
        public void Import(ExcelWorksheet sheet, List<EntityMetadata> emds, IOrganizationService service)
        {
            var rmds = new List<ManyToManyRelationshipMetadata>();

            var rowsCount = sheet.Dimension.Rows;
            var cellsCount = sheet.Dimension.Columns;
            for (var rowI = 1; rowI < rowsCount; rowI++)
            {
                var rmd = rmds.FirstOrDefault(r => r.MetadataId == new Guid(ZeroBasedSheet.Cell(sheet, rowI, 1).Value.ToString()));
                if (rmd == null)
                {
                    var currentEntity = emds.FirstOrDefault(e => e.LogicalName == ZeroBasedSheet.Cell(sheet, rowI, 0).Value.ToString());
                    if (currentEntity == null)
                    {
                        var request = new RetrieveEntityRequest
                        {
                            LogicalName = ZeroBasedSheet.Cell(sheet, rowI, 0).Value.ToString(),
                            EntityFilters = EntityFilters.Relationships
                        };

                        var response = ((RetrieveEntityResponse)service.Execute(request));
                        currentEntity = response.EntityMetadata;

                        emds.Add(currentEntity);
                    }

                    rmd = currentEntity.ManyToManyRelationships.FirstOrDefault(r => r.IntersectEntityName == ZeroBasedSheet.Cell(sheet, rowI, 2).Value.ToString());
                    rmds.Add(rmd);
                }

                int columnIndex = 5;

                if (ZeroBasedSheet.Cell(sheet, rowI, 4).Value.ToString() == "Entity1")
                {
                    rmd.Entity1AssociatedMenuConfiguration.Label = new Label();

                    while (columnIndex < cellsCount)
                    {
                        if (ZeroBasedSheet.Cell(sheet, rowI, columnIndex).Value != null)
                        {
                            var lcid = int.Parse(ZeroBasedSheet.Cell(sheet, 0, columnIndex).Value.ToString());
                            var label = ZeroBasedSheet.Cell(sheet, rowI, columnIndex).Value.ToString();

                            rmd.Entity1AssociatedMenuConfiguration.Label.LocalizedLabels.Add(new LocalizedLabel(label, lcid));
                        }

                        columnIndex++;
                    }
                }
                else if (ZeroBasedSheet.Cell(sheet, rowI, columnIndex).ToString() == "Entity2")
                {
                    rmd.Entity2AssociatedMenuConfiguration.Label = new Label();

                    while (columnIndex < cellsCount)
                    {
                        if (ZeroBasedSheet.Cell(sheet, rowI, columnIndex).Value != null)
                        {
                            var lcid = int.Parse(ZeroBasedSheet.Cell(sheet, 0, columnIndex).Value.ToString());
                            var label = ZeroBasedSheet.Cell(sheet, rowI, columnIndex).Value.ToString();

                            rmd.Entity2AssociatedMenuConfiguration.Label.LocalizedLabels.Add(new LocalizedLabel(label, lcid));
                        }
                        columnIndex++;
                    }
                }
            }

            foreach (var rmd in rmds)
            {
                var request = new UpdateRelationshipRequest
                {
                    Relationship = rmd,
                };
                service.Execute(request);
            }
        }

        internal void Export(List<EntityMetadata> entities, List<int> languages, ExcelWorksheet sheet)
        {
            var line = 1;
            var exportedRelationships = new List<Guid>();

            AddHeader(sheet, languages);

            foreach (var entity in entities.OrderBy(e => e.LogicalName))
            {
                foreach (var rel in entity.ManyToManyRelationships.ToList())
                {
                    if (exportedRelationships.Contains(rel.MetadataId.Value))
                    {
                        continue;
                    }
                    exportedRelationships.Add(rel.MetadataId.Value);

                    var cell = 0;

                    if ((!rel.Entity1AssociatedMenuConfiguration.Behavior.HasValue || rel.Entity1AssociatedMenuConfiguration.Behavior.Value != AssociatedMenuBehavior.UseLabel)
                        && (!rel.Entity2AssociatedMenuConfiguration.Behavior.HasValue || rel.Entity2AssociatedMenuConfiguration.Behavior.Value != AssociatedMenuBehavior.UseLabel))
                        continue;

                    // entity1Label
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = rel.Entity2LogicalName;
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = rel.MetadataId.Value.ToString("B");
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = rel.IntersectEntityName;
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = rel.Entity1LogicalName;
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = "Entity1";

                    foreach (var lcid in languages)
                    {
                        var entity1Label = string.Empty;

                        if (rel.Entity1AssociatedMenuConfiguration.Label != null)
                        {
                            var displayNameLabel =
                                rel.Entity1AssociatedMenuConfiguration.Label.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == lcid);
                            if (displayNameLabel != null)
                            {
                                entity1Label = displayNameLabel.Label;
                            }
                        }

                        ZeroBasedSheet.Cell(sheet, line, cell++).Value = entity1Label;
                    }

                    // Description
                    line++;
                    cell = 0;
                    // entity2Label
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = rel.Entity1LogicalName;
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = rel.MetadataId.Value.ToString("B");
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = rel.IntersectEntityName;
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = rel.Entity2LogicalName;
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = "Entity2";

                    foreach (var lcid in languages)
                    {
                        var entity2Label = string.Empty;

                        if (rel.Entity2AssociatedMenuConfiguration.Label != null)
                        {
                            var displayNameLabel =
                                rel.Entity2AssociatedMenuConfiguration.Label.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == lcid);
                            if (displayNameLabel != null)
                            {
                                entity2Label = displayNameLabel.Label;
                            }
                        }

                        ZeroBasedSheet.Cell(sheet, line, cell++).Value = entity2Label;
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

        private void AddHeader(ExcelWorksheet sheet, IEnumerable<int> languages)
        {
            var cell = 0;

            ZeroBasedSheet.Cell(sheet, 0, cell++).Value = "Entity";
            ZeroBasedSheet.Cell(sheet, 0, cell++).Value = "Relationship Id";
            ZeroBasedSheet.Cell(sheet, 0, cell++).Value = "Relationship Intersect Entity";
            ZeroBasedSheet.Cell(sheet, 0, cell++).Value = "Relationship entity";
            ZeroBasedSheet.Cell(sheet, 0, cell++).Value = "Type";

            foreach (var lcid in languages)
            {
                ZeroBasedSheet.Cell(sheet, 0, cell++).Value = lcid.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}