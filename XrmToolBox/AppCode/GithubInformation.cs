namespace XrmToolBox.AppCode
{
    public class GithubInformation
    {
        public GithubInformation()
        {}

        public GithubInformation(string version, string description)
        {
            Version = version;
            Description = description;
        }

        public string Version { get; set; }
        
        public string Description { get; set; }
    }
}
