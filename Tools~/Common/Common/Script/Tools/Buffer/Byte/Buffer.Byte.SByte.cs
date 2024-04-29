#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    public partial class BufferByte
    {
        #region IReadData Members

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

        #endregion

        #region IWriteData Members

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

        #endregion
    }
}