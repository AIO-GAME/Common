using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AIO
{
    /// <summary>
    /// 随机项
    /// </summary>
    public struct RandomItem
    {
        private Random random;

        /// <summary>
        /// 随机种子
        /// </summary>
        public int Seed { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public RandomItem(int seed) { random = new Random(Seed = seed); }

        #region Refresh

        /// <summary>
        /// 刷新随机种子
        /// </summary>
        public void SetSeed() { random = new Random(Seed = Guid.NewGuid().GetHashCode()); }

        /// <summary>
        /// 刷新随机种子
        /// </summary>
        public void SetSeed(in int seed) { random = new Random(Seed = seed); }

        #endregion

        #region Rand Double

        /// <summary>
        /// 随机获取 <see cref="double"/>
        /// </summary>
        public double RandDouble()
        {
            var bytes = new byte[8];
            random.NextBytes(bytes);
            return BitConverter.ToDouble(bytes, 0);
        }

        /// <summary>
        /// 随机获取限定范围 <see cref="double"/>
        /// </summary>
        /// <param name="min"> 最小值-包含 </param>
        /// <param name="max"> 最大值-不包含 </param>
        /// <exception cref="ArgumentException"> 当 min 大于 max 时抛出 </exception>
        /// <returns><see cref="double"/></returns>
        public double RandDouble(double min, double max)
        {
            if (min > max) throw new ArgumentException("min is great than max", nameof(min));
            return min + random.NextDouble() * (max - min);
        }

        /// <summary>
        /// 随机获取 <see cref="double"/>
        /// </summary>
        /// <param name="count">数量</param>
        public double[] RandDoubleArray(int count)
        {
            var arr = new double[count];
            for (var i = 0; i < count; i++)
            {
                var bytes = new byte[8];
                random.NextBytes(bytes);
                arr[i] = BitConverter.ToDouble(bytes, 0);
            }

            return arr;
        }

        /// <summary>
        /// 随机获取 <see cref="double"/>[]
        /// </summary>
        /// <param name="count">数量</param>
        /// <param name="min"> 最小值-包含 </param>
        /// <param name="max"> 最大值-不包含 </param>
        /// <exception cref="ArgumentException"> 当 min 大于 max 时抛出 </exception>
        /// <returns><see cref="double"/>[]</returns>
        public double[] RandDoubleArray(int count, double min, double max)
        {
            if (min > max) throw new ArgumentException("min is great than max", nameof(min));
            var arr                                = new double[count];
            for (var i = 0; i < count; i++) arr[i] = min + random.NextDouble() * (max - min);
            return arr;
        }

        #endregion

        #region Next

        /// <summary>
        /// 随机获取限定范围 <see cref="long"/>
        /// </summary>
        /// <param name="min"> 最小值-包含 </param>
        /// <param name="max"> 最大值-不包含 </param>
        /// <returns><see cref="long"/></returns>
        /// <exception cref="ArgumentException"> 当 min 大于 max 时抛出 </exception>
        public long RandLong(long min, long max)
        {
            if (min > max) throw new ArgumentException("min is great than max", nameof(min));
            return min + (long)(random.NextDouble() * (max - min));
        }

        /// <summary>
        /// 随机获取 <see cref="long"/>
        /// </summary>
        public long RandLong()
        {
            var bytes = new byte[8];
            random.NextBytes(bytes);
            return BitConverter.ToInt64(bytes, 0);
        }

        /// <summary>
        /// 随机获取 <see cref="long"/>[]
        /// </summary>
        /// <param name="count">数量</param>
        public long[] RandLongArray(int count)
        {
            var arr = new long[count];
            for (var i = 0; i < count; i++)
            {
                var bytes = new byte[8];
                random.NextBytes(bytes);
                arr[i] = BitConverter.ToInt64(bytes, 0);
            }

            return arr;
        }

        /// <summary>
        /// 随机获取限定范围 <see cref="long"/>
        /// </summary>
        /// <param name="count">数量</param>
        /// <param name="min"> 最小值-包含 </param>
        /// <param name="max"> 最大值-不包含 </param>
        /// <returns><see cref="long"/></returns>
        /// <exception cref="ArgumentException"> 当 min 大于 max 时抛出 </exception>
        public long[] RandLongArray(int count, long min, long max)
        {
            if (min > max) throw new ArgumentException("min is great than max", nameof(min));
            var arr                                = new long[count];
            for (var i = 0; i < count; i++) arr[i] = min + (long)(random.NextDouble() * (max - min));
            return arr;
        }

        #endregion

        #region Rand Array

        /// <summary>
        /// 随机获取数组 - 包含重复元素
        /// </summary>
        /// <param name="count">数量</param>
        /// <param name="array">数组</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>数组</returns>
        public T[] RandArray<T>(int count, in ICollection<T> array)
        {
            var arr = new T[count];
            while (count >= 0)
                foreach (var item in array)
                {
                    if (random.Next(0, 2) == 0) arr[--count] = item;
                    if (count == 0) return arr;
                }

            return arr;
        }

        /// <summary>
        /// 随机获取数组 - 包含重复元素
        /// </summary>
        /// <param name="count">数量</param>
        /// <param name="array">数组</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>数组</returns>
        public List<T> RandArray<T>(in int count, in List<T> array)
        {
            var arr                                = Pool.List<T>();
            for (var i = 0; i < count; i++) arr[i] = array[random.Next(0, array.Count)];
            return arr;
        }

        /// <summary>
        /// 随机获取 <see cref="IList{T}"/> - 包含重复元素
        /// </summary>
        /// <param name="count">数量</param>
        /// <param name="array">数组</param>
        /// <param name="lower">限定下限-包含</param>
        /// <param name="upper">限定上限-不包含</param>
        public List<T> RandArray<T>(int count, List<T> array, int lower, int upper)
        {
            if (upper > array.Count) upper = array.Count;
            if (lower < 0) lower           = 0;
            if (lower >= upper) throw new ArgumentException("lower value should less upper value");
            var arr                                = Pool.List<T>();
            for (var i = 0; i < count; i++) arr[i] = array[random.Next(lower, upper)];
            return arr;
        }

        /// <summary>
        /// 随机获取 <see cref="IList{T}"/> - 包含重复元素
        /// </summary>
        /// <param name="count">数量</param>
        /// <param name="array">数组</param>
        /// <param name="lower">限定下限-包含</param>
        /// <param name="upper">限定上限-不包含</param>
        public HashSet<T> RandArray<T>(int count, HashSet<T> array, int lower, int upper)
        {
            if (array.Count == 0 || count >= array.Count) return array;
            if (upper > array.Count) upper = array.Count;
            if (lower < 0) lower           = 0;
            if (lower >= upper) throw new ArgumentException("lower value should less upper value");
            var arr  = Pool.HashSet<T>();
            var list = array.ToList();
            for (var i = 0; i < count; i++) arr.Add(list[random.Next(lower, upper)]);
            return arr;
        }

        /// <summary>
        /// 随机获取 <see cref="Dictionary{T1,T2}"/> - 不包含重复元素
        /// </summary>
        /// <param name="count">数量</param>
        /// <param name="array">数组</param>
        /// <typeparam name="T1">泛型1</typeparam>
        /// <typeparam name="T2">泛型2</typeparam>
        /// <returns><see cref="Dictionary{T1,T2}"/></returns>
        public Dictionary<T1, T2> RandArray<T1, T2>(int count, in IDictionary<T1, T2> array)
        {
            if (array == null) return Pool.Dictionary<T1, T2>();
            var arr = Pool.Dictionary(array);
            if (arr.Count == 0 || count >= arr.Count) return arr;
            while (count < arr.Count)
                foreach (var item in array)
                {
                    if (random.Next(0, 2) == 0 && arr.ContainsKey(item.Key))
                    {
                        arr.Remove(item.Key);
                    }

                    if (count >= arr.Count) return arr;
                }

            return arr;
        }

        #endregion

        #region Rand Array Value

        /// <summary>
        /// 随机获取数组中的一个值 <see cref="T"/>
        /// </summary>
        /// <param name="array">数组</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns><see cref="T"/>泛型</returns>
        /// <exception cref="SystemException"> 如果字典为空或没有可用的键值对，则抛出此异常</exception>
        public T RandValue<T>(in ICollection<T> array)
        {
            if (array?.Count == 0) throw new SystemException("The collection is empty or null");
            var value = random.Next(0, array.Count);
            foreach (var item in array.Where(item => --value == 0)) return item;
            return array.FirstOrDefault(); // 如果没有找到，返回第一个键值对
        }

        /// <summary>
        /// 随机获取字典中的一个键值对
        /// </summary>
        /// <param name="array"> 要获取值的字典</param>
        /// <typeparam name="T1"> 键的类型</typeparam>
        /// <typeparam name="T2"> 值的类型</typeparam>
        /// <returns> 返回字典中的一个键值对</returns>
        /// <exception cref="SystemException"> 如果字典为空或没有可用的键值对，则抛出此异常</exception>
        public KeyValuePair<T1, T2> RandValue<T1, T2>(in IDictionary<T1, T2> array)
        {
            if (array?.Count == 0) throw new SystemException("The dictionary is empty or null");
            var value = random.Next(0, array.Count);
            foreach (var item in array.Where(item => --value == 0)) return item;
            return array.FirstOrDefault(); // 如果没有找到，返回第一个键值对
        }

        /// <summary>
        /// 随机获取 数组中的一个值 <see cref="T"/>
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="lower">下限-包含</param>
        /// <param name="upper">上限-不包含</param>
        public T RandValue<T>(in IList<T> array, int lower, int upper)
        {
            if (array?.Count == 0) throw new SystemException("The collection is empty or null");
            if (upper > array.Count) upper = array.Count;
            if (lower < 0) lower           = 0;
            if (lower >= upper) throw new ArgumentException("lower value should less upper value");
            return array[random.Next(lower, array.Count < upper ? array.Count : upper)];
        }

        /// <summary>
        /// 随机获取 数组中的一个值 <see cref="T"/>
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="lower">下限-包含</param>
        /// <param name="upper">上限-不包含</param>
        public T RandValue<T>(in ICollection<T> array, int lower, int upper)
        {
            if (array?.Count == 0) throw new SystemException("The collection is empty or null");
            if (upper > array.Count) upper = array.Count;
            if (lower < 0) lower           = 0;
            if (lower >= upper) throw new ArgumentException("lower value should less upper value");
            var value = random.Next(lower, upper);
            foreach (var item in array.Where(item => --value == 0)) return item;
            return array.FirstOrDefault(); // 如果没有找到，返回第一个键值对
        }

        /// <summary>
        /// 随机获取 字典中的一个键值对
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="lower">下限-包含</param>
        /// <param name="upper">上限-不包含</param>
        public KeyValuePair<T1, T2> RandValue<T1, T2>(in IDictionary<T1, T2> array, int lower, int upper)
        {
            if (array?.Count == 0) throw new SystemException("The collection is empty or null");
            if (upper > array.Count) upper = array.Count;
            if (lower < 0) lower           = 0;
            if (lower >= upper) throw new ArgumentException("lower value should less upper value");
            var value = random.Next(lower, upper);
            foreach (var item in array.Where(item => --value == 0)) return item;
            return array.FirstOrDefault(); // 如果没有找到，返回第一个键值对
        }

        /// <summary>
        /// 随机获取 数组中的多个值 - 不包含重复元素
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="count">数量</param>
        public T[] RandValue<T>(in IList<T> array, int count)
        {
            if (array?.Count == 0) throw new SystemException("The collection is empty or null");
            if (count >= array.Count) return array.ToArray();
            var arr  = new T[count];
            var rand = Pool.List<T>(array);
            for (var i = 0; i < rand.Count / 2; i++)
            {
                var index = random.Next(0, rand.Count);
                (rand[index], rand[i]) = (rand[i], rand[index]);
            }

            foreach (var item in rand)
            {
                if (random.Next(0, 2) == 0) arr[--count] = item;
                if (count == 0) break;
            }

            rand.Free();
            return arr;
        }

        #endregion

        #region RandWeight

        /// <summary>
        /// 随机权重
        /// </summary>
        /// <param name="array">传入数组的总和 应为1</param>
        public int RandWeight(in IList<float> array)
        {
            if (array?.Count == 0) throw new SystemException("The collection is empty or null");
            var r = random.Next(0, 100) / 100f;
            var t = 0.0f;
            for (var i = 0; i < array.Count; ++i)
            {
                t += array[i];
                if (r <= t) return i;
            }

            return array.Count - 1;
        }

        /// <summary>
        /// 随机权重
        /// </summary>
        /// <param name="array">传入数组的总和 应为1</param>
        /// <param name="values"></param>
        public T RandWeight<T>(in IList<float> array, in IList<T> values)
        {
            if (array?.Count == 0) throw new SystemException("The collection is empty or null");
            var r = random.Next(0, 100) / 100f;
            var t = 0.0f;
            for (var i = 0; i < array.Count; ++i)
            {
                t += array[i];
                if (r <= t) return values[i];
            }

            return values.Last();
        }

        /// <summary>
        /// 随机权重
        /// </summary>
        /// <param name="array">传入数组的总和 应为1</param>
        public T RandWeight<T>(in ICollection<Tuple<float, T>> array)
        {
            if (array?.Count == 0) throw new SystemException("The collection is empty or null");
            var r = random.Next(0, 100) / 100f;
            var t = 0.0f;
            foreach (var tuple in array)
            {
                t += tuple.Item1;
                if (r <= t) return tuple.Item2;
            }

            return array.Last().Item2;
        }

        /// <summary>
        /// 随机权重选择
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="min">权重最小值</param>
        /// <returns></returns>
        public int RandWeight(IList<int> array, int min)
        {
            if (array.Count <= 1) return array.Count - 1;
            var sum         = array.Sum();
            var number_rand = random.Next(0, Math.Max(sum, min));
            for (int i = 0, sum_temp = 0; i < array.Count; i++)
            {
                sum_temp += array[i];
                if (number_rand <= sum_temp) return i;
            }

            return -1;
        }

        #endregion

        #region Rand Bool

        /// <summary>
        /// 随机获取 <see cref="bool"/>
        /// </summary>
        /// <returns><see cref="bool"/></returns>
        public bool RandBool() => random.Next(0, 2) == 0;

        /// <summary>
        /// 随机获取 <see cref="bool"/>[]
        /// </summary>
        /// <param name="count">数量</param>
        /// <returns><see cref="bool"/>[]</returns>
        public bool[] RandBoolArray(in int count)
        {
            var arr                                = new bool[count];
            for (var i = 0; i < count; i++) arr[i] = random.Next(0, 2) == 0;
            return arr;
        }

        #endregion

        #region Rand Byte

        /// <summary>
        /// 随机获取 <see cref="byte"/>
        /// </summary>
        /// <returns><see cref="byte"/></returns>
        public byte RandByte() { return (byte)random.Next(0, 256); }

        /// <summary>
        /// 随机获取 <see cref="byte"/>
        /// </summary>
        /// <param name="lower">下限-包含</param>
        /// <param name="upper">上限-不包含</param>
        public byte RandByte(in byte lower, in int upper) { return (byte)random.Next(lower, byte.MaxValue < upper ? byte.MaxValue : upper); }

        /// <summary>
        /// 随机获取 <see cref="byte"/>
        /// </summary>
        /// <param name="count"></param>
        public byte[] RandByteArray(in int count)
        {
            var arr = new byte[count];
            random.NextBytes(arr);
            return arr;
        }

        /// <summary>
        /// 随机获取 <see cref="byte"/>
        /// </summary>
        /// <param name="count"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public byte[] RandByteArray(in int count, in int min, int max)
        {
            if (min < byte.MinValue) throw new ArgumentException("min value should less byte.MinValue value");
            if (max > byte.MaxValue + 1) throw new ArgumentException("lower value should less upper value");

            var arr                                     = new byte[count];
            for (var i = 0; i < arr.Length; i++) arr[i] = (byte)random.Next(min, max);

            return arr;
        }

        #endregion

        #region Rand SByte

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public sbyte RandSByte() { return (sbyte)random.Next(sbyte.MinValue, sbyte.MaxValue); }

        /// <summary>
        ///
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public sbyte[] RandSByteArray(in int count)
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
        public sbyte[] RandSByteArray(in int count, in sbyte lower, in sbyte upper)
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
        public ushort RandUInt16()
        {
            var bytes = new byte[2];
            random.NextBytes(bytes);
            return BitConverter.ToUInt16(bytes, 0);
        }

        /// <param name="lower">下限-包含</param>
        /// <param name="upper">上限-不包含</param>
        public ushort RandUInt16(in ushort lower, in int upper) { return (ushort)random.Next(lower, ushort.MaxValue < upper ? ushort.MaxValue : upper); }

        /// <summary>
        ///
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public ushort[] RandUInt16Array(in int count)
        {
            var bytes = new byte[2 * count];
            random.NextBytes(bytes);
            var array                                                                  = new ushort[count];
            for (int i = 0, index = 0; i < bytes.Length; i += 2, index++) array[index] = BitConverter.ToUInt16(bytes, i);

            return array;
        }

        #endregion

        #region Rand UInt32

        /// <param name="lower">下限-包含</param>
        /// <param name="upper">上限-不包含</param>
        public uint RandUInt32(in uint lower, in uint upper)
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
        public uint RandUInt32()
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
        public uint[] RandUInt32Array(in int count)
        {
            var bytes = new byte[4 * count];
            random.NextBytes(bytes);
            var array                                                                  = new uint[count];
            for (int i = 0, index = 0; i < bytes.Length; i += 4, index++) array[index] = BitConverter.ToUInt32(bytes, i);

            return array;
        }

        #endregion

        #region Rand UInt64

        /// <param name="lower">下限-包含</param>
        /// <param name="upper">上限-不包含</param>
        public ulong RandUInt64(in ulong lower, in ulong upper)
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
        public ulong RandUInt64()
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
        public ulong[] RandUInt64Array(in int count)
        {
            var bytes = new byte[8 * count];
            random.NextBytes(bytes);
            var array                                                                  = new ulong[count];
            for (int i = 0, index = 0; i < bytes.Length; i += 8, index++) array[index] = BitConverter.ToUInt64(bytes, i);

            return array;
        }

        #endregion

        #region Rand Int16

        /// <param name="lower">下限-包含</param>
        /// <param name="upper">上限-不包含</param>
        public short RandInt16(in short lower, in int upper) { return (short)random.Next(lower, short.MaxValue < upper ? short.MaxValue : upper); }

        /// <param name="upper">上限-不包含</param>
        public short RandInt16(in int upper) { return (short)random.Next(0, short.MaxValue < upper ? short.MaxValue : upper); }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public short RandInt16()
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
        public short[] RandInt16Array(in int count)
        {
            var bytes = new byte[2 * count];
            random.NextBytes(bytes);
            var array                                                                  = new short[count];
            for (int i = 0, index = 0; i < bytes.Length; i += 2, index++) array[index] = BitConverter.ToInt16(bytes, i);

            return array;
        }

        #endregion

        #region Rand Int32

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public int RandInt32() { return random.Next(); }

        /// <param name="lower">下限-包含</param>
        /// <param name="upper">上限-不包含</param>
        public int RandInt32(in int lower, in int upper) { return random.Next(lower, upper); }

        /// <param name="count"></param>
        /// <param name="minValue">下限-包含</param>
        /// <param name="maxValue">上限-不包含</param>
        /// <param name="hasRepeat">是否包含重复 Ture:包含 False:不包含</param>
        public int[] RandInt32Array(int count, int minValue, int maxValue, bool hasRepeat = false)
        {
            var arr = new int[count];
            if (hasRepeat)
            {
                for (var i = 0; i < arr.Length; i++)
                    arr[i] = random.Next(minValue, maxValue);
            }
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
        public int RandInt32ArrayWeight(IList<int> weights)
        {
            if (weights.Count <= 1) return weights.Count - 1;

            var sum         = weights.Sum();
            var number_rand = random.Next(0, sum + 1);
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
        public long RandInt64()
        {
            var bytes = new byte[8];
            random.NextBytes(bytes);
            return BitConverter.ToInt64(bytes, 0);
        }

        /// <param name="lower">下限-包含</param>
        /// <param name="upper">上限-不包含</param>
        public long RandInt64(in long lower, in long upper)
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
        public long[] RandInt64Array(in int count)
        {
            var bytes = new byte[8 * count];
            random.NextBytes(bytes);
            var array                                                                  = new long[count];
            for (int i = 0, index = 0; i < bytes.Length; i += 8, index++) array[index] = BitConverter.ToInt64(bytes, i);

            return array;
        }

        #endregion

        #region Rand Float

        /// <summary>
        /// 随机获取 <see cref="float"/>
        /// </summary>
        /// <returns></returns>
        public float RandFloat()
        {
            var bytes = new byte[4];
            random.NextBytes(bytes);
            return BitConverter.ToSingle(bytes, 0);
        }

        /// <summary>
        /// 随机获取 <see cref="float"/>
        /// </summary>
        /// <param name="hasNan"> 允许NaN </param>
        /// <param name="hasInfinity"> 允许Infinity </param>
        /// <returns></returns>
        public float RandFloat(bool hasNan, bool hasInfinity)
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

        #region Break

        /// <summary>
        /// 将数组打乱顺序
        /// </summary>
        /// <param name="array">数组</param>
        /// <typeparam name="T">泛型</typeparam>
        public void BreakArray<T>(IList<T> array)
        {
            if (array == null || array.Count < 2) return;

            for (var i = 0; i < array.Count / 2; i++)
            {
                var index = random.Next(0, array.Count);
                (array[index], array[i]) = (array[i], array[index]);
            }
        }

        /// <summary>
        /// 将数组打乱顺序
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ArgumentException"></exception>
        public void BreakArray<T>(IList<T> array, in int lower, int upper)
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

        #region Rand Enum

        /// <summary>
        /// 随机获取枚举值
        /// </summary>
        public T RandEnum<T>()
        where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            if (values.Length == 0) return default;
            return (T)values.GetValue(random.Next(0, values.Length));
        }

        #endregion
    }
}