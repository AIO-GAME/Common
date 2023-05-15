﻿using System.Collections.Generic;

namespace AIO
{
    public partial class Buffer<T> : IEqualityComparer<Buffer<T>>
    {
        /// <inheritdoc/>
        public bool Equals(Buffer<T> x, Buffer<T> y)
        {
            if (x.ReadIndex != y.ReadIndex) return false;
            if (x.WriteIndex != y.WriteIndex) return false;
            if (x.Capacity != y.Capacity) return false;
            if (x.Arrays != y.Arrays) return false;
            return true;
        }

        /// <inheritdoc/>
        public int GetHashCode(Buffer<T> obj)
        {
            var code = 0;
            code += obj.ReadIndex.GetHashCode();
            code += obj.WriteIndex.GetHashCode();
            code += obj.Capacity.GetHashCode();
            code += obj.Arrays.GetHashCode();
            return code;
        }
    }
}
