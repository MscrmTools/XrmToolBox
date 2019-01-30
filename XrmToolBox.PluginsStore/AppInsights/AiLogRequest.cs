using System;
using System.Runtime.Serialization;

[DataContract]
public class AiLogRequest
{
    public AiLogRequest(int sequence, AiConfig aiConfig, string action)
    {
        Name = $"Microsoft.ApplicationInsights.{aiConfig.InstrumentationKey}.{action}";
        Time = $"{DateTime.UtcNow:O}";
        Sequence = sequence.ToString("0000000000");
        InstrumentationKey = aiConfig.InstrumentationKey;
        Tags = new AiTags
        {
            OSVersion = Environment.OSVersion.ToString(),
            //DeviceType = aiConfig.PluginName,
            ApplicationVersion = aiConfig.XtbVersion,
            SessionId = aiConfig.SessionId.ToString(),
            OperationName = aiConfig.OperationName
        };
        Data = new AiData
        {
            BaseType = $"{action}Data",
            BaseData = new AiBaseData()
        };
    }

    [DataMember(Name = "data")]
    public AiData Data { get; set; }

    [DataMember(Name = "iKey")]
    public string InstrumentationKey { get; set; }

    [DataMember(Name = "name")]
    public string Name { get; set; }

    [DataMember(Name = "seq")]
    public string Sequence { get; set; }

    [DataMember(Name = "tags")]
    public AiTags Tags { get; set; }

    [DataMember(Name = "time")]
    public string Time { get; set; }
}