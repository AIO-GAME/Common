using System.Linq;

public static partial class AHelper
{
    /// <summary>
    /// hash工具
    /// </summary>
    public static partial class Hash
    {
        /// <summary>
        /// 获取哈希值
        /// </summary>
        public static int GetHashCode<T>(in T a)
        {
            return a?.GetHashCode() ?? 0;
        }

        /// <summary>
        /// 获取哈希值
        /// </summary>
        public static int GetHashCode<T1, T2>(in T1 a, in T2 b)
        {
            unchecked
            {
                var hash = 17;

                hash = hash * 23 + (a?.GetHashCode() ?? 0);
                hash = hash * 23 + (b?.GetHashCode() ?? 0);

                return hash;
            }
        }

        /// <summary>
        /// 获取哈希值
        /// </summary>
        public static int GetHashCode<T1, T2, T3>(in T1 a, in T2 b, in T3 c)
        {
            unchecked
            {
                var hash = 17;

                hash = hash * 23 + (a?.GetHashCode() ?? 0);
                hash = hash * 23 + (b?.GetHashCode() ?? 0);
                hash = hash * 23 + (c?.GetHashCode() ?? 0);

                return hash;
            }
        }

        /// <summary>
        /// 获取哈希值
        /// </summary>
        public static int GetHashCode<T1, T2, T3, T4>(in T1 a, in T2 b, in T3 c, in T4 d)
        {
            unchecked
            {
                var hash = 17;

                hash = hash * 23 + (a?.GetHashCode() ?? 0);
                hash = hash * 23 + (b?.GetHashCode() ?? 0);
                hash = hash * 23 + (c?.GetHashCode() ?? 0);
                hash = hash * 23 + (d?.GetHashCode() ?? 0);

                return hash;
            }
        }

        /// <summary>
        /// 获取哈希值
        /// </summary>
        public static int GetHashCode<T1, T2, T3, T4, T5>(in T1 a, in T2 b, in T3 c, in T4 d, in T5 e)
        {
            unchecked
            {
                var hash = 17;

                hash = hash * 23 + (a?.GetHashCode() ?? 0);
                hash = hash * 23 + (b?.GetHashCode() ?? 0);
                hash = hash * 23 + (c?.GetHashCode() ?? 0);
                hash = hash * 23 + (d?.GetHashCode() ?? 0);
                hash = hash * 23 + (e?.GetHashCode() ?? 0);

                return hash;
            }
        }

        /// <summary>
        /// 获取哈希值
        /// </summary>
        public static int GetHashCodeAlloc(params object[] values)
        {
            unchecked
            {
                return values.Aggregate(17, (current, value) => current * 23 + (value?.GetHashCode() ?? 0));
            }
        }
    }
}