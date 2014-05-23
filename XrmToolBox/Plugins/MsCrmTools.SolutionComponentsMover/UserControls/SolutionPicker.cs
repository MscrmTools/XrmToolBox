using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace MsCrmTools.SolutionComponentsMover.UserControls
{
    public partial class SolutionPicker : UserControl
    {
        private IOrganizationService service;

        public SolutionPicker()
        {
            InitializeComponent();
        }

        public IOrganizationService Service { set { service = value; } }

        public List<Entity> SelectedSolutions
        {
            get { return lvSolutions.SelectedItems.Cast<ListViewItem>().Select(i => (Entity) i.Tag).ToList(); }
        }

       public void LoadSolutions(IEnumerable<Entity> solutions)
        {
            lvSolutions.Items.Clear();

            var list = new List<ListViewItem>();

            foreach (var solution in solutions)
            {
                var item = new ListViewItem(solution.GetAttributeValue<string>("friendlyname"));
                item.SubItems.Add(solution.GetAttributeValue<string>("uniquename"));
                item.SubItems.Add(solution.GetAttributeValue<EntityReference>("publisherid").Name);
                item.Tag = solution;

                list.Add(item);
            } 

            lvSolutions.Items.AddRange(list.ToArray());
        }

       
    }
}
