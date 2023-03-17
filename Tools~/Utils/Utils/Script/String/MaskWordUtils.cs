/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


namespace AIO
{
    /// <summary> 
    /// 屏蔽字
    /// </summary>
    public static class MaskWordUtils
    {
        private static string[] words = new string[] { };

        /// <summary>
        /// 替换屏蔽字为*
        /// </summary>
        public static string Replace(string str)
        {
            for (int i = 0; i < words.Length; i++)
            {
                int index = str.IndexOf(words[i]);
                if (index < 0) continue;
                string value = "";
                if (index > 0) value = str.Substring(0, index);
                for (int j = 0; j < words[i].Length; j++)
                {
                    value += '*';
                }
                if (str.Length > index + words[i].Length) value += str.Substring(index + words[i].Length);
                str = value;
            }
            return str;
        }

        /// <summary> 加载屏蔽文本 </summary>
        public static void LoadMaskWord(string context)
        {
            if (words.Length == 0)
            {
                words = context.Split(new char[] { '\n' });
            }
        }
    }
}
