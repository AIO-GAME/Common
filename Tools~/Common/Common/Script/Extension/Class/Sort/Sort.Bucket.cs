#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class ExtendSort
    {
        private static IList<float> SortBucket(IList<float> array, in int coefficient = 10)
        {
            // 创建bucket时，在二维中增加一组标识位，其中bucket[x, 0]表示这一维所包含的数字的个数
            if (array.Count < 2) return array;
            var valueMaxMin = array.GetMaxMinValue();
            var bucketLen   = (int)((valueMaxMin.Item1 - valueMaxMin.Item2) * coefficient) + 1;
            var bucketArray = new float[bucketLen, array.Count + 1]; // 创建二维数组
            foreach (var item in array)
            {
                var xBit = (int)((item - valueMaxMin.Item2) * coefficient);
                var yBit = (int)++bucketArray[xBit, 0];
                bucketArray[xBit, yBit] = item;
            }

            for (var j = 0; j < bucketLen; j++)
            {
                // 为桶里的每一行使用插入排序
                var insertion                                           = new float[(int)bucketArray[j, 0]]; // 为桶里的行创建新的数组后使用插入排序
                for (var k = 0; k < insertion.Length; k++) insertion[k] = bucketArray[j, k + 1];
                SortInsert(insertion);                                                           // 插入排序
                for (var k = 0; k < insertion.Length; k++) bucketArray[j, k + 1] = insertion[k]; // 把排好序的结果回写到桶里
            }

            for (int count = 0, j = 0; j < bucketLen; j++)
                // 将所有桶里的数据回写到原数组中
            for (var k = 1; k <= (int)bucketArray[j, 0]; k++)
                array[count++] = bucketArray[j, k];

            return array;
        }

        /// <summary>
        ///     桶排序
        /// </summary>
        private static IList<sbyte> SortBucket(IList<sbyte> array)
        {
            if (array.Count < 2) return array;
            var bucket                                      = new LinkedList<sbyte>[array.Count];
            for (var i = 0; i < array.Count; i++) bucket[i] = new LinkedList<sbyte>();

            var max  = array.GetMaxMinValue();
            var diff = max.Item1 - max.Item2 + 1;
            foreach (var item in array)
            {
                var area = (item - max.Item2) * array.Count / diff; //区块
                InsertIntoLinkList(bucket[area], item);
            }

            for (int i = 0, index = 0; i < array.Count; i++)
            {
                if (bucket[i] == null) continue;
                foreach (var item in bucket[i]) array[index++] = item;
            }

            return array;
        }

        /// <summary>
        ///     桶排序
        /// </summary>
        private static IList<int> SortBucket(IList<int> array)
        {
            if (array.Count < 2) return array;
            var bucket                                      = new LinkedList<int>[array.Count];
            for (var i = 0; i < array.Count; i++) bucket[i] = new LinkedList<int>();

            var max  = array.GetMaxMinValue();
            var diff = max.Item1 - max.Item2 + 1;
            foreach (var item in array)
            {
                var area = (item - max.Item2) * array.Count / diff; //区块
                InsertIntoLinkList(bucket[area], item);
            }

            for (int i = 0, index = 0; i < array.Count; i++)
            {
                if (bucket[i] == null) continue;
                foreach (var item in bucket[i]) array[index++] = item;
            }

            return array;
        }

        /// <summary>
        ///     桶排序
        /// </summary>
        private static IList<long> SortBucket(IList<long> array)
        {
            if (array.Count < 2) return array;
            var bucket                                      = new LinkedList<long>[array.Count];
            for (var i = 0; i < array.Count; i++) bucket[i] = new LinkedList<long>();

            var max  = array.GetMaxMinValue();
            var diff = max.Item1 - max.Item2 + 1;
            foreach (var item in array)
            {
                var area = (item - max.Item2) * array.Count / diff; //区块
                InsertIntoLinkList(bucket[area], item);
            }

            for (int i = 0, index = 0; i < array.Count; i++)
            {
                if (bucket[i] == null) continue;
                foreach (var item in bucket[i]) array[index++] = item;
            }

            return array;
        }

        /// <summary>
        ///     桶排序
        /// </summary>
        private static IList<byte> SortBucket(IList<byte> array)
        {
            var len = (uint)array.Count;
            if (len < 2) return array;
            var bucket                              = new LinkedList<byte>[len];
            for (var i = 0; i < len; i++) bucket[i] = new LinkedList<byte>();

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

        private static IList<ushort> SortBucket(IList<ushort> array)
        {
            if (array.Count < 2) return array;
            var bucket                                      = new LinkedList<ushort>[array.Count];
            for (var i = 0; i < array.Count; i++) bucket[i] = new LinkedList<ushort>();

            var max  = array.GetMaxMinValue();
            var diff = max.Item1 - max.Item2 + 1;
            foreach (var item in array)
            {
                var area = (item - max.Item2) * array.Count / diff; //区块
                InsertIntoLinkList(bucket[area], item);
            }

            for (int i = 0, index = 0; i < array.Count; i++)
            {
                if (bucket[i] == null) continue;
                foreach (var item in bucket[i]) array[index++] = item;
            }

            return array;
        }

        private static IList<uint> SortBucket(IList<uint> array)
        {
            if (array.Count < 2) return array;
            var bucket                                      = new LinkedList<uint>[array.Count];
            for (var i = 0; i < array.Count; i++) bucket[i] = new LinkedList<uint>();

            var max  = array.GetMaxMinValue();
            var diff = max.Item1 - max.Item2 + 1;
            foreach (var item in array)
            {
                var area = (item - max.Item2) * array.Count / diff; //区块
                InsertIntoLinkList(bucket[area], item);
            }

            for (int i = 0, index = 0; i < array.Count; i++)
            {
                if (bucket[i] == null) continue;
                foreach (var item in bucket[i]) array[index++] = item;
            }

            return array;
        }

        private static IList<ulong> SortBucket(IList<ulong> array)
        {
            var len = (uint)array.Count;
            if (len < 2) return array;
            var bucket                              = new LinkedList<ulong>[len];
            for (var i = 0; i < len; i++) bucket[i] = new LinkedList<ulong>();

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

        private static IList<double> SortBucket(IList<double> array, in int coefficient = 10)
        {
            // 创建bucket时，在二维中增加一组标识位，其中bucket[x, 0]表示这一维所包含的数字的个数
            if (array.Count < 2) return array;
            var valueMaxMin = array.GetMaxMinValue();
            var bucketLen   = (int)((valueMaxMin.Item1 - valueMaxMin.Item2) * coefficient) + 1;
            var bucketArray = new double[bucketLen, array.Count + 1]; // 创建二维数组
            foreach (var item in array)
            {
                var xBit = (int)((item - valueMaxMin.Item2) * coefficient);
                var yBit = (int)++bucketArray[xBit, 0];
                bucketArray[xBit, yBit] = item;
            }

            for (var j = 0; j < bucketLen; j++)
            {
                // 为桶里的每一行使用插入排序
                var insertion                                           = new double[(int)bucketArray[j, 0]]; // 为桶里的行创建新的数组后使用插入排序
                for (var k = 0; k < insertion.Length; k++) insertion[k] = bucketArray[j, k + 1];
                SortInsert(insertion);                                                           // 插入排序
                for (var k = 0; k < insertion.Length; k++) bucketArray[j, k + 1] = insertion[k]; // 把排好序的结果回写到桶里
            }

            for (int count = 0, j = 0; j < bucketLen; j++)
                // 将所有桶里的数据回写到原数组中
            for (var k = 1; k <= (int)bucketArray[j, 0]; k++)
                array[count++] = bucketArray[j, k];

            return array;
        }

        private static IList<decimal> SortBucket(IList<decimal> array, in int coefficient = 10)
        {
            // 创建bucket时，在二维中增加一组标识位，其中bucket[x, 0]表示这一维所包含的数字的个数
            if (array.Count < 2) return array;
            var valueMaxMin = array.GetMaxMinValue();
            var bucketLen   = (int)((valueMaxMin.Item1 - valueMaxMin.Item2) * coefficient) + 1;
            var bucketArray = new decimal[bucketLen, array.Count + 1]; // 创建二维数组
            foreach (var item in array)
            {
                var xBit = (int)((item - valueMaxMin.Item2) * coefficient);
                var yBit = (int)++bucketArray[xBit, 0];
                bucketArray[xBit, yBit] = item;
            }

            for (var j = 0; j < bucketLen; j++)
            {
                // 为桶里的每一行使用插入排序
                var insertion                                           = new decimal[(int)bucketArray[j, 0]]; // 为桶里的行创建新的数组后使用插入排序
                for (var k = 0; k < insertion.Length; k++) insertion[k] = bucketArray[j, k + 1];
                SortInsert(insertion);                                                           // 插入排序
                for (var k = 0; k < insertion.Length; k++) bucketArray[j, k + 1] = insertion[k]; // 把排好序的结果回写到桶里
            }

            for (int count = 0, j = 0; j < bucketLen; j++)
                // 将所有桶里的数据回写到原数组中
            for (var k = 1; k <= (int)bucketArray[j, 0]; k++)
                array[count++] = bucketArray[j, k];

            return array;
        }

        /// <summary>
        ///     桶排序
        /// </summary>
        private static IList<short> SortBucket(in IList<short> array)
        {
            if (array.Count < 2) return array;
            var bucket                                      = new LinkedList<short>[array.Count];
            for (var i = 0; i < array.Count; i++) bucket[i] = new LinkedList<short>();

            var max  = array.GetMaxMinValue();
            var diff = max.Item1 - max.Item2 + 1;
            foreach (var item in array)
            {
                var area = (item - max.Item2) * array.Count / diff; //区块
                InsertIntoLinkList(bucket[area], item);
            }

            for (int i = 0, index = 0; i < array.Count; i++)
            {
                if (bucket[i] == null) continue;
                foreach (var item in bucket[i]) array[index++] = item;
            }

            return array;
        }
    }
}