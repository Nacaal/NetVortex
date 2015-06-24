using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetVortex.Composition.AutoFac
{
    class Program
    {
        private static IContainer _container;

        static void Main(string[] args)
        {
            ContainerBuilder builder = new ContainerBuilder();

            _container = builder.Build();
        }
    }
}
