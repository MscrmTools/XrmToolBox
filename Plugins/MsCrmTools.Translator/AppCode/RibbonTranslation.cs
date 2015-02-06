using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;
using GemBox.Spreadsheet;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace MsCrmTools.Translator.AppCode
{
    public class RibbonTranslation
    {
        public void Export(List<int> languages, ExcelWorksheet sheet, bool onlyUnmanaged, IOrganizationService service)
        {
            var qe = new QueryByAttribute("ribbondiff") {ColumnSet = new ColumnSet(true)};

            if (onlyUnmanaged)
            {
                qe.Attributes.AddRange("difftype", "ismanaged");
                qe.Values.AddRange(3, false);
            }
            else
            {
                qe.Attributes.AddRange("difftype");
                qe.Values.AddRange(3);
            }
            qe.AddOrder("entity", OrderType.Ascending);

            var records = service.RetrieveMultiple(qe);

            var line = 1;

            AddHeader(sheet, languages);
            
            foreach (var record in records.Entities)
            {
                var cell = 0;

                sheet.Cells[line, cell++].Value = record.Id.ToString("B");
                sheet.Cells[line, cell++].Value = record.GetAttributeValue<string>("entity");
                sheet.Cells[line, cell++].Value = record.GetAttributeValue<string>("diffid");

                var xml = new XmlDocument();
                xml.LoadXml(record.GetAttributeValue<string>("rdx"));

                foreach (var lcid in languages)
                {
                    var labelNode = xml.SelectSingleNode(string.Format("LocLabel/Titles/Title[@languagecode='{0}']", lcid));
                    sheet.Cells[line, cell++].Value = labelNode == null ? string.Empty : labelNode.Attributes["description"].Value;
                }

                line++;
            }

            // Applying style to cells
            for (int i = 0; i < (3 + languages.Count); i++)
            {
                sheet.Cells[0, i].Style.FillPattern.SetSolid(Color.PowderBlue);
                sheet.Cells[0, i].Style.Font.Weight = ExcelFont.BoldWeight;
            }

            for (int i = 1; i < line; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    sheet.Cells[i, j].Style.FillPattern.SetSolid(Color.AliceBlue);
                }
            }
        }

        public void Import(ExcelWorksheet sheet, IOrganizationService service)
        {
            foreach (ExcelRow row in sheet.Rows.Where(r => r.Index != 0).OrderBy(r => r.Index))
            {
                var xml = new StringBuilder(string.Format("<LocLabel Id=\"{0}\"><Titles>", row.Cells[2].Value));

                var columnIndex = 3;

                while (row.Cells[columnIndex].Value != null)
                {
                    xml.Append(string.Format("<Title description=\"{0}\" languagecode=\"{1}\"/>",
                                             row.Cells[columnIndex].Value,
                                             int.Parse(sheet.Cells[0, columnIndex].Value.ToString())));

                    columnIndex++;
                }

                xml.Append("</Titles></LocLabel>");

                var ribbonDiff = new Entity("ribbondiff") {Id = new Guid(row.Cells[0].Value.ToString())};
                ribbonDiff["rdx"] = xml.ToString();

                service.Update(ribbonDiff);
            }
        }

        private void AddHeader(ExcelWorksheet sheet, IEnumerable<int> languages)
        {
            var cell = 0;

            sheet.Cells[0, cell++].Value = "Ribbon Diff Id";
            sheet.Cells[0, cell++].Value = "Entity Logical Name";
            sheet.Cells[0, cell++].Value = "Ribbon Component";

            foreach (var lcid in languages)
            {
                sheet.Cells[0, cell++].Value = lcid.ToString(CultureInfo.InvariantCulture);
            }
        }

    }
}
