using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MsCrmTools.WebResourcesManager.UserControls
{
    public partial class WebResourceTypePicker : UserControl
    {
        public WebResourceTypePicker()
        {
            InitializeComponent();
        }

        public List<string> CheckedExtensions
        {
            get
            {
                var list = new List<string>();

                foreach (CheckBox cb in groupBox2.Controls.Cast<CheckBox>().Where(cb => cb.Checked))
                {
                    list.AddRange(cb.Tag.ToString().Split('|'));
                }

                return list;
            }
        }
    }
}
