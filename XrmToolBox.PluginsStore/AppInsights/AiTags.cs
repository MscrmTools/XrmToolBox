using System;
using System.Runtime.Serialization;

[DataContract]
public class AiTags
{
    [DataMember(Name = "ai.session.id")]
    public string SessionId { get; set; } = Guid.NewGuid().ToString();

    [DataMember(Name = "ai.operation.name")]
    public string OperationName { get; set; }

    [DataMember(Name = "ai.application.ver")]
    public string ApplicationVersion { get; set; }

    [DataMember(Name = "ai.device.osVersion")]
    public string OSVersion { get; set; }

    [DataMember(Name = "ai.device.type")]
    public string DeviceType { get; set; }
}