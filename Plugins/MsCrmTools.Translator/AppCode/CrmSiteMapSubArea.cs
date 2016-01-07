using System.Collections.Generic;

namespace MsCrmTools.Translator.AppCode
{
    internal class CrmSiteMapSubArea
    {
        public CrmSiteMapSubArea()
        {
            Titles = new Dictionary<int, string>();
            Descriptions = new Dictionary<int, string>();
        }

        public string AreaId { get; set; }
        public Dictionary<int, string> Descriptions { get; set; }
        public string GroupId { get; set; }
        public string Id { get; set; }
        public Dictionary<int, string> Titles { get; set; }
    }
}