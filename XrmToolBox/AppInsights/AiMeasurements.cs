using System.Runtime.Serialization;

[DataContract]
public class AiMeasurements
{
    [DataMember(Name = "count")]
    public double Count { get; set; }
    [DataMember(Name = "duration")]
    public double Duration { get; set; }
}