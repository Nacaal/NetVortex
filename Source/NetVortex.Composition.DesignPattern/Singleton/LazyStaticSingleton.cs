using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetVortex.Composition.DesignPattern.Singleton
{
    /// <summary>
    /// http://geekswithblogs.net/BlackRabbitCoder/archive/2010/05/19/c-system.lazylttgt-and-the-singleton-design-pattern.aspx
    /// C# gives you no guarantee of when the static field _instance will be created.  This is because the C# standard simply states 
    /// that classes (which are marked in the IL as BeforeFieldInit) can have their static fields initialized any time before the field 
    /// is accessed. This means that they may be initialized on first use, they may be initialized at some other time before, you can't be sure when.
    /// 
    /// Well, you could also take advantage of the C# standard's BeforeFieldInit and define your class with a static constructor.  
    /// It need not have a body, just the presence of the static constructor will remove the BeforeFieldInit attribute on the class 
    /// and guarantee that no fields are initialized until the first static field, property, or method is called.
    /// 
    /// Now, while this works perfectly, I hate it.  Why?  Because it's relying on a non-obvious trick of the IL to guarantee laziness.  
    /// Just looking at this code, you'd have no idea that it's doing what it's doing.  
    /// Worse yet, you may decide that the empty static constructor serves no purpose and delete it (which removes your lazy guarantee).  
    /// Worse-worse yet, they may alter the rules around BeforeFieldInit in the future which could change this.
    /// </summary>
    public class LazyStaticSingleton
    {
        // because of the static constructor, this won't get created until first use
        private static readonly LazyStaticSingleton _instance = new LazyStaticSingleton();

        // returns the singleton instance using lazy-instantiation
        public static LazyStaticSingleton Instance { get { return _instance; } }

        // private to prevent direct instantiation
        private LazyStaticSingleton() { }

        // removes BeforeFieldInit on class so static fields are not initialized before they are used
        static LazyStaticSingleton() { }
    }
}
