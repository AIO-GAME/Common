using System;
using System.Collections.Generic;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc />
        public void WriteList<T>(in IList<T> value)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            WriteJsonUTF8(value);
        }

        /// <inheritdoc />
        public void ReadList<T>(IList<T> value)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            foreach (var item in ReadJsonUTF8<IList<T>>()) value.Add(item);
        }
    }
}