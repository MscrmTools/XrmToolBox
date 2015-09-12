// PROJECT : MsCrmTools.RoleUpdater
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System.Collections.Generic;
using System.Windows.Forms;

namespace MsCrmTools.PrivDiscover.AppCode
{
    public class ListViewGroupSorter
    {
        internal ListView _listview;

        internal ListViewGroupSorter(ListView listview)
        {
            _listview = listview;
        }

        public static implicit operator ListView(ListViewGroupSorter sorter)
        {
            return sorter._listview;
        }

        public static implicit operator ListViewGroupSorter(ListView listview)
        {
            return new ListViewGroupSorter(listview);
        }

        public static bool operator !=(ListView listview, ListViewGroupSorter sorter)
        {
            return listview != sorter._listview;
        }

        public static bool operator ==(ListView listview, ListViewGroupSorter sorter)
        {
            return listview == sorter._listview;
        }

        public void SortGroups(bool ascending)
        {
            _listview.BeginUpdate();
            List<ListViewGroup> lvgs = new List<ListViewGroup>();
            foreach (ListViewGroup lvg in _listview.Groups)
                lvgs.Add(lvg);
            _listview.Groups.Clear();
            lvgs.Sort(new ListViewGroupHeaderSorter(ascending));
            _listview.Groups.AddRange(lvgs.ToArray());
            _listview.EndUpdate();
        }

        #region overridden methods

        public override bool Equals(object obj)
        {
            return _listview.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _listview.GetHashCode();
        }

        public override string ToString()
        {
            return _listview.ToString();
        }

        #endregion overridden methods
    }
}