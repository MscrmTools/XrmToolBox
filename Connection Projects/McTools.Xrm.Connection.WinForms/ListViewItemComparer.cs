using System;
using System.Collections;
using System.Windows.Forms;

namespace McTools.Xrm.Connection.WinForms
{
    internal class GroupComparer : IComparer
    {
        public int Compare(object objA, object objB)
        {
            return String.Compare(((ListViewGroup)objA).Header, ((ListViewGroup)objB).Header, StringComparison.Ordinal);
        }
    }

    /// <summary>
    /// Compares two listview items for sorting
    /// </summary>
    internal class ListViewItemComparer : IComparer
    {
        #region Variables

        /// <summary>
        /// Index of sorting column
        /// </summary>
        private int col;

        /// <summary>
        /// Sort order
        /// </summary>
        private SortOrder innerOrder;

        #endregion Variables

        #region Constructors

        /// <summary>
        /// Initializes a new instance of class ListViewItemComparer
        /// </summary>
        public ListViewItemComparer()
        {
            this.col = 0;
            this.innerOrder = SortOrder.Ascending;
        }

        /// <summary>
        /// Initializes a new instance of class ListViewItemComparer
        /// </summary>
        /// <param name="column">Index of sorting column</param>
        /// <param name="order">Sort order</param>
        public ListViewItemComparer(int column, SortOrder order)
        {
            this.col = column;
            this.innerOrder = order;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Compare tow objects
        /// </summary>
        /// <param name="x">object 1</param>
        /// <param name="y">object 2</param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            return this.Compare((ListViewItem)x, (ListViewItem)y);
        }

        /// <summary>
        /// Compare tow listview items
        /// </summary>
        /// <param name="x">Listview item 1</param>
        /// <param name="y">Listview item 2</param>
        /// <returns></returns>
        public int Compare(ListViewItem x, ListViewItem y)
        {
            if (this.innerOrder == SortOrder.Ascending)
            {
                return String.Compare(x.SubItems[this.col].Text, y.SubItems[this.col].Text);
            }
            else
            {
                return String.Compare(y.SubItems[this.col].Text, x.SubItems[this.col].Text);
            }
        }

        #endregion Methods
    }
}