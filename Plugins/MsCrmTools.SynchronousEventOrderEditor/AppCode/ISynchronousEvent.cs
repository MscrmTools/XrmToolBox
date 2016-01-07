using Microsoft.Xrm.Sdk;

namespace MsCrmTools.SynchronousEventOrderEditor.AppCode
{
    internal interface ISynchronousEvent
    {
        string Description { get; }
        string EntityLogicalName { get; }
        bool HasChanged { get; }
        string Message { get; }
        string Name { get; }
        int Rank { get; set; }
        int Stage { get; }
        string Type { get; }

        void UpdateRank(IOrganizationService service);
    }
}