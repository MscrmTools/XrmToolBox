using System;

namespace XrmToolBox.Extensibility.Interfaces
{
    public interface IXrmToolBoxPlugin
    {
        string GetCompany();

        IXrmToolBoxPluginControl GetControl();

        string GetAssemblyQualifiedName();
        
        string GetVersion();
    }
}