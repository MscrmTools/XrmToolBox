namespace XrmToolBox.AppCode
{
    public class CodePlexInformation
    {
        public CodePlexInformation()
        {}

        public CodePlexInformation(string version, string rate, string description)
        {
            Version = version;
            Rate = rate;
            Description = description;
        }

        public string Version { get; set; }

        public string Rate { get; set; }

        public string Description { get; set; }
    }
}
