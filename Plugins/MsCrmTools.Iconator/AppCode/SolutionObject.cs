// PROJECT : MsCrmTools.Iconator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Xrm.Sdk;

namespace MsCrmTools.Iconator.AppCode
{
    public class SolutionObject
    {
        public Entity Solution { get; set; }

        public override string ToString()
        {
            return Solution["uniquename"].ToString();
        }
    }
}