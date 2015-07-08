using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetVortex.Composition.DesignPattern.Singleton
{
    /// <summary>
    /// http://csharpindepth.com/articles/general/singleton.aspx
    /// 
    /// Here, instantiation is triggered by the first reference to the static member of the nested class, which only occurs in Instance. 
    /// This means the implementation is fully lazy, but has all the performance benefits of the previous ones. Note that although nested 
    /// classes have access to the enclosing class's private members, the reverse is not true, hence the need for instance to be internal here. 
    /// That doesn't raise any other problems, though, as the class itself is private. The code is a bit more complicated in order to make 
    /// the instantiation lazy, however.
    /// </summary>
    public sealed class LazyNestedSingleton
    {
        private static class Nested
        {
            // explicit static constructor to tell C# compiler not to mark type as beforefieldinit
            static Nested() { }
            internal static readonly LazyNestedSingleton _instance = new LazyNestedSingleton();
        }

        public static LazyNestedSingleton Instance { get { return Nested._instance; } }

        private LazyNestedSingleton() { }
    }
}
