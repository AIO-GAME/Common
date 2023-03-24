using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AIO
{
    public static class Empty<T>
    {
        public static readonly T[] array = Array.Empty<T>();
        public static readonly Collection<T> collection = new Collection<T>();
        public static readonly List<T> list = new List<T>(0);
        public static readonly HashSet<T> hashSet = new HashSet<T>();
    }
}