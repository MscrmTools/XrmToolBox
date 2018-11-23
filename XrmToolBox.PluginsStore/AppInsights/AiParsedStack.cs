using System.Runtime.Serialization;

[DataContract]
public class AiParsedStack
{
    [DataMember(Name = "level")]
    public int Level { get; set; }
    [DataMember(Name = "method")]
    public string Method { get; set; }
    [DataMember(Name = "assembly")]
    public string Assembly { get; set; }
    [DataMember(Name = "fileName")]
    public string FileName { get; set; }
    [DataMember(Name = "line")]
    public int Line { get; set; }
}