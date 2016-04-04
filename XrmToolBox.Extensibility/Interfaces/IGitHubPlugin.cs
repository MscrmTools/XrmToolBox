using System;

namespace XrmToolBox.Extensibility.Interfaces
{
    /// <summary>
    /// This interface allows a user to specify Github user name and repository name
    /// to add a menu to redirect end user to Github issues page
    /// </summary>
    public interface IGitHubPlugin
    {
        /// <summary>
        /// Github Repository name
        /// </summary>
        String RepositoryName { get; }

        /// <summary>
        /// Github Username
        /// </summary>
        String UserName { get; }
    }
}