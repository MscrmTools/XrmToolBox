using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MsCrmTools.ChartManager.Helpers;

namespace MsCrmTools.ChartManager.Forms
{
    public partial class ErrorsListForm : Form
    {
        private readonly List<ChartDefinition> charts;

        public ErrorsListForm(List<ChartDefinition> charts)
        {
            InitializeComponent();

            this.charts = charts;
        }

        private void ErrorsListForm_Load(object sender, EventArgs e)
        {
            foreach (var chart in charts)
            {
                foreach (var error in chart.Errors)
                {
                    var item = new ListViewItem(chart.Name);
                    item.SubItems.Add(error);

                    listView1.Items.Add(item);
                }
            }
        }
    }
}
