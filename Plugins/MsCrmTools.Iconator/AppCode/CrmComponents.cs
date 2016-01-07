using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MsCrmTools.Iconator.AppCode
{
    internal class CrmComponents
    {
        public CrmComponents()
        {
            Entities = new List<ListViewItem>();
            Icons16 = new List<ListViewItem>();
            Icons32 = new List<ListViewItem>();
            IconsOthers = new List<ListViewItem>();
            Images16 = new List<Image>();
            Images32 = new List<Image>();
            ImagesOthers = new List<Image>();
        }

        public List<ListViewItem> Entities { get; set; }
        public List<ListViewItem> Icons16 { get; set; }
        public List<ListViewItem> Icons32 { get; set; }
        public List<ListViewItem> IconsOthers { get; set; }
        public List<Image> Images16 { get; set; }
        public List<Image> Images32 { get; set; }
        public List<Image> ImagesOthers { get; set; }
    }
}