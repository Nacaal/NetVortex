using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetVortex.Composition.DesignPattern.Singleton
{
    /// <summary>
    /// http://geekswithblogs.net/BlackRabbitCoder/archive/2010/05/19/c-system.lazylttgt-and-the-singleton-design-pattern.aspx
    /// 
    /// Note, you need your lambda to call the private constructor as Lazy's default constructor can only call public constructors 
    /// of the type passed in (which we can't have by definition of a Singleton).  But, because the lambda is defined inside our type, 
    /// it has access to the private members so it's perfect.
    /// </summary>
    public sealed class LazySingleton
    {
        // static holder for instance, need to use lambda to construct since constructor private
        private static readonly Lazy<LazySingleton> _instance = new Lazy<LazySingleton>(() => new LazySingleton());

        // accessor for instance
        public static LazySingleton Instance { get { return _instance.Value; } }

        // private to prevent direct instantiation.
        private LazySingleton() { }
    }
}
