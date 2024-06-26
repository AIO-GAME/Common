#region

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#endregion

namespace AIO
{
    public static partial class ExtendIntPtr
    {
        /// <summary>
        ///     转化为引用地址
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToConverseMemory(this IntPtr intPtr)
        {
            return $"0x{intPtr.ToString("X")}";
        }

        /// <summary>
        ///     转化为int数组
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int[] TooConverseInts(this IntPtr intPtr)
        {
            var handle = GCHandle.FromIntPtr(intPtr);
            if (!(handle.Target is int[] ints)) throw new ArgumentException("The pointer does not point to a valid int array.");

            return ints;
        }

        /// <summary>
        ///     转化为 object
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object TooConverseObject(this IntPtr intPtr)
        {
            return GCHandle.FromIntPtr(intPtr).Target;
        }
    }
}