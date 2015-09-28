// PROJECT : MsCrmTools.SiteMapEditor
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace MsCrmTools.ViewLayoutReplicator.Forms
{
    public partial class XmlContentDisplayDialog : Form
    {
        public XmlContentDisplayDialog(string xmlString)
        {
            InitializeComponent();

            string indentedXml = IndentXMLString(xmlString);
            txtContent.Text = indentedXml;
        }

        private string IndentXMLString(string xml)
        {
            MemoryStream ms = new MemoryStream();
            XmlTextWriter xtw = new XmlTextWriter(ms, Encoding.Unicode);
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.LoadXml(xml);

                xtw.Formatting = Formatting.Indented;
                doc.WriteContentTo(xtw);
                xtw.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                StreamReader sr = new StreamReader(ms);
                return sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
    }
}