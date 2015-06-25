using System;
using System.Collections.Generic;

namespace Harlinn.Depends4Net.Utils.Assemblies
{
    public class AssemblyInfoComparer : System.Collections.IComparer, IComparer<AssemblyInfo>
    {
        public int Compare(object x, object y)
        {
            return string.Compare(x.ToString(), y.ToString());
        }

        public int Compare(AssemblyInfo x, AssemblyInfo y)
        {
            return string.Compare(x.ToString(), y.ToString());
        }
    }
}
