using System;
using System.Collections.Generic;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc />
        public void WriteCollection<T>(ICollection<T> value)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            WriteJsonUTF8(value);
        }

        /// <inheritdoc />
        public void ReadCollection<T>(ICollection<T> value)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            foreach (var item in ReadJsonUTF8<ICollection<T>>()) value.Add(item);
        }
    }
}