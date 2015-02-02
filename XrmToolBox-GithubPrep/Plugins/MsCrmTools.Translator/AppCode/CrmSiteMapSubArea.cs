using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MsCrmTools.Translator.AppCode
{
    class CrmSiteMapSubArea
    {
        public CrmSiteMapSubArea()
        {
            Titles = new Dictionary<int, string>();
            Descriptions = new Dictionary<int, string>();
        }
        public string Id { get; set; }
        public Dictionary<int, string> Titles { get; set; }
        public Dictionary<int, string> Descriptions { get; set; }


        public string GroupId { get; set; }

        public string AreaId { get; set; }
    }
}
