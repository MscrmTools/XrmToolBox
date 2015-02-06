// PROJECT : MsCrmTools.WebResourcesManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using MsCrmTools.WebResourcesManager.AppCode;

namespace MsCrmTools.WebResourcesManager.UserControls
{
    public class WebResourceUpdatedEventArgs : EventArgs
    {
        public string Base64Content{ get; set; }
        public Enumerations.WebResourceType Type { get; set; }
        public bool IsDirty { get; set; }
    }
}
