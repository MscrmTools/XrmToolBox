using System;
using System.Runtime.Serialization;

namespace XrmToolBox.AppCode
{
    [DataContract]
    public class Releases
    {
        [DataMember(Name = "odatametadata")]
        public string ODataMetadata { get; set; }

        [DataMember(Name = "value")]
        public Release[] Items { get; set; }
    }

    [DataContract]
    public class Release
    {
        [DataMember(Name = "mctools_releaseid")]
        public string Id { get; set; }

        [DataMember(Name = "mctools_name")]
        public string Version { get; set; }

        [DataMember(Name = "mctools_downloadurl")]
        public string DownloadUrl { get; set; }

        [DataMember(Name = "mctools_webpageid")]
        public WebPageReference WebPage { get; set; }

        [DataMember(Name = "listid")]
        public string ListId { get; set; }

        [DataMember(Name = "viewid")]
        public string ViewId { get; set; }

        [DataMember(Name = "entitypermissionsenabled")]
        public object EntityPermissionEnabled { get; set; }
    }

    [DataContract(Name = "Mctools_Webpageid")]
    public class WebPageReference
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}