// PROJECT : MsCrmTools.SiteMapEditor
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Windows.Forms;
using System.Xml;

namespace MsCrmTools.SiteMapEditor.Forms
{
    public partial class AddXmlForm : Form
    {
        public AddXmlForm()
        {
            InitializeComponent();
        }

        public XmlNode AddedXmlNode { get; set; }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AddedXmlNode = null;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(textBox1.Text);

                AddedXmlNode = doc.DocumentElement;

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, "Error while parsing Xml: " + error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}