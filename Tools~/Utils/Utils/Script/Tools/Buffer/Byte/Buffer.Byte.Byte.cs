using System.Collections.Generic;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public byte ReadByte()
        {
            return Arrays[ReadIndex++];
        }

        /// <inheritdoc/> 
        public byte[] ReadByteArray(in bool reverse = false)
        {
            return Arrays.GetByteArray(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public void WriteByte(in byte value)
        {
            AutomaticExpansion(1);
            Arrays[WriteIndex++] = value;
        }

        /// <inheritdoc/> 
        public void WriteByteArray(in ICollection<byte> value, in bool reverse = false)
        {
            AutomaticExpansion(value.Count);
            Arrays.SetByteArray(ref WriteIndex, value, reverse);
        }
    }
}