using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace XrmToolBox
{
    internal class CodeplexVersionChecker
    {
        private readonly Form currentForm;
        private readonly string currentVersion;
        private readonly int currentMajorVersion;
        private readonly int currentMinorVersion;
        private readonly int currentBuildVersion;
        private readonly int currentRevisionVersion;
        private Thread th;
        public CodeplexVersionChecker(string currentVersion, Form currentForm)
        {
            this.currentVersion = currentVersion;
            this.currentForm = currentForm;
            var versionParts = currentVersion.Split('.');
            currentMajorVersion = int.Parse(versionParts[0]);
            currentMinorVersion = int.Parse(versionParts[1]);
            currentBuildVersion = int.Parse(versionParts[2]);
            currentRevisionVersion = int.Parse(versionParts[3]); 
        }

        public void Run()
        {
            th = new Thread(() =>
            {
                var mywebBrowser = new WebBrowser();
                mywebBrowser.DocumentCompleted += mywebBrowser_DocumentCompleted;
                mywebBrowser.Navigate("http://xrmtoolbox.codeplex.com/releases");
                Application.Run();
            });
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void mywebBrowser_DocumentCompleted(Object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //Until this moment the page is not completely loaded
            var br = sender as WebBrowser;
            if (br.Url == e.Url)
            {
                HtmlDocument doc = br.Document;
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
                            if (DialogResult.Yes ==
                                MessageBox.Show(currentForm,
                                    "A new version of XrmToolBox is available!\r\n\r\nYour version: "+ currentVersion+"\r\nNew version: " + version + " \r\n\r\nWould you like to display the download page?",
                                    "Update available", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                            {
                                Process.Start("http://xrmtoolbox.codeplex.com/releases");
                            }
                        }

                        break;
                    }
                }
            }

            th.Abort();
        }
    }
}
