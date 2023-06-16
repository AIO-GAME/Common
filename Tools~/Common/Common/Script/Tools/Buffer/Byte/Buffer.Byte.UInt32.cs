using System.Collections.Generic;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public uint ReadUInt32(bool reverse = false)
        {
            return Arrays.GetUInt32(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public uint[] ReadUInt32Array(bool reverse = false)
        {
            return Arrays.GetUInt32Array(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public void WriteUInt32(uint value, bool reverse = false)
        {
            AutomaticExpansion(4);
            Arrays.SetUInt32(ref WriteIndex, value, reverse);
        }

        /// <inheritdoc/> 
        public void WriteUInt32Array(ICollection<uint> value, bool reverse = false)
        {
            WriteLen(value.Count);
            AutomaticExpansion(value.Count * 4);
            foreach (var item in value) Arrays.SetUInt32(ref WriteIndex, item, reverse);
        }
    }
}