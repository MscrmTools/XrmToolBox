using System;
using System.Reflection;

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
    public bool LogExceptions { get; set; } = true;
    public bool LogMetrics { get; set; } = true;
    public bool LogTraces { get; set; } = true;
    public string OperationName { get; set; }
    public Guid SessionId { get; } = Guid.NewGuid();
    public string XtbVersion { get; set; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();

    private static string GetLastDotPart(string identifier)
    {
        return identifier == null ? null : !identifier.Contains(".") ? identifier : identifier.Substring(identifier.LastIndexOf('.') + 1);
    }
}