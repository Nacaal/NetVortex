using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using System.Windows;
using System.Windows.Data;
using System.Security.Policy;

using Harlinn.Depends4Net.Utils.Assemblies;

namespace Harlinn.Depends4Net.Types
{
    public class Depends : Base
    {
        string assemblyName;
        ObservableCollection<AssemblyNode> rootAssemblies;
        ObservableCollection<AssemblyNode> assemblies;

        AssemblyNode selectedAssemblyNode;

        public Depends()
        {
            rootAssemblies = new ObservableCollection<AssemblyNode>();
            assemblies = new ObservableCollection<AssemblyNode>();
        }

        public string AssemblyName
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
                OnPropertyChanged("AssemblyName");
                LoadAssemblies();
                OnPropertyChanged("RootAssemblies");
                OnPropertyChanged("Assemblies");
            }
        }



        private void LoadAssemblies()
        {
            ReflectionLoader reflectionLoader = new ReflectionLoader();
            try
            {
                reflectionLoader.Load(AssemblyName);
                Dictionary<string, Utils.Assemblies.AssemblyInfo>.ValueCollection loadedAssemblies = reflectionLoader.Assemblies.Values;
                AssemblyInfo rootAssembly = reflectionLoader.RootAssembly;
                Dictionary<string, AssemblyNode> assemblyNodeDictionary = new Dictionary<string, AssemblyNode>();

                AssemblyNode rootAssemblyNode = AddAssemblyNode(null, assemblyNodeDictionary, rootAssembly);
                rootAssemblies.Clear();
                rootAssemblies.Add(rootAssemblyNode);

                // Fixup
                foreach (AssemblyInfo assemblyInfo in loadedAssemblies)
                {
                    if (assemblyInfo.FirstLoadedBy != null)
                    {
                        AssemblyNode assemblyNode = assemblyNodeDictionary[assemblyInfo.AssemblyName.FullName];
                        assemblyNode.FirstLoadedBy = assemblyNodeDictionary[assemblyInfo.FirstLoadedBy.AssemblyName.FullName];
                    }
                }


                Dictionary<string, AssemblyNode>.ValueCollection assemblyNodeCollection = assemblyNodeDictionary.Values;

                List<AssemblyNode> assemblyNodes = new List<AssemblyNode>();
                assemblyNodes.AddRange(assemblyNodeCollection);
                assemblyNodes.Sort(new NamedComparer());


                assemblies.Clear();

                foreach (AssemblyNode assemblyNode in assemblyNodes)
                {
                    assemblies.Add(assemblyNode);
                }

            }
            finally
            {
                reflectionLoader.UnloadDomain();
            }
        }


        private AssemblyNode AddAssemblyNode(AssemblyNode parent, Dictionary<string, AssemblyNode> assemblyNodes, AssemblyInfo assemblyInfo)
        {
            AssemblyNode result = null;
            if (assemblyNodes.ContainsKey(assemblyInfo.AssemblyName.FullName))
            {
                result = assemblyNodes[assemblyInfo.AssemblyName.FullName];
                if (parent != null)
                {
                    parent.ReferencedAssemblies.Add(result);
                }

            }
            else
            {
                result = new AssemblyNode();

                result.AssignFromAssemblyInfo(assemblyInfo);



                assemblyNodes.Add(assemblyInfo.AssemblyName.FullName, result);
                
                if (parent != null)
                {
                    parent.ReferencedAssemblies.Add(result);
                }


                Dictionary<string, AssemblyInfo>.ValueCollection referencedAssemblyCollection = assemblyInfo.ReferencedAssemblies.Values;

                List<AssemblyInfo> referencedAssemblies = new List<AssemblyInfo>();
                referencedAssemblies.AddRange(referencedAssemblyCollection);
                referencedAssemblies.Sort(new AssemblyInfoComparer());

                foreach (AssemblyInfo referencedAssemblyInfo in referencedAssemblies)
                {
                    AddAssemblyNode(result, assemblyNodes, referencedAssemblyInfo);
                }
            }


            return result;
        }


        public ObservableCollection<AssemblyNode> RootAssemblies
        {
            get
            {
                return rootAssemblies;
            }
        }


        public ObservableCollection<AssemblyNode> Assemblies
        {
            get
            {
                return assemblies;
            }
        }


        public AssemblyNode SelectedAssemblyNode
        {
            get
            {
                return selectedAssemblyNode;
            }
            set
            {
                if (selectedAssemblyNode == value)
                    return;
                selectedAssemblyNode = value;
                OnPropertyChanged("SelectedAssemblyNode");
            }
        }
        


    }





}
