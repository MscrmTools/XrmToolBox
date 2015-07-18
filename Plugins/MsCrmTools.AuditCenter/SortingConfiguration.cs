using System.Windows.Forms;

namespace MsCrmTools.AuditCenter
{
    internal class SortingConfiguration
    {
        public ListView List { get; set; }

        public int ColumnIndex { get; set; }

        public SortOrder Order { get; set; }
    }
}
