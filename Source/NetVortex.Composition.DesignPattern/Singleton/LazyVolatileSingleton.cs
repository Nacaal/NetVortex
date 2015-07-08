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
