using System.Collections.Generic;

namespace MsCrmTools.Translator.AppCode
{
    internal class CrmSiteMapArea
    {
        public CrmSiteMapArea()
        {
            Titles = new Dictionary<int, string>();
            Descriptions = new Dictionary<int, string>();
        }

        public Dictionary<int, string> Descriptions { get; set; }
        public string Id { get; set; }
        public Dictionary<int, string> Titles { get; set; }
    }
}