using System.Collections.Generic;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public sbyte ReadSByte()
        {
            return (sbyte)Arrays[ReadIndex++];
        }

        /// <inheritdoc/> 
        public sbyte[] ReadSByteArray(bool reverse = false)
        {
            return Arrays.GetSByteArray(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public void WriteSByte(sbyte value)
        {
            AutomaticExpansion(1);
            Arrays[WriteIndex++] = (byte)value;
        }

        /// <inheritdoc/> 
        public void WriteSByteArray(ICollection<sbyte> value, bool reverse = false)
        {
            AutomaticExpansion(value.Count);
            Arrays.SetSByteArray(ref WriteIndex, value, reverse);
        }
    }
}