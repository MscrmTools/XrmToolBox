#region License

/*
 * Copyright (c) 2013-2014, Milosz Krajewski
 * 
 * Microsoft Public License (Ms-PL)
 * This license governs use of the accompanying software. 
 * If you use the software, you accept this license. 
 * If you do not accept the license, do not use the software.
 * 
 * 1. Definitions
 * The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same 
 * meaning here as under U.S. copyright law.
 * A "contribution" is the original software, or any additions or changes to the software.
 * A "contributor" is any person that distributes its contribution under this license.
 * "Licensed patents" are a contributor's patent claims that read directly on its contribution.
 * 
 * 2. Grant of Rights
 * (A) Copyright Grant- Subject to the terms of this license, including the license conditions 
 * and limitations in section 3, each contributor grants you a non-exclusive, worldwide, 
 * royalty-free copyright license to reproduce its contribution, prepare derivative works of 
 * its contribution, and distribute its contribution or any derivative works that you create.
 * (B) Patent Grant- Subject to the terms of this license, including the license conditions and 
 * limitations in section 3, each contributor grants you a non-exclusive, worldwide, 
 * royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, 
 * import, and/or otherwise dispose of its contribution in the software or derivative works of 
 * the contribution in the software.
 * 
 * 3. Conditions and Limitations
 * (A) No Trademark License- This license does not grant you rights to use any contributors' name, 
 * logo, or trademarks.
 * (B) If you bring a patent claim against any contributor over patents that you claim are infringed 
 * by the software, your patent license from such contributor to the software ends automatically.
 * (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, 
 * and attribution notices that are present in the software.
 * (D) If you distribute any portion of the software in source code form, you may do so only under this 
 * license by including a complete copy of this license with your distribution. If you distribute 
 * any portion of the software in compiled or object code form, you may only do so under a license 
 * that complies with this license.
 * (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express
 * warranties, guarantees or conditions. You may have additional consumer rights under your local 
 * laws which this license cannot change. To the extent permitted under your local laws, the 
 * contributors exclude the implied warranties of merchantability, fitness for a particular 
 * purpose and non-infringement.
 */

#endregion

#region conditinals

#if !LIBZ_MANAGER && !LIBZ_BOOTSTRAP
#define LIBZ_INTERNAL
#endif

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Win32;
#if !NET35
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
#endif
#if !LIBZ_MANAGER
using LibZ.Bootstrap.Internal;
#endif

/*
 * NOTE: This file is a little bit messy and contains multiple classes and namespaces.
 * It is much easier to embed it directly into other library, without referencing
 * Lib.Bootstrap. It does not look nice, but makes embedding LibZResolver easy. 
 * Just drag this file into assembly and you will have access to fully functional LibZResolver.
 */

#if LIBZ_MANAGER
namespace LibZ.Manager
#else

namespace LibZ.Bootstrap
#endif
{

	#region declare visibility

#if LIBZ_MANAGER

	// in LibZ.Manager all clases are actually interface classes

	// internal partial class LibZResolver { };

	namespace Internal
	{
		public partial class LibZEntry { };

		public partial class LibZReader { };
	}

#elif LIBZ_BOOTSTRAP

	// in LibZ.Bootstrap LibZResolver needs to be exposed

	public partial class LibZResolver { };

	namespace Internal
	{
		internal partial class LibZEntry { };

		internal partial class LibZReader { };
	}

#else

	// if file is just embedded into another assembly it should be all internal

	internal partial class LibZResolver { };

	namespace Internal
	{
		internal partial class LibZEntry { };
		internal partial class LibZReader { };
	}

#endif

	#endregion

#if !LIBZ_MANAGER

	#region class LibZResolver

	/// <summary>Assembly resolver and repository of .libz files.</summary>
	partial class LibZResolver
	{
		#region consts

		/// <summary>
		///     Regular expression for embedded containers.
		/// </summary>
		private static readonly Regex LibZResourceNameRx = new Regex(
			@"^libz://[0-9A-Fa-f]{32}$",
			RegexOptions.IgnoreCase);

		private const StringComparison IgnoreCase = StringComparison.OrdinalIgnoreCase;

		#endregion

		#region static fields

		/*
		 * NOTE: This fields are populated ONLY by first caller.
		 * The rest of them are reusing it.
		 */

		/// <summary>The reader/writer lock for containers.</summary>
		private static readonly ReaderWriterLockSlim Lock;

		/// <summary>The containers.</summary>
		private static readonly List<LibZReader> ContainerRepository;

		private static readonly Dictionary<Guid, Assembly> LoadedAssemblies;

		#endregion

		#region shared static properies

		/// <summary>The shared dictionary.</summary>
		private static readonly GlobalDictionary SharedData =
			new GlobalDictionary("LibZResolver.71c503c0c0824d9785f4994d5034c8a0");

		/// <summary>Gets or sets the register stream callback.</summary>
		/// <value>The register stream callback.</value>
		private static Func<Stream, Guid> RegisterStreamCallback
		{
			get { return SharedData.Get<Func<Stream, Guid>>(0); }
			set { SharedData.Set(0, value); }
		}

		/// <summary>Gets or sets the decoders dictionary.</summary>
		/// <value>The decoders dictionary.</value>
		private static Dictionary<string, Func<byte[], int, byte[]>> Decoders
		{
			get { return SharedData.Get<Dictionary<string, Func<byte[], int, byte[]>>>(1); }
			set { SharedData.Set(1, value); }
		}

		/// <summary>
		///     Gets the search path. Please note, this list is not thread save.
		///     If you are going to modify it, put it inside lock statement.
		/// </summary>
		/// <value>The search path.</value>
		public static List<string> SearchPath
		{
			get { return SharedData.Get<List<string>>(3); }
			private set { SharedData.Set(3, value); }
		}

#if !NET35

		/// <summary>Gets or sets the GetCatalog callback.</summary>
		/// <value>The GetCatalog callback.</value>
		private static Func<Guid, ComposablePartCatalog> GetCatalogCallback
		{
			get { return SharedData.Get<Func<Guid, ComposablePartCatalog>>(4); }
			set { SharedData.Set(4, value); }
		}

		/// <summary>Gets or sets the GetAllCatalogs callback.</summary>
		/// <value>The GetAllCatalogs callback.</value>
		private static Func<IEnumerable<ComposablePartCatalog>> GetAllCatalogsCallback
		{
			get { return SharedData.Get<Func<ComposablePartCatalog[]>>(5); }
			set { SharedData.Set(5, value); }
		}

#endif

		#endregion

		#region static constructor

		/// <summary>
		///     Initializes the <see cref="LibZResolver" /> class.
		/// </summary>
		static LibZResolver()
		{
			// this is VERY bad, I know
			// there are potentially 2 classes, they have same name, they should
			// share same data, but they are not the SAME class, so to interlock them
			// I need something known to both of them
			lock (typeof(object))
			{
				if (!SharedData.IsOwner)
					return;

				ContainerRepository = new List<LibZReader>();
				LoadedAssemblies = new Dictionary<Guid, Assembly>();
				Lock = new ReaderWriterLockSlim();

				// intialize paths
				var searchPath = new List<string> { AppDomain.CurrentDomain.BaseDirectory };
				var systemPath = Environment.GetEnvironmentVariable("PATH") ?? string.Empty;
				searchPath.AddRange(systemPath.Split(';').Where(p => !IsEmptyString(p)));

				RegisterStreamCallback = RegisterStreamImplementation;

#if !NET35
				GetCatalogCallback = GetCatalogImplementation;
				GetAllCatalogsCallback = GetAllCatalogsImplementation;
#endif

				Decoders = new Dictionary<string, Func<byte[], int, byte[]>>();
				SearchPath = searchPath;

				RegisterDecoder("none", LibZReader.NoneDecoder);
				RegisterDecoder("deflate", LibZReader.DeflateDecoder);

				// initialize assembly resolver
				AppDomain.CurrentDomain.AssemblyResolve += (s, e) => Resolve(e);
			}
		}

		private static Guid RegisterStreamImplementation(Stream stream)
		{
			var container = new LibZReader(stream);

			Lock.EnterUpgradeableReadLock();
			try
			{
				var existing = ContainerRepository.FirstOrDefault(c => c.ContainerId == container.ContainerId);
				if (existing != null)
					return existing.ContainerId;

				Lock.EnterWriteLock();
				try
				{
					ContainerRepository.Add(container);
					return container.ContainerId;
				}
				finally
				{
					Lock.ExitWriteLock();
				}
			}
			finally
			{
				Lock.ExitUpgradeableReadLock();
			}
		}

#if !NET35

		private static ComposablePartCatalog GetCatalogImplementation(Guid guid)
		{
			Lock.EnterReadLock();
			try
			{
				return new LibZCatalog(
					ContainerRepository.FirstOrDefault(c => c.ContainerId == guid));
			}
			finally
			{
				Lock.ExitReadLock();
			}
		}

		private static ComposablePartCatalog[] GetAllCatalogsImplementation()
		{
			Lock.EnterReadLock();
			try
			{
				return ContainerRepository
					.Select(c => new LibZCatalog(c))
					.Cast<ComposablePartCatalog>()
					.ToArray();
			}
			finally
			{
				Lock.ExitReadLock();
			}
		}

#endif

		#endregion

		#region public interface

		/// <summary>
		///     Application startup. It is used to postpone assembly loading until
		///     LibZResolver is intialized.
		/// </summary>
		/// <param name="action">The action.</param>
		/// <returns>Exit code.</returns>
		public static int Startup(Action action)
		{
			action();
			return 0;
		}

		/// <summary>
		///     Application startup. It is used to postpone assembly loading until
		///     LibZResolver is intialized.
		/// </summary>
		/// <param name="action">The action.</param>
		/// <returns>
		///     Exit code returned by <paramref name="action" />.
		/// </returns>
		public static int Startup(Func<int> action)
		{
			return action();
		}

#if !NET35

		/// <summary>Gets the catalog.</summary>
		/// <param name="handle">The handle.</param>
		/// <returns>Catalog for given container.</returns>
		public static ComposablePartCatalog GetCatalog(Guid handle)
		{
			return GetCatalogCallback(handle);
		}

		/// <summary>Gets the catalogs.</summary>
		/// <param name="handles">The container handles.</param>
		/// <returns>Collection of catalogs.</returns>
		public static IEnumerable<ComposablePartCatalog> GetCatalogs(IEnumerable<Guid> handles)
		{
			return handles
				.Where(h => h != Guid.Empty)
				.Select(h => GetCatalogCallback(h))
				.Where(c => c != null)
				.ToArray();
		}

		/// <summary>Gets all catalogs.</summary>
		/// <returns>Collection of catalogs.</returns>
		public static IEnumerable<ComposablePartCatalog> GetAllCatalogs()
		{
			return GetAllCatalogsCallback();
		}

#endif

		/// <summary>Registers the container.</summary>
		/// <param name="stream">The stream.</param>
		/// <param name="optional">
		///     if set to <c>true</c> container is optional,
		///     so failure to load does not cause exception.
		/// </param>
		/// <returns>GUID of added container.</returns>
		/// <exception cref="System.ArgumentNullException">stream</exception>
		public static Guid RegisterStreamContainer(
			Stream stream, bool optional = true)
		{
			try
			{
				if (stream == null)
					throw new ArgumentNullException("stream");
				return RegisterStreamCallback(stream);
			}
			catch (Exception e)
			{
				Helpers.Error(e, optional);
				if (!optional)
					throw;
				return Guid.Empty;
			}
		}

		/// <summary>Registers the container from file.</summary>
		/// <param name="libzFileName">Name of the libz file.</param>
		/// <param name="optional">
		///     if set to <c>true</c> container is optional,
		///     so failure to load does not cause exception.
		/// </param>
		/// <returns>GUID of added container.</returns>
		/// <exception cref="System.IO.FileNotFoundException"></exception>
		public static Guid RegisterFileContainer(
			string libzFileName, bool optional = true)
		{
			try
			{
				var fileName = FindFile(libzFileName);

				if (fileName == null)
					throw new FileNotFoundException(
						string.Format("LibZ library '{0}' cannot be found", libzFileName));

				Helpers.Debug(string.Format("Opening library '{0}'", fileName));

				// file will be locked for writing but it will be possible to 
				// have multiple processes reading it
				var stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);

				return RegisterStreamContainer(stream);
			}
			catch (Exception e)
			{
				Helpers.Error(e, optional);
				if (!optional)
					throw;
				return Guid.Empty;
			}
		}

		/// <summary>Registers the container from resources.</summary>
		/// <param name="assemblyHook">The assembly hook.</param>
		/// <param name="libzFileName">Name of the libz file.</param>
		/// <param name="optional">
		///     if set to <c>true</c> container is optional,
		///     so failure to load does not cause exception.
		/// </param>
		/// <returns>GUID of added container.</returns>
		public static Guid RegisterResourceContainer(
			Type assemblyHook, string libzFileName, bool optional = true)
		{
			try
			{
				var resourceName =
					string.Format("libz://{0:N}",
						Hash.Get(Path.GetFileName(libzFileName) ?? string.Empty));
				Helpers.Debug(string.Format("Opening library '{0}' from '{1}'", libzFileName, resourceName));
				var stream = assemblyHook.Assembly.GetManifestResourceStream(resourceName);

				if (stream == null)
					throw new MissingManifestResourceException(
						string.Format("Resource '{0}' could not be found", resourceName));

				return RegisterStreamContainer(stream);
			}
			catch (Exception e)
			{
				Helpers.Error(e, optional);
				if (!optional)
					throw;
				return Guid.Empty;
			}
		}

		/// <summary>Registers the multiple contrainers using wildcards.</summary>
		/// <param name="libzFileNamePattern">The libz file name pattern (.\*.libz is used if not provided).</param>
		/// <returns>Collection of GUIDs identifying registered containers.</returns>
		public static IEnumerable<Guid> RegisterMultipleFileContainers(string libzFileNamePattern = null)
		{
			try
			{
				if (libzFileNamePattern == null)
					libzFileNamePattern = ".\\";
				var folder = Path.GetDirectoryName(libzFileNamePattern);
				if (IsEmptyString(folder))
					folder = ".";
				var pattern = Path.GetFileName(libzFileNamePattern);
				if (IsEmptyString(pattern))
					pattern = "*.libz";
				var proxies = Directory
					// ReSharper disable AssignNullToNotNullAttribute
					.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder), pattern)
					// ReSharper restore AssignNullToNotNullAttribute
					.Select(fn => RegisterFileContainer(fn))
					.Where(p => p != Guid.Empty)
					.ToArray();

				return proxies;
			}
			catch
			{
				// do nothing - they are all 'optional' by default
				return new Guid[0];
			}
		}

		/// <summary>Registers all contrainers from assembly.</summary>
		/// <param name="assemblyHook">The assembly hook.</param>
		/// <returns>Collection of GUIDs identifying registered containers.</returns>
		public static IEnumerable<Guid> RegisterAllResourceContainers(Type assemblyHook)
		{
			try
			{
				var resourceNames = assemblyHook.Assembly
					.GetManifestResourceNames()
					.Where(name => LibZResourceNameRx.IsMatch(name));
				var proxies = resourceNames
					.Select(rn => assemblyHook.Assembly.GetManifestResourceStream(rn))
					.Where(rs => rs != null)
					.Select(rs => RegisterStreamContainer(rs))
					.ToArray();

				return proxies;
			}
			catch
			{
				// do nothing - they are all 'optional' by default
				return new Guid[0];
			}
		}

		/// <summary>Registers the decoder.</summary>
		/// <param name="codecName">Name of the codec.</param>
		/// <param name="decoder">The decoder function.</param>
		/// <param name="overwrite">
		///     if set to <c>true</c> overwrites previously registered decoder.
		///     Useful when decoder has multiple versions (for example safe and unsafe one) but at startup
		///     we have access to only one of them.
		/// </param>
		/// <exception cref="System.ArgumentException">codecName is null or empty.</exception>
		/// <exception cref="System.ArgumentNullException">decoder is null.</exception>
		public static void RegisterDecoder(string codecName, Func<byte[], int, byte[]> decoder, bool overwrite = false)
		{
			if (String.IsNullOrEmpty(codecName))
				throw new ArgumentException("codecName is null or empty.", "codecName");
			if (decoder == null)
				throw new ArgumentNullException("decoder", "decoder is null.");

			if (overwrite)
			{
				lock (Decoders)
					Decoders[codecName] = decoder;
			}
			else
			{
				try
				{
					lock (Decoders)
						Decoders.Add(codecName, decoder);
				}
				catch
				{
					Helpers.Warn(string.Format("Codec '{0}' already registered", codecName));
				}
			}
		}

		#endregion

		#region private implementation

		private static bool IsEmptyString(string text)
		{
#if !NET35
			return string.IsNullOrWhiteSpace(text);
#else
			return string.IsNullOrEmpty(text) || text.All(char.IsWhiteSpace);
#endif
		}

		#region resolve

		/// <summary>Tries to load missing assembly from LibZ containers.</summary>
		/// <param name="args">
		///     The <see cref="ResolveEventArgs" /> instance containing the event data.
		/// </param>
		/// <returns>
		///     Loaded assembly (or <c>null</c>)
		/// </returns>
		private static Assembly Resolve(ResolveEventArgs args)
		{
			try
			{
				var name = args.Name;
				Helpers.Debug(string.Format("Resolving: '{0}'", name));
				var result =
					TryLoadAssembly3(name) ??
						MatchByPartialName(name).Select(TryLoadAssembly3).FirstOrDefault(a => a != null);

				if (result != null)
					Helpers.Debug(string.Format("Found: '{0}'", name));
				else
					Helpers.Warn(string.Format("Not found: '{0}'", name));

				return result;
			}
			catch (Exception e)
			{
				Helpers.Error(e);
			}

			return null;
		}

		/// <summary>Finds all assemblies which match given short name.</summary>
		/// <param name="shortAssemblyName">The short name.</param>
		/// <returns>Collection of full assembly names.</returns>
		private static IEnumerable<string> MatchByPartialName(string shortAssemblyName)
		{
			Lock.EnterReadLock();
			try
			{
				var expectedAssemblyName = new AssemblyName(shortAssemblyName);

				// from all containers return assemblyNames matching given string by short name
				// please note, the container is not returned, so when looking for this name
				// it will check all the containers again, waste of time but for hard to explain reason
				// it will allow to this in "better" order
				return ContainerRepository
					.SelectMany(c => c
						.Entries
						.Select(e => e.AssemblyName)
						.Where(n => IsMatchByPartialName(n, expectedAssemblyName)))
					.Distinct()
					.OrderByDescending(an => an.Version)
					.Select(an => an.FullName)
					.ToArray(); // needs to be buffered before returning
			}
			finally
			{
				Lock.ExitReadLock();
			}
		}

		/// <summary>
		/// Determines whether assembly name matches expected (usually partial) name.
		/// </summary>
		/// <param name="actualAssemblyName">Actual name of the assembly.</param>
		/// <param name="expectedAssemblyName">Expected name of the assembly.</param>
		/// <returns></returns>
		private static bool IsMatchByPartialName(
			AssemblyName actualAssemblyName, AssemblyName expectedAssemblyName)
		{
			if (string.IsNullOrEmpty(expectedAssemblyName.Name))
				return false;

			if (string.Compare(expectedAssemblyName.Name, actualAssemblyName.Name, IgnoreCase) != 0)
				return false;

			if (expectedAssemblyName.CultureInfo != null)
				if (expectedAssemblyName.CultureInfo.LCID != actualAssemblyName.CultureInfo.LCID)
					return false;

			if (expectedAssemblyName.Version != null)
				if (expectedAssemblyName.Version != actualAssemblyName.Version)
					return false;

			var expectedToken = expectedAssemblyName.GetPublicKeyToken();
			if (expectedToken != null)
				if (!EqualTokens(expectedToken, actualAssemblyName.GetPublicKeyToken()))
					return false;

			return true;
		}

		/// <summary>Checks if two tokens are the same.</summary>
		/// <param name="expectedToken">The expected token.</param>
		/// <param name="actualToken">The actual token.</param>
		/// <returns><c>true</c> if tokens are the same, <c>false</c> otherwise.</returns>
		private static bool EqualTokens(byte[] expectedToken, byte[] actualToken)
		{
			if (expectedToken == actualToken)
				return true;
			if (expectedToken == null || actualToken == null)
				return false;

			var expectedLength = expectedToken.Length;
			if (expectedLength != actualToken.Length)
				return false;
			for (var i = 0; i < expectedLength; i++)
				if (expectedToken[i] != actualToken[i])
					return false;
			return true;
		}

		/// <summary>Tries the load assembly for 3 platforms, native, any cpu, then "opossite".</summary>
		/// <param name="assemblyName">Name of the assembly.</param>
		/// <returns>Loaded assembly or <c>null</c>.</returns>
		private static Assembly TryLoadAssembly3(string assemblyName)
		{
			return
				// try native one first
				TryLoadAssembly((IntPtr.Size == 4 ? "x86:" : "x64:") + assemblyName) ??
				// ...then AnyCPU
					TryLoadAssembly(assemblyName) ??
				// ...then try the opposite platform (as far as I understand x64 may use x86)
						(IntPtr.Size == 8 ? TryLoadAssembly("x86:" + assemblyName) : null);
		}

		/// <summary>Tries to load assembly by its resource name.</summary>
		/// <param name="resourceName">Name of the resource.</param>
		/// <returns>Loaded assembly or <c>null</c>.</returns>
		private static Assembly TryLoadAssembly(string resourceName)
		{
			var guid = Hash.Get(resourceName ?? string.Empty);
			LibZReader[] containers;

			Lock.EnterReadLock();
			try
			{
				containers = ContainerRepository.ToArray();
			}
			finally
			{
				Lock.ExitReadLock();
			}

			// this needs to be outside lock
			// because it is possible that loaded module initialization 
			// may want to acquire write lock immediately
			return containers
				.Select(c => TryLoadAssembly(c, guid))
				.FirstOrDefault(a => a != null);
		}

		/// <summary>Tries the load assembly.</summary>
		/// <param name="container">The container.</param>
		/// <param name="guid">The GUID.</param>
		/// <returns>
		///     Loaded assembly or <c>null</c>
		/// </returns>
		private static Assembly TryLoadAssembly(LibZReader container, Guid guid)
		{
			var entry = container.TryGetEntry(guid);
			if (entry == null)
				return null;

			try
			{
				return CacheAssembly(guid, () => {
					var data = container.GetBytes(entry, Decoders);

					// managed assemblies can be loaded straight from memory
					return entry.Unmanaged || entry.Portable || entry.SafeLoad
						? LoadUnmanagedAssembly(container.ContainerId, guid, data)
						: Assembly.Load(data);
				});
			}
			catch (Exception e)
			{
				Helpers.Error(e);
				return null;
			}
		}

		private static Assembly CacheAssembly(Guid guid, Func<Assembly> loader)
		{
			lock (LoadedAssemblies)
			{
				Assembly cached;
				if (LoadedAssemblies.TryGetValue(guid, out cached))
					return cached;
			}

			var loaded = loader();

			lock (LoadedAssemblies)
			{
				Assembly cached;
				if (LoadedAssemblies.TryGetValue(guid, out cached))
					return cached;
				if (loaded != null)
					LoadedAssemblies[guid] = loaded;
			}

			return loaded;
		}

		/// <summary>Tries the load unmanaged assembly.</summary>
		/// <param name="containerId">The container id.</param>
		/// <param name="assemblyId">The assembly id.</param>
		/// <param name="data">The data.</param>
		/// <returns>Loaded assembly.</returns>
		private static Assembly LoadUnmanagedAssembly(Guid containerId, Guid assemblyId, byte[] data)
		{
			// unmanaged ones needs to be saved first
			var folderPath = Path.Combine(
				Path.GetTempPath(),
				containerId.ToString("N"));
			Directory.CreateDirectory(folderPath);

			var filePath = Path.Combine(folderPath, assemblyId.ToString("N") + ".dll");

			// if file exits and length is matching do not write it
			// from security point of view this is not the best approach
			// but it saves some time
			var fileInfo = new FileInfo(filePath);
			if (!fileInfo.Exists || fileInfo.Length != data.Length)
				File.WriteAllBytes(filePath, data);

			return Assembly.LoadFrom(filePath);
		}

		#endregion

		#region find files

		/// <summary>Finds the file on search path.</summary>
		/// <param name="libzFileName">Name of the libz file.</param>
		/// <returns>
		///     Full path of found LibZ file, or <c>null</c>.
		/// </returns>
		private static string FindFile(string libzFileName)
		{
			if (Path.IsPathRooted(libzFileName))
				return File.Exists(libzFileName) ? libzFileName : null;

			return SearchPath
				.Select(p => Path.GetFullPath(Path.Combine(p, libzFileName)))
				.FirstOrDefault(File.Exists);
		}

		#endregion

		#endregion
	}

	#endregion

#endif

	namespace Internal
	{
		#region class LibZReader

		#region class Entry

		/// <summary>Single container entry.</summary>
		partial class LibZEntry
		{
			#region enum EntryFlags

			/// <summary>Flags on every entry in container.</summary>
			[Flags]
			public enum EntryFlags
			{
				/// <summary>None.</summary>
				None = 0x00,

				/// <summary>Indicates unamanged assembly.</summary>
				Unmanaged = 0x01,

				/// <summary>Set when assembly is targeting AnyCPU architecture.</summary>
				AnyCPU = 0x02,

				/// <summary>Set when assembly is targeting 64-bit architectule.</summary>
				X64 = 0x04,

				/// <summary>Indicates PCL assembly.</summary>
				Portable = 0x08,

				/// <summary>The flag indicating the 'safe load' should be used.</summary>
				SafeLoad = 0x10,
			}

			#endregion

			#region stored properties

			/// <summary>Gets or sets the hash.</summary>
			/// <value>The hash.</value>
			public Guid Id { get; protected internal set; }

			/// <summary>Gets or sets the name of the assembly.</summary>
			/// <value>The name of the assembly.</value>
			public AssemblyName AssemblyName { get; protected internal set; }

			/// <summary>Gets or sets the flags.</summary>
			/// <value>The flags.</value>
			public EntryFlags Flags { get; protected internal set; }

			/// <summary>Gets or sets the offset.</summary>
			/// <value>The offset.</value>
			public long Offset { get; protected internal set; }

			/// <summary>Gets or sets the length of the original stream.</summary>
			/// <value>The length of the original stream.</value>
			public int OriginalLength { get; protected internal set; }

			/// <summary>Gets or sets the length of the storage.</summary>
			/// <value>The length of the storage.</value>
			public int StorageLength { get; protected internal set; }

			/// <summary>Gets or sets the codec name.</summary>
			/// <value>The codec name.</value>
			public string CodecName { get; protected internal set; }

			/// <summary>MD5 hash.</summary>
			/// <value>The MD5 hash.</value>
			public Guid Hash { get; protected internal set; }

			#endregion

			#region derived properties

			/// <summary>Gets a value indicating whether assembly is unmanaged.</summary>
			/// <value><c>true</c> if unmanaged; otherwise, <c>false</c>.</value>
			public bool Unmanaged { get { return (Flags & EntryFlags.Unmanaged) != 0; } }

			/// <summary>Gets a value indicating whether assembly is portable.</summary>
			/// <value><c>true</c> if portable; otherwise, <c>false</c>.</value>
			public bool Portable { get { return (Flags & EntryFlags.Portable) != 0; } }

			/// <summary>Gets a value indicating whether 'safe load' have to be forced.</summary>
			/// <value><c>true</c> if 'safe load' is required; otherwise, <c>false</c>.</value>
			public bool SafeLoad { get { return (Flags & EntryFlags.SafeLoad) != 0; } }

			#endregion

			#region constructor

			/// <summary>
			///     Initializes a new instance of the <see cref="LibZEntry" /> class.
			/// </summary>
			public LibZEntry() { }

			/// <summary>
			///     Initializes a new instance of the <see cref="LibZEntry" /> class.
			///     Copies all field from other object.
			/// </summary>
			/// <param name="other">The other entry.</param>
			public LibZEntry(LibZEntry other)
			{
				Id = other.Id;
				Hash = other.Hash;
				AssemblyName = other.AssemblyName;
				Flags = other.Flags;
				Offset = other.Offset;
				OriginalLength = other.OriginalLength;
				StorageLength = other.StorageLength;
				CodecName = other.CodecName;
			}

			#endregion
		}

		#endregion

		/// <summary>LibZ file container. Read-only aspect.</summary>
		partial class LibZReader: IDisposable
		{
			#region consts

			/// <summary>The magic identifer on container files.</summary>
			protected static readonly Guid Magic = new Guid(Encoding.ASCII.GetBytes("LibZContainer103"));

			/// <summary>The version of container format.</summary>
			protected const int CurrentVersion = 103;

			/// <summary>The length of GUID</summary>
			protected static readonly int GuidLength = Guid.Empty.ToByteArray().Length; // that's nasty, but reliable

			#endregion

			#region static fields

			/// <summary>The registered decoders.</summary>
			private static readonly Dictionary<string, Func<byte[], int, byte[]>> Decoders
				= new Dictionary<string, Func<byte[], int, byte[]>>();

			#endregion

			#region fields

			// ReSharper disable InconsistentNaming

			/// <summary>The container id</summary>
			protected Guid _containerId = Guid.Empty;

			/// <summary>The offset where metadata starts.</summary>
			protected long _magicOffset;

			/// <summary>The actual version of container file format.</summary>
			private int _version;

			/// <summary>The map of entries.</summary>
			protected Dictionary<Guid, LibZEntry> _entries = new Dictionary<Guid, LibZEntry>();

			/// <summary>The container stream.</summary>
			protected Stream _stream;

			/// <summary>The binary reader.</summary>
			protected BinaryReader _reader;

			/// <summary>Indicates if object has been already disposed.</summary>
			private bool _disposed;

			// ReSharper restore InconsistentNaming

			#endregion

			#region properties

			/// <summary>Gets the container id.</summary>
			/// <value>The container id.</value>
			public Guid ContainerId
			{
				get { return _containerId; }
			}

			/// <summary>Gets the entries in the container.</summary>
			public IEnumerable<LibZEntry> Entries
			{
				get { return _entries.Values; }
			}

			#endregion

			#region static constructor

			/// <summary>
			///     Initializes the <see cref="LibZReader" /> class.
			/// </summary>
			static LibZReader()
			{
				RegisterDecoder("deflate", DeflateDecoder);
			}

			#endregion

			#region constructor

			/// <summary>
			///     Initializes a new instance of the <see cref="LibZReader" /> class.
			/// </summary>
			protected LibZReader() { }

			/// <summary>
			///     Initializes a new instance of the <see cref="LibZReader" /> class.
			/// </summary>
			/// <param name="stream">The stream.</param>
			public LibZReader(Stream stream)
			{
				_stream = stream;
				_reader = new BinaryReader(_stream);
				OpenFile();
			}

			/// <summary>Opens the file.</summary>
			/// <exception cref="System.ArgumentException">Container file seems to be corrupted.</exception>
			/// <exception cref="System.NotSupportedException">Not supported version of container file.</exception>
			protected void OpenFile()
			{
				lock (_stream)
				{
					_stream.Position = 0;
					var guid = new Guid(_reader.ReadBytes(GuidLength));
					if (guid != Magic)
						throw new ArgumentException("Invalid LibZ file header");
					_version = _reader.ReadInt32();
					if (_version != CurrentVersion)
						throw new NotSupportedException(
							string.Format("Unsupported LibZ file version ({0})", _version));
					_stream.Position = _stream.Length - GuidLength - sizeof(long);
					_magicOffset = _reader.ReadInt64();
					guid = new Guid(_reader.ReadBytes(GuidLength));
					if (guid != Magic)
						throw new ArgumentException("Invalid LibZ file footer");
					_stream.Position = _magicOffset;
					_containerId = new Guid(_reader.ReadBytes(GuidLength));
					var count = _reader.ReadInt32();
					for (var i = 0; i < count; i++)
					{
						var entry = ReadEntry(_reader);
						_entries.Add(entry.Id, entry);
					}
				}
			}

			#endregion

			#region public interface

			/// <summary>Registers the decoder.</summary>
			/// <param name="codecName">The codec name.</param>
			/// <param name="decoder">The decoder.</param>
			/// <param name="overwrite">
			///     if set to <c>true</c> overwrites previously registered decoder.
			///     Useful when decoder has multiple versions (for example safe and unsafe one) but at startup
			///     we have access to only one of them.
			/// </param>
			/// <exception cref="System.ArgumentException">codecName is null or empty</exception>
			/// <exception cref="System.ArgumentNullException">decoder is null.</exception>
			public static void RegisterDecoder(string codecName, Func<byte[], int, byte[]> decoder, bool overwrite = false)
			{
				if (String.IsNullOrEmpty(codecName))
					throw Helpers.Error(new ArgumentException("codecName is null or empty.", "codecName"));
				if (decoder == null)
					throw Helpers.Error(new ArgumentNullException("decoder", "decoder is null."));

				var decoders = Decoders;

				if (overwrite)
				{
					lock (decoders)
						decoders[codecName] = decoder;
				}
				else
				{
					try
					{
						lock (decoders)
							decoders.Add(codecName, decoder);
					}
					catch (ArgumentException e)
					{
						throw Helpers.Error(new ArgumentException(
							string.Format("Codec '{0}' already registered", codecName), e));
					}
				}
			}

			/// <summary>Tries the get entry.</summary>
			/// <param name="guid">The GUID.</param>
			/// <returns>
			///     Entry with specified GUID or <c>null</c>.
			/// </returns>
			public LibZEntry TryGetEntry(Guid guid)
			{
				LibZEntry result;
				_entries.TryGetValue(guid, out result);
				return result;
			}

			/// <summary>Gets the bytes for given resource.</summary>
			/// <param name="entry">The entry.</param>
			/// <param name="decoders">The decoders.</param>
			/// <returns>Buffer of bytes.</returns>
			public byte[] GetBytes(LibZEntry entry, IDictionary<string, Func<byte[], int, byte[]>> decoders = null)
			{
				byte[] buffer;

				lock (_stream)
				{
					buffer = ReadBytes(_stream, entry.Offset, entry.StorageLength);
				}

				// this needs to be outside lock!
				return Decode(entry.CodecName, buffer, entry.OriginalLength, decoders ?? Decoders);
			}

			#endregion

			#region private implementation

			/// <summary>Decodes the specified data.</summary>
			/// <param name="codecName">Name of the codec.</param>
			/// <param name="data">The data.</param>
			/// <param name="outputLength">Length of the output.</param>
			/// <param name="decoders">The decoders dictionary.</param>
			/// <returns>Decoded data.</returns>
			private static byte[] Decode(
				string codecName, byte[] data, int outputLength,
				IDictionary<string, Func<byte[], int, byte[]>> decoders)
			{
				if (string.IsNullOrEmpty(codecName))
					return data;
				Func<byte[], int, byte[]> decoder;
				lock (decoders)
				{
					if (!decoders.TryGetValue(codecName, out decoder))
						throw Helpers.Error(new ArgumentException(
							string.Format("Codec '{0}' is not registered", codecName)));
				}
				return decoder(data, outputLength);
			}

			private static LibZEntry ReadEntry(BinaryReader reader)
			{
				return new LibZEntry {
					Id = new Guid(reader.ReadBytes(GuidLength)),
					AssemblyName = new AssemblyName(reader.ReadString()),
					Hash = new Guid(reader.ReadBytes(GuidLength)),
					Flags = (LibZEntry.EntryFlags)reader.ReadInt32(),
					Offset = reader.ReadInt64(),
					OriginalLength = reader.ReadInt32(),
					StorageLength = reader.ReadInt32(),
					CodecName = reader.ReadString(),
				};
			}

			#endregion

			#region utility

			/// <summary>No-compression decoder.</summary>
			/// <param name="input">The input.</param>
			/// <param name="outputLength">Length of the output.</param>
			/// <returns>Input.</returns>
			internal static byte[] NoneDecoder(byte[] input, int outputLength)
			{
				return input;
			}

			/// <summary>Deflate decoder implementation.</summary>
			/// <param name="input">The input.</param>
			/// <param name="outputLength">Length of the output.</param>
			/// <returns>Decodec bytes.</returns>
			internal static byte[] DeflateDecoder(byte[] input, int outputLength)
			{
				using (var mstream = new MemoryStream(input))
				using (var zstream = new DeflateStream(mstream, CompressionMode.Decompress))
				{
					return ReadBytes(zstream, -1, outputLength);
				}
			}

			/// <summary>Reads the buffer from stream.</summary>
			/// <param name="stream">The stream.</param>
			/// <param name="position">The position (does not change is value is negativge).</param>
			/// <param name="length">The length.</param>
			/// <returns>Buffer of bytes.</returns>
			/// <exception cref="System.IO.IOException">LibZ container is corrupted</exception>
			protected static byte[] ReadBytes(Stream stream, long position, int length)
			{
				var result = new byte[length];
				if (position >= 0)
					stream.Position = position;
				var read = stream.Read(result, 0, length);
				if (read < length)
					throw new IOException("LibZ container is corrupted");
				return result;
			}

			#endregion

			#region IDisposable Members

			/// <summary>
			///     Releases unmanaged resources and performs other cleanup operations before the
			///     object is reclaimed by garbage collection.
			/// </summary>
			~LibZReader()
			{
				Dispose(false);
			}

			/// <summary>
			///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
			/// </summary>
			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}

			/// <summary>
			///     Releases unmanaged and - optionally - managed resources
			/// </summary>
			/// <param name="disposing">
			///     <c>true</c> to release both managed and unmanaged resources;
			///     <c>false</c> to release only unmanaged resources.
			/// </param>
			private void Dispose(bool disposing)
			{
				if (_disposed)
					return;

				try
				{
					if (disposing)
						DisposeManaged();
					DisposeUnmanaged();
				}
				finally
				{
					_disposed = true;
				}
			}

			/// <summary>Disposes the managed resources.</summary>
			protected virtual void DisposeManaged()
			{
				TryDispose(ref _reader);
				TryDispose(ref _stream);
				_entries = null;
			}

			/// <summary>Disposes the unmanaged resources.</summary>
			protected virtual void DisposeUnmanaged()
			{
				// do nothing
			}

			/// <summary>Tries the dispose and object.</summary>
			/// <typeparam name="T">Type of variable.</typeparam>
			/// <param name="subject">The subject.</param>
			protected static void TryDispose<T>(ref T subject) where T: class
			{
				var disposable = subject as IDisposable;
				if (ReferenceEquals(disposable, null))
					return;
				disposable.Dispose();
				subject = null;
			}

			#endregion
		}

		#endregion

		#region class LibZCatalog

#if !NET35

		/// <summary>Catalog (in MEF sense) for given LibZReader.</summary>
		internal class LibZCatalog: ComposablePartCatalog
		{
			#region fields

			/// <summary>The reader.</summary>
			private readonly LibZReader _reader;

			/// <summary>Flag indicating that catalog has been initialized.</summary>
			private int _initialized;

			/// <summary>The partial type catalogs.</summary>
			private List<TypeCatalog> _catalogs;

			/// <summary>The parts.</summary>
			private List<ComposablePartDefinition> _parts;

			#endregion

			#region constructor

			/// <summary>
			///     Initializes a new instance of the <see cref="LibZCatalog" /> class.
			/// </summary>
			/// <param name="reader">The reader.</param>
			public LibZCatalog(LibZReader reader)
			{
				_reader = reader;
			}

			/// <summary>Initializes this instance. Deffers querying assemblies until it is actually needed.</summary>
			private void Initialize()
			{
				if (Interlocked.CompareExchange(ref _initialized, 1, 0) != 0)
					return;
				_catalogs = new List<TypeCatalog>();
				_parts = new List<ComposablePartDefinition>();

				if (_reader == null)
					return;

				var assemblyNames = _reader.Entries.Select(e => e.AssemblyName).ToList();

				foreach (var assemblyName in assemblyNames)
				{
					try
					{
						_catalogs.Add(new TypeCatalog(Assembly.Load(assemblyName).GetTypes()));
					}
					catch (Exception e)
					{
						Helpers.Error(string.Format("Could not load catalog for '{0}'", assemblyName));
						Helpers.Error(e);
					}
				}

				_parts.AddRange(_catalogs.SelectMany(c => c.Parts));
			}

			#endregion

			#region overrides

			/// <summary>Gets the part definitions that are contained in the catalog.</summary>
			/// <returns>
			///     The <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" />
			///     contained in the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" />.
			/// </returns>
			public override IQueryable<ComposablePartDefinition> Parts
			{
				get
				{
					Initialize();
					return _parts.AsQueryable();
				}
			}

			/// <summary>
			///     Gets a list of export definitions that match the constraint defined by the specified
			///     <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> object.
			/// </summary>
			/// <param name="definition">
			///     The conditions of the
			///     <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" />
			///     objects to be returned.
			/// </param>
			/// <returns>
			///     A collection of <see cref="T:System.Tuple`2" /> containing the
			///     <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> objects
			///     and their associated <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" />
			///     objects for objects that match the constraint specified by <paramref name="definition" />.
			/// </returns>
			public override IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
			{
				Initialize();
				return _catalogs.SelectMany(c => c.GetExports(definition));
			}

			#endregion
		}

#endif

		#endregion

		#region class GlobalDictionary

		/// <summary>Dictionary accessible from whole AppDomain.</summary>
		internal class GlobalDictionary
		{
			#region fields

			/// <summary>Actual data.</summary>
			private readonly Dictionary<int, object> _data;

			#endregion

			#region constructor

			/// <summary>
			///     Initializes a new instance of the <see cref="GlobalDictionary" /> class.
			/// </summary>
			/// <param name="dictionaryName">Name of the dictionary.</param>
			public GlobalDictionary(string dictionaryName)
			{
				lock (typeof(object))
				{
					_data = AppDomain.CurrentDomain.GetData(dictionaryName) as Dictionary<int, object>;
					if (_data != null)
						return;

					_data = new Dictionary<int, object>();
					AppDomain.CurrentDomain.SetData(dictionaryName, _data);
					IsOwner = true;
				}
			}

			#endregion

			#region public interface

			/// <summary>Gets a value indicating whether this instance owns the dictionary.</summary>
			/// <value>
			///     <c>true</c> if this instance owns the dictionary; otherwise, <c>false</c>.
			/// </value>
			public bool IsOwner { get; private set; }

			/// <summary>Gets the value in specified slot.</summary>
			/// <typeparam name="T">Type of slot.</typeparam>
			/// <param name="slot">The slot.</param>
			/// <param name="defaultValue">The default value.</param>
			/// <returns>
			///     Value stored in slot or <paramref name="defaultValue" />
			/// </returns>
			public T Get<T>(int slot, T defaultValue = default (T))
			{
				object result;
				if (!_data.TryGetValue(slot, out result))
					return defaultValue;
				return (T)result;
			}

			/// <summary>Sets the value in specified slot.</summary>
			/// <param name="slot">The slot.</param>
			/// <param name="value">The value.</param>
			public void Set(int slot, object value)
			{
				_data[slot] = value;
			}

			#endregion
		}

		#endregion

		#region class Hash

		/// <summary>MD5 calculator.</summary>
		internal class Hash
		{
			#region public interface

			/// <summary>Computes the MD5 for specified byte array.</summary>
			/// <param name="bytes">The bytes.</param>
			/// <returns>MD5.</returns>
			public static Guid Get(byte[] bytes)
			{
				return new Guid(MD5.Create().ComputeHash(bytes));
			}

			/// <summary>Computes MD5 for the specified text (case insensitive).</summary>
			/// <param name="text">The text.</param>
			/// <returns>MD5.</returns>
			public static Guid Get(string text)
			{
				return Get(Encoding.UTF8.GetBytes(text.ToLowerInvariant()));
			}

			#endregion
		}

		#endregion

		#region Helpers

		/// <summary>Simple helper functions.</summary>
		public static class Helpers
		{
			#region consts

			/// <summary>Trace key path.</summary>
			public const string REGISTRY_KEY_PATH = @"Software\Softpark\LibZ";

			/// <summary>Trace key name.</summary>
			public const string REGISTRY_KEY_NAME = @"Trace";

			/// <summary>This assembly</summary>
			private static readonly Assembly ThisAssembly = typeof(Helpers).Assembly;

			/// <summary>This assembly name</summary>
			private static readonly string ThisAssemblyName = ThisAssembly.GetName().Name;

			/// <summary>Flag indicating if Trace should be used.</summary>
			private static readonly bool UseTrace;

			#endregion

			/// <summary>Initializes the <see cref="Helpers"/> class.</summary>
			static Helpers()
			{
				var value =
					GetRegistryDWORD(Registry.CurrentUser, REGISTRY_KEY_PATH, REGISTRY_KEY_NAME) ??
						GetRegistryDWORD(Registry.LocalMachine, REGISTRY_KEY_PATH, REGISTRY_KEY_NAME) ??
							0;
				UseTrace = value != 0;
			}

			/// <summary>Gets bool value from registry.</summary>
			/// <param name="root">The root key.</param>
			/// <param name="path">The path to key.</param>
			/// <param name="name">The name of value.</param>
			/// <returns>Value of given... value.</returns>
			private static uint? GetRegistryDWORD(RegistryKey root, string path, string name)
			{
				var key = root.OpenSubKey(path, false);
				if (key == null)
					return null;

				var value = key.GetValue(name);
				if (value == null)
					return null;

				try
				{
					return Convert.ToUInt32(value);
				}
				catch
				{
					return null;
				}
			}

			/// <summary>Sends debug message.</summary>
			/// <param name="message">The message.</param>
			internal static void Debug(string message)
			{
				if (message == null || !UseTrace)
					return;
				Trace.TraceInformation("INFO (LibZ/{0}) {1}", ThisAssemblyName, message);
			}

			/// <summary>Sends warning message.</summary>
			/// <param name="message">The message.</param>
			internal static void Warn(string message)
			{
				if (message == null || !UseTrace)
					return;
				Trace.TraceWarning("WARN (LibZ/{0}) {1}", ThisAssemblyName, message);
			}

			/// <summary>Sends error message.</summary>
			/// <param name="message">The message.</param>
			/// <param name="ignored">
			///     <c>true</c> if exception is going to be ignored,
			///     so problem is reported as Warning instead of Error.
			/// </param>
			internal static void Error(string message, bool ignored = false)
			{
				if (message == null || !UseTrace)
					return;
				if (ignored)
				{
					Warn(message);
				}
				else
				{
					Trace.TraceError("ERROR (LibZ/{0}) {1}", ThisAssemblyName, message);
				}
			}

			/// <summary>Sends exception and error message.</summary>
			/// <param name="exception">The exception.</param>
			/// <param name="ignored">
			///     <c>true</c> if exception is going to be ignored,
			///     so problem is reported as Warning instead of Error.
			/// </param>
			internal static TException Error<TException>(TException exception, bool ignored = false)
				where TException: Exception
			{
				if (exception != null)
					Error(
						string.Format("{0}: {1}", exception.GetType().Name, exception.Message),
						ignored);
				return exception;
			}
		}

		#endregion
	}
}