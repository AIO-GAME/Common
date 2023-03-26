/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System;
using System.Runtime.CompilerServices;
using System.Text;

    public static partial class Utils
    {
        /// <summary>
        /// hash工具
        /// </summary>
        public static partial class Hash
        {
            
            /// <summary>
            /// 转化为哈希值
            /// </summary>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static string ToHash(byte[] data)
            {
                if (data == null) return default;
                var builder = new StringBuilder();
                foreach (var t in data)
                    builder.Append(t.ToString("x2"));
                return builder.ToString();
            }

            /// <summary>
            /// 比较32位校验码
            /// </summary>
            /// <param name="input"></param>
            /// <param name="hash"></param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool VerifyCrc32Hash(string input, string hash)
            {
                var comparer = StringComparer.OrdinalIgnoreCase;
                return 0 == comparer.Compare(input, hash);
            }
        }
    }
