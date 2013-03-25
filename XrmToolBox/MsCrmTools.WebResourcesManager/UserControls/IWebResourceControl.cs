// PROJECT : MsCrmTools.WebResourcesManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using MsCrmTools.WebResourcesManager.AppCode;

namespace MsCrmTools.WebResourcesManager.UserControls
{
    interface IWebResourceControl
    {
        string GetBase64WebResourceContent();
        void ReplaceWithNewFile(string filename);
        Enumerations.WebResourceType GetWebResourceType();
    }
}
