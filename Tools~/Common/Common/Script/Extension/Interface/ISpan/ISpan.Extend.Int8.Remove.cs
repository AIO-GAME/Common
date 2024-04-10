#region

using System;

#endregion

namespace AIO
{
    partial class ExtendISpan
    {
        /// <summary>
        /// 移除 重复 元素 并将其移动到数组末尾
        /// </summary>
        /// <remarks>
        /// 不重新分配内存 source.<see cref="MRemoveAll{T}"/>
        /// </remarks>
        public static sbyte[] MRemoveAll(this sbyte[] arrays, int value)
        {
            if (arrays is null) throw new ArgumentNullException(nameof(arrays));

            var index = 0;
            var len = arrays.Length;
            for (var i = 0; i < len; i++)
                if (arrays[i] == value)
                {
                    index++;
                }
                else if (index > 0)
                {
                    arrays[i - index] = arrays[i];
                    arrays[i]         = default;
                }

            for (var i = len - index; i < len; i++) arrays[i] = default;
            return arrays;
        }
    }
}