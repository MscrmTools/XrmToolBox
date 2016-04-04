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
    public partial class OverwriteConfirmationDialog : Form
    {
        private readonly List<ChartDefinition> charts;

        public OverwriteConfirmationDialog(List<ChartDefinition> charts)
        {
            InitializeComponent();

            this.charts = charts;
        }

        private void ErrorsListForm_Load(object sender, EventArgs e)
        {
            foreach (var chart in charts.Where(c => c.Exists))
            {
                var item = new ListViewItem(chart.Name) {Tag = chart};
                item.Checked = true;

                lvCharts.Items.Add(item);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvCharts.Items)
            {
                ((ChartDefinition) item.Tag).Overwrite = item.Checked;
            }

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
