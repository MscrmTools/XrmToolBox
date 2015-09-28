using System;

namespace XrmToolBox.Extensibility.Interfaces
{
    /// <summary>
    /// This interface allows a user to specify CodePlex project name to add a
    /// menu to redirect end user to CodePlex project pages (issues, discussions)
    /// </summary>
    public interface ICodePlexPlugin
    {
        /// <summary>
        /// Name of the CodePlex project to build a CodePlex url formatted as
        /// following : http://CodePlexUrlName.codeplex.com
        /// </summary>
        String CodePlexUrlName { get; }
    }
}