#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    public partial class BufferByte
    {
        #region IReadData Members

        /// <inheritdoc/> 
        public byte ReadByte()
        {
            return Arrays[ReadIndex++];
        }

        /// <inheritdoc/> 
        public byte[] ReadByteArray(bool reverse = false)
        {
            return Arrays.GetByteArray(ref ReadIndex, reverse);
        }

        #endregion

        #region IWriteData Members

        /// <inheritdoc/> 
        public void WriteByte(byte value)
        {
            AutomaticExpansion(1);
            Arrays[WriteIndex++] = value;
        }

        /// <inheritdoc/> 
        public void WriteByteArray(ICollection<byte> value, bool reverse = false)
        {
            AutomaticExpansion(value.Count);
            Arrays.SetByteArray(ref WriteIndex, value, reverse);
        }

        #endregion
    }
}