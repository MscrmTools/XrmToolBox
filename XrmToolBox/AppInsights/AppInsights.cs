/* ***********************************************************
 * XTBAppInsights.cs
 * Found at: https://gist.github.com/rappen/fbdbb644b3fffec1305b00a51b007fa6
 * Created by: Jonas Rapp https://jonasr.app/
 * Immensely inspired by: code from Jason Lattimer https://github.com/jlattimer/D365AppInsights
 *
 * Simplifies logging to Azure Application Insights from XrmToolBox tools.
 *
 * Sample from tool constructor:
 *    ai = new AppInsights(new AiConfig("https://dc.services.visualstudio.com/v2/track", "[a guid that is the key to your appinsights resource]"));
 * Sample call:
 *    ai.WriteEvent(action, count, duration, HandleAIResult);
 * Sample HandleAIResult:
 *    private void HandleAIResult(string result)
 *    {
 *        if (!string.IsNullOrEmpty(result)) { LogError("Failed to write to Application Insights:\n{0}", result); }
 *    }
 *
 *               Enjoy responsibly.
 * **********************************************************/

using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

public class AiConfig
{
    public AiConfig(string endpoint, string ikey)
    {
        AiEndpoint = endpoint;
        InstrumentationKey = ikey;
    }

    public string AiEndpoint { get; }
    public string InstrumentationKey { get; }
    public bool LogEvents { get; set; } = true;
    public string OperationName { get; set; }
    public string PluginName { get; set; } = GetLastDotPart(Assembly.GetExecutingAssembly().GetName().Name);
    public string PluginVersion { get; set; } = Assembly.GetExecutingAssembly().GetName().Version.PaddedVersion(1, 4, 2, 2);
    public Guid SessionId { get; } = Guid.NewGuid();
    public string XTBVersion { get; set; } = GetLastDotPart(Assembly.GetEntryAssembly().GetName().Name) + " " + Assembly.GetEntryAssembly().GetName().Version.PaddedVersion(1, 4, 2, 2);

    private static string GetLastDotPart(string identifier)
    {
        return identifier == null ? null : !identifier.Contains(".") ? identifier : identifier.Substring(identifier.LastIndexOf('.') + 1);
    }
}

public class AppInsights
{
    private readonly AiConfig _aiConfig;
    private int seq = 1;

    public AppInsights(AiConfig aiConfig)
    {
        _aiConfig = aiConfig;
    }

    public void WriteEvent(string eventName, double? count = null, double? duration = null, Action<string> resultHandler = null)
    {
        if (!_aiConfig.LogEvents) return;
        var logRequest = GetLogRequest("Event");
        logRequest.Data.BaseData.Name = eventName;
        if (count != null || duration != null)
        {
            logRequest.Data.BaseData.Measurements = new AiMeasurements
            {
                Count = count ?? 0,
                Duration = duration ?? 0
            };
        }
        var json = SerializeRequest<AiLogRequest>(logRequest);
        SendToAi(json, resultHandler);
    }

    public void WritePageView(string pluginName, string pluginVersion, double? count = null, double? duration = null, Action<string> resultHandler = null)
    {
        if (!_aiConfig.LogEvents) return;
        var logRequest = GetLogRequest("PageView");
        logRequest.Data.BaseData.Name = pluginName;
        logRequest.Data.BaseData.Properties = new AiProperties
        {
            PluginVersion = pluginVersion
        };

        if (count != null || duration != null)
        {
            logRequest.Data.BaseData.Measurements = new AiMeasurements
            {
                Count = count ?? 0,
                Duration = duration ?? 0
            };
        }
        var json = SerializeRequest<AiLogRequest>(logRequest);
        SendToAi(json, resultHandler);
    }

    private static string SerializeRequest<T>(object o)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            serializer.WriteObject(stream, o);
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            string json = reader.ReadToEnd();

            return json;
        }
    }

    private AiLogRequest GetLogRequest(string action)
    {
        return new AiLogRequest(seq++, _aiConfig, action);
    }

    private async void SendToAi(string json, Action<string> handleresult = null)
    {
        var result = string.Empty;
#if DEBUG
#else
        try
        {
            using (HttpClient client = HttpHelper.GetHttpClient())
            {
                var content = new StringContent(json, Encoding.UTF8, "application/x-json-stream");
                var response = await client.PostAsync(_aiConfig.AiEndpoint, content);

                if (!response.IsSuccessStatusCode)
                {
                    result = response.ToString();
                }
            }
        }
        catch (Exception e)
        {
            result = e.ToString();
        }
#endif
        handleresult?.Invoke(result);
    }
}

public class HttpHelper
{
    public static HttpClient GetHttpClient()
    {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("Connection", "close");

        return client;
    }
}

#region DataContracts

[DataContract]
public class AiBaseData
{
    [DataMember(Name = "measurements")]
    public AiMeasurements Measurements { get; set; }

    [DataMember(Name = "name")]
    public string Name { get; set; }

    [DataMember(Name = "properties")]
    public AiProperties Properties { get; set; }
}

[DataContract]
public class AiData
{
    [DataMember(Name = "baseData")]
    public AiBaseData BaseData { get; set; }

    [DataMember(Name = "baseType")]
    public string BaseType { get; set; }
}

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
            OSVersion = aiConfig.XTBVersion,
            DeviceType = aiConfig.PluginName,
            ApplicationVersion = aiConfig.PluginVersion,
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

[DataContract]
public class AiMeasurements
{
    [DataMember(Name = "count")]
    public double Count { get; set; }

    [DataMember(Name = "duration")]
    public double Duration { get; set; }
}

[DataContract]
public class AiProperties
{
    [DataMember(Name = "pluginName")]
    public string PluginName { get; set; }

    [DataMember(Name = "pluginVersion")]
    public string PluginVersion { get; set; }
}

[DataContract]
public class AiTags
{
    [DataMember(Name = "ai.application.ver")]
    public string ApplicationVersion { get; set; }

    [DataMember(Name = "ai.device.type")]
    public string DeviceType { get; set; }

    [DataMember(Name = "ai.operation.name")]
    public string OperationName { get; set; }

    [DataMember(Name = "ai.device.osVersion")]
    public string OSVersion { get; set; }

    [DataMember(Name = "ai.session.id")]
    public string SessionId { get; set; } = Guid.NewGuid().ToString();
}

#endregion DataContracts