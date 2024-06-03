#region

using System;
using System.Linq;

#endregion

namespace AIO
{
    partial class ExtendISpan
    {
        #region TrimStart

        #region Not Modify

        /// <summary>
        /// 移除 数组开始指定元素
        /// </summary>
        /// <remarks>不修改原数组</remarks>
        public static T[] TrimStart<T>(this T[] arrays)
        {
            if (arrays is null) throw new ArgumentNullException(nameof(arrays));
            var index = 0;
            var len = arrays.Length - 1;
            var def = default(T);
            for (var i = 0; i <= len; i++)
                if (arrays[i].Equals(def)) index++;
                else break;

            if (index == 0) return arrays;
            var newArrays = new T[arrays.Length - index];
            Array.Copy(arrays, index, newArrays, 0, newArrays.Length);
            return newArrays;
        }

        /// <summary>
        /// 移除 数组开始指定元素
        /// </summary>
        /// <remarks>不修改原数组</remarks>
        public static T[] TrimStart<T>(this T[] arrays, T value)
        {
            if (arrays is null) throw new ArgumentNullException(nameof(arrays));
            var index = 0;
            var len = arrays.Length - 1;
            for (var i = 0; i <= len; i++)
                if (arrays[i].Equals(value)) index++;
                else break;

            if (index == 0) return arrays;
            var newArrays = new T[arrays.Length - index];
            Array.Copy(arrays, index, newArrays, 0, newArrays.Length);
            return newArrays;
        }

        /// <summary>
        /// 移除 数组开始指定元素
        /// </summary>
        /// <remarks>不修改原数组</remarks>
        public static T[] TrimStart<T>(this T[] arrays, params T[] values)
        {
            if (arrays is null) throw new ArgumentNullException(nameof(arrays));
            var index = 0;
            var len = arrays.Length - 1;
            for (var i = 0; i <= len; i++)
                if (values.Contains(arrays[i])) index++;
                else break;

            if (index == 0) return arrays;
            var newArrays = new T[arrays.Length - index];
            Array.Copy(arrays, index, newArrays, 0, newArrays.Length);
            return newArrays;
        }

        #endregion

        #region Modify

        /// <summary>
        /// 移除 数组开始指定元素
        /// </summary>
        /// <remarks>修改原数组</remarks>
        public static T[] MTrimStart<T>(this T[] arrays)
        {
            if (arrays is null) throw new ArgumentNullException(nameof(arrays));
            var index = 0;
            var len = arrays.Length - 1;
            var def = default(T);
            for (var i = 0; i <= len; i++)
                if (arrays[i].Equals(def)) index++;
                else break;

            if (index == 0) return arrays;
            for (var i = len; i >= len - index; i--)
            {
                arrays[--index] = arrays[i];
                arrays[i]       = def;
            }

            return arrays;
        }

        /// <summary>
        /// 移除 数组开始指定元素
        /// </summary>
        /// <remarks>修改原数组</remarks>
        public static T[] MTrimStart<T>(this T[] arrays, T value)
        {
            if (arrays is null) throw new ArgumentNullException(nameof(arrays));
            var index = 0;
            var len = arrays.Length - 1;
            for (var i = 0; i <= len; i++)
                if (arrays[i].Equals(value)) index++;
                else break;

            if (index == 0) return arrays;
            for (var i = len; i >= len - index; i--)
            {
                arrays[--index] = arrays[i];
                arrays[i]       = default;
            }

            return arrays;
        }

        /// <summary>
        /// 移除 数组开始指定元素
        /// </summary>
        /// <remarks>修改原数组</remarks>
        public static T[] MTrimStart<T>(this T[] arrays, params T[] values)
        {
            if (arrays is null) throw new ArgumentNullException(nameof(arrays));
            var index = 0;
            var len = arrays.Length - 1;
            var def = default(T);
            for (var i = 0; i <= len; i++)
                if (values.Contains(arrays[i])) index++;
                else break;

            if (index == 0) return arrays;
            for (var i = len; i >= len - index; i--)
            {
                arrays[--index] = arrays[i];
                arrays[i]       = def;
            }

            arrays[0] = def;
            return arrays;
        }

        #endregion

        #endregion
    }
}