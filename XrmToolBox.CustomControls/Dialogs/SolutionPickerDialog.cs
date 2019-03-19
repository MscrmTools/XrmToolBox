using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace XrmToolBox.CustomControls.Dialogs
{
    public partial class SolutionPickerDialog : Form
    {
        private readonly IOrganizationService _service;
        private Thread filterThread;
        private List<ListViewItem> items = new List<ListViewItem>();

        public SolutionPickerDialog(IOrganizationService service)
        {
            InitializeComponent();

            _service = service;
        }

        [Browsable(false)]
        public List<Entity> SelectedSolutions { get; private set; }

        [Category("UI")]
        [DisplayName("Allow multiple solutions selection")]
        public bool MultiSelect { get; set; } = true;

        [Category("UI")]
        [DisplayName("Display filter textbox")]
        [Description("Indicates if a textbox to filter solutions should be displayed")]
        public bool DisplaySearch { get; set; } = true;

        [Category("UI")]
        [DisplayName("Display header")]
        [Description("Indicates if a blank header with title should be displayed")]
        public bool DisplayHeader { get; set; } = false;

        [Category("UI")]
        [DisplayName("Header text")]
        [Description("Indicates if a blank header with title should be displayed")]
        public string HeaderText { get; set; }

        [Category("UI")]
        [DisplayName("Text")]
        [Description("Indicates the text to display on the dialog header")]
        public string DialogTitle { get; set; } = "Solution Picker";

        [Category("Solutions")]
        [DisplayName("Display managed solutions")]
        [Description("Indicates if managed solutions should be displayed in the solutions list")]
        public bool DisplayManagedSolutions { get; set; } = true;

        [Category("Solutions")]
        [DisplayName("Display unmanaged solutions")]
        [Description("Indicates if unmanaged solutions should be displayed in the solutions list")]
        public bool DisplayUnmanagedSolutions { get; set; } = true;

        [Category("Solutions")]
        [DisplayName("Display default solution")]
        [Description("Indicates if default solution should be displayed in the solutions list")]
        public bool DisplayDefaultSolution { get; set; } = false;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SelectedSolutions = new List<Entity>();
            Close();
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            if (lvSolutions.SelectedItems.Count > 0)
            {
                SelectedSolutions = lvSolutions.SelectedItems.Cast<ListViewItem>().Select(i => (Entity)i.Tag).ToList();
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(this, "Please select at least one solution!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void lstSolutions_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            var list = (ListView)sender;
            list.Sorting = list.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            list.ListViewItemSorter = new ListViewItemComparer(e.Column, list.Sorting);
        }

        private void lstSolutions_DoubleClick(object sender, EventArgs e)
        {
            btnValidate_Click(null, null);
        }

        private EntityCollection RetrieveSolutions()
        {
            try
            {
                QueryExpression qe = new QueryExpression("solution");
                qe.Distinct = true;
                qe.ColumnSet = new ColumnSet(true);
                qe.Criteria = new FilterExpression();
                qe.Criteria.AddCondition(new ConditionExpression("isvisible", ConditionOperator.Equal, true));

                if (DisplayDefaultSolution == false)
                {
                    qe.Criteria.AddCondition(new ConditionExpression("uniquename", ConditionOperator.NotEqual, "Default"));
                }

                if (DisplayManagedSolutions == false)
                {
                    qe.Criteria.AddCondition(new ConditionExpression("ismanaged", ConditionOperator.NotEqual, true));
                }

                if (DisplayUnmanagedSolutions == false)
                {
                    qe.Criteria.AddCondition(new ConditionExpression("ismanaged", ConditionOperator.NotEqual, false));
                }

                return _service.RetrieveMultiple(qe);
            }
            catch (Exception error)
            {
                if (error.InnerException is FaultException)
                {
                    throw new Exception($"Error while retrieving solutions: {error.InnerException.Message}");
                }

                throw new Exception($"Error while retrieving solutions: {error.Message}");
            }
        }

        private void SolutionPicker_Load(object sender, EventArgs e)
        {
            tsMain.Visible = DisplaySearch;
            Text = DialogTitle;
            lvSolutions.MultiSelect = MultiSelect;
            pnlHeader.Visible = DisplayHeader;
            lblHeader.Text = string.IsNullOrEmpty(HeaderText) ? $"Please select {(MultiSelect ? "one or multiple solutions" : "a solution")}" : HeaderText;

            lvSolutions.Items.Clear();

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = RetrieveSolutions();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (Entity solution in ((EntityCollection)e.Result).Entities)
            {
                ListViewItem item = new ListViewItem(solution["friendlyname"].ToString());
                item.SubItems.Add(solution["version"].ToString());
                item.SubItems.Add(((EntityReference)solution["publisherid"]).Name);
                item.SubItems.Add((bool)solution["ismanaged"]?"Managed":"Unmanaged");
                item.Tag = solution;

                items.Add(item);
            }

            Display();

            lvSolutions.Enabled = true;
            btnValidate.Enabled = true;
        }

        private void tstFilter_TextChanged(object sender, EventArgs e)
        {
            filterThread?.Abort();
            filterThread = new Thread(Display);
            filterThread.Start();
        }

        private void Display()
        {
            Thread.Sleep(300);

            Invoke(new Action(() =>
            {
                if (tstFilter.TextBox.Text.Length == 0)
                {
                    lvSolutions.Items.Clear();
                    lvSolutions.Items.AddRange(items.ToArray());
                    return;
                }

                var filteredItems = items.Where(i => i.Tag is Entity e
                && (e.GetAttributeValue<string>("friendlyname").ToLower().IndexOf(tstFilter.TextBox.Text.ToLower()) >= 0
                || e.GetAttributeValue<string>("uniquename").ToLower().IndexOf(tstFilter.TextBox.Text.ToLower()) >= 0));

                lvSolutions.Items.Clear();
                lvSolutions.Items.AddRange(filteredItems.ToArray());
            }));
        }
    }
}