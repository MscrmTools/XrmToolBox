using XrmToolBox.AppCode;

namespace XrmToolBox.New.EventArgs
{
    public class OpenFavoritePluginEventArgs : System.EventArgs
    {
        public OpenFavoritePluginEventArgs(Favorite item)
        {
            Item = item;
        }

        public Favorite Item { get; }
    }
}