using System.Collections.Generic;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public ulong[] ReadUInt64Array(in bool reverse = false)
        {
            return Arrays.GetUInt64Array(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public void WriteUInt64Array(in ICollection<ulong> value, in bool reverse = false)
        {
            WriteLen(value.Count);
            AutomaticExpansion(value.Count * 8);
            foreach (var item in value) Arrays.SetUInt64(ref WriteIndex, item, reverse);
        }

        /// <inheritdoc/> 
        public ulong ReadUInt64(in bool reverse = false)
        {
            return Arrays.GetUInt64(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public void WriteUInt64(in ulong value, in bool reverse = false)
        {
            AutomaticExpansion(8);
            Arrays.SetUInt64(ref WriteIndex, value, reverse);
        }
    }
}