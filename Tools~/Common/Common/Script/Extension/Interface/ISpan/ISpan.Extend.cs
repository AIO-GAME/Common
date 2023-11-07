/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


namespace AIO
{
    // float    类型:System.Single    说明:32位单精度浮点数    位数:7
    // double   类型:System.Double    说明:64位双精度浮点数    位数:15/16
    // decimal  类型:System.Decimal   说明:128位双精度浮点数   位数:28/29   范围:(-7.9 x 1028 - 7.9 x 1028) / (100 - 28)

    //  sbyte   表示有符号的8位整数      -128~127
    //  byte    表示无符号的8位整数      0~255
    //  short   表示有符号的16位整数     -32768~32767
    //  ushort  表示无符号的16位整数     0~65535
    //  int     表示有符号的32位整数     -2147483648~2147483647
    //  uint    表示无符号的32位整数     0~4294967295
    //  long    表示有符号的64位整数     -9223372036854775808~9223372036854775807
    //  ulong   表示无符号的64位整数     0~18446744073709551615
    //  char    表示无符号的16位整数     0~65535

    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// 数组方法
    /// </summary>
    public static partial class ExtendISpan
    {
        /// <summary>
        /// 自动扩容
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] AutoMaticExpansion<T>(this T[] arrays, in int offset, in int count)
        {
            var currentCapacity = arrays.Length;
            var requiredCapacity = offset + count;
            if (requiredCapacity >= currentCapacity)
                Array.Resize(ref arrays, currentCapacity + count);
            return arrays;
        }
    }
}