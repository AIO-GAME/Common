using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AIO
{
    public partial class ExtendISpan
    {
        /// <summary>
        /// 复制
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] CopyTo<T>(this T[] arrays, in int offset)
        {
            var copy = new T[offset];
            Array.ConstrainedCopy(arrays, 0, copy, 0, Math.Min(offset, arrays.Length));
            return copy;
        }
    }
}
