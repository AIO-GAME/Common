/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


namespace AIO
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// 句柄
    /// </summary>
    public static partial class IntPtrExtend
    {
        /// <summary>
        /// 转化为引用地址
        /// </summary>
        public static string ToConverMemory(this IntPtr intPtr)
        {
            return string.Concat("0x", intPtr.ToString("X"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intPtr"></param>
        /// <returns></returns>
        public static int[] ToConverInts(this IntPtr intPtr)
        {
            return (int[])GCHandle.FromIntPtr(intPtr).Target;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intPtr"></param>
        /// <returns></returns>
        public static object ToConverObject(this IntPtr intPtr)
        {
            return GCHandle.FromIntPtr(intPtr).Target;
        }
    }
}