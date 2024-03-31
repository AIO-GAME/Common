using System;

namespace AIO
{
    partial class ExtendISpan
    {
        /// <summary>
        /// Slice an array from start to length
        /// </summary>
        /// <param name="array"> Array </param>
        /// <param name="start"> Start index </param>
        /// <param name="end"> End index </param>
        /// <returns> int[] </returns>
        public static unsafe int[] Slice(int* array, in int start, in int end)
        {
            var length = end - start;
            var result = new int[length];
            for (var i = 0; i < length; i++) result[i] = array[start + i];
            return result;
        }

    }
}