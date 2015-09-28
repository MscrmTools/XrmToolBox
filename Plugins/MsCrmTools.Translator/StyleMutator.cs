using OfficeOpenXml.Style;
using System.Drawing;
using ExcelWorksheet = OfficeOpenXml.ExcelWorksheet;

namespace MsCrmTools.Translator
{
    public static class StyleMutator
    {
        private static readonly Color RowColor = Color.AliceBlue;
        private static readonly Color TitleColor = Color.PowderBlue;

        public static void FontDefaults(ExcelWorksheet sheet)
        {
            sheet.Cells.Style.Font.Name = "Arial";
            sheet.Cells.Style.Font.Size = 10;
        }

        public static void HighlightedCell(ExcelStyle style)
        {
            style.Fill.PatternType = ExcelFillStyle.Solid;
            style.Fill.BackgroundColor.SetColor(RowColor);
        }

        public static void TitleCell(ExcelStyle style)
        {
            style.Fill.PatternType = ExcelFillStyle.Solid;
            style.Fill.BackgroundColor.SetColor(TitleColor);
            style.Font.Bold = true;
        }
    }
}