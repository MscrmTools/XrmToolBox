using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GemBox.Spreadsheet;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace MsCrmTools.Translator.AppCode
{
    internal class RelationshipTranslation
    {
        internal void Export(List<EntityMetadata> entities, List<int> languages, ExcelWorksheet sheet)
        {
            var line = 1;

            AddHeader(sheet, languages);
            var exportedRelationships = new List<Guid>();

            foreach (var entity in entities.OrderBy(e => e.LogicalName))
            {
                var relationships = new List<OneToManyRelationshipMetadata>();
                relationships.AddRange(entity.OneToManyRelationships);
                relationships.AddRange(entity.ManyToOneRelationships);

                foreach (var rel in relationships)
                {
                    if (exportedRelationships.Contains(rel.MetadataId.Value))
                    {
                        continue;
                    }
                    exportedRelationships.Add(rel.MetadataId.Value);

                    var cell = 0;

                    if (!rel.AssociatedMenuConfiguration.Behavior.HasValue ||
                         rel.AssociatedMenuConfiguration.Behavior.Value != AssociatedMenuBehavior.UseLabel)
                        continue;
                    
                    // entity1Label
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = rel.ReferencedEntity;
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = rel.MetadataId.Value.ToString("B");
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = rel.SchemaName;
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = rel.ReferencingEntity;

                    foreach (var lcid in languages)
                    {
                        var entity1Label = string.Empty;

                        if (rel.AssociatedMenuConfiguration.Label != null)
                        {
                            var displayNameLabel =
                                rel.AssociatedMenuConfiguration.Label.LocalizedLabels.FirstOrDefault(
                                    l => l.LanguageCode == lcid);
                            if (displayNameLabel != null)
                            {
                                entity1Label = displayNameLabel.Label;
                            }
                        }

                        ZeroBasedSheet.Cell(sheet, line, cell++).Value = entity1Label;
                    }

                    line++;
                }
            }

            // Applying style to cells
            for (int i = 0; i < (4 + languages.Count); i++)
            {
                StyleMutator.TitleCell(ZeroBasedSheet.Cell(sheet, 0, i).Style);
            }

            for (int i = 1; i < line; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    StyleMutator.HighlightedCell(ZeroBasedSheet.Cell(sheet, i, j).Style);
                }
            }
        }

        public void Import(ExcelWorksheet sheet, List<EntityMetadata> emds, IOrganizationService service)
        {
            var rmds = new List<OneToManyRelationshipMetadata>();

            foreach (var row in sheet.Rows.Where(r => r.Index != 0).OrderBy(r => r.Index))
            {
                var rmd = rmds.FirstOrDefault(r => r.MetadataId == new Guid(row.Cells[1].Value.ToString()));
                if (rmd == null)
                {
                    var currentEntity = emds.FirstOrDefault(e => e.LogicalName == row.Cells[0].Value.ToString());
                    if (currentEntity == null)
                    {
                        var request = new RetrieveEntityRequest
                        {
                            LogicalName = row.Cells[0].Value.ToString(),
                            EntityFilters = EntityFilters.Relationships
                        };

                        var response = ((RetrieveEntityResponse) service.Execute(request));
                        currentEntity = response.EntityMetadata;

                        emds.Add(currentEntity);
                    }

                    rmd =
                        currentEntity.OneToManyRelationships.FirstOrDefault(
                            r => r.SchemaName == row.Cells[2].Value.ToString());
                    if (rmd == null)
                    {
                        rmd =
                            currentEntity.ManyToOneRelationships.FirstOrDefault(
                                r => r.SchemaName == row.Cells[2].Value.ToString());
                    }

                    rmds.Add(rmd);
                }

                int columnIndex = 4;
                
                rmd.AssociatedMenuConfiguration.Label = new Label();

                while (row.Cells[columnIndex].Value != null)
                {
                    rmd.AssociatedMenuConfiguration.Label.LocalizedLabels.Add(
                        new LocalizedLabel(row.Cells[columnIndex].Value.ToString(),
                            int.Parse(sheet.Cells[0, columnIndex].Value.ToString())));

                    columnIndex++;
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


        private void AddHeader(ExcelWorksheet sheet, IEnumerable<int> languages)
        {
            var cell = 0;

            ZeroBasedSheet.Cell(sheet, 0, cell++).Value = "Entity";
            ZeroBasedSheet.Cell(sheet, 0, cell++).Value = "Relationship Id";
            ZeroBasedSheet.Cell(sheet, 0, cell++).Value = "Relationship Name";
            ZeroBasedSheet.Cell(sheet, 0, cell++).Value = "Relationship entity";

            foreach (var lcid in languages)
            {
                ZeroBasedSheet.Cell(sheet, 0, cell++).Value = lcid.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}