#region File header
// Project / File: XrmToolBox / XrmToolBox.Extensibility / Manifest.cs
//         Author: Ahmed Elsawalhy (yagasoft.com)
//   Contributors:
//        Version: 1.2021.12.53
//        Created: 2021 / 09 / 19
#endregion

#region Imports

using System;

#endregion

namespace XrmToolBox.Extensibility.Manifest
{
	public class Manifest
	{
		public AssemblyInfo[] ScannedAssemblies { get; set; } = Array.Empty<AssemblyInfo>();
		public PluginMetadata[] PluginMetadata { get; set; } = Array.Empty<PluginMetadata>();
	}
}
