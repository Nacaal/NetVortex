using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Harlinn.Depends4Net.Types
{
    public class NamedComparer : System.Collections.IComparer, IComparer<Named>
    {
        public int Compare(object x, object y)
        {
            return string.Compare(x.ToString(), y.ToString());
        }

        public int Compare(Named x, Named y)
        {
            return string.Compare(x.ToString(), y.ToString());
        }
    }
}
