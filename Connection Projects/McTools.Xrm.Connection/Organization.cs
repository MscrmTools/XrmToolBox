using Microsoft.Xrm.Sdk.Discovery;

namespace McTools.Xrm.Connection
{
    public class Organization
    {
        public OrganizationDetail OrganizationDetail { get; set; }

        public override string ToString()
        {
            return this.OrganizationDetail.FriendlyName;
        }
    }
}