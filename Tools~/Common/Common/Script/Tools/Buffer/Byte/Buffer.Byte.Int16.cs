using System.Collections.Generic;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public short ReadInt16(bool reverse = false)
        {
            return Arrays.GetInt16(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public short[] ReadInt16Array(bool reverse = false)
        {
            return Arrays.GetInt16Array(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public void WriteInt16(short value, bool reverse = false)
        {
            AutomaticExpansion(2);
            Arrays.SetInt16(ref WriteIndex, value, reverse);
        }

        /// <inheritdoc/> 
        public void WriteInt16Array(ICollection<short> value, bool reverse = false)
        {
            WriteLen(value.Count);
            AutomaticExpansion(value.Count * 2);
          foreach (var item in value) Arrays.SetInt16(ref WriteIndex, item, reverse);
        }
    }
}