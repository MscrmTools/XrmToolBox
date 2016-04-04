// PROJECT : MsCrmTools.SolutionImport
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using ICSharpCode.SharpZipLib.Zip;

namespace MsCrmTools.SolutionImport
{
    /// <summary>
    /// Class allowing to create zip archive
    /// </summary>
    internal class ZipManager
    {
        public static void ZipFiles(string inputFolderPath, string outputPathAndFile)
        {
            var fz = new FastZip();
            fz.CreateZip(outputPathAndFile, inputFolderPath, true, "");
        }
    }
}