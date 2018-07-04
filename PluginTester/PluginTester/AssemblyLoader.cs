using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.DependencyModel.Resolution;

namespace PluginTester
{
    internal class AssemblyLoader : AssemblyLoadContext
    {
        private string _folderPath;
        private ICompilationAssemblyResolver _resolver;
        private AssemblyLoadContext _loadContext;

        internal AssemblyLoader(string folderPath) => _folderPath = Path.GetDirectoryName(folderPath);


        internal Assembly Load(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            AssemblyName assemblyName = new AssemblyName(fileInfo.Name.Replace(fileInfo.Extension, string.Empty));
            _resolver = new CompositeCompilationAssemblyResolver
            (new ICompilationAssemblyResolver[]
            {
                new AppBaseCompilationAssemblyResolver(fileInfo.DirectoryName),
                new ReferenceAssemblyPathResolver(),
                new PackageCompilationAssemblyResolver()
            });
            var assembly = Load(assemblyName);
            _loadContext=AssemblyLoadContext.GetLoadContext(assembly);
            _loadContext.Resolving += AssemblyLoader_Resolving;
            return assembly;

        }

        private Assembly AssemblyLoader_Resolving(AssemblyLoadContext context, AssemblyName name)
        {
            bool NamesMatch(RuntimeLibrary runtime)
            {
                return string.Equals(runtime.Name, name.Name, StringComparison.OrdinalIgnoreCase);
            }

            RuntimeLibrary library =
                DependencyContext.Default.RuntimeLibraries.FirstOrDefault(NamesMatch);
            if (library != null)
            {
                var wrapper = new CompilationLibrary(
                    library.Type,
                    library.Name,
                    library.Version,
                    library.Hash,
                    library.RuntimeAssemblyGroups.SelectMany(g => g.AssetPaths),
                    library.Dependencies,
                    library.Serviceable);

                var assemblies = new List<string>();
                _resolver.TryResolveAssemblyPaths(wrapper, assemblies);
                if (assemblies.Count > 0)
                {
                    return _loadContext.LoadFromAssemblyPath(assemblies[0]);
                }
            }

            return null;
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            var dependencyContext = DependencyContext.Default;
            var ressource = dependencyContext.CompileLibraries.FirstOrDefault(r => r.Name.Contains(assemblyName.Name));

            if (ressource != null)
            {
                return Assembly.Load(new AssemblyName(ressource.Name));
            }

            var fileInfo = this.LoadFileInfo(assemblyName.Name);
            if (File.Exists(fileInfo.FullName))
            {
                Assembly assembly = null;
                if (this.TryGetAssemblyFromAssemblyName(assemblyName, out assembly))
                {
                    return assembly;
                }
                return this.LoadFromAssemblyPath(fileInfo.FullName);
            }

            return Assembly.Load(assemblyName);
        }

        private FileInfo LoadFileInfo(string assemblyName)
        {
            string fullPath = Path.Combine(this._folderPath, $"{assemblyName}.dll");

            return new FileInfo(fullPath);
        }

        private bool TryGetAssemblyFromAssemblyName(AssemblyName assemblyName, out Assembly assembly)
        {
            try
            {
                assembly = Default.LoadFromAssemblyName(assemblyName);
                return true;
            }
            catch
            {
                assembly = null;
                return false;
            }
        }

        public void Test()
        {

        }
    }
}
