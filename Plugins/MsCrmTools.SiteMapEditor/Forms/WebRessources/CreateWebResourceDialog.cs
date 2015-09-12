// PROJECT : MsCrmTools.SiteMapEditor
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Xrm.Sdk;
using System;
using System.IO;
using System.Windows.Forms;

namespace MsCrmTools.SiteMapEditor.Forms.WebRessources
{
    public partial class CreateWebResourceDialog : Form
    {
        private readonly int requiredType;

        private readonly IOrganizationService service;

        public CreateWebResourceDialog(WebResourceType type, IOrganizationService service)
        {
            InitializeComponent();
            this.service = service;
            requiredType = (int)type;
        }

        public enum WebResourceType
        {
            WebPage = 1,
            Css = 2,
            Script = 3,
            Data = 4,
            Image = 5,
            Silverlight = 8,
            Xsl = 9,
            Ico = 10
        }

        public Entity CreatedEntity { get; set; }

        //Encodes the Web Resource File
        public string getEncodedFileContents(String pathToFile)
        {
            FileStream fs = new FileStream(pathToFile, FileMode.Open, FileAccess.Read);
            byte[] binaryData = new byte[fs.Length];
            long bytesRead = fs.Read(binaryData, 0, (int)fs.Length);
            fs.Close();
            return System.Convert.ToBase64String(binaryData, 0, binaryData.Length);
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select a file to become a web resource";

            switch (requiredType)
            {
                case 1:
                    ofd.Filter = "HTML file (*.html)|*.html";
                    break;

                case 2:
                    ofd.Filter = "CSS Stylesheet file (*.css)|*.css";
                    break;

                case 3:
                    ofd.Filter = "JScript file (*.js)|*.js";
                    break;

                case 4:
                    ofd.Filter = "XML file (*.xml)|*.xml";
                    break;

                case 5:
                    ofd.Filter = "PNG image (*.png)|*.png|JPG image (*.jpg)|*.jpg|GIF image (*.gif)|*.gif";
                    break;

                case 8:
                    ofd.Filter = "XAP silverlight file (*.xap)|*.xap";
                    break;

                case 9:
                    ofd.Filter = "XSL Stylesheet file (*.xsl)|*.xsl";
                    break;

                case 10:
                    ofd.Filter = "ICO file (*.ico)|*.ico";
                    break;
            }

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(ofd.FileName);

                txtFile.Text = fi.FullName;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            try
            {
                Entity webR = new Entity("webresource");
                webR["displayname"] = txtDisplayName.Text;

                string ext = txtFile.Text.Split('.')[txtFile.Text.Split('.').Length - 1];

                switch (ext.ToLower())
                {
                    case "html":
                        webR["webresourcetype"] = new OptionSetValue(1);
                        break;

                    case "png":
                        webR["webresourcetype"] = new OptionSetValue(5);
                        break;

                    case "jpg":
                        webR["webresourcetype"] = new OptionSetValue(6);
                        break;

                    case "gif":
                        webR["webresourcetype"] = new OptionSetValue(7);
                        break;
                }

                webR["name"] = txtName.Text;
                webR["content"] = getEncodedFileContents(txtFile.Text);

                Guid wrId = service.Create(webR);

                webR["webresourceid"] = wrId;

                CreatedEntity = webR;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("An error occured while creating the web resource: " + error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}