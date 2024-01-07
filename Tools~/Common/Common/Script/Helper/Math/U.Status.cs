/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System;
using System.Runtime.CompilerServices;

namespace AIO
{
    public partial class AHelper
    {
        /// <summary>
        /// 状态值比较
        /// </summary>
        public partial class Status
        {
            /// <summary>
            /// 2次幂运算 判断是否相等
            /// </summary>
            /// <param name="index">次幂值 T:1,2,3..</param>
            /// <param name="mask">对比值 T:4,8,16,32..</param>
            /// <returns>Ture:相等 Flase:不相等</returns>
            /// <!--
            /// 1 << 2 = 4 相等
            /// 1 << 3 = 8 不相等
            /// -->
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool Square(int index, int mask)
            {
                return (1 << index & mask) != 0;
            }

            /// <summary>
            /// 2次幂运算 判断是否相等
            /// </summary>
            /// <param name="index">次幂值 T:1,2,3..</param>
            /// <param name="mask">对比值 T:4,8,16,32..</param>
            /// <returns>Ture:相等 Flase:不相等</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool Square(long index, long mask)
            {
                return ((1 << (int)index) & (int)mask) != 0;
            }
        }

        /// <summary>
        /// 状态值比较类
        /// </summary>
        public partial class Status
        {
            /// <summary>
            /// 源状态和指定状态是否有交集
            /// </summary>
            /// <param name="source">源状态</param>
            /// <param name="status">操作状态</param>
            /// <returns>true有相交</returns>
            [Obsolete("已过时:不推荐使用 原因:性能耗时与值类型相比 差距为几百倍 推荐使用int值", true)]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool Mix(Enum source, Enum status)
            {
                return (source.GetHashCode() & status.GetHashCode()) != 0;
            }

            /// <summary>
            /// 源状态和指定状态是否有交集
            /// </summary>
            /// <param name="source">源状态</param>
            /// <param name="status">操作状态</param>
            /// <returns>true有相交</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool Mix(in byte source, in byte status)
            {
                return (source & status) != 0;
            }

            /// <summary>
            /// 源状态和指定状态是否有交集
            /// </summary>
            /// <param name="source">源状态</param>
            /// <param name="status">操作状态</param>
            /// <returns>true有相交</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool Mix(in short source, in short status)
            {
                return (source & status) != 0;
            }

            /// <summary>
            /// 源状态和指定状态是否有交集
            /// </summary>
            /// <param name="source">源状态</param>
            /// <param name="status">操作状态</param>
            /// <returns>true有相交</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool Mix(in int source, in int status)
            {
                return (source & status) != 0;
            }

            /// <summary>
            /// 源状态和指定状态是否有交集
            /// </summary>
            /// <param name="source">源状态</param>
            /// <param name="status">操作状态</param>
            /// <returns>true有相交</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool Mix(in long source, in long status)
            {
                return (source & status) != 0;
            }
        }

        /// <summary>
        /// 状态值比较类
        /// </summary>
        public partial class Status
        {
            /// <summary>
            /// 设置状态 要求枚举结构顺序为 幂次序
            /// </summary>
            /// <param name="source">源状态</param>
            /// <param name="status">操作状态</param>
            /// <param name="b">Ture 状态添加 false 状态移除</param>
            [Obsolete("已过时:不推荐使用 原因:性能耗时与值类型相比 差距为几百倍 推荐使用int值", true)]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static T Set<T>(T source, T status, in bool b) where T : Enum
            {
                return (T)Enum.Parse(typeof(T), Set(source.GetHashCode(), status.GetHashCode(), b).ToString());
            }

            /// <summary>
            /// 设置状态
            /// </summary>
            /// <param name="source">源状态</param>
            /// <param name="status">操作状态</param>
            /// <param name="b">Ture 状态添加 false 状态移除</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static short Set(in short source, in short status, in bool b)
            {
                if (b) return (short)(source | status);
                else return (short)(source & ((~status) & 0xFFFF));
            }

            /// <summary>
            /// 设置状态
            /// </summary>
            /// <param name="source">源状态</param>
            /// <param name="status">操作状态</param>
            /// <param name="b">Ture 状态添加 false 状态移除</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static int Set(in int source, in int status, in bool b)
            {
                if (b) return source | status;
                return source & (~status);
            }

            /// <summary>
            /// 设置状态
            /// </summary>
            /// <param name="source">源状态</param>
            /// <param name="status">操作状态</param>
            /// <param name="b">Ture 状态添加 false 状态移除</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static long Set(in long source, in long status, in bool b)
            {
                if (b) return source | status;
                return source & (~status);
            }
        }

        /// <summary>
        /// 状态值比较类
        /// </summary>
        public partial class Status
        {
            /// <summary>
            /// 是否有指定状态（包含指定状态，但不限于指定状态）
            /// </summary>
            [Obsolete("已过时:不推荐使用 原因:性能耗时与值类型相比 差距为几百倍 推荐使用int值", true)]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool Has(in Enum eSource, in Enum eStatus)
            {
                var source = eSource.GetHashCode();
                if (source < 0) return false;
                var status = eStatus.GetHashCode();
                return (source & status) == status;
            }

            /// <summary>
            /// 是否有指定状态（包含指定状态，但不限于指定状态）
            /// </summary>
            /// <param name="source">源状态</param>
            /// <param name="status">操作状态</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool Has(in short source, in short status)
            {
                if (source < 0) return false;
                return (source & status) == status;
            }

            /// <summary>
            /// 是否有指定状态（包含指定状态，但不限于指定状态）
            /// </summary>
            /// <param name="source">源状态</param>
            /// <param name="status">操作状态</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool Has(in int source, in int status)
            {
                if (source < 0) return false;
                return (source & status) == status;
            }

            /// <summary>
            /// 是否有指定状态（包含指定状态，但不限于指定状态）
            /// </summary>
            /// <param name="source">源状态</param>
            /// <param name="status">操作状态</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool Has(in long source, in long status)
            {
                if (source < 0) return false;
                return (source & status) == status;
            }
        }

        /// <summary>
        /// 状态值比较类
        /// </summary>
        public partial class Status
        {
            /// <summary>
            /// 是否是指定状态 仅仅是指定状态
            /// </summary>
            /// <param name="source">源状态</param>
            /// <param name="status">操作状态</param>
            [Obsolete("已过时:不推荐使用 原因:性能耗时与值类型相比 差距为几百倍 推荐使用int值", true)]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool Only<T>(in T source, in T status) where T : IComparable
            {
                return source.CompareTo(status) == 0;
            }

            /// <summary>
            /// 是否是指定状态 仅仅是指定状态
            /// </summary>
            /// <param name="source">源状态</param>
            /// <param name="status">操作状态</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool Only(in int source, in int status)
            {
                return source == status;
            }

            /// <summary>
            /// 是否是指定状态 仅仅是指定状态
            /// </summary>
            /// <param name="source">源状态</param>
            /// <param name="status">操作状态</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool Only(in long source, in uint status)
            {
                return source == status;
            }

            /// <summary>
            /// 是否是指定状态 仅仅是指定状态
            /// </summary>
            /// <param name="source">源状态</param>
            /// <param name="status">操作状态</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool Only(in long source, in long status)
            {
                return source == status;
            }

            /// <summary>
            /// 是否是指定状态 仅仅是指定状态
            /// </summary>
            /// <param name="source">源状态</param>
            /// <param name="status">操作状态</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool Only(in ulong source, ulong status)
            {
                return source == status;
            }

            /// <summary>
            /// 是否是指定状态 仅仅是指定状态
            /// </summary>
            /// <param name="source">源状态</param>
            /// <param name="status">操作状态</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool Only(in short source, in short status)
            {
                return source == status;
            }

            /// <summary>
            /// 是否是指定状态 仅仅是指定状态
            /// </summary>
            /// <param name="source">源状态</param>
            /// <param name="status">操作状态</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool Only(in ushort source, in ushort status)
            {
                return source == status;
            }

            /// <summary>
            /// 是否是指定状态 仅仅是指定状态
            /// </summary>
            /// <param name="source">源状态</param>
            /// <param name="status">操作状态</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool Only(in byte source, in byte status)
            {
                return source == status;
            }

            /// <summary>
            /// 是否是指定状态 仅仅是指定状态
            /// </summary>
            /// <param name="source">源状态</param>
            /// <param name="status">操作状态</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool Only(in float source, in float status)
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                return source == status;
            }
        }

        /// <summary>
        /// 状态比较 推荐使用Int值 并且 不推荐使用枚举 装箱拆箱性能消耗与int值相比 差距是两倍
        /// 实例 10000000次
        /// 耗时 枚举转换 4
        /// 耗时 Int转换  2
        /// 耗时 long转换  4
        /// 耗时 Convert转换  1504
        /// </summary>
        public partial class Status
        {
            /// <summary>
            /// 删除状态
            /// </summary>
            /// <param name="source">源状态</param>
            /// <param name="status">操作状态</param>
            /// <returns>新状态</returns>
            [Obsolete("已过时:不推荐使用 原因:性能耗时与值类型相比 差距为上千倍 推荐使用int值", true)]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static T Del<T>(in T source, in T status)
                where T : IComparable, IComparable<T>, IConvertible, IEquatable<T>
            {
                return (T)Convert.ChangeType((source.GetHashCode() & (~status.GetHashCode())), typeof(T));
            }

            /// <summary>
            /// 删除状态
            /// </summary>
            /// <param name="source">源状态</param>
            /// <param name="status">操作状态</param>
            /// <returns>新状态</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static byte Del(in byte source, in byte status)
            {
                return (byte)(source & (~status));
            }

            /// <summary>
            /// 删除状态
            /// </summary>
            /// <param name="source">源状态</param>
            /// <param name="status">操作状态</param>
            /// <returns>新状态</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static short Del(in short source, in short status)
            {
                return (short)(source & (~status));
            }

            /// <summary>
            /// 删除状态
            /// </summary>
            /// <param name="source">源状态</param>
            /// <param name="status">操作状态</param>
            /// <returns>新状态</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static int Del(in int source, in int status)
            {
                return source & (~status);
            }

            /// <summary>
            /// 删除状态 2次幂序
            /// </summary>
            /// <param name="source">源状态</param>
            /// <param name="status">操作状态</param>
            /// <returns>新状态</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static long Del(in long source, in long status)
            {
                return source & (~status);
            }
        }
    }
}