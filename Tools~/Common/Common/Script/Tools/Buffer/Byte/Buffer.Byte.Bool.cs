#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    public partial class BufferByte
    {
        #region IReadData Members

        /// <inheritdoc/> 
        public bool ReadBool()
        {
            return Arrays[ReadIndex++] != 0;
        }

        /// <inheritdoc/> 
        public bool[] ReadBoolArray(bool reverse = false)
        {
            return Arrays.GetBoolArray(ref ReadIndex, reverse);
        }

        #endregion

        #region IWriteData Members

        /// <inheritdoc/> 
        public void WriteBool(bool b)
        {
            AutomaticExpansion(1);
            Arrays[WriteIndex++] = (byte)(b ? 1 : 0);
        }

        /// <inheritdoc/> 
        public void WriteBoolArray(ICollection<bool> value, bool reverse = false)
        {
            WriteLen(value.Count);
            AutomaticExpansion(value.Count);
            Arrays.SetBoolArray(ref WriteIndex, value, reverse);
        }

        #endregion
    }
}