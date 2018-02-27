using XrmToolBox.AppCode;

namespace XrmToolBox.New.EventArgs
{
    public class OpenMruPluginEventArgs : System.EventArgs
    {
        public OpenMruPluginEventArgs(MostRecentlyUsedItem item)
        {
            Item = item;
        }

        public MostRecentlyUsedItem Item { get; }
    }
}