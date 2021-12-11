using System;

namespace XrmToolBox.Extensibility.Interfaces
{
    public interface IPluginMetadataExt : IPluginMetadata
    {
	    string Company { get; }
	    string PluginType { get; }
	    string Control { get; }
	    string AssemblyQualifiedName { get; }
	    string AssemblyFilename { get; }
	    string Version { get; }
	    Guid Id { get; }
	    string[] Interfaces { get; }
    }
}