using Microsoft.Xrm.Sdk;

namespace MsCrmTools.AccessChecker.Forms
{
    internal class ViewInfo
    {
        public Entity Entity { get; set; }

        public override string ToString()
        {
            return Entity["name"].ToString();
        }
    }
}