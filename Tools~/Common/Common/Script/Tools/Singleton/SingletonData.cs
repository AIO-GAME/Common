using System;
using System.Collections.Concurrent;
using AIO.Internal;

namespace AIO
{
    internal static class SingletonData
    {
        internal static ConcurrentDictionary<Type, Singleton> Data = new ConcurrentDictionary<Type, Singleton>();

        internal static ConcurrentQueue<SingletonArray> Array = new ConcurrentQueue<SingletonArray>();
    }
}