using Microsoft.Xrm.Sdk;

namespace MsCrmTools.SynchronousEventOrderEditor.AppCode
{
    interface ISynchronousEvent
    {
        int Rank { get; set; }

        string EntityLogicalName { get; }

        int Stage { get; }

        string Message { get; }

        string Name { get; }

        string Description { get; }

        bool HasChanged { get; }

        string Type { get; }

        void UpdateRank(IOrganizationService service);
    }
}
