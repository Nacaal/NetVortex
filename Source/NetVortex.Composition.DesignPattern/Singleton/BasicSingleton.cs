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
    /// This is the most basic singleton, notice the key features:
    ///     * Static readonly field that contains the one and only instance.
    ///     * Constructor is private so it can only be called by the class itself.
    ///     * Static property that returns the single instance.
    /// 
    /// There's just one (potential) problem.  C# gives you no guarantee of when the static field _instance will be created.  
    /// This is because the C# standard simply states that classes (which are marked in the IL as BeforeFieldInit) can have their 
    /// static fields initialized any time before the field is accessed.  This means that they may be initialized on first use, 
    /// they may be initialized at some other time before, you can't be sure when.
    /// </summary>
    public class BasicSingleton
    {
        // the single instance is defined in a static field
        private static readonly BasicSingleton _instance = new BasicSingleton();

        // constructor private so users can't instantiate on their own
        private BasicSingleton() { }

        // read-only property that returns the static field
        public static BasicSingleton Instance { get { return _instance; } }
    }
}
