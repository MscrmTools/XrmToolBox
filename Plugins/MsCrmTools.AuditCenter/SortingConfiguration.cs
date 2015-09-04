using System.Windows.Forms;

namespace MsCrmTools.AuditCenter
{
    internal class SortingConfiguration
    {
        public int ColumnIndex { get; set; }
        public ListView List { get; set; }
        public SortOrder Order { get; set; }
    }
}