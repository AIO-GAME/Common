using System;
using System.Collections.Generic;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc />
        public void WriteDictionary<K, V>(IDictionary<K, V> value)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            WriteJsonUTF8(value);
        }

        /// <inheritdoc />
        public void ReadDictionary<K, V>(IDictionary<K, V> value)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            foreach (var item in ReadJsonUTF8<IDictionary<K, V>>())
            {
                value.Add(item.Key, item.Value);
            }
        }
    }
}