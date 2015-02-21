#if NO_GEMBOX
using OfficeOpenXml;
#else
using GemBox.Spreadsheet;
#endif

namespace MsCrmTools.Translator
{
    public static class ZeroBasedSheet
    {
#if NO_GEMBOX
        public static ExcelRange Cell(ExcelWorksheet sheet, int x, int y)
        {
            return sheet.Cells[x + 1, y + 1];
        }
#else
        public static ExcelCell Cell(ExcelWorksheet sheet, int x, int y)
        {
            return sheet.Cells[x, y];
        }
#endif
    }
}