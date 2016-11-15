using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NuGet;

namespace XrmToolBox.PluginsStore
{
    public class XtbNuGetPackage
    {
        public PackageInstallAction Action;
        public bool RequiresXtbRestart { get; set; }
        public IPackage Package;
        public Version CurrentVersion { get; set; }
        private Dictionary<string, int> currentVersionDownloadsCount;

        public XtbNuGetPackage(IPackage package, PackageInstallAction action, Dictionary<string, int> currentVersionDownloadsCount)
        {
            Action = action;
            Package = package;
            this.currentVersionDownloadsCount = currentVersionDownloadsCount;
        }

        public ListViewItem GetPluginsStoreItem()
        {
            var item = new ListViewItem(string.Empty);
            item.Tag = this;
            item.SubItems.Add(this.ToString());
            item.SubItems.Add(Package.Version.ToString());
            item.SubItems.Add(CurrentVersion?.ToString());
            item.SubItems.Add(Package.Description);
            item.SubItems.Add(string.Join(", ", Package.Authors));
            var actionItem = item.SubItems.Add("None");
            item.SubItems.Add(Package.DownloadCount.ToString());

            if(currentVersionDownloadsCount.ContainsKey(Package.Id))
                item.SubItems.Add(currentVersionDownloadsCount[Package.Id].ToString());
            else
                item.SubItems.Add("N/A");

            switch (Action)
            {
                case PackageInstallAction.Unavailable:
                    actionItem.Text = "Incompatible";
                    item.ForeColor = Color.Red;
                    break;
                case PackageInstallAction.Update:
                    actionItem.Text = "Update";
                    item.ForeColor = Color.Blue;
                    break;
                case PackageInstallAction.Install:
                    actionItem.Text = "Install";
                    item.ForeColor = Color.Black;
                    break;
                case PackageInstallAction.None:
                default:
                    actionItem.Text = "N/A";
                    item.ForeColor = Color.Gray;
                    break;
            }

            return item;
        }

        public override string ToString()
        {
            if (Package != null)
            {
                if (!string.IsNullOrWhiteSpace(Package.Title))
                {
                    return Package.Title.Replace(" for XrmToolBox", "");
                }
                else
                {
                    return Package.Id;
                }
            }
            return "?";
        }
    }
}
