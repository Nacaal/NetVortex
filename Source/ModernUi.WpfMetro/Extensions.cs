using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetVortex.ModernUi.WpfMetro
{
    public static class Extensions
    {
        public static void Raise<T>(this EventHandler<T> handler, object sender, T args) where T : EventArgs
        {
            if (handler != null) 
                handler(sender, args);
        }
    }
}
