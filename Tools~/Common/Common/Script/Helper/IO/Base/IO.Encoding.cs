namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: IO

        public partial class IO
        {
            /// <summary>
            /// 字节位运算加密
            /// </summary>
            private static byte EncodingBitByte(in byte num)
            {
                var state = num & 0xFF;
                for (int i = 0, n = 2; i < 8; i++)
                {
                    if (n == 64) continue;
                    if ((state & n) == n) state &= ~n;
                    else state                  |= n;
                    n *= 2;
                }

                return (byte)state;
            }
        }

        #endregion
    }
}