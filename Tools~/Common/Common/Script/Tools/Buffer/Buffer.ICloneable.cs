#region

using System;

#endregion

namespace AIO
{
    public partial class Buffer<T> : ICloneable
    {
        #region ICloneable Members

        /// <inheritdoc/>
        public object Clone()
        {
            var clone = new Buffer<T>();
            clone.Arrays = new T[Arrays.Length];
            Arrays.CopyTo(clone.Arrays, 0);
            clone.ReadIndex  = ReadIndex;
            clone.WriteIndex = WriteIndex;
            return clone;
        }

        #endregion
    }
}