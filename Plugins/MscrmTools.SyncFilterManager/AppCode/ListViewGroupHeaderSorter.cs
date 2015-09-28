using System;
using System.Collections.Generic;
using System.Windows.Forms;

public class ListViewGroupHeaderSorter : IComparer<ListViewGroup>
{
    private readonly bool ascending = true;

    public ListViewGroupHeaderSorter(bool ascending)
    {
        this.ascending = ascending;
    }

    #region IComparer<ListViewGroup> Members

    public int Compare(ListViewGroup x, ListViewGroup y)
    {
        if (ascending)
            return String.Compare(x.Header, y.Header, StringComparison.OrdinalIgnoreCase);

        return String.Compare(y.Header, x.Header, StringComparison.OrdinalIgnoreCase);
    }

    #endregion IComparer<ListViewGroup> Members
}