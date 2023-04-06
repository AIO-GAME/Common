using System.Collections.Generic;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public int[] ReadInt32Array(in bool reverse = false)
        {
            return Arrays.GetInt32Array(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public uint[] ReadUint32Array(in bool reverse = false)
        {
            return Arrays.GetUInt32Array(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public void WriteInt32Array(in ICollection<int> value, in bool reverse = false)
        {
            WriteLen(value.Count);
            AutomaticExpansion(value.Count * 4);
            foreach (var item in value) Arrays.SetInt32(ref WriteIndex, item, reverse);
        }

        /// <inheritdoc/> 
        public void WriteUint32Array(in ICollection<uint> value, in bool reverse = false)
        {
            WriteLen(value.Count);
            AutomaticExpansion(value.Count * 4);
            foreach (var item in value) Arrays.SetUInt32(ref WriteIndex, item, reverse);
        }
    }

    public partial interface IWrite
    {
        void WriteInt32Array(in ICollection<int> value, in bool reverse = false);
        void WriteUint32Array(in ICollection<uint> value, in bool reverse = false);
    }

    public partial interface IRead
    {
        int[] ReadInt32Array(in bool reverse = false);
        uint[] ReadUint32Array(in bool reverse = false);
    }
}