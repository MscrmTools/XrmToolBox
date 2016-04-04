using System;
using System.Collections.Generic;

namespace MsCrmTools.WebResourcesManager.AppCode
{
    internal class LoadCrmResourcesSettings
    {
        public Guid SolutionId { get; set; }

        public string SolutionName { get; set; }
        public string SolutionVersion { get; set; }
        public List<int> Types { get; set; }
    }
}