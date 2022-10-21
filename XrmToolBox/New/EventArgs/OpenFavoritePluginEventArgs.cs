using System;
using XrmToolBox.AppCode;

namespace XrmToolBox.New.EventArgs
{
    public class OpenFavoritePluginEventArgs : System.EventArgs
    {
        public OpenFavoritePluginEventArgs(Favorite item, bool newConnectionNeeded = false)
        {
            Item = item;

            if (item.ConnectionId == Guid.Empty)
            {
                NewConnectionNeeded = newConnectionNeeded;
            }
        }

        public Favorite Item { get; }
        public bool NewConnectionNeeded { get; }
    }
}