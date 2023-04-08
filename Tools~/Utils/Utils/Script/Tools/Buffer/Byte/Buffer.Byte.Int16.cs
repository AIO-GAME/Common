using System.Collections.Generic;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public short ReadInt16(in bool reverse = false)
        {
            return Arrays.GetInt16(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public short[] ReadInt16Array(in bool reverse = false)
        {
            return Arrays.GetInt16Array(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public void WriteInt16(in short value, in bool reverse = false)
        {
            AutomaticExpansion(2);
            Arrays.SetInt16(ref WriteIndex, value, reverse);
        }

        /// <inheritdoc/> 
        public void WriteInt16Array(in ICollection<short> value, in bool reverse = false)
        {
            WriteLen(value.Count);
            AutomaticExpansion(value.Count * 2);
            foreach (var item in value) Arrays.SetInt16(ref WriteIndex, item, reverse);
        }
    }
}