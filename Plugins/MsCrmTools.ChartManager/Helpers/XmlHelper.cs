// PROJECT : MsCrmTools.ViewLayoutReplicator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System.IO;
using System.Xml;

namespace MsCrmTools.ViewLayoutReplicator.Helpers
{
    class XmlHelper
    {
        internal static string IndentXml(string unformatedXml)
        {
            MemoryStream ms = new MemoryStream();
            XmlTextWriter xtw = new XmlTextWriter(ms, System.Text.Encoding.Unicode);
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.LoadXml(unformatedXml);

                xtw.Formatting = Formatting.Indented;
                doc.WriteContentTo(xtw);
                xtw.Flush();

                ms.Seek(0, SeekOrigin.Begin);
                StreamReader sr = new StreamReader(ms);
                return sr.ReadToEnd();
            }
            catch
            {
                return "Error while formatting Xml...";
            }
        }
    }
}
