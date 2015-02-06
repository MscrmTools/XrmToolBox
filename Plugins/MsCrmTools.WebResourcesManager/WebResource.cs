// PROJECT : MsCrmTools.WebResourcesManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using MsCrmTools.WebResourcesManager.Forms;

namespace MsCrmTools.WebResourcesManager
{
    class WebResource
    {
        private static readonly Regex InValidWrNameRegex = new Regex("[^a-z0-9A-Z_\\./]|[/]{2,}", (RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase));

        private static readonly List<string> validExtensions = new List<string>{"htm","html","css","js","xml","jpg","jpeg","png","gif","ico","xap","xslt"};

        public static List<string> ValidExtensions
        {
            get { return validExtensions; }
        }

        public string FilePath { get; set; }
        public Entity WebResourceEntity { get; set; }

        public string InitialBase64 { get; set; }

        public WebResource(Entity webResource, string filePath)
        {
            FilePath = filePath;
            WebResourceEntity = webResource;
            InitialBase64 = webResource.GetAttributeValue<string>("content");
        }

        public WebResource(Entity webresource)
            : this(webresource, string.Empty)
        {}

        public static int GetTypeFromExtension(string ext)
        {
            switch (ext.ToLower())
            {
                case "htm":
                case "html":
                    return 1;
                case "css":
                    return 2;
                case "js":
                    return 3;
                case "xml":
                    return 4;
                case "png":
                    return 5;
                case "jpg":
                case "jpeg":
                    return 6;
                case "gif":
                    return 7;
                case "xap":
                    return 8;
                case "xsl":
                case "xslt":
                    return 9;
                default:
                    return 10;
            }
        }

        public static int GetImageIndexFromExtension(string ext)
        {
            switch (ext.ToLower())
            {
                case "htm":
                case "html":
                    return 2;
                case "css":
                    return 3;
                case "js":
                    return 4;
                case "xml":
                    return 5;
                case "png":
                    return 6;
                case "jpg":
                case "jpeg":
                    return 7;
                case "gif":
                    return 8;
                case "xap":
                    return 9;
                case "xsl":
                case "xslt":
                    return 10;
                default:
                    return 11;
            }
        }

        public static bool IsInvalidName(string name)
        {
            var isInvalidName = InValidWrNameRegex.IsMatch(name);

             const string pattern = "*.config .* _* *.bin";
            
            // insert backslash before regex special characters that may appear in filenames
            var regexPattern = Regex.Replace(pattern, @"([\+\(\)\[\]\{\$\^\.])", @"\$1");
            
            // apply regex syntax
            regexPattern = string.Format("^{0}$", regexPattern.Replace(" ", "$|^").Replace("*", ".*"));

            var regex = new Regex(regexPattern, RegexOptions.IgnoreCase);

            var isInvalidName2 = regex.IsMatch(name);

            return isInvalidName || isInvalidName2;
        }

        public WebResource ShowProperties(IOrganizationService service, Control mainControl)
        {
            var form = new UpdateForm(this, service)
            {
                StartPosition = FormStartPosition.CenterParent
            };

            if (form.ShowDialog(mainControl) == DialogResult.OK)
            {
                return form.WebRessource;
            }

            return this;
        }
    }
}
