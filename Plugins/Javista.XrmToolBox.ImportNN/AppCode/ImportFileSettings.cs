namespace Javista.XrmToolBox.ImportNN.AppCode
{
    internal class ImportFileSettings
    {
        public bool FirstAttributeIsGuid { get; set; }
        public string FirstAttributeName { get; set; }
        public string FirstEntity { get; set; }

        public string Relationship { get; set; }
        public bool SecondAttributeIsGuid { get; set; }
        public string SecondAttributeName { get; set; }
        public string SecondEntity { get; set; }
    }
}