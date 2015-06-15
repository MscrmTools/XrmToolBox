using Microsoft.Xrm.Sdk;

namespace XrmToolBox.Extensibility.Interfaces
{
    public interface IXrmToolBoxPlugin
    {
        IXrmToolBoxPluginControl GetControl();

        string GetMyType();

        string GetCompany();

        string GetVersion();
    }
}
