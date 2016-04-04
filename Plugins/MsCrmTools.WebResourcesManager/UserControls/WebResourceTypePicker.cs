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

                foreach (CheckBox cb in groupBox2.Controls.Cast<CheckBox>().Where(cb => cb.Checked && cb.Name != chkAll.Name))
                {
                    list.AddRange(cb.Tag.ToString().Split('|'));
                }

                return list;
            }
        }

        private void chkAll_CheckedChanged(object sender, System.EventArgs e)
        {
            chkCss.Checked = chkAll.Checked;
            chkGif.Checked = chkAll.Checked;
            chkHtml.Checked = chkAll.Checked;
            chkIco.Checked = chkAll.Checked;
            chkJavaScript.Checked = chkAll.Checked;
            chkJpeg.Checked = chkAll.Checked;
            chkPng.Checked = chkAll.Checked;
            chkXap.Checked = chkAll.Checked;
            chkXml.Checked = chkAll.Checked;
            chkXsl.Checked = chkAll.Checked;

            chkCss.Enabled = !chkAll.Checked;
            chkGif.Enabled = !chkAll.Checked;
            chkHtml.Enabled = !chkAll.Checked;
            chkIco.Enabled = !chkAll.Checked;
            chkJavaScript.Enabled = !chkAll.Checked;
            chkJpeg.Enabled = !chkAll.Checked;
            chkPng.Enabled = !chkAll.Checked;
            chkXap.Enabled = !chkAll.Checked;
            chkXml.Enabled = !chkAll.Checked;
            chkXsl.Enabled = !chkAll.Checked;
        }
    }
}