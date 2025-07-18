#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class BufferByte : IWriteSbyte, IReadSbyte
    {
        /// <inheritdoc/> 
        public sbyte ReadSbyte() { return (sbyte)Arrays[ReadIndex++]; }

        /// <inheritdoc/> 
        public sbyte[] ReadSbyteArray(bool reverse = false) { return Arrays.GetSByteArray(ref ReadIndex, reverse); }

        /// <inheritdoc/> 
        public void WriteSbyte(sbyte value)
        {
            AutomaticExpansion(1);
            Arrays[WriteIndex++] = (byte)value;
        }

        /// <inheritdoc/> 
        public void WriteSbyteArray(ICollection<sbyte> value, bool reverse = false)
        {
            AutomaticExpansion(value.Count);
            Arrays.SetSByteArray(ref WriteIndex, value, reverse);
        }
    }
}