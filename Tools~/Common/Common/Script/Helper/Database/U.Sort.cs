/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using AIO;

public partial class UtilsGen
{
    /// <summary>
    /// 排序工具
    /// 默认排序方向 从小到大
    /// </summary>
    public static partial class Sort
    {
        /// <summary>
        /// 自动扩容，并保存数据
        /// </summary>
        private static T[] AutomaticArray<T>(in Array array, in T value)
        {
            if (array == null) return new T[] { value };
            var a = new T[array.Length + 1];
            Array.ConstrainedCopy(array, 0, a, 0, array.Length);
            a[array.Length] = value;
            return a;
        }

        /* 非比较排序 */

        #region 计数排序

        /// <summary>
        /// 计数排序
        /// </summary>
        public static IList<int> Counting(in IList<int> array)
        {
            if (array.Count < 2) return array; // 求最大最小值
            var mixin = array.GetMaxMinValue();
            var bucket = new int[mixin.Item1 - mixin.Item2 + 1]; // 初始化新组并赋值数组长度比实际个数大一
            for (var i = 0; i < bucket.Length; i++) bucket[i] = 0;
            var bias = 0 - mixin.Item2; // 正填充
            foreach (var item in array) bucket[item + bias]++;

            var arrayIndex = 0;
            var bucketIndex = 0; // 反填充
            while (arrayIndex < array.Count)
            {
                if (bucket[bucketIndex] != 0)
                {
                    array[arrayIndex++] = bucketIndex - bias;
                    bucket[bucketIndex]--;
                }
                else bucketIndex++;
            }

            return array;
        }

        /// <summary>
        /// 计数排序
        /// </summary>
        public static IList<long> Counting(in IList<long> array)
        {
            if (array.Count < 2) return array; // 求最大最小值
            var mixin = array.GetMaxMinValue();
            var bucket = new long[mixin.Item1 - mixin.Item2 + 1]; // 初始化新组并赋值数组长度比实际个数大一
            for (var i = 0; i < bucket.Length; i++) bucket[i] = 0;
            var bias = 0 - mixin.Item2; // 正填充
            foreach (var item in array) bucket[item + bias]++;

            var arrayIndex = 0;
            var bucketIndex = 0; // 反填充
            while (arrayIndex < array.Count)
            {
                if (bucket[bucketIndex] != 0)
                {
                    array[arrayIndex++] = bucketIndex - bias;
                    bucket[bucketIndex]--;
                }
                else bucketIndex++;
            }

            return array;
        }

        #endregion

        #region 基数排序

        /// <summary>
        /// 基数排序
        /// </summary>
        public static IList<long> Radix(in IList<long> array)
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

        /// <summary>
        /// 基数排序
        /// </summary>
        public static IList<int> Radix(in IList<int> array)
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

        #endregion

        #region 桶排序

        /// <summary>
        /// 桶排序
        /// </summary>
        public static IList<long> Bucket(in IList<long> array)
        {
            if (array.Count < 2) return array;
            var bucket = new LinkedList<long>[array.Count];
            for (var i = 0; i < array.Count; i++) bucket[i] = new LinkedList<long>();

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
        /// 桶排序
        /// </summary>
        public static IList<int> Bucket(in IList<int> array)
        {
            if (array.Count < 2) return array;
            var bucket = new LinkedList<int>[array.Count];
            for (var i = 0; i < array.Count; i++) bucket[i] = new LinkedList<int>();

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
        /// 桶排序
        /// </summary>
        public static IList<float> Bucket(in IList<float> array, in int coefficient = 10)
        {
            // 创建bucket时，在二维中增加一组标识位，其中bucket[x, 0]表示这一维所包含的数字的个数
            if (array.Count < 2) return array;
            var valueMaxMin = array.GetMaxMinValue();
            var bucketLen = (int)((valueMaxMin.Item1 - valueMaxMin.Item2) * coefficient) + 1;
            var bucketArray = new float[bucketLen, array.Count + 1]; // 创建二维数组
            foreach (var item in array)
            {
                var xBit = (int)((item - valueMaxMin.Item2) * coefficient);
                var yBit = (int)(++bucketArray[xBit, 0]);
                bucketArray[xBit, yBit] = item;
            }

            for (var j = 0; j < bucketLen; j++)
            {
                // 为桶里的每一行使用插入排序
                var insertion = new float[(int)bucketArray[j, 0]]; // 为桶里的行创建新的数组后使用插入排序
                for (var k = 0; k < insertion.Length; k++) insertion[k] = bucketArray[j, k + 1];
                Insert(insertion); // 插入排序
                for (var k = 0; k < insertion.Length; k++) bucketArray[j, k + 1] = insertion[k]; // 把排好序的结果回写到桶里
            }

            for (int count = 0, j = 0; j < bucketLen; j++)
            {
                // 将所有桶里的数据回写到原数组中
                for (var k = 1; k <= (int)bucketArray[j, 0]; k++) array[count++] = bucketArray[j, k];
            }

            return array;
        }

        /// <summary>
        /// 桶排序
        /// </summary>
        public static IList<double> Bucket(in IList<double> array, in int coefficient = 10)
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
                Insert(insertion); // 插入排序
                for (var k = 0; k < insertion.Length; k++) bucketArray[j, k + 1] = insertion[k]; // 把排好序的结果回写到桶里
            }

            for (int count = 0, j = 0; j < bucketLen; j++)
            {
                // 将所有桶里的数据回写到原数组中
                for (var k = 1; k <= (int)bucketArray[j, 0]; k++) array[count++] = bucketArray[j, k];
            }

            return array;
        }

        /// <summary>
        /// 桶排序
        /// </summary>
        public static IList<decimal> Bucket(in IList<decimal> array, in int coefficient = 10)
        {
            // 创建bucket时，在二维中增加一组标识位，其中bucket[x, 0]表示这一维所包含的数字的个数
            if (array.Count < 2) return array;
            var valueMaxMin = array.GetMaxMinValue();
            var bucketLen = (int)((valueMaxMin.Item1 - valueMaxMin.Item2) * coefficient) + 1;
            var bucketArray = new decimal[bucketLen, array.Count + 1]; // 创建二维数组
            foreach (var item in array)
            {
                var xBit = (int)((item - valueMaxMin.Item2) * coefficient);
                var yBit = (int)(++bucketArray[xBit, 0]);
                bucketArray[xBit, yBit] = item;
            }

            for (var j = 0; j < bucketLen; j++)
            {
                // 为桶里的每一行使用插入排序
                var insertion = new decimal[(int)bucketArray[j, 0]]; // 为桶里的行创建新的数组后使用插入排序
                for (var k = 0; k < insertion.Length; k++) insertion[k] = bucketArray[j, k + 1];
                Insert(insertion); // 插入排序
                for (var k = 0; k < insertion.Length; k++) bucketArray[j, k + 1] = insertion[k]; // 把排好序的结果回写到桶里
            }

            for (int count = 0, j = 0; j < bucketLen; j++)
            {
                // 将所有桶里的数据回写到原数组中
                for (var k = 1; k <= (int)bucketArray[j, 0]; k++) array[count++] = bucketArray[j, k];
            }

            return array;
        }

        /// <summary>  
        /// 按升序插入 linklist   
        /// </summary>  
        /// <param name="linkedList"> 要排序的链表 </param>  
        /// <param name="num"> 要插入排序的数字 </param>  
        private static void InsertIntoLinkList<T>(LinkedList<T> linkedList, T num) where T : IComparable
        {
            // 链表为空时，插入到第一位  
            if (linkedList.Count <= 0) linkedList.AddFirst(num);
            else
            {
                foreach (var node in
                         from i in linkedList
                         where i.CompareTo(num) > 0
                         select linkedList.Find(i))
                {
                    linkedList.AddBefore(node, num);
                    return;
                }

                linkedList.AddLast(num);
            }
        }

        #endregion

        /* 比较排序 */

        #region 归并排序

        /// <summary>
        /// 归并排序
        /// 数据量:1000以下适用
        /// </summary>
        public static IList<T> Merge<T>(in IList<T> array, in Func<T, T, int> Comparer)
        {
            if (array.Count < 2) return array;
            return MergeSort(array, 0, array.Count - 1, Comparer);
        }


        private static IList<T> MergeSort<T>(in IList<T> array, in int left, in int right,
            in Func<T, T, int> Comparer)
        {
            if (left < right)
            {
                int median = (left + right) / 2; //计算出中间值
                MergeSort(array, left, median, Comparer);
                MergeSort(array, median + 1, right, Comparer);
                return Merge(array, left, median, right, Comparer);
            }

            return array;
        }


        private static IList<T> Merge<T>(in IList<T> array, in int left, in int median, in int right,
            in Func<T, T, int> Comparer)
        {
            var LAL = median - left + 1; //左数组长度
            var RAL = right - median; //右数组长度
            var LA = new T[LAL];
            var RA = new T[RAL];

            // 给左右两边数组 初始化内容
            for (int i = 0; i < LAL; i++) LA[i] = array[left + i];
            for (int i = 0; i < RAL; i++) RA[i] = array[median + 1 + i];

            for (int i = 0, j = 0, k = 0; i < right - left + 1; i++)
            {
                // 遍历 从左边0开始 到右边截止长度-左边起始开始位置+1
                if (j < LAL)
                {
                    // 如果 J小于左边数组长度
                    if (k < RAL)
                    {
                        // K小于右边数组长度
                        if (Comparer(LA[j], RA[k]) <= 0)
                            array[left + i] = LA[j++];
                        else array[left + i] = RA[k++];
                    }
                    else
                    {
                        // K大于等于右边数组长度
                        for (int m = j; m < LAL; m++)
                            array[left + i + m - j] = LA[m];
                        return array;
                    }
                }
                else if (k < RAL)
                {
                    // 如果 K小于右边数组长度
                    for (int n = k; n < RAL; n++)
                        array[left + i + n - k] = RA[n];
                    return array;
                }
            }

            return array;
        }

        /// <summary>
        /// 归并排序
        /// 数据量:1000以下适用
        /// </summary>
        public static IList<T> Merge<T>(in IList<T> array) where T : IComparable
        {
            if (array.Count < 2) return array;
            return MergeSort(array, 0, array.Count - 1);
        }


        private static IList<T> MergeSort<T>(in IList<T> array, in int left, in int right) where T : IComparable
        {
            if (left < right)
            {
                int median = (left + right) / 2; //计算出中间值
                MergeSort(array, left, median);
                MergeSort(array, median + 1, right);
                return Merge(array, left, median, right);
            }

            return array;
        }


        private static IList<T> Merge<T>(in IList<T> array, in int left, in int median, in int right)
            where T : IComparable
        {
            var LAL = median - left + 1; //左数组长度
            var RAL = right - median; //右数组长度
            var LA = new T[LAL];
            var RA = new T[RAL];

            // 给左右两边数组 初始化内容
            for (int i = 0; i < LAL; i++) LA[i] = array[left + i];
            for (int i = 0; i < RAL; i++) RA[i] = array[median + 1 + i];

            for (int i = 0, j = 0, k = 0; i < right - left + 1; i++)
            {
                // 遍历 从左边0开始 到右边截止长度-左边起始开始位置+1
                if (j < LAL)
                {
                    // 如果 J小于左边数组长度
                    if (k < RAL)
                    {
                        // K小于右边数组长度
                        if (LA[j].CompareTo(RA[k]) <= 0)
                            array[left + i] = LA[j++];
                        else array[left + i] = RA[k++];
                    }
                    else
                    {
                        // K大于等于右边数组长度
                        for (int m = j; m < LAL; m++)
                            array[left + i + m - j] = LA[m];
                        return array;
                    }
                }
                else if (k < RAL)
                {
                    // 如果 K小于右边数组长度
                    for (int n = k; n < RAL; n++)
                        array[left + i + n - k] = RA[n];
                    return array;
                }
            }

            return array;
        }

        #endregion

        #region 堆排序

        /// <summary>
        /// 堆排序
        /// 数据量:1000以下适用
        /// </summary>
        public static IList<T> Heap<T>(in IList<T> array) where T : IComparable
        {
            if (array.Count < 2) return array;
            for (int i = (array.Count / 2) - 1; i >= 0; i--)
            {
                HeapSort_MaxHeaping(array, i, array.Count);
            }

            for (int i = array.Count - 1; i > 0; i--)
            {
                array.Swap(0, i);
                HeapSort_MaxHeaping(array, 0, i);
            }

            return array;
        }

        /// <summary>
        /// 将指定的结点调整为堆。
        /// </summary>
        private static void HeapSort_MaxHeaping<T>(in IList<T> array, in int index, in int size)
            where T : IComparable
        {
            var left = (2 * index) + 1;
            var right = 2 * (index + 1);
            var large = index;
            if (left < size && array[left].CompareTo(array[large]) > 0) large = left;
            if (right < size && array[right].CompareTo(array[large]) > 0) large = right;
            if (index != large)
            {
                array.Swap(index, large);
                HeapSort_MaxHeaping(array, large, size);
            }
        }

        /// <summary>
        /// 堆排序
        /// 数据量:1000以下适用
        /// </summary>
        public static IList<T> Heap<T>(in IList<T> array, in Func<T, T, int> Comparer)
        {
            if (array.Count < 2) return array;
            for (int i = (array.Count / 2) - 1; i >= 0; i--)
            {
                HeapSort_MaxHeaping(array, i, array.Count, Comparer);
            }

            for (int i = array.Count - 1; i > 0; i--)
            {
                array.Swap(0, i);
                HeapSort_MaxHeaping(array, 0, i, Comparer);
            }

            return array;
        }

        /// <summary>
        /// 将指定的结点调整为堆。
        /// </summary>
        private static void HeapSort_MaxHeaping<T>(in IList<T> array, in int index, in int size,
            in Func<T, T, int> Comparer)
        {
            var left = (2 * index) + 1;
            var right = 2 * (index + 1);
            var large = index;
            if (left < size && Comparer(array[left], array[large]) > 0) large = left;
            if (right < size && Comparer(array[right], array[large]) > 0) large = right;
            if (index != large)
            {
                array.Swap(index, large);
                HeapSort_MaxHeaping(array, large, size, Comparer);
            }
        }

        #endregion

        #region 选择排序

        /// <summary>
        /// 选择排序 数据量:100以下适用
        /// </summary>
        public static IList<T> Select<T>(in IList<T> array) where T : IComparable
        {
            if (array.Count < 2) return array;
            for (int i = 0, minIndex = i; i < array.Count - 1; i++, minIndex = i)
            {
                var minVal = array[i];
                for (int j = i + 1; j < array.Count; j++)
                    if (minVal.CompareTo(array[j]) > 0)
                    {
                        minVal = array[j];
                        minIndex = j;
                    }

                array.Swap(i, minIndex);
            }

            return array;
        }

        /// <summary>
        /// 选择排序 数据量:100以下适用
        /// </summary>
        public static IList<T> Select<T>(in IList<T> array, in Func<T, T, int> Comparer) where T : IComparable
        {
            if (array.Count < 2) return array;
            for (int i = 0, minIndex = i; i < array.Count - 1; i++, minIndex = i)
            {
                var minVal = array[i];
                for (int j = i + 1; j < array.Count; j++)
                    if (Comparer(minVal, array[j]) > 0)
                    {
                        minVal = array[j];
                        minIndex = j;
                    }

                array.Swap(i, minIndex);
            }

            return array;
        }

        #endregion

        #region 希尔排序

        /// <summary>
        /// 希尔排序
        /// </summary>
        public static IList<T> Shell<T>(in IList<T> array) where T : IComparable
        {
            if (array.Count < 2) return array;
            var gap = (array.Count / 2);
            while (gap > 0)
            {
                gap /= 2;
                for (int i = 0; i < array.Count - gap; i++)
                {
                    if (array[i].CompareTo(array[i + gap]) > 0)
                    {
                        array.Swap(i, i + gap);
                        int j = i;
                        do
                        {
                            if (j - gap < 0) break;
                            if (array[j].CompareTo(array[j - gap]) >= 0) break;
                            else array.Swap(j, j - gap);
                            j = j - gap;
                        } while (j > 0);
                    }
                }
            }

            return array;
        }

        /// <summary>
        /// 希尔排序
        /// </summary>
        public static IList<T> Shell<T>(in IList<T> array, in Func<T, T, int> Comparer)
        {
            if (array.Count < 2) return array;
            var gap = (array.Count / 2);
            while (gap > 0)
            {
                gap /= 2;
                for (int i = 0; i < array.Count - gap; i++)
                {
                    if (Comparer(array[i], array[i + gap]) > 0)
                    {
                        array.Swap(i, i + gap);
                        int j = i;
                        do
                        {
                            if (j - gap < 0) break;
                            if (Comparer(array[j], array[j - gap]) >= 0) break;
                            else array.Swap(j, j - gap);
                            j = j - gap;
                        } while (j > 0);
                    }
                }
            }

            return array;
        }

        #endregion

        #region 插入排序

        /// <summary>
        /// 插入排序
        /// </summary>
        public static IList<T> Insert<T>(in IList<T> array) where T : IComparable
        {
            if (array.Count < 2) return array;
            for (int i = 1, insertions = i - 1; i < array.Count; i++, insertions = i - 1)
            {
                var undervalue = array[i];
                while (insertions >= 0 && undervalue.CompareTo(array[insertions]) < 0)
                    array[insertions + 1] = array[insertions--];
                array[insertions + 1] = undervalue;
            }

            return array;
        }

        /// <summary>
        /// 插入排序
        /// </summary>
        public static IList<T> Insert<T>(in IList<T> array, in Func<T, T, int> Comparer)
        {
            if (array.Count < 2) return array;
            for (int i = 1, insertions = i - 1; i < array.Count; i++, insertions = i - 1)
            {
                var undervalue = array[i];
                while (insertions >= 0 && Comparer(undervalue, array[insertions]) < 0)
                    array[insertions + 1] = array[insertions--];
                array[insertions + 1] = undervalue;
            }

            return array;
        }

        #endregion

        #region 快速排序

        /// <summary>
        /// 快速排序
        /// </summary>
        public static IList<T> Quick<T>(in IList<T> array) where T : IComparable
        {
            if (array.Count < 2) return array;
            Quick(array, 0, array.Count - 1);
            return array;
        }

        private static void Quick<T>(in IList<T> a, in int l, in int r) where T : IComparable
        {
            if (l < r)
            {
                var i = l;
                var j = r;
                var x = a[i];
                while (i < j)
                {
                    while (i < j && a[j].CompareTo(x) > 0) j--;
                    if (i < j) a[i++] = a[j];
                    while (i < j && a[i].CompareTo(x) < 0) i++;
                    if (i < j) a[j--] = a[i];
                }

                a[i] = x;
                Quick(a, l, i - 1);
                Quick(a, i + 1, r);
            }
        }

        /// <summary>
        /// 快速排序
        /// </summary>
        public static IList<T> Quick<T>(in IList<T> array, in Func<T, T, int> Comparer)
        {
            if (array.Count < 2) return array;
            Quick(array, 0, array.Count - 1, Comparer);
            return array;
        }

        private static void Quick<T>(in IList<T> a, in int l, in int r, in Func<T, T, int> Comparer)
        {
            if (l < r)
            {
                var i = l;
                var j = r;
                var x = a[i];
                while (i < j)
                {
                    while (i < j && Comparer(x, a[j]) > 0) j--;
                    if (i < j) a[i++] = a[j];
                    while (i < j && Comparer(x, a[i]) < 0) i++;
                    if (i < j) a[j--] = a[i];
                }

                a[i] = x;
                Quick(a, l, i - 1, Comparer);
                Quick(a, i + 1, r, Comparer);
            }
        }

        #endregion

        #region 单向冒泡排序

        /// <summary>
        /// 单向冒泡排序
        /// </summary>
        public static IList<T> Bubble<T>(in IList<T> array) where T : IComparable
        {
            if (array.Count < 2) return array;
            var len = array.Count - 1;
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len - i; j++)
                {
                    if (array[j].CompareTo(array[j + 1]) > 0) array.Swap(j, j + 1);
                }
            }

            return array;
        }

        /// <summary>
        /// 单向冒泡排序
        /// </summary>
        public static IList<T> Bubble<T>(in IList<T> array, in Func<T, T, int> Comparer)
        {
            if (array.Count < 2) return array;
            var len = array.Count - 1;
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len - i; j++)
                {
                    if (Comparer(array[j], array[j + 1]) > 0) array.Swap(j, j + 1);
                }
            }

            return array;
        }

        #endregion

        #region 双向冒泡排序

        /// <summary>
        /// 双向冒泡排序
        /// </summary>
        public static IList<T> BubbleTwoWay<T>(in IList<T> array) where T : IComparable
        {
            if (array.Count < 2) return array;
            int L = 0, R = array.Count - 1;
            while (L < R)
            {
                var flag = 0;
                int i;
                for (i = L; i < R; i++)
                    if (array[i].CompareTo(array[i + 1]) > 0)
                    {
                        array.Swap(i, i + 1);
                        flag = 1;
                    }

                if (flag == 0) break;
                for (i = --R; i > L; i--)
                    if (array[i].CompareTo(array[i - 1]) < 0)
                    {
                        array.Swap(i, i - 1);
                    }

                L++;
            }

            return array;
        }

        /// <summary>
        /// 双向冒泡排序
        /// </summary>
        public static IList<T> BubbleTwoWay<T>(in IList<T> array, in Func<T, T, int> Comparer)
        {
            if (array.Count < 2) return array;
            int L = 0, R = array.Count - 1;
            while (L < R)
            {
                var flag = 0;
                int i;
                for (i = L; i < R; i++)
                    if (Comparer(array[i], array[i + 1]) > 0)
                    {
                        array.Swap(i, i + 1);
                        flag = 1;
                    }

                if (flag == 0) break;
                for (i = --R; i > L; i--)
                    if (Comparer(array[i], array[i - 1]) < 0)
                    {
                        array.Swap(i, i - 1);
                    }

                L++;
            }

            return array;
        }

        #endregion
    }
}