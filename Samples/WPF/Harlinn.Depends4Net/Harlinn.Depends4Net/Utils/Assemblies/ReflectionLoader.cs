using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Security.Policy;

namespace Harlinn.Depends4Net.Utils.Assemblies
{
    class ReflectionLoader
    {

        AppDomain appDomain;

        Dictionary<string, AssemblyInfo> assemblies;
        AssemblyInfo rootAssembly;


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

        public void Load(String filename )
        {
            UnloadDomain();

            if (File.Exists(filename))
            {
                FileInfo fileInfo = new FileInfo(filename);

                AppDomainSetup domainSetup = new AppDomainSetup();

                domainSetup.ApplicationName = fileInfo.Name;
                domainSetup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;


                appDomain = AppDomain.CreateDomain("ReflectionLoader", null, domainSetup);

                Loader loader = (Loader)appDomain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, 
                    "Harlinn.Depends4Net.Utils.Assemblies.Loader");
                loader.AddPath(fileInfo.DirectoryName);

                loader.LoadAssembly(filename);
                assemblies = loader.Assemblies;
                rootAssembly = loader.RootAssembly;

            }
        }


        public void UnloadDomain()
        {
            if (appDomain != null)
            {
                AppDomain domainToUnload = appDomain;
                appDomain = null;
                AppDomain.Unload(domainToUnload);
            }
        }

            

    }
}
