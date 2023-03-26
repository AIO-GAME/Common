/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-21                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;


public partial class Utils
{
    /// <summary>
    /// 随机数工具类
    /// </summary>
    public static partial class RandomX
    {
        private static Random random;

        static RandomX()
        {
            random = new Random(Guid.NewGuid().GetHashCode());
        }

        #region Refresh

        /// <summary>
        /// 刷新随机种子
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Refresh()
        {
            random = new Random(Guid.NewGuid().GetHashCode());
        }

        /// <summary>
        /// 刷新随机种子
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Refresh(in Random Random)
        {
            random = Random;
        }

        /// <summary>
        /// 刷新随机种子
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Refresh(in int seed)
        {
            random = new Random(seed);
        }

        #endregion

        #region Rand Array

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] RandArray<T>(int count, in ICollection<T> array)
        {
            var arr = new T[count];
            while (count >= 0)
            {
                foreach (var item in array)
                {
                    if (random.Next(0, 2) == 0)
                    {
                        arr[--count] = item;
                        if (count == 0) return arr;
                    }
                }
            }

            return arr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] RandArray<T>(in int count, in IList<T> array)
        {
            var arr = new T[count];
            for (int i = 0; i < arr.Length; i++) arr[i] = array[random.Next(0, array.Count)];
            return arr;
        }

        /// <param name="array"></param>
        /// <param name="lower">下限-包含</param>
        /// <param name="upper">上限-不包含</param>
        /// <param name="count"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] RandArray<T>(int count, IList<T> array, in int lower, int upper)
        {
            if (upper > array.Count) upper = array.Count;
            if (lower >= upper) throw new ArgumentException("lower value should less upper value");
            var arr = new T[count];
            for (var i = 0; i < arr.Length; i++) arr[i] = array[random.Next(lower, upper)];
            return arr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="array"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<T1, T2> RandArray<T1, T2>(in int count, in IDictionary<T1, T2> array)
        {
            if (array == null) return null;
            var arr = new Dictionary<T1, T2>(array);
            if (arr.Count == 0 || count >= arr.Count) return arr;
            while (count < arr.Count)
            {
                foreach (var item in array)
                {
                    if (random.Next(0, 2) == 0 && arr.ContainsKey(item.Key))
                    {
                        arr.Remove(item.Key);
                        if (count >= arr.Count)
                        {
                            return arr;
                        }
                    }
                }
            }

            return arr;
        }

        #endregion

        #region Rand Array Value

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T RandArrayValue<T>(in IList<T> array)
        {
            return array[RandInt32(0, array.Count)];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="SystemException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T RandArrayValue<T>(in ICollection<T> array)
        {
            var value = random.Next(0, array.Count);
            foreach (var item in array)
                if (--value == 0)
                    return item;
            throw new SystemException(); //此语句不会执行 因为不会等于0的情况还未返回
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        /// <exception cref="SystemException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyValuePair<T1, T2> RandArrayValue<T1, T2>(in IDictionary<T1, T2> array)
        {
            var value = random.Next(0, array.Count);
            foreach (var item in array)
                if (--value == 0)
                    return item;
            throw new SystemException(); //此语句不会执行 因为不会等于0的情况还未返回
        }

        /// <param name="array"></param>
        /// <param name="lower">下限-包含</param>
        /// <param name="upper">上限-不包含</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T RandArrayValue<T>(in IList<T> array, in int lower, in int upper)
        {
            return array[random.Next(lower, array.Count < upper ? array.Count : upper)];
        }

        /// <param name="array"></param>
        /// <param name="lower">下限-包含</param>
        /// <param name="upper">上限-不包含</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T RandArrayValue<T>(in ICollection<T> array, in int lower, int upper)
        {
            if (upper > array.Count) upper = array.Count;
            if (lower >= upper) throw new ArgumentException("lower value should less upper value");
            var value = random.Next(lower, upper);
            foreach (var item in array)
                if (--value == 0)
                    return item;
            throw new SystemException(); //此语句不会执行 因为不会等于0的情况还未返回
        }

        /// <param name="array"></param>
        /// <param name="lower">下限-包含</param>
        /// <param name="upper">上限-不包含</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyValuePair<T1, T2> RandArrayValue<T1, T2>(in IDictionary<T1, T2> array, in int lower,
            int upper)
        {
            if (upper > array.Count) upper = array.Count;
            if (lower >= upper) throw new ArgumentException("lower value should less upper value");
            var value = random.Next(lower, upper);
            foreach (var item in array)
                if (--value == 0)
                    return item;
            throw new SystemException(); //此语句不会执行 因为不会等于0的情况还未返回
        }

        /// <summary>
        /// 随机权重 
        /// </summary>
        /// <param name="weights">传入数组的总和 应为1</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RandArrayWeight(in IList<float> weights)
        {
            var r = random.Next(0, 100) / 100f;
            var t = 0.0f;
            for (var i = 0; i < weights.Count; ++i)
            {
                t += weights[i];
                if (r <= t) return i;
            }

            return weights.Count - 1;
        }

        /// <summary>
        /// 随机权重
        /// </summary>
        /// <param name="weights">传入数组的总和 应为1</param>
        /// <param name="values"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T RandArrayWeight<T>(in IList<float> weights, in IList<T> values)
        {
            var r = random.Next(0, 100) / 100f;
            var t = 0.0f;
            for (var i = 0; i < weights.Count; ++i)
            {
                t += weights[i];
                if (r <= t) return values[i];
            }

            return values.Last();
        }

        #endregion

        #region Rand Bool

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RandBool()
        {
            return random.Next(0, 2) == 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool[] RandBoolArray(in int count)
        {
            var arr = new bool[count];
            for (var i = 0; i < count; i++) arr[i] = random.Next(0, 2) == 0;
            return arr;
        }

        #endregion

        #region Rand Byte

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte RandByte()
        {
            return (byte)random.Next(0, 256);
        }

        /// <param name="lower">下限-包含</param>
        /// <param name="upper">上限-不包含</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte RandBytes(in byte lower, in int upper)
        {
            return (byte)random.Next(lower, byte.MaxValue < upper ? byte.MaxValue : upper);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] RandByteArray(in int count)
        {
            var arr = new byte[count];
            random.NextBytes(arr);
            return arr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] RandByteArray(in int count, in int min, int max)
        {
            if (min < byte.MinValue) throw new ArgumentException("min value should less byte.MinValue value");
            if (max > byte.MaxValue + 1) throw new ArgumentException("lower value should less upper value");

            var arr = new byte[count];
            for (var i = 0; i < arr.Length; i++)
            {
                arr[i] = (byte)random.Next(min, max);
            }

            return arr;
        }

        #endregion

        #region Rand SByte

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte RandSByte()
        {
            return (sbyte)random.Next(sbyte.MinValue, sbyte.MaxValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte[] RandSByteArray(in int count)
        {
            var bytes = new sbyte[count];
            for (var i = 0; i < count; i++)
                bytes[i] = (sbyte)random.Next(sbyte.MinValue, sbyte.MaxValue);
            return bytes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte[] RandSByteArray(in int count, in sbyte lower, in sbyte upper)
        {
            var bytes = new sbyte[count];
            for (var i = 0; i < count; i++)
                bytes[i] = (sbyte)random.Next(lower, upper);
            return bytes;
        }

        #endregion

        #region Rand UInt16

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort RandUInt16()
        {
            var bytes = new byte[2];
            random.NextBytes(bytes);
            return BitConverter.ToUInt16(bytes, 0);
        }

        /// <param name="lower">下限-包含</param>
        /// <param name="upper">上限-不包含</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort RandUInt16(in ushort lower, in int upper)
        {
            return (ushort)random.Next(lower, ushort.MaxValue < upper ? ushort.MaxValue : upper);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort[] RandUInt16Array(in int count)
        {
            var bytes = new byte[2 * count];
            random.NextBytes(bytes);
            var array = new ushort[count];
            for (int i = 0, index = 0; i < bytes.Length; i += 2, index++)
            {
                array[index] = BitConverter.ToUInt16(bytes, i);
            }

            return array;
        }

        #endregion

        #region Rand UInt32

        /// <param name="lower">下限-包含</param>
        /// <param name="upper">上限-不包含</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint RandUInt32(in uint lower, in uint upper)
        {
            var bytes = new byte[4];
            random.NextBytes(bytes);
            var value = BitConverter.ToUInt32(bytes, 0);
            if (value < lower) return lower;
            if (value >= upper) return upper - 1;
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint RandUInt32()
        {
            var bytes = new byte[4];
            random.NextBytes(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint[] RandUInt32Array(in int count)
        {
            var bytes = new byte[4 * count];
            random.NextBytes(bytes);
            var array = new uint[count];
            for (int i = 0, index = 0; i < bytes.Length; i += 4, index++)
            {
                array[index] = BitConverter.ToUInt32(bytes, i);
            }

            return array;
        }

        #endregion

        #region Rand UInt64

        /// <param name="lower">下限-包含</param>
        /// <param name="upper">上限-不包含</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong RandUInt64(in ulong lower, in ulong upper)
        {
            var bytes = new byte[8];
            random.NextBytes(bytes);
            var value = BitConverter.ToUInt64(bytes, 0);
            if (value < lower) return lower;
            if (value >= upper) return upper - 1;
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong RandUInt64()
        {
            var byte8 = new byte[8];
            random.NextBytes(byte8);
            return BitConverter.ToUInt64(byte8, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong[] RandUInt64Array(in int count)
        {
            var bytes = new byte[8 * count];
            random.NextBytes(bytes);
            var array = new ulong[count];
            for (int i = 0, index = 0; i < bytes.Length; i += 8, index++)
            {
                array[index] = BitConverter.ToUInt64(bytes, i);
            }

            return array;
        }

        #endregion

        #region Rand Int16

        /// <param name="lower">下限-包含</param>
        /// <param name="upper">上限-不包含</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short RandInt16(in short lower, in int upper)
        {
            return (short)random.Next(lower, short.MaxValue < upper ? short.MaxValue : upper);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short RandInt16()
        {
            var bytes = new byte[2];
            random.NextBytes(bytes);
            return BitConverter.ToInt16(bytes, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short[] RandInt16Array(in int count)
        {
            var bytes = new byte[2 * count];
            random.NextBytes(bytes);
            var array = new short[count];
            for (int i = 0, index = 0; i < bytes.Length; i += 2, index++)
            {
                array[index] = BitConverter.ToInt16(bytes, i);
            }

            return array;
        }

        #endregion

        #region Rand Int32

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RandInt32()
        {
            return random.Next();
        }

        /// <param name="lower">下限-包含</param>
        /// <param name="upper">上限-不包含</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RandInt32(in int lower, in int upper)
        {
            return random.Next(lower, upper);
        }

        /// <param name="count"></param>
        /// <param name="minValue">下限-包含</param>
        /// <param name="maxValue">上限-不包含</param>
        /// <param name="hasRepeat">是否包含重复 Ture:包含 False:不包含</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int[] RandInt32Array(int count, int minValue, int maxValue, bool hasRepeat = false)
        {
            var arr = new int[count];
            if (hasRepeat)
                for (var i = 0; i < arr.Length; i++)
                    arr[i] = random.Next(minValue, maxValue);
            else
            {
                if (maxValue - minValue < count)
                    throw new ArgumentException(
                        "The assignable interval must be greater than the number of targets");
                var hashtable = new Hashtable();
                while (hashtable.Count < count)
                {
                    var nValue = random.Next(minValue, maxValue);
                    if (!hashtable.ContainsKey(nValue)) // 是否包含特定值
                    {
                        arr[hashtable.Count] = nValue;
                        hashtable.Add(nValue, null);
                    }
                }
            }

            return arr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weights"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RandInt32ArrayWeight(IList<int> weights)
        {
            if (weights.Count <= 1) return weights.Count - 1;

            var sum = weights.Sum();
            var number_rand = random.Next(0, sum + 1);
            for (int i = 0, sum_temp = 0; i < weights.Count; i++)
            {
                sum_temp += weights[i];
                if (number_rand <= sum_temp) return i;
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weights"></param>
        /// <param name="weightRandomMinVal"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RandInt32ArrayWeight(IList<int> weights, int weightRandomMinVal)
        {
            if (weights.Count <= 1) return weights.Count - 1;

            var sum = weights.Sum();
            var number_rand = random.Next(0, System.Math.Max(sum, weightRandomMinVal));
            for (int i = 0, sum_temp = 0; i < weights.Count; i++)
            {
                sum_temp += weights[i];
                if (number_rand <= sum_temp) return i;
            }

            return -1;
        }

        #endregion

        #region Rand Int64

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long RandInt64()
        {
            var bytes = new byte[8];
            random.NextBytes(bytes);
            return BitConverter.ToInt64(bytes, 0);
        }

        /// <param name="lower">下限-包含</param>
        /// <param name="upper">上限-不包含</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long RandInt64(in long lower, in long upper)
        {
            var bytes = new byte[8];
            random.NextBytes(bytes);
            var value = BitConverter.ToInt64(bytes, 0);
            if (value < lower) return lower;
            if (value >= upper) return upper - 1;
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long[] RandInt64Array(in int count)
        {
            var bytes = new byte[8 * count];
            random.NextBytes(bytes);
            var array = new long[count];
            for (int i = 0, index = 0; i < bytes.Length; i += 8, index++)
            {
                array[index] = BitConverter.ToInt64(bytes, i);
            }

            return array;
        }

        #endregion

        #region Rand Float

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RandFloat()
        {
            var bytes = new byte[4];
            random.NextBytes(bytes);
            return BitConverter.ToSingle(bytes, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hasNan"></param>
        /// <param name="hasInfinity"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RandFloat(bool hasNan, bool hasInfinity)
        {
            while (true)
            {
                var bytes = new byte[4];
                random.NextBytes(bytes);
                var value = BitConverter.ToSingle(bytes, 0);
                if (!hasNan && float.IsNaN(value)) continue;
                if (!hasInfinity && float.IsInfinity(value)) continue;
                return value;
            }
        }

        #endregion

        #region Rand Double

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double RandDouble()
        {
            var bytes = new byte[8];
            random.NextBytes(bytes);
            return BitConverter.ToDouble(bytes, 0);
        }

        #endregion

        #region Break

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BreakArray<T>(IList<T> array)
        {
            if (array == null || array.Count < 2) return;

            for (var i = 0; i < array.Count; i++)
            {
                var index = random.Next(0, array.Count);
                (array[index], array[i]) = (array[i], array[index]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ArgumentException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BreakArray<T>(IList<T> array, in int lower, int upper)
        {
            if (array == null || array.Count < 2) return;
            if (upper > array.Count) upper = array.Count;
            if (lower >= upper) throw new ArgumentException("lower value should less upper value");

            for (var i = 0; i < array.Count; i++)
            {
                var index = random.Next(lower, upper);
                (array[index], array[i]) = (array[i], array[index]);
            }
        }

        #endregion

        #region Next

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long NextLong(long minValue, long maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentException("minValue is great than maxValue", nameof(minValue));
            return minValue + (long)(random.NextDouble() * (maxValue - minValue));
        }

        #endregion
    }
}
