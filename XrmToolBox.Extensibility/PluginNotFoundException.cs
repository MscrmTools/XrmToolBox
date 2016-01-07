using System;

namespace XrmToolBox.Extensibility
{
    [Serializable()]
    public class PluginNotFoundException : Exception
    {
        public PluginNotFoundException(string format, params object[] args)
            : base(String.Format(format, args))
        {
        }
    }
}