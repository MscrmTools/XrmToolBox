using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace XrmToolBox.Controls
{
    public partial class AssembliesControl : UserControl
    {
        public AssembliesControl()
        {
            InitializeComponent();

            PopulateAssemblies();
        }

        private static string assemblyPrioritizer(string assemblyName)
        {
            return
                assemblyName.Equals("XrmToolBox") ? "AAAAAAAAAAAA" :
                assemblyName.Contains("XrmToolBox") ? "AAAAAAAAAAAB" :
                assemblyName.Equals(Assembly.GetExecutingAssembly().GetName().Name) ? "AAAAAAAAAAAC" :
                assemblyName;
        }

        private ListViewItem GetListItem(AssemblyName a)
        {
            var assembly = Assembly.Load(a);
            var fi = FileVersionInfo.GetVersionInfo(assembly.Location);

            var item = new ListViewItem(a.Name);
            item.SubItems.Add(fi.FileVersion.ToString());
            return item;
        }

        private List<AssemblyName> GetReferencedAssemblies()
        {
            var names = Assembly.GetExecutingAssembly().GetReferencedAssemblies()
                    .Where(a => !a.Name.Equals("mscorlib") && !a.Name.StartsWith("System") && !a.Name.Contains("CSharp")).ToList();
            names.Add(Assembly.GetExecutingAssembly().GetName());
            names = names.OrderBy(a => assemblyPrioritizer(a.Name)).ToList();
            return names;
        }

        private void PopulateAssemblies()
        {
            var assemblies = GetReferencedAssemblies();
            var items = assemblies.Select(a => GetListItem(a)).ToArray();
            lvAssemblies.Items.Clear();
            lvAssemblies.Items.AddRange(items);
        }
    }
}