using System;
using System.Collections.Generic;
using System.Reflection;
using Harlinn.Depends4Net.Utils.Paths;


namespace Harlinn.Depends4Net.Utils.Assemblies
{
    public class AssemblyResolver
    {
        SearchPath searchPath;

        public AssemblyResolver()
        {
            searchPath = new SearchPath();
            searchPath.Initialize();
        }

        public void AddPath(string pathElement)
        {
            searchPath.Add(pathElement);
        }


        public string Resolve(AssemblyName assemblyName)
        {
            string assemblyFileName = assemblyName.Name+".dll";
            string result = searchPath.Search(assemblyFileName);
            if (result == null)
            {
                assemblyFileName = assemblyName.Name + ".exe";
                result = searchPath.Search(assemblyFileName);
            }
            return result;
        }
    }
}
