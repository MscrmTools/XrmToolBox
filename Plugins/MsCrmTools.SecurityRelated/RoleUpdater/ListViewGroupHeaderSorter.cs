// PROJECT : MsCrmTools.RoleUpdater
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System.Collections.Generic;
using System.Windows.Forms;

namespace MsCrmTools.RoleUpdater
{
    public class ListViewGroupHeaderSorter : IComparer<ListViewGroup>
    {
        private readonly bool _ascending = true;

        public ListViewGroupHeaderSorter(bool ascending)
        {
            _ascending = ascending;
        }

        #region IComparer<ListViewGroup> Members

        public int Compare(ListViewGroup x, ListViewGroup y)
        {
            if (_ascending)
                return string.Compare((x).Header, (y).Header);
            else
                return string.Compare((y).Header, (x).Header);
        }

        #endregion IComparer<ListViewGroup> Members
    }
}