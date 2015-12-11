using XrmToolBox.Extensibility.Interfaces;

namespace XrmToolBox.AppCode
{
    internal class PluginControlStatus
    {
        public PluginControlStatus(IXrmToolBoxPluginControl control, int? percentage, string message)
        {
            Control = control;
            Percentage = percentage;
            Message = message;
        }

        public IXrmToolBoxPluginControl Control { get; }
        public string Message { get; set; }
        public int? Percentage { get; set; }
    }
}