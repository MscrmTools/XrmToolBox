#region File header
// Project / File: XrmToolBox / XrmToolBox.Extensibility / PluginMetadata.cs
//         Author: Ahmed Elsawalhy (yagasoft.com)
//   Contributors:
//        Version: 1.2021.12.53
//        Created: 2021 / 09 / 19
#endregion

#region Imports

using System;
using XrmToolBox.Extensibility.Interfaces;

#endregion

namespace XrmToolBox.Extensibility.Manifest
{
	public class PluginMetadata : IPluginMetadataExt
	{
		public string Name { get; set; }
		public string BackgroundColor { get; set; }
		public string BigImageBase64 { get; set; }
		public string Description { get; set; }
		public string PrimaryFontColor { get; set; }
		public string SecondaryFontColor { get; set; }
		public string SmallImageBase64 { get; set; }
		public string Company { get; set; }
		public string PluginType { get; set; }
		public string Control { get; set; }
		public string AssemblyQualifiedName { get; set; }
		public string AssemblyFilename { get; set; }
		public string Version { get; set; }
		public Guid Id { get; set; }
		public string[] Interfaces { get; set; }
	}
}
