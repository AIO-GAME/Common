/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2024-01-03
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System.Collections.Generic;

namespace AIO
{
    public static partial class ExtendSort
    {
        /// <summary>
        /// 桶排序
        /// </summary>
        public static IList<short> Bucket(in IList<short> array)
        {
            if (array.Count < 2) return array;
            var bucket = new LinkedList<short>[array.Count];
            for (var i = 0; i < array.Count; i++) bucket[i] = new LinkedList<short>();

            var max = array.GetMaxMinValue();
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
        /// 基数排序
        /// </summary>
        public static IList<short> Radix(in IList<short> array)
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
    }
}