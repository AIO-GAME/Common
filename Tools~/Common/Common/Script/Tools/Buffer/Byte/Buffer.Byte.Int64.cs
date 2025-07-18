#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public long ReadInt64(bool reverse = false)
        {
            return Arrays.GetInt64(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public long[] ReadInt64Array(bool reverse = false)
        {
            return Arrays.GetInt64Array(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public void WriteInt64Array(ICollection<long> value, bool reverse = false)
        {
            WriteLen(value.Count);
            AutomaticExpansion(value.Count * 8);
            foreach (var item in value) Arrays.SetInt64(ref WriteIndex, item, reverse);
        }

        /// <inheritdoc/> 
        public void WriteInt64(long value, bool reverse = false)
        {
            AutomaticExpansion(8);
            Arrays.SetInt64(ref WriteIndex, value, reverse);
        }

    }
}