using System.Drawing;

#if NO_GEMBOX
using ExcelWorksheet = OfficeOpenXml.ExcelWorksheet;
using OfficeOpenXml.Style;
#else
using GemBox.Spreadsheet;
using ExcelWorksheet = GemBox.Spreadsheet.ExcelWorksheet;
#endif

namespace MsCrmTools.Translator
{
    public static class StyleMutator
    {
        private static readonly Color TitleColor = Color.PowderBlue;
        private static readonly Color RowColor = Color.AliceBlue;

#if NO_GEMBOX
        public static void TitleCell(ExcelStyle style)
        {
            style.Fill.PatternType = ExcelFillStyle.Solid;
            style.Fill.BackgroundColor.SetColor(TitleColor);
            style.Font.Bold = true;
        }

        public static void HighlightedCell(ExcelStyle style)
        {
            style.Fill.PatternType = ExcelFillStyle.Solid;
            style.Fill.BackgroundColor.SetColor(RowColor);
        }

        public static void FontDefaults(ExcelWorksheet sheet)
        {
            sheet.Cells.Style.Font.Name = "Arial";
            sheet.Cells.Style.Font.Size = 10;
        }
#else
        public static void TitleCell(CellStyle style)
        {
            style.FillPattern.SetSolid(TitleColor);
            style.Font.Weight = ExcelFont.BoldWeight;  
        }

        public static void HighlightedCell(CellStyle style)
        {
            style.FillPattern.SetSolid(RowColor);
        }

        public static void FontDefaults(ExcelWorksheet sheet)
        {
        }
#endif
    }
}

