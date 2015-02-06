using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MsCrmTools.ViewLayoutReplicator.Forms
{
    public partial class ErrorList : Form
    {
        private List<Tuple<string, string>> errors;

        public ErrorList(List<Tuple<string, string>> errors)
        {
            this.errors = errors;
            InitializeComponent();
        }

        private void ErrorListLoad(object sender, EventArgs e)
        {
            foreach (var error in errors)
            {
                var item = new ListViewItem(error.Item1);
                item.SubItems.Add(error.Item2);

                lvErrors.Items.Add(item);
            }
        }

        private void BtnCloseClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
