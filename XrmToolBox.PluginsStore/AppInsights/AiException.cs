using System.Collections.Generic;
using System.Runtime.Serialization;

[DataContract]
public class AiException
{
    [DataMember(Name = "id")]
    public int Id { get; set; }
    [DataMember(Name = "typeName")]
    public string TypeName { get; set; }
    [DataMember(Name = "message")]
    public string Message { get; set; }
    [DataMember(Name = "hasFullStack")]
    public bool HasFullStack { get; set; }
    [DataMember(Name = "parsedStack")]
    public List<AiParsedStack> ParsedStacks { get; set; }
}
