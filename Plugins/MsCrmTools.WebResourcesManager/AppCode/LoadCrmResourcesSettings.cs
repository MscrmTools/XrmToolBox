using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MsCrmTools.WebResourcesManager.AppCode
{
    internal class LoadCrmResourcesSettings
    {
        public Guid SolutionId { get; set; }

        public List<int> Types { get; set; }

        public string SolutionName { get; set; }

        public string SolutionVersion { get; set; }
    }
}
