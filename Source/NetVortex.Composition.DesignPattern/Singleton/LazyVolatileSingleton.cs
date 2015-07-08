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
    /// This is a standard double-check algorithm so that you don't lock if the instance has already been created.  However, because 
    /// it's possible two threads can go through the first if at the same time the first time back in, you need to check again after 
    /// the lock is acquired to avoid creating two instances.
    /// 
    /// https://msdn.microsoft.com/en-us/library/ff650316.aspx
    /// 
    /// This approach ensures that only one instance is created and only when the instance is needed. Also, the variable is declared to 
    /// be volatile to ensure that assignment to the instance variable completes before the instance variable can be accessed. Lastly, 
    /// this approach uses a syncRoot (_mutex) instance to lock on, rather than locking on the type itself, to avoid deadlocks.
    /// 
    /// This double-check locking approach solves the thread concurrency problems while avoiding an exclusive lock in every call to the 
    /// Instance property method. It also allows you to delay instantiation until the object is first accessed. In practice, an application 
    /// rarely requires this type of implementation. In most cases, the static initialization approach is sufficient.
    /// 
    /// http://csharpindepth.com/articles/general/singleton.aspx
    /// 
    /// This implementation attempts to be thread-safe without the necessity of taking out a lock every time. Unfortunately, there are four downsides to the pattern:
    ///     * It doesn't work in Java. This may seem an odd thing to comment on, but it's worth knowing if you ever need the singleton 
    ///       pattern in Java, and C# programmers may well also be Java programmers. The Java memory model doesn't ensure that the constructor 
    ///       completes before the reference to the new object is assigned to instance. The Java memory model underwent a reworking for version 1.5, 
    ///       but double-check locking is still broken after this without a volatile variable (as in C#).
    ///     * Without any memory barriers, it's broken in the ECMA CLI specification too. It's possible that under the .NET 2.0 memory model 
    ///       (which is stronger than the ECMA spec) it's safe, but I'd rather not rely on those stronger semantics, especially if there's 
    ///       any doubt as to the safety. Making the instance variable volatile can make it work, as would explicit memory barrier calls, although 
    ///       in the latter case even experts can't agree exactly which barriers are required. I tend to try to avoid situations where experts don't 
    ///       agree what's right and what's wrong!
    ///     * It's easy to get wrong. The pattern needs to be pretty much exactly as above - any significant changes are likely to impact either performance or correctness.
    ///     * It still doesn't perform as well as the later implementations.
    /// </summary>
    public class LazyVolatileSingleton
    {
        // lock for thread-safety laziness
        private static readonly object _mutex = new object();

        // static field to hold single instance
        private static volatile LazyVolatileSingleton _instance = null;

        // property that does some locking and then creates on first call
        public static LazyVolatileSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_mutex)
                    {
                        if (_instance == null)
                        {
                            _instance = new LazyVolatileSingleton();
                        }
                    }
                }

                return _instance;
            }
        }

        private LazyVolatileSingleton() { }
    }
}
