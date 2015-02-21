using System.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Xml;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

#if NO_GEMBOX
using OfficeOpenXml;
#else
using GemBox.Spreadsheet;
#endif

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

                ZeroBasedSheet.Cell(sheet, line, cell++).Value = record.Id.ToString("B");
                ZeroBasedSheet.Cell(sheet, line, cell++).Value = record.GetAttributeValue<string>("entity");
                ZeroBasedSheet.Cell(sheet, line, cell++).Value = record.GetAttributeValue<string>("diffid");

                var xml = new XmlDocument();
                xml.LoadXml(record.GetAttributeValue<string>("rdx"));

                foreach (var lcid in languages)
                {
                    var labelNode = xml.SelectSingleNode(string.Format("LocLabel/Titles/Title[@languagecode='{0}']", lcid));
                    ZeroBasedSheet.Cell(sheet, line, cell++).Value = labelNode == null ? string.Empty : labelNode.Attributes["description"].Value;
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
        public void Import(ExcelWorksheet sheet, IOrganizationService service)
        {
            var rowsCount = sheet.Dimension.Rows;

            for (var rowI = 1; rowI < rowsCount; rowI++)
            {
                var xml = new StringBuilder(string.Format("<LocLabel Id=\"{0}\"><Titles>", ZeroBasedSheet.Cell(sheet, rowI, 2).Value));

                var columnIndex = 3;

                while (ZeroBasedSheet.Cell(sheet, rowI, columnIndex).Value != null)
                {
                    xml.Append(string.Format("<Title description=\"{0}\" languagecode=\"{1}\"/>",
                                             ZeroBasedSheet.Cell(sheet, rowI, columnIndex).Value,
                                             int.Parse(ZeroBasedSheet.Cell(sheet, 0, columnIndex).Value.ToString())));

                    columnIndex++;
                }

                xml.Append("</Titles></LocLabel>");

                var ribbonDiff = new Entity("ribbondiff") { Id = new Guid(ZeroBasedSheet.Cell(sheet, rowI, 0).Value.ToString()) };
                ribbonDiff["rdx"] = xml.ToString();

                service.Update(ribbonDiff);
            }
        }
#else
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
                                             int.Parse(ZeroBasedSheet.Cell(sheet, 0, columnIndex).Value.ToString())));

                    columnIndex++;
                }

                xml.Append("</Titles></LocLabel>");

                var ribbonDiff = new Entity("ribbondiff") {Id = new Guid(row.Cells[0].Value.ToString())};
                ribbonDiff["rdx"] = xml.ToString();

                service.Update(ribbonDiff);
            }
        }
#endif
        private void AddHeader(ExcelWorksheet sheet, IEnumerable<int> languages)
        {
            var cell = 0;

            ZeroBasedSheet.Cell(sheet, 0, cell++).Value = "Ribbon Diff Id";
            ZeroBasedSheet.Cell(sheet, 0, cell++).Value = "Entity Logical Name";
            ZeroBasedSheet.Cell(sheet, 0, cell++).Value = "Ribbon Component";

            foreach (var lcid in languages)
            {
                ZeroBasedSheet.Cell(sheet, 0, cell++).Value = lcid.ToString(CultureInfo.InvariantCulture);
            }
        }

    }
}
