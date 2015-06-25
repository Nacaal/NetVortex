using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Harlinn.Depends4Net.Utils.Assemblies
{
    public class Loader : MarshalByRefObject
    {
        Dictionary<string, AssemblyInfo> assemblies;
        AssemblyInfo currentReferencingAssembly;
        AssemblyInfo rootAssembly;
        AssemblyResolver resolver;

        public Loader()
        {
            assemblies = new Dictionary<string, AssemblyInfo>();
            resolver = new AssemblyResolver();
        }


        public void AddPath(string pathElement)
        {
            resolver.AddPath(pathElement);
        }


        public Dictionary<string, AssemblyInfo> Assemblies
        {
            get
            {
                return assemblies;
            }
        }

        public AssemblyInfo RootAssembly
        {
            get
            {
                return rootAssembly;
            }
        }

        public void LoadAssembly(string filename)
        {
            if (File.Exists(filename))
            {
                AppDomain.CurrentDomain.AssemblyLoad += OnAssemblyLoaded;
                try
                {
                    InternalLoadAssemblyFrom(filename);
                    
                }
                finally
                {
                    AppDomain.CurrentDomain.AssemblyLoad -= OnAssemblyLoaded;
                }
            }
        }




        private bool InternalLoadAssemblyFrom(string filename)
        {
            bool result = false;
            if (File.Exists(filename))
            {
                Assembly.ReflectionOnlyLoadFrom(filename);
                result = true;
            }
            return result;
        }

        private bool InternalLoadAssembly(AssemblyName assemblyName)
        {
            bool result = false;
            try
            {
                Assembly.ReflectionOnlyLoad(assemblyName.FullName);
                result = true;
            }
            catch(Exception exc)
            {

            }
            return result;
        }



        void OnAssemblyLoaded(object sender, AssemblyLoadEventArgs args)
        {
            Assembly loadedAssembly = args.LoadedAssembly;
            AssemblyName assemblyName = loadedAssembly.GetName();

            AssemblyInfo assemblyInfo = new AssemblyInfo();
            assemblyInfo.AssemblyName = assemblyName;
            assemblyInfo.ManifestModuleName = loadedAssembly.ManifestModule.Name;
            assemblyInfo.LoadedFromGlobalAssemblyCache = loadedAssembly.GlobalAssemblyCache;
            assemblyInfo.Location = loadedAssembly.Location;
            assemblyInfo.Found = true;


            assemblies.Add(loadedAssembly.FullName, assemblyInfo);
            if (currentReferencingAssembly != null)
            {
                currentReferencingAssembly.ReferencedAssemblies.Add(loadedAssembly.FullName, assemblyInfo);
                assemblyInfo.FirstLoadedBy = currentReferencingAssembly;
            }
            else
            {
                rootAssembly = assemblyInfo;
            }

            AssemblyName[] referencedAssemblies = args.LoadedAssembly.GetReferencedAssemblies();

            if (referencedAssemblies.Length > 0)
            {
                AssemblyInfo previouslyReferencingAssembly = currentReferencingAssembly;
                currentReferencingAssembly = assemblyInfo;
                try
                {
                    foreach (AssemblyName referencedAssemblyName in referencedAssemblies)
                    {
                        if (assemblies.ContainsKey(referencedAssemblyName.FullName) == false)
                        {
                            string fileName = resolver.Resolve(referencedAssemblyName);

                            if (InternalLoadAssemblyFrom(fileName) == false)
                            {
                                if (InternalLoadAssembly(referencedAssemblyName) == false)
                                {
                                    AssemblyInfo referencedAssemblyInfo = new AssemblyInfo();
                                    referencedAssemblyInfo.AssemblyName = referencedAssemblyName;
                                    assemblies.Add(referencedAssemblyName.FullName, referencedAssemblyInfo);
                                    currentReferencingAssembly.ReferencedAssemblies.Add(referencedAssemblyName.FullName, referencedAssemblyInfo);
                                }
                            }
                        }
                        else
                        {
                            AssemblyInfo referencedAssemblyInfo = assemblies[referencedAssemblyName.FullName];
                            currentReferencingAssembly.ReferencedAssemblies.Add(referencedAssemblyName.FullName, referencedAssemblyInfo);
                        }

                    }
                }
                finally
                {
                    currentReferencingAssembly = previouslyReferencingAssembly;
                }
            }



        }

    } 
}
