using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AIO
{
    // Implementation From Roslyn:
    // http://source.roslyn.io/#microsoft.codeanalysis/InternalUtilities/ReferenceEqualityComparer.cs
    /// <summary>
    /// 引用比较
    /// </summary>
    public class ReferenceEqualityComparer : IEqualityComparer<object>
    {
        private ReferenceEqualityComparer()
        {
        }

        bool IEqualityComparer<object>.Equals(object a, object b)
        {
            return a == b;
        }

        int IEqualityComparer<object>.GetHashCode(object a)
        {
            return GetHashCode(a);
        }

        /// <summary>
        /// 实例
        /// </summary>
        public static readonly ReferenceEqualityComparer Instance = new ReferenceEqualityComparer();

        /// <summary>
        /// 获取哈希值
        /// </summary>
        public static int GetHashCode(object a)
        {
            return RuntimeHelpers.GetHashCode(a);
        }
    }

    /// <summary>
    /// 引用比较
    /// </summary>
    public class ReferenceEqualityComparer<T> : IEqualityComparer<T>
    {
        private ReferenceEqualityComparer()
        {
        }

        bool IEqualityComparer<T>.Equals(T a, T b)
        {
            return ReferenceEquals(a, b);
        }

        int IEqualityComparer<T>.GetHashCode(T a)
        {
            return GetHashCode(a);
        }

        /// <summary>
        /// 获取哈希值
        /// </summary>
        public static readonly ReferenceEqualityComparer<T> Instance = new ReferenceEqualityComparer<T>();

        /// <summary>
        /// 获取哈希值
        /// </summary>
        public static int GetHashCode(T a)
        {
            return RuntimeHelpers.GetHashCode(a);
        }
    }
}