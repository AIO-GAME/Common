using System.Collections.Generic;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public uint ReadUInt32(in bool reverse = false)
        {
            return Arrays.GetUInt32(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public uint[] ReadUInt32Array(in bool reverse = false)
        {
            return Arrays.GetUInt32Array(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public void WriteUInt32(in uint value, in bool reverse = false)
        {
            AutomaticExpansion(4);
            Arrays.SetUInt32(ref WriteIndex, value, reverse);
        }

        /// <inheritdoc/> 
        public void WriteUInt32Array(in ICollection<uint> value, in bool reverse = false)
        {
            WriteLen(value.Count);
            AutomaticExpansion(value.Count * 4);
            foreach (var item in value) Arrays.SetUInt32(ref WriteIndex, item, reverse);
        }
    }
}