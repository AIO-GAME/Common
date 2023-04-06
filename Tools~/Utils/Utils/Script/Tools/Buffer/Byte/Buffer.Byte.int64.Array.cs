using System.Collections.Generic;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public long[] ReadInt64Array(in bool reverse = false)
        {
            return Arrays.GetInt64Array(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public ulong[] ReadUInt64Array(in bool reverse = false)
        {
            return Arrays.GetUInt64Array(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public void WriteInt64Array(in ICollection<long> value, in bool reverse = false)
        {
            WriteLen(value.Count);
            AutomaticExpansion(value.Count * 8);
            foreach (var item in value) Arrays.SetInt64(ref WriteIndex, item, reverse);
        }

        /// <inheritdoc/> 
        public void WriteUInt64Array(in ICollection<ulong> value, in bool reverse = false)
        {
            WriteLen(value.Count);
            AutomaticExpansion(value.Count * 8);
            foreach (var item in value) Arrays.SetUInt64(ref WriteIndex, item, reverse);
        }
    }

    public partial interface IWrite
    {
        void WriteInt64Array(in ICollection<long> value, in bool reverse = false);
        void WriteUInt64Array(in ICollection<ulong> value, in bool reverse = false);
    }

    public partial interface IRead
    {
        long[] ReadInt64Array(in bool reverse = false);
        ulong[] ReadUInt64Array(in bool reverse = false);
    }
}