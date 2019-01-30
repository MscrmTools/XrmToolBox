using System.Runtime.Serialization;

[DataContract]
public class AiProperties
{
    [DataMember(Name = "pluginName")]
    public string PluginName { get; set; }

    [DataMember(Name = "pluginVersion")]
    public string PluginVersion { get; set; }
}