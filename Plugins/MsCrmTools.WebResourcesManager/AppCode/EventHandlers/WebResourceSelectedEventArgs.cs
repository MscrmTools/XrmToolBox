using MsCrmTools.WebResourcesManager.AppCode;
using System;

namespace MsCrmTools.WebResourcesManager.New.EventHandlers
{
    public class WebResourceSelectedEventArgs : EventArgs
    {
        public WebResourceSelectedEventArgs(WebResource webResource)
        {
            WebResource = webResource;
        }

        public WebResource WebResource { get; set; }
    }
}