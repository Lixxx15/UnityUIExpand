using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UGUIExpand
{
    public class SingletonTemplate<T> where T : class
    {
        private static T instance;
        private static object initLock = new object();
        public static T GetInstance()
        {
            if (instance == null)
            {
                CreateInstance();
            }
            return instance;
        }
        private static void CreateInstance()
        {
            lock (initLock)
            {
                if (instance == null)
                {
                    Type t = typeof(T);
                    ConstructorInfo[] ctors = t.GetConstructors();
                    if (ctors.Length > 0)
                    {
                        throw new InvalidOperationException(t.Name + " has other ctor");
                    }
                    instance = (T)Activator.CreateInstance(t, true);
                }
            }
        }
    }
}
