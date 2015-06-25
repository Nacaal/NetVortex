using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using Harlinn.Depends4Net.Utils.Assemblies;
using System.Reflection;

namespace Harlinn.Depends4Net.Types
{
    public class AssemblyNode : NodeBase
    {
        bool found;
        string location;
        string displayName;
        AssemblyNode firstLoadedBy;
        string manifestModuleName;
        AssemblyName assemblyName;

        ObservableCollection<NodeBase> children;
        AssemblyTypesNode types;
        ReferencedAssembliesNode referencedAssemblies;

        public AssemblyNode()
        {
            Initialize();    

        }

        private void Initialize()
        {
            children = new ObservableCollection<NodeBase>();
            types = new AssemblyTypesNode("Types");
            referencedAssemblies = new ReferencedAssembliesNode("Referenced Assemblies");
            children.Add(types);
            children.Add(referencedAssemblies);
        }

        public void AssignFromAssemblyInfo(AssemblyInfo assemblyInfo)
        {
            Name = assemblyInfo.AssemblyName.Name;
            location = assemblyInfo.Location;
            displayName = assemblyInfo.AssemblyName.FullName;
            manifestModuleName = assemblyInfo.ManifestModuleName;
            found = assemblyInfo.Found;
            assemblyName = assemblyInfo.AssemblyName;
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
                OnPropertyChanged("Found");
            }
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
                OnPropertyChanged("Location");
            }
        }
        public string DisplayName
        {
            get
            {
                return displayName;
            }
            set
            {
                if (displayName == value)
                    return;
                displayName = value;
                OnPropertyChanged("DisplayName");
            }
        }
        public AssemblyNode FirstLoadedBy
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
                OnPropertyChanged("FirstLoadedBy");
            }
        }
        


        public AssemblyName AssemblyName
        {
            get
            {
                return assemblyName;
            }
        }


        public AssemblyTypesNode Types
        {
            get
            {
                return types;
            }
        }
        public ReferencedAssembliesNode ReferencedAssemblies
        {
            get
            {
                return referencedAssemblies;
            }
        }

        public ObservableCollection<NodeBase> Children
        {
            get
            {
                return children;
            }
        }

    }
}
