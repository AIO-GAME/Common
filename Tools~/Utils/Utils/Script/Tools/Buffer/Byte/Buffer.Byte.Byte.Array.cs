using System.Collections.Generic;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public byte[] ReadByteArray(in bool reverse = false)
        {
            return Arrays.GetByteArray(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public void WriteByteArray(in ICollection<byte> value, in bool reverse = false)
        {
            AutomaticExpansion(value.Count);
            Arrays.SetByteArray(ref WriteIndex, value, reverse);
        }
    }

    public partial interface IWrite
    {
        void WriteByteArray(in ICollection<byte> value, in bool reverse = false);
    }

    public partial interface IRead
    {
        byte[] ReadByteArray(in bool reverse = false);
    }
}