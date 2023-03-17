/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


namespace AIO
{
    /// <summary>
    /// IO 加密
    /// </summary>
    public static partial class IOUtils
    {
        /// <summary>
        /// 字节位运算加密
        /// </summary>
        private static byte EncodingBitByte(byte num)
        {
            int state = (num & 0xFF);
            int n = 2;
            for (int i = 0; i < 8; i++)
            {
                if (n == 64) continue;
                if ((state & n) == n)
                {
                    state &= (~n);
                }
                else
                {
                    state |= n;
                }
                n = n * 2;
            }
            return (byte)state;
        }
    }
}
