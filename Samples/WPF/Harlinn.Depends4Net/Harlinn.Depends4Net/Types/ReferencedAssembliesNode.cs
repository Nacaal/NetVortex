using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Harlinn.Depends4Net.Types
{
    public class ReferencedAssembliesNode : NodeBase, IEnumerable<AssemblyNode>
    {
        ObservableCollection<AssemblyNode> assemblies;

        public ReferencedAssembliesNode()
        {
            assemblies = new ObservableCollection<AssemblyNode>();    
        }
        public ReferencedAssembliesNode(string name)
            : base(name)
        {
            assemblies = new ObservableCollection<AssemblyNode>(); 
        }

        public IEnumerator<AssemblyNode> GetEnumerator()
        {
            return assemblies.GetEnumerator(); ;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)assemblies).GetEnumerator(); 
        }


        public void Add(AssemblyNode assemblyInfo)
        {
            assemblies.Add(assemblyInfo);
        }


        public ObservableCollection<AssemblyNode> Assemblies
        {
            get
            {
                return assemblies;
            }
        }

    }

}
