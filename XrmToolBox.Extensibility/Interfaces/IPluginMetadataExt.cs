using System;

namespace XrmToolBox.Extensibility.Interfaces
{
    public interface IPluginMetadataExt : IPluginMetadata
    {
        DateTime AddedOn { get; set; }
        string AssemblyFilename { get; }
        string AssemblyQualifiedName { get; }
        string Company { get; }
        string Control { get; }
        Guid Id { get; }
        string[] Interfaces { get; }
        int numberOfUse { get; set; }
        string PluginType { get; }
        decimal Rating { get; set; }
        string Version { get; }
    }
}