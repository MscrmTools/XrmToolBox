// PROJECT : MsCrmTools.WebResourcesManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Xrm.Sdk;

namespace MsCrmTools.WebResourcesManager
{
    class Solution
    {
        public Entity InnerSolution { get; set; }
        public string CustomizationPrefix { get; set; }

        public override string ToString()
        {
            return InnerSolution != null ? InnerSolution["friendlyname"].ToString() : "(null)";
        }
    }
}
