using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Harlinn.Depends4Net.Utils.Assemblies
{
    [Serializable]
    public class AssemblyInfo
    {
        string location;
        AssemblyName assemblyName;
        AssemblyInfo firstLoadedBy;
        string manifestModuleName;
        bool found;
        bool loadedFromGlobalAssemblyCache;
        bool isFrameworkAssembly;

        Dictionary<string, AssemblyInfo> referencedAssemblies;

        public AssemblyInfo()
        {
            referencedAssemblies = new Dictionary<string, AssemblyInfo>();
        }

        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                if (location == value)
                    return;
                location = value;
            }
        }


        public AssemblyName AssemblyName
        {
            get
            {
                return assemblyName;
            }
            set
            {
                if (assemblyName == value)
                    return;
                assemblyName = value;
            }
        }

        public bool Found
        {
            get
            {
                return found;
            }
            set
            {
                if (found == value)
                    return;
                found = value;
            }
        }


        public string ManifestModuleName
        {
            get
            {
                return manifestModuleName;
            }
            set
            {
                if (manifestModuleName == value)
                    return;
                manifestModuleName = value;
            }
        }



        public AssemblyInfo FirstLoadedBy
        {
            get
            {
                return firstLoadedBy;
            }
            set
            {
                if (firstLoadedBy == value)
                    return;
                firstLoadedBy = value;
            }
        }

        public bool LoadedFromGlobalAssemblyCache
        {
            get
            {
                return loadedFromGlobalAssemblyCache;
            }
            set
            {
                if (loadedFromGlobalAssemblyCache == value)
                    return;
                loadedFromGlobalAssemblyCache = value;
            }
        }

        public bool IsFrameworkAssembly
        {
            get
            {
                return isFrameworkAssembly;
            }
            set
            {
                if (isFrameworkAssembly == value)
                    return;
                isFrameworkAssembly = value;
            }
        }
        


        public Dictionary<string, AssemblyInfo> ReferencedAssemblies
        {
            get
            {
                return referencedAssemblies;
            }
        }

        public override string ToString()
        {
 	        return AssemblyName.FullName;     
        }




    }
}
