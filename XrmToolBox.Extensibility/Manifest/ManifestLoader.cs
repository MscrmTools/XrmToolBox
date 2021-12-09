#region File header

// Project / File: XrmToolBox / XrmToolBox.Extensibility / ManifestLoader.cs
//         Author: Ahmed Elsawalhy (yagasoft.com)
//   Contributors:
//        Version: 1.2021.9.52
//        Created: 2021 / 09 / 19
//       Modified: 2021 / 09 / 19

#endregion

#region Imports

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using XrmToolBox.Extensibility.Interfaces;

#endregion

namespace XrmToolBox.Extensibility.Manifest
{
    public static class ManifestLoader
    {
        public const string ManifestFilename = "manifest.json";

        private static Manifest cachedManifest;
        private static DateTime latestModifiedDate;

        public static Manifest CreateEmptyManifest()
        {
            return new Manifest();
        }

        public static Manifest CreateManifest(IReadOnlyCollection<Lazy<IXrmToolBoxPlugin, IPluginComposedMetadata>> composition,
            DirectoryCatalog catalogue)
        {
            var pluginMetadata = composition.AsParallel()
                .Select(p =>
                    new PluginMetadata
                    {
                        BackgroundColor = p.Metadata.BackgroundColor,
                        BigImageBase64 = p.Metadata.BigImageBase64,
                        Description = p.Metadata.Description,
                        Name = p.Metadata.Name,
                        PrimaryFontColor = p.Metadata.PrimaryFontColor,
                        SecondaryFontColor = p.Metadata.SecondaryFontColor,
                        SmallImageBase64 = p.Metadata.SmallImageBase64,
                        Company = p.Value.GetCompany(),
                        Version = p.Value.GetVersion(),
                        Id = p.Value is PluginBase pb ? pb.GetId() : Guid.Empty,
                        PluginType = p.Value.GetType().FullName,
                        AssemblyQualifiedName = p.Value.GetType().Assembly.FullName,
                        AssemblyFilename = p.Value.GetType().Assembly.Location,
                        Interfaces = p.Value.GetType().GetInterfaces().Select(i => i.Name).ToArray()
                    }).ToArray();

            return
                new Manifest
                {
                    ScannedAssemblies = catalogue.LoadedFiles
                        .Select(f =>
                        new AssemblyInfo
                        {
                            Name = f,
                            Version = AssemblyName.GetAssemblyName(f).Version.ToString()
                        }).ToArray(),
                    PluginMetadata = pluginMetadata
                };
        }

        public static Manifest LoadDefaultManifest()
        {
            var manifestPath = Path.Combine(Paths.PluginsPath, ManifestFilename);
            if (!File.Exists(manifestPath))
            {
                return CreateEmptyManifest();
            }

            if (File.GetLastWriteTime(manifestPath) == latestModifiedDate)
            {
                return cachedManifest;
            }

            var manifestRaw = File.ReadAllText(manifestPath);
            cachedManifest = JsonConvert.DeserializeObject<Manifest>(manifestRaw);
            latestModifiedDate = File.GetLastWriteTime(manifestPath);

            return cachedManifest;
        }

        public static IReadOnlyCollection<Lazy<IXrmToolBoxPlugin, IPluginMetadata>> LoadPlugins(Manifest manifest)
        {
            return manifest?.PluginMetadata
                .Select(p =>
                    new Lazy<IXrmToolBoxPlugin, IPluginMetadata>(
                        () =>
                        {
                            return Activator.CreateInstance(AppDomain.CurrentDomain.GetAssemblies()
                                .First(a => a.FullName == p.AssemblyQualifiedName).GetType(p.PluginType)) as IXrmToolBoxPlugin;
                        },
                        p)).ToArray();
        }

        public static IReadOnlyCollection<Lazy<IXrmToolBoxPlugin, IPluginMetadata>> LoadPluginsDefault()
        {
            return LoadPlugins(LoadDefaultManifest());
        }

        public static Manifest ReadManifest()
        {
            var manifestPath = Path.Combine(Paths.PluginsPath, ManifestFilename);
            var manifestRaw = File.ReadAllText(manifestPath);
            return JsonConvert.DeserializeObject<Manifest>(manifestRaw);
        }

        public static void SaveManifest(Manifest manifest)
        {
            var manifestPath = Path.Combine(Paths.PluginsPath, ManifestFilename);
            var s = JsonConvert.SerializeObject(manifest);
            File.WriteAllText(manifestPath, s);
        }
    }

    public class AssemblyInfo
    {
        public string Name { get; set; }
        public string Version { get; set; }
    }
}