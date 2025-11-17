using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using AIO.Internal;

namespace AIO
{
    internal static class SingletonData
    {
        internal static Dictionary<Type, Singleton> Data = new Dictionary<Type, Singleton>();

        internal static ConcurrentQueue<SingletonArray> Array = new ConcurrentQueue<SingletonArray>();
    }
}