using System.Runtime.Serialization;

[DataContract]
public class AiDataPoint
{
    [DataMember(Name = "name")]
    public string Name { get; set; }
    [DataMember(Name = "kind")]
    public DataPointType Kind { get; set; }
    [DataMember(Name = "value")]
    public double Value { get; set; }
    [DataMember(Name = "count")]
    public int Count { get; set; }
    [DataMember(Name = "min")]
    public double Min { get; set; }
    [DataMember(Name = "max")]
    public double Max { get; set; }
    [DataMember(Name = "stdDev")]
    public double StdDev { get; set; }
}