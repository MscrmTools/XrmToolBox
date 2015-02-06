// PROJECT : MsCrmTools.AttributeBulkUpdater
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System.Collections.Generic;
using System.Windows.Forms;

namespace MsCrmTools.AttributeBulkUpdater
{
    public class UpdateSettings
    {
        internal List<ListViewItem> Items { get; set; }
        internal bool UpdateValidForAdvancedFind { get; set; }
        internal bool UpdateAuditIsEnabled { get; set; }
    }
}
