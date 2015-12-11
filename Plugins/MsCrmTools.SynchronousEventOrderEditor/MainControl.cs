using Microsoft.Xrm.Sdk.Query;
using MsCrmTools.SynchronousEventOrderEditor.AppCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace MsCrmTools.SynchronousEventOrderEditor
{
    public partial class MainControl : PluginControlBase
    {
        private List<ISynchronousEvent> events;

        public MainControl()
        {
            InitializeComponent();
        }

        private void dgvSynchronousEvent_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSynchronousEvent.Rows.Count == 0) return;
            dgvSynchronousEventRank.ValueType = typeof(Int32);
            int rank;

            if (int.TryParse(dgvSynchronousEvent.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out rank))
            {
                var sEvent = (ISynchronousEvent)dgvSynchronousEvent.Rows[e.RowIndex].Tag;
                sEvent.Rank = rank;

                dgvSynchronousEvent.Sort(dgvSynchronousEvent.Columns[e.ColumnIndex], ListSortDirection.Ascending);
            }
            else
            {
                MessageBox.Show(ParentForm, "Only integer value is allowed for rank", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void LoadEvents()
        {
            tvEvents.Nodes.Clear();

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading Sdk message filters...",
                Work = (bw, e) =>
                {
                    events = new List<ISynchronousEvent>();

                    var filters =
                        Service.RetrieveMultiple(new QueryExpression("sdkmessagefilter")
                        {
                            ColumnSet = new ColumnSet("sdkmessageid", "primaryobjecttypecode")
                        }).Entities.ToList();

                    bw.ReportProgress(0, "Loading SDK messages...");

                    var messages = Service.RetrieveMultiple(new QueryExpression("sdkmessage")
                    {
                        ColumnSet = new ColumnSet("name")
                    }).Entities.ToList();

                    bw.ReportProgress(0, "Loading Plugin steps...");

                    events.AddRange(PluginStep.RetrievePluginSteps(Service, filters, messages));

                    bw.ReportProgress(0, "Loading Synchronous workflows...");

                    events.AddRange(SynchronousWorkflow.RetrievePluginSteps(Service));
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        MessageBox.Show(ParentForm, "An error occured: " + e.Error.ToString(), "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    else
                    {
                        var tvh = new TreeViewHelper(tvEvents);

                        foreach (var sEvent in events)
                        {
                            tvh.AddSynchronousEvent(sEvent);
                        }
                    }
                },
                ProgressChanged = e => { SetWorkingMessage(e.UserState.ToString()); }
            });
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void tsbLoadEvents_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadEvents);
        }

        private void tsbUpdate_Click(object sender, EventArgs e)
        {
            var updatedEvents = events.Where(ev => ev.HasChanged);

            if (updatedEvents.Any(ev => ev.Type == "Workflow") && DialogResult.No ==
                MessageBox.Show(ParentForm, "Workflows will be deactivated, updated, then activated back. Are you sure you want to continue?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return;
            }

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Updating...",
                Work = (bw, evt) =>
                {
                    foreach (var sEvent in events.Where(ev => ev.HasChanged))
                    {
                        bw.ReportProgress(0, string.Format("Updating {0} {1}", sEvent.Type, sEvent.Name));
                        sEvent.UpdateRank(Service);
                    }
                },
                PostWorkCallBack = evt =>
                {
                    if (evt.Error != null)
                    {
                        MessageBox.Show(ParentForm, "An error occured: " + evt.Error.Message, "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                },
                ProgressChanged = evt => { SetWorkingMessage(evt.UserState.ToString()); }
            });
        }

        private void tvEvents_AfterSelect(object sender, TreeViewEventArgs e)
        {
            dgvSynchronousEvent.Rows.Clear();

            if (e.Node.Nodes.Count > 0)
            {
                return;
            }

            var localEvents = (List<ISynchronousEvent>)e.Node.Tag;

            foreach (var sEvent in localEvents)
            {
                dgvSynchronousEvent.Rows.Add(new DataGridViewRow
                {
                    Cells =
                    {
                        new DataGridViewTextBoxCell{Value = sEvent.Rank},
                        new DataGridViewTextBoxCell{Value = sEvent.Type},
                        new DataGridViewTextBoxCell{Value = sEvent.Name},
                        new DataGridViewTextBoxCell{Value = sEvent.Description}
                    },
                    Tag = sEvent
                });
            }
        }
    }
}