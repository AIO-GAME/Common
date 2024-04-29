#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class ExtendSort
    {
        private static IList<ushort> SortCounting(IList<ushort> array)
        {
            if (array.Count < 2) return array; // 求最大最小值
            var mixin  = array.GetMaxMinValue();
            var bucket = new int[mixin.Item1 - mixin.Item2 + 1]; // 初始化新组并赋值数组长度比实际个数大一
            var bias   = -mixin.Item2;                           // 正填充

            for (var i = 0; i < bucket.Length; i++) bucket[i] = 0;
            foreach (var item in array) bucket[item + bias]++;

            short arrayIndex  = 0;
            short bucketIndex = 0; // 反填充
            unchecked
            {
                while (arrayIndex < array.Count)
                    if (bucket[bucketIndex] != 0)
                    {
                        array[arrayIndex++] = (ushort)(bucketIndex - bias);
                        bucket[bucketIndex]--;
                    }
                    else
                    {
                        bucketIndex++;
                    }
            }

            return array;
        }

        private static IList<short> SortCounting(IList<short> array)
        {
            if (array.Count < 2) return array; // 求最大最小值
            var mixin  = array.GetMaxMinValue();
            var bucket = new int[mixin.Item1 - mixin.Item2 + 1]; // 初始化新组并赋值数组长度比实际个数大一
            var bias   = -mixin.Item2;                           // 正填充

            for (var i = 0; i < bucket.Length; i++) bucket[i] = 0;
            foreach (var item in array) bucket[item + bias]++;

            short arrayIndex  = 0;
            short bucketIndex = 0; // 反填充
            unchecked
            {
                while (arrayIndex < array.Count)
                    if (bucket[bucketIndex] != 0)
                    {
                        array[arrayIndex++] = (short)(bucketIndex - bias);
                        bucket[bucketIndex]--;
                    }
                    else
                    {
                        bucketIndex++;
                    }
            }

            return array;
        }

        private static IList<sbyte> SortCounting(IList<sbyte> array)
        {
            if (array.Count < 2) return array; // 求最大最小值
            var mixin  = array.GetMaxMinValue();
            var bucket = new int[mixin.Item1 - mixin.Item2 + 1]; // 初始化新组并赋值数组长度比实际个数大一
            var bias   = -mixin.Item2;                           // 正填充

            for (var i = 0; i < bucket.Length; i++) bucket[i] = 0;
            foreach (var item in array) bucket[item + bias]++;

            sbyte arrayIndex  = 0;
            sbyte bucketIndex = 0; // 反填充
            unchecked
            {
                while (arrayIndex < array.Count)
                    if (bucket[bucketIndex] != 0)
                    {
                        array[arrayIndex++] = (sbyte)(bucketIndex - bias);
                        bucket[bucketIndex]--;
                    }
                    else
                    {
                        bucketIndex++;
                    }
            }

            return array;
        }

        private static IList<int> SortCounting(IList<int> array)
        {
            if (array.Count < 2) return array; // 求最大最小值
            var mixin                                         = array.GetMaxMinValue();
            var bucket                                        = new int[mixin.Item1 - mixin.Item2 + 1]; // 初始化新组并赋值数组长度比实际个数大一
            for (var i = 0; i < bucket.Length; i++) bucket[i] = 0;
            var bias                                          = 0 - mixin.Item2; // 正填充
            foreach (var item in array) bucket[item + bias]++;

            var arrayIndex  = 0;
            var bucketIndex = 0; // 反填充
            while (arrayIndex < array.Count)
                if (bucket[bucketIndex] == 0) bucketIndex++;
                else
                {
                    array[arrayIndex++] = bucketIndex - bias;
                    bucket[bucketIndex]--;
                }

            return array;
        }

        private static IList<long> SortCounting(IList<long> array)
        {
            if (array.Count < 2) return array; // 求最大最小值
            var mixin                                         = array.GetMaxMinValue();
            var bucket                                        = new long[mixin.Item1 - mixin.Item2 + 1]; // 初始化新组并赋值数组长度比实际个数大一
            for (var i = 0; i < bucket.Length; i++) bucket[i] = 0;
            var bias                                          = 0 - mixin.Item2; // 正填充
            foreach (var item in array) bucket[item + bias]++;

            var arrayIndex  = 0;
            var bucketIndex = 0; // 反填充
            while (arrayIndex < array.Count)
                if (bucket[bucketIndex] != 0)
                {
                    array[arrayIndex++] = bucketIndex - bias;
                    bucket[bucketIndex]--;
                }
                else
                {
                    bucketIndex++;
                }

            return array;
        }

        private static IList<byte> SortCounting(IList<byte> array)
        {
            if (array.Count < 2) return array; // 求最大最小值
            var mixin                                         = array.GetMaxMinValue();
            var bucket                                        = new int[mixin.Item1 - mixin.Item2 + 1]; // 初始化新组并赋值数组长度比实际个数大一
            var bias                                          = -mixin.Item2;                           // 正填充
            for (var i = 0; i < bucket.Length; i++) bucket[i] = 0;
            foreach (var item in array) bucket[item + bias]++;

            var  arrayIndex  = 0;
            byte bucketIndex = 0; // 反填充
            unchecked
            {
                while (arrayIndex < array.Count)
                    if (bucket[bucketIndex] != 0)
                    {
                        array[arrayIndex++] = (byte)(bucketIndex - bias);
                        bucket[bucketIndex]--;
                    }
                    else bucketIndex++;
            }

            return array;
        }

        private static IList<ulong> SortCounting(IList<ulong> array)
        {
            if (array.Count < 2) return array; // 求最大最小值
            var mixin                                         = array.GetMaxMinValue();
            var bucket                                        = new int[mixin.Item1 - mixin.Item2 + 1]; // 初始化新组并赋值数组长度比实际个数大一
            for (var i = 0; i < bucket.Length; i++) bucket[i] = 0;
            var bias                                          = 0 - mixin.Item2; // 正填充
            foreach (var item in array) bucket[item + bias]++;

            var   arrayIndex  = 0;
            ulong bucketIndex = 0; // 反填充
            while (arrayIndex < array.Count)
                if (bucket[bucketIndex] != 0)
                {
                    array[arrayIndex++] = bucketIndex - bias;
                    bucket[bucketIndex]--;
                }
                else
                {
                    bucketIndex++;
                }

            return array;
        }

        private static IList<uint> SortCounting(IList<uint> array)
        {
            var len = (uint)array.Count;
            if (len < 2) return array;
            var bucket                              = new LinkedList<uint>[len];
            for (var i = 0; i < len; i++) bucket[i] = new LinkedList<uint>();

            var max  = array.GetMaxMinValue();
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
    }
}