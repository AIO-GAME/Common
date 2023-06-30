/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System;
using System.Runtime.CompilerServices;

namespace AIO
{
    public static partial class StringExtend
    {
        /// <summary>
        /// 将指定内容循环插入到字符串中
        /// </summary>
        /// <typeparam name="T">指定内容的类型</typeparam>
        /// <param name="str">原始字符串</param>
        /// <param name="space">间隔长度</param>
        /// <param name="info">指定内容</param>
        /// <returns>插入指定内容后的字符串</returns>
        public static string InsertFixed<T>(this string str, in int space, in T info)
        {
            if (string.IsNullOrEmpty(str) || space <= 0) return str;

            // 获取指定内容字符串
            var context = info.ToString();

            // 计算需要插入的次数
            if (string.IsNullOrEmpty(str))
            {
                var insertCount = (int)Math.Ceiling(str.Length / (double)space) - 1;

                // 插入指定内容字符串
                for (var i = 0; i < insertCount; i++)
                {
                    var index = (i + 1) * space + i * context.Length;
                    str = str.Insert(index, context);
                }
            }

            return str;
        }
    }
}