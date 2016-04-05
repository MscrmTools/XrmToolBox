namespace XrmToolBox.AppCode
{
    public class GithubInformation
    {
        public GithubInformation()
        { }

        public GithubInformation(string version, string description)
        {
            Version = version;
            Description = description;
        }

        public string Description { get; set; }
        public string PackageUrl { get; set; }
        public string Version { get; set; }
    }
}