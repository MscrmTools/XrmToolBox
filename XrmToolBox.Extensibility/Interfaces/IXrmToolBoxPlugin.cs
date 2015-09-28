namespace XrmToolBox.Extensibility.Interfaces
{
    public interface IXrmToolBoxPlugin
    {
        string GetCompany();

        IXrmToolBoxPluginControl GetControl();

        string GetMyType();

        string GetVersion();
    }
}