using System.Collections.Generic;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public ushort[] ReadUint16Array(in bool reverse = false)
        {
            return Arrays.GetUInt16Array(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public short[] ReadInt16Array(in bool reverse = false)
        {
            return Arrays.GetInt16Array(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public void WriteUint16Array(in ICollection<ushort> value, in bool reverse = false)
        {
            WriteLen(value.Count);
            AutomaticExpansion(value.Count * 2);
            foreach (var item in value) Arrays.SetUInt16(ref WriteIndex, item, reverse);
        }

        /// <inheritdoc/> 
        public void WriteInt16Array(in ICollection<short> value, in bool reverse = false)
        {
            WriteLen(value.Count);
            AutomaticExpansion(value.Count * 2);
            foreach (var item in value) Arrays.SetInt16(ref WriteIndex, item, reverse);
        }
    }

    public partial interface IWrite
    {
        void WriteUint16Array(in ICollection<ushort> value, in bool reverse = false);

        void WriteInt16Array(in ICollection<short> value, in bool reverse = false);
    }

    public partial interface IRead
    {
        ushort[] ReadUint16Array(in bool reverse = false);

        short[] ReadInt16Array(in bool reverse = false);
    }
}