/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System.Runtime.CompilerServices;

public partial class AHelper
{
    public partial class IO
    {
        /// <summary>
        /// 字节位运算加密
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte EncodingBitByte(in byte num)
        {
            var state = num & 0xFF;
            var n = 2;
            for (var i = 0; i < 8; i++)
            {
                if (n == 64) continue;
                if ((state & n) == n)
                    state &= (~n);
                else state |= n;
                n *= 2;
            }

            return (byte)state;
        }
    }
}
