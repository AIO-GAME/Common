using System.Collections.Generic;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public int ReadInt32(in bool reverse = false)
        {
            return Arrays.GetInt32(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public int[] ReadInt32Array(in bool reverse = false)
        {
            return Arrays.GetInt32Array(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public void WriteInt32(in int value, in bool reverse = false)
        {
            AutomaticExpansion(4);
            Arrays.SetInt32(ref WriteIndex, value, reverse);
        }

        /// <inheritdoc/> 
        public void WriteInt32Array(in ICollection<int> value, in bool reverse = false)
        {
            WriteLen(value.Count);
            AutomaticExpansion(value.Count * 4);
            foreach (var item in value) Arrays.SetInt32(ref WriteIndex, item, reverse);
        }
    }
}