using System;

namespace XrmToolBox.Extensibility.Interfaces
{
    public interface IXrmToolBoxPlugin
    {
        string GetCompany();

        IXrmToolBoxPluginControl GetControl();

        string GetMyType();

        Guid GetId();

        string GetVersion();
    }
}