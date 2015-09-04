namespace MsCrmTools.MetadataDocumentGenerator
{
    public class LanguageCode
    {
        public string Label { get; set; }
        public int Lcid { get; set; }

        public override string ToString()
        {
            return Label;
        }
    }
}