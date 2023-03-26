/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System;
using System.Runtime.CompilerServices;

public partial class Utils
{
    /// <summary> 
    /// 屏蔽字
    /// </summary>
    public static class MaskWord
    {
        private static string[] words = Array.Empty<string>();

        /// <summary>
        /// 替换屏蔽字为*
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Replace(string str)
        {
            if (words.Length == 0) return str;
            foreach (var item in words)
            {
                var index = str.IndexOf(item, StringComparison.CurrentCulture);
                if (index < 0) continue;
                var value = "";
                if (index > 0) value = str.Substring(0, index);
                for (var j = 0; j < item.Length; j++) value += '*';
                if (str.Length > index + item.Length) value += str.Substring(index + item.Length);
                str = value;
            }

            return str;
        }

        /// <summary>
        /// 加载屏蔽文本
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LoadMaskWord(string context)
        {
            if (words.Length == 0)
            {
                words = context.Split(new char[] { '\n' });
            }
        }
    }
}
