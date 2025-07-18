#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class BufferByte : IWriteInt32, IReadInt32
    {
        /// <inheritdoc/> 
        public int ReadInt32(bool reverse = false) { return Arrays.GetInt32(ref ReadIndex, reverse); }

        /// <inheritdoc/> 
        public int[] ReadInt32Array(bool reverse = false) { return Arrays.GetInt32Array(ref ReadIndex, reverse); }


        /// <inheritdoc/> 
        public void WriteInt32(int value, bool reverse = false)
        {
            AutomaticExpansion(4);
            Arrays.SetInt32(ref WriteIndex, value, reverse);
        }

        /// <inheritdoc/> 
        public void WriteInt32Array(ICollection<int> value, bool reverse = false)
        {
            WriteLen(value.Count);
            AutomaticExpansion(value.Count * 4);
            foreach (var item in value) Arrays.SetInt32(ref WriteIndex, item, reverse);
        }
    }
}