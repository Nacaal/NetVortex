using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetVortex.Composition.AutoFac.Model
{
    class Apple : Fruit, ICitrus
    {
        public int SourLevel { get; set; }
    }
}
