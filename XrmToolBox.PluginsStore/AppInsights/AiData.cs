using System.Runtime.Serialization;

[DataContract]
public class AiData
{
    [DataMember(Name = "baseType")]
    public string BaseType { get; set; }
    [DataMember(Name = "baseData")]
    public AiBaseData BaseData { get; set; }
}