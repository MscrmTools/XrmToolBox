// PROJECT : MsCrmTools.WebResourcesManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using MsCrmTools.WebResourcesManager.AppCode;
using System;

namespace MsCrmTools.WebResourcesManager.UserControls
{
    public class WebResourceUpdatedEventArgs : EventArgs
    {
        public string Base64Content { get; set; }
        public bool IsDirty { get; set; }
        public Enumerations.WebResourceType Type { get; set; }
    }
}