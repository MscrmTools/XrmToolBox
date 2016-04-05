// PROJECT : MsCrmTools.ScriptsFinder
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace MsCrmTools.ScriptsFinder
{
    public partial class ScriptsFinder : PluginControlBase
    {
        #region Constructor

        public ScriptsFinder()
        {
            InitializeComponent();
        }

        #endregion Constructor

        public void FindScripts()
        {
            lvScripts.Items.Clear();
            tsbMainFindScripts.Enabled = false;
            tsbExportToCsv.Enabled = false;

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading scripts (this can take a while...)",
                AsyncArgument = null,
                Work = (bw, e) =>
                {
                    var lScripts = new List<ListViewItem>();

                    var sManager = new ScriptsManager(Service);
                    sManager.Find();

                    foreach (var script in sManager.Scripts)
                    {
                        var item = new ListViewItem(script.Type);
                        item.SubItems.Add(script.EntityName);
                        item.SubItems.Add(script.EntityLogicalName);
                        item.SubItems.Add(script.Name);
                        item.SubItems.Add(script.Event);
                        item.SubItems.Add(script.Attribute);
                        item.SubItems.Add(script.AttributeLogicalName);
                        item.SubItems.Add(script.ScriptLocation);
                        item.SubItems.Add(script.MethodCalled);
                        item.SubItems.Add(script.Arguments);
                        item.SubItems.Add(script.IsActive.HasValue ? script.IsActive.Value.ToString() : "");

                        if (script.HasProblem)
                        {
                            item.ForeColor = Color.Red;
                        }
                        lScripts.Add(item);
                    }

                    e.Result = lScripts;
                },
                PostWorkCallBack = e =>
                {
                    lvScripts.Items.AddRange(((List<ListViewItem>)e.Result).ToArray());
                    tsbMainFindScripts.Enabled = true;
                    tsbExportToCsv.Enabled = true;
                }
            });
        }

        private void LvScriptsColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvScripts.Sorting = lvScripts.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lvScripts.ListViewItemSorter = new ListViewItemComparer(e.Column, lvScripts.Sorting);
        }

        private void TsbCloseThisTabClick(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void TsbExportToCsvClick(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog
            {
                Filter = "CSV file (*.csv)|*.csv",
                Title = "Select a file where to save the list of scripts"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (var fs = new FileStream(sfd.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    var preamble = new UTF8Encoding(true).GetPreamble();
                    fs.Write(preamble, 0, preamble.Length);

                    var header = Encoding.UTF8.GetBytes(
                        "Type,Entity Display Name,Entity Logical Name,Form name,Event,Attribute Display Name,Attribute Logical Name,Script Location,Method Called,Parameters,Enabled" +
                        Environment.NewLine);
                    fs.Write(header, 0, header.Length);

                    foreach (ListViewItem item in lvScripts.Items)
                    {
                        var line = Encoding.UTF8.GetBytes(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}{11}",
                            item.SubItems[0].Text,
                            item.SubItems[1].Text,
                            item.SubItems[2].Text,
                            item.SubItems[3].Text,
                            item.SubItems[4].Text,
                            item.SubItems[5].Text,
                            item.SubItems[6].Text,
                            item.SubItems[7].Text,
                            item.SubItems[8].Text,
                            item.SubItems[9].Text,
                            item.SubItems[10].Text,
                            Environment.NewLine));

                        fs.Write(line, 0, line.Length);
                    }

                    if (MessageBox.Show(this, string.Format("File saved to '{0}'!\r\n\r\nWould you like to open the file now?", sfd.FileName), "Question",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        Process.Start(sfd.FileName);
                    }
                }
            }
        }

        private void TsbMainFindScriptsClick(object sender, EventArgs e)
        {
            ExecuteMethod(FindScripts);
        }
    }
}