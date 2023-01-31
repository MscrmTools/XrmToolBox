using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Windows.Forms;

namespace XrmToolBox.ToolLibrary.AppCode
{
    public class XtbPluginVersion
    {
        private string downloadUrl;
        private bool isCurrent;
        private Version minCompatibleVersion;
        private Version minXtbVersion;
        private string releaseNotes;

        public XtbPluginVersion(Version version, string url, Version minCompatibleVersion, bool isCurrent, Version minXtbVersion)
        {
            Version = version;
            Url = url;

            this.isCurrent = isCurrent;
            this.minCompatibleVersion = minCompatibleVersion;
            this.minXtbVersion = minXtbVersion;
        }

        public string DownloadUrl
        {
            get
            {
                GetDetails();
                return downloadUrl;
            }
        }

        public Version MinCompatibleVersion => minCompatibleVersion;

        public string ReleaseNotes
        {
            get
            {
                GetDetails();
                return releaseNotes;
            }
        }

        public string Url { get; }
        public Version Version { get; }

        public string GetIncompatibleReason()
        {
            if (minXtbVersion > minCompatibleVersion)
            {
                return "This version is not compatible with latest XrmToolBox breaking changes";
            }

            if (minCompatibleVersion > new Version(Application.ProductVersion))
            {
                return "This version has been developed for a newer version of XrmToolBox";
            }

            return String.Empty;
        }

        public void SetIsCurrent()
        {
            isCurrent = true;
        }

        public override string ToString()
        {
            return Version.ToString()
                + (isCurrent ? " (current)" : "")
                + ((minXtbVersion > minCompatibleVersion || minCompatibleVersion > new Version(Application.ProductVersion)) ? " (incompatible)" : "");
        }

        private void GetDetails()
        {
            if (string.IsNullOrEmpty(downloadUrl) || string.IsNullOrEmpty(releaseNotes))
            {
                try
                {
                    var httpClient = new HttpClient();
                    var data = httpClient.GetAsync(Url).GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    JObject jo = JObject.Parse(data);

                    if (string.IsNullOrEmpty(downloadUrl))
                    {
                        downloadUrl = jo["packageContent"].ToString();
                    }

                    if (string.IsNullOrEmpty(releaseNotes))
                    {
                        data = httpClient.GetAsync(jo["catalogEntry"].ToString()).GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        jo = JObject.Parse(data);
                        releaseNotes = jo["releaseNotes"]?.ToString();
                    }
                }
                catch (Exception error)
                {
                    releaseNotes = $"Error while retrieving release notes for this package: {error.Message}";
                }
            }
        }
    }
}