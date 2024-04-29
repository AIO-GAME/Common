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
        public void ReadList<T>(IList<T> value)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            foreach (var item in ReadJsonUTF8<IList<T>>()) value.Add(item);
        }

        #endregion

        #region IWriteData Members

        /// <inheritdoc />
        public void WriteList<T>(IList<T> value)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            WriteJsonUTF8(value);
        }

        #endregion
    }
}