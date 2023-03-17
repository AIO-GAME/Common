/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


namespace AIO
{
    using System;

    public static partial class StringExtend
    {
        /// <summary>
        /// 循环插入指定内容
        /// 效果: str="888" space=1 info="-" 输出 "8-8-8"
        /// </summary>
        public static string InsertFixed(this string str, int space, object info)
        {
            if (str.Length == 0 || space <= 0) return str;
            var context = info.ToString();
            int h = (int)Math.Ceiling((double)(str.Length / space));
            for (int i = 1, index = 0, len; i <= h; i++)
            {
                len = index * context.Length + i * space;
                if (len < str.Length)
                {
                    str = str.Insert(len, context);
                    ++index;
                }
            }
            return str;
        }
    }
}
