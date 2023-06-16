using System.Collections.Generic;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public ushort ReadUInt16(bool reverse = false)
        {
            return Arrays.GetUInt16(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public void WriteUInt16(ushort value, bool reverse = false)
        {
            AutomaticExpansion(2);
            Arrays.SetUInt16(ref WriteIndex, value, reverse);
        }
        /// <inheritdoc/> 
        public ushort[] ReadUInt16Array(bool reverse = false)
        {
            return Arrays.GetUInt16Array(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public void WriteUInt16Array(ICollection<ushort> value, bool reverse = false)
        {
            WriteLen(value.Count);
            AutomaticExpansion(value.Count * 2);
          foreach (var item in value) Arrays.SetUInt16(ref WriteIndex, item, reverse);
        }

    }

}