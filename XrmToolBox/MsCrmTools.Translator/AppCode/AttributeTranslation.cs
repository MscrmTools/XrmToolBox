using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using GemBox.Spreadsheet;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;

namespace MsCrmTools.Translator.AppCode
{
    public class AttributeTranslation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <example>
        /// attributeId;entityLogicalName;attributeLogicalName;Type;LCID1;LCID2;...;LCODX
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
                        || attribute.AttributeType.Value == AttributeTypeCode.BigInt
                        || attribute.AttributeType.Value == AttributeTypeCode.CalendarRules
                        || attribute.AttributeType.Value == AttributeTypeCode.EntityName
                        || attribute.AttributeType.Value == AttributeTypeCode.ManagedProperty
                        || attribute.AttributeType.Value == AttributeTypeCode.Uniqueidentifier
                        || attribute.AttributeType.Value == AttributeTypeCode.Virtual
                        || attribute.AttributeOf != null
                        || !attribute.MetadataId.HasValue
                        || !attribute.IsRenameable.Value)
                        continue;

                    if (attribute.DisplayName != null && attribute.DisplayName.LocalizedLabels.All(l => string.IsNullOrEmpty(l.Label)))
                        continue;

                    sheet.Cells[line, cell++].Value = attribute.MetadataId.Value.ToString("B");
                    sheet.Cells[line, cell++].Value = entity.LogicalName;
                    sheet.Cells[line, cell++].Value = attribute.LogicalName;

                    // DisplayName
                    sheet.Cells[line, cell++].Value = "DisplayName";

                    foreach (var lcid in languages)
                    {
                        var displayName = string.Empty;

                        if (attribute.DisplayName != null)
                        {
                            var displayNameLabel = attribute.DisplayName.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == lcid);
                            if (displayNameLabel != null)
                            {
                                displayName = displayNameLabel.Label;
                            }
                        }

                        sheet.Cells[line, cell++].Value = displayName;
                    }
                    
                    // Description
                    line++;
                    cell = 0;
                    sheet.Cells[line, cell++].Value = attribute.MetadataId.Value.ToString("B");
                    sheet.Cells[line, cell++].Value = entity.LogicalName;
                    sheet.Cells[line, cell++].Value = attribute.LogicalName;
                    sheet.Cells[line, cell++].Value = "Description";

                    foreach (var lcid in languages)
                    {
                        var description = string.Empty;

                        if (attribute.Description != null)
                        {
                            var descriptionLabel = attribute.Description.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == lcid);
                            if (descriptionLabel != null)
                            {
                                description = descriptionLabel.Label;
                            }
                        }

                        sheet.Cells[line, cell++].Value = description;
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

        public void Import(ExcelWorksheet sheet, List<EntityMetadata> emds, IOrganizationService service)
        {
            var amds = new List<MasterAttribute>();

            foreach (ExcelRow row in sheet.Rows.Where(r => r.Index != 0).OrderBy(r => r.Index))
            {
                var amd = amds.FirstOrDefault(a => a.Amd.MetadataId == new Guid(row.Cells[0].Value.ToString()));
                if (amd == null)
                {
                    var currentEntity = emds.FirstOrDefault(e => e.LogicalName == row.Cells[1].Value.ToString());
                    if (currentEntity == null)
                    {
                        var request = new RetrieveEntityRequest
                        {
                            LogicalName = row.Cells[1].Value.ToString(),
                            EntityFilters = EntityFilters.Entity | EntityFilters.Attributes
                        };

                        var response = ((RetrieveEntityResponse)service.Execute(request));
                        currentEntity = response.EntityMetadata;

                        emds.Add(currentEntity);
                    }

                    amd = new MasterAttribute();
                    amd.Amd = currentEntity.Attributes.FirstOrDefault(a => a.LogicalName == row.Cells[2].Value.ToString());
                    amds.Add(amd);
                }

                int columnIndex = 4;

                if (row.Cells[3].Value.ToString() == "DisplayName")
                {
                    amd.Amd.DisplayName = new Label();

                    while (row.Cells[columnIndex].Value != null)
                    {
                        amd.Amd.DisplayName.LocalizedLabels.Add(new LocalizedLabel(row.Cells[columnIndex].Value.ToString(), int.Parse(sheet.Cells[0, columnIndex].Value.ToString())));

                        columnIndex++;
                    }
                }
                else if (row.Cells[3].Value.ToString() == "Description")
                {
                    amd.Amd.Description = new Label();

                    while (row.Cells[columnIndex].Value != null)
                    {
                        amd.Amd.Description.LocalizedLabels.Add(new LocalizedLabel(row.Cells[columnIndex].Value.ToString(), int.Parse(sheet.Cells[0, columnIndex].Value.ToString())));

                        columnIndex++;
                    }
                }
            }

            foreach (var amd in amds)
            {
                if (amd.Amd.DisplayName.LocalizedLabels.All(l => string.IsNullOrEmpty(l.Label))
                    || amd.Amd.IsRenameable.Value == false)
                    continue;

                var request = new UpdateAttributeRequest { Attribute = amd.Amd, EntityName = amd.Amd.EntityLogicalName};
                service.Execute(request);
            }
        }

        private void AddHeader(ExcelWorksheet sheet, IEnumerable<int> languages)
        {
            var cell = 0;

            sheet.Cells[0, cell++].Value = "Attribute Id";
            sheet.Cells[0, cell++].Value = "Entity Logical Name";
            sheet.Cells[0, cell++].Value = "Attribute Logical Name";
            sheet.Cells[0, cell++].Value = "Type";

            foreach (var lcid in languages)
            {
                sheet.Cells[0, cell++].Value = lcid.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}
