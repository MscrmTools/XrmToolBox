namespace MsCrmTools.MetadataDocumentGenerator
{
    public class LanguageCode
    {
        public int Lcid { get; set; }
        public string Label { get; set; }

        public override string ToString()
        {
            return Label;
        }
    }
}
