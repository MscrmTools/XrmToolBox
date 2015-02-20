using System.Drawing;

#if NO_GEMBOX
using OfficeOpenXml.Style;
#else
using GemBox.Spreadsheet;
#endif

namespace MsCrmTools.Translator
{
    public static class StyleMutator
    {

#if NO_GEMBOX
        public static void SetCellColorAndFontWeight(ExcelStyle style, Color color, bool isBold = false)
        {
            style.Fill.BackgroundColor.SetColor(color);
            style.Font.Bold = true;
        }
#else
        public static void SetCellColorAndFontWeight(CellStyle style, Color color, bool isBold = false)
        {
            style.FillPattern.SetSolid(color);

            if (isBold)
            {  
                style.Font.Weight = ExcelFont.BoldWeight;  
            }

        }
#endif
    }
}

