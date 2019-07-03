using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace XrmToolBox.PluginsStore.DTO
{
    [DataContract]
    public class XtbPlugin
    {
        public PackageInstallAction Action { get; set; }

        [DataMember(Name = "mctools_authors")]
        public string Authors { get; set; }

        [DataMember(Name = "mctools_averagedownloadcount")]
        public decimal AverageDownloadCount { get; set; }

        [DataMember(Name = "mctools_averagefeedbackratingallversions")]
        public decimal AverageFeedbackRating { get; set; }

        public CompatibleState Compatibilty { get; internal set; }

        public Version CurrentVersion { get; internal set; }

        [DataMember(Name = "mctools_description")]
        public string Description { get; set; }

        [DataMember(Name = "mctools_downloadurl")]
        public string DownloadUrl { get; set; }

        [DataMember(Name = "entity-permissions-enabled")]
        public object EntityPermissionsEnabled { get; set; }

        public List<string> Files
        {
            get { return FilesList.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Where(f => f.ToLower().IndexOf("plugins/", StringComparison.Ordinal) >= 0).ToList(); }
        }

        [DataMember(Name = "mctools_files")]
        public string FilesList { get; set; }

        [DataMember(Name = "mctools_firstreleasedate")]
        public DateTime? FirstReleaseDate { get; internal set; }

        [DataMember(Name = "mctools_pluginid")]
        public string Id { get; set; }

        [DataMember(Name = "mctools_latestreleasedate")]
        public DateTime? LatestReleaseDate { get; internal set; }

        [DataMember(Name = "mctools_latestversionid")]
        public string LatestReleaseId { get; internal set; }

        [DataMember(Name = "mctools_latestreleasenote")]
        public string LatestReleaseNote { get; internal set; }

        [DataMember(Name = "mctools_licenseurl")]
        public string LicenseUrl { get; internal set; }

        [DataMember(Name = "list-id")]
        public string ListId { get; set; }

        [DataMember(Name = "mctools_logourl")]
        public string LogoUrl { get; set; }

        [DataMember(Name = "mctools_xrmtoolboxversiondependency")]
        public string MinimalXrmToolBoxVersion { get; set; }

        [DataMember(Name = "mctools_name")]
        public string Name { get; set; }

        [DataMember(Name = "mctools_nugetid")]
        public string NugetId { get; set; }

        [DataMember(Name = "mctools_projecturl")]
        public string ProjectUrl { get; internal set; }

        [DataMember(Name = "mctools_requirelicenseacceptance")]
        public bool? RequireLicenseAcceptance { get; internal set; }

        public bool RequiresXtbRestart { get; internal set; }

        [DataMember(Name = "mctools_totaldownloadcount")]
        public int TotalDownloadCount { get; set; }

        [DataMember(Name = "mctools_totalfeedbackallversion")]
        public int TotalFeedbackRating { get; set; }

        [DataMember(Name = "mctools_version")]
        public string Version { get; set; }

        [DataMember(Name = "view-id")]
        public string ViewId { get; set; }

        public ListViewItem GetPluginsStoreItem()
        {
            var packageVersion = new Version(Version);

            var item = new ListViewItem(string.Empty) { Tag = this };

            item.SubItems.Add(Name);
            item.SubItems.Add(AverageFeedbackRating.ToString("N2"));
            item.SubItems.Add(packageVersion.ToString());
            item.SubItems.Add(CurrentVersion?.ToString());
            item.SubItems.Add(LatestReleaseDate?.ToString(CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern));
            item.SubItems.Add(Description);
            item.SubItems.Add(Authors);
            var actionItem = item.SubItems.Add("None");
            item.SubItems.Add(TotalDownloadCount.ToString("N0"));
            item.SubItems.Add(AverageDownloadCount.ToString("N2"));

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

                default:
                    actionItem.Text = "N/A";
                    item.ForeColor = Color.Gray;
                    break;
            }

            return item;
        }

        public override string ToString()
        {
            return NugetId;
        }
    }

    [DataContract]
    public class XtbPlugins
    {
        public XtbPlugins()
        {
            Plugins = new List<XtbPlugin>();
        }

        [DataMember(Name = "odata.metadata")]
        public string OdataMetadata { get; set; }

        [DataMember(Name = "value")]
        public List<XtbPlugin> Plugins { get; set; } = new List<XtbPlugin>();
    }
}