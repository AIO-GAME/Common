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
        public static IList<double> SortBucket(this IList<double> array, in int coefficient = 10)
        {
            // 创建bucket时，在二维中增加一组标识位，其中bucket[x, 0]表示这一维所包含的数字的个数
            if (array.Count < 2) return array;
            var valueMaxMin = array.GetMaxMinValue();
            var bucketLen = (int)((valueMaxMin.Item1 - valueMaxMin.Item2) * coefficient) + 1;
            var bucketArray = new double[bucketLen, array.Count + 1]; // 创建二维数组
            foreach (var item in array)
            {
                var xBit = (int)((item - valueMaxMin.Item2) * coefficient);
                var yBit = (int)(++bucketArray[xBit, 0]);
                bucketArray[xBit, yBit] = item;
            }

            for (var j = 0; j < bucketLen; j++)
            {
                // 为桶里的每一行使用插入排序
                var insertion = new double[(int)bucketArray[j, 0]]; // 为桶里的行创建新的数组后使用插入排序
                for (var k = 0; k < insertion.Length; k++) insertion[k] = bucketArray[j, k + 1];
                SortInsert(insertion); // 插入排序
                for (var k = 0; k < insertion.Length; k++) bucketArray[j, k + 1] = insertion[k]; // 把排好序的结果回写到桶里
            }

            for (int count = 0, j = 0; j < bucketLen; j++)
            {
                // 将所有桶里的数据回写到原数组中
                for (var k = 1; k <= (int)bucketArray[j, 0]; k++) array[count++] = bucketArray[j, k];
            }

            return array;
        }

    }
}