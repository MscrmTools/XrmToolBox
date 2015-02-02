using System;
using System.Windows.Forms;

namespace XrmToolBox.AppCode
{
    internal class CodeplexVersionChecker
    {
        private readonly int currentMajorVersion;
        private readonly int currentMinorVersion;
        private readonly int currentBuildVersion;
        private readonly int currentRevisionVersion;
        public CodeplexVersionChecker(string currentVersion)
        {
            var versionParts = currentVersion.Split('.');
            currentMajorVersion = int.Parse(versionParts[0]);
            currentMinorVersion = int.Parse(versionParts[1]);
            currentBuildVersion = int.Parse(versionParts[2]);
            currentRevisionVersion = int.Parse(versionParts[3]); 
        }

        public const string NoReleaseDetails = "<div>No description</div>";

        public event EventHandler OnCodePlexInforRetrieved;

        public void Run()
        {
            var mywebBrowser = new WebBrowser {ScriptErrorsSuppressed = true};
            mywebBrowser.DocumentCompleted += mywebBrowser_DocumentCompleted;
            mywebBrowser.Navigate("http://xrmtoolbox.codeplex.com/releases");
        }

        private void mywebBrowser_DocumentCompleted(Object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //Until this moment the page is not completely loaded
            var br = sender as WebBrowser;
            if (br.Url == e.Url)
            {
                var cpi = new CodePlexInformation();
                HtmlDocument doc = br.Document;
                
                HtmlElement rateTag = doc.GetElementById("releasecurrentRating");
                cpi.Rate = rateTag == null ? doc.GetElementById("NoReviewsLabel").InnerText : rateTag.GetAttribute("value");

                HtmlElement releaseNotesTag = doc.GetElementById("ReleaseNotes");
                cpi.Description = releaseNotesTag != null ? releaseNotesTag.InnerHtml : NoReleaseDetails;

                HtmlElementCollection tagCollection = doc.GetElementsByTagName("h1");

                foreach (HtmlElement tag in tagCollection)
                {
                    if (tag.InnerText.StartsWith("XrmToolBox (v"))
                    {
                        Console.WriteLine(tag.InnerText);

                        var version = tag.InnerText.Split('v')[1].Replace(")", "");
                        var versionParts = version.Split('.');
                        int majorVersion = int.Parse(versionParts[0]);
                        int minorVersion = int.Parse(versionParts[1]);
                        int buildVersion = int.Parse(versionParts[2]);
                        int revisionVersion = int.Parse(versionParts[3]);

                        if (currentMajorVersion < majorVersion
                            || currentMajorVersion == majorVersion && currentMinorVersion < minorVersion
                            ||
                            currentMajorVersion == majorVersion && currentMinorVersion == minorVersion &&
                            currentBuildVersion < buildVersion
                            ||
                            currentMajorVersion == majorVersion && currentMinorVersion == minorVersion &&
                            currentBuildVersion == buildVersion && currentRevisionVersion < revisionVersion)
                        {
                            cpi.Version = version;
                        }

                        break;
                    }
                }

                OnCodePlexInforRetrieved(this, new CodePlexInfoRetrievedEventArgs{Information = cpi});
            }
        }
    }
}
