// PROJECT : MsCrmTools.WebResourcesManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Xrm.Sdk;
using MsCrmTools.WebResourcesManager.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MsCrmTools.WebResourcesManager.AppCode
{
    public class WebResource
    {
        private static readonly Regex InValidWrNameRegex = new Regex("[^a-z0-9A-Z_\\./]|[/]{2,}", (RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase));

        private static readonly List<string> validExtensions = new List<string> { "htm", "html", "css", "js", "xml", "jpg", "jpeg", "png", "gif", "ico", "xap", "xslt" };

        public WebResource(Entity webResource, string filePath)
        {
            AssociatedResources = new List<WebResource>();
            FilePath = filePath;
            Entity = webResource;
            InitialBase64 = webResource.GetAttributeValue<string>("content");

            LoadAssociatedResources();
        }

        public WebResource(Entity webresource)
            : this(webresource, string.Empty)
        { }

        public static List<string> ValidExtensions
        {
            get { return validExtensions; }
        }

        public static WebResource LoadWebResourceFromDisk(string fileName, string name, string displayName = null)
        {
            var entity = new Entity("webresource");
            entity["content"] = Convert.ToBase64String(File.ReadAllBytes(fileName));
            entity["webresourcetype"] = new OptionSetValue(GetTypeFromExtension(Path.GetExtension(fileName)));
            entity["name"] = name;
            entity["displayname"] = displayName ?? name;
            return new WebResource(entity, fileName);
        }

        public List<WebResource> AssociatedResources { get; set; }
        public Entity Entity { get; set; }
        public string FilePath { get; set; }
        public string InitialBase64 { get; set; }

        public TreeNode Node { get; set; }

        public static int GetImageIndexFromExtension(string ext)
        {
            return GetTypeFromExtension(ext) + 1;
        }

        /// <summary>
        /// Gets the CRM WebResourceType from extension.
        /// </summary>
        /// <param name="ext">The file extension.</param>
        /// <returns></returns>
        public static int GetTypeFromExtension(string ext)
        {
            ext = ext.StartsWith(".") ? ext.Remove(0, 1).ToLower() : ext.ToLower();
            switch (ext)
            {
                case "htm":
                case "html":
                    return 1;

                case "css":
                    return 2;

                case "js":
                case "map":
                case "ts":
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

        public static bool IsNameInvalid(string name)
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

        public string GetPlainText()
        {
            byte[] b = Convert.FromBase64String(Entity.GetAttributeValue<string>("content"));
            return Encoding.UTF8.GetString(b);
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

        public void RefreshAssociatedContent()
        {
            foreach (var associated in AssociatedResources)
            {
                associated.Entity["content"] = Convert.ToBase64String(File.ReadAllBytes(associated.FilePath));
            }
        }

        private void LoadAssociatedResources()
        {
            if (!Options.Instance.PushTsMapFiles || string.IsNullOrWhiteSpace(FilePath) || !Path.HasExtension(FilePath) || Path.GetExtension(FilePath).ToLower() != ".js")
            {
                // Not loaded from Disk, not Extension, not Javascript 
                return;
            }

            var mapPath = FilePath + ".map";
            var name = Entity.GetAttributeValue<string>("name");
            var displayName = Entity.GetAttributeValue<string>("displayname");
            if (File.Exists(mapPath))
            {
                AssociatedResources.Add(LoadWebResourceFromDisk(mapPath, name + ".map", displayName +".map"));
            }
            var tsPath = Path.ChangeExtension(FilePath, "ts");
            if (File.Exists(tsPath))
            {
                AssociatedResources.Add(LoadWebResourceFromDisk(tsPath, Path.ChangeExtension(name, "ts"), Path.ChangeExtension(displayName, "ts")));
            }
        }
    }
}