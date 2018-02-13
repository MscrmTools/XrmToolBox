using XrmToolBox.AppCode;

namespace XrmToolBox.TempNew.EventArgs
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