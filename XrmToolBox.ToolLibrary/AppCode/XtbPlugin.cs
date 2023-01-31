using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using XrmToolBox.Extensibility.Interfaces;

namespace XrmToolBox.ToolLibrary.AppCode
{
    public enum CompatibleState
    {
        Compatible,
        RequireNewVersionOfXtb,
        DoesntFitMinimumVersion,
        Other
    }

    [DataContract]
    public class XtbPlugin : IXrmToolBoxLibraryTool
    {
        #region Properties

        public PackageInstallAction Action { get; set; }

        [DataMember(Name = "mctools_authors")]
        public string Authors { get; set; }

        [DataMember(Name = "mctools_averagedownloadcount")]
        public decimal AverageDownloadCount { get; set; }

        [DataMember(Name = "mctools_averagefeedbackratingallversions")]
        public decimal AverageFeedbackRating { get; set; }

        public List<string> Categories => CategoriesList?.Split(',').ToList() ?? new List<string>();

        [DataMember(Name = "mctools_categorieslist")]
        public string CategoriesList { get; set; }

        public string CleanedName
        {
            get; set;
        }

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
            get { return FilesList?.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).Where(f => f.ToLower().IndexOf("plugins/", StringComparison.Ordinal) >= 0).ToList() ?? new List<string>(); }
        }

        [DataMember(Name = "mctools_files")]
        public string FilesList { get; set; }

        [DataMember(Name = "mctools_firstreleasedate")]
        public DateTime? FirstReleaseDate { get; internal set; }

        [DataMember(Name = "mctools_pluginid")]
        public string Id { get; set; }

        public bool IsFromCustomRepo { get; set; }

        [DataMember(Name = "contact-mctools_ismvp")]
        public bool? IsMvp { get; set; }

        [DataMember(Name = "mctools_isopensource")]
        public bool? IsOpenSource { get; set; }

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

        public Image Logo { get; set; }

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
        public List<string> SearchedProperties => new List<string> { Name, Description, Authors };
        public string SourceRepositoryName { get; set; }

        [DataMember(Name = "mctools_totaldownloadcount")]
        public int TotalDownloadCount { get; set; }

        [DataMember(Name = "mctools_totalfeedbackallversion")]
        public int TotalFeedbackRating { get; set; }

        [DataMember(Name = "mctools_version")]
        public string Version { get; set; }

        public List<XtbPluginVersion> Versions { get; internal set; }

        [DataMember(Name = "view-id")]
        public string ViewId { get; set; }

        #endregion Properties

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

        [DataMember(Name = "odata.nextLink")]
        public string NextLink { get; set; }

        [DataMember(Name = "odata.metadata")]
        public string OdataMetadata { get; set; }

        [DataMember(Name = "value")]
        public List<XtbPlugin> Plugins { get; set; } = new List<XtbPlugin>();
    }
}