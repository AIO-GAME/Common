#region

using System;
using System.Collections.Generic;

#endregion

namespace AIO
{
    public partial class BufferByte
    {
        #region IReadData Members

        /// <inheritdoc />
        public void ReadCollection<T>(ICollection<T> value)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            foreach (var item in ReadJsonUTF8<ICollection<T>>()) value.Add(item);
        }

        #endregion

        #region IWriteData Members

        /// <inheritdoc />
        public void WriteCollection<T>(ICollection<T> value)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            WriteJsonUTF8(value);
        }

        #endregion
    }
}