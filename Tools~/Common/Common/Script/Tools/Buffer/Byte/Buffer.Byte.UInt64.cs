#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    public partial class BufferByte
    {
        #region IReadData Members

        /// <inheritdoc/> 
        public ulong[] ReadUInt64Array(bool reverse = false)
        {
            return Arrays.GetUInt64Array(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public ulong ReadUInt64(bool reverse = false)
        {
            return Arrays.GetUInt64(ref ReadIndex, reverse);
        }

        #endregion

        #region IWriteData Members

        /// <inheritdoc/> 
        public void WriteUInt64Array(ICollection<ulong> value, bool reverse = false)
        {
            WriteLen(value.Count);
            AutomaticExpansion(value.Count * 8);
            foreach (var item in value) Arrays.SetUInt64(ref WriteIndex, item, reverse);
        }

        /// <inheritdoc/> 
        public void WriteUInt64(ulong value, bool reverse = false)
        {
            AutomaticExpansion(8);
            Arrays.SetUInt64(ref WriteIndex, value, reverse);
        }

        #endregion
    }
}