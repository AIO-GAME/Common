using System;
using System.Linq;

namespace AIO
{
    partial class ExtendISpan
    {
        /// <summary>
        /// 移除 指定元素 默认移除 null
        /// </summary>
        /// <remarks> 不修改原始数组 </remarks>
        public static T[] Exclude<T>(this T[] arrays, T value = default)
        {
            if (arrays is null)
                return Array.Empty<T>();
            if (typeof(T).IsClass)
                return arrays.Where(item => item != null && !item.Equals(value)).ToArray();
            if (typeof(T).IsValueType)
                return arrays.Where(item => !item.Equals(value)).ToArray();
            throw new NotSupportedException($"{typeof(T).FullName} not support Exclude");
        }

        /// <summary>
        /// 将空元素移除到数组末尾
        /// </summary>
        /// <remarks>修改原始数组</remarks>
        public static T[] MExclude<T>(this T[] arrays, T value = default)
        {
            if (arrays is null) throw new ArgumentNullException(nameof(arrays));
            var index = 0;
            for (var i = 0; i < arrays.Length; i++)
            {
                if (arrays[i] != null && arrays[i].Equals(value)) index++;
                else if (index > 0)
                {
                    arrays[i - index] = arrays[i];
                    arrays[i] = default;
                }
            }

            for (var i = arrays.Length - index; i < arrays.Length; i++) arrays[i] = default;
            return arrays;
        }

        /// <summary>
        /// 移除 重复 元素 并将其移动到数组末尾
        /// </summary>
        /// <remarks>修改原始数组</remarks>
        public static T[] MRemoveAll<T>(this T[] arrays, T value)
        {
            if (arrays is null) throw new ArgumentNullException(nameof(arrays));

            var index = 0;
            for (var i = 0; i < arrays.Length; i++)
            {
                if (arrays[i] != null && arrays[i].Equals(value)) index++;
                else if (index > 0)
                {
                    arrays[i - index] = arrays[i];
                    arrays[i] = default;
                }
            }

            for (var i = arrays.Length - index; i < arrays.Length; i++) arrays[i] = default;
            return arrays;
        }

        /// <summary>
        /// 移除 重复 元素的第一个
        /// </summary>
        /// <remarks>修改原始数组</remarks>
        public static T[] Remove<T>(this T[] arrays, T value)
        {
            if (arrays is null) throw new ArgumentNullException(nameof(arrays));
            if (value is null) return arrays;
            for (var i = 0; i < arrays.Length; i++)
            {
                if (arrays[i] is null) continue;
                if (arrays[i].Equals(value))
                {
                    var newArray = new T[arrays.Length - 1];
                    Array.ConstrainedCopy(arrays, 0, newArray, 0, i);
                    Array.ConstrainedCopy(arrays, i + 1, newArray, i, arrays.Length - i - 1);
                    return newArray;
                }
            }

            return arrays;
        }

        /// <summary>
        /// 移除 重复 元素的第一个
        /// </summary>
        /// <remarks>修改原始数组</remarks>
        public static T[] MRemove<T>(this T[] arrays, T value)
        {
            if (arrays is null) throw new ArgumentNullException(nameof(arrays));
            if (value is null) return arrays;
            for (var i = 0; i < arrays.Length; i++)
            {
                if (arrays[i] is null) continue;
                if (value.Equals(arrays[i])) return arrays.MRemoveAt(i);
            }

            return arrays;
        }

        /// <summary>
        /// 移除重复元素
        /// </summary>
        public static T[] MRemoveRepeat<T>(this T[] array)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Length <= 1) return array;
            var hashSet = Pool.HashSet<T>();
            for (var index = 0; index < array.Length; index++)
            {
                var num = array[index];
                array[index] = default;
                if (hashSet.Count == 0 || !hashSet.Contains(num))
                {
                    array[hashSet.Count] = num;
                    hashSet.Add(num);
                }
            }

            hashSet.Free();
            return array;
        }

        /// <summary>
        /// 移除重复元素
        /// </summary>
        public static T[] RemoveRepeat<T>(this T[] array)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Length <= 0) return Array.Empty<T>();
            var hashSet = Pool.HashSet<T>();
            foreach (var num in array)
            {
                if (hashSet.Count == 0 || !hashSet.Contains(num))
                {
                    hashSet.Add(num);
                }
            }

            var newArray = hashSet.ToArray();
            hashSet.Free();
            return newArray;
        }

        #region RemoveAt

        #region Modify

        /// <summary>
        /// 移除 指定 下标元素
        /// </summary>
        /// <remarks>修改原始数组</remarks>
        public static T[] MRemoveAt<T>(this T[] arrays, in int index)
        {
            if (arrays is null) throw new ArgumentNullException(nameof(arrays));
            var len = arrays.Length - 1;
            if (index < 0 || index > len) throw new IndexOutOfRangeException("RemoveAt index out of range");
            for (var i = index; i < len; i++)
                arrays[i] = arrays[i + 1];
            arrays[len] = default;
            return arrays;
        }

        /// <summary>
        /// 移除 指定 下标元素
        /// </summary>
        /// <remarks>修改原始数组</remarks>
        public static T[] MRemoveAt<T>(this T[] arrays, params int[] indexes)
        {
            if (arrays is null) throw new ArgumentNullException(nameof(arrays));
            for (int i = 0, len = arrays.Length - 1; i < indexes.Length; i++)
            {
                var index = indexes[i];
                if (index < 0 || index > len) throw new IndexOutOfRangeException("RemoveAt index out of range");
                for (var j = index - i; j < len; j++)
                    arrays[j] = arrays[j + 1];
                arrays[len - i] = default;
            }

            return arrays;
        }

        #endregion

        #region Not Modify

        /// <summary>
        /// 移除 指定 下标元素
        /// </summary>
        /// <remarks>不修改原始数组</remarks>
        public static T[] RemoveAt<T>(this T[] arrays, in int index)
        {
            if (arrays is null) throw new ArgumentNullException(nameof(arrays));
            var len = arrays.Length - 1;
            if (index < 0 || index > len) throw new IndexOutOfRangeException("RemoveAt index out of range");
            var newArray = new T[len];
            Array.ConstrainedCopy(arrays, 0, newArray, 0, index);
            Array.ConstrainedCopy(arrays, index + 1, newArray, index, len - index);
            return newArray;
        }


        /// <summary>
        /// 移除 指定 下标元素
        /// </summary>
        /// <remarks>不修改原始数组</remarks>
        public static T[] RemoveAt<T>(this T[] arrays, int index, params int[] indexes)
        {
            if (arrays is null) throw new ArgumentNullException(nameof(arrays));
            var len = arrays.Length - 1;
            var newArray = new T[arrays.Length - indexes.Length];
            indexes.MRemoveRepeat().Sort();
            for (int i = 0, offset = 0, ids = 0; i <= len && index >= 0 && index <= len; i++)
            {
                if (index > i) newArray[offset++] = arrays[i];
                else
                {
                    if (ids == indexes.Length)
                    {
                        Array.ConstrainedCopy(arrays, i + 1, newArray, offset, len - i);
                        break;
                    }

                    index = indexes[ids++];
                }
            }


            return newArray;
        }

        #endregion

        #endregion
    }
}