#region

using System;
using System.Buffers;
using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class ExtendSort
    {
        private static IList<byte> SortRadix(in IList<byte> array)
        {
            if (array.Count < 2) return array;
            var Max = array.GetMaxValue();
            var maxDigit = 0;
            if (Max == 0) maxDigit = 1;
            else
                for (var temp = Max; temp != 0; temp /= 10)
                    maxDigit++;
            ArrayPool<byte>.Shared.Rent(array.Count);
            for (int i = 0, mod = 10, dev = 1, pos = 0; i < maxDigit; i++, dev *= 10, mod *= 10, pos = 0)
            {
                var counter = new byte[mod * 2][];
                foreach (var item in array)
                {
                    // 遍历数组 将数组依照 当前排序位数中的值 进行插入
                    var bucket = item % mod / dev + mod;
                    if (counter[bucket] is null)
                    {
                        counter[bucket] = new[] { item };
                    }
                    else
                    {
                        var a = new byte[counter[bucket].Length + 1];
                        Array.ConstrainedCopy(counter[bucket], 0, a, 0, a.Length);
                        a[counter[bucket].Length] = item;
                        counter[bucket]           = a;
                    }
                }

                foreach (var bucket in counter) // 插入
                {
                    if (bucket == null) continue;
                    foreach (var value in bucket) array[pos++] = value;
                    ArrayPool<byte>.Shared.Return(bucket, true);
                }

                ArrayPool<byte[]>.Shared.Return(counter, true);
            }


            return array;
        }


        private static IList<uint> SortRadix(IList<uint> array)
        {
            if (array.Count < 2) return array;
            var maxValue = array.GetMaxValue();
            var maxDigit = 0;
            if (maxValue == 0) maxDigit = 1;
            else
                for (var temp = maxValue; temp != 0; temp /= 10)
                    maxDigit++;
            for (int i = 0, mod = 10, dev = 1, pos = 0; i < maxDigit; i++, dev *= 10, mod *= 10, pos = 0)
            {
                var counter = new uint[mod * 2][];
                foreach (var item in array) // 遍历数组 将数组依照 当前排序位数中的值 进行插入
                {
                    var bucket = item % mod / dev + mod;
                    counter[bucket] = AutomaticArray(counter[bucket], item);
                }

                foreach (var bucket in counter) // 插入
                {
                    if (bucket == null) continue;
                    foreach (var value in bucket) array[pos++] = value;
                }
            }

            return array;
        }

        private static IList<long> SortRadix(IList<long> array)
        {
            if (array.Count < 2) return array;
            var Max = array.GetMaxValue();
            var maxDigit = 0;
            if (Max == 0) maxDigit = 1;
            else
                for (var temp = Max; temp != 0; temp /= 10)
                    maxDigit++;
            for (int i = 0, mod = 10, dev = 1, pos = 0; i < maxDigit; i++, dev *= 10, mod *= 10, pos = 0)
            {
                var counter = new long[mod * 2][];
                foreach (var item in array)
                {
                    // 遍历数组 将数组依照 当前排序位数中的值 进行插入
                    var bucket = item % mod / dev + mod;
                    counter[bucket] = AutomaticArray(counter[bucket], item);
                }

                foreach (var bucket in counter)
                {
                    // 插入
                    if (bucket == null) continue;
                    foreach (var value in bucket) array[pos++] = value;
                }
            }

            return array;
        }

        private static IList<ushort> SortRadix(in IList<ushort> array)
        {
            var len = (uint)array.Count;
            if (len < 2) return array;
            var bucket = new LinkedList<ushort>[len];
            for (var i = 0; i < len; i++) bucket[i] = new LinkedList<ushort>();

            var max = array.GetMaxMinValue();
            var diff = max.Item1 - max.Item2 + 1;
            foreach (var item in array)
            {
                var area = (item - max.Item2) * len / diff; //区块
                InsertIntoLinkList(bucket[area], item);
            }

            for (int i = 0, index = 0; i < len; i++)
            {
                if (bucket[i] == null) continue;
                foreach (var item in bucket[i]) array[index++] = item;
            }

            return array;
        }

        private static IList<ulong> SortRadix(IList<ulong> array)
        {
            if (array.Count < 2) return array;
            var Max = array.GetMaxValue();
            var maxDigit = 0;
            if (Max == 0) maxDigit = 1;
            else
                for (var temp = Max; temp != 0; temp /= 10)
                    maxDigit++;
            ulong mod = 10, dev = 1;
            for (int i = 0, pos = 0; i < maxDigit; i++, dev *= 10, mod *= 10, pos = 0)
            {
                var counter = new ulong[mod * 2][];
                foreach (var item in array)
                {
                    // 遍历数组 将数组依照 当前排序位数中的值 进行插入
                    var bucket = item % mod / dev + mod;
                    counter[bucket] = AutomaticArray(counter[bucket], item);
                }

                foreach (var bucket in counter)
                {
                    // 插入
                    if (bucket == null) continue;
                    foreach (var value in bucket) array[pos++] = value;
                }
            }

            return array;
        }

        /// <summary>
        ///     基数排序
        /// </summary>
        private static IList<int> SortRadix(IList<int> array)
        {
            if (array.Count < 2) return array;
            var maxValue = array.GetMaxValue();
            var maxDigit = 0;
            if (maxValue == 0) maxDigit = 1;
            else
                for (var temp = maxValue; temp != 0; temp /= 10)
                    maxDigit++;
            for (int i = 0, mod = 10, dev = 1, pos = 0; i < maxDigit; i++, dev *= 10, mod *= 10, pos = 0)
            {
                var counter = new int[mod * 2][];
                foreach (var item in array) // 遍历数组 将数组依照 当前排序位数中的值 进行插入
                {
                    var bucket = item % mod / dev + mod;
                    counter[bucket] = AutomaticArray(counter[bucket], item);
                }

                foreach (var bucket in counter) // 插入
                {
                    if (bucket == null) continue;
                    foreach (var value in bucket) array[pos++] = value;
                }
            }

            return array;
        }

        /// <summary>
        ///     基数排序
        /// </summary>
        private static IList<short> SortRadix(in IList<short> array)
        {
            if (array.Count < 2) return array;
            var Max = array.GetMaxValue();
            var maxDigit = 0;
            if (Max == 0) maxDigit = 1;
            else
                for (var temp = Max; temp != 0; temp /= 10)
                    maxDigit++;
            for (int i = 0, mod = 10, dev = 1, pos = 0; i < maxDigit; i++, dev *= 10, mod *= 10, pos = 0)
            {
                var counter = new short[mod * 2][];
                foreach (var item in array)
                {
                    // 遍历数组 将数组依照 当前排序位数中的值 进行插入
                    var bucket = item % mod / dev + mod;
                    counter[bucket] = AutomaticArray(counter[bucket], item);
                }

                foreach (var bucket in counter)
                {
                    // 插入
                    if (bucket == null) continue;
                    foreach (var value in bucket) array[pos++] = value;
                }
            }

            return array;
        }

        /// <summary>
        ///     基数排序
        /// </summary>
        private static IList<sbyte> SortRadix(in IList<sbyte> array)
        {
            if (array.Count < 2) return array;
            var Max = array.GetMaxValue();
            var maxDigit = 0;
            if (Max == 0) maxDigit = 1;
            else
                for (var temp = Max; temp != 0; temp /= 10)
                    maxDigit++;

            ArrayPool<sbyte>.Shared.Rent(array.Count);
            for (int i = 0, mod = 10, dev = 1, pos = 0; i < maxDigit; i++, dev *= 10, mod *= 10, pos = 0)
            {
                var counter = new sbyte[mod * 2][];
                foreach (var item in array)
                {
                    // 遍历数组 将数组依照 当前排序位数中的值 进行插入
                    var bucket = item % mod / dev + mod;
                    if (counter[bucket] is null)
                    {
                        counter[bucket] = new[] { item };
                    }
                    else
                    {
                        var a = new sbyte[counter[bucket].Length + 1];
                        Array.ConstrainedCopy(counter[bucket], 0, a, 0, a.Length);
                        a[counter[bucket].Length] = item;
                        counter[bucket]           = a;
                    }
                }

                foreach (var bucket in counter) // 插入
                {
                    if (bucket == null) continue;
                    foreach (var value in bucket) array[pos++] = value;
                    ArrayPool<sbyte>.Shared.Return(bucket, true);
                }

                ArrayPool<sbyte[]>.Shared.Return(counter, true);
            }

            return array;
        }
    }
}