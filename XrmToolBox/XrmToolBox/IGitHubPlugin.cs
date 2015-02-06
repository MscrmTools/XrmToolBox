using System;

namespace XrmToolBox
{
    public interface IGitHubPlugin
    {
        String UserName { get; }
        
        String RepositoryName { get; }
    }
}
