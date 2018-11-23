using System.Collections.Generic;
using System.Runtime.Serialization;

[DataContract]
public class AiBaseData
{
    [DataMember(Name = "ver")]
    public int Version { get; set; } = 2;

    [DataMember(Name = "message")]
    public string Message { get; set; }

    [DataMember(Name = "name")]
    public string Name { get; set; }

    [DataMember(Name = "severityLevel")]
    public string SeverityLevel { get; set; }

    [DataMember(Name = "application_Version")]
    public string ApplicationVersion { get; set; }

    [DataMember(Name = "user_Id")]
    public string UserId { get; set; }

    [DataMember(Name = "user_AuthenticatedId")]
    public string UserAuthenticatedId { get; set; }

    [DataMember(Name = "exceptions")]
    public List<AiException> Exceptions { get; set; }

    [DataMember(Name = "metrics")]
    public List<AiDataPoint> Metrics { get; set; }

    [DataMember(Name = "properties")]
    public AiProperties Properties { get; set; }

    [DataMember(Name = "measurements")]
    public AiMeasurements Measurements { get; set; }
}